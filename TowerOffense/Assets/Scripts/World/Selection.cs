using System.Collections;
using System;
using UnityEngine;

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
}
