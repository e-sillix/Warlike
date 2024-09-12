using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this one attached to MarchManager
public class ArmyMarchManager : MonoBehaviour
{
    //this one mananges troops selection on ground and give a position to move to.
    public LayerMask groundLayer; 
    private ArmySelector selectedObject; // Currently selected object

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
                ArmySelector clickedObject = hit.collider.GetComponent<ArmySelector>();

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
                else if (clickedObject == null && selectedObject != null && !IsGroundLayer(hit.collider.gameObject))
                {
                    // Deselect the currently selected army ,if other object is clicked
                    selectedObject.Highlight(false);
                    selectedObject = null; // Clear the selected army
                }

                //there will be attack position or something.
            }
        }
    }

    private bool IsGroundLayer(GameObject obj)
    {
        return (groundLayer.value & (1 << obj.layer)) != 0;
    }
}
