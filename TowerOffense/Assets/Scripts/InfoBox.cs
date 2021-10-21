using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBox : MonoBehaviour
{
    public enum SELECT_MODE
    {
        SINGLE, MULTIPLE
    }

    public SELECT_MODE selectMode = SELECT_MODE.SINGLE;
    Camera cam;
    public GameObject oldTarget;
    public GameObject currentTarget;
    public Node nodeSelected;


    private void Awake()
    {
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        SelectNode(nodeSelected);
    }
    public void SelectNode(Node target)
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = currentTarget.GetComponent<Node>();

            if (currentTarget.GetComponent<Node>().selected == false)
            {
                currentTarget.GetComponent<Node>().OnSelect();
            }
            else if (currentTarget.GetComponent<Node>().selected == true)
            {
                currentTarget.GetComponent<Node>().OnDeselect();
            }
        }
    }

    void FixedUpdate()
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
            currentTarget.gameObject.GetComponent<Node>().outline.enabled = true;

            if (oldTarget == null)
            {
                oldTarget = currentTarget;
            }
            if (currentTarget != oldTarget)
            {
                oldTarget.gameObject.GetComponent<Node>().outline.enabled = false;
            }

            oldTarget = currentTarget;

            res = currentTarget.gameObject.name;

            // Do something with the object that was hit by the raycast.
        }
    }
    string res = string.Empty;
    private void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 500, 200), "Object : " + res);

        if (GUI.Button(new Rect(20, 100, 250, 40), "Change Selection Mode : " + selectMode))
        {
            selectMode = selectMode == SELECT_MODE.SINGLE ? SELECT_MODE.MULTIPLE : SELECT_MODE.SINGLE;


        }
    }
}
