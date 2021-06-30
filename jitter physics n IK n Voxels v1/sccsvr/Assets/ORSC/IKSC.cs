using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jitter;
using Jitter.Collision;
using Jitter.DataStructures;
using Jitter.Dynamics;
using Jitter.LinearMath;
using SCCoreSystems;

//[ExecuteInEditMode]
public class IKSC : MonoBehaviour
{

    int touchingground = 0;

    public float lastFrameHitRayLength = 0;
    public int swtcForIKLimbsType = 0;
    public int swtcForMovingTarget = 0;
    //0 == move target
    //1 == don't move target
    Ray rayPframe;
    Ray lastrayPframe;
    float raylength = Mathf.Epsilon;
    //float raycounter = 0;
    //float raycounterMax = 10;
    float raycounterLoopMax = 10;
    //for void Update:
    // minimum 2. it seems that jitters raycasts at 2 frames inside of Unity3d's update method is working. by steve chassé aka ninekorn

    // minimum 2. it seems that jitters raycasts at 2 frames inside of Unity3d's update method is working. by steve chassé aka ninekorn




    float raycounterSwtc = 0;


    public Jitter.Dynamics.RigidBody lastFrameHitRigidBody;
    public Vector3 lastFrameHitNormal;
    public Vector3 lastFrameHitPoint;
    public int lastFrameHitrIndex;

    public Transform RaycastHitVisualObject;
    Vector3 lastFrameRayStartPosition = Vector3.zero;

    Vector3 initialPivotPosition = Vector3.zero;
    Vector3 lastFramePosition = Vector3.zero;

    public Transform legstaticpivot;
    /*public Transform upperleg;
    public Transform lowerleg;
    public Transform foot;
    public Transform footTarget;*/

    float upperleglength = 0;
    float lowerleglength = 0;
    float footlength = 0;
    float totallegLength = 0;
    Vector3 originDirection = Vector3.zero;
    float originDirectionLength = 0;

    Vector3 IdleStandingTargetPositionVariableLength;
    Vector3 IdleStandingTargetPositionMax;
    Vector3 IdleStandingTargetPositionMin;

    public Transform pointer0;
    public Transform pointer1;
    public Transform pointer2;
    public Transform pointer3;


    public Transform shoulder;
    public Transform upperArm;
    public Transform foreArm;
    public Transform hand;
    public Transform elbowTarget;
    public Transform handTarget;

    public Transform endHand;
    public Transform endForeArm;


    float lengthUpperArm;
    float lengthForeArm;
    float lengthHand;
    float lengthFromUpperToHand;
    float lengthFromUpperToTarget;
    public Vector2 polarStuff;

    Vector3 offsetFromUpperToElbowTarget;
    float angle;
    public GameObject rotationPoint;
    float lengthFromUpperToEndForeArm;
    public GameObject HandParentObject;
    Vector3 previousPos;

    Vector3 dirFromUpperArmToHandTarget;
    Vector3 dirUpperArmToElbowTarget;
    Vector3 dirElbowTargetToHandTarget;
    float totalArmLength;
    float targetDistance;

    Vector3 endPoint3;
    Vector3 endPoint4;
    Vector3 crosssss;
    Vector3 crosser;

    //public int limbtype = 0;
    //0 = arm;
    //1 = leg;

    int counterForIkFootPlacement = 0;
    int counterForIkFootPlacementMax = 1;
    int counterForIkFootPlacementSwtc = 0;

    int InitcounterForIkFootPlacement = 0;
    int InitcounterForIkFootPlacementMax = 50;
    int InitcounterForIkFootPlacementSwtc = 0;

    int counterForDebug = 0;
    int counterForDebugMax = 5;

    public float touchdowndistance = 1;

