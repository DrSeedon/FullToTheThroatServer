using System;
using Riptide;
using Riptide.Utils;
using UnityEngine;

public enum ServerToClientId : ushort
{
    playerSpawned = 1,
    foodUpdate = 2,
}

public enum ClientToServerId : ushort
{
    name = 1,
    message = 2,
    foodDataJson = 3,
}

public class NetworkManager : Singleton<NetworkManager>
{
    public Server Server { get; private set; }

    public ushort port;
    public ushort maxClientCount;

    private void Start()
    {
        Application.targetFrameRate = 60;
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.ClientConnected += ClientConnected;
        Server.ClientDisconnected += ClientDisconnected;
    }

    private void FixedUpdate()
    {
        Server.Update();
    }

    protected override void OnApplicationQuit()
    {
        Server.Stop();
    }

    #region Event
    private void ClientDisconnected(object sender, ServerDisconnectedEventArgs e)
    {
        Destroy(Customer.list[e.Client.Id].gameObject);
    }
    private void ClientConnected(object sender, ServerConnectedEventArgs e)
    {
        Debug.Log("hui");
        string jsonString = JsonUtility.ToJson(FoodCreater.Instance.foodDatas, true);
        
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ServerToClientId.foodUpdate);
        message.AddString(jsonString);
        Server.Send(message, e.Client.Id);

    }
    #endregion
}
