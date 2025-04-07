using UnityEngine;
using System;

public abstract class BaseWeapon : MonoBehaviour
{
    public string weaponName;
    public float damage;
    public float range;
    public float durability; // Added for consistency with tools
    public event Action<ItemCore> OnWeaponBreak; // Event to notify inventory

    public abstract void Use(); // Each weapon will implement its own behavior

    protected void BreakWeapon()
    {
        OnWeaponBreak?.Invoke(GetComponent<ItemCore>());
    }
}