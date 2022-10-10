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


    void Start()
    {
        nextFire = Time.time;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.transform.position, transform.position) <= range)
        {
            CheckIfTimeToFire();
        }
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireTime;
        }
    }
}
