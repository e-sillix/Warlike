using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private int deltaWood;
    [SerializeField] private int deltaGrain;
    [SerializeField] private int deltaStone;
    [SerializeField] private TextMeshProUGUI woodsCounter;
    [SerializeField] private TextMeshProUGUI grainCounter;
    [SerializeField] private TextMeshProUGUI stoneCounter;

    private string savePath;

    
    
    private Dictionary<ResourceType, int> LocalResourcesCurrencies = new Dictionary<ResourceType, int>()
    {
        { ResourceType.Wood, 1 },
        { ResourceType.Grain, 1 },
        { ResourceType.Stone, 1 }
    };

    public void LoadPreviousData()
{
    savePath = Application.persistentDataPath + "/economyData.json";

    if (File.Exists(savePath))
    {        
        LoadEconomy(); // Load saved data if it exists
    }
    else
    {
        int[] l={deltaWood,deltaGrain,deltaStone};
        SaveCurrency(l);
        // If no saved data, initialize with default values from Inspector

        LoadEconomy();

        // LocalResourcesCurrencies[ResourceType.Wood] = deltaWood;
        // LocalResourcesCurrencies[ResourceType.Grain] = deltaGrain;
        // LocalResourcesCurrencies[ResourceType.Stone] = deltaStone;
        
        // SaveEconomy(); // Save initial values to prevent data loss
    }

    UpdateUICounter();
}


    private void SaveCurrency(int[] resources){
         EconomyData data = new EconomyData
        {
            wood = resources[0],
            grain = resources[1],
            stone = resources[2],
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Economy Saved: " + json);
    }

    private void UpdateUICounter()
    {
        woodsCounter.text = "W: " + NumberFormatter.FormatNumber(LocalResourcesCurrencies[ResourceType.Wood]);
        grainCounter.text = "G: " + NumberFormatter.FormatNumber(LocalResourcesCurrencies[ResourceType.Grain]);
        stoneCounter.text = "S: " + NumberFormatter.FormatNumber(LocalResourcesCurrencies[ResourceType.Stone]);
    }

    public static class NumberFormatter
    {
        public static string FormatNumber(float num)
        {
            if (num >= 1_000_000_000) return (num / 1_000_000_000f).ToString("0.##") + "B";
            else if (num >= 1_000_000) return (num / 1_000_000f).ToString("0.##") + "M";
            else if (num >= 1_000) return (num / 1_000f).ToString("0.#") + "K";
            else return num.ToString("0");
        }
    }

    public void AddResource(ResourceType type, int amount)
    {

        // int [] wood,grain,stone;

        LoadEconomy();

        LocalResourcesCurrencies[type] += amount;
        SaveEconomy();
        UpdateUICounter();
    }

    public void CollectMinedResource(int[] resource)
    {
        LoadEconomy();
        LocalResourcesCurrencies[ResourceType.Wood] += resource[0];
        LocalResourcesCurrencies[ResourceType.Grain] += resource[1];
        LocalResourcesCurrencies[ResourceType.Stone] += resource[2];

        Debug.Log($"Resources Collected: W={resource[0]}, G={resource[1]}, S={resource[2]}");
        UpdateUICounter();
        SaveEconomy();
    }
    public void CollectRewardsResource(int[] resource)
    {
        LoadEconomy();
        LocalResourcesCurrencies[ResourceType.Wood] += resource[0];
        LocalResourcesCurrencies[ResourceType.Grain] += resource[1];
        LocalResourcesCurrencies[ResourceType.Stone] += resource[2];

        Debug.Log($"Reward Collected: W={resource[0]}, G={resource[1]}, S={resource[2]}");
        UpdateUICounter();
        SaveEconomy();
    }

    public void CollectingAllresourceAmount(ResourceType resourceTypeToCollect)
    {
        Farm[] allFarms = FindObjectsOfType<Farm>();
        foreach (Farm farm in allFarms)
        {
            if (farm.resourceType == resourceTypeToCollect)
            {
                int collected = farm.ReturnResourceAmount();
                AddResource(farm.resourceType, collected);
            }
        }
        UpdateUICounter();
        SaveEconomy();
    }

    public int[] ReturnAllResources()
    {
        LoadEconomy();
        return new int[]{LocalResourcesCurrencies[ResourceType.Wood],LocalResourcesCurrencies[ResourceType.Grain]
        ,LocalResourcesCurrencies[ResourceType.Stone]};
    }

    public void SpendBuildingCost(int woodCost, int grainCost, int stoneCost)
    {
        LoadEconomy();
        LocalResourcesCurrencies[ResourceType.Wood] -= woodCost;
        LocalResourcesCurrencies[ResourceType.Grain] -= grainCost;
        LocalResourcesCurrencies[ResourceType.Stone] -= stoneCost;

        UpdateUICounter();
        SaveEconomy();
    }

    // 💾 Saving & Loading Economy Data

    private void SaveEconomy()
    {
        EconomyData data = new EconomyData
        {
            stone = LocalResourcesCurrencies[ResourceType.Stone],
            grain = LocalResourcesCurrencies[ResourceType.Grain],
            wood = LocalResourcesCurrencies[ResourceType.Wood]
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Economy Saved: " + json);
    }

    private void LoadEconomy()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            if (!string.IsNullOrWhiteSpace(json))
            {
            EconomyData data = JsonUtility.FromJson<EconomyData>(json);

            LocalResourcesCurrencies[ResourceType.Stone] = data.stone;
            LocalResourcesCurrencies[ResourceType.Grain] = data.grain;
            LocalResourcesCurrencies[ResourceType.Wood] = data.wood;

            // Debug.Log($"Economy Loaded: Stone={data.stone}, Grain={data.grain}, Wood={data.wood}");
            }
            else
            {
                Debug.Log("Economy save file is empty.");
            }
        }
        else
        {
            Debug.Log("No economy save file found, using default values.");
        }
    }

    // void OnApplicationQuit()
    // {
    //     SaveEconomy(); // Save data when quitting the game
    // }
}

[System.Serializable]
public class EconomyData
{
    public int stone;
    public int grain;
    public int wood;
}

// Resource Type Enum
public enum ResourceType
{
    Wood,
    Grain,
    Stone
}
