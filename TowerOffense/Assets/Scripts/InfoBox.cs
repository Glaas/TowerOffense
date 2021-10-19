using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBox : MonoBehaviour
{
    Camera cam;
    public GameObject target;
    
    private void Awake() {
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.transform.gameObject;

            // Do something with the object that was hit by the raycast.
        }
    }
}
