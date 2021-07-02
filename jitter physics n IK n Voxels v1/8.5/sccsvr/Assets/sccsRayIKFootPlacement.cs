using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Threading;
using Jitter;
using Jitter.Collision;
using Jitter.Dynamics;
using Jitter.Dynamics.Constraints;
using Jitter.LinearMath;
using Material = Jitter.Dynamics.Material;
using RigidBody = Jitter.Dynamics.RigidBody;
using SCCoreSystems;

public class sccsRayIKFootPlacement : MonoBehaviour
{
    float dotX = 0;//sc_maths.Dot(dirHitPointToLegStaticPivot.x, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.x, LegStaticPivotToFootTarget.y);
    float dotYX = 0;//sc_maths.Dot(dirHitPointToLegStaticPivot.y, dirHitPointToLegStaticPivot.x, LegStaticPivotToFootTarget.y, LegStaticPivotToFootTarget.x);
    float dotZ = 0;//sc_maths.Dot(dirHitPointToLegStaticPivot.z, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.z, LegStaticPivotToFootTarget.y);
    float dotYZ = 0;// sc_maths.Dot(dirHitPointToLegStaticPivot.y, dirHitPointToLegStaticPivot.z, LegStaticPivotToFootTarget.y, LegStaticPivotToFootTarget.z);
    Vector3 dirHitPointToLegStaticPivot = Vector3.zero;// (lastFrameHitPoint - legstaticpivot.position).normalized;
    Vector3 LegStaticPivotToFootTarget = Vector3.zero;//(legstaticpivot.position + (originDirection * originDirectionLength)).normalized;

    int ForLockFootToGroundSwtc = 0;
    int ForLockFootToGroundCounterMax = 0;
    int ForLockFootToGroundCounter = 0;


    //public Transform raycastObject;
    float distanceTwo = 0;
    public int movementtype = -1;

    //public Transform parentTransform;
    Vector3 lastFrameFootTargetPosition;

    public JRaycastHit raycasthit;
    public JRaycastHit chosenraycasthit;


    float raylengthTwo = 0;
    public float raycounterLoopMaxTwo = 10;

    float raylength = 0;
    //float raycounter = 0;
    //float raycounterMax = 10;
    public float raycounterLoopMax = 20;
    float raycounterSwtc = 0;
    public float touchdowndistance = 0.1f;
    public float lateralDistance = 3;


    public Jitter.Dynamics.RigidBody lastFrameHitRigidBody;
    public Vector3 lastFrameHitNormal;
    public Vector3 lastFrameHitPoint;
    public int lastFrameHitrIndex;
    public float lastFrameHitRayLength = 0;



    //public Transform targetOne;
    //public Transform targetTwo;
    //public Transform targetThree;

    public Transform RaycastHitVisualObject;
    Vector3 initialPivotPosition = Vector3.zero;
    Vector3 lastFramePosition = Vector3.zero;

    public Transform upperleg;
    public Transform lowerleg;
    public Transform foot;
    public Transform legstaticpivot;
    public Transform footTarget;

    float upperleglength = 0;
    float lowerleglength = 0;
    float footlength = 0;
    float totallegLength = 0;
    Vector3 originDirection = Vector3.zero;
    float originDirectionLength = 0;

    Vector3 IdleStandingTargetPositionVariableLength;
    Vector3 IdleStandingTargetPositionMax;
    Vector3 IdleStandingTargetPositionMin;

    Transform parentTransform;

    // Use this for initialization
    public Transform pelvis;
    Vector3 lastFramePelvisPosition = Vector3.zero;

    int frameCounterForSetPos = 0;
    int frameCounterForSetPosMax = 5;
    int frameCounterForSetPosSwtc = 0;

    public float clampedDistance = 0;


    void Awake()
    {
        lastFrameFootTargetPosition = footTarget.position;
        lastFramePelvisPosition = pelvis.transform.position;
        parentTransform = this.transform.parent;
        initialPivotPosition = this.transform.position;
        lastFramePosition = this.transform.position;
        originDirection = initialPivotPosition - this.transform.position;
        originDirection.Normalize();
        originDirectionLength = originDirection.magnitude;
        lastFrameHitPoint = legstaticpivot.position;



        upperleglength = upperleg.localScale.z;
        lowerleglength = lowerleg.localScale.z;
        footlength = foot.localScale.z;
        totallegLength = upperleglength + lowerleglength + footlength;

        IdleStandingTargetPositionMax = transform.position + ((transform.forward * upperleglength) + (transform.forward * lowerleglength) + (transform.forward * footlength));
        IdleStandingTargetPositionMin = transform.position + ((transform.forward * (upperleglength)) + (transform.forward * (lowerleglength)) + (transform.forward * (footlength)) * 0.5f);



        originalDistanceToLimb = sc_maths.sc_check_distance_node_3d(footTarget.position, legstaticpivot.position, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        clampedDistance = originalDistanceToLimb;

        //retAdd.localScale = retAdd.localScale * planeSize;
        //retDel.localScale = retDel.localScale * planeSize;
        /*fraction = 1 / planeSize;
        radius = planeSize / 2;
        diameter = 0.1f;
        whatever = 1 / (diameter * 2);
        cam = Camera.main;*/
    }

    float originalDistanceToLimb = 0;



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

    int counterForIkFootPlacement = 0;
    public int counterForIkFootPlacementMax = 3;
    int counterForIkFootPlacementSwtc = 0;


    int InitcounterForIkFootPlacement = 0;
    public int InitcounterForIkFootPlacementMax = 50;
    int InitcounterForIkFootPlacementSwtc = 0;

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

    /*private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.transform.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
    }*/

    // Update is called once per frame
    void Update()
    {


        if (movementtype == 0)
        {
            //this.transform.parent = parentTransform;
            //this.transform.position = parentTransform.position;
        }
        else
        {
            //this.transform.parent = null;

            /*if (lastFrameHitPoint != null)
            {
                this.transform.position = lastFrameHitPoint;// parentTransform.position;
            }*/

            //this.transform.position = parentTransform.position;

            /*
            if (lastFrameHitPoint != null)
            {
                Vector3 tempDir = legstaticpivot.position - lastFrameHitPoint;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                //IdleStandingTargetPositionVariableLength

                if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                {
                    //footTarget.position = IdleStandingTargetPositionMax;
                    //tempDir.Normalize();
                    //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    Vector3 tempVect = (legstaticpivot.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * footTarget.localScale.y);
                    //MOVINGPOINTER.X = tempVect.X;
                    //MOVINGPOINTER.Y = tempVect.Y;
                    //MOVINGPOINTER.Z = tempVect.Z;
                    footTarget.position = tempVect;// hit.point;
                                                   //footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
                                                   //footTarget.position = raycasthit.Point;
                }
                else
                {
                    footTarget.position = lastFrameHitPoint;// + (tempDir * foot.localScale.y);
                }
            }*/


        }


















        if (InitcounterForIkFootPlacementSwtc == 1)
        {
            if (counterForIkFootPlacement >= counterForIkFootPlacementMax)
            {
                if (raycounterSwtc == 0 || raycounterSwtc == 1)
                {
                    if (raylength < raycounterLoopMax)
                    {



                        for (int r = 0; r < 1; r++)
                        {


                            var ray = new Ray(this.transform.position + (-transform.up * (raylength * 0.01f)), -transform.up * 0.005f);
                            Debug.DrawRay(this.transform.position + (-transform.up * (raylength * 0.01f)), -transform.up *0.005f, Color.green, 0.001f);

                            //var ray = new Ray(this.transform.position, transform.forward * (raylength * 0.1f));
                            //Debug.DrawRay(transform.position, transform.forward * (raylength * 0.1f), Color.green, 0.1f);

                            //var ray = new Ray(transform.position, -transform.up);
                            raycasthit = Raycast(ray, raylength, null);

                            if (raycasthit != null)
                            {
                                //Debug.Log("raycasthit != null");


                                if (raycasthit.Rigidbody.Shape.rTag == 0 || raycasthit.Rigidbody.Shape.rTag == -1)
                                {
                                    //Debug.Log("object hit");
                                }


                                if (raycasthit.Rigidbody != null && raycasthit.Rigidbody.Shape.rIndex != -1 && raycasthit.Rigidbody.Shape.rTag != 1)//Physics.Raycast(ray, out hit, 0.1f))
                                {
                                    //chosenraycasthit = raycasthit;


                                    //float distanceTwo = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, transform.position, 9, 9, 9, 9, 9, 9, 9, 9, 9);
                                    //
                                    float distance = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, transform.position, 9, 9, 9, 9, 9, 9, 9, 9, 9);
                                    float distanceTwo = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position + (originDirection * originDirectionLength), 9, 9, 9, 9, 9, 9, 9, 9, 9);
                                    float distanceThree = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, upperleg.transform.position + (originDirection * distance), 9, 9, 9, 9, 9, 9, 9, 9, 9);

                                    //Debug.Log("distanceTwo:" + distanceTwo);


                                    if (distanceTwo < 15)
                                    {
                                        Debug.DrawRay(transform.position, transform.up * (raylength * 0.01f), Color.green, 0.0001f);
                                        Debug.DrawRay(transform.position, -transform.up * (raylength * 0.01f), Color.green, 0.0001f);
                                        Debug.DrawRay(transform.position, transform.right * (raylength * 0.01f), Color.green, 0.0001f);
                                        Debug.DrawRay(transform.position, -transform.right * (raylength * 0.01f), Color.green, 0.0001f);
                                        Debug.DrawRay(transform.position, transform.forward * (raylength * 0.01f), Color.green, 0.0001f);
                                        Debug.DrawRay(transform.position, -transform.forward * (raylength * 0.01f), Color.green, 0.0001f);

                                        /*footTarget.position = lastFrameHitPoint;
                                        RaycastHitVisualObject.position = lastFrameHitPoint;
                                        this.transform.position = lastFrameHitPoint;*/

                                        //footTarget.localPosition = lastFrameHitPoint;
                                        //RaycastHitVisualObject.localPosition = lastFrameHitPoint;
                                        //this.transform.localPosition = lastFrameHitPoint;
                                    }
                                    else
                                    {

                                        /*footTarget.position = legstaticpivot.position + (legstaticpivot.forward * distanceTwo);
                                        RaycastHitVisualObject.position = legstaticpivot.position + (legstaticpivot.forward * distanceTwo);
                                        this.transform.position = legstaticpivot.position + (legstaticpivot.forward * distanceTwo);*/



                                        //footTarget.position = lastFrameHitPoint;
                                        //RaycastHitVisualObject.position = lastFrameHitPoint;
                                        //this.transform.position = lastFrameHitPoint;

                                        /*footTarget.position = legstaticpivot.position;
                                        RaycastHitVisualObject.position = legstaticpivot.position;
                                        this.transform.position = legstaticpivot.position;*/

                                    }






                                  
                                    if (raycounterSwtc == 0)
                                    {
                                        chosenraycasthit = raycasthit;
                                        //break;
                                        RaycastHitVisualObject.position = chosenraycasthit.Point;
                                        //transform.position = chosenraycasthit.Point;

                                        //Debug.Log("rIndex:" + raycasthit.Rigidbody.Shape.rIndex);
                                        if (raycasthit.Rigidbody.Position.X == footTarget.position.x &&
                                            raycasthit.Rigidbody.Position.Y == footTarget.position.y &&
                                            raycasthit.Rigidbody.Position.Z == footTarget.position.z)
                                        {
                                            Debug.Log("same position. self hit");
                                        }

                                        //Debug.DrawRay(raycasthit.Point, Vector3.up * (0.15f), Color.green, 0.1f);
                                        Debug.DrawRay(raycasthit.Point, Vector3.right * (0.15f), Color.red, 0.1f);
                                        Debug.DrawRay(raycasthit.Point, Vector3.forward * (0.15f), Color.blue, 0.1f);

                                        //Debug.DrawRay(transform.position, -transform.up * (raylength * 0.1f), Color.green, 0.0001f);
                                        Debug.DrawRay(transform.position, transform.right * (0.15f), Color.red, 0.0001f);
                                        Debug.DrawRay(transform.position, transform.forward * (0.15f), Color.blue, 0.0001f);

                                        var someCompTwo = this.transform.GetComponent<JRigidBody>();

                                        if (someCompTwo == null)
                                        {
                                            //RaycastHitVisualObject.position = raycasthit.Point;

                                            //this.transform.parent = null;
                                            //this.transform.position = raycasthit.Point;

                                            //Debug.Log("raycasthit != null");
                                            if (lastFrameHitRigidBody == raycasthit.Rigidbody)
                                            {
                                                
                                                if (lastFrameHitNormal == raycasthit.Normal)
                                                {
                                                    movementtype = 1;
                                                    //Debug.Log("lastFrameHitNormal == raycasthit.Normal");
                                                }
                                                else
                                                {
                                                    //Debug.Log("lastFrameHitNormal != raycasthit.Normal");
                                                    //this.transform.position = raycasthit.Point;
                                                    lastFrameHitRigidBody = raycasthit.Rigidbody;
                                                    lastFrameHitNormal = raycasthit.Normal;
                                                    lastFrameHitPoint = raycasthit.Point;
                                                    lastFrameHitrIndex = raycasthit.Rigidbody.rIndex;
                                                    lastFrameHitRayLength = raylength;
                                                    raycounterSwtc = 1;
                                                }
                                                if (lastFrameHitPoint == raycasthit.Point)
                                                {
                                                    //Debug.Log("lastFrameHitPoint == raycasthit.Point");
                                                }
                                                else
                                                {
                                                    lastFrameHitRigidBody = raycasthit.Rigidbody;
                                                    lastFrameHitNormal = raycasthit.Normal;
                                                    lastFrameHitPoint = raycasthit.Point;
                                                    lastFrameHitrIndex = raycasthit.Rigidbody.rIndex;
                                                    lastFrameHitRayLength = raylength;
                                                    //Debug.Log("lastFrameHitPoint != raycasthit.Point");
                                                    //this.transform.position = raycasthit.Point;
                                                    raycounterSwtc = 1;
                                                }
                                            }
                                            else
                                            {
                                                lastFrameHitRigidBody = raycasthit.Rigidbody;
                                                lastFrameHitNormal = raycasthit.Normal;
                                                lastFrameHitPoint = raycasthit.Point;
                                                lastFrameHitrIndex = raycasthit.Rigidbody.rIndex;
                                                lastFrameHitRayLength = raylength;
                                                //Debug.Log("lastFrameHitRigidBody != raycasthit.Rigidbody");
                                                //this.transform.position = raycasthit.Point;

                                                raycounterSwtc = 1;


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
                                                    if (lastFrameHitNormal == raycasthit.Normal)
                                                    {
                                                        //Debug.Log("lastFrameHitNormal == raycasthit.Normal");
                                                    }
                                                    else
                                                    {
                                                        //Debug.Log("lastFrameHitNormal != raycasthit.Normal");
                                                        //this.transform.position = raycasthit.Point;
                                                        lastFrameHitRigidBody = raycasthit.Rigidbody;
                                                        lastFrameHitNormal = raycasthit.Normal;
                                                        lastFrameHitPoint = raycasthit.Point;
                                                        lastFrameHitrIndex = raycasthit.Rigidbody.rIndex;
                                                        lastFrameHitRayLength = raylength;
                                                        raycounterSwtc = 1;
                                                    }
                                                    if (lastFrameHitPoint == raycasthit.Point)
                                                    {
                                                        //Debug.Log("lastFrameHitPoint == raycasthit.Point");
                                                    }
                                                    else
                                                    {
                                                        lastFrameHitRigidBody = raycasthit.Rigidbody;
                                                        lastFrameHitNormal = raycasthit.Normal;
                                                        lastFrameHitPoint = raycasthit.Point;
                                                        lastFrameHitrIndex = raycasthit.Rigidbody.rIndex;
                                                        lastFrameHitRayLength = raylength;
                                                        raycounterSwtc = 1;
                                                        //Debug.Log("lastFrameHitPoint != raycasthit.Point");
                                                        //this.transform.position = raycasthit.Point;
                                                    }
                                                }
                                                else
                                                {
                                                    //Debug.Log("lastFrameHitRigidBody != raycasthit.Rigidbody");
                                                    //this.transform.position = raycasthit.Point;
                                                    raycounterSwtc = 1;
                                                }
                                            }

                                        }
                                        raycounterSwtc = 1;
                                    }
                                }
                                else
                                {
                                    //Debug.Log("hitrIndex:" + raycasthit.Rigidbody.Shape.rIndex);
                                }
                            }
                        }
                        raylength++;
                    }
                    else
                    {
                        raylength = 0;
                    }
                }
                counterForIkFootPlacement = 0;







