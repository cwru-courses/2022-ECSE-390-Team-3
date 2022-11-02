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
    bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }




    void playAudio()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
        audioSource.Play();
        isPlaying = true;
        Debug.Log("play yellow sound");
    }

    void stopAudio()
    {
        Debug.Log("stop yellow sound");
        audioSource.Stop();
        isPlaying = false;
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
            if (isPlaying)
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
            if (isPlaying)
            {
                stopAudio();
            }
            Debug.Log("out of range");

        }
    }
}
