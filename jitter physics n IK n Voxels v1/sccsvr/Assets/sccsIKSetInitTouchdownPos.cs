using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsIKSetInitTouchdownPos : MonoBehaviour {


    Vector3 initialPivotPosition = Vector3.zero;
    Vector3 lastFramePosition = Vector3.zero;
    Vector3 lastFrameFootPosition = Vector3.zero;
    Vector3 lastFrameRaycastVisualPosition = Vector3.zero;


    Vector3 originDirection = Vector3.zero;
    float originDirectionLength = 0;

    public Transform legstaticpivot;
    public Transform RaycastHitVisualObject;

    public static Vector3 desiredStandingPosition = Vector3.zero;
    public int setDesiredPositionPerFrame = 1;

    public sccsRayIKFootPlacement SCCSRayIKFootPlacement;


    public int mainSwtcLadder = 0;

    public Transform footTarget;

    public Transform targetTwo;

    public Transform mainObject;

    // Use this for initialization
    void Start ()
    {
        initialPivotPosition = this.transform.position;
        lastFramePosition = transform.position;
        lastFrameFootPosition = footTarget.position;
        lastFrameRaycastVisualPosition = RaycastHitVisualObject.position;



        originDirection = initialPivotPosition - legstaticpivot.position;
        originDirectionLength = originDirection.magnitude;
        originDirection.Normalize();
    }



    // Update is called once per frame
    void Update ()
    {
        //desiredStandingPosition = legstaticpivot.position + (originDirection * originDirectionLength);

        if (setDesiredPositionPerFrame == 1)
        {

            desiredStandingPosition = legstaticpivot.position + (originDirection * originDirectionLength);
            //desiredStandingPosition = legstaticpivot.position + (originDirection * mainObject.GetComponent<sccsRayIKFootPlacement>().clampedDistance);

            if (mainSwtcLadder == 0)
            {
                this.transform.position = desiredStandingPosition;
            }

            if (mainSwtcLadder == 1)
            {
                this.transform.position = desiredStandingPosition;
            }

            /*if (RaycastHitVisualObject != null)
            {
                if (RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                {
                    if (RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
                    {
                        RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                    }
                }
            }

            if (footTarget != null)
            {
                if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                {
                    if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
                    {
                        footTarget.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                    }
                }
            }*/


            /*if (mainSwtcLadder == 1)
            {
                //footTarget.position = lastFrameFootPosition;
                //RaycastHitVisualObject.position = lastFrameRaycastVisualPosition;
                //lastFrameFootPosition = footTarget.position;
                //lastFrameRaycastVisualPosition = RaycastHitVisualObject.position;

                footTarget.position = targetTwo.position;
                RaycastHitVisualObject.position = targetTwo.position;

                lastFrameFootPosition = footTarget.position;
                lastFrameRaycastVisualPosition = RaycastHitVisualObject.position;
            }
            else
            {
                this.transform.position = lastFramePosition;
            }*/

            /*if (RaycastHitVisualObject != null)
            {
                if (RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                {
                    if (RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
                    {
                        RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                    }
                }
            }

            if (footTarget != null)
            {
                if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                {
                    if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
                    {
                        footTarget.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                    }
                }
            }*/
            lastFrameFootPosition = footTarget.position;
            lastFrameRaycastVisualPosition = RaycastHitVisualObject.position;
        }
        else if (setDesiredPositionPerFrame == 0)
        {
            //this.transform.position = targetHitpointTransform.GetComponent<>;

            //if (mainSwtcLadder == 1)
            {
                //this.transform.position = lastFramePosition;

                if (mainSwtcLadder == 1)
                {
                    /*footTarget.position = lastFrameFootPosition;
                    RaycastHitVisualObject.position = lastFrameRaycastVisualPosition;
                    lastFrameFootPosition = footTarget.position;
                    lastFrameRaycastVisualPosition = RaycastHitVisualObject.position;*/

                    footTarget.position = targetTwo.position;
                    RaycastHitVisualObject.position = targetTwo.position;

                    lastFrameFootPosition = footTarget.position;
                    lastFrameRaycastVisualPosition = RaycastHitVisualObject.position;
                }
                else
                {
                    //this.transform.position = lastFramePosition;
                }


                if (this.transform.GetComponent<sccsRayIKFootPlacement>() != null)
                {
                    if (this.transform.GetComponent<sccsRayIKFootPlacement>().raycasthit != null)
                    {
                        this.transform.position = this.transform.GetComponent<sccsRayIKFootPlacement>().chosenraycasthit.Point;
                    }

                   // this.transform.position = lastFramePosition;


                    /*if (RaycastHitVisualObject != null)
                    {
                        if (RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                        {
                            if (RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
                            {
                                RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                            }
                        }
                        RaycastHitVisualObject.transform.position = lastFramePosition;
                    }
      

                    if (footTarget != null)
                    {
                        if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                        {
                            if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
                            {
                                footTarget.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                            }
                        }
                        footTarget.transform.position = lastFramePosition;
                    }*/



                    /*if (footTarget != null)
                    {
                        footTarget.transform.position = lastFramePosition;// this.transform.position;
                    }*/



                    /*if (this.transform.GetComponent<sccsRayIKFootPlacement>().s != null)
                    {
                        this.transform.position = this.transform.GetComponent<sccsRayIKFootPlacement>().raycasthit.Point;
                    }*/
                }
                else
                {
                    if (SCCSRayIKFootPlacement.raycasthit != null)
                    {
                        this.transform.position = SCCSRayIKFootPlacement.raycasthit.Point;
                    }

                }
            }
            //


        }


        lastFramePosition = this.transform.position;

    }
}
