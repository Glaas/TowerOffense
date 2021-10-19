using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBox : MonoBehaviour
{
    Camera cam;
    public GameObject oldTarget;
    public GameObject currentTarget;

    private void Awake()
    {
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
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
        GUI.Label(new Rect(50, 100, 500, 200), "Object : " + res);
    }
}
