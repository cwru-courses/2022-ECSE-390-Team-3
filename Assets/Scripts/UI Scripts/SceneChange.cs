 using System.Collections;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 
 public class SceneChange : MonoBehaviour
 {
    public float delay = 1200;
    public string NextScene= "Main Menu";
    void Start()
    {
        StartCoroutine(LoadLevelAfterDelay(delay));
    }
 
    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(NextScene);
    }
 }