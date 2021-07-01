using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionReset : MonoBehaviour {

    Vector3 initialPos;
    Quaternion initialRot;

	void Start ()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
    }
	
	void Update ()
    {
        //	if (OVRInput.Get(OVRInput.Button.One))
        //	if (OVRInput.Get(OVRInput.Button.One))
        //	if (OVRInput.Get(OVRInput.Button.One))
        //	if (OVRInput.Get(OVRInput.Button.One))
        //	if (OVRInput.Get(OVRInput.Button.One))
        //	if (OVRInput.Get(OVRInput.Button.One))
        //	if (OVRInput.Get(OVRInput.Button.One))













        //if (OVRInput.Get(OVRInput.Button.One))
        {
            transform.position = initialPos;
            transform.rotation = initialRot;

            if (transform.GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            if (transform.GetComponent<MeshCollider>() != null)
            {
                if (transform.GetComponent<MeshCollider>().enabled != true)
                {
                    transform.GetComponent<MeshCollider>().enabled = true;
                }
            }
            if (transform.parent != null)
            {
                transform.parent = null;
            }
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.name == "Terrain")
        {
            transform.position = initialPos;
            transform.rotation = initialRot;

            if (transform.parent != null)
            {
                transform.parent = null;
            }

            if (transform.GetComponent<MeshCollider>() != null)
            {
                if (transform.GetComponent<MeshCollider>().enabled != true)
                {
                    transform.GetComponent<MeshCollider>().enabled = true;
                }
            }

            if (transform.GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
