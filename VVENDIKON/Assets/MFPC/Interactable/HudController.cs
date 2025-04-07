using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{

    public static HudController instance;


    private void Awake()
    {
        instance = this;
        interactionText.gameObject.SetActive(false);
    }

    [SerializeField] TMP_Text interactionText;

    public void EnableDisableInteractionText(string text, bool enable)
    {
        if (enable)
        {
            interactionText.text = text + " (E)";
            interactionText.gameObject.SetActive(true);
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }

    }
}