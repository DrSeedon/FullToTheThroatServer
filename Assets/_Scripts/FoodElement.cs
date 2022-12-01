using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class FoodElement : MonoBehaviour
{
    public TMP_Text titleText;
    public FoodData foodData;
    public Button deleteFood;
    public Button addOrRemoveFood;

    public TMP_Text stateText;
    public Color availableColor;
    public Color unavailableColor;
    public string availableText;
    public string unavailableText;

    public TMP_InputField weightInput;
    public TMP_InputField compositionInput;
    public TMP_InputField priceInput;

    public TMP_Dropdown dropdownField;
    
    public virtual void SetData(FoodData data)
    {
        this.foodData = data;
        titleText.text = data.name;
        weightInput.text = data.weight;
        compositionInput.text = data.composition;
        priceInput.text = data.price.ToString();
        dropdownField.value = foodData.idСategory;
        SetVisualState(foodData.isAvailable);
    }

    public void SaveData()
    {
        //DataManager.Instance.foodDatas.Find(x => x.name == foodData.name).composition = compositionInput.text;
        //DataManager.Instance.foodDatas.Find(x => x.name == foodData.name).weight = weightInput.text;
        
        foodData.composition = compositionInput.text;
        foodData.price = Convert.ToSingle(priceInput.text);
        foodData.weight = weightInput.text;
        foodData.idСategory = dropdownField.value;
    }

    public void SetImageId(int id)
    {
        foodData.idImage = id;
    }

    public void ChangeState()
    {
        foodData.isAvailable = !foodData.isAvailable;
        SetVisualState(foodData.isAvailable);
    }

    public void SetVisualState(bool value)
    {
        stateText.text = value ? availableText : unavailableText;
        stateText.color = value ? availableColor : unavailableColor;
    }

    public void DeleteElement()
    {
        //var find = FoodCreater.Instance.foodDatas.Find(x => x.name == foodData.name);
        DataManager.Instance.foodDatas.Remove(foodData);
        Destroy(gameObject);
    }
}
