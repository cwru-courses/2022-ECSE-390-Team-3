using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latch : MonoBehaviour
{
    [SerializeField]
    GameManager GM;

    [SerializeField]
    Animator playerAnim;

    //[SerializeField]
    //[Tooltip("latch cooldown")]
    private float cooldown = 1f;

    private bool canLatch;
    private bool latched;

    private bool onCd;
    private float timer;

    //private SpriteRenderer SR;
    private Color color;

    void Start()
    {
        //SR = GetComponent<SpriteRenderer>();
        //color = SR.color;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        timer = cooldown;
    }

    void Update()
    {
        onCd = timer < cooldown;

        if(!latched && canLatch && Input.GetMouseButtonDown(1) && !onCd)
        {
            latched = true;
            GM.Latch();
            //SR.color = new Color(0, 1, 0);
            playerAnim.SetBool("latchOn", true);
        }
        else if((Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) && latched)
        {
            latched = false;
            GM.Unlatch();
            //SR.color = color;
            playerAnim.SetBool("latchOn", false);

            timer = 0f;
        }

        if (timer < cooldown) timer += Time.deltaTime;

        Debug.Log(timer);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Latchable"))
        {
            canLatch = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // how i would love it if canLatch = collision.gameObject.CompareTag("Latchable"); worked
        if(collision.gameObject.CompareTag("Latchable"))
        {
            canLatch = false;
        }
    }

    public void Reset()
    {
        latched = false;
        timer = cooldown;
    }
}
