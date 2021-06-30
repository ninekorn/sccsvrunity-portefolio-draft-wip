using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImove : MonoBehaviour {

    public float speed;
    public Transform target;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Vector3 direction = target.transform.position - transform.position;
        transform.Translate(direction * Time.deltaTime*speed);
	}
}
