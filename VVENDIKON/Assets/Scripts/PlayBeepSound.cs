using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayBeepSound : MonoBehaviour
{
    public GameObject Player;
    public AudioSource beep;

    private float DistanceToPlayer;
    private float count = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToPlayer = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        count++;

        if (count >= DistanceToPlayer * 2.25 && DistanceToPlayer < 20)
        {
            beep.Play();
            count = 0.0f;
        }
    }
}
