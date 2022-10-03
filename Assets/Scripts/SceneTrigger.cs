using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string NextScene = "";
    public GameObject PopUpToOpen = null;
    public string TriggerType = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (TriggerType == "Transition")
            {
                ChangeScene();
            }

            if (TriggerType == "PopUp")
            {
                if (PopUpToOpen != null)
                {
                    Pop();
                }
                else
                {
                    Debug.Log("Missing Pop-Up. Please add one");
                }
            }
        }
    }

    private void ChangeScene()
    { 
        Debug.Log("Loading" + NextScene);
        SceneManager.LoadScene(NextScene);
    }

    private void Pop()
    {
        Time.timeScale = 0f;
        PopUpToOpen.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        PopUpToOpen.SetActive(false);
    }
}
