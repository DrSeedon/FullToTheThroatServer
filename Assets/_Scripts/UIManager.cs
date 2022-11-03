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
    /// <param name="message"></param>
    [MessageHandler((ushort) ClientToServerId.message)]
    private static void MessageRecived(ushort fromClientId, Message message)
    {
        AddText(fromClientId, message.GetString());
    }

    #endregion
}
