using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    [SerializeField] private float messageDisappearingTime=0.5f;
    [SerializeField] private GameObject NotEnoughCreditsGameobject,NoSpaceGameobject,MaxUpgradeGameobject;
   public void TriggerMaxBuildingUpgrade(){
        MaxUpgradeGameobject.SetActive(true);
        //create a function to turn it false after 1 sec.
        Invoke("HideMaxUpgradeMessage", messageDisappearingTime);
   }
   void HideMaxUpgradeMessage()
        {
            //this is getting invoked by messageFornotenoughcredit.
            MaxUpgradeGameobject.SetActive(false);
        }   


    public void MessageForNotEnoughCredit(){
        NotEnoughCreditsGameobject.SetActive(true);
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
        //this will be called by RSM
        NoSpaceGameobject.SetActive(true);
        Invoke("HideNotEnoughSpaceMessage", messageDisappearingTime);
    }
    void HideNotEnoughSpaceMessage()
        {
            NoSpaceGameobject.SetActive(false);
        } 
}
