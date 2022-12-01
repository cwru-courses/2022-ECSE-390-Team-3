using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    GameManager GM;
    Transform target;

    BoxCollider2D thisBox;

    int PPU = 32;

    Vector2 proxyPosition;

    Vector2 targetPosition;
    Vector2 transitionPosition;
    Vector2 transitionProxyPosition;

    Vector2 lastPosition;

    Vector2 velocity;

    Bounds bounds;

    float vertEdge;
    float horizEdge;

    float initialVertEdge;
    float initialHorizEdge;

    bool transitioning;
    bool start;

    bool zoomOut;
    bool unZoom;

    // float shakeTimer;
    // float shakeDuration = 0.1f;
    int currentShakeIndex = 10000;
    float[] screenShakeKeyFrames = {3, 3, -2, -2, -2, 2, 2, 2, -1, -1, 0};
    float shakeMagnitude = 0.04f;
    Vector2 shakeDir;

    private bool locked;
    public bool stopCam;

    float currentSize;
    [SerializeField]
    [Tooltip("amount zoomed, ei 1.25 = 125%")]
    float zoomAmount = 1.75f;
    [SerializeField]
    [Tooltip("time for zoom/unzoom")]
    float zoomDuration = 0.1f;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        start = true;
        bounds = new Bounds();
        locked = true;

        // shakeTimer = shakeDuration;

        target = GM.GetPlayer();

        initialVertEdge = vertEdge = Camera.main.orthographicSize;
        initialHorizEdge = horizEdge = Camera.main.orthographicSize * Camera.main.aspect; // multiply by aspect ratio
    }

    private void Update()
    {
        if(zoomOut)
        {
            Zoom(initialVertEdge * zoomAmount);
        }
        else if (unZoom)
        {
            Zoom(initialVertEdge);
        }
    }
    void LateUpdate()
    {

        lastPosition = transform.position;

        if (stopCam) {
            return;
        }

        if(!locked)
        {
            UnlockedFollow();
            return;
        }

        if(transitioning)
        {
            ScreenTransition();
            return;
        }

        proxyPosition = Vector2.SmoothDamp(proxyPosition, target.position, ref velocity, 0.2f);

        targetPosition.x = Mathf.Clamp(Mathf.Round(proxyPosition.x * PPU) / PPU, bounds.xMin, bounds.xMax);
        targetPosition.y = Mathf.Clamp(Mathf.Round(proxyPosition.y * PPU) / PPU, bounds.yMin, bounds.yMax);

        transform.position = new Vector3(targetPosition.x, targetPosition.y, -10.0f);

        // if(currentShakeIndex < screenShakeKeyFrames.Length)
        // {
        //     transform.localPosition += screenShakeKeyFrames[currentShakeIndex] * shakeMagnitude * -(Vector3)shakeDir;
        //     currentShakeIndex += 1;
        // }

        // if(shakeTimer < shakeDuration)
        // {
        //     transform.localPosition += Random.value * shakeMagnitude * -(Vector3)shakeDir;
        //     shakeTimer += Time.deltaTime;
        // }

        start = false;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void ToggleScreenLock(bool _locked)
    {
        locked = _locked;
    }

    public void UpdateScreen()
    {
        if (start) return;
        transitionProxyPosition = lastPosition;
        transitioning = true;
    }

    public void UpdateScreenBounds(BoxCollider2D box)
    {
        thisBox = box;

        bounds.xMin = box.bounds.min.x + horizEdge;
        bounds.xMax = box.bounds.max.x - horizEdge;
        bounds.yMin = box.bounds.min.y + vertEdge;
        bounds.yMax = box.bounds.max.y - vertEdge;

        if (box.bounds.max.x - box.bounds.min.x < 2 * horizEdge)
        {
            bounds.xMin = bounds.xMax = (int)(box.bounds.min.x + ((box.bounds.max.x - box.bounds.min.x) / 2));
        }
        if (box.bounds.max.y - box.bounds.min.y < 2 * vertEdge)
        {
            bounds.yMin = bounds.yMax = (int)(box.bounds.min.y + ((box.bounds.max.y - box.bounds.min.y) / 2));
        }
    }

    void ScreenTransition()
    {
        targetPosition = new Vector3(
                                     Mathf.Clamp(target.position.x, bounds.xMin, bounds.xMax),
                                     Mathf.Clamp(target.position.y, bounds.yMin, bounds.yMax),
                                     -10f);

        transitionProxyPosition = Vector2.SmoothDamp(transitionProxyPosition, targetPosition, ref velocity, 0.2f);

        transform.position = new Vector3(
                                         transitionProxyPosition.x,
                                         transitionProxyPosition.y,
                                         -10f);

        if((targetPosition - (Vector2)transform.position).magnitude < 0.05f)
        {
            proxyPosition = lastPosition;
            transitioning = false;
        }
    }

    void UnlockedFollow()
    {
        // fuck my camera system was so bad
        proxyPosition = Vector2.SmoothDamp(proxyPosition, target.position, ref velocity, 0.2f);
        transform.position = new Vector3(proxyPosition.x, proxyPosition.y, -10f);

        // if(shakeTimer < shakeDuration)
        // {
        //     transform.localPosition += Random.value * shakeMagnitude * -(Vector3)shakeDir;
        //     shakeTimer += Time.deltaTime;
        // }
        // if(currentShakeIndex < screenShakeKeyFrames.Length)
        // {
        //     transform.localPosition += screenShakeKeyFrames[currentShakeIndex] * shakeMagnitude * -(Vector3)shakeDir;
        //     currentShakeIndex += 1;
        // }
    }

    public void Shake(Vector2 dir)
    {
        shakeDir = dir.normalized;
        // shakeTimer = 0f;
        currentShakeIndex = 0;
    }

    void Zoom(float targetSize)
    {
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetSize, ref currentSize, zoomDuration);
        vertEdge = Camera.main.orthographicSize;
        horizEdge = Camera.main.orthographicSize * Camera.main.aspect;
        UpdateScreenBounds(thisBox);
    }

    public void ZoomOut()
    {
        unZoom = false;
        zoomOut = true;
    }

    public void Unzoom()
    {
        zoomOut = false;
        unZoom = true;
    }

    public void Rotate()
    {
        RotateBounds();
    }

    private void RotateBounds()
    {
        Bounds rotatedBounds = new Bounds();
        rotatedBounds.xMin = bounds.yMin;
        rotatedBounds.xMax = bounds.yMax;
        rotatedBounds.yMin = bounds.xMax;
        rotatedBounds.yMax = bounds.xMax;

        bounds = rotatedBounds;
    }

    public struct Bounds
    {
        public float xMin, xMax;
        public float yMin, yMax;
    }
}
