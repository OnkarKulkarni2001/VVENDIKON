using System.Diagnostics;
using UnityEngine;

public class Net : BaseTool
{
    public float range = 5f; // Distance the net can reach
    public float durabilityLossPerUse = 10f;

    private void Start()
    {
        toolName = "Net";
        durability = 100f;
    }

    public override void Use()
    {
        if (durability <= 0)
        {
            Debug.Log($"{toolName} is too worn out to use!");
            BreakTool();
            return;
        }

        Debug.Log($"{toolName} is being swung!");
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            Creature creature = hit.collider.GetComponent<Creature>();
            if (creature != null)
            {
                Transform holder = transform; // The net itself holds the creature
                creature.Grab(holder);
                durability -= durabilityLossPerUse;
                Debug.Log($"Caught {creature.creatureName} with {toolName}!");
                if (durability <= 0) BreakTool();
            }
            else
            {
                Debug.Log("No creature caught!");
            }
        }
        else
        {
            Debug.Log("Missed with the net!");
        }
    }
}