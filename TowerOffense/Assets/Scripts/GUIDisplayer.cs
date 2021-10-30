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
}
