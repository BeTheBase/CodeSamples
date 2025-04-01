using UnityEngine;
using UnityEngine.Assertions;

using Unity.Collections;
using Unity.Networking.Transport;

public interface IServer
{
    void BroadCast(NetMessage msg);
    void SendToClient(NetworkConnection connection, NetMessage msg);
}

public class ServerBehaviour : GenericSingleton<ServerBehaviour, IServer>, IServer
{
    public string IpAdress = "62.45.15.150";
    public ushort Port = 5522;
    public NetworkDriver Driver;
    protected NativeList<NetworkConnection> connections;
    protected UpdatePlayerPosition gameManager;
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdateServer();
    }

    private void OnDestroy()
    {
        Shutdown();
    }


    public virtual void Init()
    {
        //init driver
        Driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.Parse(IpAdress, Port); //who can connect
        endpoint.Port = 5522;
        if (Driver.Bind(endpoint) != 0)
            Debug.Log("There was error binding to port" + endpoint.Port);
        else
            Driver.Listen();

        //init the connection
        connections = new NativeList<NetworkConnection>(4, Allocator.Persistent); 
    }

    public virtual void UpdateServer()
    {
        Driver.ScheduleUpdate().Complete();
        CleanupConnections();
        AcceptNewConnections();
        UpdateMessagePump();
    }

    private void CleanupConnections()
    {
        for(int i = 0; i < connections.Length; i++)
        {
            if (!connections[i].IsCreated)
            {
                connections.RemoveAtSwapBack(i);
                --i;
            }
        }
    }

    private void AcceptNewConnections()
    {
        NetworkConnection c;
        while((c =Driver.Accept())!=default(NetworkConnection))
        {
            connections.Add(c);
            Debug.Log("Accepted a connection");
        }
    }

    public virtual void BroadCast(NetMessage msg)
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].IsCreated)
                SendToClient(connections[i], msg);
        }
    }

    public virtual void SendToClient(NetworkConnection connection, NetMessage msg)
    {
       
            DataStreamWriter writer;
            Driver.BeginSend(connection, out writer);
            msg.Serialize(ref writer);
            Driver.EndSend(writer);
       
    }

    protected virtual void UpdateMessagePump()
    {
        DataStreamReader stream;
        for(int i = 0; i < connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while((cmd = Driver.PopEventForConnection(connections[i], out stream))!= NetworkEvent.Type.Empty)
            {
                if(cmd==NetworkEvent.Type.Data)
                {
                    OnData(stream);                  

                    //Sync data
                }
                else if(cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from server");
                    connections[i] = default(NetworkConnection);
                }
            }
        }    
    }

    public virtual void OnData(DataStreamReader stream)
    {
        NetMessage msg = null;
        var opCode = (OpCode)stream.ReadByte();
        switch(opCode)
        {
            case OpCode.CHAT_MESSAGE:
                msg = new NeChatMessage(stream);
                break;
            case OpCode.PLAYER_POSITION:
                msg = new NetPlayerPosition(stream);
                break;
            case OpCode.ENTER_LEVEL:
                msg = new NetEnterWorld(stream);
                break;
            case OpCode.PROJECTILE_POSITION:
                msg = new NetProjectilePosition(stream);
                break;
            default:
                Debug.Log("No OpCode");
                break;
        }
        gameManager = UpdatePlayerPosition.Instance;
        msg.ReceivedOnServer(this, gameManager);
    }

    public virtual void Shutdown()
    {
        Driver.Dispose();
        connections.Dispose();
    }

}
