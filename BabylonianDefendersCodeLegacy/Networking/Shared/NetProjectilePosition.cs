using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

public class NetProjectilePosition : NetMessage
{
    public int ProjectileId { get; private set; }
    public float PositionX { get; private set; }
    public float PositionY { get; private set; }
    public float PositionZ { get; private set; }


    public NetProjectilePosition()
    {
        Code = OpCode.PROJECTILE_POSITION;
    }

    public NetProjectilePosition(DataStreamReader reader)
    {
        Code = OpCode.PROJECTILE_POSITION;
        Deserialize(reader);
    }

    public NetProjectilePosition(int projectileId, float x, float y, float z)
    {
        Code = OpCode.PROJECTILE_POSITION;
        ProjectileId = projectileId;
        PositionX = x;
        PositionY = y;
        PositionZ = z;
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(ProjectileId);
        writer.WriteFloat(PositionX);
        writer.WriteFloat(PositionY);
        writer.WriteFloat(PositionZ);

    }

    public override void Deserialize(DataStreamReader reader)
    {
        //The fisrt byte is handled already 
        ProjectileId = reader.ReadInt();
        PositionX = reader.ReadFloat();
        PositionY = reader.ReadFloat();
        PositionZ = reader.ReadFloat();
    }

    public override void ReceivedOnServer(ServerBehaviour server, UpdatePlayerPosition upp)
    {
        //Debug.Log("SERVER:" + PlayerId + "::" + PositionX + PositionY + PositionZ);
        server.BroadCast(this);
        if (upp != null) upp.SyncProjectiles(new Vector3(PositionX, PositionY, PositionZ), ProjectileId);
    }

    public override void ReceivedOnClient(UpdatePlayerPosition upp)
    {
        //Debug.Log("CLIENT:" + PlayerId + "::" + PositionX + PositionY + PositionZ);
        if (upp != null) upp.SyncProjectiles(new Vector3(PositionX, PositionY, PositionZ), ProjectileId);
    }
}
