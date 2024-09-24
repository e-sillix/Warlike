using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    //handle all the UI interaction for resoruce spawning   and function messages triggered by RSpM
    [SerializeField] private GameObject ConfirmationUIGameobject;
    [SerializeField] private GameObject ConstructionUIGameobject;
    [SerializeField] private GameObject CheckingUpUIGameobject;
    [SerializeField] private GameObject BottomLeftUIGameobject;
    [SerializeField] private GameObject NotEnoughCreditsGameobject;
    [SerializeField] private GameObject NoSpaceGameobject;
    [SerializeField] private float messageDisappearingTime=0.5f;
   
    public void DisappearConfirmScreen(){
        //triggered only by RSpM or something logical,when spawning is done with no problem
        ConfirmationUIGameobject.SetActive(false);
        ConstructionUIGameobject.SetActive(false);
        CheckingUpUIGameobject.SetActive(true);
        BottomLeftUIGameobject.SetActive(true);        
    }
    public void RefreshUIConstruction(){
        //trigger to refresh ui.this will be triggered when choosing cancellation of construction,except 
        //for colliding situation
        ConfirmationUIGameobject.SetActive(false);
        ConstructionUIGameobject.SetActive(false);
        CheckingUpUIGameobject.SetActive(true);
        BottomLeftUIGameobject.SetActive(true); 

    }
    public void MessageForNotEnoughCredit(){
        NotEnoughCreditsGameobject.SetActive(true);
        RefreshUIConstruction();
        //create a function to turn it false after 1 sec.
        Invoke("HideNotEnoughCreditsMessage", messageDisappearingTime);
    }

// This method will deactivate the GameObject
    void HideNotEnoughCreditsMessage()
        {
            //this is getting invoked by messageFornotenoughcredit.
            NotEnoughCreditsGameobject.SetActive(false);
        }   
    public void MessageForNotEnoughSpace(){
        NoSpaceGameobject.SetActive(true);
        Invoke("HideNotEnoughSpaceMessage", messageDisappearingTime);
    }
    void HideNotEnoughSpaceMessage()
        {
            NoSpaceGameobject.SetActive(false);
        } 
}
