using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    private Renderer re;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        re = GetComponent<Renderer>();
        re.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<BossDrip>() != null && Vector3.Distance(FindObjectOfType<BossDrip>().transform.position, transform.position) < 1)
        {
            re.enabled = true;
            anim.SetBool("hit", true);
            Destroy(GameObject.FindWithTag("bossDrip"));
            StartCoroutine(remove(.667f));
        }
        
    }

    IEnumerator remove(float time)
    {
        yield return new WaitForSeconds(time);
        re.enabled = false;

    }

    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.tag == "bossDrip")
         {
             re.enabled = true;

         }
     }*/

}
