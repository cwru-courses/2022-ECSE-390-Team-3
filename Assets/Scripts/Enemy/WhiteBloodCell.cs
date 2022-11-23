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
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame

    //anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5
    void Update()
    {
        if (bonked && anim.GetCurrentAnimatorStateInfo(0).IsName("wbc swim") )
        {

          /*  Vector3 vectorToTarget = patrolPoints[pointIndex].position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 100000);*/

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
            gameManager.GetComponent<GameManager>().Rotate90();
            if (pointIndex < patrolPoints.Length - 1) pointIndex++;
            bonked = true;

            // FindObjectOfType<Player>().bonkedBoss(new Vector2(0, 350));
            collision.gameObject.GetComponentInParent<Player>();
           
        }

    }

}
