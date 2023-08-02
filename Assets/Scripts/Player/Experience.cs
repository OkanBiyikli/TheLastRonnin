using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Experience : MonoBehaviour
{
    public Image expImg;
    public Text levelText;
    public int currentLevel;

    [HideInInspector]
    public float currentExperience;
    public float expToNextLevel;

    public AudioSource levelUpAS;
    
    public static Experience instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        expImg.fillAmount = currentExperience / expToNextLevel;
        currentLevel = 1;
        levelText.text = currentLevel.ToString();

        currentExperience = PlayerPrefs.GetFloat("Experience", 0);
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);//başlangıçta da bu değerleri alsın diye startta yazıyoruz
    }

    
    void Update()
    {
        expImg.fillAmount = currentExperience / expToNextLevel;
        levelText.text = currentLevel.ToString();
    }

    public void expMod(float experience)
    {
        currentExperience += experience;

        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);

        expImg.fillAmount = currentExperience / expToNextLevel;
        if(currentExperience >= expToNextLevel)
        {
            expToNextLevel *= 2;
            currentExperience = 0;
            currentLevel++;
            levelText.text = currentLevel.ToString();
            PlayerHealth.instance.maxHealth += 5;//lvl aldığımızda canı 5 arttır
            PlayerHealth.instance.currentHealth += 5;
            

            PlayerController.instance.damage += 5;
            AudioManager.instance.PlayAudio(levelUpAS);
        
            //currentLevel = PlayerPrefs.GetInt("CurrentLevel", currentLevel);
        }

        
    }

    public void DataSave()
    {
        DataManager.instance.ExperienceData(currentExperience);
        DataManager.instance.ExperienceToNextLevel(expToNextLevel);
        DataManager.instance.LevelData(currentLevel);//buralar sürekli artabilen değere sahip olduğundan dolayı update etsin diye yazdık
        //bu yaptığımız updateden sonraki değişiklikten sonra aşağıya bir daha playerprefs komuıtunu çağırıyoruz ki kaydetsin

        DataManager.instance.CurrentHealth(PlayerHealth.instance.currentHealth);
        PlayerHealth.instance.currentHealth = PlayerPrefs.GetFloat("CurrentHealth");
            
        DataManager.instance.MaxHealth(PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.maxHealth = PlayerPrefs.GetFloat("MaxHealth");

        currentExperience = PlayerPrefs.GetFloat("Experience");
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL");
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        DataManager.instance.CurrentStars(StarBank.instance.bankStar);
        StarBank.instance.bankStar = PlayerPrefs.GetInt("StarAmount");

        DataManager.instance.CurrentCoin(CoinBank.instance.bank);
        CoinBank.instance.bank = PlayerPrefs.GetInt("CoinAmount");
    }
}
