using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FlameTorch : BaseTool
{
    public float burnTime = 10f;

    private void Start()
    {
        toolName = "Flametorch";
        durability = 100f;
    }

    public override void Use()
    {
        Debug.Log(toolName + " is burning!");
        // Add flame effect or logic here
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
