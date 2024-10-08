using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private GameObject target;
   public void PerformAction(GameObject TheArmy){

        target=TheArmy.GetComponent<TheUnit>().target;
        Debug.Log("will perform on this "+TheArmy.GetComponent<TheUnit>().ArmyId);    
    }
}
