using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scik : MonoBehaviour {

    public Transform shoulder;
    public Transform upperarm;
    public Transform forearm;
    public Transform hand;

    public Transform elbowtarget;
    public Transform handtarget;

    public float sizeUpperArm = 0.0f;
    public float sizeForeArm = 0.0f;
    public float sizeHand = 0.0f;




    void Start ()
    {
		
	}

	void Update ()
    {
        var distanceshouldertohandtarget = handtarget.position - shoulder.position;
        var bigR = sizeUpperArm; //radius of red circle
        var smallR = sizeForeArm; //radius of blue circle




    }
}
