using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingInstance : MonoBehaviour
{
    //this will be attached to every building ,interact with click.
    [SerializeField] private GameObject UIButtonPanel;
    [SerializeField] private BuildingInstanceUI buildingInstanceUI;
    private bool BuildingIsBeingUpgraded;
    private Coroutine UpgradeCoroutine;
    private BuildingPersistenceManager buildingPersistenceManager;
    private BuildingDependencyManager buildingDependencyManager;
    private BuildingStatsManager buildingStatsManager;
    private UpgradeStats upgradeStats;
    private TimeElapsedManagement timeElapsedManagement;
    // private string name;
    // private int level;

    public TimeElapsedManagement ReturnTimeElapsedManagement(){
        return timeElapsedManagement;
    }
    public void ProvideBasicDependency(BuildingDependencyManager BuildingDependencyManager){
        //called by when it is spawned after closing the game.
        buildingDependencyManager=BuildingDependencyManager;

        //buildingdepen. for all the dependencies.

        buildingDependencyManager.ProvideAllConditionalDependencies(gameObject);
    }
    
    public void GetallBuildingDependencies(BuildingPersistenceManager BuildingPersistenceManager,
    BuildingInstanceUI BuildingInstanceUI,
    UpgradeStats UpgradeStats,TimeElapsedManagement TimeElapsedManagement){
        //this will be countercall by provideBasicDependency.
        buildingPersistenceManager=BuildingPersistenceManager;
        buildingInstanceUI=BuildingInstanceUI;
        // buildingStatsManager=BuildingStatsManager;
        upgradeStats=UpgradeStats;
        timeElapsedManagement=TimeElapsedManagement;
        // upgradeStats.SetData(gameObject);

    }
    // public void OnApplicationQuit()
    // {
    //     buildingPersistenceManager.SaveBuildingData(gameObject);        
    // }

    public void SetData(){
        //by barrack ,farm when they are established.
        upgradeStats.SetData(gameObject);
    }
    public void DependencyInjection(BuildingPersistenceManager BuildingPersistenceManager){
        //this will be called when it is replaced by construction instance.
        buildingPersistenceManager=BuildingPersistenceManager;
        
        buildingPersistenceManager.SaveBuildingData(gameObject);
        }
    // private BuildingInstanceUI buildingInstanceUI;  
    public void assigningManager(BuildingInstanceUI BuildingInstanceUI){
        //this will be called by BuildingManager when prefab is created.
        buildingInstanceUI=BuildingInstanceUI;
    }
    public void Dependency(BuildingInstanceUI BuildingInstanceUI,BuildingPersistenceManager
     BuildingPersistenceManager){
        //this will be called by BuildingManager when prefab is created.
        buildingInstanceUI=BuildingInstanceUI;
        buildingPersistenceManager=BuildingPersistenceManager;
    }

    public bool ReturnBuildingStatus(){
        //by BuildingInstanceUI
        return BuildingIsBeingUpgraded;
    }
    // public void SetBuildingStatus(bool status){
    //     BuildingIsBeingUpgraded=status;
    // }

    public void UpgradeIsOrdered(int[] UpgradeData,int time){//order:
    // level + 1, upgradeCost.capacity, upgradeCost.rate;

        BuildingIsBeingUpgraded=true;
        UpgradeCoroutine=StartCoroutine(UpgradeProcess( UpgradeData,time));
        }

// Coroutine to wait and then apply the upgrade
    private IEnumerator UpgradeProcess( int[] UpgradeData,int time)
    {
    // Wait for the specified upgrade time
    yield return new WaitForSeconds(time);

    if(GetComponent<Farm>()){
            // Farm farm=GetComponent<Farm>();                     
            GetComponent<Farm>().UpgradeStats(UpgradeData[0],UpgradeData[1],UpgradeData[2]);
        }
         else if(GetComponent<TheBarrack>()){
            // TheBarrack barrack=GetComponent<TheBarrack>();
            GetComponent<TheBarrack>().UpgradeStats(UpgradeData[0],UpgradeData[1],UpgradeData[2]);
            // upgradeData=new int[] { level + 1, upgradeCost.capacity, upgradeCost.rate };
        }
        else if(GetComponent<Base>()){
            // Base TheBase=Target.GetComponent<Base>();
            GetComponent<Base>().UpgradeStats(UpgradeData[0]);
        }
        else if(GetComponent<Laboratory>()){
            // Laboratory laboratory=Target.GetComponent<Laboratory>();
            GetComponent<Laboratory>().UpgradeStats(UpgradeData[0],UpgradeData[1]);
        }
        else{
            Debug.LogError("BuildingInstance error");
        }

    // Apply upgrade logic here (level, capacity, rate)
    // ApplyUpgrade(upgradeData);
    Debug.Log("Building Upgraded");
    buildingPersistenceManager.SaveBuildingData(gameObject);
    // Upgrade complete
    BuildingIsBeingUpgraded = false;
    }

    public void CancelUpgrade(){
        if(UpgradeCoroutine!=null){
            StopCoroutine(UpgradeCoroutine);
            BuildingIsBeingUpgraded = false;
            Debug.Log("Cancelation done.");
        }
    }
    public void BuildingClicked(){
        //this will be called by global ui ,after finding this script in parent of the collider.        
        //trigger ui button 
        UIButtonPanel.SetActive(true);
    }

    public void InfoClicked(){
        //directly called by info button
        Debug.Log("Info is Clicked");
        buildingInstanceUI.InfoIsClicked(gameObject);
        // GetAllTheStats();
    }
    public void UpgradeClicked(){
        //directly called by Upgrade button
        Debug.Log("Upgrade is Clicked");
        buildingInstanceUI.UpgradeIsClicked(gameObject);
    }
    public void DisableUI(){
         UIButtonPanel.SetActive(false);
    }
   
}
