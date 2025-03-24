using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int level ;
    public string buildingName="Base";

    public void UpgradeStats(int Level){
        level=Level;
    } 
    public void SetStats(int c){
        // level=Level;
        Debug.Log("Base updgrading does nothing.");
    }
    public void SettingPreviousData(int l){
        level =l;
        GetComponent<BuildingInstance>().SetData();
    }
}
