using System.Diagnostics;
using UnityEngine;

public class GrabberClaw : BaseTool
{
    public float grabRadius = 2f; // Spherical range around the tool
    public float durabilityLossPerUse = 15f;

    private void Start()
    {
        toolName = "Grabber Claw";
        durability = 120f;
    }

    public override void Use()
    {
        if (durability <= 0)
        {
            Debug.Log($"{toolName} is broken!");
            BreakTool();
            return;
        }

        Debug.Log($"{toolName} is reaching out!");
        Collider[] hits = Physics.OverlapSphere(transform.position, grabRadius);
        foreach (Collider hit in hits)
        {
            Creature creature = hit.GetComponent<Creature>();
            if (creature != null)
            {
                Transform holder = transform; // Claw holds the creature
                creature.Grab(holder);
                durability -= durabilityLossPerUse;
                Debug.Log($"Grabbed {creature.creatureName} with {toolName}!");
                if (durability <= 0) BreakTool();
                return; // Grab only one creature per use
            }
        }
        Debug.Log("No creatures in range!");
    }

    // Optional: Visualize the grab radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
    }
}