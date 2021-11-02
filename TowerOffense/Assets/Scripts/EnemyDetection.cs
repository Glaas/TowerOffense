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
        Debug.Log("Current target is " + currentTarget);

    }
    private void Update()
    {
        if (currentTarget == null) return;
    }

    void Shoot(GameObject realTarget)
    {

        PlayShootAnim();
        var b = GameObject.Instantiate(bulletPrefab, transform.position + (Vector3.up * 4.5f), Quaternion.identity);
        float c = (Vector3.Distance(realTarget.transform.position, transform.position) / b.GetComponent<BulletBehavior>().speed) / 2;
        Destroy(b, c);
        b.GetComponent<BulletBehavior>().InitTarget(realTarget.transform);

    }

    [Button("PlayShootAnim)")]
    void PlayShootAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger("Shoot");
        GetComponentInChildren<ParticleSystem>().Play();
    }

}