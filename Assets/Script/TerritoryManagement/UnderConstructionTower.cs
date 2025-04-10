using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnderConstructionTower : MonoBehaviour
{
    [SerializeField] private float ConstructionTime = 5f;
    [SerializeField]private GameObject TowerPrefab;
    [SerializeField] private Slider ConstructionProgressBar; // Assign in Inspector
    [SerializeField] private TextMeshProUGUI timeRemainingText; // Assign for time display
    private TroopsExpeditionManager troopsExpeditionManager;
    private TowerPointPlacer towerPointPlacer;
    public void Dependency(TroopsExpeditionManager TroopsExpeditionManager,
    TowerPointPlacer TowerPointPlacer){
        troopsExpeditionManager=TroopsExpeditionManager;
        towerPointPlacer=TowerPointPlacer;
    }
    void Start()
    {
        StartCoroutine(BuildingCorotine());
    }

    IEnumerator BuildingCorotine()
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

        // GameObject tower = Instantiate(TowerPrefab, transform.position, Quaternion.identity); 
        // instantiate the tower prefab at the current position of the under construction tower


        if (TowerPrefab != null)
    {
        GameObject SpawnedBuilding = Instantiate(TowerPrefab, transform.position, 
        Quaternion.identity);
        // buildingDependencyManager.DependencyTo(SpawnedBuilding);
        // SpawnedBuilding.GetComponent<BuildingInstance>().ProvideBasicDependency
        // (buildingDependencyManager);

        SpawnedBuilding.GetComponent<TowerInstance>().SetOwnerShip(true);
        SpawnedBuilding.GetComponent<TowerCombat>().Dependency(troopsExpeditionManager,
        towerPointPlacer);
        Destroy(gameObject);
    }
    }

}
