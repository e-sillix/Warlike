using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDependencyManager : MonoBehaviour
{
    [SerializeField] private BuildingInstanceUI buildingInstanceUI;
     [SerializeField] private TroopsTrainingManager troopsTrainingManager;
     [SerializeField]private TroopsCountManager troopsCountManager;
     [SerializeField]private GameObject ParentObject;
    public void DependencyTo(GameObject gameObject)
    {
        gameObject.transform.SetParent(ParentObject.transform);
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.GetComponent<BuildingInstance>().assigningManager(buildingInstanceUI);
        if(gameObject.GetComponent<TheBarrack>()){
            gameObject.GetComponent<TheBarrack>().InitDependency(troopsTrainingManager
            ,troopsCountManager);
        }
    }
}
