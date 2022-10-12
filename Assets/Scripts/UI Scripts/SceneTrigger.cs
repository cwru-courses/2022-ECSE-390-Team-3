using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string NextScene = "";
    public float delay = 0;
    public GameObject PopUpToOpen = null;
    public string TriggerType = "";
    public Animator anim;
    private bool popupOpen = false;
    private bool popupHasBeenTriggered = false;

    private void Update()
    {
        if(popupOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Resume();
                popupOpen = false;
                PauseMenu.GamePaused = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (TriggerType == "Transition")
            {
                ChangeScene();
            }

            if (TriggerType == "PopUp" && popupHasBeenTriggered == false)
            {
                if (PopUpToOpen != null)
                {
                    Pop();
                    popupOpen = true;
                }
                else
                {
                    Debug.Log("Missing Pop-Up. Please add one");
                }
            }
        }
    }
 
    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(NextScene);
    }

    private void ChangeScene()
    { 
        Debug.Log("Loading" + NextScene);
        anim.SetBool("FadeIn?", true);
        StartCoroutine(LoadLevelAfterDelay(delay));
    }

    private void Pop()
    {
        popupHasBeenTriggered = true;
        Time.timeScale = 0f;
        PopUpToOpen.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        PopUpToOpen.SetActive(false);
    }
}