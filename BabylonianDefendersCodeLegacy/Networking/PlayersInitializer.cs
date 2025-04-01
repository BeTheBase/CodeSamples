using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInitializer : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject PlayerPrefab;

    public Transform Player1Spawn;
    public Transform Player2Spawn;
    public void Start()
    {

        if (Object.FindObjectOfType<ClientBehaviour>() == null)
        {
            //We have a server yay! Can't play game without server
            //We are the server so we are player1
            //GameObject player1 = GameObject.Instantiate(PlayerPrefab, Player1Spawn);
            Player1.GetComponent<SendPlayerPosition>().Id = 1;
            Player1.SetActive(true);
            Player2.GetComponent<SendPlayerPosition>().Id = 2;
            Player2.SetActive(true);
            Player2.GetComponent<SendPlayerPosition>().ToggleScripts(false);
            Debug.LogError("01");
        }
        else if(Object.FindObjectOfType<ServerBehaviour>() == null)
        {
            //alright we are a client 
            Player1.GetComponent<SendPlayerPosition>().Id = 1;
            Player1.SetActive(true);
            Player2.GetComponent<SendPlayerPosition>().Id = 2;
            Player2.SetActive(true);
            Player1.GetComponent<SendPlayerPosition>().ToggleScripts(false);
            Debug.LogError("02");
        }
        else
        {
            Player1.GetComponent<SendPlayerPosition>().Id = 1;
            Player1.SetActive(true);
            Player2.GetComponent<SendPlayerPosition>().Id = 2;
            Player2.SetActive(true);
            Player2.GetComponent<SendPlayerPosition>().ToggleScripts(false);
            Debug.LogError("03");
        }
    }
}
