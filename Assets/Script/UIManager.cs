using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Barrack ui handling here.
    //this will be transferred to new script .


    [SerializeField] private GameObject BarrackTrainingUiPanel; // Assign your UI Panel in the Inspector
    [SerializeField] private GameObject BarrackCancelUiPanel; // Assign your UI Panel in the Inspector
    // private BarrackCollider selectedObject;
    private BarrackCollider clickedObject;
    [SerializeField] private TroopsTraining TrainingManager;

    void Start()
    {
        //Remove this-------
        BarrackTrainingUiPanel.SetActive(false); // Start with the panel hidden
        TrainingManager=TrainingManager.GetComponent<TroopsTraining>();
    }

     private void TrainingPanel()
    {
        // selectedObject = barrack; // Keep track of the selected barrack
        BarrackTrainingUiPanel.SetActive(true); // Show the panel
    }
    private void CancelPanel(){

        BarrackCancelUiPanel.SetActive(true); // Show the panel
    }
    
    void Update(){
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the click was on a Barrack
                clickedObject = hit.collider.GetComponent<BarrackCollider>();

                if (clickedObject != null)
                {
                    clickedObject.ClickedResponse();
                    if(TrainingManager.isTrainingInProgress){
                        CancelPanel();
                    }
                    else{
                        TrainingPanel();
                    }
                }
                
            }
        }
    }
}
