using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    bool isPaused;
    // Start is called before the first frame update

    private void Awake() 
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    public void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Time.timeScale = 0;//oyunu durdurma (eğer buraya 2 yazsaydım oyun 2 katı hızlanırdı)
            pauseMenu.SetActive(true);
            isPaused = true;
        }else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;//oyunu devam ettirme
            pauseMenu.SetActive(false);
            isPaused = false;
        }
    }
}
