using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyManager : MonoBehaviour
{
     [SerializeField] private BuildingInstanceUI buildingInstanceUI;
     [SerializeField] private TroopsTrainingManager troopsTrainingManager;
     [SerializeField]private TroopsCountManager troopsCountManager;
    public void ProvideDependency(GameObject gameObject){
        gameObject.GetComponent<BuildingInstance>().assigningManager(buildingInstanceUI);
        TargetTypeDependency(gameObject);
    }
    void TargetTypeDependency(GameObject gameObject){
        if(gameObject.GetComponent<TheBarrack>()){
            gameObject.GetComponent<TheBarrack>().InitDependency(troopsTrainingManager
            ,troopsCountManager);
        }

    }
}
