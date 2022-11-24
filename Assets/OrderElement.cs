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
    public List<FoodData> foodData;
    public Button readyFood;
    public Button addOrRemoveFood;

    public GameObject prefabDataElement;
    public GameObject parentDataElement;

    private void Start()
    {
        readyFood.onClick.AddListener(FoodReady);
    }

    public void FoodReady()
    {
        string jsonString = JsonHelper.ToJson(foodData, true);
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ServerToClientId.foodReady);
        message.AddString(jsonString);
        NetworkManager.Instance.Server.SendToAll(message);
    }
    
    
    public virtual void SetData(List<FoodData> datas)
    {
        foodData = datas;

        foreach (var data in datas)
        {
            var obj = Instantiate(prefabDataElement, parentDataElement.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<OrderRowElement>();
            dataElement.SetData(data);
        }
    }
    
    
}
