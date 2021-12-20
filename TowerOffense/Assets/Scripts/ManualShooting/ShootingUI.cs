using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUI : MonoBehaviour
{
    public GlobalDataHandler globalDataHandler;
    public ManualShooting manualShooting;

    public bool showShootingUI;

    private void Start()
    {
        globalDataHandler = FindObjectOfType<GlobalDataHandler>();
        manualShooting = FindObjectOfType<ManualShooting>();
    }

    void OnGUI()
    {
        if (showShootingUI)
        {
            GUI.Label(new Rect(10, 10, 300, 50), "Turrets destroyed: " + globalDataHandler.turretsDestroyed.ToString());

            //if (globalDataHandler.turretsDestroyed >= 10) //todo reinstate this line
            {
                if (GUI.Button(new Rect(10, 30, 150, 100), "Activate Targeting"))
                {
                    manualShooting.TargetingMode();
                }
            }
        }
    }
}
