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
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, 0);
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
   
        if (Physics2D.OverlapCircle(wallCheckPos.position, 0.1f, groundLayer)){
            lastOrientation = transform.rotation.z;
           
            if(flipped > 0)
                transform.Rotate(0, 0, 270);
            else if(flipped < 0)
                transform.Rotate(0, 0, 90);

            currentOrientation = transform.rotation.z;
        }

        
        if (flipped > 0)
        {
            if (lastOrientation < 0.001 && lastOrientation >= 0 && currentOrientation < 0.8 && currentOrientation > 0)
                rb.velocity = new Vector2(0, -walkSpeed * Time.fixedDeltaTime);
                

            else if (lastOrientation < 0 && currentOrientation == 1)
                rb.velocity = new Vector2(-walkSpeed * Time.fixedDeltaTime, 0);
                

            else if (lastOrientation == 1 && currentOrientation < 0 && currentOrientation > -0.8)
                rb.velocity = new Vector2(0, walkSpeed * Time.fixedDeltaTime);

            else if (lastOrientation > 0 && lastOrientation < 0.8 && currentOrientation > -0.001 && currentOrientation <= 0)
                rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, 0);
                
        }
        else if(flipped < 0)
        {
            if (lastOrientation >= 0 && lastOrientation < 0.001 && currentOrientation > 0 && currentOrientation < 0.8)
                rb.velocity = new Vector2(0, walkSpeed * Time.fixedDeltaTime);

            else if (lastOrientation > 0 && lastOrientation < 0.8 && currentOrientation == 1)
                rb.velocity = new Vector2(-walkSpeed * Time.fixedDeltaTime, 0);
               

            else if (lastOrientation == 1 && currentOrientation > 0 && currentOrientation < 0.8)
                rb.velocity = new Vector2(0, -walkSpeed * Time.fixedDeltaTime);
                

            else if (lastOrientation < 0 && lastOrientation > -0.8 && currentOrientation < 0 && currentOrientation > -0.001)
                rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, 0);

        }



        if (!(Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer)))
        {
            Flip();               

        }
        
    }

    void Flip()
    {
        
        if (rb.velocity.y == 0)
        {
            rb.velocity = new Vector2((-rb.velocity.x * 50) * Time.fixedDeltaTime, 0);
            walkSpeed = rb.velocity.x * 50;
            Debug.Log(walkSpeed);
            //Debug.Log(rb.velocity.x * 50);
        }
          
        if (rb.velocity.x == 0)
        {
            rb.velocity = new Vector2(0, (-(rb.velocity.y) * 50) * Time.fixedDeltaTime);
            walkSpeed = rb.velocity.y * 50;
            Debug.Log(walkSpeed);
            //Debug.Log(rb.velocity.y * 50);

        }

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        flipped *= -1;
        //Debug.Log(flipped);
        
    }

    //flipy

}


/*
 *   if (rb.velocity.x == 0)
        {
            rb.velocity = new Vector2(0, walkSpeed * Time.fixedDeltaTime);
            Debug.Log("Vertical flip");

        }
            

        else if (rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, 0);
          
        } 

*/
