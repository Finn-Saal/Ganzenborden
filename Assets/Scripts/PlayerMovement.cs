using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;
using System;
using System.Linq;

public class PlayerMovement : MonoBehaviour 
{ 
    //define all variables 
    public const int startPos = 1; 
    public static int playNum = 0; //defines the player that rolls dice starting with player 0
    public GameObject board;
    public GameObject effects;    
    public Vector3 positionToGo;
    public Vector3 positionCheck;
    public static int playerMax = 2; //defines the amount of players
    public static int[] curLoc = {1,1,1,1};
    public static int[] prevLoc = {1,1,1,1};
    public string tileLoc = null;
    public static bool roundStarted = false;
    public static bool someTrapped = false;
    public static bool someSkip = false;
    public bool bridgeUsed = false;
    public bool[] finishReached = {false,false,false,false};
    public bool[] finishCheck = {true, true, true, true};
    public static bool[] skipRound = {false, false, false, false};
    public static int[] skipRoundTurn = {0, 0, 0, 0};
    public static bool[] playTrap = {false, false, false, false};
    public int[] playTrapTurn = {0, 0, 0, 0};
    public int[] playTrapLoc = {0, 0, 0, 0};
    public int multiplier;
    public int stepsBack;
    public bool movedBack = true;



    public bool isEqual;
    public bool firstFinish = false;
    public bool doneEvent = false;
    public bool startRound = false;
    public Quaternion rotationToGo;
    public Quaternion startRotation;
    public static int roundNum = 0;

    


    private Vector3 endPosition;
    private Vector3 startPosition;
    private float desiredDuration = 5f;
    public static float elapsedTime;

    public float percentageComplete;
    public float blend;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        //alle actieve spelers naar vakje verplaatsten
        for(int i = 0; i<=playerMax; i++)
        {
            //moves player to first position 
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
       movedBack = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //Debug.Log(board.transform.Find("Vakje " + curLoc).position);
        positionCheck = transform.GetChild(playNum).position; //kijkt naar de positie van de speler 
       //defines the rounds played and adds the next round to the total number  
        if(playNum == 0 && startRound == true)
            {
                roundNum++;
                startRound = false;
            }
        else{
            startRound = false;
        }
//kijkt of speler op een speciaal vakje staat
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
            else if(curLoc[playNum] == 64)
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
            //regulate if player has reached the finish
            else if(curLoc[playNum] > 64 && roundStarted == true && finishReached[playNum])
            {
                curLoc[playNum] = 64;
                MovePlayer();
            }
            //regulates if player exceeds finish and moves back player
            else if(curLoc[playNum] > 64 && roundStarted == true && !finishReached[playNum])
            {
                if(movedBack){
                    stepsBack = curLoc[playNum] - 64;
                    movedBack = false;
                }
                curLoc[playNum] = 65;
                MovePlayer();
                if(blend >= 0.9 && DiceScript.throwReady == false && roundStarted)
                {
                    FindPosition();
                    startPosition = transform.GetChild(playNum).position; 
                    startRotation = transform.GetChild(playNum).rotation; 
                    curLoc[playNum] = 64-stepsBack;
                    PlayerMovement.elapsedTime = 0;
                    MovePlayer();
                    movedBack = true;
                }

            }
        }
        //special events, https://123bordspellen.com/hoe-speel-je-ganzenbord/
        //repeat previously thrown number als speler op rood gans vakje komt 
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
        //als speler op de eerste brug komt dan gaat hij naar de volgende brug maar komt de speler op de tweede brug dan gaat hij terug naar de eerste brug 
        else if(curLoc[playNum] == 6+1 && !bridgeUsed) 
        {
            if(blend >= 0.9 && DiceScript.throwReady == false && roundStarted)
            {
                FindPosition();
                startPosition = transform.GetChild(playNum).position; 
                startRotation = transform.GetChild(playNum).rotation; 
                curLoc[playNum] = 12+1;
                PlayerMovement.elapsedTime = 0;
                bridgeUsed = true;
            }
        }
        else if(curLoc[playNum] == 12+1 && !bridgeUsed)
        {
            if(blend >= 0.9 && DiceScript.throwReady == false && roundStarted)
            {
                FindPosition();
                startPosition = transform.GetChild(playNum).position; 
                startRotation = transform.GetChild(playNum).rotation; 
                curLoc[playNum] = 6+1;
                PlayerMovement.elapsedTime = 0;
                bridgeUsed = true;
            }
        }
        //skip 1 turn if player ends on number 20 
        else if(curLoc[playNum] == 19+1)
        {
            if(skipRound[playNum] == false && roundStarted)
            {
                
                skipRoundTurn[playNum] = roundNum;
                skipRound[playNum] = true;
                doneEvent = true;
            }
        }
        //labyrinth to 37 beweegt speler terug naar nummer 37 
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
        //speler gevangen toggle 
        else if( curLoc[playNum] == 31+1 || curLoc[playNum] == 52+1)
        {
            if(playTrap[playNum] == false && roundStarted)
            {
                playTrapTurn[playNum] = roundNum;
                playTrap[playNum] = true;
                doneEvent = true;
                playTrapLoc[playNum] = curLoc[playNum];
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
                curLoc[playNum] = 2;
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
    // gaat naar de volgende speler 
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
    // eindigt ronde 
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
            if(DiceNumberTextScript.diceNumber == 1){
				multiplier = 3;
			}
			else if(DiceNumberTextScript.diceNumber == 2){
				multiplier = 2;
			}
			else if(DiceNumberTextScript.diceNumber == 3){
				multiplier = 2;
			}
			else if(DiceNumberTextScript.diceNumber == 4){
				multiplier = 2;
			}
            else if(DiceNumberTextScript.diceNumber == 5){
				multiplier = 1;
			}
            else if(DiceNumberTextScript.diceNumber == 6){
				multiplier = 1;
			}
            elapsedTime += Time.deltaTime*multiplier;
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
                    prevLoc[playNum] = curLoc[playNum];
                    curLoc[playNum] += DiceNumberTextScript.diceNumber;
                    PlayerMovement.elapsedTime = 0;
                    doneEvent = false;
                    someSkip = false;
                    //kijkt of de speler getrapped is 
                    foreach(bool i in playTrap)
                    {
                        if(i){
                            someTrapped = true;
                        }
                    }
                    foreach(bool i in skipRound)
                    {
                        if(i){
                            someSkip = true;
                        }
                    }
                   //als je over een gevangen speler springt dan komt de gavangen speler weer los  
                    for(int i = 0; i<=playerMax; i++)
                    {
                        if(someTrapped && prevLoc[playNum]<playTrapLoc[i] && playTrapLoc[i]<curLoc[playNum])
                        {
                            playTrap[i] = false;
                        }
                    }
                    someTrapped = false;
                    

                    bridgeUsed = false;
                    DiceCheckZoneScript.diceStat = false;
                    Debug.Log(curLoc[playNum] +" "+ playNum);
                }
    }
}

