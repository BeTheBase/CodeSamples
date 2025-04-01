using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInputData
{
    public string name;
    public GameObject CurrentPlayerObject;
    public InputControllScheme ThisPlayerControllScheme;
    public float Vitality;
    public float AttackForce;
    public float JumpForce;
}

[System.Serializable]
public class InputControllScheme
{
    public KeyCode Left, Right, Jump, Attack;
}

public class PlayerInputManager : MonoBehaviour
{
    public List<PlayerInputData> Players;

    private void Start()
    {
        foreach(PlayerInputData player in Players)
        {
            if (player == null) return;
            player.CurrentPlayerObject.GetComponent<PlayerInputGiver>().SetPlayerData(player);
        }
    }
}
