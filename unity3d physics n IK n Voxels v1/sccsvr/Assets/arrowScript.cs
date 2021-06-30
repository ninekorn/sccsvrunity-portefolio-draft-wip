using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour {

    private Rigidbody rigid;
    public bool arrowIsFired = false;
    bool arrowInMidAir = false;
    //public GameObject currentBow;

	void Start ()
    {
        rigid = GetComponent<Rigidbody>();
    }
	
	void Update () {

        if (arrowIsFired)
        {
            Rigidbody rigid = transform.GetComponent<Rigidbody>();
            rigid.isKinematic = false;
            float force = 2000;
            //rigid.AddForce(transform.forward * force, ForceMode.Force);
            rigid.AddForceAtPosition(transform.forward * force, transform.position,ForceMode.Force);
            arrowInMidAir = true;
            arrowIsFired = false;
        }

        if (arrowInMidAir)
        {
            if (rigid.velocity != Vector3.zero)
            {
                rigid.transform.rotation = Quaternion.LookRotation(rigid.velocity);
            }
        }
        





    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "collisionObject")
        {
            arrowIsFired = false;
            arrowInMidAir = false;
        }
    }
}
