using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistancManager : MonoBehaviour
{
    [SerializeField] private BuildingPersistenceManager buildingPersistenceManager;
    [SerializeField] private CurrencyManager currencyManager;

    [SerializeField]private TroopsCountManager troopsCountManager;

    void Start()
    {
        var timeElapsed =GetComponent<TimeElapsedManagement>().CalculateTimeElapsed();
        //load Currency data
        currencyManager.LoadPreviousData();
        
        //load buildings 
        buildingPersistenceManager.LoadPreviousBuildingData();

        //load troops data

          
    }
}
