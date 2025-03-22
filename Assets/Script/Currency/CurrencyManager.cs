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
    
    private Dictionary<ResourceType, int> resourceCurrencies = new Dictionary<ResourceType, int>()
    {
        { ResourceType.Wood, 0 },
        { ResourceType.Grain, 0 },
        { ResourceType.Stone, 0 }
    };

    void Start()
{
    savePath = Application.persistentDataPath + "/economyData.json";

    if (File.Exists(savePath))
    {
        LoadEconomy(); // Load saved data if it exists
    }
    else
    {
        // If no saved data, initialize with default values from Inspector
        resourceCurrencies[ResourceType.Wood] = deltaWood;
        resourceCurrencies[ResourceType.Grain] = deltaGrain;
        resourceCurrencies[ResourceType.Stone] = deltaStone;
        
        SaveEconomy(); // Save initial values to prevent data loss
    }

    UpdateUICounter();
}


    private void UpdateUICounter()
    {
        woodsCounter.text = "W: " + NumberFormatter.FormatNumber(resourceCurrencies[ResourceType.Wood]);
        grainCounter.text = "G: " + NumberFormatter.FormatNumber(resourceCurrencies[ResourceType.Grain]);
        stoneCounter.text = "S: " + NumberFormatter.FormatNumber(resourceCurrencies[ResourceType.Stone]);
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
        resourceCurrencies[type] += amount;
        UpdateUICounter();
        SaveEconomy();
    }

    public void CollectMinedResource(int[] resource)
    {
        resourceCurrencies[ResourceType.Wood] += resource[0];
        resourceCurrencies[ResourceType.Grain] += resource[1];
        resourceCurrencies[ResourceType.Stone] += resource[2];

        Debug.Log($"Resources Collected: W={resource[0]}, G={resource[1]}, S={resource[2]}");
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

    public Dictionary<ResourceType, int> ReturnAllResources()
    {
        return new Dictionary<ResourceType, int>(resourceCurrencies);
    }

    public void SpendBuildingCost(int woodCost, int grainCost, int stoneCost)
    {
        resourceCurrencies[ResourceType.Wood] -= woodCost;
        resourceCurrencies[ResourceType.Grain] -= grainCost;
        resourceCurrencies[ResourceType.Stone] -= stoneCost;

        UpdateUICounter();
        SaveEconomy();
    }

    // 💾 Saving & Loading Economy Data

    private void SaveEconomy()
    {
        EconomyData data = new EconomyData
        {
            stone = resourceCurrencies[ResourceType.Stone],
            grain = resourceCurrencies[ResourceType.Grain],
            wood = resourceCurrencies[ResourceType.Wood]
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
            EconomyData data = JsonUtility.FromJson<EconomyData>(json);

            resourceCurrencies[ResourceType.Stone] = data.stone;
            resourceCurrencies[ResourceType.Grain] = data.grain;
            resourceCurrencies[ResourceType.Wood] = data.wood;

            Debug.Log($"Economy Loaded: Stone={data.stone}, Grain={data.grain}, Wood={data.wood}");
        }
        else
        {
            Debug.Log("No economy save file found, using default values.");
        }
    }

    void OnApplicationQuit()
    {
        SaveEconomy(); // Save data when quitting the game
    }
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
