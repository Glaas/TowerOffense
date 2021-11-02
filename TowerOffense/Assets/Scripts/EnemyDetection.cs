using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyDetection : MonoBehaviour
{
    public List<Collider> enemiesDetected = new List<Collider>();
    public GameObject currentTarget;
    private Transform towerTransform;
    public GameObject enemyClosestToTower;

    public GameObject bulletPrefab;
    public float detectionRadius = 8f;

    public float bulletSpeed = 5;
    public float delayBetweenShots = 1;

    private void Awake()
    {
        enemiesDetected = new List<Collider>();
        GetComponent<SphereCollider>().radius = detectionRadius;
    }
    private void Start()
    {
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
        if (towerTransform == null) towerTransform = GameObject.Find("Tower").transform;

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
            PlayShootAnim();
            var b = GameObject.Instantiate(bulletPrefab, transform.position + (Vector3.up * 4.5f), Quaternion.identity);
            b.GetComponent<BulletBehavior>().InitTarget(currentTarget);
        }
        yield return new WaitForSeconds(delayBetweenShots);
        StartCoroutine(nameof(Shoot));
    }

    [Button("PlayShootAnim)")]
    void PlayShootAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger("Shoot");
        GetComponentInChildren<ParticleSystem>().Play();
    }

}
