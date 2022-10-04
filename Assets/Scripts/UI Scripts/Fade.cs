using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject FadeScreen;

    void Start()
    {
        FadeScreen.GetComponent<Animation>().Play("Fade");
    }

    public void Fading()
    {

    }
}
