using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class TouchMarching : MonoBehaviour
{
    private bool isHolding = false;
    private TheUnit selectedUnit;
    [SerializeField] private CameraSystem cameraSystem;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GlobalUIManager globalUIManager;
    [SerializeField] private TroopsUI troopsUI;
    private TroopsExpeditionManager troopsExpeditionManager;

    private TroopsInstanceUI LastClickedTroopsInstanceUI;

    private TheCreep LastCreep;

 
        void Start()
{
    troopsExpeditionManager = GetComponent<TroopsExpeditionManager>();

    if (lineRenderer == null)
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    lineRenderer.positionCount = 2;
    lineRenderer.startWidth = 0.1f;
    lineRenderer.endWidth = 0.1f;
    lineRenderer.enabled = false; // Hide initially

    // Auto-assign material (Optional: Replace with your actual material)
    // lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
    // lineRenderer.material.color = Color.red;


    }

    void Update()
{
    if(Input.touchCount>0&&!EventSystem.current.IsPointerOverGameObject( Input.GetTouch(0).fingerId)){
        if(LastClickedTroopsInstanceUI){
            LastClickedTroopsInstanceUI.RefreshUIB();
        }
        // return;
    }
    if (Input.touchCount == 1&&!EventSystem.current.
    IsPointerOverGameObject( Input.GetTouch(0).fingerId))
    {
        Touch touch = Input.GetTouch(0);
        Ray ray = Camera.main.ScreenPointToRay(touch.position);

        if (touch.phase == TouchPhase.Began) // Touch started
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                TheUnit unit = hit.collider.GetComponentInParent<TheUnit>();
                if (unit != null)
                {
                    if(!unit.returnIsDefeated()){
                    isHolding = true;
                    selectedUnit = unit;
                    selectedUnit.GetComponent<TroopsInstanceUI>().TriggerSelectedRings(true);
                    cameraSystem.SetTheUniHold(true);

                    // Enable UI Line & set start point
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, selectedUnit.transform.position);
                    }
                }
            }
        }

        if (isHolding) // While dragging
        {
            // ✅ Update start position to follow the unit
            lineRenderer.SetPosition(0, selectedUnit.transform.position);

            Ray dragRay = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(dragRay, out RaycastHit dragHit))
            {
                lineRenderer.SetPosition(1, dragHit.point); // Update end position
            }
            else
            {
                lineRenderer.SetPosition(1, dragRay.origin + dragRay.direction * 10f); // Extend in direction
            }

            if(touch.phase==TouchPhase.Moved){
                 Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                 if(LastCreep){
                    LastCreep.GetComponent<CreepUI>().SetCreepRing(false);
                 }
                if (Physics.Raycast(ray1, out RaycastHit hit, Mathf.Infinity))
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    if(clickedObject.GetComponentInParent<TheCreep>()){
                        LastCreep=clickedObject.GetComponentInParent<TheCreep>();
                        clickedObject.GetComponentInParent<CreepUI>().SetCreepRing(true);
                    }
                }
            }
        }

        if (touch.phase == TouchPhase.Ended) // Touch released
        {
            if(LastCreep){
                LastCreep.GetComponent<CreepUI>().SetCreepRing(false);
                LastCreep=null;
            }
            if (isHolding)
            {
                cameraSystem.SetTheUniHold(false);
                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray1, out RaycastHit hit, Mathf.Infinity))
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    if(clickedObject.GetComponentInParent<TheUnit>()){
                        TheUnit ClickedUnit=clickedObject.GetComponentInParent<TheUnit>();
                         if (ClickedUnit.gameObject != selectedUnit.gameObject)
                        {
                        Debug.Log("Marching to: " + clickedObject.name);
                        troopsExpeditionManager.MarchUsingDrag(selectedUnit, clickedObject, hit);
                        selectedUnit.GetComponent<TroopsInstanceUI>().TriggerSelectedRings(false);
                    }
                    else{
                         TroopsInstanceUI troopsInstanceUI=selectedUnit.
                         GetComponentInParent<TroopsInstanceUI>();
            //nothing should happen visibly
                    troopsInstanceUI.GetTroopsUIComponent(troopsUI,globalUIManager);
            troopsInstanceUI.TriggerUIButtons();
            cameraSystem.FollowTheTarget(selectedUnit.gameObject);
            LastClickedTroopsInstanceUI=troopsInstanceUI;
                    }
                    }
                    else
                    {
                        Debug.Log("Marching to: " + clickedObject.name);
                        troopsExpeditionManager.MarchUsingDrag(selectedUnit, clickedObject, hit);
                        selectedUnit.GetComponent<TroopsInstanceUI>().TriggerSelectedRings(false);
                    }
                   
                }
            }

            // Reset values
            if (isHolding)
            {
                isHolding = false;
                selectedUnit = null;
                lineRenderer.enabled = false; // Hide UI Line
                // LastCreep=null;
            }
        }
    }
}

}
