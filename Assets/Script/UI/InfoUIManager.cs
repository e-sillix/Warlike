using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class InfoUIManager : MonoBehaviour
{
    [SerializeField] private GameObject CreepUI, MineUI;   
    [SerializeField] private TextMeshProUGUI mineTypeText,mineLevelText,mineResourceText,
    creepTypeText,creepLevelText,creepRewardText;


    public void MineInfoClicked(int level,string mineType,int currentResource){
        MineUI.SetActive(true);     
        mineLevelText.text="Level: "+level;
        mineTypeText.text="Mine Type: "+mineType;
        mineResourceText.text="Current Resource: "+currentResource;   
    } 
    public void CreepInfoClicked(int level,string barrackType,string[] Rewards){
        CreepUI.SetActive(true);
        creepLevelText.text="Level: "+level;
        creepTypeText.text="Barrack Type: "+barrackType;
        string rewardText="Rewards: ";
        for (int i = 0; i < Rewards.Length; i++)
        {
            rewardText+=Rewards[i]+" ";
        }
        creepRewardText.text=rewardText;
    }
}
