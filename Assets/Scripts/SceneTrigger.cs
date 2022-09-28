using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string NextScene = "";

private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
        Debug.Log("Loading" + NextScene);
        SceneManager.LoadScene(NextScene);
        }
    }
}
