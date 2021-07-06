using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myMath3d
{


    public static Vector3 RotateVector2D(Vector3 oldDirection, float angle)
    {
        float newX = Mathf.Cos(angle * Mathf.Deg2Rad) * (oldDirection.x) - Mathf.Sin(angle * Mathf.Deg2Rad) * (oldDirection.y);
        float newY = Mathf.Sin(angle * Mathf.Deg2Rad) * (oldDirection.x) + Mathf.Cos(angle * Mathf.Deg2Rad) * (oldDirection.y);
        float newZ = oldDirection.z;
        return new Vector3(newX, newY, newZ);
    }

    public static float getAngleIn3dWithAtan2(Vector3 dir1, Vector3 dir2)
    {
        float angle = Mathf.Atan2((Vector3.Cross(dir1, dir2).magnitude), Vector3.Dot(dir1, dir2));
        return angle;
    }

    public static Quaternion rotateTowards(Transform currentTransform, Transform targetTransform, Vector3 forward, float speed)
    {
        Vector3 vectorToTarget = -(targetTransform.position - currentTransform.position);
        float angle = -Mathf.Atan2(vectorToTarget.x, vectorToTarget.y) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, forward);
        return Quaternion.Slerp(currentTransform.rotation, q, Time.deltaTime * speed);
    }

    public static Quaternion LookAt(Vector3 sourcePoint, Vector3 destPoint)
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














    public static float getAngle(Transform currentTransform, Vector3 targetPosition)
    {
        // the position to compare with
        //var targetPosition = new Vector3();
        // use my gameobject's transform to determine fwd vector to target
        var localTarget = currentTransform.InverseTransformPoint(targetPosition);
        // Use Trig to get my Angle IN RANGE -180 to 180
        var targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        return targetAngle;
    }



    public static Vector2 CartesianToPolar(Vector3 point)
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

    public static Vector3 PolarToCartesian(Vector2 polar)
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

    public static float angleBetween(Transform currentTransform, Transform targetTransform)
    {
        var angle = Vector3.Angle(Vector3.ProjectOnPlane(currentTransform.forward, Vector3.up).normalized,
                    Vector3.ProjectOnPlane(targetTransform.position - currentTransform.position, Vector3.up).normalized);
        return angle;
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
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
        if (angle > 180)
        {
            return angle - 360f;
        }
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

    public static Quaternion FromToRotation(Vector3 v1, Vector3 v2, float multiplier)
    {
        return Quaternion.AngleAxis(Vector3.Angle(v1, v2) * multiplier, Vector3.Cross(v2, v1));
    }

    public static float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
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

    public static int[] SortAndIndex<T>(T[] rg)
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
