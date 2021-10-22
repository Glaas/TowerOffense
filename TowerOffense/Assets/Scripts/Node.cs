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
            transform.DOMoveY(transform.localPosition.y + 3f, .4f);
            selected = true;
        }
        else
        {
            transform.DOMoveY(transform.localPosition.y + -3f, .4f);
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
                    posArr = PlayGrid.HorizontalLine(pos);
                    foreach (var node in posArr) playgrid.gridObject[node.Item1, node.Item2].GetComponent<Node>().outline.enabled = isOutlined;
                    break;
                case Selection.PATTERNS.VERTICAL_LINE:
                    posArr = PlayGrid.VerticalLine(pos);
                    foreach (var node in posArr) playgrid.gridObject[node.Item1, node.Item2].GetComponent<Node>().outline.enabled = isOutlined;
                    break;
                case Selection.PATTERNS.CROSS:
                    posArr = PlayGrid.Cross(pos);
                    foreach (var node in posArr) playgrid.gridObject[node.Item1, node.Item2].GetComponent<Node>().outline.enabled = isOutlined;
                    break;
                case Selection.PATTERNS.SQUARE:
                    posArr = PlayGrid.Square(pos);
                    foreach (var node in posArr) playgrid.gridObject[node.Item1, node.Item2].GetComponent<Node>().outline.enabled = isOutlined;
                    break;
                default: throw new NotImplementedException();
            }
        }
    }
}
