using System.Diagnostics;
using UnityEngine;

public class FlameTorch : BaseWeapon
{
    public float burnTime = 10f; // Duration of flame per use
    public float stunDuration = 2f; // How long creatures are stunned
    private float burnTimer = 0f;
    private bool isBurning = false;
    [SerializeField] private ParticleSystem flameEffect; // Optional flame effect

    protected override void Awake()
    {
        base.Awake();
        weaponName = "Flame Torch";
        damage = 5f; // Minor damage per tick
        range = 5f; // Short range for flame
        maxDurability = 100;
        durabilityLossPerUse = 10; // Loss per full burn cycle
    }

    public override void Use()
    {
        if (currentDurability <= 0)
        {
            BreakWeapon();
            return;
        }

        if (!isBurning)
        {
            Debug.Log($"{weaponName} is burning!");
            isBurning = true;
            burnTimer = burnTime;
            if (flameEffect != null) flameEffect.Play();
        }
    }

    protected virtual void Update()
    {
        if (isBurning)
        {
            burnTimer -= Time.deltaTime;
            AffectCreaturesInRange();

            if (burnTimer <= 0)
            {
                isBurning = false;
                if (flameEffect != null) flameEffect.Stop();
                Debug.Log($"{weaponName} has burned out!");
                ReduceDurability();
            }
        }
    }

    private void AffectCreaturesInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (Collider hit in hits)
        {
            Creature creature = hit.GetComponent<Creature>();
            if (creature != null)
            {
                creature.ApplyStun(stunDuration);
                Debug.Log($"{creature.creatureName} was stunned by {weaponName}!");
            }
        }
    }
}