    public static JRaycastHit Raycast(Ray ray, float maxDistance, RaycastCallback callback = null)
    {
        RigidBody hitBody;
        JVector hitNormal;
        float hitFraction;

        var origin = ray.origin.ToJVector();
        var direction = ray.direction.ToJVector();

        if (JPhysics.collisionSystem.Raycast(origin, direction, callback, out hitBody, out hitNormal, out hitFraction))
        {
            if (hitFraction <= maxDistance)
            {
                return new JRaycastHit(hitBody, hitNormal.ToVector3(), ray.origin, ray.direction, hitFraction);
            }
        }
        else
        {
            direction *= maxDistance;
            if (JPhysics.collisionSystem.Raycast(origin, direction, callback, out hitBody, out hitNormal, out hitFraction))
            {
                return new JRaycastHit(hitBody, hitNormal.ToVector3(), ray.origin, direction.ToVector3(), hitFraction);
            }
        }
        return null;
    }
    private bool RaycastCallback(Jitter.Dynamics.RigidBody body, JVector normal, float fraction)
    {
        if (body.IsStatic)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    void Start()
    {
        initialPivotPosition = handTarget.position;// this.transform.position;
        lastFramePosition = handTarget.position;
        originDirection = initialPivotPosition - shoulder.position;
        originDirectionLength = originDirection.magnitude;
        originDirection.Normalize();


        lengthUpperArm = Vector3.Distance(upperArm.transform.position, foreArm.transform.position);
        lengthFromUpperToHand = Vector3.Distance(upperArm.transform.position, endHand.transform.position);
        offsetFromUpperToElbowTarget = elbowTarget.transform.position - upperArm.transform.position;
        lengthForeArm = Vector3.Distance(endForeArm.transform.position, foreArm.transform.position);
        lengthFromUpperToEndForeArm = lengthUpperArm + lengthForeArm;
        lengthHand = Vector3.Distance(hand.position, endHand.position);
        //lengthForeArm += lengthHand;

        totalArmLength = lengthForeArm + lengthUpperArm;
        lengthFromUpperToTarget = Vector3.Distance(upperArm.transform.position, handTarget.transform.position);
    }

    void LateUpdate()
    {
        dirFromUpperArmToHandTarget = handTarget.transform.position - upperArm.transform.position;
        dirUpperArmToElbowTarget = elbowTarget.transform.position - upperArm.transform.position;
        dirElbowTargetToHandTarget = handTarget.position - elbowTarget.position;
        var dirLowerArmToHandTarget = handTarget.transform.position - foreArm.transform.position;


        float distshoulderToHandtarget = Vector3.Distance(shoulder.position, handTarget.position);
        distshoulderToHandtarget = Mathf.Min(distshoulderToHandtarget, totalArmLength - totalArmLength * 0.001f);


        //circlecircleintersect
        float circircinteradjacentX = ((distshoulderToHandtarget * distshoulderToHandtarget) - (lengthForeArm * lengthForeArm) + (lengthUpperArm * lengthUpperArm)) / (2 * distshoulderToHandtarget);

        //pythagore //c2=a2+b2
        float circircinterhalfA = Mathf.Sqrt((lengthUpperArm * lengthUpperArm) - (circircinteradjacentX * circircinteradjacentX));

        Vector3 dircircircinteradjacentX = dirFromUpperArmToHandTarget.normalized * circircinteradjacentX;//getting the vector3 direction from the shoulder to the end of length of circircinteradjacentX

        //rounding
        /*float xnewDir = Mathf.Round(newDirec.x * 100) / 100;
        float ynewDir = Mathf.Round(newDirec.y * 100) / 100;
        float znewDir = Mathf.Round(newDirec.z * 100) / 100;
        Vector3 newDirFloored = new Vector3(xnewDir, ynewDir, znewDir);
        endPoint3 = upperArm.position + (newDirec.normalized * circircinteradjacentX);
        */

        Vector3 locOfPointadjacentX = shoulder.position + dircircircinteradjacentX;

        crosssss = Vector3.Cross(dircircircinteradjacentX, dirUpperArmToElbowTarget); ////  

        crosser = -Vector3.Cross(dircircircinteradjacentX, crosssss); ////
        Vector3 elbowposition = locOfPointadjacentX + (crosser.normalized * circircinterhalfA);

        Vector3 upperToElbow = elbowposition - shoulder.position;
        Debug.DrawRay(locOfPointadjacentX, crosser.normalized * 0.15f, Color.red, 0.1f);


        if (swtcForMovingTarget == 0)
        {
            //to readd if it doesn't work
            elbowTarget.position = locOfPointadjacentX + (crosser.normalized);
            //to readd if it doesn't work

        }
        else if (swtcForMovingTarget == 1)
        {

        }





        upperToElbow.Normalize();
        dirFromUpperArmToHandTarget.Normalize();


        //for 2D IK
        //upperToElbow.z = 0;
        //dirFromUpperArmToHandTarget.z = 0;

        Quaternion rotation = Quaternion.LookRotation(upperToElbow, dirFromUpperArmToHandTarget);
        transform.rotation = rotation;

        //Quaternion q = transform.rotation;
        //q.eulerAngles = new Vector3(0, q.eulerAngles.y, q.eulerAngles.z);
        //transform.rotation = q;



        Vector3 ElbowToHand = hand.position - foreArm.position;
        ElbowToHand.Normalize();
        dirLowerArmToHandTarget.Normalize();

        rotation = Quaternion.LookRotation(dirLowerArmToHandTarget, foreArm.up);
        foreArm.transform.rotation = rotation;


        
     

        var rayLengthModded = raylength * 0.01f;
        if (swtcForIKLimbsType == 1)
        {
            /*if (touchingground == 0)
            {
                handTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
            }*/


            if (swtcForMovingTarget == 0 || swtcForMovingTarget == 1)
            {
                //to readd if it doesn't work
                //elbowTarget.position = locOfPointadjacentX + (crosser.normalized);
                //to readd if it doesn't work
                //handTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);

                if (InitcounterForIkFootPlacementSwtc == 1)
                {
                    if (counterForIkFootPlacement >= counterForIkFootPlacementMax)
                    {
                        if (raycounterSwtc == 0)
                        {


                            //for (int i = 0; i < 10; i++)
                            {


                                if (raylength < raycounterLoopMax)
                                {


                                    rayPframe = new Ray(handTarget.position, -handTarget.up);
                                    var raycasthit = Raycast(rayPframe, rayLengthModded, null);

                                    /*float rayvelo = 0.1f;

                                    var smoothx = sc_maths.SmoothDampVec(lastrayPframe.origin.x, rayPframe.origin.x, ref rayvelo, 1, 10, 1 / 60);
                                    var smoothy = sc_maths.SmoothDampVec(lastrayPframe.origin.y, rayPframe.origin.y, ref rayvelo, 1, 10, 1 / 60);
                                    var smoothz = sc_maths.SmoothDampVec(lastrayPframe.origin.z, rayPframe.origin.z, ref rayvelo, 1, 10, 1 / 60);
                                    */



                                    /*float fraction;
                                    Jitter.Dynamics.RigidBody body;
                                    JVector normal;
                                    bool someResult = JPhysics.collisionSystem.Raycast(JitterExtensions.ToJVector(transform.position), JitterExtensions.ToJVector(-transform.up * raylength * 0.1f), RaycastCallback, out body, out normal, out fraction);

                                    if (someResult)
                                    {
                                        Debug.Log("hit1");
                                    }
                                    if (body != null)
                                    {
                                        Debug.Log("hit2");
                                    }*/






                                    Debug.DrawRay(handTarget.position, -handTarget.up * (0.5f), Color.green, 0.5f);

                                    if (raycasthit != null)
                                    {
                                        /*raycounterSwtc = 1;
                                        lastFrameHitRigidBody = raycasthit.Rigidbody;
                                        lastFrameHitNormal = raycasthit.Normal;
                                        lastFrameHitPoint = raycasthit.Point;
                                        lastFrameHitrIndex = raycasthit.Rigidbody.rIndex;
                                        lastFrameHitRayLength = raylength;*/

                                        //Debug.Log("raycasthit != null");
                                        Debug.DrawRay(raycasthit.Point, Vector3.right * (0.5f), Color.red, 0.5f);

                                        //RaycastHitVisualObject.position = raycasthit.Point;
                                        //handTarget.position = raycasthit.Point; ;// legstaticpivot.position + (originDirection * originDirectionLength);
                                        if (raycasthit.Rigidbody != null && raycasthit.Rigidbody.Shape.rIndex != -1)//Physics.Raycast(ray, out hit, 0.1f))
                                        {
                                            Debug.DrawRay(raycasthit.Point, Vector3.forward * (0.5f), Color.blue, 0.5f);



                                            var hitPointForwardNormal = handTarget.forward;
                                            var hitPointUpNormal = handTarget.up;
                                            var hitPointRightNormal = handTarget.right;

                                            Debug.DrawRay(handTarget.position, hitPointForwardNormal * (0.5f), Color.blue, 0.0001f);
                                            Debug.DrawRay(handTarget.position, hitPointUpNormal * (0.5f), Color.green, 0.0001f);
                                            Debug.DrawRay(handTarget.position, hitPointRightNormal * (0.5f), Color.red, 0.0001f);


                                            //handTarget.position = raycasthit.Point + (handTarget.up * 0.1f);

                                            //Vector3 dirToTarget = raycasthit.Point - handTarget.position;





                                            //var rotationDOTVELOTWO = SC_Utilities.Dot(dirToWaypointX, dirToWaypointY, cData.nData.nVelo.x, cData.nData.nVelo.y);
                                            //sc_maths.Dot();
                                            //sc_maths.NSEW();

                                            //Vector3.Cross();
                                            //Vector3 someEndResult = Vector3.Project(dirToTarget, raycasthit.Point);
                                            //Vector3 someEndResult = Vector3.ProjectOnPlane(dirToTarget, raycasthit.Normal);
                                            //Debug.DrawRay(legstaticpivot.position, someEndResult - legstaticpivot.position, Color.magenta, 0.0001f);
                                            //Debug.DrawRay(raycasthit.Point, someEndResult, Color.magenta, 0.0001f);




                                            //var distToTarget = Vector3.Distance(legstaticpivot.position, RaycastHitVisualObject.position);
                                            //Debug.Log("distToTarget:" + raycasthit.Distance + "/distToTarget:" + distToTarget);
                                            /*if (raycasthit.Distance < touchdowndistance)
                                            {
                                                Debug.Log("touchdown IKSC.cs");  
                                            }
                                            else
                                            {
                                                Debug.Log("!touchdown IKSC.cs");
                                            }*/





                                            //handTarget.position = lastFrameHitPoint;


                                            /*if (raycasthit.Rigidbody.Position.X == footTarget.position.x &&
                                                raycasthit.Rigidbody.Position.Y == footTarget.position.y &&
                                                raycasthit.Rigidbody.Position.Z == footTarget.position.z)
                                            {
                                                Debug.Log("same position");
                                            }*/



                                            var someCompTwo = this.transform.GetComponent<JRigidBody>();

                                            if (someCompTwo == null)
                                            {
                                                if (lastFrameHitRigidBody == raycasthit.Rigidbody)
                                                {
                                                    raycounterSwtc = 0;
                                                    if (lastFrameHitNormal == raycasthit.Normal)
                                                    {
                                                        //Debug.Log("lastFrameHitNormal == raycasthit.Normal");
                                                    }
                                                    else
                                                    {
                                                        //Debug.Log("lastFrameHitNormal != raycasthit.Normal");
                                                    }
                                                    if (lastFrameHitPoint == raycasthit.Point)
                                                    {
                                                        //Debug.Log("lastFrameHitPoint == raycasthit.Point");
                                                    }
                                                    else
                                                    {
                                                        //Debug.Log("lastFrameHitPoint != raycasthit.Point");
                                                    }
                                                }
                                                else
                                                {
                                                    //Debug.Log("lastFrameHitRigidBody != raycasthit.Rigidbody");

                                                    //raycounterSwtc = 1;
                                                }

                                                lastFrameHitRigidBody = raycasthit.Rigidbody;
                                                lastFrameHitNormal = raycasthit.Normal;
                                                lastFrameHitPoint = raycasthit.Point;
                                                lastFrameHitrIndex = raycasthit.Rigidbody.rIndex;
                                                lastFrameHitRayLength = raylength;

                                                if (raycounterSwtc == 1)
                                                {
                                                    //break;
                                                }
                                            }
                                            else
                                            {
                                                //Debug.Log("index:" + this.transform.GetComponent<JRigidBody>().rIndex);
                                                if (this.transform.GetComponent<JRigidBody>().rIndex != raycasthit.Rigidbody.Shape.rIndex)
                                                {
                                                    //RaycastHitVisualObject.position = raycasthit.Point;


                                                    if (lastFrameHitRigidBody == raycasthit.Rigidbody)
                                                    {
                                                        raycounterSwtc = 0;
                                                        if (lastFrameHitNormal == raycasthit.Normal)
                                                        {
                                                            //Debug.Log("lastFrameHitNormal == raycasthit.Normal");
                                                        }
                                                        else
                                                        {
                                                            //Debug.Log("lastFrameHitNormal != raycasthit.Normal");
                                                        }
                                                        if (lastFrameHitPoint == raycasthit.Point)
                                                        {
                                                            //Debug.Log("lastFrameHitPoint == raycasthit.Point");
                                                        }
                                                        else
                                                        {
                                                            //Debug.Log("lastFrameHitPoint != raycasthit.Point");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //Debug.Log("lastFrameHitRigidBody != raycasthit.Rigidbody");

                                                        //raycounterSwtc = 1;
                                                    }

                                                    lastFrameHitRigidBody = raycasthit.Rigidbody;
                                                    lastFrameHitNormal = raycasthit.Normal;
                                                    lastFrameHitPoint = raycasthit.Point;
                                                    lastFrameHitrIndex = raycasthit.Rigidbody.rIndex;
                                                    lastFrameHitRayLength = raylength;

                                                    if (raycounterSwtc == 1)
                                                    {
                                                        //break;
                                                    }
                                                }
                                            }

                                            //RaycastHitVisualObject.position = JitterExtensions.ToVector3(raycasthit.Rigidbody.Position);
                                            //Debug.Log(raycasthit.Rigidbody.Shape.rIndex + "_");
                                            /*if (raycasthit.Rigidbody.rIndex != this.transform.GetComponent<JRigidBody>().rigidIndex)
                                            {

                                            }*/

                                            //Debug.Log(this.transform.GetComponent<JRigidBody>().rigidIndex);

                                            /*if (this.transform.GetComponent<JRigidBody>() != null)
                                            {
                                                var someComp = this.transform.GetComponent<JRigidBody>().body;

                                                if (someComp == null)
                                                {
                                                    Debug.Log("null0");

                                                }
                                                else
                                                {
                                                    Debug.Log("!null0");
                                                }

                                            }*/

                                        }
                                        else
                                        {
                                            Debug.Log("hitrIndex:" + raycasthit.Rigidbody.Shape.rIndex);
                                        }
                                    }
                                    else
                                    {
                                        //handTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
                                    }

                                    raylength++;
                                }
                                else
                                {
                                    raylength = Mathf.Epsilon;
                                }
                            }
                        }

                        counterForIkFootPlacement = 0;


                        if (raycounterSwtc == 1)
                        {
                            RaycastHitVisualObject.position = lastFrameHitPoint;
                            //this.transform.position = RaycastHitVisualObject.position;

                            var ray = new Ray(transform.position, -transform.up * ((lastFrameHitRayLength * 0.85f) * 0.25f));
                            //var raycasthit = Raycast(ray, raylength, null);

                            //var ray = new Ray(transform.position, -transform.up);
                            var raycasthit = Raycast(ray, raylength, null);

                            if (raycasthit != null)
                            {
                                if (raycasthit.Rigidbody != null)//Physics.Raycast(ray, out hit, 0.1f))
                                {
                                    if (lastFrameHitRigidBody == raycasthit.Rigidbody)
                                    {
                                        lastFrameHitPoint = raycasthit.Point;
                                        Debug.Log("lastFrameHitRigidBody == raycasthit.Rigidbody");
                                    }
                                }
                            }
                        }

                        /*

                        if (raycounterSwtc == 1)
                        {
                            touchingground = 0;
                            RaycastHitVisualObject.position = lastFrameHitPoint;
                            var ray = new Ray(handTarget.position, -handTarget.up * (rayLengthModded));
                            var raycasthit = Raycast(ray, rayLengthModded, null);

                            if (raycasthit != null)
                            {
                                //touchingground = 1;
                                // raycasthit.Point;

                                if (raycasthit.Rigidbody != null)//Physics.Raycast(ray, out hit, 0.1f))
                                {
                                    if (lastFrameHitRigidBody == raycasthit.Rigidbody)
                                    {
                                        Debug.Log("lastFrameHitRigidBody == raycasthit.Rigidbody");
                                    }
                                }
                            }
                            else
                            {
                     
                                raycounterSwtc = 0;
                            }
                            raycounterSwtc = 0;
                        }*/
                    }
                    counterForIkFootPlacement++;
                }

                if (InitcounterForIkFootPlacementSwtc == 0)
                {
                    if (InitcounterForIkFootPlacement >= InitcounterForIkFootPlacementMax)
                    {
                        Debug.Log("***INIT COUNTER REACHED. can start ray***");
                        InitcounterForIkFootPlacementSwtc = 1;
                        InitcounterForIkFootPlacement = 0;
                    }
                    InitcounterForIkFootPlacement++;
                }

                if (counterForDebug >= counterForDebugMax)
                {
                    //Debug.Log("raylength: " + raylength);
                    counterForDebug = 0;
                }

                counterForDebug++;
                lastFramePosition = handTarget.position;
                lastrayPframe = rayPframe;
            }
            else if (swtcForMovingTarget == 1)
            {

            }
        }
        
       //
       /*if (elbowposition.x == null || elbowposition.y == null || elbowposition.z == null)
       {

       }*/
       pointer0.position = elbowposition;
        pointer1.position = shoulder.position + (crosssss);
        pointer2.position = elbowTarget.position + (crosssss);









        Debug.DrawRay(pointer1.position, crosssss.normalized * 0.15f, Color.blue, 0.1f);
        Debug.DrawRay(pointer2.position, crosssss.normalized * 0.15f, Color.blue, 0.1f);
        Debug.DrawRay(pointer3.position, crosssss.normalized * 0.15f, Color.blue, 0.1f);
        previousPos = transform.position;
    }




    //for 2D IK
    //ElbowToHand.z = 0;
    //dirLowerArmToHandTarget.z = 0;





    // q = foreArm.rotation;
    //q.eulerAngles = new Vector3(0, q.eulerAngles.y, q.eulerAngles.z);
    //foreArm.rotation = q;



    //foreArm.transform.LookAt(handTarget);








    /*float speed = 10;
    float rotate_speed = 10;

    float max_distance = 1f;

    var pivotofthislimbVec = elbowposition;
    var thislimb = foreArm;
    var thislimbstarget = handTarget.position;

    var clamped = pivotofthislimbVec;
    clamped.x = Mathf.Clamp(clamped.x, thislimbstarget.x - max_distance, thislimbstarget.x + max_distance);
    clamped.y = Mathf.Clamp(clamped.y, thislimbstarget.y - max_distance, thislimbstarget.y + max_distance);
    pivotofthislimbVec = clamped;
    var rot = new Quaternion();
    rot.SetLookRotation(thislimb.transform.forward, -(thislimbstarget - pivotofthislimbVec).normalized);
    thislimb.transform.rotation = Quaternion.Lerp(thislimb.transform.rotation, rot, rotate_speed);
    */


















    /*
    Vector3 pivotofthislimbVec = shoulder.position;
    thislimb = upperArm;
    thislimbstarget = elbowposition;
    clamped = pivotofthislimbVec;
    clamped.x = Mathf.Clamp(clamped.x, thislimbstarget.x - max_distance, thislimbstarget.x + max_distance);
    clamped.y = Mathf.Clamp(clamped.y, thislimbstarget.y - max_distance, thislimbstarget.y + max_distance);
    pivotofthislimbVec = clamped;
    rot = new Quaternion();
    rot.SetLookRotation(thislimb.transform.forward, -(thislimbstarget - pivotofthislimbVec).normalized);
    thislimb.transform.rotation = Quaternion.Lerp(thislimb.transform.rotation, rot, rotate_speed);*/
    /*
    pivotofthislimbVec = elbowposition;
    thislimb = foreArm;
    thislimbstarget = handTarget.position;
    clamped = pivotofthislimbVec;
    clamped.x = Mathf.Clamp(clamped.x, thislimbstarget.x - max_distance, thislimbstarget.x + max_distance);
    clamped.y = Mathf.Clamp(clamped.y, thislimbstarget.y - max_distance, thislimbstarget.y + max_distance);
    pivotofthislimbVec = clamped;
    rot = new Quaternion();
    rot.SetLookRotation(thislimb.transform.forward, -(thislimbstarget - pivotofthislimbVec).normalized);
    thislimb.transform.rotation = Quaternion.Lerp(thislimb.transform.rotation, rot, rotate_speed);
    */




















    //var theheadrotmatrix = Matrix4x4.LookAt(shoulder.position, shoulder.position + shoulder.right, shoulder.up);
    //theheadrotmatrix.Invert();
    //shoulder.rotation = theheadrotmatrix.rotation;

    //transform.LookAt(elbowposition);
    //foreArm.transform.LookAt(handTarget);


    //Quaternion rotation = Quaternion.LookRotation(upperToElbow, dirFromUpperArmToHandTarget);
    //transform.rotation = rotation;
    //foreArm.transform.LookAt(handTarget);









    //Quaternion rotation = Quaternion.LookRotation(lastoDir, dirFromUpperArmToHandTarget);
    //transform.rotation = rotation;
    //foreArm.transform.LookAt(handTarget);

    //https://pastebin.com/fAFp6NnN // Also found on the unity3D forums by aldonaletto
    public static Vector3 _getDirection(Vector3 value, Quaternion rotation)
    {
        Vector3 vector;
        double num12 = rotation.x + rotation.x;
        double num2 = rotation.y + rotation.y;
        double num = rotation.z + rotation.z;
        double num11 = rotation.w * num12;
        double num10 = rotation.w * num2;
        double num9 = rotation.w * num;
        double num8 = rotation.x * num12;
        double num7 = rotation.x * num2;
        double num6 = rotation.x * num;
        double num5 = rotation.y * num2;
        double num4 = rotation.y * num;
        double num3 = rotation.z * num;
        double num15 = ((value.x * ((1f - num5) - num3)) + (value.y * (num7 - num9))) + (value.z * (num6 + num10));
        double num14 = ((value.x * (num7 + num9)) + (value.y * ((1f - num8) - num3))) + (value.z * (num4 - num11));
        double num13 = ((value.x * (num6 - num10)) + (value.y * (num4 + num11))) + (value.z * ((1f - num8) - num5));
        vector.x = (float)num15;
        vector.y = (float)num14;
        vector.z = (float)num13;
        return vector;
    }
    private Vector3 RotateVector2D(Vector3 oldDirection, float angle)
    {
        float newX = Mathf.Cos(angle * Mathf.Deg2Rad) * (oldDirection.x) - Mathf.Sin(angle * Mathf.Deg2Rad) * (oldDirection.y);
        float newY = Mathf.Sin(angle * Mathf.Deg2Rad) * (oldDirection.x) + Mathf.Cos(angle * Mathf.Deg2Rad) * (oldDirection.y);
        float newZ = oldDirection.z;
        return new Vector3(newX, newY, newZ);
    }



    float getAngleIn3dWithAtan2(Vector3 dir1, Vector3 dir2)
    {
        float angle = Mathf.Atan2((Vector3.Cross(dir1, dir2).magnitude), Vector3.Dot(dir1, dir2));
        return angle;
    }














    Quaternion rotateTowards(Transform currentTransform, Transform targetTransform, Vector3 forward, float speed)
    {
        Vector3 vectorToTarget = -(targetTransform.position - currentTransform.position);
        float angle = -Mathf.Atan2(vectorToTarget.x, vectorToTarget.y) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, forward);
        return Quaternion.Slerp(currentTransform.rotation, q, Time.deltaTime * speed);
    }






    public Quaternion LookAt(Vector3 sourcePoint, Vector3 destPoint)
    {
        Vector3 forwardVector = Vector3.Normalize(destPoint - sourcePoint);

        float dot = Vector3.Dot(Vector3.forward, forwardVector);

        if (Mathf.Abs(dot - (-1.0f)) < 0.000001f)
        {
            return new Quaternion(Vector3.up.x, Vector3.up.y, Vector3.up.z, 3.1415926535897932f);
        }
        if (Mathf.Abs(dot - (1.0f)) < 0.000001f)
        {
            return Quaternion.identity;
        }

        float rotAngle = (float)Mathf.Acos(dot);
        Vector3 rotAxis = Vector3.Cross(Vector3.forward, forwardVector);
        rotAxis = Vector3.Normalize(rotAxis);
        return CreateFromAxisAngle(rotAxis, rotAngle);
    }

    // just in case you need that function also
    public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
    {
        float halfAngle = angle * .5f;
        float s = (float)System.Math.Sin(halfAngle);
        Quaternion q;
        q.x = axis.x * s;
        q.y = axis.y * s;
        q.z = axis.z * s;
        q.w = (float)System.Math.Cos(halfAngle);
        return q;
    }














    private float getAngle(Transform currentTransform, Vector3 targetPosition)
    {
        // the position to compare with
        //var targetPosition = new Vector3();
        // use my gameobject's transform to determine fwd vector to target
        var localTarget = currentTransform.InverseTransformPoint(targetPosition);
        // Use Trig to get my Angle IN RANGE -180 to 180
        var targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        return targetAngle;
    }



    private Vector2 CartesianToPolar(Vector3 point)
    {
        Vector2 polar;
        //calc longitude 
        polar.y = Mathf.Atan2(point.x, point.z);
        //this is easier to write and read than sqrt(pow(x,2), pow(y,2))!
        var xzLen = new Vector2(point.x, point.z).magnitude;
        //do the atan thing to get our latitude
        polar.x = Mathf.Atan2(-point.y, xzLen);
        //convert to degrees
        polar *= Mathf.Rad2Deg;
        return polar;
    }

    private Vector3 PolarToCartesian(Vector2 polar)
    {
        //an origin vector, representing lat,lon of 0,0. 
        var origin = new Vector3(0, 0, 1);
        //var origin = parent.transform.position;
        //generate a rotation quat based on polar's angle values
        var rotation = Quaternion.Euler(polar.x, polar.y, 0);
        //rotate origin by rotation
        Vector3 point = rotation * origin;
        return point;
    }

    float angleBetween(Transform currentTransform, Transform targetTransform)
    {
        var angle = Vector3.Angle(Vector3.ProjectOnPlane(currentTransform.forward, Vector3.up).normalized,
                    Vector3.ProjectOnPlane(targetTransform.position - currentTransform.position, Vector3.up).normalized);
        return angle;
    }

    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

    public float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }

