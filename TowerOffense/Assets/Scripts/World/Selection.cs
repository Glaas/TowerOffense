using System.Net.Sockets;
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
        public enum PATTERNS { HORIZONTAL_LINE = 1, VERTICAL_LINE = 2, CROSS = 3, SQUARE = 4 }
        public enum TOOL_TYPE { BUILD_WALLS, PLACE_BUILDINGS }
        public enum CURSOR_STATE { NEUTRAL, BUILDING }

        public SELECT_MODE selectMode = SELECT_MODE.SINGLE;
        public PATTERNS pattern = PATTERNS.CROSS;
        public TOOL_TYPE toolType = TOOL_TYPE.BUILD_WALLS;
        public int i = 1;

        public CURSOR_STATE cursorState = CURSOR_STATE.NEUTRAL;
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

                switch (cursorState)
                {
                    case CURSOR_STATE.NEUTRAL: //When in Cursor neutral, this is what happens when hovering over an object
                        break;
                    case CURSOR_STATE.BUILDING: //When in Cursor buildings, this is what happens when hovering over an object
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
                        break;
                    default: throw new NotImplementedException();
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

        public void CycleCursorState()
        {
            if (cursorState == CURSOR_STATE.NEUTRAL)
            {
                cursorState = CURSOR_STATE.BUILDING;
                Cursor.SetCursor(selectCursorTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                cursorState = CURSOR_STATE.NEUTRAL;
                Cursor.SetCursor(normalCursorTexture, Vector2.zero, CursorMode.Auto);
            }
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
            if (Input.GetMouseButtonDown(0))
            {
                switch (cursorState)
                {
                    case CURSOR_STATE.NEUTRAL:
                        break;
                    case CURSOR_STATE.BUILDING:
                        switch (selectMode)
                        {
                            case SELECT_MODE.SINGLE:
                                if (nodeSelected == null) return;
                                if (!nodeSelected.isSelected) nodeSelected.OnSelect();
                                GlobalDataHandler.instance.SubtractMoney(FindObjectOfType<UiHandler>().costOfNextAction);
                                UiHandler.instance.UpdateCoins();
                                UiHandler.instance.SetInfo("Structure was built successfully");
                                GameObject.Find("PlaceTurretSFX").GetComponent<AudioSource>().Play();
                                ExitPlacingTurret();
                                break;
                            case SELECT_MODE.MULTIPLE:
                                throw new NotImplementedException();

                            default: throw new NotImplementedException();
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

        public void EnterPlacingTurret()
        {
            cursorState = CURSOR_STATE.BUILDING;
            Cursor.SetCursor(selectCursorTexture, Vector2.zero, CursorMode.Auto);

            toolType = TOOL_TYPE.PLACE_BUILDINGS;
        }

        public void ExitPlacingTurret()
        {
            cursorState = CURSOR_STATE.NEUTRAL;
            Cursor.SetCursor(normalCursorTexture, Vector2.zero, CursorMode.Auto);
            ResetOutlines();

        }
    }
}