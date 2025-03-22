using System.Diagnostics;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public string creatureName = "Unnamed Creature";
    private bool isGrabbed = false;

    public void Grab(Transform holder)
    {
        if (!isGrabbed)
        {
            isGrabbed = true;
            transform.SetParent(holder);
            transform.localPosition = Vector3.zero;
            Debug.Log($"{creatureName} has been grabbed!");
            // Disable physics or movement if applicable
            if (TryGetComponent<Rigidbody>(out var rb)) rb.isKinematic = true;
        }
    }

    public void Release()
    {
        if (isGrabbed)
        {
            isGrabbed = false;
            transform.SetParent(null);
            if (TryGetComponent<Rigidbody>(out var rb)) rb.isKinematic = false;
            Debug.Log($"{creatureName} has been released!");
        }
    }
}