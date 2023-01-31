using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;

public class PlayerMovement : MonoBehaviour
{ 
    public int playNum = 0; //defines the player that rolls dice starting with player 0
    public GameObject board;
    public Vector3 positionToGo;
    public int playerMax = 3;
    public int curLoc = 1;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(board.transform.Find("Vakje " + curLoc).position);
        if(DiceCheckZoneScript.diceStat == true)
        {
            curLoc += DiceNumberTextScript.diceNumber;
            DiceCheckZoneScript.diceStat = false;
            if(playNum <= playerMax)
            {
                playNum++;
            }
            else
            {
                playNum = 0;
            }
        }
        
        //makes sure tile number cant go over 63 and moves correct player to new position
        if(curLoc <= 63){
        FindPosition();
        transform.GetChild(playNum).position = positionToGo;
        }
        else{
            curLoc = 63;
        }
    }

    void PlayerTurn()
    {
        
    }

    //Defines position of player
    void FindPosition()
    {
        positionToGo = board.transform.Find("Vakje " + curLoc).position; 
    }   
}
