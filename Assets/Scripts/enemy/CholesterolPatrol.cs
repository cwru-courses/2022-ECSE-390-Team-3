using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CholesterolPatrol : MonoBehaviour
{
    public bool mustPatrol;
    private int flipped;
    private double lastOrientation;
    private double currentOrientation;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public Transform wallCheckPos;
    public float walkSpeed;
    public Collider2D bodyCollider;

    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        flipped = 1;
        mustPatrol = true;
        rb.velocity = transform.right * walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (mustPatrol)
        {
            Patrol();
        }

    }

    void Patrol()
    {

        if (Physics2D.OverlapCircle(wallCheckPos.position, 0.1f, groundLayer))
        {
            transform.Rotate(0, 0, -90);
            rb.velocity = transform.right * walkSpeed;
        }

        if (!(Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer)))
        {
            transform.Rotate(0, 180, 0);
            rb.velocity = transform.right * walkSpeed;
        }


    }

}