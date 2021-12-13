using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class TurretAttack : MonoBehaviour
{
    List<Collider> enemiesDetected = new List<Collider>();
    public GameObject currentTarget;

    private Transform towerTransform;

    public GameObject bulletPrefab;
    public GameObject destructionPrefab;
    public float detectionRadius = 8f;

    public float bulletSpeed = 5;
    public float delayBetweenShots = 1;

    public int enemiesKilled = 0;

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
        if (chosenEnemy != null && shortestDistance <= detectionRadius)
        {
            currentTarget = chosenEnemy.gameObject;
            DOTween.Kill(this);
            if (GetComponentInChildren<Light>()) GetComponentInChildren<Light>().DOColor(Color.red, .4f); //null check to make DoTween happy
        }
        else
        {
            DOTween.Kill(this);
            currentTarget = null;
            if (GetComponentInChildren<Light>()) GetComponentInChildren<Light>().DOColor(Color.white, .4f);

        }
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
        GetComponent<AudioSource>().Play();
        GameObject bulletInstantiated = GameObject.Instantiate(bulletPrefab, transform.position + (Vector3.up * 4.5f), Quaternion.identity);
        Destroy(bulletInstantiated, 1);
        if (bulletInstantiated == null) yield break;
        BulletBehavior bulletBehaviour = bulletInstantiated.GetComponent<BulletBehavior>();
        bulletBehaviour.InitTarget(currentTarget);
        yield return new WaitUntil(() => bulletBehaviour.distanceToTarget <= 1.2f);
        if (bulletInstantiated == null) yield break;
        GetComponent<BuildingStats>().TakeDamage(1);
        var explosionInstance = Instantiate(destructionPrefab, bulletInstantiated.transform.position, Quaternion.identity);
        Destroy(explosionInstance.gameObject, 4f);
        Destroy(bulletInstantiated);
        currentTarget.GetComponent<EnemyStats>().TakeDamage(1);

        GlobalDataHandler.instance.enemiesKilled++;
        enemiesKilled++;

    }

    [Button("PlayShootAnim)")]
    void PlayShootAnim()
    {
        GetComponentInChildren<Animator>().SetTrigger("Shoot");
        GetComponentInChildren<ParticleSystem>().Play();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

    }

}