                //Debug.Log("raycounterSwtc:" + raycounterSwtc);

                //frame is entering this state because a surface for touchdown was found and the target position is now locked in place.
                if (raycounterSwtc == 1)
                {
                    if (raylengthTwo < raycounterLoopMaxTwo)
                    {
                        //var ray = new Ray(this.transform.position, -transform.up * (raylength * 0.1f));
                        //Debug.DrawRay(transform.position, -transform.up * (raylength * 0.1f), Color.green, 0.1f);

                        //var ray = new Ray(this.transform.position, transform.forward * (raylength * 0.1f));
                        //Debug.DrawRay(transform.position, transform.forward * (raylength * 0.1f), Color.green, 0.1f);

                        //var ray = new Ray(transform.position, -transform.up);

                        //raycasthit = Raycast(ray, raylength, null);

                        if (chosenraycasthit != null)
                        {
                            //Debug.Log("chosenraycasthit != null");


                            if (chosenraycasthit.Rigidbody.Shape.rTag == 0 || chosenraycasthit.Rigidbody.Shape.rTag == -1)
                            {
                                //Debug.Log("object hit");
                            }


                            if (chosenraycasthit.Rigidbody != null && chosenraycasthit.Rigidbody.Shape.rIndex != -1 && chosenraycasthit.Rigidbody.Shape.rTag != 1)//Physics.Raycast(ray, out hit, 0.1f))
                            {
                                //Debug.Log("hit");

                                //float distanceTwo = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, transform.position, 9, 9, 9, 9, 9, 9, 9, 9, 9);
                                //
                                float distance = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, foot.position, 9, 9, 9, 9, 9, 9, 9, 9, 9);
                                //float distanceTwo = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position + (originDirection * originDirectionLength), 9, 9, 9, 9, 9, 9, 9, 9, 9);
                                //float distanceThree = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, upperleg.transform.position + (originDirection * distance), 9, 9, 9, 9, 9, 9, 9, 9, 9);

                                //Debug.Log("d:" + distance);

                                //var offsetLastFrameHitpoint = 

                                //Debug.Log("rIndex:" + chosenraycasthit.Rigidbody.Shape.rIndex);
                                if (chosenraycasthit.Rigidbody.Position.X == footTarget.position.x &&
                                    chosenraycasthit.Rigidbody.Position.Y == footTarget.position.y &&
                                    chosenraycasthit.Rigidbody.Position.Z == footTarget.position.z)
                                {
                                    //Debug.Log("same position. self hit");
                                }

                                //Debug.DrawRay(lastFrameHitPoint, Vector3.up * (0.15f), Color.green, 0.1f);
                                Debug.DrawRay(chosenraycasthit.Point, Vector3.right * (0.15f), Color.red, 0.1f);
                                Debug.DrawRay(chosenraycasthit.Point, Vector3.forward * (0.15f), Color.blue, 0.1f);

                                /*Debug.DrawRay(transform.position + (-transform.up * (raylength * 0.01f)), -transform.up * (raylength * 0.01f), Color.green, 0.0001f);
                                Debug.DrawRay(transform.position, transform.right * (0.15f), Color.red, 0.0001f);
                                Debug.DrawRay(transform.position, transform.forward * (0.15f), Color.blue, 0.0001f);*/

                                dirHitPointToLegStaticPivot = (lastFrameHitPoint - legstaticpivot.position).normalized;
                                LegStaticPivotToFootTarget = (footTarget.position- legstaticpivot.position).normalized;//(legstaticpivot.position + (originDirection * originDirectionLength)).normalized;

                                dotX = sc_maths.Dot(dirHitPointToLegStaticPivot.x, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.x, LegStaticPivotToFootTarget.y);
                                dotYX = sc_maths.Dot(dirHitPointToLegStaticPivot.y, dirHitPointToLegStaticPivot.x, LegStaticPivotToFootTarget.y, LegStaticPivotToFootTarget.x);
                                dotZ = sc_maths.Dot(dirHitPointToLegStaticPivot.z, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.z, LegStaticPivotToFootTarget.y);
                                dotYZ = sc_maths.Dot(dirHitPointToLegStaticPivot.y, dirHitPointToLegStaticPivot.z, LegStaticPivotToFootTarget.y, LegStaticPivotToFootTarget.z);


                                float distOne = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f);

                                Vector3 HitPointPivotRelatingToLegPivot = legstaticpivot.position + (legstaticpivot.forward * distOne);
                                HitPointPivotRelatingToLegPivot.Normalize();

                                Vector3 directionRayToOffsetHipPivotWhenMovingOrNot = lastFrameHitPoint - HitPointPivotRelatingToLegPivot;
                                directionRayToOffsetHipPivotWhenMovingOrNot.Normalize();

                                var dot = Vector3.Dot((HitPointPivotRelatingToLegPivot - legstaticpivot.position).normalized, legstaticpivot.forward); //get the dot product for the multiplicator
                                var multiplicator = dot;
                                clampedDistance = Mathf.Clamp(distance * (multiplicator*1.15f), 0, originalDistanceToLimb);
                         
                                float currentHitPointDistance = sc_maths.sccscheckdistancenode3dRadius(lastFrameHitPoint, foot.position, 1);

                               // Debug.Log("pelvis is moving: clampedDistance:" + clampedDistance + "/originalDistanceToLimb:" + originalDistanceToLimb + "/:distance" + distance + "/currentHitPointDistance:" + currentHitPointDistance + "/dot:" + dot);





                                //raylengthTwo
                                //var dotTwo = Vector3.Dot((HitPointPivotRelatingToLegPivot - legstaticpivot.position).normalized, legstaticpivot.forward); //get the dot product for the multiplicator
                                //var multiplicatorTwo = dot;
                                var clampedDistanceTwo = Mathf.Clamp(raylengthTwo , 0, distOne);

                                //RAYTWO
                                var rayTwo = new Ray(legstaticpivot.position+ (legstaticpivot.forward * (clampedDistanceTwo)), legstaticpivot.forward * (clampedDistanceTwo));
                                Debug.DrawRay(legstaticpivot.position + (legstaticpivot.forward * (clampedDistanceTwo)), legstaticpivot.forward * (clampedDistanceTwo), Color.magenta, 0.001f);

                                var someray = Raycast(rayTwo, raylengthTwo, null);

                                if (someray != null)
                                {
                                    //Debug.Log("tag:"+ someray.Rigidbody.Shape.rTag);

                                    if (someray.Rigidbody.Shape.rTag == 0 || someray.Rigidbody.Shape.rTag == -1)
                                    {
                                        //Debug.Log("0object hit");
                                    }

                                    if (someray.Rigidbody != null && someray.Rigidbody.Shape.rIndex != -1 && someray.Rigidbody.Shape.rTag != 1)//Physics.Raycast(ray, out hit, 0.1f))
                                    {
                                        Debug.Log("#####object hit#####");
                                        //raycounterSwtc = 2;
                                        //Debug.DrawRay(someray.Point, Vector3.up * (0.15f), Color.green, 0.0001f);
                                        Debug.DrawRay(someray.Point, Vector3.right * (0.15f), Color.red, 10);
                                        Debug.DrawRay(someray.Point, Vector3.forward * (0.15f), Color.blue, 10);

                                        //Debug.DrawRay(legstaticpivot.position, -transform.up * (raylengthTwo * 0.1f), Color.green, 0.0001f);
                                        Debug.DrawRay(legstaticpivot.position, transform.right * (0.15f), Color.red, 10);
                                        Debug.DrawRay(legstaticpivot.position, transform.forward * (0.15f), Color.blue, 10);

                                        float distTwo= sc_maths.sc_check_distance_node_3d(someray.Point, legstaticpivot.position, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f);
                                        //lastFrameHitPoint = legstaticpivot.forward * (distTwo);

                                        

                                    }
                                }
                                //RAYTWO

                                //float distOne = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f);









                                if (distance < originalDistanceToLimb || currentHitPointDistance < originalDistanceToLimb)
                                {
                                    if (distance < originalDistanceToLimb)
                                    {
                                        //this.transform.parent = null;
                                        Debug.Log("0touchdown clamped *****LOCKED*****");
                                        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                                        {
                                            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                            {
                                                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                            }
                                            /*if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                            {
                                                RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                            }*/
                                            if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                            {
                                                footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                            }

                                            ForLockFootToGroundSwtc = 1;
                                            /*
                                            if (ForLockFootToGroundSwtc == 1)
                                            {
                                                if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                                {
                                                    Debug.Log("*****LOCKING FOOT******");
                                                    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                                    ForLockFootToGroundSwtc = 0;
                                                }
                                            }*/
                                        }
                                    }
                                    else
                                    {
                                        if (currentHitPointDistance < originalDistanceToLimb * 1) //0.5f //* 0.25f
                                        {
                                            if (distance > originalDistanceToLimb*5)
                                            {
                                                if (ForLockFootToGroundSwtc == 0)
                                                {
                                                    Debug.Log("3touchdown clamped *****RELEASED*****");

                                                    if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                                                    {
                                                        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                                        {
                                                            this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                                        }
                                                        /*if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                                        {
                                                            RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                                        }*/
                                                        if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                                        {
                                                            footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                                        }
                                                        //raycounterSwtc = 0;
                                                        ForLockFootToGroundSwtc = 1;
                                                        //raycounterSwtc = 0;
                                                        /*

                                                            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                                            {
                                                                Debug.Log("*****LOCKING FOOT******");
                                                                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                                                ForLockFootToGroundSwtc = 0;
                                                            }
                                                        }*/
                                                    }
                                                }
                                            }
                                        }
                                        //Debug.Log("2touchdown clamped *****DISTANCE ISSUE*****");
                                    }


                                    if (currentHitPointDistance < originalDistanceToLimb * 1) //0.5f //* 0.25f
                                    {
                                        //this.transform.parent = null;

                                        Debug.Log("1touchdown clamped *****LOCKED*****");

                                        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                                        {
                                            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                            {
                                                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                            }
                                            /*if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                            {
                                                RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                            }*/
                                            if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                            {
                                                footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                            }

                                            ForLockFootToGroundSwtc = 1;
                                            /*
                                            if (ForLockFootToGroundSwtc == 1)
                                            {
                                                if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                                {
                                                    Debug.Log("*****LOCKING FOOT******");
                                                    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                                    ForLockFootToGroundSwtc = 0;
                                                }
                                            }*/
                                        }

                                    }
                                    else
                                    {
                                        Debug.Log("0touchdown clamped *****RELEASED*****");

                                        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                                        {
                                            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                            {
                                                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                            }
                                            /*if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                            {
                                                RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                            }*/
                                            if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                            {
                                                footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                            }
                                            raycounterSwtc = 0;
                                            ForLockFootToGroundSwtc = 0;
                                            //raycounterSwtc = 0;
                                            /*
                                            if (ForLockFootToGroundSwtc == 1)
                                            {
                                                if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                                {
                                                    Debug.Log("*****LOCKING FOOT******");
                                                    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                                    ForLockFootToGroundSwtc = 0;
                                                }
                                            }*/
                                        }
                                    }
                                }
                                else
                                {
                                    Debug.Log("1touchdown clamped *****RELEASED*****");

                                    if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                                    {
                                        if (ForLockFootToGroundSwtc == 1)
                                        {
                                            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                            {
                                                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;

                                                /*if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                                {
                                                    RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                                }*/
                                                if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                                {
                                                    footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                                }



                                                raycounterSwtc = 0;
                                                ForLockFootToGroundSwtc = 0;
                                            }

                                        }
                                        /*if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                                        {
                                            if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                            {
                                                footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                            }
                                        }
                                        if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                                        {
                                            if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                            {
                                                RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                            }
                                        }*/

                                    }
                                }



                                //Debug.Log("dotX:" + dotX + "/dotZ:" + dotZ + "dotYX:" + dotYX + "/dotYZ:" + dotYZ);
                                if (dotYZ >= 0 && dotYZ < 0.99f || dotYZ < 0 && dotYZ > -0.99f ||
                                    dotYX >= 0 && dotYX < 0.99f || dotYX < 0 && dotYX > -0.99f)
                                {
                                    distance = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.transform.position, 1, 1, 1, 1, 1, 1, 1, 1, 1);

                                    //Debug.Log("moving pelvis");

                                    if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                                    {
                                        if (ForLockFootToGroundSwtc == 1)
                                        {
                                            //ForLockFootToGroundSwtc = 0;
                                            //raycounterSwtc = 0;
                                            /*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled)
                                            {
                                                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
                                            }

                                            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                            {
                                                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;

                                                if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                                {
                                                    RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                                }
                                                if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                                {
                                                    footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                                }
                                            }*/
                                            //this.transform.position = legstaticpivot.forward * (distOne);
                                            //transform.Translate((legstaticpivot.forward * (distOne))-this.transform.position);

                                            //lastFrameHitPoint = legstaticpivot.forward * (distOne);

                                            //raycounterSwtc = 0;
                                            //ForLockFootToGroundSwtc = 0;

                                            /*if (!this.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled)
                                            {
                                                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
                                            }*/


                                        }
                                    }

                                        /*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                                        {
                                            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                                            {
                                                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                                            }
                                            raycounterSwtc = 0;
                                            /*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                            {
                                                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                            }
                                        }*/

                                    }
                                else
                                {
                                    //this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                }

                                /*dirHitPointToLegStaticPivot = (lastFrameHitPoint - legstaticpivot.position).normalized;
                                LegStaticPivotToFootTarget = (footTarget.position - legstaticpivot.position).normalized;//(legstaticpivot.position + (originDirection * originDirectionLength)).normalized;

                                dotX = sc_maths.Dot(dirHitPointToLegStaticPivot.x, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.x, LegStaticPivotToFootTarget.y);
                                dotYX = sc_maths.Dot(dirHitPointToLegStaticPivot.y, dirHitPointToLegStaticPivot.x, LegStaticPivotToFootTarget.y, LegStaticPivotToFootTarget.x);
                                dotZ = sc_maths.Dot(dirHitPointToLegStaticPivot.z, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.z, LegStaticPivotToFootTarget.y);
                                dotYZ = sc_maths.Dot(dirHitPointToLegStaticPivot.y, dirHitPointToLegStaticPivot.z, LegStaticPivotToFootTarget.y, LegStaticPivotToFootTarget.z);

                                distance = sc_maths.sc_check_distance_node_3d(raycasthit.Point, legstaticpivot.transform.position, 1, 1, 1, 1, 1, 1, 1, 1, 1);

                                var clamped = Mathf.Clamp(distance, distance, originalDistanceToLimb);
                                */
                                //Debug.Log("distance:" + distance + "/distanceClamped:" + clamped + "/distanceTwo:" + distanceTwo + "/distanceThree:" + distanceThree + "/distOne:" + distOne + "/originalDistanceToLimb:" + originalDistanceToLimb);
                                //dotYX >= 0 && dotYX < 0.995f || dotYX < 0 && dotYX > -0.995f ||
                                //Debug.Log("dotX:" + dotX + "/dotZ:" + dotZ + "dotYX:" + dotYX + "/dotYZ:" + dotYZ);

                                //Debug.Log("dotProd:" + dotProd);
                                //dotX >= 0 && dotX < 0.995f || dotX < 0 && dotX > -0.995f ||
                                //dotZ >= 0 && dotZ < 0.995f || dotZ < 0 && dotZ > -0.995f ||


                                /*
                                if (clamped <= originalDistanceToLimb)
                                {
                                    Debug.Log("touchdown clamped");

                                    /*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
                                    {
                                        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                        {
                                            this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                        }
                                        else if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                                        {
                                            this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    Debug.Log("!touchdown");
                                }*/


                                if (dotYZ >= 0 && dotYZ < 0.995f || dotYZ < 0 && dotYZ > -0.995f)
                                {

                                    //Debug.Log("pelvis is moving");

                                    

                                }
                                else
                                {
                                    /*var rayTwo = new Ray(legstaticpivot.position, legstaticpivot.forward * (distOne));
                                    Debug.DrawRay(legstaticpivot.position, legstaticpivot.forward * (distOne), Color.magenta, 0.001f);

                                    var someray = Raycast(rayTwo, raylength, null);

                                    if (someray != null)
                                    {
                                        //Debug.Log("tag:"+ someray.Rigidbody.Shape.rTag);

                                        if (someray.Rigidbody.Shape.rTag == 0 || someray.Rigidbody.Shape.rTag == -1)
                                        {
                                            //Debug.Log("0object hit");
                                        }

                                        if (someray.Rigidbody != null && someray.Rigidbody.Shape.rIndex != -1 && someray.Rigidbody.Shape.rTag != 1)//Physics.Raycast(ray, out hit, 0.1f))
                                        {
                                            //raycounterSwtc = 2;
                                            //Debug.DrawRay(someray.Point, Vector3.up * (0.15f), Color.green, 0.0001f);
                                            Debug.DrawRay(someray.Point, Vector3.right * (0.15f), Color.red, 10);
                                            Debug.DrawRay(someray.Point, Vector3.forward * (0.15f), Color.blue, 10);

                                            //Debug.DrawRay(legstaticpivot.position, -transform.up * (raylength * 0.1f), Color.green, 0.0001f);
                                            Debug.DrawRay(legstaticpivot.position, transform.right * (0.15f), Color.red, 10);
                                            Debug.DrawRay(legstaticpivot.position, transform.forward * (0.15f), Color.blue, 10);


                                            //chosenraycasthitVisualObject.position = someray.Point;
                                            //Debug.Log("0object hit from leg pivot");
                                            //raycounterSwtc = 0;



                                            //chosenraycasthitVisualObject.transform.position = (someray.Point);
                                            //footTarget.transform.position = (someray.Point);
                                            //transform.position = someray.Point;
                                        }
                                        else
                                        {

                                        }
                                    }*/
                                }
                                        }

                                    }
                        raylengthTwo++;
                    }
                    else
                    {
                        raylengthTwo = 0;
                    }
                }





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

        lastFrameFootTargetPosition = footTarget.position;
        lastFramePelvisPosition = pelvis.transform.position;
        lastFramePosition = transform.position;
    }
}







