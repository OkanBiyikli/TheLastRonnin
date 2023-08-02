using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        DontDestroyOnLoad(gameObject);    
    }

            //PLAYERPREFSLERİ ARTIŞ AZALIŞ DURUMLARI NERELERDEYSE ORALARDA ÇAĞIRICAĞIZ
    public void SetMusicData(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void FXData(float value)
    {
        PlayerPrefs.SetFloat("FXVolume", value);
    }

    public void ExperienceData(float value)
    {
        PlayerPrefs.SetFloat("Experience", value);
    }

    public void LevelData(int value)
    {
        PlayerPrefs.SetInt("CurrentLevel", value);
    }

    public void ExperienceToNextLevel(float value)
    {
        PlayerPrefs.SetFloat("ExperienceTNL", value);
    }

    public void CurrentStars(int value)
    {
        PlayerPrefs.SetInt("StarAmount", value);
    }

    public void CurrentCoin(int value)//BURAYI PARA HARCAMA KISMINDA DA YAZMAYI UNUTMA 
    {
        PlayerPrefs.SetInt("CoinAmount", value);
    }

    public void MaxHealth(float value)
    {
        PlayerPrefs.SetFloat("MaxHealth", value);
    }

    public void CurrentHealth(float value)
    {
        PlayerPrefs.SetFloat("CurrentHealth", value);
    }
}
