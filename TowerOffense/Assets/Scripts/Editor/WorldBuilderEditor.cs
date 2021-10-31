using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldBuilder))]
public class WorldBuilderEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     var t = (target as WorldBuilder);

    //     base.OnInspectorGUI();

    //     GUILayout.BeginHorizontal();

    //     if (GUILayout.Button("Create world"))
    //     {
    //         t.GenerateGrid();
    //     }
    //     GUILayout.EndHorizontal();
    // }
}
