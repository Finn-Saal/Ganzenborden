using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void SetPlayers (int players)
    {
        PlayerMovement.playerMax= players-1;
    }
}
