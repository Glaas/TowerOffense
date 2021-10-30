using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{

    public GameObject gun;
    public Transform target;
    public EnemyDetection enemyDetection;
    // Start is called before the first frame update
    void Awake()
    {
        enemyDetection = GetComponentInChildren<EnemyDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyDetection.currentTarget != null)
        {
            target = enemyDetection.currentTarget.transform;
            gun.transform.LookAt(target);
        }
    }


}
