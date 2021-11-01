using System;
using UnityEngine;
using EPOOutline;
using DG.Tweening;
using UnityEngine.AI;
using System.Collections;
using TowerOffense;
public class Node : MonoBehaviour, ISelectable
{
    public Outlinable outline;
    public PlayGrid playgrid;
    public Selection selection;

    [ColorUsage(false, true)]
    public Color baseColor;
    [ColorUsage(false, true)]
    public Color offSetColor;

    public (int, int) pos;

    public bool isSelected { get; set; }

    private void Awake()
    {
        outline = GetComponent<Outlinable>();
        selection = FindObjectOfType<Selection>();
        playgrid = FindObjectOfType<WorldBuilder>().grid;
        baseColor = GetComponent<MeshRenderer>().material.GetColor("_Color");
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
    //void OnMouseUp() => Select(selected);

    public void OnSelect()
    {
        isSelected = true;
        transform.DOMoveY(transform.position.y + 3f, .2f);
        GetComponent<MeshRenderer>().material.DOColor(offSetColor, "_Color", .2f);

        gameObject.AddComponent<NavMeshObstacle>();
        GetComponent<NavMeshObstacle>().carving = true;
    }
    public void OnDeselect()
    {
        isSelected = false;
        transform.DOMoveY(transform.position.y + -3f, .2f);
        GetComponent<MeshRenderer>().material.DOColor(baseColor, "_Color", .2f);

        Destroy(gameObject.GetComponent<NavMeshObstacle>());
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
                    foreach (var node in posArr) playgrid.nodeGrid[node.Item1, node.Item2].GetComponentInChildren<Node>().outline.enabled = isOutlined;
                    break;
                case Selection.PATTERNS.VERTICAL_LINE:
                    posArr = playgrid.VerticalLine(pos);
                    foreach (var node in posArr) playgrid.nodeGrid[node.Item1, node.Item2].GetComponentInChildren<Node>().outline.enabled = isOutlined;
                    break;
                case Selection.PATTERNS.CROSS:
                    posArr = playgrid.Cross(pos);
                    foreach (var node in posArr) playgrid.nodeGrid[node.Item1, node.Item2].GetComponentInChildren<Node>().outline.enabled = isOutlined;
                    break;
                case Selection.PATTERNS.SQUARE:
                    posArr = playgrid.Square(pos);
                    foreach (var node in posArr) playgrid.nodeGrid[node.Item1, node.Item2].GetComponentInChildren<Node>().outline.enabled = isOutlined;
                    break;
                default: throw new NotImplementedException();
            }
        }
    }
}
