using Unity.Networking.Transport;
using UnityEngine;

public enum OpCode
{
    CHAT_MESSAGE = 1,
    PLAYER_POSITION = 2,
    ENTER_LEVEL = 3,
    PROJECTILE_POSITION =4,
}
public class NetMessage
{ 
    public OpCode Code { set; get; }

    public virtual void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }

    public virtual void Deserialize(DataStreamReader reader)
    {

    }


    public virtual void ReceivedOnClient(UpdatePlayerPosition upp)
    {
        
    }

    public virtual void ReceivedOnServer(ServerBehaviour server, UpdatePlayerPosition upp)
    {

    }
}
