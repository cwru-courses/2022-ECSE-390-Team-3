using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transition : MonoBehaviour
{
    public Animator anim;
    public bool isExit;
    private bool inLevel;
    public float openDist; 
    public AudioManager AM;
    private int pastPipeMouth = 0;


    // Start is called before the first frame update
    void Start()
    {
        AM = FindObjectOfType<AudioManager>();        
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

            if (distance < 10f)
            {
                anim.SetBool("playerTouch", true);
                if (AM != null) AM.Play("pipeIn");        
            }

            else
            {
                anim.SetBool("playerNear", false);
            }

        }

        else
        {
            if (distance < 3.4f)
            {
                pastPipeMouth = 1;
            }
            if (distance > 3.5f && pastPipeMouth == 1)
            {
                Debug.Log(distance);
                transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            }


                if (distance < 8f && inLevel == false)
            {
                anim.SetBool("playerNear", true);
                anim.SetBool("playerTouch", true);
                if(AM != null) AM.Play("pipeOut"); 
                inLevel = true;
            }

            if (distance > 8f)
            {
                anim.SetBool("playerNear", false);
                anim.SetBool("playerTouch", false);
                transform.position = new Vector3(transform.position.x, transform.position.y, -1);

            }

            if(distance > 18f)
            {
                inLevel = false;
            }



        }
        

        
    }
}
