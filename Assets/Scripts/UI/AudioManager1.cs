using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager1 : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager1 instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
           // s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mix;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

      //  s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 0.3f, s.volume / 0.3f));
       // s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitch / 1f, s.pitch / 1f));

        s.source.Stop();
    }

    void Start()
    {
       
       /* if(SceneManager.sceneCount==0)
        {
            Play("MenuMusic");
        }
        if(SceneManager.sceneCount==1)
        {

            Play("BackgroundMusic");
            
        
        }*/
        
    }
}
