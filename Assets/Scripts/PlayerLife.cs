using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{

    //private Animator anim;

    private void Start()
    {
        //anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Die();
        }
    }

    private void Die()
    {
        //anim.SetTrigger("death");

        //diable player movement
        //GetComponentInParent<Player>().enabled = false;
        // there is really no reason to do that if you immediately reload the scene afterwards;
        // we won't need an animator to do timer, it'll be handled in gamemanager
        Debug.Log("died");
        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
