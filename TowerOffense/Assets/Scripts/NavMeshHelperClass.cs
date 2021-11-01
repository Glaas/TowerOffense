using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class NavMeshHelperClass : MonoBehaviour
{
    NavMeshSurface navMesh;


    private void Start()
    {
        navMesh = GetComponent<NavMeshSurface>();
    }
    [Button("Build")]
    void BuildNavMesh()
    {
        navMesh.BuildNavMesh();
    }
    [Button("Clear")]
    void ClearNavMesh()
    {

        navMesh.RemoveData();
    }
}
