using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAllChild : MonoBehaviour
{
    public AudioSource audioSource;
    public float maxVolume = 5f;
    public float range = 10f;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<GearSound>() == null)
            {
                if (child.name == "gear_sound")
                {

                }
                else
                {
                    child.gameObject.AddComponent<GearSound>();
                    GearSound audioPlayer = child.GetComponent<GearSound>();
                    audioPlayer.audioSource = audioSource;
                    audioPlayer.maxVolume = maxVolume;
                    audioPlayer.range = range;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
