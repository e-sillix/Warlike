using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class TroopsUI : MonoBehaviour
{
    [SerializeField] private GlobalUIManager globalUIManager;
    [SerializeField] private GameObject TroopsStatsPanel;
    [SerializeField] private TextMeshProUGUI TroopsTypeText,TroopsStatsText,TroopsLoad,
    TroopsResourceTypeLoad;

    private string troopsType;
    private int[] troopsData,resourceTypeLoad;
    private int usedCapacity,totalCapacity;
    
    
    public void TroopsClicked(TheUnit clickedUnit){
        //this will be called by global ui.

        //getting that unit data 
        troopsType=clickedUnit.troopsType;
        troopsData=clickedUnit.troopsStats;
        usedCapacity=clickedUnit.usedCapacity;
        totalCapacity=clickedUnit.totalResourceCapacity;
        resourceTypeLoad=clickedUnit.ReturnResourceTypeLoad();
        DisplayData();

    }

    void DisplayData(){
        TroopsTypeText.text=troopsType;
        TroopsStatsText.text="lvl1: "+troopsData[0]+"lvl2: "+troopsData[1]+"lvl3: "+troopsData[2]
        +"lvl4: "+troopsData[3]+"lvl5: "+troopsData[4];
        TroopsLoad.text="Load :"+usedCapacity+"/"+totalCapacity;
        TroopsResourceTypeLoad.text="W:"+resourceTypeLoad[0]+"G:"+resourceTypeLoad[1]
        +"S:"+resourceTypeLoad[2];

        TroopsStatsPanel.SetActive(true);
    }
    
    public void CancelClicked(){
        //when cancel is clicked in troops ui panel,directly attached to it.
        TroopsStatsPanel.SetActive(false);
        globalUIManager.RefreshPermission();
    }
}