/*
Vector3 tempDir = upperleg.transform.position - footTarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;
if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
{
    //footTarget.position = IdleStandingTargetPositionMax;
    //tempDir.Normalize();
    //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
    //Vector3 tempVect = (upperleg.transform.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
    //MOVINGPOINTER.X = tempVect.X;
    //MOVINGPOINTER.Y = tempVect.Y;
    //MOVINGPOINTER.Z = tempVect.Z;
    //footTarget.position = tempVect;// hit.point;
    //transform.position = tempVect;// raycasthit.Point + (tempDir * foot.localScale.y);
    //RaycastHitVisualObject.position = tempVect;


}
else
{
    RaycastHitVisualObject.position = raycasthit.Point;
    footTarget.position = raycasthit.Point;// + (tempDir * foot.localScale.y);
    //transform.position = raycasthit.Point;//+ (tempDir * foot.localScale.y);
}*/


//RaycastHitVisualObject.position = raycasthit.Point;
//footTarget.position = raycasthit.Point;



/*Vector3 tempDir = raycasthit.Point - this.transform.position; //footTarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;
if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
{
    //footTarget.position = IdleStandingTargetPositionMax;
    tempDir.Normalize();
    //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
    Vector3 tempVect = (this.transform.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
    //MOVINGPOINTER.X = tempVect.X;
    //MOVINGPOINTER.Y = tempVect.Y;
    //MOVINGPOINTER.Z = tempVect.Z;
    footTarget.position = tempVect;// hit.point;
    //transform.position = tempVect;// raycasthit.Point + (tempDir * foot.localScale.y);
    RaycastHitVisualObject.position = tempVect;
}
else
{
    RaycastHitVisualObject.position = raycasthit.Point;
    footTarget.position = raycasthit.Point;// + (tempDir * foot.localScale.y);
    //transform.position = raycasthit.Point;//+ (tempDir * foot.localScale.y);
}*/
//RaycastHitVisualObject.position = raycasthit.Point;
//float distance = Vector3.Distance(lastFrameHitPoint, transform.position);
//0.04 < 0.01




