using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsKinematicObjectMomentum : MonoBehaviour {

    public Vector3 currentPosition = Vector3.zero;
    public Vector3 lastFramePosition = Vector3.zero;
    public Vector3 Velocity = Vector3.zero;

    public Transform parent;

    public float veloFrameDiffMag = 0;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        veloFrameDiffMag = Mathf.Abs((parent.position - lastFramePosition).magnitude);

        Velocity = (parent.position - lastFramePosition).normalized;

        lastFramePosition = parent.position;
    }
}
