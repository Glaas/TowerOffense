using System.Collections;
using System;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public enum SELECT_MODE { SINGLE, MULTIPLE }
    public enum PATTERNS { CROSS = 1, HORIZONTAL_LINE = 2, VERTICAL_LINE = 3 };
    public SELECT_MODE selectMode = SELECT_MODE.SINGLE;
    public PATTERNS pattern = PATTERNS.CROSS;
    public Node nodeSelected;
    public int i = 1;
    public PlayGrid playGrid;

    private void Awake()
    {
        playGrid = FindObjectOfType<PlayGrid>();
    }
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
}
