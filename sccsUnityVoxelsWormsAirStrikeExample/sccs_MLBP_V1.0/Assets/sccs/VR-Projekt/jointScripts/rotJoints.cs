using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotJoints : MonoBehaviour
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
    public LayerMask layerMask;

    public GameObject grabPosition;

    Vector3 currentPos;
    Vector3 lastPos;
    Vector3 vel;

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject rightHandIndex;

    void Start()
    {
        currentPos = grabPosition.transform.position;
        lastPos = grabPosition.transform.position;
    }

    void Update()
    {












        /*
        float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

        currentPos = grabPosition.transform.position;
        vel = (currentPos - lastPos) * 150;

        Debug.DrawRay(grabPosition.transform.position, vel * 10, Color.red, 0.1f);
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
        /*
        Vector3 newRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

        float RotAngle = 360f - Mathf.Round(Input * 90f);

        //Debug.Log(RotAngle);

        RaycastHit hitter;

        //RaycastHit? hitObject;

        //if (!Physics.SphereCast(rayCastBegin.transform.position, 0.1f, rayCastBegin.transform.up, out hitter, 0.1f, layerMask) && !Physics.SphereCast(rayCastEnd.transform.position, 0.1f, rayCastEnd.transform.up, out hitter, 0.1f, layerMask))

        if (newRot.x > 360f || newRot.x >= 0f && newRot.x < 45f || newRot.x < 0)
        {
            newRot.x = 360f;
            //transform.localEulerAngles = newRot;
        }

        else if (newRot.x < 270f && newRot.x > 45f)
        {
            newRot.x = 270f;
            //transform.localEulerAngles = newRot;
        }

        if (newRot.x >= 270f && newRot.x <= 360f)
        {        
            if (Input >= 0)
            {
                if (Physics.Raycast(rayCastBegin.transform.position, rayCastBegin.transform.up, out hitter, 0.1f, layerMask) || Physics.Raycast(rayCastEnd.transform.position, rayCastEnd.transform.up, out hitter, 0.1f, layerMask))
                {
                    float distanceToObjectBegin = Vector3.Distance(rayCastBegin.transform.position, hitter.point);
                    float distanceToObjectEnd = Vector3.Distance(rayCastEnd.transform.position, hitter.point);

                    if (distanceToObjectBegin > 0.01f && distanceToObjectEnd > 0.01f)
                    {
                        if (RotAngle >= newRot.x + 5f) //RELEASE
                        {
                            if (hitter.transform.parent != null)
                            {
                                hitter.transform.parent = null;
                            }

                            Transform[] transformsChilds = leftHand.transform.GetComponentsInChildren<Transform>();

                            for (int i = 0; i < transformsChilds.Length; i++)
                            {
                                if (transformsChilds[i].GetComponent<MeshCollider>() != null)
                                {
                                    if (transformsChilds[i].GetComponent<MeshCollider>().enabled == true)
                                    {
                                        transformsChilds[i].GetComponent<MeshCollider>().enabled = false;
                                    }
                                }
                            }

                            hitter.transform.GetComponent<Rigidbody>().isKinematic = false;
                            hitter.transform.GetComponent<Rigidbody>().velocity = vel;
                            newRot.x += 1f * Time.deltaTime * speed;
                            transform.localEulerAngles = newRot;
                        }

                        else if (RotAngle < newRot.x - 5f) //GRAB
                        {

                            newRot.x -= 1f * Time.deltaTime * speed;
                            transform.localEulerAngles = newRot;
                        }
                    }
                    else
                    {
                        if (RotAngle >= newRot.x + 5f) //RELEASE
                        {
                            if (hitter.transform.parent != null)
                            {
                                hitter.transform.parent = null;
                            }
                            Transform[] transformsChilds = leftHand.transform.GetComponentsInChildren<Transform>();

                            for (int i = 0; i < transformsChilds.Length; i++)
                            {
                                if (transformsChilds[i].GetComponent<MeshCollider>() != null)
                                {
                                    if (transformsChilds[i].GetComponent<MeshCollider>().enabled == true)
                                    {
                                        transformsChilds[i].GetComponent<MeshCollider>().enabled = false;
                                    }
                                }
                            }
                            hitter.transform.GetComponent<Rigidbody>().isKinematic = false;

                            hitter.transform.GetComponent<Rigidbody>().velocity = vel;

                            newRot.x += 1f * Time.deltaTime * speed;
                            transform.localEulerAngles = newRot;
                        }

                        else if (RotAngle < newRot.x - 5f) //GRAB
                        {
                            if (hitter.transform.parent != grabPosition.transform)
                            {                              
                                if (hitter.transform.tag == "gun" || hitter.transform.tag == "bow")
                                {
                                    Transform[] childObjects = hitter.transform.GetComponentsInChildren<Transform>();

                                    for (int j = 0; j < childObjects.Length;j++)
                                    {
                                        if (childObjects[j].name == "grip")
                                        {
                                            hitter.transform.parent = grabPosition.transform;

                                            Vector3 hitterPos = hitter.transform.position;
                                            Vector3 hitterPoint = hitter.point;

                                            float distToPoint = Vector3.Distance(hitter.transform.position, hitter.point);

                                            Vector3 dirHitterTransformToHitterPoint = hitterPos - hitterPoint;

                                            float mag = dirHitterTransformToHitterPoint.magnitude;

                                            hitter.transform.right = -grabPosition.transform.forward;
                                            Vector3 diff = childObjects[j].transform.position - hitter.transform.position;
                                            hitter.transform.position = grabPosition.transform.position - diff;
                                        }

                                        if (childObjects[j].name == "ropeGrip")
                                        {
                                            //Debug.Log("found RopeGrip");
                                            childObjects[j].transform.parent = rightHandIndex.transform;
                                            childObjects[j].position = rightHandIndex.transform.position;
                                        }
                                    }
                                }

                                if (hitter.transform.tag == "stick")
                                {
                                    hitter.transform.parent = grabPosition.transform;

                                    Vector3 hitterPos = hitter.transform.position;
                                    Vector3 hitterPoint = hitter.point;

                                    float distToPoint = Vector3.Distance(hitter.transform.position, hitter.point);

                                    Vector3 dirHitterTransformToHitterPoint = hitterPos - hitterPoint;

                                    float mag = dirHitterTransformToHitterPoint.magnitude;

                                    Transform[] childObjects = hitter.transform.GetComponentsInChildren<Transform>();

                                    for (int j = 0; j < childObjects.Length; j++)
                                    {
                                        if (childObjects[j].name == "grip")
                                        {

                                            hitter.transform.up = grabPosition.transform.up;

                                            Vector3 diff = childObjects[j].transform.position - hitter.transform.position;
                                            hitter.transform.position = grabPosition.transform.position - diff;
                                        }
                                    }
                                            //hitter.transform.up = grabPosition.transform.up;
                                            //hitter.transform.rotation = grabPosition.transform.rotation;
                                            //hitter.transform.position = grabPosition.transform.position + (dirHitterTransformToHitterPoint);
                                            //hitter.transform.rotation = grabPosition.transform.rotation;
                                            //hitter.transform.up = grabPosition.transform.up;

                                  /*if (dotter < 0f) //towards bottom
                                    {
                                        mag *= 1;
                                    }
                                    else
                                    {
                                        mag *= -1;
                                    }

                                    hitter.transform.position = grabPosition.transform.position;// + (grabPosition.transform.up * mag);
                                    hitter.transform.rotation = grabPosition.transform.rotation;*/


                                    //hitter.transform.rotation = grabPosition.transform.rotation;
                                    /*if (dotter < 0f) //towards bottom
                                    {
                                        hitter.transform.position = grabPosition.transform.position + (-grabPosition.transform.up * mag);
                                        //hitter.transform.rotation = grabPosition.transform.rotation;
                                        //hitter.transform.up = grabPosition.transform.up;
                                    }
                                    else // towards top
                                    {
                                        hitter.transform.position = grabPosition.transform.position + (grabPosition.transform.up * mag);
                                        //hitter.transform.rotation = grabPosition.transform.rotation;
                                        //hitter.transform.position = grabPosition.transform.position;// + (-grabPosition.transform.up * mag);
                                        //hitter.transform.up = grabPosition.transform.up;
                                        //hitter.transform.up = grabPosition.transform.up;
                                    }
                                }
                              
                                Transform[] transformsChilds = leftHand.transform.GetComponentsInChildren<Transform>();

                                for (int i = 0; i < transformsChilds.Length; i++)
                                {
                                    if (transformsChilds[i].GetComponent<MeshCollider>() != null)
                                    {
                                        if (transformsChilds[i].GetComponent<MeshCollider>().enabled == false)
                                        {
                                            transformsChilds[i].GetComponent<MeshCollider>().enabled = true;
                                        }
                                    }
                                }

                                hitter.transform.GetComponent<Rigidbody>().isKinematic = true;
                            }
                            newRot.x -= 1f * Time.deltaTime * speed;
                            transform.localEulerAngles = newRot;
                        }
                    }
                }

                if (!Physics.Raycast(rayCastBegin.transform.position, rayCastBegin.transform.up, out hitter, 0.1f, layerMask) && !Physics.Raycast(rayCastEnd.transform.position, rayCastEnd.transform.up, out hitter, 0.1f, layerMask))
                {
                    if (RotAngle >= newRot.x + 5f) //RELEASE
                    {
                        if (grabPosition.transform.childCount > 0)
                        {
                            Transform grabbedObject = grabPosition.transform.GetChild(0);

                            Transform[] transformsChilds = leftHand.transform.GetComponentsInChildren<Transform>();

                            for (int i = 0; i < transformsChilds.Length; i++)
                            {
                                if (transformsChilds[i].GetComponent<MeshCollider>() != null)
                                {
                                    if (transformsChilds[i].GetComponent<MeshCollider>().enabled == true)
                                    {
                                        transformsChilds[i].GetComponent<MeshCollider>().enabled = false;
                                    }
                                }                     
                            }

                            grabbedObject.transform.GetComponent<Rigidbody>().isKinematic = false;
                            grabbedObject.transform.GetComponent<Rigidbody>().velocity = vel;
                            grabbedObject.transform.parent = null;
                        }
                        newRot.x += 1f * Time.deltaTime * speed;
                        transform.localEulerAngles = newRot;
                    }

                    if (RotAngle < newRot.x - 5f) //GRAB
                    {

                        newRot.x -= 1f * Time.deltaTime * speed;
                        transform.localEulerAngles = newRot;
                    }

                }
            }
        }
        lastPos = grabPosition.transform.position;*/
    }
}
