using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    /*[SerializeField]
    GameManager GM;*/

    float moveSpeed = 7f;

    Rigidbody2D rb;

    GameObject target;
    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
        //GM = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Player"))
        {
            Debug.Log("HIT!!");
            //GM.PlayerDeath();
            Destroy(gameObject);
        }
    }
}
