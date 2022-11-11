using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBloodCell : MonoBehaviour
{
    public Animator anim;
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
    private bool bonked;
    private int pointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bonked && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[pointIndex].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[pointIndex].position) < .002f)
            {
                anim.SetBool("unbonk", true);
                anim.SetBool("bonked", false);
                
            }

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("bonked", true);
            anim.SetBool("unbonk", false);
            if (pointIndex < patrolPoints.Length - 1) pointIndex++;
            bonked = true;

           FindObjectOfType<Player>().bonkedBoss(new Vector2(0, 350));
           
        }

    }

}
