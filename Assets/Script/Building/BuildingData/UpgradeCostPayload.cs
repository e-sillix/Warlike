using UnityEngine;

[System.Serializable]
public class UpgradeCostPayload
{
    public int capacity, rate;
    public UpgradeCostPayload(int Capacity,int Rate)
    {
       capacity=Capacity;
       rate=Rate;
    }
}