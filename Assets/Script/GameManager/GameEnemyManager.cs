using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameEnemyManager : MonoBehaviour
{

   
    private string filePath;
    [SerializeField] private GameObject GameBeatenPanel;
    // [SerializeField]private GameObject EnemyRewardPanel;
    [SerializeField]private int Stone,Wood,Grain;
    [SerializeField]private RewardManager rewardManager;
    void Start()
    {
        filePath = Application.persistentDataPath + "/DEnemyKingDom.json";
        RemoveDEnemyState();
    }
    void RemoveDEnemyState(){
         if (!File.Exists(filePath))
    {
        Debug.LogWarning("❌ No Enemy save file found at: " + filePath);
        return;
    }else{
        string existingJson = File.ReadAllText(filePath);
        DefeatedEnemyData data = new DefeatedEnemyData();
        data = JsonUtility.FromJson<DefeatedEnemyData>(existingJson);
        Boss[] allBosses = FindObjectsOfType<Boss>();
        
        foreach (Boss boss in allBosses)
        {
            if (data.defeatedEnemyIDs.Contains(boss.ReturnBossId()))
            {
                // defeatedBosses.Add(boss);
                boss.gameObject.SetActive(false);
            }
        }

    }
    }
    public void AEnemyKingDomIsDefeated(int enemyID){
        Boss[] allBosses = FindObjectsOfType<Boss>();
        if(allBosses.Length==1){
            // Debug.Log("Game beaten.");
            BeatenTheGame();
            return;
        }
        rewardManager.GiveReward(new int[]{Wood,Grain,Stone});
        filePath = Application.persistentDataPath + "/DEnemyKingDom.json";
        DefeatedEnemyData data = new DefeatedEnemyData();
    //this to save on memory and give rewards to player.
     if (File.Exists(filePath))
    {
        string existingJson = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<DefeatedEnemyData>(existingJson);
    }
     // Add new ID if not already there
        if (!data.defeatedEnemyIDs.Contains(enemyID))
        {
            data.defeatedEnemyIDs.Add(enemyID);
            string newJson = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, newJson);
            Debug.Log("✅ Enemy ID added: " + enemyID);
        }
        else
        {
            Debug.Log("⚠️ Enemy ID already exists: " + enemyID);
        }
      

   }

   void BeatenTheGame(){
    GameBeatenPanel.SetActive(true);
   }
}
 [System.Serializable]
public class DefeatedEnemyData
{
    public List<int> defeatedEnemyIDs = new List<int>();
}
