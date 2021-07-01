//learned first from programyourface and then i ended up learning myself on wolfram circlecircleintersection

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSCLeft : MonoBehaviour
{
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
    //public GameObject rotationPoint;
    float lengthFromUpperToEndForeArm;
    //public GameObject HandParentObject;
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

    Vector3 dirRealElbowToHandTarget;
    Vector3 dirRealForeArmToHand;


    void Start()
    {
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
        //hand.transform.rotation = HandParentObject.transform.rotation;
        //hand.transform.rotation = HandParentObject.transform.rotation;
        //hand.transform.position = HandParentObject.transform.position;

        Vector3 currentMovement = transform.position - previousPos;

        dirFromUpperArmToHandTarget = handTarget.transform.position - upperArm.transform.position;
        dirUpperArmToElbowTarget = elbowTarget.transform.position - upperArm.transform.position;
        dirElbowTargetToHandTarget = handTarget.position - elbowTarget.position;
        dirRealElbowToHandTarget = handTarget.position - foreArm.position;
        dirRealForeArmToHand = hand.position - foreArm.position;

        targetDistance = Vector3.Distance(upperArm.position, handTarget.position);
        targetDistance = Mathf.Min(targetDistance, totalArmLength - totalArmLength * 0.001f);

        float adjacent = ((targetDistance * targetDistance) - (lengthForeArm * lengthForeArm) + (lengthUpperArm * lengthUpperArm)) / (2 * targetDistance); //circlecircleintersectionwolfram

        float anotherSide = Mathf.Sqrt((lengthUpperArm * lengthUpperArm) - (adjacent * adjacent));

        Vector3 newDirec = dirFromUpperArmToHandTarget.normalized * adjacent;

        float xnewDir = Mathf.Round(newDirec.x * 100) / 100;
        float ynewDir = Mathf.Round(newDirec.y * 100) / 100;
        float znewDir = Mathf.Round(newDirec.z * 100) / 100;

        Vector3 newDirFloored = new Vector3(xnewDir, ynewDir, znewDir);

        endPoint3 = upperArm.position + (newDirec.normalized * adjacent);
        crosssss = Vector3.Cross(newDirec, dirUpperArmToElbowTarget); ////                                                                                          
        crosser = -Vector3.Cross(newDirec, crosssss); ////
        endPoint4 = endPoint3 + (crosser.normalized * anotherSide);

        Vector3 lastoDir = endPoint4 - upperArm.position;

        Quaternion rotation = Quaternion.LookRotation(lastoDir, dirFromUpperArmToHandTarget);
        transform.rotation = rotation; // PIVOT OF UPPERARM

        //Quaternion rotter = Quaternion.LookRotation(dirRealElbowToHandTarget, -crosser);
        Quaternion rotter = Quaternion.LookRotation(dirRealElbowToHandTarget, -hand.right);

        //Debug.DrawRay(hand.position, -hand.right, Color.green, 0.1f);

        foreArm.transform.rotation = rotter; // PIVOT OF UPPERARM
        //foreArm.transform.LookAt(handTarget);

        Debug.DrawRay(hand.position, -hand.right, Color.green, 0.1f);



        //Debug.DrawRay(endPoint3, crosser.normalized * anotherSide, Color.red, 0.1f);
        //Debug.DrawRay(upperArm.transform.position, newDirec, Color.red, 0.1f);
        Debug.DrawRay(endPoint3, crosssss, Color.blue, 0.1f);
        Debug.DrawRay(endPoint3, crosser, Color.magenta, 0.1f);
        Debug.DrawRay(endPoint4, lastoDir, Color.cyan, 0.1f);
        //Debug.DrawRay(endPoint4, handTarget.position - endPoint4, Color.green, 0.1f);
        //Debug.DrawRay(endPoint3, handTarget.position-endPoint3, Color.green, 0.1f);
        //Debug.DrawRay(upperArm.position, endPoint4-upperArm.position, Color.green, 0.1f);
        //Debug.DrawRay(upperArm.transform.position, dirFromUpperArmToHandTarget, Color.green, 0.1f);
        //Debug.DrawRay(elbowTarget.transform.position, dirElbowTargetToHandTarget, Color.green, 0.1f);
        //Debug.DrawRay(upperArm.transform.position, dirUpperArmToElbowTarget, Color.green, 0.1f);

        //Instantiate(rotationPoint, endPoint3, Quaternion.identity);
        //Instantiate(rotationPoint, endPoint4, Quaternion.identity);

        previousPos = transform.position;

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





