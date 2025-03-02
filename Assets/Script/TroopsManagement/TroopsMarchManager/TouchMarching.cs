using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMarching : MonoBehaviour
{


    private bool isHolding = false;
    private float holdTime = 0f;
    private TheUnit selectedUnit;
    private bool holdDone,holdingCompleted;
    [SerializeField] private CameraSystem cameraSystem;
    
    [SerializeField] private float holdThreshold = 0.7f; // Time required to register a "hold"

    private TheUnit selecetedUnit;
    private TroopsExpeditionManager troopsExpeditionManager;

    void Start()
    {
        troopsExpeditionManager=GetComponent<TroopsExpeditionManager>();
    }
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            
            if (touch.phase == TouchPhase.Began) // Touch started
            {
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    TheUnit unit = hit.collider.GetComponentInParent<TheUnit>(); // Check for TheUnit component
                    if (unit != null)
                    {
                        isHolding = true;
                        selectedUnit = unit;
                        holdTime = 0f; // Reset timer
                        Debug.Log("Touch Started: " + selectedUnit.gameObject.name);
                    }
                }
            }

            if (isHolding && touch.phase == TouchPhase.Stationary) // Finger is holding still
            {
                holdTime += Time.deltaTime;

                if (holdTime >= holdThreshold)
                {
                    Debug.Log("Holding done " );
                    holdDone=true;
                    cameraSystem.SetTheUniHold(true);
                    // Perform any action while holding
                }
            }

            if (touch.phase == TouchPhase.Ended) // Touch released
            {
                if (holdDone)
                {
                    Debug.Log("Released " );

                    // Perform action on release
                    cameraSystem.SetTheUniHold(false);
                    Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray1, out RaycastHit hit, Mathf.Infinity))
                    {        
                    // Prevent interaction if clicking on UI
                    GameObject clickedObject=hit.collider.gameObject;
                    troopsExpeditionManager.MarchUsingDrag(selectedUnit,clickedObject,hit);
                    }
                    isHolding = false;
                    holdDone=false;
                    selectedUnit = null;
                }
            }
        }
    }
    public bool ReturnIsUnitHolding(){
        return isHolding;
    }
}
