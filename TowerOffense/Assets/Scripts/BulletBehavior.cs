using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour //HACK this whole class is a hack
{
    public EnemyDetection shooter;
    public Transform target;
    [Range(0.0f, 1.0f)]
    public float speed = 1;
    public float distanceToTarget = float.MaxValue;

    public GameObject explosionPrefab;
    void Start()
    {
        shooter = DetectShooter();
        speed = shooter.bulletSpeed;
    }

    EnemyDetection DetectShooter()
    {
        EnemyDetection oneRandomGun = FindObjectOfType<EnemyDetection>(); //To avoid nullRefs
        EnemyDetection[] allGuns = FindObjectsOfType<EnemyDetection>();

        foreach (var gun in allGuns)
        {
            if (Vector3.Distance(transform.position, gun.transform.position) < Vector3.Distance(transform.position, oneRandomGun.transform.position))
            {
                oneRandomGun = gun;
            }
        }
        return oneRandomGun;
    }

    public void InitTarget(Transform target)
    {
        this.target = target.transform;
    }
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);      


        transform.Translate(Vector3.Normalize(target.position - transform.position) * speed * Time.deltaTime);

        if (Vector3.Distance(target.transform.position, transform.position) < .2f)
        {
            Instantiate(explosionPrefab, target.transform.position, Quaternion.identity);
            //TODO put this elsewhere DebugDisplay.enemiesKilled += 1;

            //TODO make a proper destroy function that has consequences
        }
    }

}
