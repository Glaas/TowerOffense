using System;
using UnityEngine;
using EPOOutline;
using DG.Tweening;

public class Node : MonoBehaviour
{
    public Outlinable outline;
    public WorldBuilder worldBuilder;
    public PlayGrid playgrid;
    public Selection selection;

    public (int, int) pos;
    public Vector3 posInWorld;
    public string coordinates;
    public bool selected = false;

    private void Awake()
    {
        posInWorld = transform.position;
        outline = GetComponent<Outlinable>();
        selection = FindObjectOfType<Selection>();
        worldBuilder = FindObjectOfType<WorldBuilder>();
        playgrid = worldBuilder.grid;
        outline.enabled = false;
        posInWorld = transform.position;
    }

    private void OnMouseOver()
    {
        Highlighting(true);
    }
    private void OnMouseExit()
    {
        Highlighting(false);
    }
    void OnMouseUp() => Select(selected);

    public void Select(bool isSelected)
    {
        if (!selected)
        {
            transform.DOMoveY(transform.localPosition.y + 1f, .2f);
            selected = true;
        }
        else
        {
            transform.DOMoveY(transform.localPosition.y + -1f, .2f);
            selected = false;
        }
    }
    void Highlighting(bool isOutlined)
    {
        if (selection.selectMode == Selection.SELECT_MODE.SINGLE)
        {
            outline.enabled = isOutlined;
        }
        else if (selection.selectMode == Selection.SELECT_MODE.MULTIPLE)
        {
            (int, int)[] posArr;
            switch (selection.pattern)
            {
                case Selection.PATTERNS.HORIZONTAL_LINE:
                    posArr = playgrid.HorizontalLine(pos);
                    foreach (var node in posArr) playgrid.nodeGrid[node.Item1, node.Item2].GetComponent<Node>().outline.enabled = isOutlined;
                    break;
                case Selection.PATTERNS.VERTICAL_LINE:
                    posArr = playgrid.VerticalLine(pos);
                    foreach (var node in posArr) playgrid.nodeGrid[node.Item1, node.Item2].GetComponent<Node>().outline.enabled = isOutlined;
                    break;
                case Selection.PATTERNS.CROSS:
                    posArr = playgrid.Cross(pos);
                    foreach (var node in posArr) playgrid.nodeGrid[node.Item1, node.Item2].GetComponent<Node>().outline.enabled = isOutlined;
                    break;
                case Selection.PATTERNS.SQUARE:
                    posArr = playgrid.Square(pos);
                    foreach (var node in posArr) playgrid.nodeGrid[node.Item1, node.Item2].GetComponent<Node>().outline.enabled = isOutlined;
                    break;
                default: throw new NotImplementedException();
            }
        }
    }
}
