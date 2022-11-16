using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAllChild : MonoBehaviour
{
    public AudioSource audioSource;
    public float maxVolume = 5f;
    public float range = 10f;
    float distance;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player");
        /*foreach (Transform child in transform)
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
        }*/
    }

    void playAudio(GameObject gear)
    {
        distance = Vector3.Distance(target.transform.position, gear.transform.position);
        audioSource.Play();
        audioSource.loop = true;
    }

    void stopAudio()
    {
        audioSource.Stop();
    }

    void updateVolume(GameObject gear)
    {
        distance = Vector3.Distance(target.transform.position, gear.transform.position);
        audioSource.volume = maxVolume * (1 / distance);
    }

    // Update is called once per frame
    void Update()
    {
        float minDist = 9999f;
        GameObject nearest = null;
        foreach (Transform child in transform)
        {
            if (child.name == "gear_sound")
            {

            }
            else
            {
                distance = Vector3.Distance(target.transform.position, child.position);
                if(distance <= minDist)
                {
                    minDist = distance;
                    nearest = child.gameObject;
                }
            }
        }
        if(minDist > range)
        {
            if (audioSource.isPlaying)
            {
                stopAudio();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                updateVolume(nearest);
            }
            else
            {
                playAudio(nearest);
            }
        }
    }
}
