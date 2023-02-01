using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void SetPlayers (float players)
    {
        int playerInt = (int)players;
        PlayerMovement.playerMax = playerInt-1;
        //Debug.Log(players + " SM");

    }
}
