using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderRowElement : MonoBehaviour
{
    public TMP_Text titleText;
    public FoodData foodData;

    public virtual void SetData(FoodData data)
    {
        foodData = data;
        titleText.text = data.name;
    }
    
}
