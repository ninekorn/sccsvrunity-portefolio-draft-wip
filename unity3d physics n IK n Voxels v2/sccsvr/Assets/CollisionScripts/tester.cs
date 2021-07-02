using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour {

    static int countingTimes = 0;

	void Start ()
    {
        countingTimes++;
        Debug.Log(countingTimes);
	}
	
	void Update () {
		
	}
}
