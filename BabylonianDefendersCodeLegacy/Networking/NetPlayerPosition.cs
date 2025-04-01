using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;
using System.Linq;
using Unity.Collections;
public class NetPlayerPosition : NetMessage
{
    public int PlayerId { get; private set; }
    public float PositionX { get; private set; }
    public float PositionY { get; private set; }
    public float PositionZ { get; private set; }

    public FixedString4096Bytes ChatMessage { set; get; }

    public NetPlayerPosition()
    {
        Code = OpCode.PLAYER_POSITION;
    }

    public NetPlayerPosition(DataStreamReader reader)
    {
        Code = OpCode.PLAYER_POSITION;
        Deserialize(reader);
    }

    public NetPlayerPosition(int playerId, float x, float y, float z)
    {
        Code = OpCode.PLAYER_POSITION;
        PlayerId = playerId;
        PositionX = x;
        PositionY = y;
        PositionZ = z;
    }

    public Vector3 GetPlayerPosition()
    {
        return new Vector3(PositionX, PositionY, PositionZ);
    }


    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(PlayerId);
        writer.WriteFloat(PositionX);
        writer.WriteFloat(PositionY);
        writer.WriteFloat(PositionZ);

    }

    public override void Deserialize(DataStreamReader reader)
    {
        //The fisrt byte is handled already 
        PlayerId = reader.ReadInt();
        PositionX = reader.ReadFloat();
        PositionY = reader.ReadFloat();
        PositionZ = reader.ReadFloat();
    }

    public override void ReceivedOnServer(ServerBehaviour server, UpdatePlayerPosition upp)
    {
        //Debug.Log("SERVER:" + PlayerId + "::" + PositionX + PositionY + PositionZ);
        server.BroadCast(this);
        if (upp!=null) upp.ReceiveNewPlayerPosition(new Vector3(PositionX, PositionY, PositionZ), PlayerId);
    }

    public override void ReceivedOnClient(UpdatePlayerPosition upp)
    {
        //Debug.Log("CLIENT:" + PlayerId + "::" + PositionX + PositionY + PositionZ);
        if (upp != null) upp.ReceiveNewPlayerPosition(new Vector3(PositionX, PositionY, PositionZ), PlayerId);
    }
}
