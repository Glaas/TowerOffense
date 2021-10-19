using System;
using UnityEngine;
using EPOOutline;

public class Node : MonoBehaviour
{
    public (int, int) pos;
    public string coordinates;
    public Outlinable outline;


    private void Awake()
    {
        char[] separators = new char[] { ',', ' ' };
        //string[] nb = gameObject.name.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //pos = (Int32.Parse(nb[0]), Int32.Parse(nb[1]));
        //coordinates = pos.ToString();

        outline = GetComponent<Outlinable>();
        outline.enabled = false;
    }
}
