using UnityEngine;
using System;

public abstract class BaseTool : MonoBehaviour
{
    public string toolName;
    public float durability;
    public event Action<ItemCore> OnToolBreak; // Event to notify when the tool breaks

    public abstract void Use(); // Each tool must implement its own usage behavior

    protected void BreakTool()
    {
        OnToolBreak?.Invoke(GetComponent<ItemCore>()); // Pass the ItemCore to the inventory
    }
}