using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUI : MonoBehaviour
{
    //shooting bar should go up when turrets despawn -> talk to turret script
    //shooting bar value (0 - 10) should be shown in GUI Label
    //button should only be pressable once the bar reaches 10

    public GlobalDataHandler globalDataHandler;
    public ManualShooting manualShooting;

    private void Start()
    {
        globalDataHandler = FindObjectOfType<GlobalDataHandler>();
        manualShooting = FindObjectOfType<ManualShooting>();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 50), "Turrets destroyed: " + globalDataHandler.turretsDestroyed.ToString());

        if (globalDataHandler.turretsDestroyed >= 10)
        {
            if (GUI.Button(new Rect(10, 30, 150, 100), "Activate Targeting"))
            {
                print("activating targeting");
                manualShooting.TargetingMode();
                print("Targeting activated!");
            }
        }
    }
}
