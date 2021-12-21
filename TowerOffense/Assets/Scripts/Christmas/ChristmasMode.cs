using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasMode : MonoBehaviour
{
    private Camera sceneCamera;

    public bool ShowChristmasMode = false;
    public bool showDebugChristmasMode = false;
    GlobalDataRetriever globalDataRetriever;

    public LayerMask defaultLayerMask;
    public LayerMask christmasLayerMask;

    private void Awake()
    {
        sceneCamera = Camera.main;
        globalDataRetriever = FindObjectOfType<GlobalDataRetriever>();
        ShowChristmasMode = globalDataRetriever.isChristmasModeOn;
    }
    private void Update()
    {
        if (ShowChristmasMode != globalDataRetriever.isChristmasModeOn)
        {
            ShowChristmasMode = globalDataRetriever.isChristmasModeOn;
            ToggleChristmasMode(ShowChristmasMode);
        }
    }

    void OnGUI()
    {
        if (showDebugChristmasMode)
        {
            if (GUI.Button(new Rect(10, 120, 150, 100), "Activate Xmas Mode"))
            {
                ToggleChristmasMode(true);

            }
            if (GUI.Button(new Rect(10, 220, 150, 100), "Deactivate Xmas Mode"))
            {
                ToggleChristmasMode(false);
            }

            if (GUI.Button(new Rect(10, 320, 150, 100), "print culling mask"))
            {
                Debug.Log("culling mask: " + sceneCamera.cullingMask);
            }
        }
    }

    public void ToggleChristmasMode(bool xmasmode) //todo this should take the bool from the spreadsheet
    {
        if (xmasmode) //DEPRECATED CODE, kept here to have a trace of some bitwise operations
        {
            //turn christmas layer on
            //sceneCamera.cullingMask = sceneCamera.cullingMask |= (1 << 12);
        }
        if (!xmasmode)
        {
            //turn christmas layer off
            //sceneCamera.cullingMask = sceneCamera.cullingMask = ~(1 << 12);
        }

        switch (xmasmode)
        {
            case true:
                print("christmas mode is on");
                sceneCamera.cullingMask = christmasLayerMask;

                break;
            case false:
                print("christmas mode is off");
                sceneCamera.cullingMask = defaultLayerMask;
                break;
            default:
                throw new NotImplementedException();
        }
    }
}