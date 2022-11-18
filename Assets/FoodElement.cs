using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class FoodElement : MonoBehaviour
{
    public TMP_Text titleText;
    public FoodData FoodData;
    public Button deleteFood;
    public Button addOrRemoveFood;
    public virtual void SetData(FoodData data)
    {
        this.FoodData = data;
        titleText.text = data.name;
    }
}
