using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Gun : BaseWeapon
{
    public float fireRate = 0.5f; // Additional property for this weapon

    private void Start()
    {
        weaponName = "Gun";
        damage = 10f;
        range = 50f;
    }

    public override void Use()
    {
        Debug.Log(weaponName + " fired!");
        // Add shooting mechanics here
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
