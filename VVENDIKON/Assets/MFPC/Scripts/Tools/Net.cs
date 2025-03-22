using UnityEngine;

public class Net : Tool
{
    private Creature grabbedCreature;

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

        Debug.Log("Swinging Net!");
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            grabbedCreature = hit.collider.GetComponent<Creature>();
            if (grabbedCreature != null)
            {
                grabbedCreature.Grab(transform);
                ReduceDurabilityUse();
                Debug.Log($"Caught {grabbedCreature.creatureName}!");
            }
        }

        base.Use(); // Trigger OnToolUsed
    }

    public override void ToolThrown()
    {
        if (grabbedCreature != null)
        {
            grabbedCreature.Release();
            grabbedCreature = null;
        }
        base.ToolThrown();
    }
}