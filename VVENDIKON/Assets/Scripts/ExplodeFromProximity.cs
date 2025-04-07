using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplodeFromProximity : MonoBehaviour
{
    public GameObject Player;
    public ParticleSystem ParticleSystem;
    public AudioSource explosion;

    public float DistanceToBomb = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < DistanceToBomb)
        {
            //Debug.Log("BOOM!");
            explosion.Play();
            ParticleSystem.Play();
            Destroy(gameObject);
        }
    }
}