    public float CalculateAngle180_v3(Vector3 fromDir, Vector3 toDir)
    {
        float angle = Quaternion.FromToRotation(fromDir, toDir).eulerAngles.y;
        if (angle > 180)
        {
            return angle - 360f;
        }
        return angle;
    }

    public float SignedVectorAngle(Vector3 referenceVector, Vector3 otherVector, Vector3 normal)
    {
        Vector3 perpVector;
        float angle;

        //Use the geometry object normal and one of the input vectors to calculate the perpendicular vector
        perpVector = Vector3.Cross(normal, referenceVector);

        //Now calculate the dot product between the perpendicular vector (perpVector) and the other input vector
        angle = Vector3.Angle(referenceVector, otherVector);
        angle *= Mathf.Sign(Vector3.Dot(perpVector, otherVector));

        return angle;
    }

    public float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
          Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    public float AngleOffAroundAxis(Vector3 v, Vector3 forward, Vector3 axis)
    {
        Vector3 right = Vector3.Cross(axis, forward);
        forward = Vector3.Cross(right, axis);
        return Mathf.Atan2(Vector3.Dot(v, right), Vector3.Dot(v, forward));
    }

    Quaternion FromToRotation(Vector3 v1, Vector3 v2, float multiplier)
    {
        return Quaternion.AngleAxis(Vector3.Angle(v1, v2) * multiplier, Vector3.Cross(v2, v1));
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    public float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        bool inverse = false;
        var tmin = min;
        var tangle = angle;
        if (min > 180)
        {
            inverse = !inverse;
            tmin -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        var result = !inverse ? tangle > tmin : tangle < tmin;
        if (!result)
            angle = min;

        inverse = false;
        tangle = angle;
        var tmax = max;
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tmax -= 180;
        }

        result = !inverse ? tangle < tmax : tangle > tmax;
        if (!result)
            angle = max;
        return angle;
    }

