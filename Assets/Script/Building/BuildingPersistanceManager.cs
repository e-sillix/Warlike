using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuildingPersistenceManager : MonoBehaviour
{
    private string savePath;

    [SerializeField]private BuildingStatsManager buildingStatsManager;
    [SerializeField]private BuildingDependencyManager buildingDependencyManager;
    private List<BuildingInfo> buildings = new List<BuildingInfo>();

    [SerializeField] private List<GameObject> buildingPrefabs; // Assign prefabs in Inspector

    public void LoadPreviousBuildingData()
    {
        savePath = Application.persistentDataPath + "/buildingsData.json";
        LoadBuildingData();
    }

   public void SaveBuildingData(GameObject building)
{
    string nameOfBuilding = building.name;
    int level = GetBuildingLevel(building);
    Vector3 pos = building.transform.position;

    // Generate a unique random ID between 100 and 1000
    int buildingID;
    do
    {
        buildingID = Random.Range(100, 1001); // Range is 100 to 1000 (1001 is exclusive)
    } while (buildings.Exists(b => b.buildingId == buildingID)); // Ensure ID is unique

    // Check if building already exists (by name)
    for (int i = 0; i < buildings.Count; i++)
    {
        if (buildings[i].buildingName == nameOfBuilding)
        {
            buildings[i].level = level;
            buildings[i].position = pos;
            
            SaveToFile();
            return;
        }
    }

    // If new, add to list with unique ID
    BuildingInfo newBuilding = new BuildingInfo(buildingID, nameOfBuilding, level, pos);
    buildings.Add(newBuilding);
    SaveToFile();
}


    private int GetBuildingLevel(GameObject building)
    {
        if (building.TryGetComponent<Farm>(out Farm farm)) return farm.level;
        if (building.TryGetComponent<TheBarrack>(out TheBarrack barrack)) return barrack.level;
        if (building.TryGetComponent<Base>(out Base baseBuilding)) return baseBuilding.level;
        if (building.TryGetComponent<Laboratory>(out Laboratory lab)) return lab.level;
        return 1; // Default level if not found
    }

    public void LoadBuildingData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            buildings = JsonUtility.FromJson<BuildingList>(json).buildings;
            Debug.Log("Loaded " + buildings.Count + " buildings from file.");

            SpawnBuildings();
        }
        else
        {
            Debug.Log("No saved building data found.");
        }
    }

    private void SpawnBuildings()
    {
        foreach (BuildingInfo data in buildings)
        {
            GameObject prefab = GetBuildingPrefab(data.buildingName);
            if (prefab != null)
            {
                GameObject spawnedBuilding = Instantiate(prefab, data.position, Quaternion.identity);
                
                // buildingDependencyManager.DependencyProvider(spawnedBuilding);
                
                spawnedBuilding.GetComponent<BuildingInstance>().
                ProvideBasicDependency(buildingDependencyManager);
                SetBuildingLevel(spawnedBuilding, data.level);
            }
            else
            {
                Debug.LogWarning("No prefab found for " + data.buildingName);
            }
        }
    }

    private GameObject GetBuildingPrefab(string buildingName)
    {
        Debug.Log("Searching for prefab: " + buildingName);
        return buildingStatsManager.GetBuildingStats(buildingName,1).TheOriginal;
        // return null;
    }

    private void SetBuildingLevel(GameObject building, int level)
    {
        if (building.GetComponent<Farm>())
{
    Farm farm = building.GetComponent<Farm>();
    // farm.level = level;
    farm.SettingPreviousData(level);
}
else if (building.GetComponent<TheBarrack>())
{
    TheBarrack barrack = building.GetComponent<TheBarrack>();
    // barrack.level = level;
    barrack.SettingPreviousData(level);
}
else if (building.GetComponent<Base>())
{
    Base baseBuilding = building.GetComponent<Base>();
    // baseBuilding.level = level;
    baseBuilding.SettingPreviousData(level);
}
else if (building.GetComponent<Laboratory>())
{
    Laboratory lab = building.GetComponent<Laboratory>();
    // lab.level = level;
    // lab.SettingPreviousData(int level);
}

    }

    private void SaveToFile()
    {
        string json = JsonUtility.ToJson(new BuildingList(buildings), true);
        File.WriteAllText(savePath, json);
        Debug.Log("Buildings saved to: " + savePath);
    }
}

// Helper class for saving
[System.Serializable]
public class BuildingList
{
    public List<BuildingInfo> buildings;
    public BuildingList(List<BuildingInfo> list) { buildings = list; }
}

// Stores building data
[System.Serializable]
public class BuildingInfo
{
    public string buildingName;
    public int level,buildingId;
    public Vector3 position;

    public BuildingInfo(int ID,string name, int lvl, Vector3 pos)
    {
        buildingName = name;
        level = lvl;
        position = pos;
        buildingId=ID;
    }
}
