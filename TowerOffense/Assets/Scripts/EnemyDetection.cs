using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyDetection : MonoBehaviour
{
    public List<Collider> enemiesDetected = new List<Collider>();
    public GameObject currentTarget;

    private Transform towerTransform;

    public GameObject bulletPrefab;
    public GameObject destructionPrefab;
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
        InvokeRepeating("ComputeTarget", 1, .5f);
        InvokeRepeating("Shoot", 1, delayBetweenShots);
    }
    private void ComputeTarget()
    {
        var allEnemiesInScene = FindObjectsOfType<EnemyStats>();
        float shortestDistance = Mathf.Infinity;
        EnemyStats chosenEnemy = null;

        foreach (var enemy in allEnemiesInScene)
        {
            float distanceToEnemy = (Vector3.Distance(transform.position, enemy.transform.position));
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                chosenEnemy = enemy;
            }
        }
        if (chosenEnemy != null)
        {
            currentTarget = chosenEnemy.gameObject;
        }
        else currentTarget = null;
    }
    private void Update()
    {
        if (currentTarget == null) return;
    }

    void Shoot() => StartCoroutine(nameof(ShootCorout));

    IEnumerator ShootCorout()
    {
        if (currentTarget == null) yield break;

        PlayShootAnim();
        GameObject bulletInstantiated = GameObject.Instantiate(bulletPrefab, transform.position + (Vector3.up * 4.5f), Quaternion.identity);
        BulletBehavior bulletBehaviour = bulletInstantiated.GetComponent<BulletBehavior>();
        bulletBehaviour.InitTarget(currentTarget.transform);
        yield return new WaitUntil(() => bulletBehaviour.distanceToTarget <= 0.3f);
        var explosionInstance = Instantiate(destructionPrefab, bulletInstantiated.transform.position, Quaternion.identity);
        Destroy(explosionInstance.gameObject, 4f);
        Destroy(bulletInstantiated);
        Destroy(currentTarget);

        DebugDisplay.enemiesKilled++;

    }

    [Button("PlayShootAnim)")]
    void PlayShootAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger("Shoot");
        GetComponentInChildren<ParticleSystem>().Play();
    }

}