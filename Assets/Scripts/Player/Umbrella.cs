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
    float glide = 2f;
    [SerializeField]
    [Tooltip("the max magnitude of impulse due opening in a wave")]
    float impulse = 10f;
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
            // unlike wave, we talk to the GM for this

            // here's the idea:
            // we split the direction of the umbrella into its components, projected onto the wind direction
            // that is, the projection parallel to the wind, and the resulting perpendicular piece
            // we use the angle of the original direction on the wind direction to determine how to adjust these componenets
            // then return the new vector sum

            // calculate direction vector
            // also occurs in the other method but like idk
            // should probably be something innate to player
            Vector2 direction = Quaternion.AngleAxis(pivot.eulerAngles.z, Vector3.forward) * Vector2.up;
            Debug.Log(direction.y);

            // now we want to grab the current wind direction
            // multiple winds may be active but i'm just going to grab the most recent for now
            Vector2 windDirection = GM.GetCurrentWindDirection();

            // calculate the angle
            float angle = Vector2.Angle(windDirection, direction);

            // generate components
            Vector2 parallelComponent = Vector3.Project(direction, windDirection);
            Vector2 normalComponent = Vector3.Project(direction, Vector2.Perpendicular(windDirection));

            // if the angle is 0 (90 - angle) we make no adjustment
            // for every percent off, we reduce the parallel componenet
            // and increase the normal component proportionally

            parallelComponent *= (90f - angle) / 90f;
            normalComponent *= angle / 90f;

            // the effect of this is that opening the umbrella will give an impulse
            // this impulse will have the feeling of pushing AGAINST either
            // the wind or
            // gravity
            /*
            if (angle > 90) parallelComponent *= -1;
            if (direction.y < 0) normalComponent *= -1;
            */

            // finally use this to generate velocity
            Vector2 velocity = parallelComponent + normalComponent;

            velocity *= glide;
            GM.SetUmbrellaVelocity(velocity);
            GM.SetUmbrellaStatus(true);

            Debug.DrawRay(transform.position, velocity, Color.blue);

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
