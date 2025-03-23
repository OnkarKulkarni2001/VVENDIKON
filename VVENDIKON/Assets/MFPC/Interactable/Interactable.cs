using MFPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string description;
    public int itemPrice;
    public UnityEvent onInteract;

    CurrencySystem currencySystem;

    void Start()
    {
        currencySystem = FindObjectOfType<CurrencySystem>();
        showUi(false);
    }

    public void showUi(bool show)
    {
        if (show)
        {
            Debug.Log("Ui is Visble");
        }
        else
        {
            Debug.Log("Ui is Not Visble");
        }
    }

    public void Interact()
    {
        if(currencySystem.currentMoney >= itemPrice)
        {
            onInteract.Invoke();
            currencySystem.currentMoney -= itemPrice;
        }
        else
        {
            Debug.Log("Get Some Money peasant");
        }
    }
}