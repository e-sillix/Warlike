using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDependencyManager : MonoBehaviour
{
    [SerializeField] private BuildingInstanceUI buildingInstanceUI;
     [SerializeField] private TroopsTrainingManager troopsTrainingManager;
     [SerializeField]private TroopsCountManager troopsCountManager;
     [SerializeField]private BuildingPersistenceManager buildingPersistenceManager;
     [SerializeField]private BuildingUpgrade buildingUpgrade;
     [SerializeField]private GameObject ParentObject;
     [SerializeField]private BuildingStatsManager buildingStatsManager;
     [SerializeField]private UpgradeStats upgradeStats;
     [SerializeField]private TimeElapsedManagement timeElapsedManagement;
    //  [SerializeField] private BuildingInstanceUI buildingInstanceUI;
    public void DependencyTo(GameObject gameObject)
    {
        //called by construction instance when corotinue ends
        gameObject.transform.SetParent(ParentObject.transform);
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.GetComponent<BuildingInstance>().assigningManager(buildingInstanceUI);
        if(gameObject.GetComponent<TheBarrack>()){
            gameObject.GetComponent<TheBarrack>().InitDependency(troopsTrainingManager
            ,troopsCountManager);
        }
        gameObject.GetComponent<BuildingInstance>()
        .DependencyInjection(buildingPersistenceManager);
    }
    // public void DependencyProvider(GameObject building){
    //     building.GetComponent<BuildingInstance>().Dependency(buildingInstanceUI,
    //     this.buildingPersistenceManager);
    //     if(building.GetComponent<TheBarrack>()){
    //         building.GetComponent<TheBarrack>().InitDependency(troopsTrainingManager
    //         ,troopsCountManager);
    //     }
    // }

    public void ProvideAllConditionalDependencies(GameObject building){
        //called by building instance.
        building.transform.SetParent(ParentObject.transform);
        building.transform.localRotation = Quaternion.identity;
        building.GetComponent<BuildingInstance>().GetallBuildingDependencies
        (buildingPersistenceManager,buildingInstanceUI,upgradeStats,timeElapsedManagement
        ,buildingUpgrade);

        if(building.GetComponent<TheBarrack>()){
            building.GetComponent<TheBarrack>().InitDependency(troopsTrainingManager
            ,troopsCountManager);
        }
        }
}
