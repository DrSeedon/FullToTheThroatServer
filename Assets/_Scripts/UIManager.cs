using System;
using System.Collections;
using System.Collections.Generic;
using Riptide;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static TMP_Text messageText;

    private void Start()
    {
        messageText = FindObjectOfType<TMP_Text>();
    }

    private static void AddText(ushort id, string value)
    {
        Debug.Log(id + " " + value);
        //messageText.text += value + "/n";
    }

    #region Messages

    /// <summary>
    /// Принять сообщение с текстом
    /// </summary>
    /// <param name="fromClientId">ID клиента</param>
    /// <param name="message">Сообщение</param>
    [MessageHandler((ushort) ClientToServerId.message)]
    private static void MessageReceived(ushort fromClientId, Message message)
    {
        AddText(fromClientId, message.GetString());
    }

    /// <summary>
    /// Принять сообщение с едой
    /// </summary>
    /// <param name="fromClientId">ID клиента</param>
    /// <param name="message">Сообщение</param>
    [MessageHandler((ushort) ClientToServerId.foodDataJson)]
    private static void FoodDataJsonReceived(ushort fromClientId, Message message)
    {
        List<FoodData> foodDatas = JsonHelper.FromJsonList<FoodData>(message.GetString());
        FoodOrderController.Instance.CreateOrder(foodDatas);
    }
    
    #endregion
}
