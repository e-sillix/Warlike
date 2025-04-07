using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UnderConstructionInstance : MonoBehaviour
{
    // Start is called before the first frame update
    // private BuildingInstanceUI buildingInstanceUI;
    private GameObject RealBuilding;
    public int ConstructionTime;
    private Coroutine BuildingCoroutine;
    private int timeCost;
    private GameObject SpawnedBuilding;
    private BuildingDependencyManager buildingDependencyManager;
    [SerializeField] private Slider ConstructionProgressBar; // Assign in Inspector
    [SerializeField] private TextMeshProUGUI timeRemainingText; // Assign for time display
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
    float elapsedTime = 0f;
    ConstructionProgressBar.value = 0;  // Start from 0 progress

    while (elapsedTime < ConstructionTime)
    {
        elapsedTime += Time.deltaTime;
        ConstructionProgressBar.value = elapsedTime / ConstructionTime;  // Update progress bar
        int timeLeft = Mathf.CeilToInt(ConstructionTime - elapsedTime); // Calculate remaining time
        timeRemainingText.text = $"{timeLeft}s"; // Update UI text
        yield return null;  // Wait for the next frame
    }

    ConstructionProgressBar.value = 1f;  // Ensure it reaches 100% at the end
    // timeRemainingText.text = "Completed!"; // Show completion message

    if (RealBuilding != null)
    {
        SpawnedBuilding = Instantiate(RealBuilding, transform.position, Quaternion.identity);
        buildingDependencyManager.DependencyTo(SpawnedBuilding);
        SpawnedBuilding.GetComponent<BuildingInstance>().ProvideBasicDependency(buildingDependencyManager);

        Destroy(gameObject);
    }
}


}
