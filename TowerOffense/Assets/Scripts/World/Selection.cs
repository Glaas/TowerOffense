using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using EPOOutline;

namespace TowerOffense
{
    public class Selection : MonoBehaviour
    {
        public enum SELECT_MODE { SINGLE, MULTIPLE }
        public enum PATTERNS { HORIZONTAL_LINE = 1, VERTICAL_LINE = 2, CROSS = 3, SQUARE = 4 };
        public enum TOOL_TYPE { BUILD_WALLS, PLACE_BUILDINGS }
        public SELECT_MODE selectMode = SELECT_MODE.SINGLE;
        public PATTERNS pattern = PATTERNS.CROSS;
        public TOOL_TYPE toolType = TOOL_TYPE.BUILD_WALLS;
        public int i = 1;

        public Texture2D normalCursorTexture;
        public Texture2D selectCursorTexture;

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
                        HighlightTarget(currentTarget);
                        break;
                    case SELECT_MODE.MULTIPLE:
                        nodesSelected = new HashSet<Node>();
                        // nodesSelected = ReturnTargets(currentTarget); 
                        //FIXME this is temporarily disabled 
                        //bc i cannot find the source of the bug and I have more pressing shit to do
                        HighlightTargets(nodesSelected);
                        break;
                }
            }
            else
            {
                currentTarget = null;
                oldTarget = null;
                nodeSelected = null;
                nodesSelected = null;
                ResetOutlines();
            }

            if (oldTarget != currentTarget) ResetOutlines();
            oldTarget = currentTarget;
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
        public void CycleTool(TOOL_TYPE _toolType)
        {
            toolType = _toolType == TOOL_TYPE.PLACE_BUILDINGS ? TOOL_TYPE.BUILD_WALLS : TOOL_TYPE.PLACE_BUILDINGS;
            if (toolType == TOOL_TYPE.BUILD_WALLS)
            {
                Cursor.SetCursor(selectCursorTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(normalCursorTexture, Vector2.zero, CursorMode.Auto);
            }
           
        }

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
            if (Input.GetMouseButtonDown(1))
            {
                CycleTool(toolType);
                ResetOutlines();
                Debug.Log(toolType);
            }
            if (Input.GetMouseButtonDown(0))
            {
                // switch (toolType)
                // {
                //     case TOOL_TYPE.BUILD_WALLS:
                //         break;
                //     case TOOL_TYPE.PLACE_BUILDINGS:
                //         break;
                //     default: throw new NotImplementedException();
                // }

                switch (selectMode)
                {
                    case SELECT_MODE.SINGLE:
                        if (nodeSelected == null) return;
                        if (!nodeSelected.isSelected) nodeSelected.OnSelect();
                        else nodeSelected.OnDeselect();
                        break;
                    case SELECT_MODE.MULTIPLE:
                        if (nodesSelected == null) return;
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
        void HighlightTarget(GameObject target)
        {
            if (!target.GetComponent<Outlinable>())
            {
                Debug.Log(target.name + " is not outlinable");
                return;
            }
            var outlinable = target.GetComponent<Outlinable>();
            if (target.GetComponentInChildren<Node>())
            {
                if (target.GetComponentInChildren<Node>().canBeOutlined)
                {
                    nodeSelected.outline.enabled = true;
                }
            }
        }
        void HighlightTargets(HashSet<Node> targets)
        {
            foreach (var target in targets)
            {
                if (!target.GetComponentInChildren<Node>().canBeOutlined)
                {
                    continue;
                }
                target.GetComponentInChildren<Outlinable>().enabled = true;
            }
        }
        void ResetOutlines() { foreach (var target in FindObjectsOfType<Outlinable>()) target.enabled = false; }


    }
}