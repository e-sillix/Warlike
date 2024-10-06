using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private GameObject target;
   public void PerformAction(TheUnit TheArmy){
        target=TheArmy.target;
        Debug.Log("will perform on this "+target);    
    }
}
