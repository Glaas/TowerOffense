using UnityEngine;
using System.Collections.Generic;

public class PlayGrid : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject[,] gridObject;
    public static int worldWidth = 10;
    public static int worldHeight = 10;

    public static (int, int) North((int, int) x) { return ClampToGrid((x.Item1, x.Item2 + 1)); }
    public static (int, int) East((int, int) x) { return ClampToGrid((x.Item1 + 1, x.Item2)); }
    public static (int, int) South((int, int) x) { return ClampToGrid((x.Item1, x.Item2 - 1)); }
    public static (int, int) West((int, int) x) { return ClampToGrid((x.Item1 - 1, x.Item2)); }

    public static (int, int) NorthEast((int, int) x) { return ClampToGrid((x.Item1 + 1, x.Item2 + 1)); }
    public static (int, int) SouthEast((int, int) x) { return ClampToGrid((x.Item1 + 1, x.Item2 - 1)); }
    public static (int, int) SouthWest((int, int) x) { return ClampToGrid((x.Item1 - 1, x.Item2 - 1)); }
    public static (int, int) NorthWest((int, int) x) { return ClampToGrid((x.Item1 - 1, x.Item2 + 1)); }

    public static (int, int)[] HorizontalLine((int, int) x)
    {
        List<(int, int)> result = new List<(int, int)>();
        result.Clear();
        result.Add(x);
        result.Add(West(x));
        result.Add(East(x));
        return result.ToArray();
    }
    public static (int, int)[] VerticalLine((int, int) x)
    {
        List<(int, int)> result = new List<(int, int)>();
        result.Clear();
        result.Add(x);
        result.Add(North(x));
        result.Add(South(x));
        return result.ToArray();
    }

    public static (int, int)[] Cross((int, int) x)
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
    public static (int, int)[] Square((int, int) x)
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

    static (int, int) ClampToGrid((int, int) x)
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

}
