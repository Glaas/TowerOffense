using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public EnemyDetection shooter;
    public Transform target;
    [Range(0.0f, 1.0f)]
    public float speed = 100f;

    public GameObject explosionPrefab;
    void OnEnable()
    {
        DetectShooter();
        Destroy(gameObject,3);
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
        if (target == null) return;

        //transform.position += (target.transform.position - transform.position) * .1f;
        transform.Translate(Vector3.Normalize(target.position - transform.position) * speed);

        if (Vector3.Distance(target.transform.position, transform.position) < .2f)
        {
            Instantiate(explosionPrefab, target.transform.position, Quaternion.identity);
            DebugDisplay.enemiesKilled +=1;
            Destroy(target.gameObject);
            Destroy(gameObject);
            //TODO make a proper destroy function that has consequences
        }
    }

}