    int[] SortAndIndex<T>(T[] rg)
    {
        int i, c = rg.Length;
        var keys = new int[c];
        if (c > 1)
        {
            for (i = 0; i < c; i++)
                keys[i] = i;

            System.Array.Sort(rg, keys /*, ... */);
        }
        return keys;
    }




}







//targetDistance = Mathf.Min(targetDistance, totalArmLength);
//Debug.Log(targetDistance);

//float adjacent = ((lengthUpperArm * lengthUpperArm) - (lengthForeArm * lengthForeArm) + (targetDistance * targetDistance)) / (2 * targetDistance);
//hand.transform.rotation = HandParentObject.transform.rotation;
//hand.transform.rotation = HandParentObject.transform.rotation;
//hand.transform.position = HandParentObject.transform.position;

//float perimeter = (lengthUpperArm + lengthForeArm + targetDistance) / 2;
//float areaOfTriangle = Mathf.Sqrt(perimeter * ((perimeter - lengthUpperArm) * (perimeter - lengthForeArm) * (perimeter - targetDistance)));
//float height = areaOfTriangle / (0.5f * lengthForeArm);
//float adjacent = Mathf.Sqrt((lengthUpperArm * lengthUpperArm) - (height * height));

//area = 1/2 b*h
/*if (adjacent < 0)
{
    adjacent = -adjacent;
}*/
/*if (adjacent < 0)
{
    adjacent = -adjacent;
}*/
//Debug.Log(adjacent);
//Debug.Log(newDirec.z);
//Vector3 newDirec = Vector3.ClampMagnitude(dirFromUpperArmToHandTarget, adjacent);


