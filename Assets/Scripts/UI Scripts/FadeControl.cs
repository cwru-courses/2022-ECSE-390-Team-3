using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeControl : MonoBehaviour
{
    public GameObject fadeScreen;
    public Animator anim;
    public string NextScene = "";

    public void Start()
    {
        fadeScreen.SetActive(true);
        anim.SetBool("FadeIn?", true);
        anim.SetBool("FadeOut?", false);
    }

    public void ChangeScene()
    {
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
}
