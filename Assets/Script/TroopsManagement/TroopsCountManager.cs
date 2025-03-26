using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TroopsCountManager : MonoBehaviour
{//this will store troop count for all types and manage it.
   [SerializeField]private int[] cavalry=new int[5],infantry=new int[5],archer=new int[5],mage=new int[5];
   [SerializeField] private MessageManager messageManager;
   private string savePath;
    
    public void LoadPreviousTroopsData(){
        //by persistance manager
        savePath = Application.persistentDataPath + "/TroopsData.json";
        LoadTroopsData();
    }
    void LoadTroopsData(){
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            if (!string.IsNullOrWhiteSpace(json))
            {
                //  string json = File.ReadAllText(savePath);
            TroopData data = JsonUtility.FromJson<TroopData>(json);

            cavalry = data.cavalry;
            infantry = data.infantry;
            archer = data.archer;
            mage = data.mage;

            Debug.Log("Troop data loaded!");
            }
            else{
            Debug.Log("Troops file is empty.");
                
            }
        }
        else
        {
            SaveData();
            // Debug.Log("No saved Troops data found.");
        }
    }

    void SaveData(){
        TroopData data = new TroopData(cavalry, infantry, archer, mage);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText( Application.persistentDataPath + "/TroopsData.json", json);
        Debug.Log("Troop data saved!");
    }
   public void UpdateTroopsCount(string barrackType,int[] troopsData){
      //this will be called by barrack for now.
      //when training is done
      // Make sure that troopsData length matches the size of your arrays (e.g., 5)
    
    // Check for the barrack type and update the corresponding array
    // LoadTroopsData();
    if (barrackType == "Cavalry")
    {
        for (int i = 0; i < cavalry.Length; i++)
        {
            cavalry[i] += troopsData[i];  // Add corresponding troop data to cavalry array
        }
        
    }
    else if (barrackType == "Infantry")
    {
        for (int i = 0; i < infantry.Length; i++)
        {
            infantry[i] += troopsData[i];  // Add corresponding troop data to infantry array
        }
         
    }
    else if (barrackType == "Archer")
    {
        for (int i = 0; i < archer.Length; i++)
        {
            archer[i] += troopsData[i];  // Add corresponding troop data to infantry array
        }
         
   }
    else if (barrackType == "Mage")
    {
        for (int i = 0; i < mage.Length; i++)
        {
            mage[i] += troopsData[i];  // Add corresponding troop data to infantry array
        }
   }
    else
    {
        Debug.LogError("Unknown barrack type in countmanager: " + barrackType);
    }
    messageManager.MessageForTroopsAdded(barrackType, troopsData);
    // Debug.Log("Message should be displayed mentioning troops detailes which were added.");
   SaveData();
   }

   public int[] GetTroopsCount(string TroopsType){
    //for marching or ui purposes
    if (TroopsType == "Cavalry")
    {
        return cavalry;
    }
    else if (TroopsType == "Infantry")
    {
       return infantry;
    }
    else if (TroopsType == "Archer")
    {
       return archer;
    }
    else if (TroopsType == "Mage")
    {
       return mage;
    }
    else{
        Debug.Log("data not found in Count Mananger");
        return new int[5];
    }
    }
   
   public void WithDrawTroops(string barrackType,int[] troopsData){
    //called when march starting
    //depleting troops for marching from base.or injured 
     // Check for the barrack type and update the corresponding array
    if (barrackType == "Cavalry")
    {
        for (int i = 0; i < cavalry.Length; i++)
        {
            cavalry[i] -= troopsData[i];  // Add corresponding troop data to cavalry array
        }
        for(int i=0;i<5;i++){
   }
    }
    else if (barrackType == "Infantry")
    {
        for (int i = 0; i < infantry.Length; i++)
        {
            infantry[i] -= troopsData[i];  // Add corresponding troop data to infantry array
        }
         for(int i=0;i<5;i++){
   }
    }
    else if (barrackType == "Archer")
    {
        for (int i = 0; i < archer.Length; i++)
        {
            archer[i] -= troopsData[i];  // Add corresponding troop data to infantry array
        }
         for(int i=0;i<5;i++){
   }
   }
    else if (barrackType == "Mage")
    {
        for (int i = 0; i < mage.Length; i++)
        {
            mage[i] -= troopsData[i];  // Add corresponding troop data to infantry array
        }
         for(int i=0;i<5;i++){
   }
   }
    else
    {
        Debug.LogError("Unknown barrack type in countmanager: " + barrackType);
    }

   }
   public void ReturnTroops(string barrackType,int[] troopsData){
    //getting back troops from march or hospital
    if (barrackType == "Cavalry")
    {
        for (int i = 0; i < cavalry.Length; i++)
        {
            cavalry[i] += troopsData[i];  // Add corresponding troop data to cavalry array
        }
        for(int i=0;i<5;i++){
            Debug.Log("cav added=" +troopsData[i]);
   }
    }
    else if (barrackType == "Infantry")
    {
        for (int i = 0; i < infantry.Length; i++)
        {
            infantry[i] += troopsData[i];  // Add corresponding troop data to infantry array
        }
         for(int i=0;i<5;i++){
   }
    }
    else if (barrackType == "Archer")
    {
        for (int i = 0; i < archer.Length; i++)
        {
            archer[i] += troopsData[i];  // Add corresponding troop data to infantry array
        }
         for(int i=0;i<5;i++){
   }
   }
    else if (barrackType == "Mage")
    {
        for (int i = 0; i < mage.Length; i++)
        {
            mage[i] += troopsData[i];  // Add corresponding troop data to infantry array
        }
         for(int i=0;i<5;i++){
   }
   }
    else
    {
        Debug.LogError("Unknown barrack type in countmanager: " + barrackType);
    }
   }


   public int[][] ReturnAllTroops(){
    //for TroopsUI in profile

    return new int[][] { cavalry, infantry, archer, mage };
   }
}





[System.Serializable]
public class TroopData
{
    public int[] cavalry;
    public int[] infantry;
    public int[] archer;
    public int[] mage;

    public TroopData(int[] cavalry, int[] infantry, int[] archer, int[] mage)
    {
        this.cavalry = cavalry;
        this.infantry = infantry;
        this.archer = archer;
        this.mage = mage;
    }
}
