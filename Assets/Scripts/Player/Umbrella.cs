using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    [SerializeField]
    GameManager GM;
    Transform pivot;
    Player player;
    SpriteRenderer SR;
    Color color;

    [SerializeField]
    [Tooltip("the additive velocity due to an open umbrella")]
    float umbrelocity = 2f;
    [SerializeField]
    [Tooltip("the max magnitude of impulse due opening in a wave")]
    float umbrimpulse = 10f;
    [SerializeField]
    [Tooltip("%length of the wave which grants max push")]
    float forgiveness = 0.2f;

    private Wave wave;

    private bool inWave;
    private bool inWind;

    void Start()
    {
        pivot = GetComponentInParent<Transform>();
        player = GetComponentInParent<Player>();
        SR = GetComponent<SpriteRenderer>();
        color = SR.color;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inWave)
        {
            // wave should never be null when this is executed

            Vector2 direction = Quaternion.AngleAxis(pivot.eulerAngles.z, Vector3.forward) * Vector2.up;
            float angle = Vector2.Angle(wave.GetDirection(), direction);

            if (angle < 90f)
            {
                float percentage = wave.GetPositionPercentage(transform.position);
                float impulse = wave.GetMaxImpulse();

                percentage = Mathf.Clamp(percentage, percentage + forgiveness, 1);
                player.ApplyImpulse(direction * percentage * impulse);

                SR.color = new Color(1 - percentage, percentage, 0);
            }
        }
        else if (Input.GetMouseButton(0) && inWind)
        {
            Vector2 velocity = Quaternion.AngleAxis(pivot.eulerAngles.z, Vector3.forward) * Vector2.up;
            velocity *= umbrelocity;
            GM.SetUmbrellaVelocity(velocity);
            GM.SetUmbrellaStatus(true);

            if (!inWave) SR.color = new Color(1, 0.6f, 0);
        }
        
    }

    void LateUpdate()
    {
        if(Input.GetMouseButtonUp(0) || !inWind)
        {
            GM.SetUmbrellaStatus(false);
            SR.color = color;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wave"))
        {
            inWave = true;
            wave = collision.gameObject.GetComponentInParent<Wave>();
        }

        if (collision.gameObject.CompareTag("Wind"))
        {
            // later, we will send angle information to GameManager to let it compute the appropriate vector
            // for now, we'll just add an arbitrary amount of velocity
            inWind = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wave"))
        {
            inWave = false;
            // wave = null
        }

        if (collision.gameObject.CompareTag("Wind"))
        {
            inWind = false;
            // this should never occur in the final game
        }
    }
}