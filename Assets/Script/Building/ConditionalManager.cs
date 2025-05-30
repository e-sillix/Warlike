using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalManager : MonoBehaviour
{
    private BuildingManager buildingManager;
    private BluePrint bluePrint;
    [SerializeField] private TradingManager tradingManager;
    private GameObject TheChosenBlueprint;
    void Start()
    {
        buildingManager=GetComponent<BuildingManager>();
    }
    public int CheckAllTheCondition(){
        //space
        //inside innerkingdom
        if(!bluePrint.ReturnIsInsideKingdom()){
            // inside the kingdom
            Debug.Log(bluePrint.ReturnIsInsideKingdom());
            return 1;
        }               
        else if(bluePrint.ReturnIsColliding()){
            return 2;
        }
        
        return 0; //if no problem
    }
    public bool CheckIsEnough(int woodCost,int grainCost,int stoneCost){
        return tradingManager.IsEnoughResource( woodCost, grainCost, stoneCost);
    }

    public void SpawningBluePrint(GameObject chosenBlueprint){
        //instiate blueprint and assign them
        Vector3 spawnPosition = Vector3.zero; // Default position

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            spawnPosition = hit.point; // Place on the ground
        }

TheChosenBlueprint = Instantiate(chosenBlueprint, spawnPosition, Quaternion.identity);
        // TheChosenBlueprint=Instantiate(chosenBluepsrint);
        bluePrint=TheChosenBlueprint.GetComponent<BluePrint>();
    }
    // public Vector3 GetTheBlueprintPosition(){
    //     return TheChosenBlueprint.transform.position;
    // }
    public GameObject ReturnBlueprintObj(){
        return TheChosenBlueprint;
    }
    public void DestroyTheBlueprint(){
        Destroy(TheChosenBlueprint);
        bluePrint=null;
    }
    
   

}
