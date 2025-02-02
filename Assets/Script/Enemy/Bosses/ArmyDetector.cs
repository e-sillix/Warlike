using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyDetector : MonoBehaviour
{
    public float detectionRange = 200f;  // 200 units range
    [SerializeField] private LayerMask unitLayer;  // Optional: specify a layer for units to make detection faster

    public float detectionInterval = 5f; // Time interval between each detection (5 seconds)
    
    // private TheUnit[] playerArmies=new TheUnit[5]; // Array to store the detected units
    // void Start()
    // {
    //     // Start the Coroutine when the game begins
    //     StartCoroutine(DetectUnitsEveryInterval());
    // }

    // Coroutine to run the detection function every 'detectionInterval' seconds
    // IEnumerator DetectUnitsEveryInterval()
    // {
    //     while (true) // Keep running the detection indefinitely
    //     {
    //         FindNearbyUnits(); // Find the nearby units
    //         if(playerArmies[0]!=null){
    //             Debug.Log("Player unit found.");
    //             // Call the PlayerUnitFound method in the Boss script
    //             // This method will be called when a player unit is found
    //             // Pass the detected units as an argument
    //             GetComponent<Boss>().PlayerUnitFound(playerArmies);
    //         }
    //         yield return new WaitForSeconds(detectionInterval); // Wait for the specified interval
    //     }
    // }

    public TheUnit[] FindNearbyUnits()
    {
        TheUnit[] playerArmies=new TheUnit[5];
        // Find all colliders within a 200 unit radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, unitLayer);
        int  i=0;
        foreach (Collider collider in colliders)
        {
            // Check if the object has TheUnit script attached
            TheUnit unit = collider.GetComponentInParent<TheUnit>();
            if (unit != null)
            {
                // Unit found within the detection range
                // You can do something with the unit here, like targeting or attacking
                playerArmies[i]=unit;
                i++;
            }
        }
        // Debug.Log("Found "+i+" units:");
        // for(int j=0;j<i;j++){
        //     Debug.Log(playerArmies[j].gameObject.name);
        // }
        return playerArmies;
    }
}
