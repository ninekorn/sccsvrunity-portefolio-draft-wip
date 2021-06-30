using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabObject : MonoBehaviour
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

    public GameObject rayCastPalmLeft;
    public GameObject rayCastPalmRight;



    public LayerMask layerMask;

    public GameObject grabPositionLeft;
    public GameObject grabPositionRight;

    Vector3 currentPos;
    Vector3 lastPos;
    Vector3 vel;

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftHandIndex;
    public GameObject rightHandIndex;
    public GameObject currentGrabbedObjectLeft;
    public GameObject currentGrabbedObjectRight;

    public GameObject objectToInstantiate;

    bool currentlyHoldsOneObjectLeft = false;
    bool currentlyHoldsOneObjectRight = false;

    void Start()
    {
        currentPos = grabPositionLeft.transform.position;
        lastPos = grabPositionLeft.transform.position;
    }

    bool arrowInPosition = false;

    void Update()
    {
        //float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        currentPos = grabPositionLeft.transform.position;
        vel = (currentPos - lastPos) * 150;
        //Vector3 newRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        //float RotAngle = 360f - Mathf.Round(Input * 90f);

        RaycastHit hitter;

        bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (buttonPressedLeft && currentlyHoldsOneObjectLeft == false)
        {
            if (Physics.Raycast(rayCastPalmLeft.transform.position, rayCastPalmLeft.transform.right, out hitter, 0.1f, layerMask))
            {
                if (hitter.transform.parent != grabPositionLeft.transform)
                {
                    if (hitter.transform.tag == "gun" || hitter.transform.tag == "bow")
                    {
                        Transform[] childObjects = hitter.transform.GetComponentsInChildren<Transform>();

                        for (int j = 0; j < childObjects.Length; j++)
                        {
                            if (childObjects[j].name == "grip")
                            {

                                hitter.transform.position = grabPositionLeft.transform.position;
                                hitter.transform.rotation = Quaternion.LookRotation(grabPositionLeft.transform.forward, grabPositionLeft.transform.up);
                                Vector3 diff = childObjects[j].transform.position - hitter.transform.position;
                                hitter.transform.position -= diff;
                                hitter.transform.parent = grabPositionLeft.transform;

                                                      
                                if (hitter.transform.GetComponent<Rigidbody>().isKinematic == false)
                                {
                                    hitter.transform.GetComponent<Rigidbody>().isKinematic = true;
                                }

                                currentGrabbedObjectLeft = hitter.transform.gameObject;
                                currentlyHoldsOneObjectLeft = true;
                            }

                            /*if (childObjects[j].name == "ropeGrip")
                            {
                                if (hitter.transform.GetComponent<Rigidbody>().isKinematic == false)
                                {
                                    hitter.transform.GetComponent<Rigidbody>().isKinematic = true;
                                }

                                childObjects[j].transform.parent = rightHandIndex.transform;
                                childObjects[j].position = rightHandIndex.transform.position;
                                currentGrabbedObjectLeft = hitter.transform.gameObject;
                                currentlyHoldsOneObjectLeft = true;
                            }*/
                        }
                    }
                
                    if (hitter.transform.tag == "arrow")
                    {
                        Transform[] childObjects = hitter.transform.GetComponentsInChildren<Transform>();

                        //hitter.transform.rotation = grabPositionLeft.transform.rotation;
                        //hitter.transform.up = -grabPositionLeft.transform.forward;                       
                        //hitter.transform.forward = -grabPositionLeft.transform.right;

                        for (int j = 0; j < childObjects.Length; j++)
                        {
                            if (childObjects[j].name == "grip")
                            {                              
                                hitter.transform.position = leftHandIndex.transform.position;

                                hitter.transform.rotation = Quaternion.LookRotation(grabPositionLeft.transform.forward, grabPositionLeft.transform.up);
                                Vector3 diff = childObjects[j].transform.position - hitter.transform.position;
                                hitter.transform.position -= diff;

                                if (hitter.transform.GetComponent<Rigidbody>().isKinematic == false)
                                {
                                    hitter.transform.GetComponent<Rigidbody>().isKinematic = true;
                                }

                                hitter.transform.parent = grabPositionLeft.transform;

                                currentGrabbedObjectLeft = hitter.transform.gameObject;
                                currentlyHoldsOneObjectLeft = true;
                            }
                        }
                    }

                    if (hitter.transform.tag == "stick")
                    {
                        hitter.transform.parent = grabPositionLeft.transform;

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
                                hitter.transform.up = grabPositionLeft.transform.up;
                                if (hitter.transform.GetComponent<Rigidbody>().isKinematic == false)
                                {
                                    hitter.transform.GetComponent<Rigidbody>().isKinematic = true;
                                }
                                Vector3 diff = childObjects[j].transform.position - hitter.transform.position;
                                hitter.transform.position = grabPositionLeft.transform.position - diff;
                                currentGrabbedObjectLeft = hitter.transform.gameObject;
                                currentlyHoldsOneObjectLeft = true;
                            }
                        }
                    }
                }
            }
        }

        else if (buttonPressedLeft == false && currentlyHoldsOneObjectLeft == true)
        {
            if (currentGrabbedObjectLeft != null)
            {
                if (currentGrabbedObjectLeft.transform.GetComponent<Rigidbody>().isKinematic == true)
                {
                    currentGrabbedObjectLeft.transform.GetComponent<Rigidbody>().isKinematic = false;
                }
                currentGrabbedObjectLeft.transform.parent = null;
                currentlyHoldsOneObjectLeft = false;
            }
        }

        RaycastHit hitterRight;
        if (buttonPressedRight && currentlyHoldsOneObjectRight == false)
        {
            if (Physics.Raycast(rayCastPalmRight.transform.position, -rayCastPalmRight.transform.right, out hitterRight, 0.1f, layerMask))
            {
                if (hitterRight.transform.parent != grabPositionRight.transform)
                {
                    /*if (hitterRight.transform.tag == "gun" || hitterRight.transform.tag == "bow")
                    {
                        Transform[] childObjects = hitterRight.transform.GetComponentsInChildren<Transform>();

                        for (int j = 0; j < childObjects.Length; j++)
                        {
                            if (childObjects[j].name == "grip")
                            {
                                //Instantiate(objectToInstantiate, childObjects[j].transform.position, Quaternion.identity);

                                //hitter.transform.forward = childObjects[j].transform.forward;
                                hitterRight.transform.right = -grabPositionRight.transform.forward;
                                hitterRight.transform.forward = grabPositionRight.transform.up;

                                Vector3 diff = childObjects[j].transform.position - hitterRight.transform.position;
                                hitterRight.transform.position = grabPositionRight.transform.position - diff;

                                hitterRight.transform.parent = grabPositionRight.transform;

                                //hitter.transform.right = -grabPositionLeft.transform.forward;

                                if (hitterRight.transform.GetComponent<Rigidbody>().isKinematic == false)
                                {
                                    hitterRight.transform.GetComponent<Rigidbody>().isKinematic = true;
                                }

                                currentGrabbedObjectRight = hitterRight.transform.gameObject;
                                currentlyHoldsOneObjectRight = true;
                            }

                            if (childObjects[j].name == "ropeGrip")
                            {
                                if (hitterRight.transform.GetComponent<Rigidbody>().isKinematic == false)
                                {
                                    hitterRight.transform.GetComponent<Rigidbody>().isKinematic = true;
                                }

                                childObjects[j].transform.parent = rightHandIndex.transform;
                                childObjects[j].position = rightHandIndex.transform.position;
                                currentGrabbedObjectRight = hitterRight.transform.gameObject;
                                currentlyHoldsOneObjectRight = true;
                            }
                        }
                    }*/

                    if (hitterRight.transform.tag == "arrow")
                    {
                        Transform[] childObjects = hitterRight.transform.GetComponentsInChildren<Transform>();

                        for (int j = 0; j < childObjects.Length; j++)
                        {
                            if (childObjects[j].name == "grip")
                            {
                                hitterRight.transform.position = rightHandIndex.transform.position;

                                hitterRight.transform.rotation = Quaternion.LookRotation(grabPositionRight.transform.forward, grabPositionRight.transform.up);
                                Vector3 diff = childObjects[j].transform.position - hitterRight.transform.position;
                                hitterRight.transform.position -= diff;

                                if (hitterRight.transform.GetComponent<Rigidbody>().isKinematic == false)
                                {
                                    hitterRight.transform.GetComponent<Rigidbody>().isKinematic = true;
                                }

                                hitterRight.transform.parent = grabPositionRight.transform;

                                currentGrabbedObjectRight = hitterRight.transform.gameObject;
                                currentlyHoldsOneObjectRight = true;
                            }
                        }
                    }

                    /*if (hitterRight.transform.tag == "stick")
                    {
                        hitterRight.transform.parent = grabPositionRight.transform;

                        Vector3 hitterPos = hitterRight.transform.position;
                        Vector3 hitterPoint = hitterRight.point;

                        float distToPoint = Vector3.Distance(hitterRight.transform.position, hitterRight.point);

                        Vector3 dirHitterTransformToHitterPoint = hitterPos - hitterPoint;

                        float mag = dirHitterTransformToHitterPoint.magnitude;

                        Transform[] childObjects = hitterRight.transform.GetComponentsInChildren<Transform>();

                        for (int j = 0; j < childObjects.Length; j++)
                        {
                            if (childObjects[j].name == "grip")
                            {
                                hitterRight.transform.up = grabPositionRight.transform.up;
                                if (hitterRight.transform.GetComponent<Rigidbody>().isKinematic == false)
                                {
                                    hitterRight.transform.GetComponent<Rigidbody>().isKinematic = true;
                                }
                                Vector3 diff = childObjects[j].transform.position - hitterRight.transform.position;
                                hitterRight.transform.position = grabPositionRight.transform.position - diff;
                                currentGrabbedObjectRight = hitterRight.transform.gameObject;
                                currentlyHoldsOneObjectRight = true;
                            }
                        }
                    }*/
                }
            }
        }

        else if (buttonPressedRight == false && currentlyHoldsOneObjectRight == true)
        {
            if (arrowInPosition)
            {
                //Rigidbody rigid = currentGrabbedObjectRight.transform.GetComponent<Rigidbody>();
                //rigid.isKinematic = false;
                //float force = 2000;
                //rigid.AddForce(currentGrabbedObjectRight.transform.forward * force, ForceMode.Force);

                //currentGrabbedObjectRight.GetComponent<arrowScript>().currentBow = currentGrabbedObjectLeft;
                currentGrabbedObjectRight.GetComponent<arrowScript>().arrowIsFired = true;

                if (currentGrabbedObjectRight != null)
                {
                    if (currentGrabbedObjectRight.transform.GetComponent<Rigidbody>().isKinematic == true)
                    {
                        currentGrabbedObjectRight.transform.GetComponent<Rigidbody>().isKinematic = false;
                    }
                    currentGrabbedObjectRight.transform.parent = null;
                    currentlyHoldsOneObjectRight = false;
                }                
            }
            if (!arrowInPosition)
            {
                if (currentGrabbedObjectRight != null)
                {
                    if (currentGrabbedObjectRight.transform.GetComponent<Rigidbody>().isKinematic == true)
                    {
                        currentGrabbedObjectRight.transform.GetComponent<Rigidbody>().isKinematic = false;
                    }
                    currentGrabbedObjectRight.transform.parent = null;
                    currentlyHoldsOneObjectRight = false;
                }
            }
        }

        if (currentGrabbedObjectRight != null && currentGrabbedObjectLeft != null && currentlyHoldsOneObjectRight && currentlyHoldsOneObjectLeft && buttonPressedRight && buttonPressedLeft)
        {
            if (currentGrabbedObjectRight.tag == "arrow" && currentGrabbedObjectLeft.tag == "bow")
            {
                if (Vector3.Distance(currentGrabbedObjectRight.transform.position, currentGrabbedObjectLeft.transform.position) < 0.75f)
                {
                    Transform[] childObjectsRightArrow = currentGrabbedObjectRight.transform.GetComponentsInChildren<Transform>();
                    Transform[] childObjectsLeftBow = currentGrabbedObjectLeft.transform.GetComponentsInChildren<Transform>();

                    for (int i = 0; i < childObjectsLeftBow.Length; i++)
                    {
                        if (childObjectsLeftBow[i].name == "arrowTarget")
                        {
                            currentGrabbedObjectRight.transform.rotation = Quaternion.LookRotation((childObjectsLeftBow[i].transform.position - currentGrabbedObjectRight.transform.position).normalized * 10, childObjectsLeftBow[i].transform.up);
                            arrowInPosition = true;
                        }
                        for (int j = 0; j < childObjectsRightArrow.Length; j++)
                        {
                            if (childObjectsRightArrow[j].name == "grip" && childObjectsLeftBow[i].name == "ropeGrip")
                            {
                                childObjectsLeftBow[i].transform.position = childObjectsRightArrow[j].transform.position;
                            }
                        }
                    }
                }
                else
                {
                    arrowInPosition = false;

                    /*Transform[] childObjectsRightArrow = currentGrabbedObjectRight.transform.GetComponentsInChildren<Transform>();
                    Transform[] childObjectsLeftBow = currentGrabbedObjectLeft.transform.GetComponentsInChildren<Transform>();

                    for (int i = 0; i < childObjectsLeftBow.Length; i++)
                    {
                        for (int j = 0; j < childObjectsRightArrow.Length; j++)
                        {
                            if (childObjectsRightArrow[j].name == "grip" && childObjectsLeftBow[i].name == "ropeGrip")
                            {
                                childObjectsLeftBow[i].transform.position = childObjectsRightArrow[j].transform.position;
                            }
                        }
                    }*/
                }
            }
        }





































        /*float Input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

        currentPos = grabPosition.transform.position;
        vel = (currentPos - lastPos) * 150;

        Debug.DrawRay(grabPosition.transform.position, vel * 10, Color.red, 0.1f);

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

                                    for (int j = 0; j < childObjects.Length; j++)
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
}*/
        lastPos = grabPositionLeft.transform.position;
    }
}
