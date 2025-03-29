using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

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
    // void OnApplicationQuit()
    // {
    //     var buildingInstances = FindObjectsOfType<BuildingInstance>();
    //     foreach (var instance in buildingInstances)
    //     {
    //         SaveBuildingData(instance.gameObject);
    //     }
    // }
    public void SaveAllBuildingData(){
        Debug.Log("savingBuilding called.");
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
    int resourceAmount=0; // Default value

    float TrainingProgression=0,TotalTime=0;
    bool isTrainingOngoing=false;
    int []troopsData=null;
    
    bool buildingStatus=building.GetComponent<BuildingInstance>().ReturnBuildingStatus();
    int []upgradeData=building.GetComponent<BuildingInstance>().GetUpgradeProgress();

    // If it's a farm, get its resourceAmount
    if (building.TryGetComponent<Farm>(out Farm farm))
    {
        resourceAmount = farm.resourceAmount;
    }
    else if(building.TryGetComponent<TheBarrack>(out TheBarrack theBarrack)){
        // ProgressTime = (int)trainingHandler.GetProgressedTime();
        Debug.Log("TrainingHandler is found.");
        TrainingHandler trainingHandler=theBarrack.GetTrainingHandler();
        isTrainingOngoing=trainingHandler.ReturnIsTrainingOngoing();
        if(isTrainingOngoing){
            Debug.Log("Training was ongoing.");
            troopsData=theBarrack.GetTroopsData();
            TotalTime=trainingHandler.GetTotalTime();
            TrainingProgression=trainingHandler.GetProgressedTime();
        }
    }

    // Generate a unique random ID between 100 and 1000
    int buildingID;
    do
    {
        buildingID = UnityEngine.Random.Range(100, 1001);
    } while (buildings.Exists(b => b.buildingId == buildingID));

    // Check if building already exists (by name)
    for (int i = 0; i < buildings.Count; i++)
    {
        if (buildings[i].buildingName == nameOfBuilding)
        {
            buildings[i].level = level;
            buildings[i].position = pos;
            buildings[i].buildingStatus=buildingStatus;
            buildings[i].upgradeData=upgradeData;

            if(building.GetComponent<Farm>()){
            buildings[i].resourceAmount = resourceAmount; // Save resource progress
            }else if(building.GetComponent<TheBarrack>()){
                // buildings[i].resourceAmount = (int)TrainingProgression;
                buildings[i].isTrainingOngoing=isTrainingOngoing;
                buildings[i].TrainingProgression=TrainingProgression;
                buildings[i].TotalTime=TotalTime;
                buildings[i].troopsData=troopsData;
            }

            SaveToFile();
            return;
        }
    }

    // If new, add to list with unique ID and resource amount\
    
    BuildingInfo newBuilding = new BuildingInfo(buildingID, nameOfBuilding, level, pos, resourceAmount
    ,isTrainingOngoing,TrainingProgression,TotalTime,troopsData);
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
             DateTime lastSaveTime = DateTime.ParseExact(JsonUtility.FromJson<BuildingList>(json)
             .lastSavedTime,
              "yyyy-MM-dd HH:mm:ss", null);
                TimeSpan SavedTimeElapsed = DateTime.UtcNow - lastSaveTime;
            SpawnBuildings(SavedTimeElapsed);
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

    private void SpawnBuildings(TimeSpan SavedTimeElapsed)
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
                
                if(spawnedBuilding.GetComponent<Farm>()){

                SetPreviousFarm(spawnedBuilding, data.level,data.resourceAmount);
                }
                else if(spawnedBuilding.GetComponent<TheBarrack>()){
                    SetPreviousBarrack(spawnedBuilding, data.level,data.isTrainingOngoing,
                    data.TrainingProgression,data.TotalTime,data.troopsData);
                
                }
                // }else if(spawnedBuilding.GetComponent<Base>()){

                //     SetPreviousBase(data.level);
                // }
                // else if(spawnedBuilding.<Laboratory>()){
                //     Set
                // }
                spawnedBuilding.GetComponent<BuildingInstance>().BuildingStatusRestoring(
                    data.buildingStatus,data.upgradeData,SavedTimeElapsed);
                // else if(spawnedBuilding.GetComponent<Base>()){
                //     SetPreviousBase(spawnedBuilding, data.level);
                // }
            }
            // else
            // {
            //     Debug.LogWarning("No prefab found for " + data.buildingName);
            // }
            if(data.buildingName=="Base"){
                SetPreviousBase(data.level,data.buildingStatus,data.upgradeData,SavedTimeElapsed);
            }
        }
    }

    private GameObject GetBuildingPrefab(string buildingName)
    {
        // Debug.Log("Searching for prefab: " + buildingName);
        return buildingStatsManager.GetBuildingStats(buildingName,1).TheOriginal;
        // return null;
    }

    private void SetPreviousFarm(GameObject building, int level,int resource)
    {
        // if (building.GetComponent<Farm>())
{
    Farm farm = building.GetComponent<Farm>();
    // farm.level = level;
    farm.SettingPreviousData(level);
    farm.SetResourceAmount(resource);
}}
void SetPreviousBarrack(GameObject building, int level,bool isTrainingOngoing,
float TrainingProgression,float TotalTime,int[] troopsData)
{
    TheBarrack barrack = building.GetComponent<TheBarrack>();
    // barrack.level = level;
    barrack.SettingPreviousData(level,isTrainingOngoing,TrainingProgression,TotalTime,troopsData);
}
void SetPreviousBase( int level,bool buildingStatus,int[] upgradeData,TimeSpan SavedTimeElapsed)
{
    GameObject baseObject = GameObject.FindObjectOfType<Base>()?.gameObject;
    if (baseObject != null)
    {
        Base baseBuilding = baseObject.GetComponent<Base>();
    // baseBuilding.level = level;
    baseBuilding.SettingPreviousData(level);
    baseBuilding.GetComponent<BuildingInstance>().BuildingStatusRestoring(
                    buildingStatus,upgradeData,SavedTimeElapsed);
}}
// else if (building.GetComponent<Laboratory>())
// {
//     Laboratory lab = building.GetComponent<Laboratory>();
//     // lab.level = level;
//     // lab.SettingPreviousData(int level);
// }

    

    private void SaveToFile()
{
    BuildingList dataToSave = new BuildingList(buildings);
    dataToSave.lastSavedTime = System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"); // Update time

    string json = JsonUtility.ToJson(dataToSave, true);
    File.WriteAllText(savePath, json);
    Debug.Log("Buildings saved to: " + savePath);
}

}

// Helper class for saving
[System.Serializable]
public class BuildingList
{
    public List<BuildingInfo> buildings;
    public string lastSavedTime; // Store the time when the file was last saved

    public BuildingList(List<BuildingInfo> list)
    {
        buildings = list;
        lastSavedTime = System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"); // Store in UTC format
    }
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
    public bool isTrainingOngoing;
    public float TrainingProgression,TotalTime;
    public int[] troopsData;

    public bool buildingStatus;
    public int[] upgradeData;
    public BuildingInfo(int id, string name, int lvl, Vector3 pos, int resource = 0,
    bool IsTrainingOngoing=false,float trainingProgression=0,float totalTime=0,int[] TroopsData=null
    ,bool BuildingStatus=false,int[] UpgradeData=null)
    {
        buildingId = id;
        buildingName = name;
        level = lvl;
        position = pos;
        resourceAmount = resource; // Default is 0, but will be assigned if it's a Farm
        isTrainingOngoing=IsTrainingOngoing;
        TrainingProgression=trainingProgression;
        TotalTime=totalTime;
        troopsData=TroopsData;
        buildingStatus=BuildingStatus;
        upgradeData=UpgradeData;
        
    }
}



