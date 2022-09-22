using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
     public float delay = 0;
     public string NextScene= "Main Menu";

     public void Start()
     {
          if(delay > 0) 
          {
          StartCoroutine(LoadSceneWithDelay(delay));
          }
     }
     
     public void LoadScene()
     {
          SceneManager.LoadScene(NextScene);
     }

     IEnumerator LoadSceneWithDelay(float delay)
     {
          yield return new WaitForSeconds(delay);
          SceneManager.LoadScene(NextScene);
     }

     public void Quit()
     {
          Application.Quit();
     }
}
