using UnityEngine;

public class GrabberClaw : Tool
{
    private Creature grabbedCreature;

    protected override void Awake()
    {
        base.Awake();
        range = 2f; // Using range as grab radius
        maxDurability = 120;
        durabilityLossPerUse = 15;
    }

    public override void Use()
    {
        if (currentDurability <= 0)
        {
            BreakTool();
            return;
        }

        Debug.Log("Reaching with Grabber Claw!");
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (Collider hit in hits)
        {
            grabbedCreature = hit.GetComponent<Creature>();
            if (grabbedCreature != null)
            {
                grabbedCreature.Grab(transform);
                ReduceDurabilityUse();
                Debug.Log($"Grabbed {grabbedCreature.creatureName}!");
                break;
            }
        }

        base.Use();
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