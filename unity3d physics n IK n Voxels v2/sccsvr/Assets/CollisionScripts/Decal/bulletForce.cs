using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletForce : MonoBehaviour
{
    public GameObject gunEnd;
    public float force = 1000;

    void Start()
    {    
        Rigidbody rigid = transform.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        ///rigid.velocity = new Vector3(0, -10, 0);
        rigid.AddForce(gunEnd.transform.forward*force, ForceMode.Force);
    }
}
