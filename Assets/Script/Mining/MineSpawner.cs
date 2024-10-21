using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject woodMinePrefab,grainMinePrefab,stoneMinePrefab,spawnPoint;



    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.R)){
        SpawnMine();
     }   
    }
    void SpawnMine(){
        TheMine theMine=Instantiate(woodMinePrefab,spawnPoint.transform.position, Quaternion.identity).
        GetComponent<TheMine>();
        theMine.InitializeMineStats(2);
    }
}
