using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionDependencyManager : MonoBehaviour
{
     
     [SerializeField] private BuildingDependencyManager buildingDependencyManager;
    public void ProvideDependency(GameObject gameObject,GameObject TheBuilding){
        //this is one is called when the building is constructed for first time.
        gameObject.GetComponent<UnderConstructionInstance>().ConstructionDependency(TheBuilding,
        buildingDependencyManager);
    }
    
}
