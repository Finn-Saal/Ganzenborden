using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{ 
    public static int playNum = 0; //defines the player that rolls dice starting with player 0
    public GameObject board;
    public Vector3 positionToGo;
    public Vector3 positionCheck;
    public static int playerMax = 3;
    public static int[] curLoc = {1,1,1,1};
    public string tileLoc = null;
    public static bool roundStarted = false;
    public bool[] finishReached = {false,false,false,false};
    public bool[] finishCheck = {true, true, true, true};
    public bool isEqual;
    public bool firstFinish = false;


    private Vector3 endPosition;
    private Vector3 startPosition;
    private float desiredDuration = 5f;
    public static float elapsedTime;

    public float percentageComplete;
    public float blend;
    
    // Start is called before the first frame update
    void Start()
    {
        //alle actieve spelers naar vakje verplaatsten
        for(int i = 0; i<=playerMax; i++)
        {
            SubLocation(i);
            transform.GetChild(i).position = board.transform.Find("Vakje " + 1).Find(tileLoc).GetChild(0).position;
            curLoc[i] = 59;
            finishReached[i] = false;
            //Debug.Log(curLoc[i]);
        }
       //alle nonactieve spelers uitzetten
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
            //defines the starting position of player
            FindPosition();
            startPosition = transform.GetChild(playNum).position; 
            roundStarted = true;
            curLoc[playNum] += DiceNumberTextScript.diceNumber;
            PlayerMovement.elapsedTime = 0;
            if(finishReached[playNum] == true)
            {
                EndRound();
            }
            DiceCheckZoneScript.diceStat = false;
            
        }
        
        //makes sure tile number cant go over 63 and moves correct player to new position
        if(curLoc[playNum] < 63){
            MovePlayer();
        }
        //regulate if player has reached or exceeded the finish
        else if(curLoc[playNum] >= 63 && roundStarted == true)
        {
            curLoc[playNum] = 63;
            MovePlayer();
            finishReached[playNum] = true;
            if(firstFinish == false)
            {
                Debug.Log("you won!");
                firstFinish = true;
            }
        }



        //adds 1 to player number after round is finished
        if(blend == 1 && DiceScript.throwReady == false && roundStarted == true)
        {
            EndRound();
        }
        SubLocation(playNum);
    }

    //assigns sublocation to player to avoid player collision
    void SubLocation(int i)
    {
        if(i==0){
            tileLoc="A";
        }
        else if(i==1){
            tileLoc="B";
        }
        else if(i==2){
            tileLoc="C";
        }
        else if(i==3){
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

    void EndRound()
    {
        NextPlayer();
        roundStarted = false;
        DiceScript.throwReady = true;
        //see if all players have finished
        if(isEqual = finishCheck.SequenceEqual(finishReached))
        {
            Debug.Log("finished");
        }
        //skip player
        else if(finishReached[playNum] == true)
            {
                NextPlayer();
            }
    }

    void MovePlayer(){
            //defines time for the lerp function and smoothens the animation
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / desiredDuration;
            blend = Mathf.SmoothStep(0, 1, percentageComplete);

            //stops percentageComplete from going above 1
            if(percentageComplete >= 1)
            {
                percentageComplete = 1;
            }

            FindPosition();
            endPosition = positionToGo;

            //lerps player to new loacation
            transform.GetChild(playNum).position = Vector3.Lerp(startPosition, endPosition, blend);
    }
}

