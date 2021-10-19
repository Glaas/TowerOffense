using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawn))]
public class SpawnEditor : Editor
{


    public override void OnInspectorGUI()
    {
        var t = (target as Spawn);

        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Create world"))
        {
            t.CreateWorld();
        }
        if (GUILayout.Button("Destroy world"))
        {
            t.DestroyWorld();
        }
        GUILayout.EndHorizontal();
    }
}
