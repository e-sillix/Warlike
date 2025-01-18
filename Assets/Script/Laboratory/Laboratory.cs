using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laboratory : MonoBehaviour
{
    public int level,researchRate;
    public string buildingName="Laboratory";
    public void ResearchIsClicked(){
        Debug.Log("Research is Clicked.");
    }
    public void UpgradeStats(int Level,int rate){
        level=Level;
        researchRate=rate; 
    }
}
