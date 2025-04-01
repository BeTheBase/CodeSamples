using UnityEngine;
using UnityEngine.Assertions;

using Unity.Collections;
using Unity.Networking.Transport;

public interface IClient
{
    void SendToServer(NetMessage msg);
}

public class ClientBehaviour : GenericSingleton<ClientBehaviour, IClient>, IClient
{
    public string IpAdress = "62.45.15.150";
    public ushort Port = 5522;
    public NetworkDriver Driver;
    protected NetworkConnection connection;
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
        connection = default(NetworkConnection);
        NetworkEndPoint endpoint = NetworkEndPoint.Parse(IpAdress, Port); //who can connect
        endpoint.Port = 5522;
        connection = Driver.Connect(endpoint);
    }

    public virtual void UpdateServer()
    {
        Driver.ScheduleUpdate().Complete();
        CheckAlive();
        UpdateMessagePump();
    }

    private void CheckAlive()
    {
        if(!connection.IsCreated)
        {
            Debug.Log("Something whent wrong, lost connection to server");
        }
    }

    protected virtual void UpdateMessagePump()
    {
        DataStreamReader stream;


            NetworkEvent.Type cmd;
            while ((cmd = connection.PopEvent(Driver, out stream))!= NetworkEvent.Type.Empty)
            {
                if(cmd== NetworkEvent.Type.Connect)
                {
                    Debug.Log("We are connected to server");
                }
                else if(cmd == NetworkEvent.Type.Data)
            {
                OnData(stream);
            }
                else if(cmd == NetworkEvent.Type.Disconnect)
            {
                Debug.Log("Client got disconnected from server");
                connection = default(NetworkConnection);
            }
            }       
    }

    public virtual void OnData(DataStreamReader stream)
    {
        NetMessage msg = null;
        var opCode = (OpCode)stream.ReadByte();
        switch (opCode)
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
        msg.ReceivedOnClient(gameManager);
    }

    public virtual void SendToServer(NetMessage msg)
    {
        DataStreamWriter writer;
        Driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        Driver.EndSend(writer);
    }

    public virtual void Shutdown()
    {
        Driver.Dispose();
    }
}
