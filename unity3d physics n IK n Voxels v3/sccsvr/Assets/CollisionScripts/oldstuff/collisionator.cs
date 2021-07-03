using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionator : MonoBehaviour {

    Rigidbody myRigidbody;
    Vector3 previousPos;
    public LayerMask layerMask = -1; //make sure we aren't in this layer 
    Vector3 currentPos;
    Vector3 movementThisStep;
    float movementSqrMagnitude;
    float movementMagnitude;
    Vector3 localPositionOffset;
    public GameObject collisionPoint;

    private void Start()
    {
        localPositionOffset = transform.localPosition;
        myRigidbody = transform.GetComponent<Rigidbody>();
        previousPos = myRigidbody.position;
    }

    private void FixedUpdate()
    {
        currentPos = myRigidbody.position;
        movementThisStep = myRigidbody.position - previousPos;
        //Vector3 currentDirection = previousPos - transform.position;

        movementSqrMagnitude = movementThisStep.sqrMagnitude;
        movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);

        RaycastHit hitInfo;

        myRigidbody.isKinematic = true;

        Vector3 centerOfCube = transform.GetComponent<Collider>().bounds.center;





        //transform.GetComponent<Collider>().bounds.size;







        /*if (Physics.Raycast(myRigidbody.position, movementThisStep, out hitInfo, 0.1f, layerMask.value))
        {
            myRigidbody.isKinematic = true;
            Debug.Log("fuck you");
            Instantiate(collisionPoint, hitInfo.point, Quaternion.identity);

            //Vector3 reflectedPos = Vector3.Reflect(movementThisStep, hitInfo.normal);
            //myRigidbody.AddForce(reflectedPos * 100, ForceMode.Force);

            //transform.position = movementThisStep * -1f;         
        }*/
        previousPos = myRigidbody.position;
    }
}
