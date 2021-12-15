using System;
using System.Collections;
using System.Collections.Generic;
using SplineMesh;
using UnityEngine;

public class SelectEnemy : MonoBehaviour
{
    public Camera sceneCamera;
    public ManualShooting manualShooting;

    void Start()
    {
        sceneCamera = FindObjectOfType<Camera>();
        manualShooting = FindObjectOfType<ManualShooting>();
    }
    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && manualShooting.targetingMode)
            {
                RaycastHit hit;
                Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100.0f)) 
                {
                    if (hit.transform && hit.transform.CompareTag("Enemy")) //if something is hit
                    {
                        TargetEnemy(hit.transform.gameObject);
                        print($"raycast hit " + hit.transform.gameObject);
                    }
                }
            }
    }

    public void TargetEnemy(GameObject go)
    {
        KillEnemy();
        go.GetComponent<EnemyStats>().currentHealth -= 100;
        if (go.GetComponent<EnemyStats>().currentHealth < 0)
        {
            go.GetComponent<EnemyStats>().currentHealth = 0;
        }
        print($"enemy health is now " + go.GetComponent<EnemyStats>().currentHealth);
    }

    public void KillEnemy()
    {
        
        //GameObject.Find("CoinSFX").GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        //GameObject.Find("CoinSFX").GetComponent<AudioSource>().Play();
            
        // 500 points damage to enemy, which will undoubtedly kill it
        /*
        var obj = Instantiate(enemyShotEffect, transform.position, Quaternion.identity);
        Destroy(obj, 1.2f);

        Destroy(gameObject);
        */
    }
}