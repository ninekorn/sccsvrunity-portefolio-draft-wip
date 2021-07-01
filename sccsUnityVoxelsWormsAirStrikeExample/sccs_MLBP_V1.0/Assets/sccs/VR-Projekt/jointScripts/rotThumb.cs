using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotThumb : MonoBehaviour
{

    public bool inverseInput = false;
    public float speed = 1f;
    float firstInput;
    float lastInput;

    float _inputValue1 = 0;
    float _inputValue2 = 0;
    float _inputValue3 = 0;
    bool isIncreasing = false;
    bool isDecreasing = false;


    public GameObject knuckle;
    public GameObject fingerJoint1;
    public GameObject fingerJoint2;
    public GameObject fingerJoint3;

    void Start()
    {

    }

    void Update()
    {

        // float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        // float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        // float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        // float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        // float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        // float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        // float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        // float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        // float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);













        /*
        float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

        if (transform.name == "fingerJoint3")
        {
            Vector3 newRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

            float RotAngle = 360f - Mathf.Round(Input * 90f);

            if (newRot.y > 360f)
            {
                newRot.y = 360f;
            }
            if (newRot.y < 315f)
            {
                newRot.y = 315f;
            }
            //Debug.Log(pitchDeg + "pitchDeg");

            if (newRot.y <= 360f && newRot.y >= 315f)
            {
                if (Input >= 0)
                {
                    if (RotAngle < newRot.y + 5f && RotAngle > newRot.y - 5f)
                    {

                    }
                    else if (RotAngle >= newRot.y + 5f)
                    {
                        newRot.y += 0.25f;
                        transform.localEulerAngles = newRot;
                    }
                    else if (RotAngle < newRot.y - 5f)
                    {
                        newRot.y -= 0.25f;
                        transform.localEulerAngles = newRot;
                    }
                }
            }
        }*/

        /*
        if (transform.name == "fingerJoint2")
        {
            Vector3 newRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

            float RotAngle = 360f - Mathf.Round(Input * 90f);


            if (newRot.y > 360f)
            {
                newRot.y = 360f;
            }
            if (newRot.y < 315f)
            {
                newRot.y = 315f;
            }
            //Debug.Log(pitchDeg + "pitchDeg");

            if (newRot.y <= 360f && newRot.y >= 315f)
            {
                if (Input >= 0)
                {
                    if (RotAngle < newRot.y + 5f && RotAngle > newRot.y - 5f)
                    {

                    }
                    else if (RotAngle >= newRot.y + 5f)
                    {
                        newRot.y += 0.25f;
                        transform.localEulerAngles = newRot;
                    }
                    else if (RotAngle < newRot.y - 5f)
                    {
                        newRot.y -= 0.25f;
                        transform.localEulerAngles = newRot;
                    }
                }
            }

        }*/

        /*
        if (transform.name == "fingerJoint1")
        {
            Vector3 newRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

            float RotAngle = 360f - Mathf.Round(Input * 90f);

            if (newRot.z > 360f)
            {
                newRot.z = 360f;
            }
            if (newRot.z < 315f)
            {
                newRot.z = 315f;
            }
            //Debug.Log(pitchDeg + "pitchDeg");

            if (newRot.z <= 360f && newRot.z >= 315f)
            {
                if (Input >= 0)
                {
                    if (RotAngle < newRot.z + 5f && RotAngle > newRot.z - 5f)
                    {

                    }
                    else if (RotAngle >= newRot.z + 5f)
                    {
                        newRot.z += 1f;
                        transform.localEulerAngles = newRot;
                    }
                    else if (RotAngle < newRot.z - 5f)
                    {
                        newRot.z -= 1f;
                        transform.localEulerAngles = newRot;
                    }
                }
            }
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision occured");
    }


}
