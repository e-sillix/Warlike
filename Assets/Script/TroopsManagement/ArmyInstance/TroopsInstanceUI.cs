using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsInstanceUI : MonoBehaviour
{
   private TroopsUI troopsUIManager;
   private GlobalUIManager globalUIManager;
   [SerializeField] private GameObject UIButtonPanel;
    public void GetTroopsUIComponent(TroopsUI troopsUI,GlobalUIManager GlobalUIManager){
        //by global ui manager 
        globalUIManager=GlobalUIManager;
        troopsUIManager=troopsUI;
    }

    public void TriggerUIButtons(){
        //by Global ui manager
        UIButtonPanel.SetActive(true);
    }
    public void RefreshUIB(){
        //called by global or ui buttons
        //for refreshing
        UIButtonPanel.SetActive(false);
        globalUIManager.RefreshPermission();
    }
    
    public void InfoUIButtonClicked(){
        //called by button directly
        troopsUIManager.ShowTroopsInfo(gameObject.GetComponent<TheUnit>());
    }

}
