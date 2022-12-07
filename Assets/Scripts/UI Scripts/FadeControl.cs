using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeControl : MonoBehaviour
{
    public GameObject fadeScreen;
    public Animator anim;
    public string NextScene = "";
    public AudioManager AM;
    public bool isCredits = false;

    public void Start()
    {
        AM = FindObjectOfType<AudioManager>();        
        fadeScreen.SetActive(true);
        anim.SetBool("FadeIn?", true);
        anim.SetBool("FadeOut?", false);

        if(isCredits == true) StartCoroutine(LoadLevelAfterDelayCredits());
    }

    public void Update()
    {
        if(isCredits && (Input.GetKey(KeyCode.Escape)))
        {
            if (SpeedrunStats.notFullStats == true)
            {
                SceneManager.LoadScene("Main Menu");
            }
            else
            {
                SceneManager.LoadScene("New Stats");
            }
        }
    }

    public void ChangeScene()
    {
        if(!isCredits) AM.Play("uiClick");
        anim.SetBool("FadeOut?", true);
        anim.SetBool("FadeIn?", false);
        Debug.Log("Loading" + NextScene);
        StartCoroutine(LoadLevelAfterDelay());
    }

    IEnumerator LoadLevelAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(NextScene);
    }

    IEnumerator LoadLevelAfterDelayCredits()
    {
        yield return new WaitForSeconds(8.0f);
        ChangeScene();
    }

    public void ResetStats()
    {
        SpeedrunStats.resetAll();
    }
    }
