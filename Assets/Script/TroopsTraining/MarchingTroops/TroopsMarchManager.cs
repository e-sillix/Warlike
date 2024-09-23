
using UnityEngine;

public class TroopsMarchManager : MonoBehaviour
{
    // it should only be triggered by ui and it's manager
    private TheUnit selectedObject;
    private GameObject selectedGO;
    [SerializeField] private GameObject TheUnitPrefab;
    [SerializeField] private GameObject Spawnpoint;
    [SerializeField] private ArmyCount troopsCounter;
   

  
   public void InitiateTheMarchProcess(TheUnit selectedObject,Vector3 position){
     //this will be triggered by MUIM if there is selected
     //it will be triggered by initiatenewmarchprocess  
     selectedObject.SetTargetPosition(position);
   }
   public TheUnit InitiateNewMarchProcess(int troopsCount,Vector3 position){
     //this will be triggered when MUIM doesn't have selected
     selectedGO=Instantiate(TheUnitPrefab,Spawnpoint.transform.position, Spawnpoint.transform.rotation);

     //cut the soldier count from the base
     troopsCounter.WithDrawingTroopsFromBase(troopsCount);

     selectedObject=selectedGO.GetComponent<TheUnit>();
     selectedObject.SetTroopsVisualCount(troopsCount);
     InitiateTheMarchProcess(selectedObject,position);
     return selectedObject;
   }
   

}
