using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasMode : MonoBehaviour
{
    private Camera sceneCamera;
    
    private void Start()
    {
        sceneCamera = FindObjectOfType<Camera>();
        
        //christmas layer invisible to main camera
        sceneCamera.cullingMask = sceneCamera.cullingMask =~ (1 << 12);
    }

    //todo find a good way to subscribe to the event of the 1/0 changing
    private void Update()
    {
        
    }

    public void ToggleChristmasMode(bool xmasmode)
    {
        if (xmasmode)
        {
            //turn christmas layer on
            sceneCamera.cullingMask = sceneCamera.cullingMask |= (1 << 12);
        }
        if (!xmasmode)
        {
            //turn christmas layer off
            sceneCamera.cullingMask = sceneCamera.cullingMask =~ (1 << 12);
        }
        else
        {
            print("the grinch has arrived. there is no christmas mode.");
        }
    }

    /*public void ToggleChristmasMode(bool xmasmode)
    {
        sceneCamera.cullingMask = sceneCamera.cullingMask ^ (1 << 12);
    }*/
}
