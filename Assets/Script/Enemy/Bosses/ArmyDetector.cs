using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyDetector : MonoBehaviour
{
    public float detectionRange = 200f;  // 200 units range
    [SerializeField] private LayerMask unitLayer;  // Optional: specify a layer for units to make detection faster

    public float detectionInterval = 5f; // Time interval between each detection (5 seconds)
    
    public GameObject[] FindNearbyUnits()
    {
        GameObject[] playerArmies=new GameObject[5];
        // Find all colliders within a 200 unit radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, unitLayer);
        int  i=0;
        foreach (Collider collider in colliders)
        {
            // Check if the object has TheUnit script attached
            Attacking unit = collider.GetComponentInParent<Attacking>();
            if (unit != null)
            {
                // Unit found within the detection range
                // You can do something with the unit here, like targeting or attacking
                playerArmies[i]=unit.gameObject;
                i++;
            }
        }
        return playerArmies;
    }
   public bool FindCurrentTarget(GameObject target)
{
    // Find all colliders within the detection range
    Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, unitLayer);

    foreach (Collider collider in colliders)
    {
        // Check if the object has TheUnit script attached
        Attacking unit = collider.GetComponentInParent<Attacking>();
        if (unit == target.GetComponent<Attacking>())
        {
            // Unit found within the detection range
            return true;
        }
    }
    
    // If we reach here, the target was not found
    return false;
}

}
