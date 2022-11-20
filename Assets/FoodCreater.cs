using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodCreater : Singleton<FoodCreater>
{
    public TMP_InputField inputFieldName;
    public TMP_InputField inputFieldPrice;

    [SerializeReference]
    public List<FoodData> foodDatas = new List<FoodData>();

    public GameObject prefabDataElement;
    public GameObject parentDataElement;

    protected virtual void CreateElements()
    {
        foreach (var data in foodDatas)
        {
            var obj = Instantiate(prefabDataElement, parentDataElement.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<FoodElement>();
            dataElement.SetData(data);
        }
    }
    void Start()
    {
        LoadGame();
        CreateElements();
    }
    
    private void OnDestroy()
    {
        SaveGame();
    }
    
    

    public void CreateFood()
    {
        FoodData foodData = new FoodData();
        foodData.name = inputFieldName.text;
        foodData.price = Convert.ToInt32(inputFieldPrice.text);

        foodDatas.Add(foodData);

        var obj = Instantiate(prefabDataElement, parentDataElement.transform);
        obj.SetActive(true);
        var dataElement = obj.GetComponent<FoodElement>();
        dataElement.SetData(foodData);
        //dataElement.deleteFood.onClick.AddListener(DeleteFood);
        //dataElement.deleteFood.onClick.AddListener(() => actionToMaterial(index));
    }

    [Serializable]
    public class SaveData
    {
        public List<FoodData> foodDatas = new List<FoodData>();
    }

    public SaveData saveData = new SaveData();

    public void SaveGame()
    {
        ResetData();
        saveData.foodDatas = foodDatas;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.streamingAssetsPath
                                      + "/data.dat");

        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Game data saved!");
    }

    void LoadGame()
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