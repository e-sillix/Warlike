using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data.Common;

public class TowerInstance : MonoBehaviour
{
    [SerializeField]private GameObject TowerBoundary1,TowerBoundary2,
    TowerBoundary3,TowerBoundary4;

    [SerializeField] private GameObject Flag;
    [SerializeField]private GameObject UIComponent;

    [SerializeField]private GameObject UIButtonForAttack;

    [SerializeField]private float TotalHealth,RecoveringRate;

    [SerializeField]private Vector3 ScanningVolume;
    [SerializeField]private LayerMask unitLayer;
    private GameObject Boss;

    private float CurrentHealth;
    private String NameOfTheBoss;
    
    private int BossId=0;
    private bool isPlayerTheOwner;
    private Coroutine IndividualScanning;
    private int TowerId;

    public List<GameObject> towersToSave; // Drag and drop towers here

    private string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/towers.json";
    }

    void SaveTower()
{
    filePath = Application.persistentDataPath + "/towers.json";
    TowerDataList towerDataList = new TowerDataList();

    // 🔍 If file exists, load previous data
    if (File.Exists(filePath))
    {
        string existingJson = File.ReadAllText(filePath);
        towerDataList = JsonUtility.FromJson<TowerDataList>(existingJson);
    }


     // 🔢 Create a HashSet of all used towerIds
    HashSet<int> usedIds = new HashSet<int>();
    foreach (TowerData data in towerDataList.towers)
    {
        usedIds.Add(data.towerId);
    }

    // 🆔 Find the smallest unused towerId starting from 1
    int newTowerId = 1;
    while (usedIds.Contains(newTowerId))
    {
        newTowerId++;
    }

    // 🔁 Add this new tower with the newTowerId
    TowerId = newTowerId; // store this towerId in your class if needed later


    // ➕ Add this new tower
    towerDataList.towers.Add(new TowerData(transform.position, BossId, TowerId));

    // 💾 Save the updated list
    string json = JsonUtility.ToJson(towerDataList, true);
    File.WriteAllText(filePath, json);

    Debug.Log("✅ Tower saved to: " + filePath);
}

    public void PersistanceSpawning(int id,int towerId){
        BossId=id;
        TowerId=towerId;
        if(BossId!=0){
            isPlayerTheOwner=false;
            Boss[] allBosses = GameObject.FindObjectsOfType<Boss>();

            foreach (Boss boss in allBosses)
            {
                // Debug.Log("Found Boss: " + boss.gameObject.name);
                if(boss.GetComponent<Boss>().ReturnBossId()==BossId){
                    Boss=boss.gameObject;
                    AssignColor(boss.ReturnColor());
                    // Debug.Log("Color:"+boss.ReturnColor());
                    break;
                }
            }


        }
        else{
            isPlayerTheOwner=true;
        }

    }

    public int ReturnBossId(){
        return BossId;
    }
    public void EnemyTowerDependency(int bossId,Color newColor,GameObject boss){
        isPlayerTheOwner=false;
        BossId=bossId;
        AssignColor(newColor);
        Boss=boss;
        SaveTower();
        // IndividualScanning=StartCoroutine(ScanningTerritory());
    }
    public void SetOwnerShip(bool isOwner){
        Debug.Log("SetOwnerShip "+isOwner);
        isPlayerTheOwner=isOwner;
        SaveTower();
    }
    public bool IsTowerBelongToPlayer(){
        if(isPlayerTheOwner){

        Debug.Log("IsTowerBelongToPlayer "+isPlayerTheOwner);
        }
        return isPlayerTheOwner;
    }
    public void AssignColor(Color newColor)
{
    Renderer renderer1 = Flag.GetComponent<Renderer>();
    // Renderer renderer2 = TowerBoundary2.GetComponent<Renderer>();
    // Renderer renderer3 = TowerBoundary3.GetComponent<Renderer>();
    // Renderer renderer4 = TowerBoundary4.GetComponent<Renderer>();

    if (renderer1 != null) renderer1.material.SetColor("_Color", newColor);
    // if (renderer2 != null) renderer2.material.SetColor("_Color", newColor);
    // if (renderer3 != null) renderer3.material.SetColor("_Color", newColor);
    // if (renderer4 != null) renderer4.material.SetColor("_Color", newColor);
}


    public void RemoveFromTheTowerList(){
        //tower combat when defeated.

         // 📂 Remove this tower's entry from the saved file
    string filePath = Application.persistentDataPath + "/towers.json";

    if (File.Exists(filePath))
    {
        string existingJson = File.ReadAllText(filePath);
        TowerDataList towerDataList = JsonUtility.FromJson<TowerDataList>(existingJson);

        // 🧹 Remove tower with matching TowerId
        towerDataList.towers.RemoveAll(t => t.towerId == TowerId);

        // 💾 Save the updated list back to file
        string updatedJson = JsonUtility.ToJson(towerDataList, true);
        File.WriteAllText(filePath, updatedJson);

        Debug.Log("🗑️ Tower removed from file with TowerId: " + TowerId);
    }
    else
    {
        Debug.LogWarning("❌ No towers.json file found to remove tower from.");
    }

        // if(Boss!=null){
        //     Boss.GetComponent<EnemyWatchTowerSpawner>().RemoveTowerFromList(this.gameObject);
        // }
    }
    public void OnClickOnCollider(){
        //indirectly
        UIComponent.SetActive(true);
        if(!isPlayerTheOwner){
            UIButtonForAttack.SetActive(true);
        }
        else{
            UIButtonForAttack.SetActive(false);
        }
    }

    public void DeSelectTower(){
        UIComponent.SetActive(false);
    }
    public void AttackClicked(){
        GetComponent<TowerCombat>().AttackIsClick();;
    }
    public void InfoClicked(){

    }

   
}

[System.Serializable]
public class TowerData
{
    public float x, y, z;
    public int bossId;
    public int towerId; // New tower ID

    public TowerData(Vector3 pos, int id, int TowerId)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
        bossId = id;
        towerId = TowerId;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }
}


[System.Serializable]
public class TowerDataList
{
    public List<TowerData> towers = new List<TowerData>();
}