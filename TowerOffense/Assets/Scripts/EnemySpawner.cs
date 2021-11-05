using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public ARPGFX.ARPGFXPortalScript portalManager;
    public float spawnDelay;
    public int enemiesToSpawn = 10;
    private void Awake()
    {
        portalManager = GetComponent<ARPGFX.ARPGFXPortalScript>();

    }
   

    public void EnemySpawn(GameObject enemyPrefab)
    {
        var enemy = GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
    }


}
