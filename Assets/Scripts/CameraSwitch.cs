using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    //define all variables 
    public GameObject cameraMain;
    public GameObject cameraA;
    public GameObject cameraB;
    public GameObject cameraC;
    public GameObject cameraD;

    AudioListener cameraMainAudioLis;
    AudioListener cameraMainALis;
    AudioListener cameraMainBLis;
    AudioListener cameraMainCLis;
    AudioListener cameraMainDLis;

    public int cameraPositionCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get Camera Listeners
        cameraMainAudioLis = cameraMain.GetComponent<AudioListener>();

        //sets start camera
        cameraMain.SetActive(true);
        cameraMainAudioLis.enabled = true;
        cameraA.SetActive(false);
        cameraB.SetActive(false);
        cameraC.SetActive(false);
        cameraD.SetActive(false);
    }

    // Update is called once per frame 
    void Update()
    {
        //zegt wanneer de camera verandert moet worden 
        if(PlayerMovement.roundStarted)
        {
            cameraPositionChange(PlayerMovement.playNum + 1);
        }
        else if(!PlayerMovement.roundStarted)
        {
            cameraPositionChange(0);
        }
    }

    void cameraPositionChange(int camPosition)
    {
    //sets camera and activates audio listener 
        if(camPosition == 0)
        {
            cameraMain.SetActive(true);
            cameraMainAudioLis.enabled = true;
            cameraA.SetActive(false);
            cameraB.SetActive(false);
            cameraC.SetActive(false);
            cameraD.SetActive(false);
        }

        if(camPosition == 1)
        {
            cameraMain.SetActive(false);
            cameraMainAudioLis.enabled = false;
            cameraA.SetActive(true);
            cameraB.SetActive(false);
            cameraC.SetActive(false);
            cameraD.SetActive(false);
        }

        if(camPosition == 2)
        {
            cameraMain.SetActive(false);
            cameraMainAudioLis.enabled = false;
            cameraA.SetActive(false);
            cameraB.SetActive(true);
            cameraC.SetActive(false);
            cameraD.SetActive(false);
        }

        if(camPosition == 3)
        {
            cameraMain.SetActive(false);
            cameraMainAudioLis.enabled = false;
            cameraA.SetActive(false);
            cameraB.SetActive(false);
            cameraC.SetActive(true);
            cameraD.SetActive(false);

        }

        if(camPosition == 4)
        {
            cameraMain.SetActive(false);
            cameraMainAudioLis.enabled = false;
            cameraA.SetActive(false);
            cameraB.SetActive(false);
            cameraC.SetActive(false);
            cameraD.SetActive(true);
        }

    }
}
