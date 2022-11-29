using System;
using System.Collections;
using System.Collections.Generic;
using Riptide;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderElement : MonoBehaviour
{
    public TMP_Text titleText;
    public Order order;
    public Button readyFood;
    public Button issuedFood;

    public GameObject prefabDataElement;
    public GameObject parentDataElement;

    public bool isReady = false;

    private void Start()
    {
        readyFood.onClick.AddListener(FoodReady);
        issuedFood.onClick.AddListener(FoodIssued);
    }

    public void FoodIssued()
    {
        Destroy(gameObject);
        if(!isReady)
            FoodReady();
    }
    public void FoodReady()
    {
        string jsonString = JsonUtility.ToJson(order, true);
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ServerToClientId.foodReady);
        message.AddString(jsonString);
        NetworkManager.Instance.Server.Send(message, order.idClient);
        isReady = true;
        readyFood.interactable = false;
    }
    
    
    public virtual void SetData(Order data)
    {
        order = data;
        titleText.text = "Номер: " + data.numberOrder;

        foreach (var orderRow in order.orderRows)
        {
            var obj = Instantiate(prefabDataElement, parentDataElement.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<OrderRowElement>();
            dataElement.SetData(orderRow.foodData, orderRow.count);
        }
    }
    
    
}
