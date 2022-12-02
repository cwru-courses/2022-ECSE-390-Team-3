using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrip : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    public Sprite[] drops;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        drip();



    }

    void drip()
    {
        var speed = rb.velocity.magnitude;
        if (speed < 5)
        {
            spr.sprite = drops[0];
        }
        else if (speed > 5)
        {
            spr.sprite = drops[1];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Latchable"))
        {
            splat();
        }
    }
}
