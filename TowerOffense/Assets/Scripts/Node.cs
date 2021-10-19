using System;
using UnityEngine;
public class Node : MonoBehaviour
{
    public (int, int) pos;
    public string coordinates;

    private void Awake()
    {
        char[] separators = new char[] { ',', ' ' };
        string[] nb = gameObject.name.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        pos = (Int32.Parse(nb[0]), Int32.Parse(nb[1]));
        coordinates = pos.ToString();
    }
}
