using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{ 
    public const int startPos = 30;
    public static int playNum = 0; //defines the player that rolls dice starting with player 0
    public GameObject board;
    public GameObject effects;    
    public Vector3 positionToGo;
    public Vector3 positionCheck;
    public static int playerMax = 2;
    public static int[] curLoc = {1,1,1,1};
    public string tileLoc = null;
    public static bool roundStarted = false;
    public bool[] finishReached = {false,false,false,false};
    public bool[] finishCheck = {true, true, true, true};
    public bool[] skipRound = {false, false, false, false};
    public int[] skipRoundTurn = {0, 0, 0, 0};
    public bool[] playTrap = {false, false, false, false};
    public int[] playTrapTurn = {0, 0, 0, 0};


    public bool isEqual;
    public bool firstFinish = false;
    public bool doneEvent = false;
    public bool startRound = false;
    public Quaternion rotationToGo;
    public Quaternion startRotation;
    public int roundNum = 0;

    


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
            transform.GetChild(i).position = board.transform.Find("Vakje " + startPos).Find(tileLoc).GetChild(0).position;
            curLoc[i] = startPos;
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
        if(playNum == 0 && startRound == true)
            {
                roundNum++;
                startRound = false;
            }
        else{
            startRound = false;
        }

        if(skipRound[playNum] == true && roundNum > skipRoundTurn[playNum])
        {
            skipRound[playNum] = false;
            Debug.Log("skip round " + playNum);
            EndRound();  
        }
        else if(playTrap[playNum] && DiceNumberTextScript.diceNumber != 6 && roundNum > playTrapTurn[playNum] && DiceCheckZoneScript.diceStat == true){
            playTrap[playNum] = false;
            Debug.Log("trapped done " + playNum);
            DiceCheckZoneScript.diceStat = false;
            EndRound();  
        }
        else if(playTrap[playNum] == false || DiceNumberTextScript.diceNumber == 6)
        {
            playTrap[playNum] = false;
            CheckDice();
        }
        
        
        
        //makes sure tile number cant go over 64 and moves correct player to new position
        if(skipRound[playNum] == false || roundNum == skipRoundTurn[playNum])
        {
            if(curLoc[playNum] == startPos)
            {
                SubLocation(playNum);
                transform.GetChild(playNum).position = board.transform.Find("Vakje " + startPos).Find(tileLoc).GetChild(0).position;
            }
            
            else if(curLoc[playNum] < 64){
                MovePlayer();
            }
            //regulate if player has reached or exceeded the finish
            else if(curLoc[playNum] >= 64 && roundStarted == true)
            {
                curLoc[playNum] = 64;
                MovePlayer();
                finishReached[playNum] = true;
                if(firstFinish == false)
                {
                    //Debug.Log("you won!");
                    if(blend >= 0.8)
                    {
                    GameObject.Find("ConfettiBlastBlue").GetComponent<ParticleSystem>().Play();
                    }
                    if(blend >= 0.99)
                    {
                    firstFinish = true;

                    }
                }
            }
        }
        //special events, https://123bordspellen.com/hoe-speel-je-ganzenbord/
        //repeat previously thrown number
        if( curLoc[playNum] == 5+1 || curLoc[playNum] == 9+1 ||curLoc[playNum] == 14+1 ||curLoc[playNum] == 18+1 ||curLoc[playNum] == 23+1 ||
            curLoc[playNum] == 27+1||curLoc[playNum] == 32+1 ||curLoc[playNum] == 36+1 ||curLoc[playNum] == 41+1 ||curLoc[playNum] == 45+1 ||
            curLoc[playNum] == 50+1||curLoc[playNum] == 54+1 ||curLoc[playNum] == 59+1)
        {
            if(blend >= 0.9 && DiceScript.throwReady == false && roundStarted)
            {
                FindPosition();
                startPosition = transform.GetChild(playNum).position; 
                startRotation = transform.GetChild(playNum).rotation; 
                curLoc[playNum] += DiceNumberTextScript.diceNumber;
                PlayerMovement.elapsedTime = 0;
            }
        }
        //skip 1 turn
        else if(curLoc[playNum] == 19+1)
        {
            if(skipRound[playNum] == false && roundStarted)
            {
                
                skipRoundTurn[playNum] = roundNum;
                skipRound[playNum] = true;
                doneEvent = true;
            }
        }
        //labyrinth to 37
        else if( curLoc[playNum] == 42+1)
        {
            if(blend >= 0.9 && DiceScript.throwReady == false && roundStarted)
            {
                FindPosition();
                startPosition = transform.GetChild(playNum).position; 
                startRotation = transform.GetChild(playNum).rotation; 
                curLoc[playNum] = 38;
                PlayerMovement.elapsedTime = 0;
            }
        }
        else if( curLoc[playNum] == 31+1 || curLoc[playNum] == 52+1)
        {
            if(playTrap[playNum] == false && roundStarted)
            {
                playTrapTurn[playNum] = roundNum;
                playTrap[playNum] = true;
                doneEvent = true;
            }
        }
        //death to start
        else if( curLoc[playNum] == 58+1)
        {
            if(blend >= 0.9 && DiceScript.throwReady == false && roundStarted)
            {
                FindPosition();
                startPosition = transform.GetChild(playNum).position; 
                startRotation = transform.GetChild(playNum).rotation; 
                curLoc[playNum] = 1;
                PlayerMovement.elapsedTime = 0;
            }
        }
        else
        {
            doneEvent = true;
        }


        //adds 1 to player number after round is finished
        if(blend == 1 && DiceScript.throwReady == false && roundStarted && doneEvent)
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
        rotationToGo = board.transform.GetChild(curLoc[playNum]-1).rotation;
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
        startRound = true;
        NextPlayer();
        roundStarted = false;
        DiceScript.throwReady = true;
        //see if all players have finished
        if(isEqual = finishCheck.SequenceEqual(finishReached))
        {
            Debug.Log("finished");
        }
        //skip player
        else if(finishReached[playNum] == true && isEqual == false)
            {
                EndRound();
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
            transform.GetChild(playNum).rotation = Quaternion.Lerp(startRotation, rotationToGo, percentageComplete);
            transform.GetChild(playNum).position = Vector3.Lerp(startPosition, endPosition, blend);
    }

    void CheckDice(){
        if(DiceCheckZoneScript.diceStat == true)
                {
                    //defines the starting position of player
                    FindPosition();
                    startPosition = transform.GetChild(playNum).position; 
                    startRotation = transform.GetChild(playNum).rotation; 
                    roundStarted = true;
                    
                    curLoc[playNum] += DiceNumberTextScript.diceNumber;
                    PlayerMovement.elapsedTime = 0;
                    doneEvent = false;
                    DiceCheckZoneScript.diceStat = false;
                    Debug.Log(curLoc[playNum] +" "+ playNum);
                }
    }
}

