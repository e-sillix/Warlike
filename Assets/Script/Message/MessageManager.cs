using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    [SerializeField] private float messageDisappearingTime=1.5f;    
    [SerializeField] private GameObject NormalMessagePanel;
    [SerializeField]private TextMeshProUGUI NormalMessageText;   
    [SerializeField]private string TroopsTrainingStarted,messageForNotEnoughCredit,
    messageForNotEnoughSpace,messageForMaxBuildingUpgrade,messageForBuildingLimit,
    messageForUpgradeNotAllowed,messageForLaboratoryUpgrade,messageForBuildingLaboratory
    ,trainingStartedMessage,trainingCancelledMessage,SuccessBuildingMessage,BuildingNotInsideMessage;

//Training related messages
    public void MessageForTroopsAdded(string barrackType, int[] troopsData)
    {
        //troopsCountManager will call this function to display the message.
        string message="Troops added to " + barrackType + " are: ";
        for (int i = 0; i < troopsData.Length; i++)
        {
            if(troopsData[i]>0){
                message += "Level " + (i + 1) + ": " + troopsData[i] + " ";
            }
        }
        //this will be called by TroopsCountManager        
        // Debug.Log(message);
        displayNormalMessage(message);
    }
    public void BuildingSuccessfullyBuilt(){
        displayNormalMessage(SuccessBuildingMessage);
    }
    public void BuildingNotInside(){
        displayNormalMessage(BuildingNotInsideMessage);
    }
    public void TrainingStartedMessage(){
        displayNormalMessage(trainingStartedMessage);
    }
    public void TrainingCancelledMessage(){
        displayNormalMessage(trainingCancelledMessage);
    }
    public void MessageForTroopsTraining(){
        displayNormalMessage(TroopsTrainingStarted);
    }
    
   public void TriggerMaxBuildingUpgrade(){
        displayNormalMessage(messageForMaxBuildingUpgrade);
   }
    public void MessageForNotEnoughCredit(){
        displayNormalMessage(messageForNotEnoughCredit);
    }

    public void MessageForNotEnoughSpace(){
       displayNormalMessage(messageForNotEnoughSpace);
    }   

   public void MessageForBuildingLimit(){
    displayNormalMessage(messageForBuildingLimit);       
    }
    public void UpgradeNotAllowed(){
        displayNormalMessage(messageForUpgradeNotAllowed);
    }
    public void BuildLaboratory(){
        displayNormalMessage(messageForBuildingLaboratory);
    }
    public void UpgradeLaboratoryMessage(){
        displayNormalMessage(messageForLaboratoryUpgrade);
    }

    public void displayNormalMessage(string message){
        NormalMessagePanel.SetActive(true);
        NormalMessageText.text=message;
        Invoke("HideNormalMessage", messageDisappearingTime);
    }
        void HideNormalMessage()
        {
            NormalMessagePanel.SetActive(false);
        }
}
