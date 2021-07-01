//https://answers.unity.com/questions/511841/how-to-make-an-object-move-away-from-three-or-more.html by entity476
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findcentroid : MonoBehaviour
{
    public Transform originCompassPivots;


    public Transform object0;
    public Transform object1;
    public Transform object2;

    public Transform centroidTransform;
    public Transform bullseye;
    List<Vector3> listOfVecs = new List<Vector3>();

    public void Start()
    {
  
    }

    public void Update()
    {
        listOfVecs.Clear();
        listOfVecs.Add(object0.transform.position);
        listOfVecs.Add(object1.transform.position);
        listOfVecs.Add(object2.transform.position);

        Vector3 centroid = FindCentroid(listOfVecs);

        centroidTransform.position = centroid;
        Debug.DrawRay(centroidTransform.position, (bullseye.position - centroidTransform.position).normalized * 1.5f, Color.gray);

        Debug.DrawRay(originCompassPivots.transform.position, (centroidTransform.position - originCompassPivots.transform.position).normalized * 1.5f, new Color(1.0f,0.65f,0.15f,1.0f));
    }

    private Vector3 FindCentroid(List<Vector3> targets)
    {
        Vector3 centroid;
        Vector3 minPoint = targets[0];
        Vector3 maxPoint = targets[0];

        for (int i = 1; i < targets.Count; i++)
        {
            Vector3 pos = targets[i];
            if (pos.x < minPoint.x)
                minPoint.x = pos.x;
            if (pos.x > maxPoint.x)
                maxPoint.x = pos.x;
            if (pos.y < minPoint.y)
                minPoint.y = pos.y;
            if (pos.y > maxPoint.y)
                maxPoint.y = pos.y;
            if (pos.z < minPoint.z)
                minPoint.z = pos.z;
            if (pos.z > maxPoint.z)
                maxPoint.z = pos.z;
        }

        centroid = minPoint + 0.5f * (maxPoint - minPoint);
        return centroid;
    }
}
