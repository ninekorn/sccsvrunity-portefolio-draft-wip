using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRotation : MonoBehaviour
{
    public Transform _CubeTransform;
    public Transform _QuadTransform;


    void Update()
    {
        //Controllig the Cube's rotation with keyboard
        if (Input.GetKey(KeyCode.A))
            _CubeTransform.Rotate(_CubeTransform.forward, Space.Self);
        if (Input.GetKey(KeyCode.D))
            _CubeTransform.Rotate(_CubeTransform.forward * -1, Space.Self);
        if (Input.GetKey(KeyCode.W))
            _CubeTransform.Rotate(_CubeTransform.up, Space.Self);
        if (Input.GetKey(KeyCode.S))
            _CubeTransform.Rotate(_CubeTransform.up * -1, Space.Self);
        //Update quad's rotation in the desired axis (Vector3.forward here )
        UpdateRotation(_QuadTransform, _QuadTransform.forward);
        //print(transform.forward);
    }

    private void UpdateRotation(Transform target, Vector3 axis)
    {
        Vector3 rot = _CubeTransform.rotation.eulerAngles;
        rot = Hadamard(rot, axis);
        target.rotation = Quaternion.Euler(rot);
    }

    //returns hadamard product of two vectors
    private Vector3 Hadamard(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }
}