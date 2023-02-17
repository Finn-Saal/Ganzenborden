using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    //public GameObject jukebox;
    public Slider volumeSlider;

    public void SetPlayers (float players)
    {
        int playerInt = (int)players;
        PlayerMovement.playerMax = playerInt-1;
        //Debug.Log(players + " SM");

    }
//pakt volume van slider 
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
//past het volume aan 
    public void SetValue()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

}
