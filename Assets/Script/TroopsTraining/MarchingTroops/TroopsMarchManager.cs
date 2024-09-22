using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsMarchManager : MonoBehaviour
{
    // it should only be triggered by ui and it's manager
   
   void PassThePositionAndNumberToMarch(UnitSelector selectedObject,Vector3 position){//it should pass type of soldier and lvl too.
        selectedObject.NotifyParentPosition(position);//this selected GO is the Troop prefab
   }
   void GetTheSoldierCount(){

   }
   public void InitiateTheMarchProcess(UnitSelector selectedObject,Vector3 position){
    //it will be triggered by MUIM ~44
    GetTheSoldierCount();
    PassThePositionAndNumberToMarch(selectedObject,position);
   }
}
