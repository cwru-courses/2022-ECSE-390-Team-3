using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    [SerializeField]
    GameManager GM;
    Transform pivot;
    Player player;

    [SerializeField]
    [Tooltip("the additive velocity due to an open umbrella")]
    float umbrelocity = 2f;
    [SerializeField]
    [Tooltip("the max magnitude of impulse due opening in a wave")]
    float umbrimpulse = 10f;

    void Start()
    {
        pivot = GetComponentInParent<Transform>();
        player = GetComponentInParent<Player>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(Input.GetMouseButton(0))
        {
            if(collision.gameObject.CompareTag("Wind"))
            {
                Vector2 velocity = Quaternion.AngleAxis(pivot.eulerAngles.z, Vector3.forward) * Vector2.up;
                velocity *= umbrelocity;
                GM.SetUmbrellaVelocity(velocity);
                GM.SetUmbrellaStatus(true);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            GM.SetUmbrellaStatus(false);
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (collision.gameObject.CompareTag("Wave"))
            {
                Wave wave = collision.gameObject.GetComponentInParent<Wave>();
                Vector2 direction = Quaternion.AngleAxis(pivot.eulerAngles.z, Vector3.forward) * Vector2.up;
                float angle = Vector2.Angle(wave.GetDirection(), direction);
                if (angle < 90f)
                {
                    float percentage = wave.GetPositionPercentage(transform.position);
                    float impulse = wave.GetMaxImpulse();
                    //Debug.Log(percentage);
                    player.ApplyImpulse(direction * percentage * impulse);
                }
            }
        }
    }
}
