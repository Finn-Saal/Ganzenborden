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
    public static int playerMax;
    public int[] curLoc = new int[playerMax];
    public string tileLoc = null;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<curLoc.Length; i++)
        {
            transform.GetChild(i).position = board.transform.Find("Vakje " + 1).position;
            curLoc[i]=1;
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
        SubLocation();
    }

    void SubLocation()
    {
        if(playNum==0){
            tileLoc="A";
        }
        else if(playNum==1){
            tileLoc="B";
        }
        else if(playNum==2){
            tileLoc="C";
        }
        else if(playNum==3){
            tileLoc="D";
        }
    }

    //Defines position of player
    void FindPosition()
    {
        positionToGo = board.transform.GetChild(curLoc[playNum]-1).Find(tileLoc).GetChild(0).position; 
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
