using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class billboardsprite : MonoBehaviour {

    public Camera MainCamera;


    void Start ()
    {
		
	}
	
	void Update ()
    {


        var camera = Camera.current;//.transform.rotation;//.WorldToViewportPoint;// SceneView.currentDrawingSceneView;
        if (camera != null)
        {
            //var target = new GameObject();
            //target.transform.position = NewCameraPosition;
            //target.transform.rotation = NewCameraRotation;
            //view.AlignViewToObject(target.transform);
            this.transform.rotation = camera.transform.rotation;



            //GameObject.DestroyImmediate(target);
        }


        //transform.LookAt(transform.position + MainCamera.transform.rotation * Vector3.forward,
        //MainCamera.transform.rotation * Vector3.up);
    }
}