//this.transform.position = raycasthit.Point;



//footTarget.position = lastFrameHitPoint;

//if (lastFrameHitPoint.x == raycasthit.Point.x &&
//    lastFrameHitPoint.y == raycasthit.Point.y &&
//    lastFrameHitPoint.z == raycasthit.Point.z)
//{
//    movementtype = 1;
//}
//else
//{
//    movementtype = 0;
//}


//if (raycasthit.Rigidbody != lastFrameHitRigidBody)
//{
//    //movementtype = 1;
//    Debug.Log("rigidbody isn't the same");
//}




//RaycastHitVisualObject.position = raycasthit.Point;
//footTarget.position = raycasthit.Point;// + (tempDir * foot.localScale.y);
//transform.position = raycasthit.Point;//+ (tempDir * foot.localScale.y);

/*Debug.DrawRay(raycasthit.Point, Vector3.up * (0.15f), Color.green, 0.1f);
Debug.DrawRay(raycasthit.Point, Vector3.right * (0.15f), Color.red, 0.1f);
Debug.DrawRay(raycasthit.Point, Vector3.forward * (0.15f), Color.blue, 0.1f);

Debug.DrawRay(transform.position, -transform.up * (raylength * 0.1f), Color.green, 0.0001f);
Debug.DrawRay(transform.position, transform.right * (0.15f), Color.red, 0.0001f);
Debug.DrawRay(transform.position, transform.forward * (0.15f), Color.blue, 0.0001f);
*/











/*
if (distance < originalDistanceToLimb * 3)
{
    if (clampedDistance < originalDistanceToLimb)
    {
        Debug.Log("touchdown clamped");
        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
        {
            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
            {
                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
            }
            if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
            {
                RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
            }
            if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
            {
                footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
            }
            if (ForLockFootToGroundSwtc == 1)
            {
                if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                {
                    Debug.Log("*****LOCKING FOOT******");
                    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                    ForLockFootToGroundSwtc = 0;
                }
            }
        }
    }
    else //if (clampedDistance >= originalDistanceToLimb * 3)
    {
        Debug.Log("unclamped");
        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
        {
            /*if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
            {
                if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                {
                    footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                }
            }
            if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
            {
                if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                {
                    RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                }
            }

            if (ForLockFootToGroundSwtc == 0)
            {
                if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                {
                    Debug.Log("*****RELEASING FOOT******");
                    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                    ForLockFootToGroundSwtc = 1;
                }


            }

            //raycounterSwtc = 0;


            /*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
            {
                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
            }


        }


        /*
        if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
        {
            if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
            {
                footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
            }
            /*if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
            {
                footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
            }

            if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
            {
                footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
            }
        }*/
/*
if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
{
    if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
    {
        RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
    }
    /*if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
    {
        RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
    }

   if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
    {
        RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
    }
}

//Debug.Log("!touchdown");
}
/*else
{
if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
{
    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
}
}
}
else
{

/*if (ForLockFootToGroundSwtc == 1)
{
Debug.Log("releasing foot lock");
if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
{
    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
}
ForLockFootToGroundSwtc = 0;
}
}*/



/*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
{
    if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
    {
        this.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
    }
    if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
    {
        this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
    }
    if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
    {
        this.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
    }
}*/

/*if (chosenraycasthitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
{

    if (chosenraycasthitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
    {
        chosenraycasthitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
    }
    if (chosenraycasthitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
    {
        chosenraycasthitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
    }
}
if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
{

    if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
    {
        footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
    }
    if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
    {
        footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
    }
}*/

//chosenraycasthitVisualObject.transform.position = (chosenraycasthit.Point);
//footTarget.transform.position = (chosenraycasthit.Point);
//transform.position = chosenraycasthit.Point;

//raycounterSwtc = 0;



/*if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
{
    if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
    {
        if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
        {
            RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
        }
        if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
        {
            RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
        }
        if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
        {
            RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
        }
    }
    else if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
    {
        RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
    }
}
if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
{
    if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
    {
        if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
        {
            footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
        }
        if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
        {
            footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
        }
        if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
        {
            footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
        }
    }
    else if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
    {
        footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
    }
}*/


















/*
    //if (distance < 3)
    {
        Debug.DrawRay(transform.position, transform.up * (raylength * 0.01f), Color.green, 0.0001f);
        Debug.DrawRay(transform.position, -transform.up * (raylength * 0.01f), Color.green, 0.0001f);
        Debug.DrawRay(transform.position, transform.right * (raylength * 0.01f), Color.green, 0.0001f);
        Debug.DrawRay(transform.position, -transform.right * (raylength * 0.01f), Color.green, 0.0001f);
        Debug.DrawRay(transform.position, transform.forward * (raylength * 0.01f), Color.green, 0.0001f);
        Debug.DrawRay(transform.position, -transform.forward * (raylength * 0.01f), Color.green, 0.0001f);
        */




//float distanceTwo = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, transform.position, 9, 9, 9, 9, 9, 9, 9, 9, 9);
//
/*distance = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, this.transform.position, 100, 100, 100, 100, 100, 100, 100, 100, 100);
distanceTwo = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position + (originDirection * originDirectionLength), 9, 9, 9, 9, 9, 9, 9, 9, 9);
distanceThree = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position + (originDirection * distance), 9, 9, 9, 9, 9, 9, 9, 9, 9);


//Debug.Log("distance:" + distance+ "/distanceTwo:" + distanceTwo + "/distanceThree:" + distanceThree);

float dotProd = Vector3.Dot((lastFrameHitPoint - legstaticpivot.position).normalized, (legstaticpivot.position + (originDirection * originDirectionLength).normalized));
//Debug.Log("dot:" + dotProd);

dirHitPointToLegStaticPivot = (lastFrameHitPoint - legstaticpivot.position).normalized;
LegStaticPivotToFootTarget = (legstaticpivot.position + (originDirection * originDirectionLength)).normalized;

//JPhysics.currentscript.gravity

 dotX = sc_maths.Dot(dirHitPointToLegStaticPivot.x, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.x, LegStaticPivotToFootTarget.y);
 dotYX = sc_maths.Dot(dirHitPointToLegStaticPivot.y, dirHitPointToLegStaticPivot.x, LegStaticPivotToFootTarget.y, LegStaticPivotToFootTarget.x);
 dotZ = sc_maths.Dot(dirHitPointToLegStaticPivot.z, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.z, LegStaticPivotToFootTarget.y);
 dotYZ = sc_maths.Dot(dirHitPointToLegStaticPivot.y, dirHitPointToLegStaticPivot.z, LegStaticPivotToFootTarget.y, LegStaticPivotToFootTarget.z);
 */

