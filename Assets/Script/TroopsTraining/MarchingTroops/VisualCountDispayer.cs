using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCountDispayer : MonoBehaviour
{//attached to visual GO onside Army prefab for 
    [SerializeField] private GameObject soldierPrefab;  // The soldier prefab to instantiate
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(3, 0, 3);  // Size of the area in which soldiers can spawn
    public int SoldierCountRightNow;
    public int minSoldiers ;
    public int maxSoldiers ;

    // void Start(){
    //     DisplaySoldiers(SoldierCountRightNow);
    // }
    public void DisplaySoldiers(int soldierCount)
    {
        //probably called on creation of army and during combat

        // Clamp the soldier count to be between minSoldiers and maxSoldiers
        int soldiersToDisplay = Mathf.Clamp(soldierCount, minSoldiers, maxSoldiers);

        // First, clear the existing soldiers under the armyParent
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject); // Destroy existing soldier instances
        }

        // Now, instantiate new soldier prefabs under the armyParent
        for (int i = 0; i < soldiersToDisplay; i++)
        {
            GameObject soldier = Instantiate(soldierPrefab, gameObject.transform);

            // Optionally, position soldiers in a grid or specific formation
            soldier.transform.localPosition = GetSoldierPosition(i, soldiersToDisplay);
        }

        Debug.Log("Displayed " + soldiersToDisplay + " soldiers.");
    }

    private Vector3 GetSoldierPosition(int index, int totalSoldiers)
    {
        // Define a grid pattern for soldier placement
        int soldiersPerRow = Mathf.CeilToInt(Mathf.Sqrt(totalSoldiers));  // Number of soldiers in each row
        float spacingX = spawnAreaSize.x / soldiersPerRow;  // Horizontal spacing
        float spacingZ = spawnAreaSize.z / soldiersPerRow;  // Vertical spacing

        // Calculate the row and column based on the index
        int row = index / soldiersPerRow;
        int column = index % soldiersPerRow;

        // Calculate the position within the grid
        float xPosition = (column + 0.5f) * spacingX - spawnAreaSize.x / 2;
        float zPosition = (row + 0.5f) * spacingZ - spawnAreaSize.z / 2;

        return new Vector3(xPosition, 0, zPosition);  // y is 0 for ground level
    }
}
