using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMusic : MonoBehaviour
{
    private AudioManager AM;
    // Start is called before the first frame update
    void Start()
    {
     AM = FindObjectOfType<AudioManager>();
     if(AM != null){
        AM.Stop("all");
        AM.Play("vyrusVibing");
     }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