//Debug.Log("dotYX: " + dotYX + "/dotYZ:"+dotYZ);


/*float distOne = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f);

Vector3 desiredPos = legstaticpivot.position + (legstaticpivot.forward * distOne);// (legstaticpivot.forward * originalDistanceToLimb);


if (desiredPos.magnitude >= originalDistanceToLimb && distOne < originalDistanceToLimb && distOne != 0)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
{
    Debug.Log("legs are straight");
    //footTarget.position = IdleStandingTargetPositionMax;
    desiredPos.Normalize();
    //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
    Vector3 tempVect = (this.transform.position + (desiredPos * ((-distOne * 0.5f)))) + (-desiredPos * foot.localScale.y);
    //MOVINGPOINTER.X = tempVect.X;
    //MOVINGPOINTER.Y = tempVect.Y;
    //MOVINGPOINTER.Z = tempVect.Z;
    //footTarget.position = tempVect;// hit.point;
    //transform.position = tempVect;// raycasthit.Point + (tempDir * foot.localScale.y);

    //footTarget.position = tempVect;// legstaticpivot.position + (legstaticpivot.forward * distOne);
    //RaycastHitVisualObject.position = tempVect;//legstaticpivot.position + (legstaticpivot.forward * distOne);
    //this.transform.position = tempVect;//legstaticpivot.position + (legstaticpivot.forward * distOne);

}
else
{
    Debug.Log("legs are bent");
    //var currentDistToLimb = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f);

    desiredPos = legstaticpivot.position + (legstaticpivot.forward * distOne);

    //footTarget.position = desiredPos;// raycasthit.Point;// + (tempDir * foot.localScale.y);
    //transform.position = desiredPos;//raycasthit.Point;//+ (tempDir * foot.localScale.y);
    //RaycastHitVisualObject.transform.position = desiredPos;
}*/

// raycounterSwtc = 0;






/*if (dotX >= 0 && dotX < 0.995f || dotX < 0 && dotX > -0.995f ||
   dotZ >= 0 && dotZ < 0.995f || dotZ < 0 && dotZ > -0.995f ||
   dotYX >= 0 && dotYX < 0.995f || dotYX < 0 && dotYX > -0.995f ||
   dotYZ >= 0 && dotYZ < 0.995f || dotYZ < 0 && dotYZ > -0.995f)
{
    if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
    {
        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
        {
            this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
        }
        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
        {
            this.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
        }
    }
    if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
    {

        if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
        {
            RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
        }
        if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
        {
            RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
        }
    }
    if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
    {

        if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
        {
            footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
        }
        if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
        {
            footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
        }
    }




    raycounterSwtc = 0;*/
//Vector3.Distance(raycasthit.Point, legstaticpivot.position);
/*var distOner = sc_maths.sc_check_distance_node_3d(raycasthit.Point, legstaticpivot.position, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f);

distance = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, this.transform.position, 100, 100, 100, 100, 100, 100, 100, 100, 100);
distanceTwo = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position + (originDirection * originDirectionLength), 9, 9, 9, 9, 9, 9, 9, 9, 9);
distanceThree = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position + (originDirection * distance), 9, 9, 9, 9, 9, 9, 9, 9, 9);

Debug.Log("distance:" + distance + "/distanceTwo:" + distanceTwo + "/distanceThree:" + distanceThree + "/distOne:" + distOner);

var clampeder = Mathf.Clamp(distOner, originalDistanceToLimb * 0.005f, originalDistanceToLimb);
*/

/*RaycastHitVisualObject.transform.position = (raycasthit.Point);
footTarget.transform.position = (raycasthit.Point);
transform.position = raycasthit.Point;


if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
{
    if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
    {
        this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
    }
    /*if (this.transform.parent != null)
    {
        this.transform.parent = null;
    }
}*/





/*if (this.transform.parent == null)
{
    this.transform.parent = parentTransform;
}

Debug.Log("pelvis is moving");
if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
{
    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
}*/

//Vector3 secondTriangleVert = footTarget.position + (legstaticpivot.right);
//float distOne = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 9, 9, 9, 9, 9, 9, 9, 9, 9);
//float distOne = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 2, 2, 2, 2, 2, 2, 2, 2, 2);
//float distOne = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 1, 1, 1, 1, 1, 1, 1, 1, 1);
//float distOne = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f);
//float distOne = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 0.485f, 0.485f, 0.485f, 0.485f, 0.485f, 0.485f, 0.485f, 0.485f, 0.485f);
/* float distOne = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f);

 float seeTwo = Vector3.Distance(footTarget.position, legstaticpivot.position);
 float ahTwo = Vector3.Distance(footTarget.position, secondTriangleVert);

 //c2 = a2+b2
 //c2-a2=b2
 //
 var thirdTriangleSideFloat = Mathf.Abs(seeTwo - ahTwo);



 Vector3 desiredPos = legstaticpivot.position + (legstaticpivot.forward * distOne);// (legstaticpivot.forward * originalDistanceToLimb);


 if (desiredPos.magnitude >= originalDistanceToLimb)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
 {
     Debug.Log("legs are straight");
     //footTarget.position = IdleStandingTargetPositionMax;
     desiredPos.Normalize();
     //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
     Vector3 tempVect = (this.transform.position + (desiredPos * ((-distOne * 0.5f)))) + (-desiredPos * foot.localScale.y);
     //MOVINGPOINTER.X = tempVect.X;
     //MOVINGPOINTER.Y = tempVect.Y;
     //MOVINGPOINTER.Z = tempVect.Z;
     //footTarget.position = tempVect;// hit.point;
     //transform.position = tempVect;// raycasthit.Point + (tempDir * foot.localScale.y);

     //footTarget.position = tempVect;// legstaticpivot.position + (legstaticpivot.forward * distOne);
     //RaycastHitVisualObject.position = tempVect;//legstaticpivot.position + (legstaticpivot.forward * distOne);
     //this.transform.position = tempVect;//legstaticpivot.position + (legstaticpivot.forward * distOne);

 }
 else
 {
     Debug.Log("legs are bent");
     var currentDistToLimb = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f);

     desiredPos = legstaticpivot.position + (legstaticpivot.forward * originalDistanceToLimb);

     footTarget.position = desiredPos;// raycasthit.Point;// + (tempDir * foot.localScale.y);
     transform.position = desiredPos;//raycasthit.Point;//+ (tempDir * foot.localScale.y);

 }*/






/*footTarget.position = legstaticpivot.position + (legstaticpivot.forward * distOne);
RaycastHitVisualObject.position = legstaticpivot.position + (legstaticpivot.forward * distOne);
this.transform.position = legstaticpivot.position + (legstaticpivot.forward * distOne);

footTarget.position = legstaticpivot.position + (legstaticpivot.forward * Mathf.Clamp(distOne, totallegLength * 0.5f, totallegLength));
RaycastHitVisualObject.position = legstaticpivot.position + (legstaticpivot.forward * Mathf.Clamp(distOne, totallegLength * 0.5f, totallegLength));
this.transform.position = legstaticpivot.position + (legstaticpivot.forward * Mathf.Clamp(distOne, (totallegLength * 0.5f) * 0.3333f, (totallegLength) * 0.3333f));
*/



//var clamped = Mathf.Clamp(distOne, totallegLength * 0.05f, totallegLength);



//Debug.Log("clampeddistOne:" + clamped);

/*if (distOne != 0)
{
    Vector3 desiredPos = legstaticpivot.position + (legstaticpivot.forward * distOne);

    //Vector3 tempDir = raycasthit.Point - this.transform.position; //footTarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;


    footTarget.position = legstaticpivot.position + (legstaticpivot.forward * distOne);

}*/
//footTarget.position = legstaticpivot.position + (legstaticpivot.forward * totallegLength*0.5f);



/*Vector3 desiredPos = legstaticpivot.position +(legstaticpivot.forward * distOne) ;// (legstaticpivot.forward * originalDistanceToLimb);


if (desiredPos.magnitude >= originalDistanceToLimb)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
{
    Debug.Log("legs are straight");
    //footTarget.position = IdleStandingTargetPositionMax;
    desiredPos.Normalize();
    //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
    Vector3 tempVect = (this.transform.position + (desiredPos * ((-distOne * 0.5f)))) + (-desiredPos * foot.localScale.y);
    //MOVINGPOINTER.X = tempVect.X;
    //MOVINGPOINTER.Y = tempVect.Y;
    //MOVINGPOINTER.Z = tempVect.Z;
    //footTarget.position = tempVect;// hit.point;
    //transform.position = tempVect;// raycasthit.Point + (tempDir * foot.localScale.y);

    //footTarget.position = tempVect;// legstaticpivot.position + (legstaticpivot.forward * distOne);
    //RaycastHitVisualObject.position = tempVect;//legstaticpivot.position + (legstaticpivot.forward * distOne);
    //this.transform.position = tempVect;//legstaticpivot.position + (legstaticpivot.forward * distOne);

}
else
{
    Debug.Log("legs are bent");
    var currentDistToLimb = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f);

    desiredPos = legstaticpivot.position + (legstaticpivot.forward * originalDistanceToLimb);

    footTarget.position = desiredPos;// raycasthit.Point;// + (tempDir * foot.localScale.y);
    transform.position = desiredPos;//raycasthit.Point;//+ (tempDir * foot.localScale.y);

}*/




//var clamped = Mathf.Clamp((raylength * 0.025f)* distOne, 0.01f, (distOne * 0.5f) * 0.1f);
//var clampedTwo = Mathf.Clamp((raylength * 0.025f)* distOne, 0.01f, distOne * 0.5f) * 0.1f;

