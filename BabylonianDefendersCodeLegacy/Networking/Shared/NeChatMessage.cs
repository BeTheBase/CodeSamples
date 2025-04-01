using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;
using System.Linq;
using Unity.Collections;

public class NeChatMessage : NetMessage
{

    public FixedString4096Bytes ChatMessage { set; get; }

    public NeChatMessage()
    {
        Code = OpCode.CHAT_MESSAGE;
    }

    public NeChatMessage(DataStreamReader reader)
    {
        Code = OpCode.CHAT_MESSAGE;
        Deserialize(reader);
    }

    public NeChatMessage(string msg)
    {
        Code = OpCode.CHAT_MESSAGE;
        ChatMessage = msg;
    }


    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteFixedString4096(ChatMessage);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        //The fisrt byte is handled already 
        ChatMessage = reader.ReadFixedString4096();
    }

    public override void ReceivedOnServer(ServerBehaviour server, UpdatePlayerPosition upp)
    {
        Debug.Log("SERVER:" + ChatMessage);
    }

    public override void ReceivedOnClient(UpdatePlayerPosition upp)
    {
        Debug.Log("CLIENT:" + ChatMessage);
    }
}
