using UnityEngine;

public class FlameTorch : Tool
{
    public float burnTime = 10f;
    private float burnTimer = 0f;
    private bool isBurning = false;
    [SerializeField] private ParticleSystem flameEffect;

    protected override void Awake()
    {
        base.Awake();
        range = 5f;
        maxDurability = 100;
        durabilityLossPerUse = 10;
    }

    public override void Use()
    {
        if (currentDurability <= 0)
        {
            BreakTool();
            return;
        }

        if (!isBurning)
        {
            UnityEngine.Debug.Log("FlameTorch is burning!");
            isBurning = true;
            burnTimer = burnTime;
            if (flameEffect != null) flameEffect.Play();
            base.Use(); // Trigger OnToolUsed
        }
    }

    protected override void Update()
    {
        base.Update();
        if (isBurning)
        {
            burnTimer -= Time.deltaTime;
            AffectCreaturesInRange();

            if (burnTimer <= 0)
            {
                isBurning = false;
                if (flameEffect != null) flameEffect.Stop();
                UnityEngine.Debug.Log("FlameTorch has burned out!");
                ReduceDurabilityUse();
            }
        }
    }

    private void AffectCreaturesInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, damageableLayers);
        foreach (Collider hit in hits)
        {
            Creature creature = hit.GetComponent<Creature>();
            if (creature != null)
            {
                creature.ApplyStun(2f);
                UnityEngine.Debug.Log($"{creature.creatureName} was stunned by FlameTorch!");
            }
        }
    }
}