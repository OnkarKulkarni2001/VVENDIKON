using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentMoney;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckCanBuy(int price)
    {
        if (currentMoney > price)
        {
            Debug.Log("Can Buy");
        }
        else
        {
            Debug.Log("Cant Buy");
        }
    }
}
