using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsIKSetInitPos : MonoBehaviour {


    Vector3 initialPivotPosition = Vector3.zero;
    Vector3 lastFramePosition = Vector3.zero;
    Vector3 originDirection = Vector3.zero;
    float originDirectionLength = 0;

    public Transform legstaticpivot;
    public Transform targetHitpointTransform;

    public static Vector3 desiredStandingPosition = Vector3.zero;
    public int setDesiredPositionPerFrame = 1;

    public sccsRayIKFootPlacement SCCSRayIKFootPlacement;

    // Use this for initialization
    void Start ()
    {
        initialPivotPosition = this.transform.position;
        lastFramePosition = transform.position;
        originDirection = initialPivotPosition - legstaticpivot.position;
        originDirectionLength = originDirection.magnitude;
        originDirection.Normalize();
    }



    // Update is called once per frame
    void Update ()
    {
        desiredStandingPosition = legstaticpivot.position + (originDirection * originDirectionLength);

        if (setDesiredPositionPerFrame == 1)
        {
            this.transform.position = desiredStandingPosition;
        }
        else if (setDesiredPositionPerFrame == 0)
        {
            //this.transform.position = targetHitpointTransform.GetComponent<>;

            if (this.transform.GetComponent<sccsRayIKFootPlacement>() != null)
            {
                this.transform.position = this.transform.GetComponent<sccsRayIKFootPlacement>().raycasthit.Point;
            }
            else
            {
                this.transform.position = SCCSRayIKFootPlacement.raycasthit.Point; ;
            }
            //


        }




    }
}
