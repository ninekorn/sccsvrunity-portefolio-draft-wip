using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocitor : MonoBehaviour {

	void Start ()
    {
   
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.R))
        {
            Rigidbody rigid = transform.GetComponent<Rigidbody>();
            rigid.isKinematic = false;
            ///rigid.velocity = new Vector3(0, -10, 0);
            rigid.AddForce(new Vector3(0, -100000, 0), ForceMode.Force);
        }
	}
}
