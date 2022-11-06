using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSound : MonoBehaviour
{
    public AudioSource audioSource;
    GameObject target;
    public float maxVolume = 5f;
    float distance;
    public float range = 10f;
    //bool isPlaying = false;
    bool inrange = false;

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
        //isPlaying = true;
        Debug.Log(transform.name + " play gear sound");
    }

    void stopAudio()
    {
        Debug.Log(transform.name + " stop gear sound");
        audioSource.Stop();
        //isPlaying = false;
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
            //if just get into range
            if (inrange == false)
            {
                inrange = true;
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
            //if already in range
            else
            {
                updateVolume();
            }
            if (!audioSource.isPlaying)
            {
                updateVolume();
                playAudio();
            }
        }
        else
        {
            if(inrange == true)
            {
                inrange = false;
                if (audioSource.isPlaying)
                {
                    stopAudio();
                }
                Debug.Log("out of range");
            }

        }
    }
}


