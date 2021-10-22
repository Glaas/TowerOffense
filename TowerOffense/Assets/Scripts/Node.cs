using System;
using UnityEngine;
using EPOOutline;
using DG.Tweening;

public class Node : MonoBehaviour
{
    public (int, int) pos;
    public string coordinates;
    public Outlinable outline;
    public PlayGrid playgrid;
    public Selection selection;
    public bool selected = false;

    private void Awake()
    {
        outline = GetComponent<Outlinable>();
        selection = FindObjectOfType<Selection>();
        playgrid = FindObjectOfType<PlayGrid>();
        outline.enabled = false;
    }

    public void Select(bool isSelected)
    {
        if (!selected)
        {
            transform.DOMoveY(transform.localPosition.y + 3f, .4f);
            selected = true;
        }
        else
        {
            transform.DOMoveY(transform.localPosition.y + -3f, .4f);
            selected = false;
        }
    }

    private void OnMouseOver()
    {
        if (selection.selectMode == Selection.SELECT_MODE.SINGLE)
        {
            outline.enabled = true;
        }
        else if (selection.selectMode == Selection.SELECT_MODE.MULTIPLE)
        {
            switch (selection.pattern)
            {
                case Selection.PATTERNS.CROSS:
                    var grp = GridHelpers.WestAndCenterAndEast(this);
                    foreach (var node in grp)
                    {
                        playgrid.gridObject[node.Item1, node.Item2].GetComponent<Node>().outline.enabled = true;
                    }
                    break;
                case Selection.PATTERNS.HORIZONTAL_LINE:
                    break;
                default:
                    break;
            }

        }
    }//TODO group those two monstrosities in one to avoid code duplication
    //TODO fix the outOfBounds error when highlighting something on the edges
    //TODO Implement the rest of the patterns
    private void OnMouseExit()
    {
        if (selection.selectMode == Selection.SELECT_MODE.SINGLE)
        {
            outline.enabled = false;
        }
        else if (selection.selectMode == Selection.SELECT_MODE.MULTIPLE)
        {
            switch (selection.pattern)
            {
                case Selection.PATTERNS.CROSS:
                    var grp = GridHelpers.WestAndCenterAndEast(this);
                    foreach (var node in grp)
                    {
                        playgrid.gridObject[node.Item1, node.Item2].GetComponent<Node>().outline.enabled = false;
                    }
                    break;
                case Selection.PATTERNS.HORIZONTAL_LINE:
                    break;
                default:
                    break;
            }

        }
    }
    void OnMouseUp() => Select(selected);
}
