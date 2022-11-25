using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderRowElement : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text countText;
    public FoodData foodData;

    public virtual void SetData(FoodData data, int count)
    {
        foodData = data;
        titleText.text = data.name;
        countText.text = count.ToString();
    }
    
}
