using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCreep : MonoBehaviour
{
      public enum enemyType
    {
        Infantry,
        Archer,
        Cavalry,
        Mage
    }

    // Public variable to select from dropdown in the Inspector
    public enemyType barrackType;
    public int level;

}
