using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionSelectUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        var options = AudioManager.LoadAudioOptions();
        musicSlider.value = options.musicVolume;
        sfxSlider.value = options.sfxVolume;

        musicSlider.onValueChanged.AddListener((float val) =>
        {
            AudioManager._instance.ChangeMusicVolume(val);
            AudioManager._instance.Save();
            
        });

        sfxSlider.onValueChanged.AddListener((float val) =>
        {
            AudioManager._instance.ChangeSFXVolume(val);
            AudioManager._instance.Save();
            Debug.Log("change sfx slider");
        });

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
