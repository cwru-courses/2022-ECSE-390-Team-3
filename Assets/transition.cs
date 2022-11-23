using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Animator anim;
    public bool isExit;
    private bool inLevel;
    public float openDist;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isExit", isExit);

    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(FindObjectOfType<Player>().transform.position, transform.position);

        if (isExit)
        {
            if (distance < openDist)
            {
                anim.SetBool("playerNear", true);
            }

            if (distance < 8.5f)
            {
                anim.SetBool("playerTouch", true);
            }

            else
            {
                anim.SetBool("playerNear", false);
            }

        }

        else
        {

            
            if (distance < 8f && inLevel == false)
            {
                anim.SetBool("playerNear", true);
                anim.SetBool("playerTouch", true);
                inLevel = true;
            }

            if (distance > 8f)
            {
                anim.SetBool("playerNear", false);
                anim.SetBool("playerTouch", false);

            }

            if(distance > 25)
            {
                inLevel = false;
            }

        }
        

        
    }
}
