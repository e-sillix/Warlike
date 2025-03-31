using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class RewardManager : MonoBehaviour
{

    [SerializeField] private GameObject RewardPanel;
    [SerializeField]private TextMeshProUGUI Rewards;

    [SerializeField]private CurrencyManager currencyManager;
    public void GiveReward(int[] Resources){
        Rewards.text="Gained Nothing";
        RewardPanel.SetActive(true);
        // if(Resources[0]&&Resources[1]&&Resources[2])
        Rewards.text="Wood:"+Resources[0]+",Grain:"+Resources[1]+",Stone:"+Resources[2];
        currencyManager.CollectRewardsResource(Resources);
    }
}
