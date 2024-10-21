using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesStats : MonoBehaviour
{
   [SerializeField] private int[] woodResourceWithLevel,grainResourceWithLevel,stoneResourceWithLevel;
   //five level at most for now.
   public int GetResourceValue(string mineType,int level){//this will be called when mine is spawned
      if(mineType=="wood"){        
         return woodResourceWithLevel[level];         
      }
      else if(mineType=="grain"){        
         return grainResourceWithLevel[level];         
      }
      else if(mineType=="stone"){        
         return stoneResourceWithLevel[level];         
      }
      else{
         Debug.Log("unknown mine resource asked for.");
         return 0;
      }
   }
}
