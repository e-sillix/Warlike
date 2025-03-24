using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderConstructionInstance : MonoBehaviour
{
    // Start is called before the first frame update
    // private BuildingInstanceUI buildingInstanceUI;
    private GameObject RealBuilding;
    private Coroutine BuildingCoroutine;
    private int timeCost;
    private GameObject SpawnedBuilding;
    private BuildingDependencyManager buildingDependencyManager;
   public void ConstructionDependency(GameObject realBuilding,BuildingDependencyManager 
   BuildingDependencyManager){
        RealBuilding=realBuilding;
        buildingDependencyManager=BuildingDependencyManager;

        // Start the coroutine when this function is called
    if (BuildingCoroutine != null)
    {
        StopCoroutine(BuildingCoroutine);
    }
    BuildingCoroutine = StartCoroutine(BuildingConstructionCoroutine());
   }
   private IEnumerator BuildingConstructionCoroutine()
{
    yield return new WaitForSeconds(3f); // Wait for 10 seconds

    if (RealBuilding != null)
    {
        SpawnedBuilding=Instantiate(RealBuilding, transform.position, Quaternion.identity);
        buildingDependencyManager.DependencyTo(SpawnedBuilding);
        SpawnedBuilding.GetComponent<BuildingInstance>().ProvideBasicDependency(
            buildingDependencyManager);
        

        Destroy(gameObject);
    }
}
}
