using System.Diagnostics;
using System;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public string creatureName = "Unnamed Creature";
    private bool isGrabbed = false;
    private bool isStunned = false;
    private float stunTimer = 0f;
    private float baseGrabResistance = 1f; // Higher = harder to grab
    private float currentGrabResistance;

    private void Awake()
    {
        currentGrabResistance = baseGrabResistance;
    }

    private void Update()
    {
        if (isStunned && stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                isStunned = false;
                currentGrabResistance = baseGrabResistance;
                Debug.Log($"{creatureName} is no longer stunned!");
            }
        }
    }

    public void Grab(Transform holder)
    {
        if (!isGrabbed)
        {
            // Use UnityEngine.Random explicitly
            if (UnityEngine.Random.value < currentGrabResistance && !isStunned)
            {
                Debug.Log($"{creatureName} resisted the grab!");
                return;
            }

            isGrabbed = true;
            transform.SetParent(holder);
            transform.localPosition = Vector3.zero;
            Debug.Log($"{creatureName} has been grabbed!");
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

    public void ApplyStun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
        currentGrabResistance = 0f; // Stunned creatures can’t resist
        Debug.Log($"{creatureName} is stunned for {duration} seconds!");
    }

    public void ApplyWeakness(float resistanceReduction, float duration)
    {
        currentGrabResistance = Mathf.Max(0, baseGrabResistance - resistanceReduction);
        Debug.Log($"{creatureName} is weakened! Grab resistance: {currentGrabResistance}");
        // Reset after duration (could use a coroutine or timer)
        Invoke(nameof(ResetResistance), duration);
    }

    private void ResetResistance()
    {
        currentGrabResistance = baseGrabResistance;
        Debug.Log($"{creatureName} regained strength!");
    }
}