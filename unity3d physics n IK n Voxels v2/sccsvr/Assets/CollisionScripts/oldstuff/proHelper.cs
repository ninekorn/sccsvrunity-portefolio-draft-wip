using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proHelper : MonoBehaviour {


    Rigidbody rigid;
    Vector3[] vertices;
    Rigidbody myRigidbody;

    void Start ()
    {
        myRigidbody = transform.GetComponent<Rigidbody>();
        vertices = transform.GetComponent<MeshFilter>().mesh.vertices;

    }

    void Update ()
    {



        myRigidbody.AddForce(vertices[0] * 0.1f, ForceMode.Impulse);













        /*Vector3 velocity = rigid.velocity;
        Vector3 currentPos = transform.position;
        Vector3 currentVelocity = velocity;

        ProjectileHelper.UpdateProjectile(ref currentPos, ref currentVelocity, -9.81f, Time.deltaTime*10);

        transform.position = currentPos;
        rigid.velocity = currentVelocity;*/


    }
}
