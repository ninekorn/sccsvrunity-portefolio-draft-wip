using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakcube : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    /*private void OnCollisionEnter(Collision collision)
    {
        var sccsfracturescript = collision.transform.gameObject.GetComponent<cubicFrac>();

        if (sccsfracturescript != null)
        {
            sccsfracturescript.enabled = true;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("col:" + other.transform.name);
        var sccsfracturescript = other.transform.gameObject.GetComponent<Fracture4>();

        if (sccsfracturescript != null)
        {
            sccsfracturescript.enabled = true;
        }
    }
}
