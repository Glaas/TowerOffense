using UnityEngine;
using System.Collections.Generic;

public static class GridHelpers
{
    public static (int, int) West((int, int) x) { return (x.Item1 - 1, x.Item1); }
    public static (int, int) East((int, int) x) { return (x.Item1 + 1, x.Item1); }
    public static (int, int) North((int, int) x) { return (x.Item1, x.Item1 + 1); }
    public static (int, int) South((int, int) x) { return (x.Item1, x.Item1 - 1); }
    public static (int, int)[] Cross((int, int) x)
    {
        List<(int, int)> result = new List<(int, int)>();
        result.Clear();
        result.Add(x);
        result.Add(GridHelpers.North(x));
        result.Add(GridHelpers.South(x));
        result.Add(GridHelpers.West(x));
        result.Add(GridHelpers.East(x));

        return result.ToArray();
    }
    public static (int, int)[] HorizontalLine((int, int) x)
    {
        List<(int, int)> result = new List<(int, int)>();
        result.Clear();
        result.Add(x);
        result.Add(GridHelpers.West(x));
        result.Add(GridHelpers.East(x));
        return result.ToArray();
    } 
    public static (int, int)[] VerticalLine((int, int) x)
    {
        List<(int, int)> result = new List<(int, int)>();
        result.Clear();
        result.Add(x);
        result.Add(GridHelpers.North(x));
        result.Add(GridHelpers.South(x));
        return result.ToArray();
    }
}
