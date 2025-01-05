using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedPointer : MonoBehaviour
{
    private TroopsExpeditionManager troopsExpeditionManager;
    GameObject Target;RaycastHit Hit;
    public Canvas worldCanvas;
    public void Dependency(TroopsExpeditionManager TroopsExpeditionManager
    ,GameObject target,RaycastHit hit){
        //called when spawned.
        troopsExpeditionManager=TroopsExpeditionManager;
        Target=target;
        Hit=hit;
        worldCanvas.worldCamera = Camera.main;
    }
    public void MarchClicked(){
        //triggered when clicked tick
        Debug.Log("March clicked");
        troopsExpeditionManager.PotentialTargetForMarchClicked(Target,Hit,gameObject);
    }
    public void CancelClicked(){
        //triggered when clicked cross

        //destroy
        Destroy(gameObject);
    }
}
