using System.Diagnostics;
using UnityEngine;

public class FlameTorch : BaseTool
{
    public float burnTime = 10f;
    private float burnTimer = 0f;
    private bool isBurning = false;
    [SerializeField] private ParticleSystem flameEffect; // Optional: Add a particle effect in the Inspector

    private void Start()
    {
        toolName = "Flametorch";
        durability = 100f;
    }

    public override void Use()
    {
        if (!isBurning && durability > 0)
        {
            Debug.Log($"{toolName} is burning!");
            isBurning = true;
            burnTimer = burnTime;
            if (flameEffect != null) flameEffect.Play();
        }
    }

    private void Update()
    {
        if (isBurning)
        {
            burnTimer -= Time.deltaTime;
            durability -= Time.deltaTime * (100f / burnTime); // Deplete durability over burnTime

            if (burnTimer <= 0 || durability <= 0)
            {
                isBurning = false;
                if (flameEffect != null) flameEffect.Stop();
                Debug.Log($"{toolName} has burned out!");
                if (durability <= 0) OnToolBreak?.Invoke(this); // Trigger break event
            }
        }
    }
}