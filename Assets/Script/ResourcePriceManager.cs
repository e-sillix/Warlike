using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePriceManager : MonoBehaviour
{
    [SerializeField] private int FarmPrice;


    public int ReturnFarmPrice(){
        return FarmPrice;
    }
}
