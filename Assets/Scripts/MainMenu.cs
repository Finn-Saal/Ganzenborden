using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame ()
    //swithces scene to game 
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1);
    }  

     public void QuitGame ()
         //swithces scene to main menu 
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }   
}
