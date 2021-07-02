using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepOffset : MonoBehaviour {

    public GameObject Shoulder;
    public GameObject body;
    public GameObject parentObject;

    Vector3 relativeShoulderPosToPlayerController;
    float yOffset;
    Vector3 position;
    Vector3 relativeShoulderPosToHead;
    public Transform baseCharacter; //Assign ball to this in inspector

    public GameObject handPosition;
    public GameObject handAnchor;
    Quaternion initialRot;
    Vector3 initialAngle;

    void Start()
    {
        relativeShoulderPosToHead = parentObject.transform.position - body.transform.position;
        position = parentObject.transform.position - relativeShoulderPosToHead;
        yOffset = position.y;
        initialRot = body.transform.rotation;
        initialAngle = baseCharacter.transform.position - body.transform.position;
    }

    void Update ()
    {
        body.transform.position = parentObject.transform.position - relativeShoulderPosToHead;

        /*if (Vector3.Distance(handPosition.transform.position, handAnchor.transform.position) > 0.15f)
        {
            //Debug.Log("gotta bend body");
            Vector3 forward = handAnchor.transform.position - body.transform.position;
            body.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        }
        else
        {
            body.transform.rotation = initialRot;
        }*/
        /*Vector3 eulerAngles = transform.eulerAngles;
        float xRot = eulerAngles.x;
        float yRot = parentObject.transform.eulerAngles.y;
        float zRot = eulerAngles.z;

        transform.eulerAngles = new Vector3(xRot, yRot, zRot);*/
        //transform.right = parentObject.transform.right;
        //Calculate xRot and zRot of the seat here, or set them to constant values
    }
}
