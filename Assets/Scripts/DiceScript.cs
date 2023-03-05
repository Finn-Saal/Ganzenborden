using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	public static Rigidbody rb;
	public static Vector3 diceVelocity;
	public static bool throwReady = true;
	public static bool spacePressed = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		diceVelocity = rb.velocity;
		if(throwReady == true) {
			if (Input.GetKeyDown (KeyCode.Space) && throwReady) {
				throwReady = false;
				DiceNumberTextScript.diceNumber = 0;
				float dirX = Random.Range (500, 2000);
				float dirY = Random.Range (500, 2000);
				float dirZ = Random.Range (500, 2000);
				transform.position = transform.position + new Vector3 (0, 3, 0);
				transform.rotation = Quaternion.identity;
				rb.AddForce (transform.up * 500);
				rb.AddTorque (dirX, dirY, dirZ);
				spacePressed = true;
			}
		}
	}
}
