using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latch : MonoBehaviour
{
    [SerializeField]
    GameManager GM;

    private bool canLatch;

    private SpriteRenderer SR;
    private Color color;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        color = SR.color;
    }

    void Update()
    {
        if (!canLatch) return;

        if(Input.GetMouseButton(1))
        {
            GM.Latch();
            SR.color = new Color(0, 1, 0);
        }
        else
        {
            GM.Unlatch();
            SR.color = color;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Latchable"))
        {
            canLatch = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // how i would love it if canLatch = collision.gameObject.CompareTag("Latchable"); worked
        if(collision.gameObject.CompareTag("Latchable"))
        {
            canLatch = false;
        }
    }
}
