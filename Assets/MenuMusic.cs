using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    AudioManager AM;
    // Start is called before the first frame update
    void Start()
    {
        AM = FindObjectOfType<AudioManager>();
        if(AM != null) AM.Play("briansTheme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
