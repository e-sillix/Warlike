using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class AttackStatPayload
{
   public int[] damage=new int[5],health=new int[5];
   public float[] armor=new float[5];     //all levels damage
   

    public AttackStatPayload(int[] Damage,int [] Health,float[] Armor)
    {
        damage=Damage;
        health=Health;
        armor=Armor;
    }
}
