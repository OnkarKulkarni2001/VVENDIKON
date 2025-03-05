using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string description;
    public UnityEvent onInteract;

    void Start()
    {
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
        onInteract.Invoke();
    }

}