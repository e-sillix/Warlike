using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class AttackStatPayload
{
   public int[] damage=new int[5],health=new int[5],moveSpeed=new int[5]; //all levels damage
   public float[] armor=new float[5],attackRange=new float[5];     //all levels damage
   

    public AttackStatPayload(int[] Damage,int [] Health,float[] Armor,int[] MoveSpeed,float[] AttackRange)
    {
        damage=Damage;
        health=Health;
        armor=Armor;
        moveSpeed=MoveSpeed;
        attackRange=AttackRange;
    }
}
