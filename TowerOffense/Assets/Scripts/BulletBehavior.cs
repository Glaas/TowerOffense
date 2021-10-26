using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public EnemyDetection shooter;
    public Transform target;
    [Range(0.0f, 1.0f)]
    public float speed = 100f;
    void OnEnable()
    {
        DetectShooter();
        target = FindObjectOfType<Controller>().transform;
    }
    void DetectShooter()
    {
        EnemyDetection oneRandomGun = FindObjectOfType<EnemyDetection>(); //To avoid nullRefs
        EnemyDetection[] allGuns = FindObjectsOfType<EnemyDetection>();
        foreach (var gun in allGuns)
        {
            if (Vector3.Distance(transform.position, gun.transform.position) < Vector3.Distance(transform.position, oneRandomGun.transform.position))
            {
                shooter = gun;
            }
        }
    }
    private void Update()
    {
        transform.position += (target.transform.position - transform.position) * .5f;
    }
    void DetectTarget()
    {
        //target = shooter.currentTarget.transform;
    }
}
