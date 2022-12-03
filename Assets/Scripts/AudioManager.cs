using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
    [SerializeField] private float bossMusicStartTime = 19;


    // Use this for initialization
    void Awake()
    {

        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        if (!s.source.isPlaying) {
            if (name == "boss") {
                s.source.time = bossMusicStartTime;
            }
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        if(name == "all"){
            Debug.Log("stopping all sounds");
            foreach (Sound x in sounds)
            {
                x.source.Stop();
            }
            return;
        }

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        s.source.Stop();
    }
}