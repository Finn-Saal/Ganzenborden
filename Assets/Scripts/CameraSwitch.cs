using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
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
        //cameraAAudioLis = cameraA.GetComponent<AudioListener>();
        //cameraBAudioLis = cameraB.GetComponent<AudioListener>();
        //cameraCAudioLis = cameraC.GetComponent<AudioListener>();
        //cameraDAudioLis = cameraD.GetComponent<AudioListener>();

        //sets start camera
        cameraMain.SetActive(true);
        cameraMainAudioLis.enabled = true;
        cameraA.SetActive(false);
        //cameraAAudioLis.enabled = false;
        cameraB.SetActive(false);
        //cameraBAudioLis.enabled = false;
        cameraC.SetActive(false);
        //cameraCAudioLis.enabled = false;
        cameraD.SetActive(false);
        //cameraDAudioLis.enabled = false;
    }

    // Update is called once per frame 
    void Update()
    {
        //SwitchCamera();
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
            //cameraAAudioLis.enabled = false;
            cameraB.SetActive(false);
            //cameraBAudioLis.enabled = false;
            cameraC.SetActive(false);
            //cameraCAudioLis.enabled = false;
            cameraD.SetActive(false);
            //cameraDAudioLis.enabled = false;
        }

        if(camPosition == 1)
        {
            cameraMain.SetActive(false);
            cameraMainAudioLis.enabled = false;
            cameraA.SetActive(true);
            //cameraAAudioLis.enabled = true;
            cameraB.SetActive(false);
            //cameraBAudioLis.enabled = false;
            cameraC.SetActive(false);
            //cameraCAudioLis.enabled = false;
            cameraD.SetActive(false);
            //cameraDAudioLis.enabled = false;
        }

        if(camPosition == 2)
        {
            cameraMain.SetActive(false);
            cameraMainAudioLis.enabled = false;
            cameraA.SetActive(false);
            //cameraAAudioLis.enabled = false;
            cameraB.SetActive(true);
            //cameraBAudioLis.enabled = true;
            cameraC.SetActive(false);
            //cameraCAudioLis.enabled = false;
            cameraD.SetActive(false);
            //cameraDAudioLis.enabled = false;
        }

        if(camPosition == 3)
        {
            cameraMain.SetActive(false);
            cameraMainAudioLis.enabled = false;
            cameraA.SetActive(false);
            //cameraAAudioLis.enabled = false;
            cameraB.SetActive(false);
            //cameraBAudioLis.enabled = false;
            cameraC.SetActive(true);
            //cameraCAudioLis.enabled = true;
            cameraD.SetActive(false);
            //cameraDAudioLis.enabled = false;
        }

        if(camPosition == 4)
        {
            cameraMain.SetActive(false);
            cameraMainAudioLis.enabled = false;
            cameraA.SetActive(false);
            //cameraAAudioLis.enabled = false;
            cameraB.SetActive(false);
            //cameraBAudioLis.enabled = false;
            cameraC.SetActive(false);
            //cameraCAudioLis.enabled = false;
            cameraD.SetActive(true);
            //cameraDAudioLis.enabled = true;
        }

    }
}
