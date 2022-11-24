using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrderController : StaticInstance<FoodOrderController>
{
    public List<FoodData> foodData = new List<FoodData>();
    public GameObject prefabDataElement;
    public GameObject parentDataElement;

    public void CreateOrder(List<FoodData> data)
    {
        foodData = data;
        
        var obj = Instantiate(prefabDataElement, parentDataElement.transform);
        obj.SetActive(true);
        var dataElement = obj.GetComponent<OrderElement>();
        dataElement.SetData(data);
    }

    protected virtual void CreateElements()
    {
        
    }
}
