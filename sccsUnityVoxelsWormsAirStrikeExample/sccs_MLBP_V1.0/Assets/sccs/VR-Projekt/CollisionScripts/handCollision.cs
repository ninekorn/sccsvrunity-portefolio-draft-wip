using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handCollision : MonoBehaviour
{

    public LayerMask layerMask = -1;
    public LayerMask layerMaskTwo = -1;

    Rigidbody handRigid;
    GameObject shadowObject;
    GameObject currentObject;
    public GameObject collPoint;
    public GameObject mainCharacterController;
    public GameObject rayCastHandPalm;


    Vector3[] vertices;
    Vector3[] rayhits;
    Vector3[] rayhitsNorms;
    Vector3[] listOfVertices;
    float[] vertDistToCol;
    Transform[] objectsCollidingWith;
    int[] triangleIndexHit;

    Vector3 previousPos;
    Vector3 movementThisStep;
    Vector3 velocity;
    Vector3 worldPt;
    RaycastHit rayHit;
    Vector3 offsetter1;

    bool stickToSurface0 = false;
    bool stickToSurface1 = false;



    bool checkDistanceToCollision = false;
    bool hasReachedTarget = false;

    int pos = 0;
    int getData = 0;
    static int counter = 0;

    Transform parent;
    Vector3 initialPosition;
    Vector3 currentPosition;

    float xPosClamped = 0;
    float yPosClamped = 0;
    float zPosClamped = 0;

    private float minimumExtent;
    private float partialExtent;
    private float sqrMinimumExtent;
    private MeshCollider myCollider;
    public float skinWidth = 0.1f;
    Vector3 lastHitNormal;
    Vector3 lastMainHitNormal;


    Transform[] childObjects;
    Vector3 previousPosOfMainControl;

    RaycastHit RayHitMain;

    Vector3 originalEulerAngles;
    Quaternion _initialRotation;
    Quaternion m_CharacterTargetRot;

    RaycastHit rayhitLookAt;
    Vector3 lastLookAtHit;
    bool storeEulerData = false;
    Vector3 storedEulerData;

    Vector3 storedRayHitPoint;
    Vector3 storedRayHitNorm;
    Quaternion lastRotation;
    //public GameObject directionObject;




    void Start()
    {      
        





        //Rigidbody leftShoulderRigid = leftShoulder.GetComponent<Rigidbody>();
        //leftShoulderRigid.isKinematic = true;
        //leftShoulderRigid.constraints = RigidbodyConstraints.FreezePosition;
        //Vector3 leftShoulderPosition = transform.position;
        //leftShoulderPosition.x = 0;










         m_CharacterTargetRot = transform.rotation;
        _initialRotation = transform.localRotation;
        currentObject = this.gameObject;
        handRigid = transform.GetComponent<Rigidbody>();

        //shadowObject = Instantiate(currentObject, transform.position, transform.rotation); /// lol ok now i understand... moron. wow
        /*Destroy(shadowObject.GetComponent<MeshCollider>());
        shadowObject.GetComponent<Rigidbody>().isKinematic = false;*/
        previousPos = transform.position;
        parent = transform.parent;
        initialPosition = transform.position;

        vertices = transform.GetComponent<MeshFilter>().mesh.vertices;
        rayhits = new Vector3[vertices.Length];
        rayhitsNorms = new Vector3[vertices.Length];
        listOfVertices = new Vector3[vertices.Length];
        vertDistToCol = new float[vertices.Length];
        objectsCollidingWith = new Transform[vertices.Length];
        triangleIndexHit = new int[vertices.Length * 3];

        myCollider = GetComponent<MeshCollider>();
        minimumExtent = Mathf.Min(Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y), myCollider.bounds.extents.z);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;

        int children = transform.childCount;
        childObjects = new Transform[children];
        for (int i = 0; i < children; i++)
        {
            childObjects[i] = transform.GetChild(i);
        }
        originalEulerAngles = transform.eulerAngles;

    }
    Quaternion rotationToKeepInData;
    Quaternion newRot;
    void Update()
    {
      





        Vector3 movementOfMainControl = mainCharacterController.transform.position - previousPosOfMainControl;
        movementThisStep = transform.position- previousPos;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        //Vector3 dirNorm = movementThisStep.normalized;
        //Debug.DrawRay(transform.position, movementThisStep*10, Color.green,0.1f);

        float fixedDist = 0.001f;

        RaycastHit hit;
 

        if (checkDistanceToCollision == false)
        {

            /*if (Physics.Raycast(transform.position, movementOfMainControl, out RayHitMain, 1f, layerMask) && RayHitMain.normal != lastMainHitNormal)
            {
                stickToSurface1 = true;
                stickToSurface0 = false;
            }*/
          
            if (Physics.Raycast(transform.position, movementThisStep, out rayHit, 0.25f, layerMask) && rayHit.normal != lastHitNormal)
            {
                lastHitNormal = rayHit.normal;
                lastMainHitNormal = RayHitMain.normal;
                lastLookAtHit = rayhitLookAt.point;

                storedRayHitPoint = rayHit.point;
                storedRayHitNorm = rayHit.normal;

                stickToSurface0 = true;
                stickToSurface1 = false;
            }

            /*if (Physics.Raycast(rayCastHandPalm.transform.position, rayCastHandPalm.transform.forward, out rayhitLookAt, 0.25f, layerMask) && Vector3.Distance(rayhitLookAt.point,lastLookAtHit)>0.5f)
            {

            }*/
            /*else
            {
                Vector3 directionTowardsParent = parent.transform.position - transform.position;
                if (!Physics.Raycast(transform.position, directionTowardsParent, out rayHit, 3, layerMask)|| !Physics.Raycast(transform.position, directionTowardsParent, out RayHitMain, 3, layerMask))
                {
                    transform.position = parent.transform.position;
                }
            }*/
        }

        //Instantiate(collPoint, rayHit.point, Quaternion.identity);
        //transform.position = Vector3.Lerp(transform.position, p, Time.deltaTime * 1000f * moveMag);

        if (stickToSurface0 == true)
        {
            float rayX = rayHit.point.x;
            float rayY = rayHit.point.y;
            float rayZ = rayHit.point.z;

            //Vector3 p = rayHit.point + (rayHit.normal * 0.03f);
            //Vector3 p = storedRayHitPoint + (storedRayHitNorm * 0.03f);

            float moveMag = movementThisStep.magnitude;

            if (moveMag < 1)
            {
                moveMag += 1;
            }

            RaycastHit newHit;
            if (Physics.Raycast(transform.position, -rayHit.normal, out newHit, 0.1f, layerMask))
            {
                //Instantiate(collPoint, newHit.point, Quaternion.identity);
                Vector3 p = newHit.point + (newHit.normal * 0.03f);
                transform.position = Vector3.Lerp(transform.position, p, 1000f * moveMag);

                if (storeEulerData == false)
                {
                    rotationToKeepInData = transform.rotation;
                    //transform.rotation = Quaternion.identity;
                    //directionObject.transform.rotation = Quaternion.identity;
                    //directionObject.transform.position = newHit.point;
                    storedEulerData = transform.eulerAngles;
                    storeEulerData = true;
                }

                //var vec = Vector3.ProjectOnPlane(newHit.point, newHit.normal);
                Vector3 perpendicular = Vector3.Cross(newHit.normal, -transform.right);
                Vector3 perpendicularLeft = Vector3.Cross(newHit.normal, -Vector3.forward);

                Debug.DrawRay(newHit.point, newHit.normal, Color.red, 0.1f); 
                Debug.DrawRay(newHit.point, perpendicular, Color.green, 0.1f);
                Debug.DrawRay(newHit.point, perpendicularLeft, Color.blue, 0.1f);

                //directionObject.transform.rotation = Quaternion.LookRotation(newHit.normal, perpendicular);

                float angle = Vector3.SignedAngle(perpendicular, perpendicularLeft, newHit.normal);
                //Debug.Log(angle);






































                //float angle = Vector3.SignedAngle(newHit.normal, Vector3.up, newHit.normal);
                //Debug.Log(angle);

                //float angle = Vector3.SignedAngle(newHit.normal, Vector3.up, newHit.normal);
                //Debug.Log(angle);

                //Quaternion originalRotation = transform.rotation;
                //Quaternion newLocalRotation = transform.localRotation;
                //transform.rotation = originalRotation;

                //Quaternion newRot = Quaternion.identity;
                //transform.rotation = newRot;

                /*Quaternion originalRotation = transform.rotation;
                Quaternion newRot = Quaternion.identity;
                transform.rotation = newRot;
                Quaternion newLocalRotation = transform.localRotation;
                transform.rotation = originalRotation;*/

                //transform.localRotation = Quaternion.Inverse(parent.rotation) * newRot;

                //directionObject.transform.position = p;
                //Vector3 perp = Vector3.Cross(newHit.normal, transform.forward);
                //Vector3 targetDir = Vector3.Project(newHit.point, perp).normalized;

                //Vector3 proj = transform.rotation*(transform.forward - (Vector3.Dot(transform.forward, newHit.normal)) * newHit.normal);
                //Vector3 crossProd = Vector3.Cross(projForward,projRight);

                //Quaternion q = Quaternion.LookRotation(proj, newHit.normal);
                //Quaternion q = Quaternion.FromToRotation(Vector3.up, hit.normal);
                //var pos1 = newHit.point + q * proj;
                //Debug.DrawRay(newHit.point, pos1, Color.red, 0.1f);


                //transform.rotation = Quaternion.LookRotation(proj, newHit.normal);




                //Quaternion rotation = Quaternion.Euler(x, y, z);
                //Vector3 myVector = Vector3.one;
                //Vector3 rotateVector = rotation * myVector;




                //Quaternion lookRotationOfDirectionVector = Quaternion.LookRotation(proj, transform.up);
                //Quaternion playerRotation = transform.rotation;
                //float gunRotAngle = Quaternion.Angle(playerRotation, lookRotationOfDirectionVector);
                //Debug.Log(gunRotAngle);






















                //Quaternion playerRotation = transform.rotation;
                //Quaternion gunRotation = Quaternion.LookRotation(proj, newHit.normal);
                //float gunRotAngle = Quaternion.Angle(playerRotation, gunRotation);
                //Debug.Log(gunRotAngle);


                //Vector3 targetDir = newHit.point - transform.position;
                //float angle = Vector3.Angle(proj, transform.up);

                //Debug.DrawRay(transform.position, proj, Color.red, 0.1f);


                /*Vector3 minDir;
                Vector3 masDir;
                minDir = transform.up;
                Quaternion quat = Quaternion.AngleAxis(0, transform.forward);
                minDir = quat * minDir;
                masDir = transform.up;
                quat = Quaternion.AngleAxis(0, transform.forward);
                masDir = quat * masDir;

                directionObject.transform.rotation = Quaternion.Euler(proj);
                Debug.DrawRay(transform.position, transform.up, Color.cyan);
                Debug.DrawRay(transform.position, minDir, Color.yellow);
                Debug.DrawRay(transform.position, masDir, Color.yellow);*/



                //float angle = Vector3.SignedAngle(proj, transform.up, newHit.normal);

                //Debug.Log(angle);






















                /*// get a "forward vector" for each rotation
                var forwardA = playerRotation * proj;
                var forwardB = gunRotation * newHit.normal;

                // get a numeric angle for each vector, on the X-Z plane (relative to world forward)
                var angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
                var angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;


                // get the signed difference in these angles
                var angleDiff = Mathf.DeltaAngle(angleA, angleB);

                Debug.Log(angleDiff);*/










                /*if (gunRotAngle >= 0 && gunRotAngle <= 90)
                {
                    transform.rotation = parent.transform.rotation;
                }
                else
                {
                    transform.rotation = lastRotation;
                }*/













                //Vector3 proj = transform.forward - (Vector3.Dot(transform.forward, rayHit.normal)) * rayHit.normal;
                //float angle = Vector3.SignedAngle(proj, transform.up, newHit.normal); // this one returns both pos and neg values wtf

                /*if (angle <= -90 && angle >= -180f)
                {
                    transform.rotation = parent.rotation;
                }
                else
                {
                    transform.rotation = lastRotation;
                }*/
                //print(angle);



                /*Quaternion rot = transform.rotation;

                float roll = Mathf.Atan2(2 * rot.y * rot.w + 2 * rot.x * rot.z, 1 - 2 * rot.y * rot.y - 2 * rot.z * rot.z);
                float pitch = Mathf.Atan2(2 * rot.x * rot.w + 2 * rot.y * rot.z, 1 - 2 * rot.x * rot.x - 2 * rot.z * rot.z);
                float yaw = Mathf.Asin(2 * rot.x * rot.y + 2 * rot.z * rot.w);

                float rollRad2Deg = roll * Mathf.Rad2Deg; ////////Y
                float pitchRad2Deg = pitch * Mathf.Rad2Deg; ///////X
                float yawRad2Deg = yaw * Mathf.Rad2Deg;///////Z      

                if (pitchRad2Deg <= 0 && pitchRad2Deg >= -90)
                {
                    transform.rotation = parent.transform.rotation;
                }
                else
                {
                    transform.rotation = lastRotation;
                }*/



                //Vector3 targetDir = newHit.point - transform.position;
                //float angle = Vector3.SignedAngle(targetDir, transform.up, newHit.normal); // this one returns both pos and neg values wtf
                //float angle = Vector3.Angle(targetDir, transform.up);/// this one returns only 0 to 180 degrees
                //float angle = AngleBetweenVector2(targetDir, transform.up); // this one return angles over 90 when its supposed to be under 90 and returns avr 100 to 120 when its supposed to be close to 180
                //float angle = AngleSigned(targetDir,transform.up,newHit.normal); //that one returns crap
                //float angle = SignedVectorAngle(targetDir,transform.up,newHit.normal);


                /* if (angle <= -90 && angle >= -180f)
                 {
                     transform.rotation = parent.rotation;
                 }
                 else
                 {
                     transform.rotation = lastRotation;
                 }*/
                //print(angle);



                /*if (angle < -5.0F)
                    print("turn left");
                else if (angle > 5.0F)
                    print("turn right");
                else
                    print("forward");
                    */



                ////Vector3 targetDir = newHit.point - transform.position;
                //float angleForward  = AngleBetweenVector2(targetDir, transform.up);
                //float newForwardAngle = AngleSigned(targetDir,transform.up,newHit.normal);
                //float angleForward = Vector3.Angle(targetDir, transform.up);


                //float angleRight = Vector3.Angle(targetDir, transform.right);

                //print(angleForward);

                /*if (angleForward <= 180f && angleForward >= 90f)
                {
                    transform.rotation = parent.transform.rotation;
                }
                else
                {
                    transform.rotation = lastRotation;
                }*/






                //Debug.DrawRay(transform.position, proj, Color.red, 0.1f);
            }



















            /*float minX = -10;
            float maxX = 5;

            float minY = -10;
            float maxY = 10;

            float minZ = -10;
            float maxZ = 10;

            //float x = transform.localEulerAngles.x;
            //float y = transform.localEulerAngles.y;
            //float z = transform.localEulerAngles.z;

            float x = ClampAngle(transform.localEulerAngles.x, storedEulerData.x + minX, storedEulerData.x + maxX);
            float y = ClampAngle(transform.localEulerAngles.y, storedEulerData.y+ minY, storedEulerData.y+ maxY);
            float z = ClampAngle(transform.localEulerAngles.z, storedEulerData.z + minZ, storedEulerData.z + maxZ);

            Vector3 desiredRot = new Vector3(x, y, z);

            transform.rotation = Quaternion.Euler(desiredRot);*/


            //transform.rotation = Quaternion.FromToRotation(transform.up, lastHitNormal) * transform.rotation;

            //transform.rotation = Quaternion.LookRotation(transform.forward, newHit.normal);

            //Vector3 proj = transform.forward /*- (Vector3.Dot(transform.forward, rayHit.normal)) * rayHit.normal*/;
            //transform.rotation = Quaternion.LookRotation(proj, rayHit.normal);

            //Quaternion rot = new Quaternion();
            //rot.SetLookRotation(transform.forward, -(newHit.point - transform.position).normalized);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10);

            Vector3 directionTowardsParent = parent.transform.position - transform.position;
            RaycastHit rayToParent;
            if (!Physics.Raycast(transform.position, directionTowardsParent, out rayToParent, 3, layerMask))
            {
                transform.position = parent.transform.position;
                transform.rotation = parent.transform.rotation;
                transform.eulerAngles = parent.transform.eulerAngles;
                //directionObject.transform.rotation = parent.transform.rotation;
                storeEulerData = false;
            }
        }





















        if (stickToSurface1 == true)
        {
            lastMainHitNormal = RayHitMain.normal;

            Vector3 p1 = RayHitMain.point + (RayHitMain.normal * 0.03f);

            float moveMag = movementOfMainControl.magnitude;

            if (moveMag < 1)
            {
                moveMag += 1;
            }
            transform.position = Vector3.Lerp(transform.position, p1, Time.deltaTime * 1000f * moveMag);

            if (storeEulerData == false)
            {
                storedEulerData = transform.eulerAngles;
                storeEulerData = true;
            }

            float minX = -10;
            float maxX = 5;

            float minY = -10;
            float maxY = 10;

            float minZ = -10;
            float maxZ = 10;
            //float x = transform.eulerAngles.x;
            //float y = transform.eulerAngles.y;
            //float z = transform.eulerAngles.z;

           /* float x = ClampAngle(transform.eulerAngles.x, storedEulerData.x + minX, storedEulerData.x + maxX);
            float y = ClampAngle(transform.eulerAngles.y, storedEulerData.y + minY, storedEulerData.y + maxY);
            float z = ClampAngle(transform.eulerAngles.z, storedEulerData.z + minZ, storedEulerData.z + maxZ);

            Vector3 desiredRot = new Vector3(x, y, z);

            ///transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(desiredRot), 10);

            transform.rotation = Quaternion.Euler(desiredRot);*/

            Vector3 directionTowardsParent = parent.transform.position - transform.position;

            if (!Physics.Raycast(transform.position, directionTowardsParent, out rayHit, 3, layerMask))
            {
                transform.position = parent.transform.position;
                //transform.rotation = parent.transform.rotation;
                //transform.eulerAngles = parent.transform.eulerAngles;
                storeEulerData = false;
            }
        }












        //lastHitNormal = rayHit.normal;
        lastRotation = transform.rotation;
        previousPos = transform.position;
        previousPosOfMainControl = mainCharacterController.transform.position;
    }








    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }







    public static float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }

    public static float CalculateAngle180_v3(Vector3 fromDir, Vector3 toDir)
    {
        float angle = Quaternion.FromToRotation(fromDir, toDir).eulerAngles.y;
        if (angle > 180) { return angle - 360f; }
        return angle;
    }




    public static float SignedVectorAngle(Vector3 referenceVector, Vector3 otherVector, Vector3 normal)
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

    public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
          Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }




    public static float AngleOffAroundAxis(Vector3 v, Vector3 forward, Vector3 axis)
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


    public static float ClampAngle(float angle, float min, float max)
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





















































