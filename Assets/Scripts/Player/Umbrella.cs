using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    GameManager GM;
    AudioManager AM;
    Transform pivot;
    Player player;
    SpriteRenderer SR;
    Color color;

    [SerializeField]
    Animator playerAnim;
    [SerializeField]
    GameObject soundSources;

    float glide = 0.25f;
    [SerializeField]
    [Tooltip("the maximum speed allowed to be gained from 'catching' wind")]
    float maxGlide = 1.5f;


    float repulsion = 0.25f;

    [SerializeField]
    [Tooltip("the max magnitude of impulse due opening in a wave")]
    float impulse = 10f;
    [SerializeField]
    [Tooltip("%length of the wave which grants max push")]
    float forgiveness = 0.2f;

    private Wave wave;

    private bool inWave;
    private bool inWind;

    [SerializeField]
    float jumpCd = 2f;
    float jumpTimer = 2f;

    private float dashFreezeFrames = 2f;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        AM = FindObjectOfType<AudioManager>();
        pivot = GetComponentInParent<Transform>();
        player = GetComponentInParent<Player>();
        SR = GetComponent<SpriteRenderer>();
        color = SR.color;
    }

    void Update()
    {
        if (jumpTimer <= jumpCd) jumpTimer += Time.deltaTime;
        // Debug.Log(jumpTimer);

        if(Input.GetMouseButtonDown(0))
        {
            if (jumpTimer >= jumpCd)
            {
                player.ResetGravity();
                jumpTimer = 0f;
            }
            playerAnim.SetBool("openUmbrella", true);
            AudioSource latchSound = soundSources.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
            latchSound.PlayOneShot(latchSound.clip);
            Debug.Log("open");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            playerAnim.SetBool("openUmbrella", false);
        }

        // Refactor please
        // Thoughts: Make it a different number than 90 degrees so the player has a bit of leeway
        // Also you could combine these because the same calcultion is repeaed
        // actually the only one we're using right now is wave percentage which is easy
        if (inWave) {
            Vector2 direction = Quaternion.AngleAxis(pivot.eulerAngles.z, Vector3.forward) * Vector2.up;
            float angle = Vector2.Angle(wave.GetDirection(), direction);

            if (angle < 90f)
            {
                float percentage = wave.GetPositionPercentage(transform.position);

                percentage = Mathf.Clamp(percentage, percentage + forgiveness, 1);
                SR.color = new Color(1 - percentage, percentage, 0);
            }
        }

        if (Input.GetMouseButtonDown(0) && inWave)
        {
            // wave should never be null when this is executed

            Vector2 direction = Quaternion.AngleAxis(pivot.eulerAngles.z, Vector3.forward) * Vector2.up;
            float angle = Vector2.Angle(wave.GetDirection(), direction);

            if (angle < 90f)
            {
                AM.Play("dash");
                float percentage = wave.GetPositionPercentage(transform.position);
                float impulse = wave.GetMaxImpulse();

                percentage = Mathf.Clamp(percentage, percentage + forgiveness, 1);

                player.ApplyImpulse(direction * percentage * impulse);

                SR.color = new Color(1 - percentage, percentage, 0);

                StartCoroutine(freezeOnDash());
            }
        }
        else if (Input.GetMouseButton(0) && inWind)
        {
            UmbrellaInWindOld();
        }   
    }

    void UmbrellaInWind()
    {
        Vector2 direction = Quaternion.AngleAxis(pivot.eulerAngles.z, Vector3.forward) * Vector2.up;
        Vector2 velocity = Vector2.zero;

        // now we do calculations for every active wind
        // in case we have multiple at once
        // though we probably shouldn't
        foreach(Wind wind in GM.GetWinds())
        {
            float windSpeed = wind.GetSpeed();
            Vector2 windDirection = wind.GetVelocity().normalized;
            // calculate the angle
            float angle = Vector2.Angle(windDirection, direction);

            // componentize the direction vector relative to wind direction
            Vector2 parallelComponent = Vector3.Project(direction, windDirection);
            Vector2 normalComponent = Vector3.Project(direction, Vector2.Perpendicular(windDirection));

            // these components are generate with magnitudes between 0-1
            // which is defined as the normalized component of the velocity

            parallelComponent *= (90f - angle) / 90f;
            normalComponent *= angle / 90f;

            // if facing against the wind
            if (angle > 90)
            {
                parallelComponent *= -1;
                parallelComponent *= repulsion * windSpeed;
            }
            else
            {
                parallelComponent *= Mathf.Min(glide * windSpeed, maxGlide);
                normalComponent *= Mathf.Min(glide * windSpeed, maxGlide);
            }

            // finally use this to generate velocity
            Vector2 thisVelocity = parallelComponent + normalComponent;

            velocity += thisVelocity;
        }

        GM.SetUmbrellaVelocity(velocity);
        GM.SetUmbrellaStatus(true);

        Debug.DrawRay(transform.position, velocity * 5f, Color.blue);
        // Debug.Log(velocity);

        if (!inWave) SR.color = new Color(1, 0.6f, 0);
    }

    // Freezes the player for a few frames when catching a wave
    IEnumerator freezeOnDash() {
        GM.SetFreeze(true);
        yield return new WaitForSeconds(Time.deltaTime * dashFreezeFrames);
        GM.SetFreeze(false);
    }

    void UmbrellaInWindOld()
    {
        Vector2 direction = Quaternion.AngleAxis(pivot.eulerAngles.z, Vector3.forward) * Vector2.up;
        GM.SetUmbrellaVelocity(direction * maxGlide);
        GM.SetUmbrellaStatus(true);
        Debug.DrawRay(transform.position, direction * 7.5f, Color.blue);
        // Debug.Log(velocity);

        if (!inWave) SR.color = new Color(1, 0.6f, 0);
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
