using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AudioOptions
{
    public float sfxVolume = 0;
    public float musicVolume = 0;
    public float muteSFX = 0;
    public float muteMusic = 0;
}

public class AudioManager : MonoBehaviour
{
    public static AudioOptions LoadAudioOptions()
    {
        AudioOptions savedData = new AudioOptions();
        savedData.sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        savedData.muteSFX = PlayerPrefs.GetFloat("MuteSFX");
        if (savedData.sfxVolume == 0)
        {
            if (savedData.muteSFX == 0)
            {
                savedData.sfxVolume = 0.25f;
            }
        }
        savedData.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        savedData.muteMusic = PlayerPrefs.GetFloat("MuteMusic");
        if (savedData.musicVolume == 0)
        {
            if (savedData.muteMusic == 0)
            {
                savedData.musicVolume = 0.15f;
            }
        }
        return savedData;
    }

    public static void Save(AudioOptions data)
    {
        PlayerPrefs.SetFloat($"SFXVolume", data.sfxVolume);
        PlayerPrefs.SetFloat($"MusicVolume", data.musicVolume);
        PlayerPrefs.SetFloat($"MuteSFX", data.muteSFX);
        PlayerPrefs.SetFloat($"MuteMusic", data.muteMusic);

        PlayerPrefs.Save();
    }

    public void Save()
    {
        Save(options);
    }

    public static AudioManager _instance;
    public AudioOptions options;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public GameManager manager;

    private static bool spawned = false;
    private void Awake()
    {
        if (!spawned)
        {
            spawned = true;
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
            
            //DontDestroyOnLoad(_instance.transform.GetChild(0));
            //DontDestroyOnLoad(_instance.transform.GetChild(1));
        }
    }

    public void ChangeMusicVolume(float changeAmount)
    {
        musicSource.volume = changeAmount;
        options.musicVolume = changeAmount;

        if (changeAmount == 0)
        {
            options.muteMusic = 1;
        }
        else
        {
            options.muteMusic = 0;
        }
        Debug.Log(changeAmount);
    }

    public void ChangeSFXVolume(float changeAmount)
    {
        sfxSource.volume = changeAmount;
        options.sfxVolume = changeAmount;
        
        if (changeAmount == 0)
        {
            options.muteSFX = 1;
        }
        else
        {
            options.muteSFX = 0;
        }
        sfxSource.Play();
    }

    public void LoadVolumeSettings()
    {
        options = LoadAudioOptions();
        musicSource.volume = options.musicVolume;
        sfxSource.volume = options.sfxVolume;
    }

    public void OnTimeChange()
    {
     
    }
    public void Attach()
    {
        AudioManager._instance.musicSource.pitch = 1.0f;
        manager = FindObjectOfType<GameManager>();
        manager.OnTimeChange += OnTimeChange;
    }
    public void Start()
    {
        LoadVolumeSettings();

        manager = FindObjectOfType<GameManager>();
        if (manager)
        {
            if (manager.level)
            {
                
                if (manager.level.music != null)
                {
                    musicSource.clip = manager.level.music.clip;
                    //musicSource.Stop();                    
                }
            }
            if (musicSource != null)
            {
                if (!musicSource.isPlaying)
                {
                    musicSource.Play();
                    musicSource.loop = true;
                }
            }
        }
   
    }

    public void PlaySong(AudioClip song)
    {
        if (song != null)
        {
            if(musicSource.clip == song)
            {
                return;
            }
            musicSource.clip = song;
            musicSource.Play();
            musicSource.loop = true;
        }
    }

    public void PlaySFX(AudioClip sfx, float modifier = 0.0f)
    {
        sfxSource.clip = sfx;
        sfxSource.Play();
        sfxSource.loop = false;
    }
}
    
