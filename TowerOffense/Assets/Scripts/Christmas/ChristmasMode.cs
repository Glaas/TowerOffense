using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasMode : MonoBehaviour
{
    private Camera sceneCamera;
    
    private void Start()
    {
        sceneCamera = FindObjectOfType<Camera>(); //todo Camera.main
        
        //christmas layer invisible to main camera
        sceneCamera.cullingMask = sceneCamera.cullingMask =~ (1 << 12);
    }

    public void ToggleChristmasMode(bool xmasmode) //todo this should take the bool from the spreadsheet
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
            print("there is no christmas mode.");
        }
    }
}
