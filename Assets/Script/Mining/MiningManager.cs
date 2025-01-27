using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningManager : MonoBehaviour
{
    private Mining mining;
    [SerializeField] private CurrencyManager currencyManager;

    public void InitiateMiningProcess(GameObject TheUnit,TheMine theMine){
        Mining mining=TheUnit.GetComponent<Mining>();
        if(!theMine.IsMineOccupied()){
            if(mining.IsMiningPossible()){                
                mining.StartMining(theMine);
                TheUnit.GetComponent<TheUnit>().isMining=true;
                theMine.setMineStatus(true);
                Refresh();
            }
            else{
                Debug.Log("can't load ,full ");
            }
        }
        else{
            Debug.Log("already occupied,can't mine");
        }
    }
    

    public void CollectMinedResources(GameObject Army){
        Mining mining=Army.GetComponent<Mining>();
        currencyManager.CollectMinedResource(mining.ReturnResourcesMine());
    }

    void Refresh(){
        mining=null;
    }
}