/*Vector3 targetDir = newHit.point - transform.position;
float angle = Vector3.Angle(targetDir, transform.forward);
Vector3 cross = Vector3.Cross(targetDir, transform.forward);
if (cross.y< 0)
{
angle = -angle;
}

Debug.Log(angle);*/














/*Quaternion rot = transform.localRotation;

float roll = Mathf.Atan2(2 * rot.y * rot.w + 2 * rot.x * rot.z, 1 - 2 * rot.y * rot.y - 2 * rot.z * rot.z);
float pitch = Mathf.Atan2(2 * rot.x * rot.w + 2 * rot.y * rot.z, 1 - 2 * rot.x * rot.x - 2 * rot.z * rot.z);
float yaw = Mathf.Asin(2 * rot.x * rot.y + 2 * rot.z * rot.w);

float rollRad2Deg = roll * Mathf.Rad2Deg; ////////Y
float pitchRad2Deg = pitch * Mathf.Rad2Deg; ///////X
float yawRad2Deg = yaw * Mathf.Rad2Deg;///////Z

Debug.Log(pitchRad2Deg);*/












/*Vector3 minDir;
Vector3 masDir;


minDir = transform.up;
Quaternion quat = Quaternion.AngleAxis(135, transform.forward);
minDir = quat* minDir;
masDir = transform.up;
quat = Quaternion.AngleAxis(-135, transform.forward);
masDir = quat* masDir;


Debug.DrawRay(transform.position, transform.up, Color.cyan);
Debug.DrawRay(transform.position, minDir, Color.yellow);
Debug.DrawRay(transform.position, masDir, Color.yellow);*/











