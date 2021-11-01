using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using EPOOutline;

public class Selection : MonoBehaviour
{
    public enum SELECT_MODE { SINGLE, MULTIPLE }
    public enum PATTERNS { HORIZONTAL_LINE = 1, VERTICAL_LINE = 2, CROSS = 3, SQUARE = 4 };
    public SELECT_MODE selectMode = SELECT_MODE.SINGLE;
    public PATTERNS pattern = PATTERNS.CROSS;
    public int i = 1;

    private Camera cam;
    public GameObject oldTarget;
    public GameObject currentTarget;

    public PlayGrid playgrid;
    public Node nodeSelected;
    public HashSet<Node> nodesSelected;

    private void Start()
    {
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        playgrid = FindObjectOfType<WorldBuilder>().grid;
    }
    void FixedUpdate() => RegisterTarget();

    void RegisterTarget()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            currentTarget = hit.transform.gameObject;
            switch (selectMode)
            {
                case SELECT_MODE.SINGLE:
                    nodeSelected = ReturnTarget(currentTarget);
                    break;
                case SELECT_MODE.MULTIPLE:
                    nodesSelected = new HashSet<Node>();
                    nodesSelected = ReturnTargets(currentTarget);
                    break;
            }
        }
        else currentTarget = null;
    }
    Node ReturnTarget(GameObject objectHitByRay) { return objectHitByRay.GetComponentInChildren<Node>(); }
    HashSet<Node> ReturnTargets(GameObject objectHitByRay)
    {
        HashSet<Node> returnedArr = new HashSet<Node>();
        Node targetNodePos = objectHitByRay.GetComponentInChildren<Node>();
        switch (pattern)
        {
            case Selection.PATTERNS.HORIZONTAL_LINE:
                foreach (var node in playgrid.HorizontalLine(targetNodePos.pos)) returnedArr.Add(playgrid.nodeGrid[node.Item1, node.Item2].GetComponentInChildren<Node>()); break;
            case Selection.PATTERNS.VERTICAL_LINE:
                foreach (var node in playgrid.VerticalLine(targetNodePos.pos)) returnedArr.Add(playgrid.nodeGrid[node.Item1, node.Item2].GetComponentInChildren<Node>()); break;
            case Selection.PATTERNS.CROSS:
                foreach (var node in playgrid.Cross(targetNodePos.pos)) returnedArr.Add(playgrid.nodeGrid[node.Item1, node.Item2].GetComponentInChildren<Node>()); break;
            case Selection.PATTERNS.SQUARE:
                foreach (var node in playgrid.Square(targetNodePos.pos)) returnedArr.Add(playgrid.nodeGrid[node.Item1, node.Item2].GetComponentInChildren<Node>()); break;
            default: throw new NotImplementedException();
        }
        return returnedArr;
    }

    public void CycleModes(SELECT_MODE _selectMode) => selectMode = _selectMode == SELECT_MODE.SINGLE ? SELECT_MODE.MULTIPLE : SELECT_MODE.SINGLE;

    public void CyclePatterns()
    {
        var values = Enum.GetValues(typeof(Selection.PATTERNS));
        i++;
        pattern = (Selection.PATTERNS)i;
        if (i >= values.Length)
        {
            i = 0;
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
        {
            CyclePatterns();
            ResetOutlines();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CycleModes(selectMode);
            ResetOutlines();
        }
        if (Input.GetMouseButtonDown(0))
        {
            switch (selectMode)
            {
                case SELECT_MODE.SINGLE:
                    if (!nodeSelected.isSelected) nodeSelected.OnSelect();
                    else nodeSelected.OnDeselect();
                    break;
                case SELECT_MODE.MULTIPLE:
                    foreach (Node node in nodesSelected)
                    {
                        if (!node.isSelected) node.OnSelect();
                        else node.OnDeselect();
                    }
                    break;
                default: throw new NotImplementedException();

            }
        }
    }
    void ResetOutlines()
    {
        foreach (var outline in FindObjectsOfType<Outlinable>())
        {
            outline.enabled = false;
        }
    }

}
