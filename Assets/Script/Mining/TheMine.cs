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
    
    public int InitialAmount, currentResource=10;

    private MinesStats minesStats;
    private MineSpawner mineSpawner;    
    private bool occupied;

    public void MineDependency(MineSpawner MineSpawner){
        mineSpawner=MineSpawner;
    }
   
    public void InitializeMineStats(int Level){//this will be called by MinesManager after
    //  spawning to init the stats 
    //to it.        
        level = Level;
        // Debug.Log(mineType.ToString());

        minesStats = GameObject.Find("MinesStats").GetComponent<MinesStats>();
        if(minesStats==null){           
            Debug.Log("can't find minesStats.");
        }  
        Debug.Log("Called by minemanager");

        // Fetch the initial amount of resources based on mine type and level
        InitialAmount = minesStats.GetResourceValue(mineType.ToString(), level-1);
        currentResource = InitialAmount;

        Debug.Log(InitialAmount);
    }
    public void DeductResources(int Amount){//mining rate
        //this will be called by TheUnit every second mining
        currentResource=currentResource-Amount;

        if(currentResource<=0){
           //when resources reaches zero despawn.
           MineEmpty(); 
        }
        Debug.Log("Resource deducted:"+Amount);
    }
    // public int returnResource(){
    //     //this will be called by when clicked on mine
    //     return currentResource;
    // }

    public int ReturnResources(){
        return currentResource;
    }
    public bool IsMineOccupied(){
        return occupied;
    }
    public void setMineStatus(bool status){
        occupied=status;
    }
    void MineEmpty(){
        mineSpawner.AMineIsFinsihed(mineType.ToString());
        Destroy(gameObject);
    }

}
