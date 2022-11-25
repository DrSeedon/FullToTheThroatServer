using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrderController : StaticInstance<FoodOrderController>
{
    public GameObject prefabDataElement;
    public GameObject parentDataElement;

    public void CreateOrder(Order data)
    {
        DataManager.Instance.orders.Add(data);
        var obj = Instantiate(prefabDataElement, parentDataElement.transform);
        obj.SetActive(true);
        var dataElement = obj.GetComponent<OrderElement>();
        dataElement.SetData(data);
    }

    protected virtual void CreateElements()
    {
        
    }
}
