using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsIKRayLink : MonoBehaviour {

    public Transform targetTransform;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //targetTransform.position = this.transform.position;
        this.transform.position = targetTransform.position;



    }
}
