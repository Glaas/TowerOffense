using UnityEngine;
using System.Collections.Generic;

public class PlayGrid
{

    public PlayGrid(int _worldWidth, int _worldHeight)
    {
        worldWidth = _worldWidth;
        worldHeight = _worldHeight;
        nodeGrid = new GameObject[worldWidth, worldHeight];

    }

    public GameObject[,] nodeGrid;
    public Edge[] edges;
    public int worldWidth;
    public int worldHeight;

    public (int, int) North((int, int) x) { return ClampToGrid((x.Item1, x.Item2 + 1)); }
    public (int, int) East((int, int) x) { return ClampToGrid((x.Item1 + 1, x.Item2)); }
    public (int, int) South((int, int) x) { return ClampToGrid((x.Item1, x.Item2 - 1)); }
    public (int, int) West((int, int) x) { return ClampToGrid((x.Item1 - 1, x.Item2)); }
    public (int, int) NorthEast((int, int) x) { return ClampToGrid((x.Item1 + 1, x.Item2 + 1)); }
    public (int, int) SouthEast((int, int) x) { return ClampToGrid((x.Item1 + 1, x.Item2 - 1)); }
    public (int, int) SouthWest((int, int) x) { return ClampToGrid((x.Item1 - 1, x.Item2 - 1)); }
    public (int, int) NorthWest((int, int) x) { return ClampToGrid((x.Item1 - 1, x.Item2 + 1)); }

    //TODO on grid generation, check if all neighbours exist or are outside of the grid, and if they exist, create an edge at this location

    public (int, int)[] HorizontalLine((int, int) x)
    {
        List<(int, int)> result = new List<(int, int)>();
        result.Clear();
        result.Add(x);
        result.Add(West(x));
        result.Add(East(x));
        return result.ToArray();
    }
    public (int, int)[] VerticalLine((int, int) x)
    {
        List<(int, int)> result = new List<(int, int)>();
        result.Clear();
        result.Add(x);
        result.Add(North(x));
        result.Add(South(x));
        return result.ToArray();
    }

    public (int, int)[] Cross((int, int) x)
    {
        List<(int, int)> result = new List<(int, int)>();
        result.Clear();
        result.Add(x);
        result.Add(North(x));
        result.Add(South(x));
        result.Add(West(x));
        result.Add(East(x));

        return result.ToArray();
    }
    public (int, int)[] Square((int, int) x)
    {
        List<(int, int)> result = new List<(int, int)>();
        result.Clear();
        result.Add(x);
        result.Add(North(x));
        result.Add(NorthEast(x));
        result.Add(East(x));
        result.Add(SouthEast(x));
        result.Add(South(x));
        result.Add(SouthWest(x));
        result.Add(West(x));
        result.Add(NorthWest(x));

        return result.ToArray();
    }

    (int, int) ClampToGrid((int, int) x)
    {
        (int, int) returnedValue = (0, 0);
        if (x.Item1 < 0)
        {
            returnedValue.Item1 = 0;
        }
        else if (x.Item1 > worldWidth - 1)
        {
            returnedValue.Item1 = worldWidth - 1;
        }
        else returnedValue.Item1 = x.Item1;
        if (x.Item2 < 0)
        {
            returnedValue.Item2 = 0;
        }
        else if (x.Item2 > worldHeight - 1)
        {
            returnedValue.Item2 = worldHeight - 1;
        }
        else returnedValue.Item2 = x.Item2;
        return returnedValue;
    }

    bool CheckIfValid((int, int) x) { if (x.Item1 < 0 || x.Item2 < 0 || x.Item1 > worldWidth - 1 || x.Item2 > worldHeight - 1) return false; else return true; }


    // static bool AverageCells(((int, int), (int, int)) x)
    // {
    //
    // }
    //TODO generate the link between two cells
}

public struct Edge
{
    ((int, int), (int, int)) position;
    // bool isWalkable = true;
    int weight;
}

