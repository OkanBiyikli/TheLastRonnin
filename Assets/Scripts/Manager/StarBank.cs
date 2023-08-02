using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBank : MonoBehaviour
{
    public int bankStar;
    public Text starText;

    public static StarBank instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        bankStar = PlayerPrefs.GetInt("StarAmount", 0);
        starText.text = "x " + bankStar.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        starText.text = "x " + bankStar.ToString();
    }

    public void Collect(int starCollected)
    {
        bankStar += starCollected;
        starText.text = "x " + bankStar.ToString();

        //DataManager.instance.CurrentStars(bankStar);//bankadaki yıldızlarımı set et
        //bankStar = PlayerPrefs.GetInt("StarAmount");//bankadaki yıldızlarımı getir
        //experienceda datasave fonksiyoununa taşıdım
    }
}
