using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    public float fireTime = 1f;
    float nextFire;
    public float range = 10f;
    public float shootSpeed = 7f;
    GameObject target;
    Animator animator;
    PlayAudioInRange audioPlayer;

    private bool frozen;

    void Start()
    {
        nextFire = Time.time;
        target = GameObject.FindGameObjectWithTag("Player");
        audioPlayer = transform.GetComponent<PlayAudioInRange>();
        animator = GetComponent<Animator>();
        bullet.GetComponent<Bullet>().speed = shootSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (frozen) return;

        Vector3 scale = new Vector3(1.5f, 1.5f, 1);
        transform.localScale = scale;
        //animator.SetBool("inRange", false);

        if (Vector3.Distance(target.transform.position, transform.position) <= range)
        {
            CheckIfTimeToFire();
            //animator.SetBool("inRange", true);
        }
    }

    public void SetFreeze(bool _frozen)
    {
        frozen = _frozen;
    }

    private bool isCoroutineExecuting = false;

    IEnumerator ExecuteAfterTime(float time)
    {
        if (isCoroutineExecuting)
            yield break;

        audioPlayer.playAudio();

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        Instantiate(bullet, transform.position, Quaternion.identity);
        //audioPlayer.playAudio();
        Debug.Log("should play");
        nextFire = Time.time + fireTime;

        isCoroutineExecuting = false;
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            animator.Play("projectile enemy");
            StartCoroutine(ExecuteAfterTime(1f));
            /*Instantiate(bullet, transform.position, Quaternion.identity);
            audioPlayer.playAudio();
            Debug.Log("should play");
            nextFire = Time.time + fireTime;*/
        }
    }
}
