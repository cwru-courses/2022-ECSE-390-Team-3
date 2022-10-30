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
    Animator animator;
    PlayAudioInRange audioPlayer;


    void Start()
    {
        nextFire = Time.time;
        target = GameObject.FindGameObjectWithTag("Player");
        audioPlayer = transform.GetComponent<PlayAudioInRange>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = new Vector3(1.5f, 1.5f, 1);
        transform.localScale = scale;
        //animator.SetBool("inRange", false);

        if (Vector3.Distance(target.transform.position, transform.position) <= range)
        {
            CheckIfTimeToFire();
            //animator.SetBool("inRange", true);
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(1);
        Instantiate(bullet, transform.position, Quaternion.identity);
        audioPlayer.playAudio();
        Debug.Log("should play");
        nextFire = Time.time + fireTime;
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            animator.Play("projectile enemy");
            StartCoroutine(waiter());
            /*Instantiate(bullet, transform.position, Quaternion.identity);
            audioPlayer.playAudio();
            Debug.Log("should play");
            nextFire = Time.time + fireTime;*/
        }
    }
}
