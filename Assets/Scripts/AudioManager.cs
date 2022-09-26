using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioSource EffectsSource;
	public AudioSource MusicSource;
	public static AudioManager Instance = null;
	
	private void Awake()
	{
       if(Instance == null){
        Instance = this;
       }
       else{
        Destroy(gameObject);
       }
		DontDestroyOnLoad (gameObject);
	}

	public void playClip(AudioClip clip, String type)
	{
        if(type == "FX"){
            EffectsSource.clip = clip;
		    EffectsSource.Play();
        }
        else{
            MusicSource.clip = clip;
            MusicSource.Play();
        }
		
	}
	
}