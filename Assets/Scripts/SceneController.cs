using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   public void Credits()
   {
       SceneManager.LoadScene("Credits");
   }
   public void Intro()
   {
        SceneManager.LoadScene("Intro");
   }
   public void Sandbox()
   {
        SceneManager.LoadScene("Sandbox");
   }
}
