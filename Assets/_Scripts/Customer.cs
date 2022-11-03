using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;
using Unity.VisualScripting;

public class Customer : MonoBehaviour
{
    public static Dictionary<ushort, Customer> list = new Dictionary<ushort, Customer>();
    
    public ushort Id { get; private set; }
    public string Username { get; private set; }

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    public static void Spawn(ushort id, string username)
    {
        foreach (var otherCustomer in list.Values)
            otherCustomer.SendSpawned(id);
        
        Customer customer = Instantiate(ShopLogic.Instance.clientPrefab, Vector3.zero, Quaternion.identity)
            .GetComponent<Customer>();
        customer.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
        customer.Id = id;
        customer.Username = string.IsNullOrEmpty(username) ? $"Guest {id}" : username;

        customer.SendSpawned();
        list.Add(id, customer);
    }

    #region Messages

    /// <summary>
    /// Отправить всем клиентам сообщение
    /// </summary>
    private void SendSpawned()
    {
        NetworkManager.Instance.Server.SendToAll(AddSpawnData(Message.Create(MessageSendMode.Reliable,
            (ushort) ServerToClientId.playerSpawned)));
    }

    /// <summary>
    /// Отправить определенному клиенту сообщение
    /// </summary>
    private void SendSpawned(ushort toClientId)
    {
        NetworkManager.Instance.Server.Send(AddSpawnData(Message.Create(MessageSendMode.Reliable,
            (ushort) ServerToClientId.playerSpawned)), toClientId);
    }
    /// <summary>
    /// Вспомогательный метод. Добавляем данные в сообщение
    /// </summary>
    /// <param name="message">Инициализированное сообщение в которое добавляем данные</param>
    /// <returns>Сообщение с данными</returns>
    private Message AddSpawnData(Message message)
    {
        message.AddUShort(Id);
        message.AddString(Username);
        return message;
    }
    /// <summary>
    /// Ловим данные от клиента с указанным id (ClientToServerId.name)
    /// </summary>
    /// <param name="fromClientId">ID Клиента</param>
    /// <param name="message">Сообщение</param>
    [MessageHandler((ushort) ClientToServerId.name)]
    private static void Name(ushort fromClientId, Message message)
    {
        Spawn(fromClientId, message.GetString());
    }
    #endregion
}
