using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc : MonoBehaviour
{
    AudioManager AM;
    // Start is called before the first frame update
    void Start()
    {
        AM = FindObjectOfType<AudioManager>();
        if(AM != null) AM.Play("vyrusVibing");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
