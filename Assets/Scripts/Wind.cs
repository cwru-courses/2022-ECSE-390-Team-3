using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Wind : MonoBehaviour
{
    [SerializeField]
    bool windOn, waveOn;

    GameManager GM;

    [SerializeField]
    float windSpeed;
    Vector2 direction;
    Vector2 currVelocity;
    Vector2 refVelocity;

    BoxCollider2D box;
    SpriteRenderer SR;
    Color color;
    
    [SerializeField]
    GameObject wave;
    [SerializeField]
    int numWaves = 1;
    [SerializeField]
    [Tooltip("length of wave in unity units")]
    float waveLength = 2f;
    [SerializeField]
    [Tooltip("speed of wave")]
    float speed = 15f;
    [SerializeField]
    [Tooltip("the maximum impulse from catching the wave with umbrella")]
    float maxImpulse = 10f;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        SR = GetComponent<SpriteRenderer>();
        color = SR.color;

        // do not change rotation mid-game
        direction = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * Vector2.right;

        if (!waveOn) return;

        StartCoroutine(InitializeWaves(numWaves));
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SR.color = new Color(0, 1, 0, 0.1f);

            if (windOn) GM.AddWind(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SR.color = color;
            if (windOn) GM.RemoveWind(this);
        }
    }

    private void SpawnWave()
    {
        GameObject waveObject = Instantiate(wave);
        Wave waveSettings = waveObject.GetComponent<Wave>();

        Quaternion rotation = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward);

        Vector2 start = box.bounds.center + (rotation * Vector2.left * (transform.localScale.x - waveLength) / 2);
        Vector2 end = box.bounds.center + (rotation * Vector2.right * (transform.localScale.x - waveLength) / 2);

        Debug.DrawLine(start, end, Color.cyan);

        waveSettings.SetDimensions(waveLength, transform.localScale.y);
        waveSettings.SetRotation(transform.eulerAngles.z);
        waveSettings.SetPath(start, end);
        waveSettings.SetSpeed(speed);
        waveSettings.SetMaxImpulse(maxImpulse);
    }

    public Vector2 GetVelocity()
    {
        return direction.normalized * windSpeed;
    }

    public Vector2 GetCurrVelocity()
    {
        return currVelocity;
    }

    public ref Vector2 GetRefVelocity()
    {
        return ref refVelocity;
    }

    public void SetCurrVelocity(Vector2 vel)
    {
        currVelocity = vel;
    }

    public void SetGameManager(GameManager _GM)
    {
        GM = _GM;
    }

    IEnumerator InitializeWaves(int num)
    {
        float time = ((this.transform.localScale.x - waveLength) / speed) / (float)num;

        for (int i = 0; i < num; i++)
        {
            SpawnWave();
            yield return new WaitForSeconds(time);
        }

        yield return null;
    }
}
