using System.Diagnostics;
using UnityEngine;

public class TranquilizerDart : BaseWeapon
{
    public float fireRate = 1f;
    public float weaknessDuration = 5f;
    public float resistanceReduction = 0.8f; // Reduces resistance by 80%
    private float nextFireTime = 0f;

    protected override void Awake()
    {
        base.Awake();
        weaponName = "Tranquilizer Dart";
        damage = 2f; // Minimal damage
        range = 20f;
        maxDurability = 30;
        durabilityLossPerUse = 10;
    }

    public override void Use()
    {
        if (Time.time < nextFireTime || currentDurability <= 0) return;

        Debug.Log($"{weaponName} fired!");
        nextFireTime = Time.time + fireRate;

        Creature creature = GetHitCreature();
        if (creature != null)
        {
            creature.ApplyWeakness(resistanceReduction, weaknessDuration);
            ReduceDurability();
            Debug.Log($"Weakened {creature.creatureName}!");
        }
        else
        {
            Debug.Log("Missed!");
        }
    }
}