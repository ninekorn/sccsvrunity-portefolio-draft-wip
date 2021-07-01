using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orientationCheck : MonoBehaviour {

    public GameObject target;


	void Start () {
		
	}
	
	void Update ()
    {

        Vector3 relativePoint = transform.InverseTransformPoint(target.transform.position);

        if (relativePoint.z > 0.001f)
        {

            if (relativePoint.x >= -0.001f && relativePoint.x < 0f)
            {
                Debug.Log("where am I 00");
            }

            if (relativePoint.x < -0.001f)
            {
                Debug.Log("where am I 01");
            }
            if (relativePoint.x <= 0.001f && relativePoint.x >= 0f)
            {
                Debug.Log("where am I 02");
            }

            if (relativePoint.x > 0.001f)
            {
                Debug.Log("where am I 03");
            }
        }


        if (relativePoint.z <= -0.001f)
        {
            if (relativePoint.x >= -0.001f && relativePoint.x < 0f)
            {
                Debug.Log("where am I 04");
            }

            if (relativePoint.x < -0.001f)
            {
                Debug.Log("where am I 05");
            }
            if (relativePoint.x <= 0.001f && relativePoint.x >= 0f)
            {
                Debug.Log("where am I 06");
            }

            if (relativePoint.x > 0.001f)
            {
                Debug.Log("where am I 07");
            }
        }

        /*if (relativePoint.z <= 0.001f && relativePoint.z > 0f)
        {
            if (relativePoint.x >= -0.001f && relativePoint.x < 0f)
            {

            }

            if (relativePoint.x < -0.001f)
            {

            }
            if (relativePoint.x <= 0.001f && relativePoint.x >= 0f)
            {

            }

            if (relativePoint.x > 0.001f)
            {

            }
        }

        if (relativePoint.z >= -0.001f && relativePoint.z <= 0f)
        {
            if (relativePoint.x >= -0.001f && relativePoint.x < 0f)
            {

            }

            if (relativePoint.x < -0.001f)
            {

            }
            if (relativePoint.x <= 0.001f && relativePoint.x >= 0f)
            {

            }

            if (relativePoint.x > 0.001f)
            {

            }
        }*/





    }
}
