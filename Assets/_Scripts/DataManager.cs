using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : StaticInstance<DataManager>
{
    [SerializeReference] public List<FoodData> foodDatas = new List<FoodData>();
    [SerializeReference] public List<Order> orders = new List<Order>();
    [SerializeReference] public List<LoggingManager.LoginData> loginDatas = new List<LoggingManager.LoginData>();
    
    [Serializable]
    public class SaveData
    {
        public List<FoodData> foodDatas = new List<FoodData>();
        public List<Order> orders = new List<Order>();
        public List<LoggingManager.LoginData> loginDatas = new List<LoggingManager.LoginData>();
        public int orderNumber = 0;
    }

    public SaveData saveData = new SaveData();


    void Start()
    {
        LoadGame();
        FoodCreater.Instance.CreateElements();
        LoggingManager.Instance.ParseCSV();
    }
    private void OnDestroy()
    {
        SaveGame();
    }


    public void SaveGame()
    {
        ResetData();
        saveData.foodDatas = foodDatas;
        saveData.orders = orders;
        saveData.loginDatas = loginDatas;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.streamingAssetsPath
                                      + "/data.dat");

        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.streamingAssetsPath
                        + "/data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.streamingAssetsPath
                          + "/data.dat", FileMode.Open);
            saveData = (SaveData) bf.Deserialize(file);
            foodDatas = saveData.foodDatas;
            orders = saveData.orders;
            loginDatas = saveData.loginDatas;
            file.Close();
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }

    void ResetData()
    {
        if (File.Exists(Application.streamingAssetsPath
                        + "/data.dat"))
        {
            File.Delete(Application.streamingAssetsPath
                        + "/data.dat");
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }

    void EraseData()
    {
        ResetData();
        foodDatas.Clear();
    }
}
