using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TowerPersistanceManager : MonoBehaviour
{ 
    [SerializeField]private TroopsExpeditionManager troopsExpeditionManager;
    private TowerPointPlacer towerPointPlacer;
    [SerializeField] private GameObject towerPrefab; // assign your tower prefab in Inspector
    private string filePath;
    [SerializeField]private GameObject Parent;

    void Start()
    {
        towerPointPlacer=GetComponent<TowerPointPlacer>();
        filePath = Application.persistentDataPath + "/towers.json";
        LoadAndSpawnTowers();
    }

    void LoadAndSpawnTowers()
{
    if (!File.Exists(filePath))
    {
        Debug.LogWarning("❌ No tower save file found at: " + filePath);
        return;
    }

    string json = File.ReadAllText(filePath);
    TowerDataList dataList = JsonUtility.FromJson<TowerDataList>(json);

    foreach (TowerData data in dataList.towers)
    {
        Vector3 pos = data.GetPosition();

        // Now parent the instantiated tower
        GameObject newTower = Instantiate(towerPrefab, pos, Quaternion.identity, Parent.transform);

        TowerInstance instance = newTower.GetComponent<TowerInstance>();
        if (instance != null)
        {
            if(data.bossId != 0)
            {
                instance.PersistanceSpawning(data.bossId,data.towerId); // pass boss obj if needed
                newTower.GetComponent<TowerCombat>().Dependency(troopsExpeditionManager,
                towerPointPlacer);
            }
        }
    }

    // Debug.Log("✅ Loaded and spawned " + dataList.towers.Count + " towers.");
}

}
