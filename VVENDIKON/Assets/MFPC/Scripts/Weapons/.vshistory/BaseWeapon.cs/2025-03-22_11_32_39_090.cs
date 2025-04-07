using UnityEngine;
using System;
using System.Diagnostics;

public abstract class BaseWeapon : MonoBehaviour
{
    public string weaponName;
    public float damage;
    public float range;
    public event Action<ItemCore> OnWeaponBreak; // Added for consistency

    [SerializeField] protected int maxDurability = 100;
    [SerializeField] protected int durabilityLossPerUse = 1;
    [SerializeField] protected int currentDurability;
    private bool isBroken = false;

    public float DurabilityPercentage => (float)currentDurability / maxDurability;

    protected virtual void Awake()
    {
        currentDurability = maxDurability;
    }

    public abstract void Use();

    protected void ReduceDurability()
    {
        if (isBroken) return;
        currentDurability = Mathf.Max(0, currentDurability - durabilityLossPerUse);
        if (currentDurability <= 0) BreakWeapon();
    }

    protected void BreakWeapon()
    {
        isBroken = true;
        OnWeaponBreak?.Invoke(GetComponent<ItemCore>());
        Debug.Log($"{weaponName} broke!");
    }
}