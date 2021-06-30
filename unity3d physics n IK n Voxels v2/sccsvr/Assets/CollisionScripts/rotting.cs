using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotting : MonoBehaviour
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


    //public GameObject knuckle;
    //public GameObject fingerJoint1;
    //public GameObject fingerJoint2;
    //public GameObject fingerJoint3;

    //public GameObject parent;
    Vector3 worldOffset;
    Quaternion defaultRot;

    public GameObject rayCastBegin;
    public GameObject rayCastEnd;



    void Start()
    {

    }

    void Update()
    {




        //float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

        /*if (transform.name == "fingerJoint3")
        {
            Vector3 newRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

            float RotAngle = Mathf.Round(Input * 90f);

            if (newRot.x > 90f)
            {
                newRot.x = 90f;
            }
            if (newRot.x < 0)
            {
                newRot.x = 0;
            }
            //Debug.Log(pitchDeg + "pitchDeg");

            if (newRot.x <= 90f && newRot.x >= 0f)
            {
                if (Input >= 0)
                {
                    if (RotAngle < newRot.x + 5f && RotAngle > newRot.x - 5f)
                    {

                    }
                    else if (RotAngle >= newRot.x + 5f)
                    {
                        newRot.x += 1f;
                        transform.localEulerAngles = newRot;
                    }
                    else if (RotAngle < newRot.x - 5f)
                    {
                        newRot.x -= 1f;
                        transform.localEulerAngles = newRot;

                        //transform.Rotate(new Vector3(-1, 0, 0));
                    }
                }
            }
        }


        if (transform.name == "fingerJoint2")
        {
             Vector3 newRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

            float RotAngle = Mathf.Round(Input * 90f);

            if (newRot.x > 90f)
            {
                newRot.x = 90f;
            }
            if (newRot.x < 0)
            {
                newRot.x = 0;
            }
            //Debug.Log(pitchDeg + "pitchDeg");

            if (newRot.x <= 90f && newRot.x >= 0f)
            {
                if (Input >= 0)
                {
                    if (RotAngle < newRot.x + 5f && RotAngle > newRot.x - 5f)
                    {

                    }
                    else if (RotAngle >= newRot.x + 5f)
                    {
                        newRot.x += 1f;
                        transform.localEulerAngles = newRot;
                    }
                    else if (RotAngle < newRot.x - 5f)
                    {
                        newRot.x -= 1f;
                        transform.localEulerAngles = newRot;

                        //transform.Rotate(new Vector3(-1, 0, 0));
                    }
                }
            }
        }*/

        Vector3 newRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

        RaycastHit hitter;

        if (Physics.SphereCast(rayCastBegin.transform.position,0.1f, rayCastBegin.transform.up,out hitter,0.25f)|| Physics.SphereCast(rayCastEnd.transform.position, 0.1f, rayCastEnd.transform.up, out hitter, 0.25f))
        {
            Debug.Log("hitting");
        }


        /*
        float RotAngle = 360f - Mathf.Round(Input * 90f);

        if (newRot.x > 360f || newRot.x >= 0f && newRot.x < 45f || newRot.x < 0)
        {
            newRot.x = 360f;
        }
        if (newRot.x < 270f && newRot.x > 45f)
        {
            newRot.x = 270f;
        }

        //Debug.Log(newRot.x);

        if (newRot.x >= 270f && newRot.x <= 360f)
        {
            if (Input >= 0)
            {
                //Debug.Log(RotAngle);

                if (RotAngle >= newRot.x + 5f) // RELEASE
                {
                    //Debug.Log("test0");
                    newRot.x += 1f;
                    transform.localEulerAngles = newRot;
                }

                else if (RotAngle < newRot.x - 5f) // GRAB
                {
                    //Debug.Log("test1");
                    newRot.x -= 1f;
                    transform.localEulerAngles = newRot;
                }
            }

            /*if (Input >= 0)
            {
                if (RotAngle < newRot.x + 5f && RotAngle > newRot.x - 5f)
                {

                }

                else if (RotAngle >= newRot.x + 5f) // GRAB
                {
                    newRot.x += 1f;
                    transform.localEulerAngles = newRot;
                }

                else if (RotAngle < newRot.x - 5f) // RELEASE
                {
                    newRot.x -= 1f;
                    transform.localEulerAngles = newRot;
                }

            }
        }*/










        /*if (newRot.x > 90f)
        {
            newRot.x = 90f;
        }
        if (newRot.x < 0)
        {
            newRot.x = 0;
        }

        //Debug.Log();

        if (newRot.x <= 90f && newRot.x >= 0f)
        {
            if (Input >= 0)
            {

                if (RotAngle < newRot.x + 5f && RotAngle > newRot.x - 5f)
                {

                }

                else if (RotAngle >= newRot.x + 5f) // GRAB
                {
                    newRot.x += 1f;
                    transform.localEulerAngles = newRot;
                }

                else if (RotAngle < newRot.x - 5f) // RELEASE
                {
                    newRot.x -= 1f;
                    transform.localEulerAngles = newRot;
                }

            }
        }*/
    }
}
