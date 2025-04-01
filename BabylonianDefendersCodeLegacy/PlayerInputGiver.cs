using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputGiver : MonoBehaviour
{
    public PlayerInputData ThisPlayerData;

    public void SetPlayerData(PlayerInputData inputData)
    {
        ThisPlayerData = inputData;
    }
}
