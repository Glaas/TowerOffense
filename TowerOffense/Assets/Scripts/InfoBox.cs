using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

public class InfoBox : MonoBehaviour
{
    public enum SELECT_MODE
    {
        SINGLE, MULTIPLE
    }

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
    }

    void Update()
    {
        HighlightTarget();

    }
    public void SelectNode()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //  target = currentTarget.GetComponent<Node>();
            //  //If non selected, select. If selected, un-select.
            //  if (currentTarget.GetComponent<Node>().selected == false) currentTarget.GetComponent<Node>().OnSelect();
            // else if (currentTarget.GetComponent<Node>().selected == true) currentTarget.GetComponent<Node>().OnDeselect();
        }
    }
    void RegisterTarget()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.GetComponent<Node>())
            {
                return;
            }
            currentTarget = hit.transform.gameObject;
        }
        else currentTarget = null;
    }

    void FixedUpdate()
    {
        RegisterTarget();
    }
    void HighlightTarget()
    {
        if (currentTarget == null || !currentTarget.GetComponent<Outlinable>()) return; //Don't highlight if target is non-existent or non-highlightable

        currentTarget.gameObject.GetComponent<Outlinable>().enabled = true; //Outline current selection

        if (oldTarget == null) oldTarget = currentTarget; //Error catcher to prevent nullRef

        if (currentTarget != oldTarget) oldTarget.gameObject.GetComponent<Outlinable>().enabled = false; //Using delta to UN-highlight previous selection

        oldTarget = currentTarget;//Giving the delta enough information to work with
    }
}