//Vector3 proj = transform.forward - (Vector3.Dot(transform.forward, rayHit.normal)) * rayHit.normal;
//transform.rotation = Quaternion.LookRotation(proj, rayHit.normal);


//Vector3.Lerp(transform.position, parent.transform.position, Time.deltaTime * 10f);

/*for (int i = 0; i < childObjects.Length; i++)
{
    //Vector3 movementThisStepOfChildObject = childObjects[i].position - previousPos;
    if (!Physics.Raycast(childObjects[i].position, directionTowardsParent, out rayHit, 1f, layerMask) && rayHit.normal != lastHitNormal)
    {
    }
}  */

//xPosClamped = Mathf.Clamp(transform.position.x, rayHit.point.x, transform.position.x);
//yPosClamped = Mathf.Clamp(transform.position.y, rayHit.point.y, transform.position.y);
//zPosClamped = Mathf.Clamp(transform.position.z, rayHit.point.z, transform.position.z);

//xPosClamped = Mathf.Clamp(transform.position.x, rayHit.point.x, transform.position.x);
//yPosClamped = Mathf.Clamp(transform.position.y, rayHit.point.y, transform.position.y);
//zPosClamped = Mathf.Clamp(transform.position.z, rayHit.point.z, transform.position.z);

//xPosClamped = Mathf.Clamp(transform.position.x, p.x, transform.position.x);
//yPosClamped = Mathf.Clamp(transform.position.y, p.y, transform.position.y);
//zPosClamped = Mathf.Clamp(transform.position.z, p.z, transform.position.z);

//Vector3 newPos = new Vector3(transform.position.x, yPosClamped, transform.position.z);
//Vector3 newPos = new Vector3(xPosClamped, yPosClamped, yPosClamped);


//transform.position = new Vector3(transform.position.x, yPosClamped, transform.position.z);




/*if (Physics.Raycast(transform.position, movementThisStep, out hit, 1, layerMask))
{
    Vector3 perp = Vector3.Cross(hit.normal, transform.up);
    Vector3 targetDir = Vector3.Project(hit.point, perp).normalized;

    Vector3 fixedPos = hit.point + hit.normal * fixedDist;

    Instantiate(collPoint, fixedPos, Quaternion.identity);
    //Vector3 currentDir = transform.TransformPoint(Vector3.forward) - transform.position;
    //transform.position = targetDir;
    /*RaycastHit hit2;
    if (Physics.Raycast(transform.position, -hit.normal, out hit2, 1, layerMask))
    {
        Vector3 fixedPos = hit2.point + hit.normal * fixedDist;
        Vector3 predictPos = fixedPos + targetDir;
        transform.position = predictPos;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(predictPos - transform.position), 0.05f);
    }

}*/
