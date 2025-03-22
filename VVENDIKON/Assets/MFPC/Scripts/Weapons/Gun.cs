using UnityEngine;

public class Gun : BaseWeapon
{
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    protected override void Awake()
    {
        base.Awake();
        weaponName = "Gun";
        damage = 10f;
        range = 50f;
        maxDurability = 50;
    }

    public override void Use()
    {
        if (Time.time < nextFireTime || currentDurability <= 0) return;

        Debug.Log($"{weaponName} fired!");
        nextFireTime = Time.time + fireRate;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            Debug.Log($"Hit {hit.collider.name} for {damage} damage!");
        }

        ReduceDurability();
    }
}