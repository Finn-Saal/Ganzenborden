using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceNumberTextScript : MonoBehaviour {

	public TextMeshProUGUI dice;
	public TextMeshProUGUI locations;
	public TextMeshProUGUI trapped;
	public TextMeshProUGUI skipping;
	public TextMeshProUGUI turn;
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
		skipping.text = "Players ";

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
			
			if(PlayerMovement.playTrap[i] && PlayerMovement.someTrapped){
				trapped.text += color + ", ";
			}       
			else{
				trapped.text = "No players are trapped";
			}

			if(PlayerMovement.skipRound[i] && PlayerMovement.someSkip){
				skipping.text += color + ",<br>";
			}
			else{
				skipping.text = "No players are skipping this turn";
			}
		}


	}
}
