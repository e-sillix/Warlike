using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsCountManager : MonoBehaviour
{//this will store troop count for all types and manage it.


   private int[] cavalry=new int[5],infantry=new int[5],archer=new int[5],mage=new int[5];
    void Start(){
        cavalry = new int[] { 0,0,0,0,0 };
        infantry = new int[] { 1, 2, 3, 4, 5 };  // Similarly for other arrays
        archer = new int[] { 3,4,2, 62 ,2 };
        mage = new int[] { 0,0,0,0,0};
    }
    
   public void UpdateTroopsCount(string barrackType,int[] troopsData){
      //this will be called by barrack for now.
      //when training is done
      // Make sure that troopsData length matches the size of your arrays (e.g., 5)
    if (troopsData.Length != cavalry.Length)
    {
        Debug.LogError("Mismatch in troop data length!");
        return;
    }

    // Check for the barrack type and update the corresponding array
    if (barrackType == "Cavalry")
    {
        for (int i = 0; i < cavalry.Length; i++)
        {
            cavalry[i] += troopsData[i];  // Add corresponding troop data to cavalry array
        }
        for(int i=0;i<5;i++){
      Debug.Log(cavalry[i]);
   }
    }
    else if (barrackType == "Infantry")
    {
        for (int i = 0; i < infantry.Length; i++)
        {
            infantry[i] += troopsData[i];  // Add corresponding troop data to infantry array
        }
         for(int i=0;i<5;i++){
      Debug.Log(infantry[i]);
   }
    }
    else
    {
        Debug.LogError("Unknown barrack type in countmanager: " + barrackType);
    }

    Debug.Log(barrackType + " troops updated.");
   
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
   
   public void WithDrawTroops(){
    //depleting troops for marching from base.or injured 

   }
   public void ReturnTroops(){
    //getting back troops from march or hospital

   }
}
