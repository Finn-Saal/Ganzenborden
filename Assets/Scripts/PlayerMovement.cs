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
    public static int playerMax = 2;
    public static int[] curLoc = new int[playerMax+1];
    public string tileLoc = null;

    private Vector3 endPosition;
    private Vector3 startPosition;
    private float desiredDuration = 2f;
    private float elapsedTime;
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(playerMax);
        for(int i = 0; i<=playerMax; i++)
        {
            transform.GetChild(i).position = board.transform.Find("Vakje " + 1).position;
            curLoc[i] = 1;
        }
       // Debug.Log(curLoc[1]);
       for(int i = playerMax+1; i<=3; i++)
        {
            GameObject.Find(i.ToString()).SetActive(false);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //Debug.Log(board.transform.Find("Vakje " + curLoc).position);
        positionCheck = transform.GetChild(playNum).position;
        if(DiceCheckZoneScript.diceStat == true)
        {
            
            FindPosition();
            startPosition = positionToGo; 

            curLoc[playNum] += DiceNumberTextScript.diceNumber;
            DiceCheckZoneScript.diceStat = false;
            
        }
        
        //makes sure tile number cant go over 63 and moves correct player to new position
        if(curLoc[playNum] <= 63){
            
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;

            FindPosition();
            endPosition = positionToGo;

            transform.GetChild(playNum).position = Vector3.Lerp(startPosition, endPosition, percentageComplete);

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

    //assigns sublocation to player to avoid player collision
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
