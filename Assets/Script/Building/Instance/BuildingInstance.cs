using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuildingInstance : MonoBehaviour
{
    //this will be attached to every building ,interact with click.
    [SerializeField] private GameObject UIButtonPanel;
    [SerializeField] private BuildingInstanceUI buildingInstanceUI;
    private bool BuildingIsBeingUpgraded;
    private Coroutine UpgradeCoroutine;
    [SerializeField]private BuildingPersistenceManager buildingPersistenceManager;
    // [SerializeField]private  UpgradeStats upgradeStats;
    private BuildingDependencyManager buildingDependencyManager;
    [SerializeField]private BuildingUpgrade buildingUpgrade;
    private BuildingStatsManager buildingStatsManager;
    [SerializeField]private UpgradeStats upgradeStats;
    [SerializeField]private TimeElapsedManagement timeElapsedManagement;
    private (int years, int months, int days, int hours, int minutes, int seconds) timeElapsed;
    // private bool isBuildingBeingUpgraded;

    private float ConstrucionProgress,ConstructionTime;

     [SerializeField]private GameObject ConstructionProgressBarPanel;
    [SerializeField] private Slider ConstructionProgressBar; // Assign in Inspector
    [SerializeField] private TextMeshProUGUI timeRemainingText; // Assign for time display

    private TimeSpan SavedTimeElapsed;
    private int[] previousUpdateData;

    private int buildingId;
    // private string name;
    // private int level;

    // public bool ReturnBuildingIsBeingUpgraded(){
    //     //by building persistence manager
    //     return BuildingIsBeingUpgraded;
    // }

    public void SetBuildingId(int i){
        buildingId=i;
    }
    public int GetBuildingId(){
        return buildingId;
    }
    public TimeElapsedManagement ReturnTimeElapsedManagement(){
        return timeElapsedManagement;
    }
    public void TriggerSaveAll(){
        //by attached barrack,farm
        buildingPersistenceManager.SaveAllBuildingData();
    }
    public void ProvideBasicDependency(BuildingDependencyManager BuildingDependencyManager){
        //called by when it is spawned after closing the game.
        buildingDependencyManager=BuildingDependencyManager;

        //buildingdepen. for all the dependencies.

        buildingDependencyManager.ProvideAllConditionalDependencies(gameObject);
    }
    
    public void GetallBuildingDependencies(BuildingPersistenceManager BuildingPersistenceManager,
    BuildingInstanceUI BuildingInstanceUI,
    UpgradeStats UpgradeStats,TimeElapsedManagement TimeElapsedManagement,BuildingUpgrade 
    BuildingUpgrade){
        //this will be countercall by provideBasicDependency.
        buildingPersistenceManager=BuildingPersistenceManager;
        buildingInstanceUI=BuildingInstanceUI;
        // buildingStatsManager=BuildingStatsManager;
        upgradeStats=UpgradeStats;
        timeElapsedManagement=TimeElapsedManagement;
        buildingUpgrade=BuildingUpgrade;
        // upgradeStats.SetData(gameObject);

    }
    // public void OnApplicationQuit()
    // {
    //     buildingPersistenceManager.SaveBuildingData(gameObject);        
    // }
    public void TriggerSave(){
        buildingPersistenceManager.SaveBuildingData(gameObject);
    }
    public void SetData(){
        //by barrack ,farm when they are established.
        if(upgradeStats){

        upgradeStats.SetData(gameObject);
        }else{
            Debug.LogError("UpgradeStats is not found."+gameObject.name);
        }
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
        //by BuildingInstanceUI and buildingPersistenceManager
        return BuildingIsBeingUpgraded;
    }

    public int[] GetUpgradeProgress(){
        if(BuildingIsBeingUpgraded){

        return new int[]{(int)ConstrucionProgress,(int)ConstructionTime};
        }
        else{
            return null;
        }
    }
    // public void SetBuildingStatus(bool status){
    //     BuildingIsBeingUpgraded=status;
    // }
    
    public void UpgradeIsOrdered(int[] UpgradeData,int time){//order:
    // level + 1, upgradeCost.capacity, upgradeCost.rate;

        BuildingIsBeingUpgraded=true;
        UpgradeCoroutine=StartCoroutine(UpgradeProcess( UpgradeData,0,time));
        buildingPersistenceManager.SaveAllBuildingData();
        }

// Coroutine to wait and then apply the upgrade
    private IEnumerator UpgradeProcess( int[] UpgradeData,int progressTime,int time)
    {
        ConstructionProgressBarPanel.SetActive(true);
        ConstructionTime=time;

        ConstrucionProgress =progressTime;
        // buildingPersistenceManager.SaveAllBuildingData();
        // buildingPersistenceManager.SaveBuildingData(gameObject);

        // ConstructionProgressBar.value = 0;  // Start from 0 progress

    while (ConstrucionProgress < ConstructionTime)
    {
        ConstrucionProgress += Time.deltaTime;

        float progress = ConstrucionProgress / ConstructionTime;
            ConstructionProgressBar.value = progress; // Update progress bar

            int remainingTime = Mathf.CeilToInt(ConstructionTime-ConstrucionProgress); // Calculate remaining time
            UpdateTimeDisplay(remainingTime); // Update UI text
        // ConstructionProgressBar.value = construcionProgress / constructionTime;  // Update progress bar
        // int timeLeft = Mathf.CeilToInt(ConstructionTime - ConstrucionProgress); // Calculate remaining time
        // timeRemainingText.text = $"Time Left: {timeLeft}s"; // Update UI text
        yield return null;  // Wait for the next frame
    }

    // Wait for the specified upgrade time
    // yield return new WaitForSeconds(time);

    UpgradeStats(UpgradeData);
     ConstructionProgressBarPanel.SetActive(false);

    // Apply upgrade logic here (level, capacity, rate)
    // ApplyUpgrade(upgradeData);
    // Debug.Log("Building Upgraded");
    // buildingPersistenceManager.SaveBuildingData(gameObject);
    buildingPersistenceManager.SaveAllBuildingData();
    // Upgrade complete
    BuildingIsBeingUpgraded = false;
    }
    private void UpdateTimeDisplay(int secondsLeft)
    {
        int minutes = secondsLeft / 60;
        int seconds = secondsLeft % 60;
        timeRemainingText.text = $"{minutes:D2}:{seconds:D2}"; // Format: MM:SS
    }
    public void BuildingStatusRestoring(bool status,int[] UpgradeTiming,TimeSpan savedTimeElapsed){
        //by buildingPersistenceManager
        // BuildingIsBeingUpgraded=status;
        if(status){
            // UpgradeStats(UpgradeData);
            SavedTimeElapsed=savedTimeElapsed;
            ContinueUpgrading(UpgradeTiming);
        }

    }
    public void RecieveUpgradeData(int[] UpgradeData){
        // UpgradeStats(UpgradeData)
        previousUpdateData=UpgradeData;
    }
    void ContinueUpgrading(int [] UpgradeTiming){
        previousUpdateData=buildingUpgrade.DirectBuildingUpgrade(gameObject);
        // timeElapsed=timeElapsedManagement.CalculateTimeElapsed();
        // timeElapsed=(SavedTimeElapsed.days,);
        timeElapsed = (
    SavedTimeElapsed.Days / 365,                 // Approximate years
    (SavedTimeElapsed.Days % 365) / 30,          // Approximate months
    SavedTimeElapsed.Days % 30,                  // Remaining days
    SavedTimeElapsed.Hours,
    SavedTimeElapsed.Minutes,
    SavedTimeElapsed.Seconds

);

        if(timeElapsed.years>0||timeElapsed.months>2){
// resourceAmount=capacity;
        // UpgradeStats(UpgradeData);

    // Apply upgrade logic here (level, capacity, rate)
    // ApplyUpgrade(upgradeData);
        UpgradeStats(previousUpdateData);
        // Debug.Log("Building Upgraded");
        buildingUpgrade.DirectBuildingUpgrade(gameObject);
        buildingPersistenceManager.SaveBuildingData(gameObject);
    // Upgrade complete
        BuildingIsBeingUpgraded = false;
        }else{
            float timeElapsedInSeconds =timeElapsed.days * 86400 + timeElapsed.hours 
        * 3600 + timeElapsed.minutes * 60 + timeElapsed.seconds;
            int progression=UpgradeTiming[0];
            int totalTime=UpgradeTiming[1];
            if(totalTime<timeElapsedInSeconds||totalTime<(timeElapsedInSeconds+progression))
            {
                //  UpgradeStats(UpgradeData);
                UpgradeStats(previousUpdateData);
                buildingUpgrade.DirectBuildingUpgrade(gameObject);
                

    // Apply upgrade logic here (level, capacity, rate)
    // ApplyUpgrade(upgradeData);
        // Debug.Log("Building Upgraded");
        buildingPersistenceManager.SaveBuildingData(gameObject);
    // Upgrade complete
        BuildingIsBeingUpgraded = false;
            }
            else{
                // StartTraining(troopsData,(int)(TotalTime-(timeElapsedInSeconds+TrainingProgression)));
                ResumeConstruction((int)(timeElapsedInSeconds+progression),totalTime,
                previousUpdateData);               
            }
        }
    }

    void ResumeConstruction(int progresstime,float TotalTime,int[] UpgradeData){
        //call function  for training
        // UpgradeCoroutine=StartCoroutine(UpgradeProcess(UpgradeStats.GetUpgradeData(),TotalTime));
        BuildingIsBeingUpgraded=true;
        UpgradeCoroutine=StartCoroutine(UpgradeProcess(UpgradeData,progresstime,(int)TotalTime));
    }

    void UpgradeStats(int[] UpgradeData){
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
    }
    public void CancelUpgrade(){
        if(UpgradeCoroutine!=null){
            StopCoroutine(UpgradeCoroutine);
            ConstructionProgressBarPanel.SetActive(false);
            BuildingIsBeingUpgraded = false;
             buildingPersistenceManager.SaveAllBuildingData();
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
        UIButtonPanel.SetActive(false);
        // GetAllTheStats();
    }
    public void UpgradeClicked(){
        //directly called by Upgrade button
        Debug.Log("Upgrade is Clicked");
        buildingInstanceUI.UpgradeIsClicked(gameObject);
        UIButtonPanel.SetActive(false);
    }
    public void DisableUI(){
         UIButtonPanel.SetActive(false);
    }
   
}
