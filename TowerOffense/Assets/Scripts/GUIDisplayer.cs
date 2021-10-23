using UnityEngine;
using System;


public class GUIDisplayer : MonoBehaviour
{
    public InfoBox infoBox;
    string sel = String.Empty;
    public Selection selectionClass;
    public WorldBuilder worldBuilder;

    void OnEnable()
    {
        worldBuilder = FindObjectOfType<WorldBuilder>();
        infoBox = FindObjectOfType<InfoBox>();
        selectionClass = FindObjectOfType<Selection>();
    }
    private void OnGUI()
    {
        if (infoBox.currentTarget == null) sel = "Nothing selected";
        else sel = infoBox.currentTarget.gameObject.name;

        GUI.Box(new Rect(0, 0, 330, 250), "Debug Controls :D Wow so pro");
        GUI.Label(new Rect(25, 25, 300, 50), "Object : " + sel);
        if (GUI.Button(new Rect(25, 50, 250, 50), "Generate world")) worldBuilder.GenerateGrid();
        if (GUI.Button(new Rect(25, 110, 250, 50), "Change Selection Mode : " + selectionClass.selectMode)) selectionClass.CycleModes(selectionClass.selectMode);
        if (selectionClass.selectMode == Selection.SELECT_MODE.MULTIPLE) if (GUI.Button(new Rect(25, 160, 300, 50), "Change Selection Mode : " + selectionClass.pattern)) selectionClass.CyclePatterns();
    }
}
