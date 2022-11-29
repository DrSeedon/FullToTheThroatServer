using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Riptide;
using UnityEngine;
using yutokun;

public class LoggingManager : StaticInstance<LoggingManager>
{
    [Serializable]
    public class LoginData
    {
        public string login;
        public string password;
        public string name;

        public LoginData(string log, string pass)
        {
            login = log;
            password = pass;
        }
    }
    
    public ushort IdClient;

    public void ParseCSV()
    {
        
        var sheet = CSVParser.LoadFromPath(Application.streamingAssetsPath + "/loginData.csv");
        DataManager.Instance.loginDatas.Clear();
        
        foreach (var row in sheet)
        {
            string[] stringData = new string[] { };
            foreach (var cell in row)
            {
                stringData = cell.Split(';');
            }
            DataManager.Instance.loginDatas.Add(new LoginData(stringData[0], stringData[1]));
        }
    }
    
    
    private void CheckLogin(LoginData data)
    {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort) ServerToClientId.loggingResponse);

        bool isCorrect = false;
        foreach (var loginData in DataManager.Instance.loginDatas)
        {
            if (loginData.login == data.login && loginData.password == data.password)
                isCorrect = true;
        }
        message.AddBool(isCorrect);
        NetworkManager.Instance.Server.Send(message, IdClient);
    }
    
    [MessageHandler((ushort) ClientToServerId.loggingRequest)]
    private static void FoodDataJsonReceived(ushort fromClientId, Message message)
    {
        LoginData data = JsonUtility.FromJson<LoginData>(message.GetString());
        Instance.IdClient = fromClientId;
        Instance.CheckLogin(data);
    }



}
