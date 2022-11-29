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


    public GameObject prefabDataElement;
    public GameObject parentDataElement;

    public virtual void CreateElements()
    {
        foreach (var data in DataManager.Instance.foodDatas)
        {
            var obj = Instantiate(prefabDataElement, parentDataElement.transform);
            obj.SetActive(true);
            var dataElement = obj.GetComponent<FoodElement>();
            dataElement.SetData(data);
        }
    }
    
    public void CreateFood()
    {
        FoodData foodData = new FoodData();
        foodData.name = inputFieldName.text;
        foodData.price = Convert.ToInt32(inputFieldPrice.text);

        DataManager.Instance.foodDatas.Add(foodData);

        var obj = Instantiate(prefabDataElement, parentDataElement.transform);
        obj.SetActive(true);
        var dataElement = obj.GetComponent<FoodElement>();
        dataElement.SetData(foodData);
        //dataElement.deleteFood.onClick.AddListener(DeleteFood);
        //dataElement.deleteFood.onClick.AddListener(() => actionToMaterial(index));
    }

}