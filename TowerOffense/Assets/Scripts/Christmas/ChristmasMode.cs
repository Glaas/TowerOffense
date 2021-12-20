using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasMode : MonoBehaviour
{
    private Camera sceneCamera;
    public bool ShowChristmasMode = false;

    private void Start()
    {
        sceneCamera = FindObjectOfType<Camera>(); //todo Camera.main

        //christmas layer invisible to main camera
        sceneCamera.cullingMask = sceneCamera.cullingMask = ~(1 << 12);
    }
    private void Update()
    {
        if (ShowChristmasMode != FindObjectOfType<GlobalDataRetriever>().isChristmasModeOn)
        {
            ShowChristmasMode = FindObjectOfType<GlobalDataRetriever>().isChristmasModeOn;
            ToggleChristmasMode(ShowChristmasMode);
        }
    }

    void OnGUI()
    {
        if (ShowChristmasMode)
        {

            if (GUI.Button(new Rect(10, 120, 150, 100), "Activate Xmas Mode"))
            {
                //turn christmas layer on
                sceneCamera.cullingMask = sceneCamera.cullingMask |= (1 << 12);
            }
            if (GUI.Button(new Rect(10, 220, 150, 100), "Deactivate Xmas Mode"))
            {
                //turn christmas layer off
                sceneCamera.cullingMask = sceneCamera.cullingMask = ~(1 << 12);
            }
        }
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
            sceneCamera.cullingMask = sceneCamera.cullingMask = ~(1 << 12);
        }
        else
        {
            print("there is no christmas mode.");
        }
    }

}
