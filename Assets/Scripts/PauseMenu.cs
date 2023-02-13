using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        overlay.SetActive(true);
        Time.timeScale = 1f;
        gamePause = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        overlay.SetActive(false);
        Time.timeScale = 0f;
        gamePause = true;
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    public void LoadMenu()
    {
        Resume();
        PlayerMovement.roundStarted = false;
        DiceScript.throwReady = true;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

