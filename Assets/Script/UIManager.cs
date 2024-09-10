using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Barrack ui handling here.
    //this will be transferred to new script .


    [SerializeField] private GameObject BarrackUiPanel; // Assign your UI Panel in the Inspector
    private BarrackCollider selectedObject;
    private BarrackCollider clickedObject;

    void Start()
    {
        //Remove this-------
        BarrackUiPanel.SetActive(false); // Start with the panel hidden
    }

     private void ShowPanel(BarrackCollider barrack)
    {
        selectedObject = barrack; // Keep track of the selected barrack
        BarrackUiPanel.SetActive(true); // Show the panel
    }

    public void HidePanel()
    {
        selectedObject = null; // Clear the selected object
        BarrackUiPanel.SetActive(false); // Hide the panel
    }

    public void TrainTroops(){       
        if (selectedObject != null)
        {
            selectedObject.StartTraining(); // Trigger the training in the selected barrack
        }     
    }

    void Update(){
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the click was on a moving object
                BarrackCollider clickedObject = hit.collider.GetComponent<BarrackCollider>();

                if (clickedObject != null)
                {
                    clickedObject.ClickedResponse();
                    ShowPanel(clickedObject);
                }
                
            }
        }
    }
}
