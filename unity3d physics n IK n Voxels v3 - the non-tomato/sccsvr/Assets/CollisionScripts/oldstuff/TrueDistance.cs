using UnityEngine;
using System.Collections;

public class TrueDistance : MonoBehaviour
{
    public GameObject otherObject;

    Vector3 closestSurfacePoint1;
    Vector3 closestSurfacePoint2;



    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider collider = GetComponent<Collider>();

        Collider colliderOther = otherObject.transform.GetComponent<Collider>();



        // the surface point of this collider that is closer to the position of the other collider
        closestSurfacePoint1 = collider.ClosestPointOnBounds(otherObject.transform.position);

        // the surface point of the other collider that is closer to the position of this collider
        closestSurfacePoint2 = colliderOther.ClosestPointOnBounds(transform.position);

        // the distance between the surfaces of the 2 colliders
        float surfaceDistance = Vector3.Distance(closestSurfacePoint1, closestSurfacePoint2);
        Debug.Log(surfaceDistance);
        // visualize the different distances (debug)
        Debug.DrawLine(transform.position, otherObject.transform.position, Color.yellow);
        Debug.DrawLine(closestSurfacePoint1, closestSurfacePoint2, Color.magenta);
    }
}
