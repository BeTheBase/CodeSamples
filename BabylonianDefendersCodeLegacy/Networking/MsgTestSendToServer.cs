using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MsgTestSendToServer : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    public void OnMessage()
    {
        NeChatMessage m = new NeChatMessage(input.text);
        FindObjectOfType<ServerBehaviour>()?.BroadCast(m);
    }
}
