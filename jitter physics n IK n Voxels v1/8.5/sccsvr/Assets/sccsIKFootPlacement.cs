using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Threading;
using Jitter;
using Jitter.Collision;
using Jitter.Dynamics;
using Jitter.Dynamics.Constraints;
using Jitter.LinearMath;
using UnityEngine;
using Material = Jitter.Dynamics.Material;
using System.Collections;
using RigidBody = Jitter.Dynamics.RigidBody;


public class sccsIKFootPlacement : MonoBehaviour
{


    float raylength = 0;
    //float raycounter = 0;
    //float raycounterMax = 10;
    public float raycounterLoopMax = 10;
    float raycounterSwtc = 0;
    public float touchdowndistance = 0.25f;

    public Jitter.Dynamics.RigidBody lastFrameHitRigidBody;
    public Vector3 lastFrameHitNormal;
    public Vector3 lastFrameHitPoint;
    public int lastFrameHitrIndex;
    public float lastFrameHitRayLength = 0;



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

    // Use this for initialization
    void Start()
    {

        initialPivotPosition = this.transform.position;
        lastFramePosition = transform.position;
        originDirection = initialPivotPosition - legstaticpivot.position;
        originDirectionLength = originDirection.magnitude;
        originDirection.Normalize();



        upperleglength = upperleg.localScale.z;
        lowerleglength = lowerleg.localScale.z;
        footlength = foot.localScale.z;
        totallegLength = upperleglength + lowerleglength + footlength;

        IdleStandingTargetPositionMax = transform.position + ((transform.forward * upperleglength) + (transform.forward * lowerleglength) + (transform.forward * footlength));
        IdleStandingTargetPositionMin = transform.position + ((transform.forward * (upperleglength)) + (transform.forward * (lowerleglength)) + (transform.forward * (footlength)) * 0.5f);
        


        //retAdd.localScale = retAdd.localScale * planeSize;
        //retDel.localScale = retDel.localScale * planeSize;
        /*fraction = 1 / planeSize;
        radius = planeSize / 2;
        diameter = 0.1f;
        whatever = 1 / (diameter * 2);
        cam = Camera.main;*/
    }

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
        if (InitcounterForIkFootPlacementSwtc == 1)
        {


            if (counterForIkFootPlacement <= counterForIkFootPlacementMax)
            {
                var ray = new Ray(transform.position, transform.forward);

                RaycastHit hit;
                //Debug.DrawRay(transform.position, transform.forward * 3, Color.green, 0.001f);

                ray = new Ray(legstaticpivot.position, transform.forward);

                //RaycastHit hittwo;
                //Debug.DrawRay(transform.position, transform.forward * 3, Color.green, 0.001f);

                var raycasthit = Raycast(ray, 10, null);

                if (raycasthit != null)
                {
                    //Debug.Log("raycasthit != null");
                    //Debug.DrawRay(raycasthit.Point, transform.right * (raylength * 0.1f), Color.yellow, 0.0001f);

                    RaycastHitVisualObject.position = raycasthit.Point;

                    if (raycasthit.Rigidbody != null && raycasthit.Rigidbody.Shape.rIndex != -1)//Physics.Raycast(ray, out hit, 0.1f))
                    {
                        //if (Physics.Raycast(ray, out hit, totallegLength, layerMask))
                        {
                            //if (raycasthit.transform.tag == "collisionObject")
                            {
                                Vector3 tempDir = legstaticpivot.position - footTarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

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



                                /*if (tempDir.magnitude < (totallegLength * 0.5f))
                                {
                                    foottarget.position = IdleStandingTargetPositionMin;
                                }
                                else
                                {
                                    foottarget.position = hit.point;
                                }*/

                            }
                        }
                    }
                }
                counterForIkFootPlacement = 0;
            }
            counterForIkFootPlacement++;
        }
    }
}














        /*
                        if (raycasthit != null)
                        {
                            //Debug.Log("raycasthit != null");
                            //Debug.DrawRay(raycasthit.Point, transform.right * (raylength * 0.1f), Color.yellow, 0.0001f);

                            RaycastHitVisualObject.position = raycasthit.Point;

                            if (raycasthit.Rigidbody != null && raycasthit.Rigidbody.Shape.rIndex != -1)//Physics.Raycast(ray, out hit, 0.1f))
                            {
                                //Debug.Log("rIndex:" + raycasthit.Rigidbody.Shape.rIndex);
                                if (raycasthit.Rigidbody.Position.X == footTarget.position.x &&
                                    raycasthit.Rigidbody.Position.Y == footTarget.position.y &&
                                    raycasthit.Rigidbody.Position.Z == footTarget.position.z)
                                {
                                    Debug.Log("same position");
                                }


                                var someCompTwo = this.transform.GetComponent<JRigidBody>();

                                if (someCompTwo == null)
                                {
                                    //Debug.Log("null1");
                                }
                                else
                                {
                                    Debug.Log("index:" + this.transform.GetComponent<JRigidBody>().rIndex);
                                    if (this.transform.GetComponent<JRigidBody>().rIndex != raycasthit.Rigidbody.Shape.rIndex)
                                    {
                                        //RaycastHitVisualObject.position = raycasthit.Point;


                                        if (lastFrameHitRigidBody == raycasthit.Rigidbody)
                                        {
                                            if (lastFrameHitNormal == raycasthit.Normal)
                                            {
                                                Debug.Log("lastFrameHitNormal == raycasthit.Normal");
                                            }
                                            else
                                            {
                                                Debug.Log("lastFrameHitNormal != raycasthit.Normal");
                                            }
                                            if (lastFrameHitPoint == raycasthit.Point)
                                            {
                                                Debug.Log("lastFrameHitPoint == raycasthit.Point");
                                            }
                                            else
                                            {
                                                Debug.Log("lastFrameHitPoint != raycasthit.Point");
                                            }
                                        }
                                        else
                                        {
                                            Debug.Log("lastFrameHitRigidBody != raycasthit.Rigidbody");

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
                            }
                            else
                            {
                                Debug.Log("hitrIndex:" + raycasthit.Rigidbody.Shape.rIndex);
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



        lastFramePosition = transform.position;
    }*/




//RaycastHitVisualObject.position = JitterExtensions.ToVector3(raycasthit.Rigidbody.Position);
//Debug.Log(raycasthit.Rigidbody.Shape.rIndex + "_");
/*if (raycasthit.Rigidbody.rIndex != this.transform.GetComponent<JRigidBody>().rigidIndex)
{

}*/

//Debug.Log(this.transform.GetComponent<JRigidBody>().rigidIndex);


/*var someComp = this.transform.GetComponent<JRigidBody>().body;

if (someComp == null)
{
    Debug.Log("null0");

}
else
{
    Debug.Log("!null0");
}*/



/*
if (counterForIkFootPlacement >= counterForIkFootPlacementMax)
{
    if (raycounterSwtc == 0 || raycounterSwtc == 1)
    {
        if (raylength < raycounterLoopMax)
        {
            var ray = new Ray(transform.position, -transform.up * (raylength));
            //var ray = new Ray(transform.position, -transform.up);
            var raycasthit = Raycast(ray, raylength, null);

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

//Debug.DrawRay(transform.position, -transform.up * (raylength * 0.1f), Color.magenta, 0.0001f);


/*if (raycounterSwtc == 1)
{
    RaycastHitVisualObject.position = lastFrameHitPoint;
    this.transform.position = RaycastHitVisualObject.position;


    var ray = new Ray(transform.position, -transform.up * ((lastFrameHitRayLength * 0.85f) * 0.25f));
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
