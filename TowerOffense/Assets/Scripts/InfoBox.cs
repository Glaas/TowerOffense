using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

public class InfoBox : MonoBehaviour
{
    public enum SELECT_MODE { SINGLE, MULTIPLE }
    public enum PATTERNS { CROSS = 1, HORIZONTAL_LINE = 2, VERTICAL_LINE = 3 };
    public PATTERNS pattern = PATTERNS.CROSS;
    public int i = 1;
    public PlayGrid playGrid;
    private Camera cam;
    public SELECT_MODE selectMode = SELECT_MODE.SINGLE;
    public GameObject oldTarget;
    public GameObject currentTarget;
    public Node nodeSelected;
    public Node[] nodeGrpSelected;
    public string res = string.Empty;


    private void Awake()
    {
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        playGrid = FindObjectOfType<PlayGrid>();
    }

    void Update()
    {
        HighlightTarget();
        if (RegisterTarget())
        {
            SelectNode();
        }

    }
    void FixedUpdate()
    {
        RegisterTarget();
    }
    public void SelectNode()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Clicked");
            //  target = currentTarget.GetComponent<Node>();
            //  //If non selected, select. If selected, un-select.
            //  if (currentTarget.GetComponent<Node>().selected == false) currentTarget.GetComponent<Node>().OnSelect();
            // else if (currentTarget.GetComponent<Node>().selected == true) currentTarget.GetComponent<Node>().OnDeselect();
        }
    }
    bool RegisterTarget()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.GetComponent<Node>())
            {
                return false;
            }
            currentTarget = hit.transform.gameObject;

            return true;
        }
        else currentTarget = null;
        return false;
    }


    void HighlightTarget()
    {
        if (currentTarget != null) nodeSelected = currentTarget.GetComponent<Node>();
        if (selectMode == SELECT_MODE.SINGLE)
        {
            //Highlight(currentTarget);
        }
        else if (selectMode == SELECT_MODE.MULTIPLE)
        {
            if (currentTarget == null) return;
            switch (pattern)
            {
                case PATTERNS.CROSS:

                    var cArr = GridHelpers.WestAndCenterAndEast(nodeSelected);
                    foreach ((int, int) c in cArr)
                    {

                        //Highlight(playGrid.gridObject[c.Item1, c.Item2]);
                    }
                    break;
                case PATTERNS.HORIZONTAL_LINE:
                    break;
                case PATTERNS.VERTICAL_LINE:
                    break;
                default:
                    throw new NotImplementedException();
            }

        }
    }
    void Highlight(GameObject target, bool isMultple = false)
    {
        if (target == null || !target.GetComponent<Outlinable>())
        {
            if (oldTarget != null)
            {
                oldTarget.gameObject.GetComponent<Outlinable>().enabled = false;
                oldTarget = null;
            }
            return; //Don't highlight if target is non-existent or non-highlightable
        }

        target.gameObject.GetComponent<Outlinable>().enabled = true; //Outline current selection

        if (oldTarget == null) oldTarget = target; //Error catcher to prevent nullRef

        if (target != oldTarget) oldTarget.gameObject.GetComponent<Outlinable>().enabled = false; //Using delta to UN-highlight previous selection

        oldTarget = target;//Giving the delta enough information to work with
    }
    void Highlight(GameObject[] targets)
    {
      
    }
}
