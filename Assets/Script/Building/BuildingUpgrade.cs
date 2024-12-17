using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgrade : MonoBehaviour
{
    //this is a manager for upgrading building.

    private GameObject Target;

    public void UpgradeTarget(GameObject target){  //(buildingGO)
        //should be called by BuidlingInstance when upgrade button clicked.
        //store the target
        Target=target;

        //analyse the target for the type and level

        GetTheStats();        
    }
    void GetTheStats(){
        //get and store stats of the next level.

    }

    public void ConfirmUpgrade(){
        //called by upgrade by ui or indirectly 

        //if enough credits
        Upgrade();        
    }

    void Upgrade(){
        //Replace all target info ,in adv replace prefab.

    }
    public void Refresh(){
        //refresh after using or cancelling

    }
}
