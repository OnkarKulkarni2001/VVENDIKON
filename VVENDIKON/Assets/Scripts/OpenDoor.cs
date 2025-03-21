using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    public GameObject Player;

    public bool button = false;
    public bool useButton = false;

    private Vector3 original_position;
    public float distance_to_door = 3.0f;
    public float door_move_speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        original_position = Door.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (useButton) // Door opens with 'button' pushed
        {
            if (button) // Open door
            {
                if (transform.position.y < original_position.y + 2.25) // Moving up
                {
                    transform.position += Vector3.up * door_move_speed;
                }
                else // Door at top position
                {
                    transform.position = original_position + new Vector3(0, 2.25f, 0);
                }
            }
            else // Close door
            {
                if (transform.position.y >= original_position.y)
                {
                    transform.position -= Vector3.up * door_move_speed;
                }
            }
        }
        else // Door opens by proximity
        {
            if ( Mathf.Abs(Vector3.Distance (original_position, Player.transform.position)) < distance_to_door) // Close to door, door moves up
            {     
                if (transform.position.y < original_position.y + 2.25) // Moving up
                {
                    transform.position += Vector3.up * door_move_speed;
                }
                else // Door at top position
                {
                    transform.position = original_position + new Vector3(0, 2.25f, 0);
                }
            }
            else // Far from door, door moves down
            {
                if (transform.position.y >= original_position.y)
                {
                    transform.position -= Vector3.up * door_move_speed;
                }
            }
        }
        
    }
}
