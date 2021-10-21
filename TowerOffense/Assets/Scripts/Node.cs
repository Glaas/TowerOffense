using System;
using UnityEngine;
using EPOOutline;
using DG.Tweening;

public class Node : MonoBehaviour
{
    public (int, int) pos;
    public string coordinates;
    public Outlinable outline;
    public bool selected = false;

    private void Awake()
    {
        char[] separators = new char[] { ',', ' ' };
        //string[] nb = gameObject.name.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //pos = (Int32.Parse(nb[0]), Int32.Parse(nb[1]));
        //coordinates = pos.ToString();

        outline = GetComponent<Outlinable>();
        outline.enabled = false;
    }

    public void OnSelect()
    {
        selected = true;
        transform.DOMoveY(transform.localPosition.y + 3f, .4f);
    }
    public void OnDeselect()
    {
        selected = false;
        transform.DOMoveY(transform.localPosition.y + -3f, .4f);

    }
}
