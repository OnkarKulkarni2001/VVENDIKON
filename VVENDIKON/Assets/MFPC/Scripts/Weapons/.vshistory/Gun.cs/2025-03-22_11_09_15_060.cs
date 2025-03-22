using System.Diagnostics;
using UnityEngine;

public class Gun : BaseWeapon
{
    public float fireRate = 0.5f; // Shots per second
    private float nextFireTime = 0f;

    private void Start()
    {
        weaponName = "Gun";
        damage = 10f;
        range = 50f;
        durability = 50f; // Shots before breaking
    }

    public override void Use()
    {
        if (Time.time >= nextFireTime && durability > 0)
        {
            Debug.Log($"{weaponName} fired!");
            nextFireTime = Time.time + fireRate;

            // Shooting mechanic
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
            {
                Debug.Log($"Hit {hit.collider.name} for {damage} damage!");
                // Apply damage to target here (e.g., Creature or health system)
            }

            durability -= 1f; // Decrease durability per shot
            if (durability <= 0)
            {
                Debug.Log($"{weaponName} has broken!");
                BreakWeapon();
            }
        }
    }
}