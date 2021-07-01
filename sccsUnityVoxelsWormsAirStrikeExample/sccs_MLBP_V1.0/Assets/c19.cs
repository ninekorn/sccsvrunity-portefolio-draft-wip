using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c19 : MonoBehaviour {


    public Transform drone;
    public float rotspeed = 0.0f;

   
	void Start () {
		
	}
	
	void Update ()
    {
        this.transform.RotateAround(drone.position, new Vector3(0,0,1), Time.deltaTime * rotspeed);

	}
}