//Debug.Log(newDirFloored);

/*if (newDirFloored == Vector3.zero)
{
    newDirec = dirFromUpperArmToHandTarget.normalized * 0.001f;
    endPoint3 = upperArm.position + newDirec;
    //Debug.Log("zero");
    crosssss = Vector3.Cross(newDirec, dirUpperArmToElbowTarget); ////                        
    crosser = -Vector3.Cross(newDirec, crosssss); ////
    //endPoint4 = (foreArm.position - upperArm.position).normalized * anotherSide;
    //anotherSide = lengthUpperArm;
    //endPoint4 = endPoint3 + (crosser.normalized * anotherSide);
}
else
{
    //Debug.Log("not zero");
    endPoint3 = upperArm.position + (newDirec.normalized * adjacent);
    crosssss = Vector3.Cross(newDirec, dirUpperArmToElbowTarget); ////                        
                                                                  //Debug.Log(crosssss);
    crosser = -Vector3.Cross(newDirec, crosssss); ////
    //endPoint4 = endPoint3 + (crosser.normalized * anotherSide);
}*/


//Debug.DrawRay(upperArm.transform.position, newDirec, Color.red, 0.1f);
//Debug.DrawRay(endPoint3, crosssss, Color.blue, 0.1f);
//Debug.DrawRay(endPoint3, crosser, Color.magenta, 0.1f);

