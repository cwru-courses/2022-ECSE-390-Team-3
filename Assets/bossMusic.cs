using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMusic : MonoBehaviour
{
    private AudioManager AM;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Play Music")]
    public void startBossMusic() {
     AM = FindObjectOfType<AudioManager>();
        if(AM != null) {
            AM.Stop("all");
            AM.Play("boss");
        }   
    }
}
