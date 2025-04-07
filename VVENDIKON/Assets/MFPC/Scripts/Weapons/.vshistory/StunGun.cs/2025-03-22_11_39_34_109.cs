using System.Diagnostics;
using UnityEngine;

public class StunGun : BaseWeapon
{
    public float fireRate = 0.5f;
    public float stunDuration = 3f;
    private float nextFireTime = 0f;

    protected override void Awake()
    {
        base.Awake();
        weaponName = "Stun Gun";
        damage = 5f; // Minor damage
        range = 10f;
        maxDurability = 50;
        durabilityLossPerUse = 5;
    }

    public override void Use()
    {
        if (Time.time < nextFireTime || currentDurability <= 0) return;

        Debug.Log($"{weaponName} fired!");
        nextFireTime = Time.time + fireRate;

        Creature creature = GetHitCreature();
        if (creature != null)
        {
            creature.ApplyStun(stunDuration);
            ReduceDurability();
            Debug.Log($"Stunned {creature.creatureName}!");
        }
        else
        {
            Debug.Log("Missed!");
        }
    }
}