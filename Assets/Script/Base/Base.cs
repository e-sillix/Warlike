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
}
