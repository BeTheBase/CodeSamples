using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetEnterWorld : NetMessage
{
    private int index;

    public NetEnterWorld()
    {
        Code = OpCode.ENTER_LEVEL;
    }

    public NetEnterWorld(int _index)
    {
        Code = OpCode.ENTER_LEVEL;
        index = _index;
    }

    public NetEnterWorld(DataStreamReader reader)
    {
        Code = OpCode.ENTER_LEVEL;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        //The fisrt byte is handled already 
    }

    public override void ReceivedOnServer(ServerBehaviour server, UpdatePlayerPosition upp)
    {
        server.BroadCast(this);
        if (upp != null) upp.NextLevel(index);

    }

    public override void ReceivedOnClient(UpdatePlayerPosition upp)
    {
        //Start level syn with server start level
        //LevelLoader.loadnextlevel aanroepen???
        //hier?
        if (upp != null) upp.NextLevel(index);
    }
}
