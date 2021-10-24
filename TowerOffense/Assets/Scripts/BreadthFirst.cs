using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirst : MonoBehaviour
{
    private PlayGrid grid;
    public Vector2 startPoint = new Vector2(5, 5);
    public (int,int) startPointTuple;

    public Queue frontier;
    public List<(int,int)> reached;

    private void Start()
    {
        grid = new PlayGrid(GetComponent<WorldBuilder>().worldWidth, GetComponent<WorldBuilder>().worldHeight);
        startPointTuple = ((int)startPoint.x, (int)startPoint.y);
        frontier.Enqueue(startPoint);
        reached.Add(startPointTuple);


    }
}
