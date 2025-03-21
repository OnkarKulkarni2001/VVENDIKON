using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject Player;
    public Light light;

    private bool light_increase = true;

    // Start is called before the first frame update
    void Start()
    {
        light.intensity = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (light.intensity > 1.75)
        {
            light_increase = false;
        }
        else if (light.intensity <= 0.01)
        {
            light_increase = true;
        }

        if (light_increase)
        {
            light.intensity += 0.015f;
        }
        else
        {
            light.intensity -= 0.015f;
        }
    }
}
