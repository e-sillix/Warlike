using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBluePrint : MonoBehaviour
{
    private bool movingAllowed;

   private void Update()
    {
        UpdateBlueprintPosition();
    }
    void UpdateBlueprintPosition()
    {
        
        if (Input.touchCount == 1)
        {
        Touch touch = Input.GetTouch(0);
        if(touch.phase==TouchPhase.Began){
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
                int layerMask = LayerMask.GetMask("Blue", "Ground"); // Allow only "Blue" and "Ground"

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                    {
                    BluePrint detectedBlueprint = hit.collider.GetComponentInParent<BluePrint>();
                    if (detectedBlueprint) {
                    //    return;
                    movingAllowed=true;
                    }
                    else{
                        movingAllowed=false;
                    }
                    
                }
        }
        if (movingAllowed && touch.phase == TouchPhase.Moved)
        {

    Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
    Ray prevTouchRay = Camera.main.ScreenPointToRay(touch.position - touch.deltaPosition);

    Plane groundPlane = new Plane(Vector3.up, transform.position); // Use current Y instead of y=0

    if (groundPlane.Raycast(prevTouchRay, out float enterPrev) && groundPlane.Raycast(touchRay, out float enterCurr))
    {
        Vector3 prevPoint = prevTouchRay.GetPoint(enterPrev);
        Vector3 currPoint = touchRay.GetPoint(enterCurr);

        Vector3 moveDir = prevPoint - currPoint;
        
        // Preserve Y position by setting it to current Y
        transform.position -= new Vector3(moveDir.x, 0, moveDir.z);
    
}

        }
        if(touch.phase==TouchPhase.Ended){
            movingAllowed=false;
        }
    }
    }
}
