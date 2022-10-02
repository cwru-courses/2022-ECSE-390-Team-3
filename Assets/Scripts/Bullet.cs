using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 7f;
    float duration = 5f;
    Transform target;
    Vector2 moveDirection;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = (target.position - transform.position).normalized;
        StartCoroutine(Despawn(duration));
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
            Debug.Log("hit");
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
