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
        Debug.Log("hit");
    }

    private void Die()
    {
        //anim.SetTrigger("death");

        //diable player movement
        GetComponentInParent<Player>().enabled = false;
        Debug.Log("died");
        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
