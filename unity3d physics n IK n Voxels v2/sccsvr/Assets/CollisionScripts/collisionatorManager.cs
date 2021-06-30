using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Timers;

public class meshManager
{
    int index;
    public meshManager(int indexor)
    {
        index = indexor;
    }
}

public class collisionatorManager : MonoBehaviour {


    meshManager[] meshManage;
    bool startDeform = false;
    //public GameObject collPoint;
    public LayerMask layerMask = -1;

    void Start ()
    {
		
	}
	
	void Update ()
    {		
        if (startDeform == true)
        {

        }
	}

    float forceApplied = 0f;
    Vector3[] vertices;
    int[] triangles;

    public void collisionForceToApply(Transform objectToApplyForce, Vector3 collisionPoint, Vector3 velocity, Vector3 directionOfCollision, Vector3 verticePointOfOtherObject, int startTriangleIndex, int verticeColIndex, Transform objectThatCollidesWithMe,RaycastHit hitPoint)
    {

        for (int i = 0; i < verticesManager.arrayOfObjectsData.Length;i++)
        {
            if (verticesManager.arrayOfObjectsData[i].nameOfObject == objectToApplyForce.name)
            {
                vertices = verticesManager.arrayOfObjectsData[i].verticesList;
                triangles = verticesManager.arrayOfObjectsData[i].triangleList;
            }
        }




        //Vector3[] vertices = objectToApplyForce.transform.GetComponent<MeshFilter>().mesh.vertices;
        //int[] triangles = objectToApplyForce.transform.GetComponent<MeshFilter>().mesh.triangles;

        Vector3 p0 = vertices[triangles[hitPoint.triangleIndex * 3 + 0]];
        Vector3 p1 = vertices[triangles[hitPoint.triangleIndex * 3 + 1]];
        Vector3 p2 = vertices[triangles[hitPoint.triangleIndex * 3 + 2]];


        /*Vector3[] vertices = objectToApplyForce.transform.GetComponent<MeshFilter>().mesh.vertices;
        int[] triangles = objectToApplyForce.transform.GetComponent<MeshFilter>().mesh.triangles;

        Vector3 p0 = vertices[triangles[startTriangleIndex * 3 + 0]];
        Vector3 p1 = vertices[triangles[startTriangleIndex * 3 + 1]];
        Vector3 p2 = vertices[triangles[startTriangleIndex * 3 + 2]];*/

        p0 = objectToApplyForce.TransformPoint(p0);
        p1 = objectToApplyForce.TransformPoint(p1);
        p2 = objectToApplyForce.TransformPoint(p2);

        //Vector3[] verticesOfObjectThatCollidesWithMe = objectThatCollidesWithMe.transform.GetComponent<MeshFilter>().mesh.vertices;
        //int[] trianglesOfObjectThatCollidesWithMe = objectThatCollidesWithMe.transform.GetComponent<MeshFilter>().mesh.triangles;

        forceApplied = velocity.magnitude;
        if (forceApplied > 0.25f)
        {
            /*for (int n = 0; n < triangles.Length; n += 3)
            {
                Vector3 pointOne = vertices[triangles[0 + n]];
                Vector3 pointTwo = vertices[triangles[1 + n]];
                Vector3 pointThree = vertices[triangles[2 + n]];

                pointOne = objectToApplyForce.TransformPoint(pointOne);
                pointTwo = objectToApplyForce.TransformPoint(pointTwo);
                pointThree = objectToApplyForce.TransformPoint(pointThree);

                Vector3 meshObjectCenter = new Vector3(((pointOne.x + pointTwo.x + pointThree.x) / 3), ((pointOne.y + pointTwo.y + pointThree.y) / 3), ((pointOne.z + pointTwo.z + pointThree.z) / 3));

                var dir = Vector3.Cross(pointTwo - pointOne, pointThree - pointOne);
                var norm = Vector3.Normalize(dir);

                for (int i = 0; i < trianglesOfObjectThatCollidesWithMe.Length; i += 3)
                {
                    Vector3 p00 = verticesOfObjectThatCollidesWithMe[trianglesOfObjectThatCollidesWithMe[0 + i]];
                    Vector3 p01 = verticesOfObjectThatCollidesWithMe[trianglesOfObjectThatCollidesWithMe[1 + i]];
                    Vector3 p02 = verticesOfObjectThatCollidesWithMe[trianglesOfObjectThatCollidesWithMe[2 + i]];

                    p00 = objectThatCollidesWithMe.TransformPoint(p00);
                    p01 = objectThatCollidesWithMe.TransformPoint(p01);
                    p02 = objectThatCollidesWithMe.TransformPoint(p02);

                    //Vector3 meshObjectCenter0 = new Vector3(((p00.x + p01.x + p02.x) / 3), ((p00.y + p01.y + p02.y) / 3), ((p00.z + p01.z + p02.z) / 3));

                    //var dir0 = Vector3.Cross(p01 - p00, p02 - p00);
                    //var norm0 = Vector3.Normalize(dir0);

                    /*Vector3 linePoint;
                    Vector3 lineVec;

                    if (TriTriOverlap.TriTriIntersect(p00, p01, p02, pointOne, pointTwo, pointThree))
                    {
                        forceApplied = velocity.magnitude;

                        pointOne += velocity.normalized * (forceApplied * 0.1f) * 0.01f;
                        pointTwo += velocity.normalized * (forceApplied * 0.1f) * 0.01f;
                        pointThree += velocity.normalized * (forceApplied * 0.1f) * 0.01f;

                        //vertices[idx[i]] = objectToApplyForce.InverseTransformPoint(worldPt);
                        vertices[triangles[0 + n]] = objectToApplyForce.InverseTransformPoint(pointOne);
                        vertices[triangles[1 + n]] = objectToApplyForce.InverseTransformPoint(pointTwo);
                        vertices[triangles[2 + n]] = objectToApplyForce.InverseTransformPoint(pointThree);
                    }

                    //Debug.DrawLine(meshObjectCenter0, norm0 + meshObjectCenter0, Color.red, 1000f);
                    //Debug.DrawLine(meshObjectCenter, norm + meshObjectCenter, Color.red, 1000f);
                    //Debug.DrawLine(meshObjectCenter0, norm0 + meshObjectCenter0, Color.red,1000f);
                }
                //Debug.DrawLine(meshObjectCenter,norm+meshObjectCenter,Color.red,1000f);
                //Instantiate(collPoint, meshObjectCenter, Quaternion.identity);
            }*/


            forceApplied = velocity.magnitude;

            p0 += velocity.normalized * (forceApplied * 0.1f) * 0.01f;
            p1 += velocity.normalized * (forceApplied * 0.1f) * 0.01f;
            p2 += velocity.normalized * (forceApplied * 0.1f) * 0.01f;


            //p0 += velocity.normalized * 0.00025f;
            //p1 += velocity.normalized * 0.00025f;
            //p2 += velocity.normalized * 0.00025f;

            //vertices[idx[i]] = objectToApplyForce.InverseTransformPoint(worldPt);
            vertices[triangles[startTriangleIndex * 3 + 0]] = objectToApplyForce.InverseTransformPoint(p0);
            vertices[triangles[startTriangleIndex * 3 + 1]] = objectToApplyForce.InverseTransformPoint(p1);
            vertices[triangles[startTriangleIndex * 3 + 2]] = objectToApplyForce.InverseTransformPoint(p2);



            for (int i = 0; i < verticesManager.arrayOfObjectsData.Length; i++)
            {
                if (verticesManager.arrayOfObjectsData[i].nameOfObject == objectToApplyForce.name)
                {
                    verticesManager.arrayOfObjectsData[i].verticesList = vertices;
                }
            }

            MeshCollider col = objectToApplyForce.transform.GetComponent<MeshCollider>();


            Mesh mesh = new Mesh();

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            //col.sharedMesh = mesh;

            //col.sharedMesh.vertices = vertices;
            //col.sharedMesh.uv = cmesh.uv;
            //col.sharedMesh.triangles = triangles;
            //col.sharedMesh.normals = cmesh.normals;
            //col.sharedMesh.RecalculateBounds();

            Destroy(objectToApplyForce.transform.GetComponent<MeshCollider>());
            //objectToApplyForce.transform.GetComponent<MeshFilter>().mesh = mesh;
            // col.sharedMesh = mesh;
            //objectToApplyForce.GetComponent<MeshFilter>().mesh = null;
            objectToApplyForce.GetComponent<MeshFilter>().mesh = mesh;
            //objectToApplyForce.GetComponent<MeshFilter>().sharedMesh = mesh;
            objectToApplyForce.GetComponent<MeshFilter>().mesh.RecalculateNormals();
            objectToApplyForce.transform.gameObject.AddComponent<MeshCollider>();
        }
    }














