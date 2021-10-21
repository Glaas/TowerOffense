using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIDisplayer : MonoBehaviour
{
    public InfoBox infoBox;
    public Spawn spawn;
    enum PATTERNS { CROSS = 1, HORIZONTAL_LINE = 2, VERTICAL_LINE = 3 };
    PATTERNS pattern = PATTERNS.CROSS;
    int i = 1;


    void OnEnable()
    {
        infoBox = FindObjectOfType<InfoBox>();
        spawn = FindObjectOfType<Spawn>();

    }

    private void OnGUI()
    {
        if (infoBox.currentTarget == null)
        {
            infoBox.res = "Nothing selected";
        }
        else infoBox.res = infoBox.currentTarget.gameObject.name;

        GUI.Box(new Rect(0, 0, 330, 250), "Debug Controls :D Wow so pro");
        GUI.Label(new Rect(25, 25, 300, 50), "Object : " + infoBox.res);
        if (GUI.Button(new Rect(25, 50, 250, 50), "Generate world"))
        {
            spawn.CreateWorld();
        }
        if (GUI.Button(new Rect(25, 110, 250, 50), "Change Selection Mode : " + infoBox.selectMode))
        {
            infoBox.selectMode = infoBox.selectMode == InfoBox.SELECT_MODE.SINGLE ? InfoBox.SELECT_MODE.MULTIPLE : InfoBox.SELECT_MODE.SINGLE;
        }
        if (infoBox.selectMode == InfoBox.SELECT_MODE.MULTIPLE)
        {
            var values = Enum.GetValues(typeof(PATTERNS));

            if (GUI.Button(new Rect(25, 160, 300, 50), "Change Selection Mode : " + pattern))
            {
                i++;
                pattern = (PATTERNS)i;
                if (i >= values.Length)
                {
                    i = 0;
                }
                print(i);
            }

        }
    }
}