/*for (int i = 0; i < 10;i ++)
{

    var rayTwo = new Ray(legstaticpivot.position, legstaticpivot.forward * (distOne));
    Debug.DrawRay(legstaticpivot.position, legstaticpivot.forward * (distOne), Color.magenta, 0.001f);

    var someray = Raycast(rayTwo, raylength, null);

    if (someray != null)
    {
        //Debug.Log("tag:"+ someray.Rigidbody.Shape.rTag);

        if (someray.Rigidbody.Shape.rTag == 0 || someray.Rigidbody.Shape.rTag == -1)
        {
            Debug.Log("0object hit");
        }

        if (someray.Rigidbody != null && someray.Rigidbody.Shape.rIndex != -1 && someray.Rigidbody.Shape.rTag != 1)//Physics.Raycast(ray, out hit, 0.1f))
        {
            raycounterSwtc = 2;
            //Debug.DrawRay(someray.Point, Vector3.up * (0.15f), Color.green, 0.0001f);
            Debug.DrawRay(someray.Point, Vector3.right * (0.15f), Color.red, 10);
            Debug.DrawRay(someray.Point, Vector3.forward * (0.15f), Color.blue, 10);

            //Debug.DrawRay(legstaticpivot.position, -transform.up * (raylength * 0.1f), Color.green, 0.0001f);
            Debug.DrawRay(legstaticpivot.position, transform.right * (0.15f), Color.red, 10);
            Debug.DrawRay(legstaticpivot.position, transform.forward * (0.15f), Color.blue, 10);

         é
            //RaycastHitVisualObject.position = someray.Point;
            //Debug.Log("0object hit from leg pivot");
            //raycounterSwtc = 0;
        }
        else
        {

        }
    }
}


if (dotYX >= 0 && dotYX < 0.95f || dotYX < 0 && dotYX > -0.95f ||
    dotYZ >= 0 && dotYZ < 0.95f || dotYZ < 0 && dotYZ > -0.95f)
{
    /*if (GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
    {
        GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
    }*/

/*if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
{
    footTarget.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
}

if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
{
    footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
}
}
else
{

/*if (GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
{
    GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
}

if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
{
    footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
}
if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
{
    footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
}
if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
{
    footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
}
}









/*if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
{
footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
}*/

/*if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
{
    footTarget.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
}
//footTarget.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;


}*/



/*
if (dotX >= 0 && dotX < 0.95f || dotX < 0 && dotX > -0.95f ||
dotZ >= 0 && dotZ < 0.95f || dotZ < 0 && dotZ > -0.95f)
{
    if (dotX >= 0 && dotX < 0.95f && dotZ < 0 && dotZ > -0.95f ||
        dotX >= 0 && dotX < 0.95f && dotZ >= 0 && dotZ < 0.95f ||
        dotX < 0 && dotX > -0.95f && dotZ >= 0 && dotZ < 0.95f ||
        dotX < 0 && dotX > -0.95f && dotZ < 0 && dotZ > -0.95f)
    {
        Debug.Log("testing0");
    }
    else
    {
        Debug.Log("testing1");
    }
    //Debug.Log("dotX:" + dotX + "/dotZ:" + dotZ);
    //Debug.Log("character is approx NOT standing straight compared to the hit normal surface and the character is crouching");
}
else
{
    //Debug.Log("testing1");
    //Debug.Log("character is approx standing straight compared to the hit normal surface and the character is crouching");
}*/



//dotProd = Mathf.Clamp(dotProd,0,1);
//dotProd = sc_maths.ClampValue(dotProd, 0, 1);
//Debug.Log("name:" + this.transform.name +  "/distance: " + distance+ "/distanceThree: " + distanceThree);





/*

if (distanceTwo < 15)
{

    //footTarget.position = lastFrameHitPoint;
    //this.transform.position = lastFrameFootTargetPosition;
    //RaycastHitVisualObject.position = lastFrameHitPoint;

    /*if (RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
    {
        RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
    }

    if (this.transform.parent != null)
    {
        this.transform.parent = null;
    }
    if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
    {
        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
        {
            this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
        }
    }


    if (frameCounterForSetPos >= frameCounterForSetPosMax)
    {
        if (dotProd > -1)
        {
            //Debug.Log("0name:" + this.transform.name + "/character is touching down and is not standing straight");
        }
        else
        {
            //Debug.Log("1name:" + this.transform.name + "/character is touching down and is standing straight");
        }
        frameCounterForSetPos = 0;
    }*/


/*
if (frameCounterForSetPos >= frameCounterForSetPosMax)
{if (dotProd > -1)
    {
        Debug.Log("0name:" + this.transform.name + "/character is touching down and is not standing straight");




    if (dotProd > -1)
    {
        Debug.Log("0name:" + this.transform.name + "/character is touching down and is not standing straight");
        footTarget.position = lastFrameHitPoint;
        this.transform.position = lastFrameFootTargetPosition;
        RaycastHitVisualObject.position = lastFrameHitPoint;

        if (this.transform.parent != null)
        {
            this.transform.parent = null;
        }
        /*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
        {
            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
            {
                this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
            }
        }

    }
    else
    {
        Debug.Log("1name:" + this.transform.name + "/character is touching down and is standing straight");
        //raycounterSwtc



        if (lastFramePelvisPosition != pelvis.transform.position)
        {
            if (this.transform.parent == null)
            {
                this.transform.parent = parentTransform;
            }

            //character is moving reset raycast positions
            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
            {
                /*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
                {
                    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
                }

            }




            footTarget.position = lastFrameHitPoint;
            this.transform.position = lastFrameFootTargetPosition;
            RaycastHitVisualObject.position = lastFrameFootTargetPosition;
        }
        else
        {
            //character is NOT moving don't reset raycasts positions
            if (this.transform.parent != null)
            {
                this.transform.parent = null;
            }
            if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
            {
                if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
                {
                    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
                }
            }
            footTarget.position = lastFrameHitPoint;
            this.transform.position = lastFrameFootTargetPosition;
            RaycastHitVisualObject.position = lastFrameFootTargetPosition;
        }

    }
    frameCounterForSetPos = 0;
}

}
else
{
/*if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
{
    footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
}*/

/*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
{
    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
}


if (this.transform.parent == null)
{
    this.transform.parent = parentTransform;
}*/

/*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
{
    this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
}*/


/*if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
{
    RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
}
if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
{
    footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
}



if (RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
{
    RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
}


if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
{
    footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled = false;
}*/


//footTarget.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;


/*footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
this.transform.position = legstaticpivot.position + (originDirection * originDirectionLength);
RaycastHitVisualObject.position = legstaticpivot.position + (originDirection * originDirectionLength);

//raycounterSwtc = 0;
Debug.Log("2name:" + this.transform.name + "/character is NOT touching down");
}*/











/*if (dotProd < -3)
{
    /*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
    {
        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
        {
            this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
        }
        if (this.transform.parent != null)
        {
            this.transform.parent = null;
        }
    }
    Debug.Log("character is crouching");
}
else
{
    /*if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
    {
        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 0)
        {
            this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 1;
        }
        if (this.transform.parent == null)
        {
            this.transform.parent = parentTransform;
        }
    }
    Debug.Log("character is NOT crouching");

    if (dotX >= 0 && dotX < 0.95f || dotX < 0 && dotX > -0.95f ||
        dotZ >= 0 && dotZ < 0.95f || dotZ < 0 && dotZ > -0.95f)
    {
        if (dotX >= 0 && dotX < 0.95f && dotZ < 0 && dotZ > -0.95f ||
            dotX >= 0 && dotX < 0.95f && dotZ >= 0 && dotZ < 0.95f ||
            dotX < 0 && dotX > -0.95f && dotZ >= 0 && dotZ < 0.95f ||
            dotX < 0 && dotX > -0.95f && dotZ < 0 && dotZ > -0.95f)
        {
            Debug.Log("testing0");
        }
        else
        {
            Debug.Log("testing1");
        }
        //Debug.Log("dotX:" + dotX + "/dotZ:" + dotZ);
        //Debug.Log("character is approx NOT standing straight compared to the hit normal surface and the character is crouching");
    }
    else
    {
        //Debug.Log("testing1");
        //Debug.Log("character is approx standing straight compared to the hit normal surface and the character is crouching");
    }
}*/



/*else
{
    this.transform.position = SCCSRayIKFootPlacement.raycasthit.Point; ;
}*/


/*if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
{
    footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
}
if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled == false)
{
    RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
}*/





/*if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
{
    if (RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
    {
        if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
        {
            RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
        }
    }
    else
    {
        //RaycastHitVisualObject.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
        if (RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
        {
            RaycastHitVisualObject.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
        }
    }
}


if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
{
    if (footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
    {
        if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
        {
            footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
        }
    }
    else
    {
        //footTarget.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
        if (footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
        {
            footTarget.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
        }
    }
}


if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>() != null)
{
    if (this.GetComponent<sccsIKSetInitTouchdownPos>().enabled == true)
    {
        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
        {
            this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
        }
    }
    else
    {
        //this.GetComponent<sccsIKSetInitTouchdownPos>().enabled = true;
        if (this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame == 1)
        {
            this.transform.GetComponent<sccsIKSetInitTouchdownPos>().setDesiredPositionPerFrame = 0;
        }
    }
}*/





//Vector3 desiredPos = legstaticpivot.position + (legstaticpivot.forward * clamped);
//footTarget.position = desiredPos;// raycasthit.Point;// + (tempDir * foot.localScale.y);
//transform.position = desiredPos;//raycasthit.Point;//+ (tempDir * foot.localScale.y);



//Debug.Log("clampeddistOne:" + clamped);

/*if (distOne != 0)
{
    Vector3 desiredPos = legstaticpivot.position + (legstaticpivot.forward * distOne);

    //Vector3 tempDir = raycasthit.Point - this.transform.position; //footTarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;


    footTarget.position = legstaticpivot.position + (legstaticpivot.forward * distOne);

}*/
//footTarget.position = legstaticpivot.position + (legstaticpivot.forward * totallegLength*0.5f);



//Vector3 desiredPos = legstaticpivot.position +(legstaticpivot.forward * distOne) ;// (legstaticpivot.forward * originalDistanceToLimb);


/*if (desiredPos.magnitude >= originalDistanceToLimb)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
{
    Debug.Log("legs are straight");
    //footTarget.position = IdleStandingTargetPositionMax;
    desiredPos.Normalize();
    //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
    Vector3 tempVect = (this.transform.position + (desiredPos * ((-distOne * 0.5f)))) + (-desiredPos * foot.localScale.y);
    //MOVINGPOINTER.X = tempVect.X;
    //MOVINGPOINTER.Y = tempVect.Y;
    //MOVINGPOINTER.Z = tempVect.Z;
    //footTarget.position = tempVect;// hit.point;
    //transform.position = tempVect;// raycasthit.Point + (tempDir * foot.localScale.y);

    //footTarget.position = tempVect;// legstaticpivot.position + (legstaticpivot.forward * distOne);
    //RaycastHitVisualObject.position = tempVect;//legstaticpivot.position + (legstaticpivot.forward * distOne);
    //this.transform.position = tempVect;//legstaticpivot.position + (legstaticpivot.forward * distOne);

}
else
{
    Debug.Log("legs are bent");
    var currentDistToLimb = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f, 0.465f);

    desiredPos = legstaticpivot.position + (legstaticpivot.forward * originalDistanceToLimb);

    footTarget.position = desiredPos;// raycasthit.Point;// + (tempDir * foot.localScale.y);
    transform.position = desiredPos;//raycasthit.Point;//+ (tempDir * foot.localScale.y);

}*/




