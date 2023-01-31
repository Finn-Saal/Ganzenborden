using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;

public class PlayerMovement : MonoBehaviour
{ 
    public int playNum = 0; //defines the player that rolls dice starting with player 0
    public GameObject board;
    public Vector3 positionToGo;
    public Vector3 positionCheck;
    public int playerMax = 3;
    public int[] curLoc = {1,1,1,1};
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<curLoc.Length; i++)
        {
            transform.GetChild(i).position = board.transform.Find("Vakje " + 1).position;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(board.transform.Find("Vakje " + curLoc).position);
        positionCheck = transform.GetChild(playNum).position;
        if(DiceCheckZoneScript.diceStat == true)
        {
            curLoc[playNum] += DiceNumberTextScript.diceNumber;
            DiceCheckZoneScript.diceStat = false;
            
        }
        
        //makes sure tile number cant go over 63 and moves correct player to new position
        if(curLoc[playNum] <= 63){
        FindPosition();
        transform.GetChild(playNum).position = positionToGo;
        }
        else
        {
            curLoc[playNum] = 63;
        }

        if(positionCheck != transform.GetChild(playNum).position)
        {
            NextPlayer();
        }
    }

    void PlayerTurn()
    {
        
    }

    //Defines position of player
    void FindPosition()
    {
        positionToGo = board.transform.Find("Vakje " + curLoc[playNum]).position; 
    }   

    void NextPlayer()
    {
        if(playNum < playerMax)
            {
                playNum++;
            }
            else
            {
                playNum = 0;
            }
    }
}
