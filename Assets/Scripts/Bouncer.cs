using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;

    // Update is called once per frame
    void OnCollisionEnter(Collider col)
	{
			//defines the number on the 
			switch (col.gameObject.CompareTag) {
			case "Side1":
				break;
			case "Side2":
				break;
			case "Side3":
				break;
			case "Side4":
				break;
			case "Side5":
				break;
			case "Side6":
				break;
			}
}
