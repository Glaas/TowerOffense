using System;
using UnityEngine;
using EPOOutline;
using DG.Tweening;
using UnityEngine.AI;
using System.Collections;

public class Node : MonoBehaviour
{
    public NavMeshSurface navmeshSurface;
    public Outlinable outline;
    public WorldBuilder worldBuilder;
    public PlayGrid playgrid;
    public Selection selection;

    // public bool isWalkable { set => StartCoroutine(nameof(RebuildNavMesh)); }

    public int gCost, hCost;
    public int fCost { get { return gCost + hCost; } }

    public (int, int) pos;
    public Vector3 posInWorld;
    public bool selected = false;

    Node(Vector3 _posInWorld)
    {
        posInWorld = _posInWorld;
    }

    private void Awake()
    {
        posInWorld = transform.position;
        outline = GetComponent<Outlinable>();
        selection = FindObjectOfType<Selection>();
        worldBuilder = FindObjectOfType<WorldBuilder>();
        navmeshSurface = GameObject.Find("NavMesh").GetComponent<NavMeshSurface>();
        playgrid = worldBuilder.grid;
        outline.enabled = false;
        posInWorld = transform.position;
        // isWalkable = true;
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
            transform.DOMoveY(transform.position.y + 2f, .2f);
            selected = true;
            gameObject.AddComponent<NavMeshObstacle>();
            GetComponent<NavMeshObstacle>().carving = true;
        }
        else
        {
            transform.DOMoveY(transform.position.y + -2f, .2f);
            selected = false;
            Destroy(gameObject.GetComponent<NavMeshObstacle>());
        }

        StartCoroutine(nameof(RebuildNavMesh));
    }
    IEnumerator RebuildNavMesh()
    {
        yield return new WaitForSeconds(0.7f);
        navmeshSurface.RemoveData();
        navmeshSurface.BuildNavMesh();

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
