using UnityEngine;
using System.Collections;

public class CustomDontGoThroughThings : MonoBehaviour
{
    // Careful when setting this to true - it might cause double
    // events to be fired - but it won't pass through the trigger
    public bool sendTriggerMessage = false;

    public LayerMask layerMask = -1; //make sure we aren't in this layer 
    public float skinWidth = 0.1f; //probably doesn't need to be changed 

    private float minimumExtent;
    private float partialExtent;
    private float sqrMinimumExtent;
    private Vector3 previousPosition;
    private Rigidbody myRigidbody;
    private Collider myCollider;

    public GameObject parentObject;

    Vector3 localPositionOffset;


    void Start()
    {
        localPositionOffset = transform.localPosition;
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        previousPosition = myRigidbody.position;
        minimumExtent = Mathf.Min(Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y), myCollider.bounds.extents.z);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }









    void FixedUpdate()
    {
        //have we moved more than our minimum extent? 
        Vector3 movementThisStep = myRigidbody.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
            RaycastHit hitInfo;

            //check for obstructions we might have missed 
            if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
            {
                //myRigidbody.position = hitInfo.point - (movementThisStep * movementMagnitude) * partialExtent;
                //transform.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;
                //myRigidbody.position = movementThisStep * 0.25f;
                //if (myRigidbody.position == hitInfo.point)
                //{
                    Debug.Log("position reached");
                //}
            }
            /*else
            {
                myRigidbody.transform.localPosition = localPositionOffset;
            }*/
        }
        previousPosition = myRigidbody.position;
    }
}