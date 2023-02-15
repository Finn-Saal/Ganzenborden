using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;


public class DiceNumberTextScript : NetworkBehaviour {

	public TextMeshProUGUI dice;
	public TextMeshProUGUI locations;
	public TextMeshProUGUI trapped;
	public TextMeshProUGUI skipping;
	public TextMeshProUGUI turn;
	public TextMeshProUGUI curPlay;
	string color;
	public static int diceNumber;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		dice.text = diceNumber.ToString ();
		locations.text = "";
		turn.text = "Currently in round " + (PlayerMovement.roundNum+1).ToString();
		trapped.text = "Trapped Players:<br>";
		skipping.text = "Players skipping this round:<br>";
		curPlay.text = "Current player is ";
		for(int i = 0; i<=PlayerMovement.playerMax; i++)
        {
			if(i == 0){
				color = "Red";
			}
			else if(i == 1){
				color = "Blue";
			}
			else if(i == 2){
				color = "Green";
			}
			else if(i == 3){
				color = "Yellow";
			}
            locations.text += color + ": " + (PlayerMovement.curLoc[i]-1).ToString() + "<br>";  
			
			if(PlayerMovement.playTrap[i]){
				trapped.text += color + "<br>";
			}       

			if(PlayerMovement.skipRound[i]){
				skipping.text += color + "<br>";
			}
		}
			if(PlayerMovement.playNum == 0){
				curPlay.text += "Red";
			}
			else if(PlayerMovement.playNum == 1){
				curPlay.text += "Blue";
			}
			else if(PlayerMovement.playNum == 2){
				curPlay.text += "Green";
			}
			else if(PlayerMovement.playNum == 3){
				curPlay.text += "Yellow";
			}	
		}
}