/*
var someCompTwo = this.transform.GetComponent<JRigidBody>();

if (someCompTwo == null)
{
    //RaycastHitVisualObject.position = raycasthit.Point;



    //this.transform.parent = null;
    //this.transform.position = raycasthit.Point;



    //Debug.Log("raycasthit != null");
    if (lastFrameHitRigidBody == raycasthit.Rigidbody)
    {
        if (lastFrameHitNormal == raycasthit.Normal)
        {
            movementtype = 1;
            //Debug.Log("lastFrameHitNormal == raycasthit.Normal");
        }
        else
        {
            //Debug.Log("lastFrameHitNormal != raycasthit.Normal");
            //this.transform.position = raycasthit.Point;
            raycounterSwtc = 1;
        }
        if (lastFrameHitPoint == raycasthit.Point)
        {
            //Debug.Log("lastFrameHitPoint == raycasthit.Point");
        }
        else
        {
            //Debug.Log("lastFrameHitPoint != raycasthit.Point");
            //this.transform.position = raycasthit.Point;
            raycounterSwtc = 1;
        }
    }
    else
    {
        //Debug.Log("lastFrameHitRigidBody != raycasthit.Rigidbody");
        //this.transform.position = raycasthit.Point;

        raycounterSwtc = 1;


    }

    /*lastFrameHitRigidBody = raycasthit.Rigidbody;
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
            if (lastFrameHitNormal == raycasthit.Normal)
            {
                //Debug.Log("lastFrameHitNormal == raycasthit.Normal");
            }
            else
            {
                //Debug.Log("lastFrameHitNormal != raycasthit.Normal");
                //this.transform.position = raycasthit.Point;
            }
            if (lastFrameHitPoint == raycasthit.Point)
            {
                //Debug.Log("lastFrameHitPoint == raycasthit.Point");
            }
            else
            {
                //Debug.Log("lastFrameHitPoint != raycasthit.Point");
                //this.transform.position = raycasthit.Point;
            }
        }
        else
        {
            //Debug.Log("lastFrameHitRigidBody != raycasthit.Rigidbody");
            //this.transform.position = raycasthit.Point;
            raycounterSwtc = 1;
        }

        /*lastFrameHitRigidBody = raycasthit.Rigidbody;
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
}*/




/*
//var ray = new Ray(legstaticpivot.position, transform.forward);
var ray = new Ray(this.transform.position, -transform.up * (raylength * 0.1f));
//RaycastHit hittwo;
//Debug.DrawRay(legstaticpivot.position, transform.forward * 3, Color.green, 0.001f);

//if (Physics.Raycast(ray, out hit, totallegLength, layerMask))

raycasthit = Raycast(ray, raylength, null);

if (raycasthit != null)
{
    if (raycasthit.Rigidbody != null && raycasthit.Rigidbody.Shape.rIndex != -1 && raycasthit.Rigidbody.Shape.rTag != 1)//raycasthit.transform.tag == "collisionObject")
    {
        Debug.Log("TEST");
        Vector3 tempDir = raycasthit.Point- this.transform.position; //footTarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

        //IdleStandingTargetPositionVariableLength

        RaycastHitVisualObject.position = raycasthit.Point;
        footTarget.position = raycasthit.Point;// + (tempDir * foot.localScale.y);
        transform.position = raycasthit.Point;//+ (tempDir * foot.localScale.y);

        Debug.DrawRay(raycasthit.Point, Vector3.up * (0.15f), Color.green, 0.1f);
        Debug.DrawRay(raycasthit.Point, Vector3.right * (0.15f), Color.red, 0.1f);
        Debug.DrawRay(raycasthit.Point, Vector3.forward * (0.15f), Color.blue, 0.1f);

        Debug.DrawRay(transform.position, -transform.up * (raylength * 0.1f), Color.green, 0.0001f);
        Debug.DrawRay(transform.position, transform.right * (0.15f), Color.red, 0.0001f);
        Debug.DrawRay(transform.position, transform.forward * (0.15f), Color.blue, 0.0001f);

        if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
        {
            //footTarget.position = IdleStandingTargetPositionMax;
            tempDir.Normalize();
            //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
            Vector3 tempVect = (this.transform.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
            //MOVINGPOINTER.X = tempVect.X;
            //MOVINGPOINTER.Y = tempVect.Y;
            //MOVINGPOINTER.Z = tempVect.Z;
            footTarget.position = tempVect;// hit.point;
            transform.position = tempVect;// raycasthit.Point + (tempDir * foot.localScale.y);
        }
        else
        {
            footTarget.position = raycasthit.Point;// + (tempDir * foot.localScale.y);
            transform.position = raycasthit.Point;//+ (tempDir * foot.localScale.y);
        }

        /*Vector3 tempDir = legstaticpivot.position - footTarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

        //IdleStandingTargetPositionVariableLength


        if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
        {
            footTarget.position = IdleStandingTargetPositionMax;
            tempDir.Normalize();
            //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
            Vector3 tempVect = (legstaticpivot.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
            //MOVINGPOINTER.X = tempVect.X;
            //MOVINGPOINTER.Y = tempVect.Y;
            //MOVINGPOINTER.Z = tempVect.Z;
            footTarget.position = tempVect;// hit.point;
        }
        else
        {
            footTarget.position = raycasthit.Point + (tempDir * foot.localScale.y);
        }

    }
}*/



















/*
if (raycounterSwtc == 1)
{
    //float distanceTwo = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, transform.position, 9, 9, 9, 9, 9, 9, 9, 9, 9);
    //
    float distance = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, transform.position, 9, 9, 9, 9, 9, 9, 9, 9, 9);
    //float distanceTwo = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position + (originDirection * originDirectionLength), 9, 9, 9, 9, 9, 9, 9, 9, 9);
    float distanceThree = sc_maths.sc_check_distance_node_3d(lastFrameHitPoint, legstaticpivot.position + (originDirection * distance), 9, 9, 9, 9, 9, 9, 9, 9, 9);

    footTarget.position = lastFrameHitPoint;
    RaycastHitVisualObject.position = lastFrameHitPoint;

    float dotProd = Vector3.Dot((lastFrameHitPoint - legstaticpivot.position).normalized, (legstaticpivot.position + (originDirection * originDirectionLength).normalized));
    //Debug.Log("dot:" + dotProd);

    Vector3 dirHitPointToLegStaticPivot = (lastFrameHitPoint - legstaticpivot.position).normalized;
    Vector3 LegStaticPivotToFootTarget = (legstaticpivot.position + (originDirection * originDirectionLength)).normalized;

    //JPhysics.currentscript.gravity

    float dotX = sc_maths.Dot(dirHitPointToLegStaticPivot.x, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.x, LegStaticPivotToFootTarget.y);
    //float dotY = sc_maths.Dot(dirHitPointToLegStaticPivot.x, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.x, LegStaticPivotToFootTarget.y);
    float dotZ = sc_maths.Dot(dirHitPointToLegStaticPivot.z, dirHitPointToLegStaticPivot.y, LegStaticPivotToFootTarget.z, LegStaticPivotToFootTarget.y);

    //dotProd = Mathf.Clamp(dotProd,0,1);
    //dotProd = sc_maths.ClampValue(dotProd, 0, 1);
    //Debug.Log("dotProd: " + dotProd);


    if (dotProd > -1)
    {
        Debug.Log("character is crouching");
    }
    else
    {
        Debug.Log("character is NOT crouching");

        if (dotX >= 0 && dotX < 0.95f || dotX < 0 && dotX > -0.95f ||
            dotZ >= 0 && dotZ < 0.95f || dotZ < 0 && dotZ > -0.95f)
        {
            if (dotX >= 0 && dotX < 0.95f && dotZ < 0 && dotZ > -0.95f ||
                dotX >= 0 && dotX < 0.95f && dotZ >= 0 && dotZ < 0.95f ||
                dotX < 0 && dotX > -0.95f && dotZ >= 0 && dotZ < 0.95f ||
                dotX < 0 && dotX > -0.95f && dotZ < 0 && dotZ > -0.95f)
            {
                Debug.Log("testing0");
            }
            else
            {
                Debug.Log("testing1");
            }
            //Debug.Log("dotX:" + dotX + "/dotZ:" + dotZ);
            //Debug.Log("character is approx NOT standing straight compared to the hit normal surface and the character is crouching");
        }
        else
        {
            //Debug.Log("testing1");
            //Debug.Log("character is approx standing straight compared to the hit normal surface and the character is crouching");
        }
    }
}*/










/*
if (distanceThree < totallegLength * 2)
{
    if (dotX >= 0 && dotX < 0.95f || dotX < 0 && dotX > -0.95f ||
        dotZ >= 0 && dotZ < 0.95f || dotZ < 0 && dotZ > -0.95f)
    {
        //Debug.Log("dotX:" + dotX + "/dotZ:" + dotZ);
        //Debug.Log("character is approx NOT standing straight compared to the hit normal surface and the character is crouching");
    }
    else
    {
        //Debug.Log("character is approx standing straight compared to the hit normal surface and the character is crouching");

    }
}
else
{
    if (dotX >= 0 && dotX < 0.95f || dotX < 0 && dotX > -0.95f ||
        dotZ >= 0 && dotZ < 0.95f || dotZ < 0 && dotZ > -0.95f)
    {
        //Debug.Log("dotX:" + dotX + "/dotZ:" + dotZ);
        //Debug.Log("character is approx NOT standing straight compared to the hit normal surface and the character is NOT crouching");
    }
    else
    {
        //Debug.Log("character is approx standing straight compared to the hit normal surface and the character is NOT crouching");

    }
}*/




/*Debug.Log("distance:" + distance + "/distanceTwo:" + distanceTwo + "/distanceThree:" + distanceThree + "/totalleglength:" + totallegLength);
if (distanceThree < totallegLength * 2)
{
    Debug.Log("character is standing straight");
}
else
{
    Debug.Log("character is standing not standing straight");
}*/




//c2=a2+b2
//((distance*distance)+())
//this.transform.parent = null;
//Debug.Log("touchdown");

