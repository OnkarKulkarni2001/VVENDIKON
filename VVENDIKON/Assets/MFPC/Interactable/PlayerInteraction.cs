using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3.0f;
    Interactable currentInteractable;

    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
        else
        {
            //Debug.Log("Idher");
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);



        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if (currentInteractable != null && newInteractable != currentInteractable)
                {
                    currentInteractable = newInteractable;
                }

                if (newInteractable.enabled)
                {
                    SetNewInteractable(newInteractable);
                }
                else
                {
                    DisabelCurrentInteractable();
                }
            }
            else
            {
                DisabelCurrentInteractable();
            }
        }
        else
        {
            DisabelCurrentInteractable();
        }
    }

    void SetNewInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.showUi(true);
        HudController.instance.EnableDisableInteractionText(currentInteractable.description, true);
    }

    void DisabelCurrentInteractable()
    {
        if (currentInteractable)
        {
            HudController.instance.EnableDisableInteractionText(currentInteractable.description, false);
            currentInteractable.showUi(false);
            currentInteractable = null;

        }
    }
}