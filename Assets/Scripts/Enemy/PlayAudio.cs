using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;
    GameObject target;
    public float maxVolume = 5f;
    float distance;
    public float range = 10f;
    //bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }




    void playAudio()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
        audioSource.Play();
        audioSource.loop = true;
        
    }

    void stopAudio()
    {
        audioSource.Stop();
    }

    void updateVolume()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
        audioSource.volume = maxVolume * (1 / distance);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= range)
        {
            if (audioSource.isPlaying)
            {
                updateVolume();
            }
            else
            {
                updateVolume();
                playAudio();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                stopAudio();
            }
        }
    }
}
