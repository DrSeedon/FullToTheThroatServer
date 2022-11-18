using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodCreater : MonoBehaviour
{
    public TMP_InputField inputFieldName;
    public TMP_InputField inputFieldPrice;

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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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

    public void DeleteFood()
    {
        
    }

    public void ChangeFoodAvailable()
    {
        
    }
}