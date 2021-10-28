using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   public GameObject enemyPrefab;
   public Transform enemyParent;
   public float spawnDelay;

   private void Awake() {
       enemyParent = GameObject.Find("--Enemies--").transform;
       StartCoroutine(nameof(EnemySpawn));

   }

   IEnumerator EnemySpawn(){
       var enemy = GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity,enemyParent);
       yield return new WaitForSeconds(spawnDelay);
       StartCoroutine(nameof(EnemySpawn));
   }
}
