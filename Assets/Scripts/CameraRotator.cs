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
    {
        transform.RotateAround(GameObject.Find(PlayerMovement.playNum.ToString()).transform.position, Vector3.up, -(1.9 * DiceNumberTextScript.diceNumber * Time.deltaTime));
    }
}
