using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : Singleton<AudioManager>
{
    public Transform wayPointLeft;
    public Transform wayPointRight;
    public GameObject Player;
    public bool mute;
    public GameObject mutebut;


    public Sound[] sounds;
    public Sprite notMuted;
    public Sprite muted;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        Player = GameObject.FindWithTag("Player");


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.Name == sound);
        s.source.Stop();
    }


    private void Update()
    {



        if (mute)
        {
            foreach (AudioSource audioBron in GetComponents<AudioSource>())
            {
                audioBron.mute = mute;
            }
            mutebut.GetComponent<Image>().sprite = muted;
        }
        else if (!mute)
        {
            foreach (AudioSource audioBron in GetComponents<AudioSource>())
            {
                audioBron.mute = mute;
            }
            mutebut.GetComponent<Image>().sprite = notMuted;
        }
    }


    public void mutee()
    {
        mute = !mute;

    }
}
