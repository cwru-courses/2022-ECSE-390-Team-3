using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Wind : MonoBehaviour
{
    [SerializeField]
    GameManager GM;

    [SerializeField]
    float windSpeed;
    Vector2 direction;
    Vector2 currVelocity;
    Vector2 refVelocity;

    SpriteRenderer SR;
    Color color;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        color = SR.color;
        direction = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * Vector2.right;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, direction * 5f, Color.cyan);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SR.color = Color.green;
            GM.AddWind(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SR.color = color;
            GM.RemoveWind(this);
        }
    }

    public Vector2 GetVelocity()
    {
        return direction.normalized * windSpeed;
    }

    public Vector2 GetCurrVelocity()
    {
        return currVelocity;
    }

    public ref Vector2 GetRefVelocity()
    {
        return ref refVelocity;
    }

    public void SetCurrVelocity(Vector2 vel)
    {
        currVelocity = vel;
    }
}
