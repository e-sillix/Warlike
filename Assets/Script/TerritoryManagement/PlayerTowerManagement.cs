using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTowerManagement : MonoBehaviour
{
    [SerializeField]private GameObject TowerUI;

    [SerializeField]private int[] resourcesNeededForTower=new int[3];
    [SerializeField] private TextMeshProUGUI Cost;
    [SerializeField]private CurrencyManager currencyManager;
    [SerializeField]private TradingManager tradingManager;
    [SerializeField]private MessageManager messageManager;
    [SerializeField]private GameObject UnderConstructionTowerPrefab,BluePrintPrefab;
    [SerializeField]private CameraSystem cameraSystem;

    private GameObject Pointer;
    public void BuildClicked(GameObject g){
        
        Pointer=g;
        TowerUI.SetActive(true);
        Cost.text="W"+resourcesNeededForTower[0]+", G"+resourcesNeededForTower[1]
        +", S"+resourcesNeededForTower[2];
    }

    public void BuildingConfirmed(){
        //directly called from UI
        if(tradingManager.IsEnoughResource(resourcesNeededForTower[0],resourcesNeededForTower[1]
        ,resourcesNeededForTower[2])){
            currencyManager.SpendBuildingCost(resourcesNeededForTower[0],resourcesNeededForTower[1]
            ,resourcesNeededForTower[2]);
            SpawnTheTower();
        }else{
            messageManager. MessageForNotEnoughCredit();
        }
        TowerUI.SetActive(false);
    }
    void SpawnTheTower(){
        GameObject Blue=Instantiate(BluePrintPrefab,Pointer.transform.position,Quaternion.identity);
        Destroy(Pointer);
        Blue.GetComponent<TowerBluePrint>().AllDependencies(UnderConstructionTowerPrefab,
        cameraSystem,messageManager);
        // cameraSystem.SetException(true);
    }
}
