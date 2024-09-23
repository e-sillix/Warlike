
using UnityEngine;

public class TroopsMarchManager : MonoBehaviour
{
    // it should only be triggered by ui and it's manager
    private TheUnit selectedObject;
    private GameObject selectedGO;
    [SerializeField] private GameObject TheUnitPrefab;
    [SerializeField] private GameObject Spawnpoint;
   

  
   public void InitiateTheMarchProcess(TheUnit selectedObject,Vector3 position){
     //this will be triggered by MUIM if there is selected
     //it will be triggered by initiatenewmarchprocess  
     selectedObject.SetTargetPosition(position);
   }
   public TheUnit InitiateNewMarchProcess(int troopsdata,Vector3 position){
     //this will be triggered when MUIM doesn't have selected
     selectedGO=Instantiate(TheUnitPrefab,Spawnpoint.transform.position, Spawnpoint.transform.rotation);

     selectedObject=selectedGO.GetComponent<TheUnit>();
     InitiateTheMarchProcess(selectedObject,position);
     return selectedObject;
   }
   

}
