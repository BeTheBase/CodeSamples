using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatMSGTEST : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    public void OnMessage()
    {
        NeChatMessage m = new NeChatMessage(input.text);
        FindObjectOfType<ClientBehaviour>()?.SendToServer(m);
    }
}
