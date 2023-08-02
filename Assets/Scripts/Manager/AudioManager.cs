using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer, effectMixer;

    public AudioSource backgroundMusicAS;

    public Slider masterSldr, effectSldr;

    public Image musicHandle, effectHandle;

    public Sprite soundOff, soundOn;

    [Range(-80, 20)]
    public float effectVol, masterVol;

    
    public static AudioManager instance;
    
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        PlayAudio(backgroundMusicAS);
        //masterSldr.value = masterVol;
       // effectSldr.value = effectVol;//-80 ve +20 arasındaki değeri alması iiçin bunu vgirdik

        masterSldr.minValue = -80;
        masterSldr.maxValue = 20;

        effectSldr.minValue = -80;
        effectSldr.maxValue = 20;

        masterSldr.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        effectSldr.value = PlayerPrefs.GetFloat("FXVolume", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //MasterVolume();
        //EffectVolume();
        Slider();
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void MasterVolume()
    {
        DataManager.instance.SetMusicData(masterSldr.value);
        musicMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void EffectVolume()
    {
        DataManager.instance.FXData(effectSldr.value);
        effectMixer.SetFloat("effectVolume", PlayerPrefs.GetFloat("FXVolume"));
    }

    public void Slider()
    {
        if(masterSldr.value <= -65)
        {
            musicHandle.sprite = soundOff;
        }else
        {
            musicHandle.sprite = soundOn;
        }

        if(effectSldr.value <= -65)
        {
            effectHandle.sprite = soundOff;
        }
        else
        {
            effectHandle.sprite = soundOn;
        }
    }
}
