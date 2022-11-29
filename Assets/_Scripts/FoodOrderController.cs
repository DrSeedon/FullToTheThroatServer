using System.Collections;
using System.Collections.Generic;
using Riptide;
using UnityEngine;

public class FoodOrderController : StaticInstance<FoodOrderController>
{
    public GameObject prefabDataElement;
    public GameObject parentDataElement;

    public void CreateOrder(Order data)
    {
        DataManager.Instance.orders.Add(data);
        var obj = Instantiate(prefabDataElement, parentDataElement.transform);
        obj.SetActive(true);
        var dataElement = obj.GetComponent<OrderElement>();
        dataElement.SetData(data);

        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ServerToClientId.orderNumberResponse);
        message.AddInt(data.numberOrder);
        NetworkManager.Instance.Server.Send(message, data.idClient);
    }

    protected virtual void CreateElements()
    {
        
    }
}
