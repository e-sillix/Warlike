using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    public LayerMask groundLayer; 
    private SoldierSelector selectedObject; // Currently selected object

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the click was on a moving object
                SoldierSelector clickedObject = hit.collider.GetComponent<SoldierSelector>();

                if (clickedObject != null)
                {
                    // Deselect the currently selected object, if any
                    if (selectedObject != null)
                    {
                        selectedObject.Highlight(false); // Remove highlight from the previously selected object
                    }

                    // Select the new clicked object
                    selectedObject = clickedObject;
                    selectedObject.Highlight(true); // Highlight the newly selected object
                }
                // Otherwise, if the click was on the ground and an object is selected
                else if (selectedObject != null && IsGroundLayer(hit.collider.gameObject))
                {
                    // Set the target position for the selected object
                    selectedObject.NotifyParentPosition(hit.point);
                }
            }
        }
    }

    private bool IsGroundLayer(GameObject obj)
    {
        return (groundLayer.value & (1 << obj.layer)) != 0;
    }
}
