using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioInRange : MonoBehaviour
{

    public AudioSource audioSource;
    GameObject target;
    public float maxVolume = 5f;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    

    public void playAudio()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
        audioSource.PlayOneShot(audioSource.clip, maxVolume * (2/distance));
    }
}
