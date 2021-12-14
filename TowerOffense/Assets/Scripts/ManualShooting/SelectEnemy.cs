using System;
using System.Collections;
using System.Collections.Generic;
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
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform && CompareTag("Enemy")) //if something is hit 
                {
                    PrintName(hit.transform.gameObject);
                }
            }
        }
    }

    public void PrintName(GameObject go)
    {
        print("killing enemy");
        manualShooting.KillEnemy();
        Destroy(go);
    }
}