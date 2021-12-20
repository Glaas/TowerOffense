using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatBob : MonoBehaviour
{
    public Transform headTransform;
    public Transform hatTransform;

    public Vector3 headTransformPosition;

    private void Start()
    {
        headTransform = GameObject.Find("mixamorig:HeadTop_End").GetComponent<Transform>();
        hatTransform = GameObject.Find("SM_christmashat").GetComponent<Transform>();

        headTransformPosition = headTransform.position;
    }

    private void Update()
    {
        //hatTransform.position = headTransform.position;
        
        //hatTransform.position = new Vector3(headTransform.position.x-1.2f, headTransform.position.y+1.4f, headTransform.position.z+0.55f);
        hatTransform.position = new Vector3(headTransform.position.x-0.9f, headTransform.position.y+1.4f, headTransform.position.z-1.2f);

        //hatTransform.position = new Vector3(44.6f, 6.7f,  19.8f);

        //x 44.67426
        //y 6.705082
        //z 19.88749
    }
}
