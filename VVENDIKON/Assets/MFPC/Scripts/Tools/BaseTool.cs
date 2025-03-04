using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTool : MonoBehaviour
{
    public string toolName;
    public float durability;

    public abstract void Use(); // Each tool must implement its own usage behavior

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
