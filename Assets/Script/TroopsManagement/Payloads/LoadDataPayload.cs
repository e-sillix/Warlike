using UnityEngine;

[System.Serializable]
public class LoadDataPayload
{
    //training cost
    public int[] load=new int[5];     

    public LoadDataPayload(int[] Load)
    {
       load=Load;
    }
}