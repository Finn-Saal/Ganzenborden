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
    public bool roundStarted = false;

    private Vector3 endPosition;
    private Vector3 startPosition;
    private float desiredDuration = 5f;
    public static float elapsedTime;

    public float percentageComplete;
    public float blend;
    
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
            //defines the starting position of player
            FindPosition();
            startPosition = transform.GetChild(playNum).position; 
            roundStarted = true;
            curLoc[playNum] += DiceNumberTextScript.diceNumber;
            PlayerMovement.elapsedTime = 0;
            DiceCheckZoneScript.diceStat = false;
            
        }
        
        //makes sure tile number cant go over 63 and moves correct player to new position
        if(curLoc[playNum] <= 63){
            
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
        else
        {
            curLoc[playNum] = 63;
        }

        //adds 1 to player number after round is finished
        if(blend == 1 && DiceScript.throwReady == false && roundStarted == true)
        {
            NextPlayer();
            roundStarted = false;
            DiceScript.throwReady = true;
        }
        SubLocation();
        Debug.Log(DiceScript.throwReady);
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
