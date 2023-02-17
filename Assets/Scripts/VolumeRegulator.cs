using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeRegulator : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeVolume(float newVolume)
     {
        //applies volume to game 
         PlayerPrefs.SetFloat("volume", newVolume);
         AudioListener.volume = PlayerPrefs.GetFloat("volume");
     }
}
