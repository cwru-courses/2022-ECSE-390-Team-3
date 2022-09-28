using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    GameManager GM;
    Transform target;

    Vector2 proxyPosition;

    Vector2 targetPosition;
    Vector2 transitionPosition;
    Vector2 transitionProxyPosition;

    Vector2 lastPosition;

    Vector2 velocity;

    Bounds bounds;

    float vertEdge;
    float horizEdge;

    bool transitioning;
    bool start;

    void Start()
    {
        start = true;
        bounds = new Bounds();

        target = GM.GetPlayer();

        vertEdge = Camera.main.orthographicSize;
        horizEdge = Camera.main.orthographicSize * Camera.main.aspect; // multiply by aspect ratio
    }

    void LateUpdate()
    {
        lastPosition = transform.position;

        if(transitioning)
        {
            ScreenTransition();
            return;
        }

        proxyPosition = Vector2.SmoothDamp(proxyPosition, target.position, ref velocity, 0.2f);

        targetPosition.x = Mathf.Clamp(proxyPosition.x, bounds.xMin, bounds.xMax);
        targetPosition.y = Mathf.Clamp(proxyPosition.y, bounds.yMin, bounds.yMax);

        transform.position = new Vector3(targetPosition.x, targetPosition.y, -10.0f);

        start = false;
    }

    public void UpdateScreen()
    {
        if (start) return;
        transitionProxyPosition = lastPosition;
        transitioning = true;
    }

    public void UpdateScreenBounds(BoxCollider2D box)
    {
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


    public struct Bounds
    {
        public float xMin, xMax;
        public float yMin, yMax;
    }
}
