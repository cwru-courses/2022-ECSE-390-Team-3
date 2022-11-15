using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public float speed = 7f;
    public AudioSource deathSound;
    float duration = 5f;
    Transform target;
    Vector2 moveDirection;
    GameObject hazardWall;
    GameObject latchableWall;
    GameManager GM;


    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = (target.position - transform.position).normalized;
        StartCoroutine(Despawn(duration));
        hazardWall = GameObject.FindGameObjectsWithTag("Hazard")[0];
        latchableWall = GameObject.FindGameObjectsWithTag("Latchable")[0];
    }

    void Update()
    {
        Vector2 velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            GM.PlayerDeath();
            SpeedrunStats.playerDeath();
            deathSound.PlayOneShot(deathSound.clip);
        }
        else if (collision.gameObject == latchableWall)
        {
            Debug.Log("hit the wall");
            Destroy(this.gameObject);
        }
        else if (collision.gameObject == hazardWall)
        {
            Debug.Log("hit the hazard wall");
            Destroy(this.gameObject);
        }
    }

    // three methods for when an object instantiates the bullet, it can override the default speed, duration, and target
    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void SetLifeTime(float _duration)
    {
        duration = _duration;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    IEnumerator Despawn(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }
}
