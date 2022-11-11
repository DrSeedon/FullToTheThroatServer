using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodCreater : MonoBehaviour
{
    public TMP_InputField inputFieldName;
    public TMP_InputField inputFieldPrice;

    public List<FoodData> foodDatas = new List<FoodData>();
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
    }
}
