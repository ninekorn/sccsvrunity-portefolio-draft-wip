using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class IKSC : MonoBehaviour
{
    public int swtcForMovingTarget = 0;
    //0 == move target
    //1 == don't move target


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
        dirFromUpperArmToHandTarget = handTarget.transform.position - upperArm.transform.position;
        dirUpperArmToElbowTarget = elbowTarget.transform.position - upperArm.transform.position;
        dirElbowTargetToHandTarget = handTarget.position - elbowTarget.position;
        var dirLowerArmToHandTarget = handTarget.transform.position - foreArm.transform.position;


        float distshoulderToHandtarget = Vector3.Distance(shoulder.position, handTarget.position);
        distshoulderToHandtarget = Mathf.Min(distshoulderToHandtarget, totalArmLength - totalArmLength * 0.001f); //from the youtuber ProgramYourFace IK


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


        //
        /*if (elbowposition.x == null || elbowposition.y == null || elbowposition.z == null)
        {

        }
        pointer0.position = elbowposition;
        pointer1.position = shoulder.position + (crosssss);
        pointer2.position = elbowTarget.position + (crosssss);*/









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









