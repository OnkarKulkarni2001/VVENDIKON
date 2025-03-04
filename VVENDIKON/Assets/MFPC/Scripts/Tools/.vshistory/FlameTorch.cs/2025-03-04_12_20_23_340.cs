using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FlameTorch : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
