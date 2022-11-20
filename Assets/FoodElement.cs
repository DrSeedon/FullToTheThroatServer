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
    
    public virtual void SetData(FoodData data)
    {
        this.foodData = data;
        titleText.text = data.name;
        SetVisualState(foodData.isAvailable);
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
        FoodCreater.Instance.foodDatas.Remove(foodData);
        Destroy(gameObject);
    }
}
