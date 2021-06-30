using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsIKSetLockPos : MonoBehaviour {

    public Transform targetTransform;
    //public Transform legpivotstatic;

    //public RaycastHit raycasthit;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (targetTransform.GetComponent<sccsRayIKFootPlacement>()!= null)
        {
            var someTargetPos = targetTransform.GetComponent<sccsRayIKFootPlacement>().lastFrameHitPoint;

            transform.position = targetTransform.position + (targetTransform.up * 0.1f);

            /*if (targetTransform.GetComponent<sccsRayIKFootPlacement>().raycasthit != null)
            {
            
            }*/
        }
       
    }
}
