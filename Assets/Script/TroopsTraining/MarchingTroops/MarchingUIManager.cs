using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingUIManager : MonoBehaviour
{//responsible for ui and accepting position and passing it to tmm and selecting army
    // Update is called once per frame
    public LayerMask groundLayer; 
    [SerializeField] UnitSelectionManager selectionManager;
    private UnitSelector selectedObject;
    
    [SerializeField] private TroopsMarchManager marchManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))//this will move
            {                
                //this will pass the to check if it is a army
                //then selected
                selectedObject=selectionManager.selectedObject;
                if(CheckArmySelected(hit)){//return true it clicked on army
                    if(selectedObject!=null){//null if previous
                    selectionManager.DeselectTheUnit();//deselecting previous unit
                    }
                    selectionManager.selectTheUnit(CheckArmySelected(hit));// pass the clicked unit prefab
                }
                 else if (selectedObject != null && IsGroundLayer(hit.collider.gameObject))
                {                    
                    marchManager.InitiateTheMarchProcess(selectedObject,hit.point);
                }
                else if (!CheckArmySelected(hit) && selectedObject != null &&
                !IsGroundLayer(hit.collider.gameObject))
                {
                    // Deselect the currently selected army ,if other object is clicked
                    selectedObject.Highlight(false);
                    selectionManager.DeselectTheUnit();//deselecting previous unit
                }
            }
        }
    }
    private bool IsGroundLayer(GameObject obj)
    {
        return (groundLayer.value & (1 << obj.layer)) != 0;
    }
    public UnitSelector CheckArmySelected(RaycastHit hit){      
        UnitSelector clickedObject = hit.collider.GetComponent<UnitSelector>();  
        return clickedObject;
    }
    public void DeselectTheTroops(){
        
    }
}
