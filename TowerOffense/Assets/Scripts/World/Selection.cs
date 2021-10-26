using System.Collections;
using System;
using UnityEngine;
using EPOOutline;

public class Selection : MonoBehaviour
{
    public enum SELECT_MODE { SINGLE, MULTIPLE }
    public enum PATTERNS { HORIZONTAL_LINE = 1, VERTICAL_LINE = 2, CROSS = 3, SQUARE = 4 };
    public SELECT_MODE selectMode = SELECT_MODE.SINGLE;
    public PATTERNS pattern = PATTERNS.CROSS;
    public int i = 1;

    public void CycleModes(SELECT_MODE _selectMode) => selectMode = _selectMode == SELECT_MODE.SINGLE ? SELECT_MODE.MULTIPLE : SELECT_MODE.SINGLE;

    public void CyclePatterns()
    {
        var values = Enum.GetValues(typeof(Selection.PATTERNS));
        i++;
        pattern = (Selection.PATTERNS)i;
        if (i >= values.Length)
        {
            i = 0;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftControl))
        {
            CyclePatterns();
            ResetOutlines();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            CycleModes(selectMode);
            ResetOutlines();

        }

    }
    void ResetOutlines()
    {
        foreach (var outline in FindObjectsOfType<Outlinable>())
        {
            outline.enabled = false;
        }
    }
}
