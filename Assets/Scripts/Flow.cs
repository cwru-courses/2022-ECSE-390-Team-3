using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Flow : MonoBehaviour
{
    [SerializeField]
    GameManager gM;
    BoxCollider2D box;

    [SerializeField]
    float force, duration, cooldown;

    private float durationTimer, cdTimer;
    private bool active;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        cdTimer = 0f;
        durationTimer = 0f;
    }

    void Update()
    {
        if(cdTimer < cooldown)
        {
            cdTimer += Time.deltaTime;
        }
        else if(durationTimer < duration)
        {
            durationTimer += Time.deltaTime;
            if(active) gM.PushPlayer(GetPushWave(), force);
            Debug.DrawRay(transform.position, this.transform.right.normalized * force, Color.red);
        }
        else
        {
            cdTimer = 0f;
            durationTimer = 0f;
        }
    }

    public Vector2 GetPushWave()
    {
        return this.transform.right.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            active = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            active = false;
        }
    }
}