    public static bool PlanePlaneIntersection(out Vector3 linePoint, out Vector3 lineVec, Vector3 plane1Normal, Vector3 plane1Position, Vector3 plane2Normal, Vector3 plane2Position)
    {
        linePoint = Vector3.zero;
        lineVec = Vector3.zero;

        //We can get the direction of the line of intersection of the two planes by calculating the 
        //cross product of the normals of the two planes. Note that this is just a direction and the line
        //is not fixed in space yet. We need a point for that to go with the line vector.
        lineVec = Vector3.Cross(plane1Normal, plane2Normal);

        //Next is to calculate a point on the line to fix it's position in space. This is done by finding a vector from
        //the plane2 location, moving parallel to it's plane, and intersecting plane1. To prevent rounding
        //errors, this vector also has to be perpendicular to lineDirection. To get this vector, calculate
        //the cross product of the normal of plane2 and the lineDirection.		
        Vector3 ldir = Vector3.Cross(plane2Normal, lineVec);

        float denominator = Vector3.Dot(plane1Normal, ldir);

        //Prevent divide by zero and rounding errors by requiring about 5 degrees angle between the planes.
        if (Mathf.Abs(denominator) > 0.006f)
        {
            Vector3 plane1ToPlane2 = plane1Position - plane2Position;
            float t = Vector3.Dot(plane1Normal, plane1ToPlane2) / denominator;
            linePoint = plane2Position + t * ldir;
            return true;
        }

        //output not valid
        else
        {
            return false;
        }
    }





