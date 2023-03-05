using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    // Update is called once per frame
    void OnCollisionEnter(Collision col)
	{
		
			//defines the number on the 
			switch (col.gameObject.name) {
			case "Wall1":
				DiceScript.rb.AddForce(transform.right * 500);;
				break;
			case "Wall2":
				DiceScript.rb.AddForce(transform.forward * -500);;
				break;
			case "Wall3":
				DiceScript.rb.AddForce(transform.right * -500);;
				break;
			case "Wall4":
				DiceScript.rb.AddForce(transform.forward * 500);;
				break;
			}
	}
}
