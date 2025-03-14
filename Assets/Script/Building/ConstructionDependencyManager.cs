using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionDependencyManager : MonoBehaviour
{
     
     [SerializeField] private BuildingDependencyManager buildingDependencyManager;
    public void ProvideDependency(GameObject gameObject,GameObject TheBuilding){
        // gameObject.GetComponent<BuildingInstance>().assigningManager(buildingInstanceUI);
        // TargetTypeDependency(gameObject);

        gameObject.GetComponent<UnderConstructionInstance>().ConstructionDependency(TheBuilding,
        buildingDependencyManager);
    }
    
}
