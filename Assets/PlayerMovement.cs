using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int curLoc= 1;

    public GameObject board;
    public Vector3 positionToGo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(board.transform.Find("Vakje " + curLoc).position);
        FindPosition();
        transform.position = positionToGo;
    }

    void FindPosition()
    {
        positionToGo = board.transform.Find("Vakje " + curLoc).position; 
    }
}
