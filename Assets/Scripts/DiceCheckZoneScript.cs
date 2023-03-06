using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour {
	
	public GameObject dice;
	Vector3 diceVelocity;
	public static bool diceStat = false;	//check if dice has stopped rolling in the current round
	bool BoolCheck(bool a, bool b, bool c)
	{
	return (a != b) || (b != c);
	}


	// Update is called once per frame
	void FixedUpdate () {
		diceVelocity = DiceScript.diceVelocity;
	}

	void OnTriggerStay(Collider col)
	{
		if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f) //kijkt of the dobbelsteen stil staat 
		{
			//Debug.Log(BoolCheck(dice.transform.localRotation.eulerAngles.x % 90 <= 0.001, dice.transform.localRotation.eulerAngles.y % 90 <= 0.001, dice.transform.localRotation.eulerAngles.z % 90 <= 0.001));

			if(BoolCheck(dice.transform.localRotation.eulerAngles.x % 90 <= 0.001, dice.transform.localRotation.eulerAngles.y % 90 <= 0.001, dice.transform.localRotation.eulerAngles.z % 90 <= 0.001)){

			
				//defines the number on the 
				switch (col.gameObject.name) {
				case "Side1":
					DiceNumberTextScript.diceNumber = 6;
					break;
				case "Side2":
					DiceNumberTextScript.diceNumber = 5;
					break;
				case "Side3":
					DiceNumberTextScript.diceNumber = 4;
					break;
				case "Side4":
					DiceNumberTextScript.diceNumber = 3;
					break;
				case "Side5":
					DiceNumberTextScript.diceNumber = 2;
					break;
				case "Side6":
					DiceNumberTextScript.diceNumber = 1;
					break;
				}

				if(DiceScript.throwReady == false && DiceScript.spacePressed){
					diceStat = true;
					DiceScript.spacePressed = false;	
				}
			
			}
			else{
				DiceScript.rb.AddForce(transform.right * 500);

			}
		}
	}
}
