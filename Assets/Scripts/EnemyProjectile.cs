using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    public float fireTime = 1f;
    float nextFire;
    public float range = 10f;
    GameObject target;
    public Animator animator;


    void Start()
    {
        nextFire = Time.time;
        target = GameObject.FindGameObjectWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = new Vector3(1.5f, 1.5f, 1);
        transform.localScale = scale;
        animator.SetBool("inRange", false);

        if (Vector3.Distance(target.transform.position, transform.position) <= range)
        {
            CheckIfTimeToFire();
            animator.SetBool("inRange", true);
        }
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {

            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireTime;
        }
    }
}
