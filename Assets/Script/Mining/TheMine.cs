using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMine : MonoBehaviour
{
     public enum MineType
    {
        wood,
        grain,
        stone
    }

    // Public variable to select from dropdown in the Inspector
    public MineType mineType;
    public int level;
    
    public int InitialAmount;
    public int currentResource;

    private MinesStats minesStats;
    
   
    public void InitializeMineStats(int Level){//this will be called by MinesManager after spawning to init the stats 
    //to it.        
        level = Level;
        // Debug.Log(mineType.ToString());

        minesStats = GameObject.Find("MinesStats").GetComponent<MinesStats>();
        if(minesStats==null){           
            Debug.Log("can't find minesStats.");
        }  

        // Fetch the initial amount of resources based on mine type and level
        InitialAmount = minesStats.GetResourceValue(mineType.ToString(), level-1);
        currentResource = InitialAmount;

        Debug.Log(InitialAmount);
    }
    public void reduceResource(int Amount){//mining rate
        //this will be called by TheUnit every second mining
        currentResource=currentResource-Amount;

        if(currentResource<=0){
           //when resources reaches zero despawn. 
           Destroy(gameObject);
        }
    }
    public int returnResource(){
        //this will be called by when clicked on mine
        return currentResource;
    }

}
