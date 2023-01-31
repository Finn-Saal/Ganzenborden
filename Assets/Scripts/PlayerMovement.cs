using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int curLoc; 

    public GameObject board;
    public Vector3 positionToGo;
    
    // Start is called before the first frame update
    void Start()
    {
        curLoc = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(board.transform.Find("Vakje " + curLoc).position);

        if(DiceCheckZoneScript.diceStat == true)
        {
            curLoc += DiceNumberTextScript.diceNumber;
            DiceCheckZoneScript.diceStat = false;
        }
        if(curLoc <=63){
        FindPosition();
        transform.GetChild(1).position = positionToGo;
        }
        else{
            curLoc = 63;
        }
    }

    //Defines position of player
    void FindPosition()
    {
        
        positionToGo = board.transform.Find("Vakje " + curLoc).position; 
        
    }

     
}
