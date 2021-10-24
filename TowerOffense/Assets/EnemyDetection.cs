using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public List<Collider> enemiesDetected = new List<Collider>();
    public GameObject currentTarget;
    private Transform towerTransform;
    public GameObject enemyClosestToTower;

    private void Awake()
    {
        enemiesDetected = new List<Collider>();
        towerTransform = GameObject.Find("Tower").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        enemiesDetected.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        enemiesDetected.Remove(other);
        if (other == currentTarget.GetComponent<Collider>())
        {
            currentTarget = null;
            enemyClosestToTower = null;
        }
    }
    private void Update()
    {
        ComputeCurrentTarget();
    }

    void ComputeCurrentTarget()
    {
        if (enemiesDetected.Count > 0)
        {
            if (enemyClosestToTower == null) enemyClosestToTower = enemiesDetected[0].gameObject;
            foreach (Collider enemy in enemiesDetected)
            {
                if (Vector3.Distance(enemy.transform.position, towerTransform.position) <= Vector3.Distance(enemyClosestToTower.transform.position, towerTransform.position))
                {
                    enemyClosestToTower = enemy.gameObject;
                    currentTarget = enemyClosestToTower;
                }
            }
        }
        else
        {
            enemyClosestToTower = null;
            currentTarget = null;
        }
    }


}
