using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuildingPersistenceManager : MonoBehaviour
{
    private string savePath;

    [SerializeField]private BuildingStatsManager buildingStatsManager;
    [SerializeField]private BuildingDependencyManager buildingDependencyManager;
    private List<BuildingInfo> buildings = new List<BuildingInfo>();

    private (int years, int months, int days, int hours, int minutes, int seconds) timeElapsed;
    [SerializeField] private List<GameObject> buildingPrefabs; // Assign prefabs in Inspector

    public void LoadPreviousBuildingData()
    {
        savePath = Application.persistentDataPath + "/buildingsData.json";
        LoadBuildingData();
    }
    void OnApplicationQuit()
    {
        var buildingInstances = FindObjectsOfType<BuildingInstance>();
        foreach (var instance in buildingInstances)
        {
            SaveBuildingData(instance.gameObject);
        }
    }

    public void SetTimeElapsed((int years, int months, int days, int hours, int minutes, int seconds)
     time)
    {
        timeElapsed = time;
    }
    public (int years, int months, int days, int hours, int minutes, int seconds) GetTimeElapsed()
    {
        //by all the building when they are established.
        return timeElapsed;
    }
   public void SaveBuildingData(GameObject building)
{
    string nameOfBuilding = building.name;
    int level = GetBuildingLevel(building);
    Vector3 pos = building.transform.position;
    int resourceAmount = 0; // Default value

    // If it's a farm, get its resourceAmount
    if (building.TryGetComponent<Farm>(out Farm farm))
    {
        resourceAmount = farm.resourceAmount;
    }

    // Generate a unique random ID between 100 and 1000
    int buildingID;
    do
    {
        buildingID = Random.Range(100, 1001);
    } while (buildings.Exists(b => b.buildingId == buildingID));

    // Check if building already exists (by name)
    for (int i = 0; i < buildings.Count; i++)
    {
        if (buildings[i].buildingName == nameOfBuilding)
        {
            buildings[i].level = level;
            buildings[i].position = pos;
            buildings[i].resourceAmount = resourceAmount; // Save resource progress

            SaveToFile();
            return;
        }
    }

    // If new, add to list with unique ID and resource amount
    BuildingInfo newBuilding = new BuildingInfo(buildingID, nameOfBuilding, level, pos, resourceAmount);
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
            if (!string.IsNullOrWhiteSpace(json))
            {
        
            // string json = File.ReadAllText(savePath);
            if(JsonUtility.FromJson<BuildingList>(json).buildings == null)
            {
                Debug.Log("Building File is empty.");
                return;
            }
            buildings = JsonUtility.FromJson<BuildingList>(json).buildings;
            Debug.Log("Loaded " + buildings.Count + " buildings from file.");

            SpawnBuildings();
            }
            else{
            Debug.Log("Building file is empty.");
                
            }
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
                SetBuildingLevel(spawnedBuilding, data.level,data.resourceAmount);
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

    private void SetBuildingLevel(GameObject building, int level,int resource)
    {
        if (building.GetComponent<Farm>())
{
    Farm farm = building.GetComponent<Farm>();
    // farm.level = level;
    farm.SettingPreviousData(level);
    farm.SetResourceAmount(resource);
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
    public int buildingId;       // Unique building ID
    public string buildingName;  // Name of the building
    public int level;            // Level of the building
    public Vector3 position;     // Position in world space
    public int resourceAmount;   // Stores resources (for farms only)

    public BuildingInfo(int id, string name, int lvl, Vector3 pos, int resource = 0)
    {
        buildingId = id;
        buildingName = name;
        level = lvl;
        position = pos;
        resourceAmount = resource; // Default is 0, but will be assigned if it's a Farm
    }
}
