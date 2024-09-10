using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //this script will be replaced with input controller for unity for android.
   [SerializeField] private float moveSpeed;
   void Update()
    {
        // Get input from WASD keys or arrow keys
        float horizontal = Input.GetAxis("Horizontal"); // A and D keys (or left/right arrow keys)
        float vertical = Input.GetAxis("Vertical"); // W and S keys (or up/down arrow keys)

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontal, 0, vertical);

        // Move the camera
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        //Space.World ensures that the movement is in world space, 
        //not relative to the camera's current orientation.
    }
}
