using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyrot : MonoBehaviour {

    public Transform objectToCopyRotFrom;
    float angleOfobjectToCopyRotFrom = 0;

    void Start ()
    {
        
    }
	
	void Update ()
    {

        /*var right = objectToCopyRotFrom.right;
        right.y = 0;
        right *= Mathf.Sign(objectToCopyRotFrom.up.y);
        var fwd = Vector3.Cross(right, Vector3.up).normalized;
        float pitch = Vector3.Angle(fwd, objectToCopyRotFrom.forward) * Mathf.Sign(objectToCopyRotFrom.forward.y);


        var fwrd = objectToCopyRotFrom.forward;
        fwrd.y = 0;
        fwrd *= Mathf.Sign(objectToCopyRotFrom.up.y);
        var righter = Vector3.Cross(Vector3.up, fwrd).normalized;
        float roll = Vector3.Angle(righter, objectToCopyRotFrom.right) * Mathf.Sign(objectToCopyRotFrom.right.y);


        transform.rotation = Quaternion.Euler(0, 0, roll);*/




        /*angleOfobjectToCopyRotFrom =objectToCopyRotFrom.eulerAngles.y;

        Vector3 oriEulers = transform.eulerAngles;
        oriEulers.y = angleOfobjectToCopyRotFrom;

        transform.eulerAngles = oriEulers;*/


        UpdateRotation(transform, transform.up);

    }

    private void UpdateRotation(Transform target, Vector3 axis)
    {
        Vector3 rot = objectToCopyRotFrom.eulerAngles;
        rot.x = target.eulerAngles.x;
        rot.z = target.eulerAngles.z;
        rot = Hadamard(rot, axis);
        target.rotation = target.rotation * Quaternion.AngleAxis(rot.y, axis);// Quaternion.Euler(rot);


        /*Vector3 rot = objectToCopyRotFrom.rotation.eulerAngles;
        rot.x = target.rotation.eulerAngles.x;
        rot.z = target.rotation.eulerAngles.z;

        rot = Hadamard(rot, axis);
        target.rotation = Quaternion.Euler(rot);*/
    }

    //returns hadamard product of two vectors
    private Vector3 Hadamard(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }
}
