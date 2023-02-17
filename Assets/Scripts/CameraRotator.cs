using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateHorizontal();
        
    }

    void RotateHorizontal()
    //zorgt dat camera horizontal rond speler draait 
    {
        transform.RotateAround(GameObject.Find(PlayerMovement.playNum.ToString()).transform.position, Vector3.up, -((float)1.9 * (float)DiceNumberTextScript.diceNumber * Time.deltaTime));
    }
}
