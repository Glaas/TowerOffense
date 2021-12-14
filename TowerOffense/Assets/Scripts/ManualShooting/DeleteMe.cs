using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMe : MonoBehaviour
{
    public Camera camera;
    public ManualShooting manualShooting;

    void Start()
    {
        camera = FindObjectOfType<Camera>();
        manualShooting = FindObjectOfType<ManualShooting>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

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
        manualShooting.KillEnemy();
        Destroy(go);
    }
}