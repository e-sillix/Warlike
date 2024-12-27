using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingStat", menuName = "Building/Building Stat SO")]
public class BuildingStatSO : ScriptableObject
{
    public string buildingName;

    // Costs for upgrading from level 1 to 30
    public int[] Capacity=new int[30],Rate = new int[30];  

    //might add prefab for each 5 level gap.
   
}