    static int[] SortAndIndex<T>(T[] rg)
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



    public static bool AreLineSegmentsCrossing(Vector3 pointA1, Vector3 pointA2, Vector3 pointB1, Vector3 pointB2)
    {

        Vector3 closestPointA;
        Vector3 closestPointB;
        int sideA;
        int sideB;

        Vector3 lineVecA = pointA2 - pointA1;
        Vector3 lineVecB = pointB2 - pointB1;

        bool valid = ClosestPointsOnTwoLines(out closestPointA, out closestPointB, pointA1, lineVecA.normalized, pointB1, lineVecB.normalized);

        //lines are not parallel
        if (valid)
        {

            sideA = PointOnWhichSideOfLineSegment(pointA1, pointA2, closestPointA);
            sideB = PointOnWhichSideOfLineSegment(pointB1, pointB2, closestPointB);

            if ((sideA == 0) && (sideB == 0))
            {

                return true;
            }

            else
            {

                return false;
            }
        }

        //lines are parallel
        else
        {

            return false;
        }
    }

    public static bool ClosestPointsOnTwoLines(out Vector3 closestPointLine1, out Vector3 closestPointLine2, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
    {

        closestPointLine1 = Vector3.zero;
        closestPointLine2 = Vector3.zero;

        float a = Vector3.Dot(lineVec1, lineVec1);
        float b = Vector3.Dot(lineVec1, lineVec2);
        float e = Vector3.Dot(lineVec2, lineVec2);

        float d = a * e - b * b;

        //lines are not parallel
        if (d != 0.0f)
        {

            Vector3 r = linePoint1 - linePoint2;
            float c = Vector3.Dot(lineVec1, r);
            float f = Vector3.Dot(lineVec2, r);

            float s = (b * f - c * e) / d;
            float t = (a * f - c * b) / d;

            closestPointLine1 = linePoint1 + lineVec1 * s;
            closestPointLine2 = linePoint2 + lineVec2 * t;

            return true;
        }

        else
        {
            return false;
        }
    }
    public static int PointOnWhichSideOfLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {

        Vector3 lineVec = linePoint2 - linePoint1;
        Vector3 pointVec = point - linePoint1;

        float dot = Vector3.Dot(pointVec, lineVec);

        //point is on side of linePoint2, compared to linePoint1
        if (dot > 0)
        {

            //point is on the line segment
            if (pointVec.magnitude <= lineVec.magnitude)
            {

                return 0;
            }

            //point is not on the line segment and it is on the side of linePoint2
            else
            {

                return 2;
            }
        }

        //Point is not on side of linePoint2, compared to linePoint1.
        //Point is not on the line segment and it is on the side of linePoint1.
        else
        {

            return 1;
        }
    }
    public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
    {

        Vector3 lineVec3 = linePoint2 - linePoint1;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parrallel
        if (Mathf.Abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
            intersection = linePoint1 + (lineVec1 * s);
            return true;
        }
        else
        {
            intersection = Vector3.zero;
            return false;
        }
    }


}










/*for (int i = 0; i < trianglesOfObjectThatCollidesWithMe.Length; i+=3)
  {
      //Vector3 worldPt = objectThatCollidesWithMe.TransformPoint(verticesOfObjectThatCollidesWithMe[i]);
      Vector3 verticeOfContact = verticesOfObjectThatCollidesWithMe[verticeColIndex];
      RaycastHit rayHit;

      Vector3 p00 = verticesOfObjectThatCollidesWithMe[trianglesOfObjectThatCollidesWithMe[0 + i]];
      Vector3 p01 = verticesOfObjectThatCollidesWithMe[trianglesOfObjectThatCollidesWithMe[1 + i]];
      Vector3 p02 = verticesOfObjectThatCollidesWithMe[trianglesOfObjectThatCollidesWithMe[2 + i]];

      p00 = objectThatCollidesWithMe.TransformPoint(p00);
      p01 = objectThatCollidesWithMe.TransformPoint(p01);
      p02 = objectThatCollidesWithMe.TransformPoint(p02);

      for (int j = 0; j < triangles.Length; j += 3)
      {
          //Vector3 worldPt = objectThatCollidesWithMe.TransformPoint(verticesOfObjectThatCollidesWithMe[i]);

          Vector3 p000 = vertices[triangles[0 + j]];
          Vector3 p001 = vertices[triangles[1 + j]];
          Vector3 p002 = vertices[triangles[2 + j]];

          p000 = objectToApplyForce.TransformPoint(p000);
          p001 = objectToApplyForce.TransformPoint(p001);
          p002 = objectToApplyForce.TransformPoint(p002);


          //Debug.DrawLine(p00, p01, Color.red, 1000f);
          //Debug.DrawLine(p01, p02, Color.red, 1000f);
          //Debug.DrawLine(p02, p00, Color.red, 1000f);
          if (AreLineSegmentsCrossing(p000,p001,p00,p01))
          {

              forceApplied = velocity.magnitude;
              p000 += velocity.normalized * forceApplied * 0.0001f;
              p001 += velocity.normalized * forceApplied * 0.0001f;

              vertices[triangles[0 + j]] = objectToApplyForce.InverseTransformPoint(p000);
              vertices[triangles[1 + j]] = objectToApplyForce.InverseTransformPoint(p001);
          }

          if (AreLineSegmentsCrossing(p001, p002, p01, p02))
          {


              forceApplied = velocity.magnitude;
              p001 += velocity.normalized * forceApplied * 0.0001f;
              p002 += velocity.normalized * forceApplied * 0.0001f;

              vertices[triangles[1 + j]] = objectToApplyForce.InverseTransformPoint(p001);
              vertices[triangles[2 + j]] = objectToApplyForce.InverseTransformPoint(p002);
          }

          if (AreLineSegmentsCrossing(p002, p000, p02, p00))
          {

              forceApplied = velocity.magnitude;
              p000 += velocity.normalized * forceApplied * 0.0001f;
              p002 += velocity.normalized * forceApplied * 0.0001f;

              vertices[triangles[0 + j]] = objectToApplyForce.InverseTransformPoint(p000);
              vertices[triangles[2 + j]] = objectToApplyForce.InverseTransformPoint(p002);
          }
      }
  }*/























//Debug.DrawLine(p00, p01, Color.red, 1000f);
//Debug.DrawLine(p01, p02, Color.red, 1000f);
//Debug.DrawLine(p02, p00, Color.red, 1000f);

/*public void collisionForceToApply(Transform objectToApplyForce, Vector3 collisionPoint, Vector3 velocity, Vector3 directionOfCollision, Vector3 collisionPointOfOtherObject)
{
    Vector3[] vertices = objectToApplyForce.transform.GetComponent<MeshFilter>().mesh.vertices;
    int[] triangles = objectToApplyForce.transform.GetComponent<MeshFilter>().mesh.triangles;
    float[] verticesDistanceToCollision = new float[vertices.Length];
    meshManage = new meshManager[vertices.Length];
    MeshCollider col = objectToApplyForce.transform.GetComponent<MeshCollider>();
    Mesh mesh = new Mesh();
    float[] indexOfVertices = new float[vertices.Length];

    for (int i = 0; i < vertices.Length; i++)
    {
        Vector3 worldPt = objectToApplyForce.TransformPoint(vertices[i]);
        verticesDistanceToCollision[i] = Vector3.Distance(collisionPoint, worldPt);
    }

    int[] idx = SortAndIndex(verticesDistanceToCollision);

    forceApplied = velocity.magnitude;

    for (int i = 0; i < idx.Length; i++)
    {
        Vector3 worldPt = objectToApplyForce.TransformPoint(vertices[idx[i]]);
        worldPt += velocity.normalized * forceApplied * 0.01f;
        vertices[idx[i]] = objectToApplyForce.InverseTransformPoint(worldPt);
        if (forceApplied > 0)
        {
            forceApplied -= 0.01f;
        }
    }

    mesh.vertices = vertices;
    mesh.triangles = triangles;
    col.sharedMesh = mesh;
    objectToApplyForce.GetComponent<MeshFilter>().mesh = null;
    objectToApplyForce.GetComponent<MeshFilter>().mesh = mesh;

    objectToApplyForce.GetComponent<MeshFilter>().mesh.RecalculateNormals();
    Debug.Log("test");
}*/













/*if (Physics.Raycast(p00, p01 - p00, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p01, p02 -p01, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if(Physics.Raycast(p02, p00 - p02, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }


       if (Physics.Raycast(p01, p00 -p01, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if(Physics.Raycast(p02, p01 -p02, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if(Physics.Raycast(p00, p02 -p00 , out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if(Physics.Raycast(p00, p00 - p01, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if(Physics.Raycast(p01, p01 - p02, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if(Physics.Raycast(p02, p02 - p00, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }


      if(Physics.Raycast(p01, p01 - p00, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if(Physics.Raycast(p02, p02 - p01, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if(Physics.Raycast(p00, p00 - p02, out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p00, -(p01 - p00), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p01, -(p02 - p01), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p02, -(p00 - p02), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }


      if (Physics.Raycast(p01, -(p00 - p01), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p02, -(p01 - p02), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p00, -(p02 - p00), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p00, -(p00 - p01), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p01, -(p01 - p02), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p02, -(p02 - p00), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }


      if (Physics.Raycast(p01, -(p01 - p00), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p02, -(p02 - p01), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }

      if (Physics.Raycast(p00, -(p00 - p02), out rayHit, Mathf.Infinity, layerMask))
      {
          Instantiate(collPoint, rayHit.point, Quaternion.identity);
      }*/