//Instantiate(rotationPoint, endPoint3, Quaternion.identity);
//Instantiate(rotationPoint, endPoint4, Quaternion.identity);

//Debug.DrawRay(endPoint4, lastoDir, Color.cyan, 0.1f);
//Debug.DrawRay(endPoint4, handTarget.position - endPoint4, Color.green, 0.1f);
//Debug.DrawRay(endPoint3, handTarget.position-endPoint3, Color.green, 0.1f);
//Debug.DrawRay(upperArm.position, endPoint4-upperArm.position, Color.green, 0.1f);



//Debug.Log((endPoint4 - upperArm.position).magnitude);
//Debug.Log((handTarget.position - endPoint4).magnitude);






/*if ((upperArm.transform.forward).normalized == dirFromUpperArmToHandTarget.normalized)
{
    Debug.Log("viewing Vector == zero");
}

if ((upperArm.transform.forward).normalized == Vector3.zero)
{
    Debug.Log("viewing Vector == zero");
}

if (currentMovement == Vector3.zero)
{
    Debug.Log("viewing Vector == zero");
}*/

/*if (lastoDir != Vector3.zero)
{
    Quaternion rotation = Quaternion.LookRotation(lastoDir, dirFromUpperArmToHandTarget);
    transform.rotation = rotation;
}*/



//Debug.DrawRay(upperArm.transform.position, dirFromUpperArmToHandTarget, Color.green, 0.1f);
//Debug.DrawRay(elbowTarget.transform.position, dirElbowTargetToHandTarget, Color.green, 0.1f);
//Debug.DrawRay(upperArm.transform.position, dirUpperArmToElbowTarget, Color.green, 0.1f);