/*
if (distance < crouchingDistance)
{
    Debug.DrawRay(transform.position, transform.up * (raylength * 0.01f), Color.green, 0.0001f);
    Debug.DrawRay(transform.position, -transform.up * (raylength * 0.01f), Color.green, 0.0001f);
    Debug.DrawRay(transform.position, transform.right * (raylength * 0.01f), Color.green, 0.0001f);
    Debug.DrawRay(transform.position, -transform.right * (raylength * 0.01f), Color.green, 0.0001f);
    Debug.DrawRay(transform.position, transform.forward * (raylength * 0.01f), Color.green, 0.0001f);
    Debug.DrawRay(transform.position, -transform.forward * (raylength * 0.01f), Color.green, 0.0001f);

    if (distanceThree < 0.5f)
    {
        Debug.Log("bullzeye.");
    }
    else
    {
        Debug.Log("!bullzeye.");

    }
}
else
{
    if (raycasthit!= null)
    {

        if (raycasthit.Normal != null && lastFrameHitNormal != null)
        {

            if (lastFrameHitNormal.x == raycasthit.Normal.x &&
                lastFrameHitNormal.y == raycasthit.Normal.y &&
                lastFrameHitNormal.z == raycasthit.Normal.z)
            {
                //movementtype = 1;
            }
            else
            {
                //Vector3.SmoothDamp(this.transform.position);
                movementtype = 0;
            }
        }
    }

    //movementtype = 0;

    Debug.Log("distance > touchdowndistance ___ dist:" + distance);
    Debug.DrawRay(transform.position, transform.up * (raylength * 0.01f), Color.red, 0.0001f);
    Debug.DrawRay(transform.position, -transform.up * (raylength * 0.01f), Color.red, 0.0001f);
    Debug.DrawRay(transform.position, transform.right * (raylength * 0.01f), Color.red, 0.0001f);
    Debug.DrawRay(transform.position, -transform.right * (raylength * 0.01f), Color.red, 0.0001f);
    Debug.DrawRay(transform.position, transform.forward * (raylength * 0.01f), Color.red, 0.0001f);
    Debug.DrawRay(transform.position, -transform.forward * (raylength * 0.01f), Color.red, 0.0001f);
}*/



/*Vector3 somePos = new Vector3(raycasthit.Point.x, raycasthit.Point.y, raycasthit.Point.z);
somePos.y = 0;
somePos.x = raycasthit.Point.x;
//somePos.y = raycasthit.Point.y;
somePos.z = raycasthit.Point.z;

raycastObject.position = somePos;*/
//this.transform.position = footTarget.position;
/*if (targetOne != null)
{
    targetOne.transform.position = footTarget.position;
}
if (targetTwo != null)
{
    targetTwo.transform.position = footTarget.position;
}

if (targetThree != null)
{
    targetThree.transform.position = footTarget.position;
}*/



/*var ray = new Ray(transform.position, -transform.up * ((lastFrameHitRayLength * 0.85f) * 0.25f));
//var raycasthit = Raycast(ray, raylength, null);

//var ray = new Ray(transform.position, -transform.up);
var raycasthit = Raycast(ray, raylength, null);

if (raycasthit != null)
{
    if (raycasthit.Rigidbody != null)//Physics.Raycast(ray, out hit, 0.1f))
    {


        var distToTarget = Vector3.Distance(raycasthit.Point, transform.position);

        if (distToTarget < touchdowndistance)
        {
            transform.position = lastFrameHitPoint;
        }




        if (lastFrameHitRigidBody == raycasthit.Rigidbody)
        {
            Debug.Log("lastFrameHitRigidBody == raycasthit.Rigidbody");
        }
        lastFrameHitPoint = raycasthit.Point;
    }
}*/






/*
var ray = new Ray(transform.position + (-transform.up), -transform.up);
//ray = new Ray(footTarget.position, -transform.up);
RaycastHit hit;
//RaycastHit hittwo;
//Debug.DrawRay(lastFramePosition, -transform.up * 0.5f, Color.magenta, 0.001f);

//if (Physics.Raycast(ray, out hit, totallegLength, layerMask))
//var raycasthit = Raycast(ray, 0.5f, null);


float fraction;
Jitter.Dynamics.RigidBody body;
JVector normal;

bool someResult = JPhysics.collisionSystem.Raycast(JitterExtensions.ToJVector(transform.position), JitterExtensions.ToJVector(-transform.up), RaycastCallback, out body, out normal, out fraction);

/*if (raycasthit.Rigidbody.Shape.rIndex != this.transform.)
{

}


if (someResult)
{
    Debug.Log("hit1");
}
if (body!= null)
{
    Debug.Log("hit2");
}
//Debug.Log("fraction:"+ fraction );


var raycasthit = Raycast(ray, 0.1f, null);

if (raycasthit != null)
{
    if (raycasthit.Rigidbody != null)//Physics.Raycast(ray, out hit, 0.1f))
    {
        //raycasthit.fr
        RaycastHitVisualObject.position = raycasthit.Point;
        /*var camp = footTarget.position;

        if (fraction < 0.1f)
        {
            fraction = 0.1f;
        }
        var hitPoint = camp + (fraction * -transform.up);

        RaycastHitVisualObject.position = hitPoint;
        Debug.DrawRay(footTarget.position, hitPoint * 5, Color.cyan, 0.001f);

        //Debug.Log("dist:" + raycasthit.Distance);
        if (raycasthit.Distance < 0.5f && raycasthit.Distance != 0 &&
                  raycasthit.Point.x != footTarget.position.x &&
                  raycasthit.Point.y != footTarget.position.y &&
                  raycasthit.Point.z != footTarget.position.z)
        {

            Debug.Log("test11");

        }
        else
        {
            if (raycasthit.Distance == 0)
            {
                Debug.Log("test13");
                footTarget.position = raycasthit.Point;/// legstaticpivot.position + (originDirection * originDirectionLength);
            }
            else
            {
                if (raycasthit.Distance < 0.05f)
                {
                    if (raycasthit.Point.x != footTarget.position.x &&
                        raycasthit.Point.y != footTarget.position.y &&
                        raycasthit.Point.z != footTarget.position.z)
                    {
                        Debug.Log("test12");
                        footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
                    }
                    else
                    {
                        Debug.Log("test122:" + fraction);
                        //footTarget.position = footTarget.position + raycasthit.Point;






                    }
                }          
            }
        }
    }
    else
    {
        Debug.Log("test10");
        footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
    }
}*/






/*
//Debug.Log("index0:" + raycasthit.Rigidbody.Shape.rIndex);
if (raycasthit.Distance < 0.1f && raycasthit.Distance != 0 &&
    raycasthit.Point.x != footTarget.position.x &&
    raycasthit.Point.y != footTarget.position.y &&
    raycasthit.Point.z != footTarget.position.z)
{
    Vector3 tempDir = lastFramePosition - legstaticpivot.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

    //IdleStandingTargetPositionVariableLength
    //Debug.Log("index1:" + raycasthit.Rigidbody.Shape.rIndex);

    if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
    {
        Debug.Log("test2");
        /*var originDirection = initialPivotPosition - legstaticpivot.position;
        var originDirectionLength = originDirection.magnitude;

        //foottarget.position = IdleStandingTargetPositionMax;
        tempDir.Normalize();
        //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
        Vector3 tempVect = (legstaticpivot.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
        //MOVINGPOINTER.X = tempVect.X;
        //MOVINGPOINTER.Y = tempVect.Y;
        //MOVINGPOINTER.Z = tempVect.Z;
        foottarget.position = tempVect; // raycasthit.Point;

        footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
    }
    else
    {
        /*if (tempDir.magnitude > 0.5f)
        {
            var forwardx = legstaticpivot.forward.x * (tempDir.x * foot.localScale.y);
            var forwardy = legstaticpivot.forward.y * (tempDir.y * foot.localScale.y);
            var forwardz = legstaticpivot.forward.z * (tempDir.z * foot.localScale.y);
            foottarget.position = legstaticpivot.position - (new Vector3(forwardx, forwardy, forwardz));// raycasthit.Point + (tempDir * foot.localScale.y);
        }

        var originDirection = initialPivotPosition - legstaticpivot.position;
        var originDirectionLength = originDirection.magnitude;

        //var forwardx = legstaticpivot.forward.x + (tempDir.x);
        //var forwardy = legstaticpivot.forward.y + (tempDir.y);
        //var forwardz = legstaticpivot.forward.z + (tempDir.z);

        //footTarget.position = legstaticpivot.position + (new Vector3(forwardx, forwardy, forwardz));
        var distanceToHitPoint = (raycasthit.Point - legstaticpivot.position).magnitude;
        RaycastHitVisualObject.position = raycasthit.Point;

        //Debug.Log("distanceToHitPoint:" + distanceToHitPoint + " raycasthit.Distance:" + raycasthit.Distance);
        if (distanceToHitPoint < 0.5f)
        {
            Debug.Log("test0");
            var forwardx = legstaticpivot.forward.x * (distanceToHitPoint);
            var forwardy = legstaticpivot.forward.y * (distanceToHitPoint);
            var forwardz = legstaticpivot.forward.z * (distanceToHitPoint);
            footTarget.position = legstaticpivot.position + (new Vector3(forwardx, forwardy, forwardz));
        }
        else
        {
            Debug.Log("test1");
            footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);

        }

        /*var distanceToHitPoint = (raycasthit.Point - legstaticpivot.position).magnitude;
        var forwardx = legstaticpivot.forward.x * (distanceToHitPoint);
        var forwardy = legstaticpivot.forward.y * (distanceToHitPoint);
        var forwardz = legstaticpivot.forward.z * (distanceToHitPoint);
        footTarget.position = legstaticpivot.position + (new Vector3(forwardx, forwardy, forwardz));
        Debug.Log("distanceToHitPoint:" + distanceToHitPoint);
    }
}
else
{
    /*if (raycasthit.Point.x == footTarget.position.x &&
        raycasthit.Point.y == footTarget.position.y &&
        raycasthit.Point.z == footTarget.position.z)
    {
        Debug.Log("***TEST***");
    }

    var distanceToHitPoint = (raycasthit.Point - legstaticpivot.position).magnitude;


    //Debug.Log("1distanceToHitPoint:" + distanceToHitPoint + " raycasthit.Distance:" + raycasthit.Distance);
    if (distanceToHitPoint < 0.5f)
    {
        RaycastHitVisualObject.position = raycasthit.Point;
        Debug.Log("test00");//* foot.localScale.y

        var norm = raycasthit.Normal;

        var someProjVec = Vector3.ProjectOnPlane(norm, (raycasthit.Point - legstaticpivot.position));
        Debug.DrawRay(footTarget.position, someProjVec * 5, Color.cyan, 0.001f);


        /*var forwardx = legstaticpivot.forward.x + (distanceToHitPoint);
        var forwardy = legstaticpivot.forward.y + (distanceToHitPoint);
        var forwardz = legstaticpivot.forward.z + (distanceToHitPoint);
        footTarget.position = legstaticpivot.position + (new Vector3(forwardx, forwardy, forwardz));
    }
    else
    {
        Debug.Log("test11");
        footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);

    }

    //Debug.Log("test2");
    //footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
}*/
