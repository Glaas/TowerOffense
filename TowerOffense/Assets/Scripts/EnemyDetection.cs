using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public List<Collider> enemiesDetected = new List<Collider>();
    public GameObject currentTarget;
    private Transform towerTransform;
    public GameObject enemyClosestToTower;

    public GameObject bulletPrefab;
    public float detectionRadius = 8f;

    public float bulletSpeed = 5;

    private void Awake()
    {
        enemiesDetected = new List<Collider>();
    }
    private void Start()
    {
        towerTransform = GameObject.Find("Tower").transform;
        StartCoroutine(nameof(Shoot));

    }


    public List<Collider> enemyDetectedcols;
    void OnTriggerEnter(Collider other)
    {
        enemiesDetected = new List<Collider>();
        foreach (var item in Physics.OverlapSphere(transform.position, detectionRadius))
        {
            if (item.GetComponent<Controller>())
            {
                enemiesDetected.Add(item);

            }
        }
        ComputeCurrentTarget();
    }
    private void OnTriggerExit(Collider other)
    {
        if (enemiesDetected.Count == 0)
        {
            enemyClosestToTower = null;
            currentTarget = null;
        }
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
    }

    IEnumerator Shoot()
    {
        if (currentTarget == null)
        {
        }
        else
        {
            var b = GameObject.Instantiate(bulletPrefab, transform.position + (Vector3.up * 4.5f), Quaternion.identity);
            b.GetComponent<BulletBehavior>().target = currentTarget.transform;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(nameof(Shoot));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
