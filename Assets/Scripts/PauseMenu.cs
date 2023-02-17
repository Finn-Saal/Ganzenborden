using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //define all variables 
    public static bool gamePause = false;
    public GameObject pauseMenuUI;
    public GameObject overlay;
    public Slider volumeSlider;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePause)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }
//zorgt dat je door kan gaan als je op resume klikt 
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        overlay.SetActive(true);
        Time.timeScale = 1f;
        gamePause = false;
    }
//zorgt dat het spel op pauze komt te staan  
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        overlay.SetActive(false);
        Time.timeScale = 0f;
        gamePause = true;
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }
//laad het menu 
    public void LoadMenu()
    {
        Resume();
        PlayerMovement.roundStarted = false;
        DiceScript.throwReady = true;
        SceneManager.LoadScene("Menu");
    }
// zorgt dat je kan stoppen 
    public void QuitGame()
    {
        Application.Quit();
    }
}

