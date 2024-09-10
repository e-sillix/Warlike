using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheArmy : MonoBehaviour
{
    public LayerMask groundLayer; // Assign this to your ground layer in the Inspector
    public float moveSpeed = 5f; // Speed of movement
    
    private Vector3 targetPosition;
    private bool shouldMove = false;

   

    void Update()
    {
        if (shouldMove)
        {
            // Move the object towards the target position smoothly
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the object has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                shouldMove = false; // Stop moving
            }
        }
    }

    // Method to set the target position and start moving
    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        shouldMove = true;
    }

}
