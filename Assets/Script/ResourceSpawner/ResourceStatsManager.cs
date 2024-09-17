using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStatsManager : MonoBehaviour
{//Returns Stats of Resource for spawning.




    [SerializeField] private int farmPrice;
    [SerializeField] private int barrackPrice;
    
    public int ReturnFarmPrice(){
        return farmPrice;
    }
    public int ReturnBarrackPrice(){
        return barrackPrice;
    }

    
}


