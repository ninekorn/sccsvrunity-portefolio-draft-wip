using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Threading;
using Jitter;
using Jitter.Collision;
using Jitter.Dynamics;
using Jitter.Dynamics.Constraints;
using Jitter.LinearMath;
using UnityEngine;
using Material = Jitter.Dynamics.Material;
using System.Collections;
using RigidBody = Jitter.Dynamics.RigidBody;




public class playerInteraction : MonoBehaviour
{
    float raylength = 0;
    float raycounter = 0;
    float raycounterMax = 10;
    float raycounterLoopMax = 100;
    float raycounterSwtc = 0;


    public Jitter.Dynamics.RigidBody lastFrameHitRigidBody;
    public Vector3 lastFrameHitNormal;
    public Vector3 lastFrameHitPoint;
    public int lastFrameHitrIndex;


    public Transform RaycastHitVisualObject;
    Vector3 initialPivotPosition = Vector3.zero;
    Vector3 lastFramePosition = Vector3.zero;

    public Transform upperleg;
    public Transform lowerleg;
    public Transform foot;
    public Transform legstaticpivot;

    float upperleglength = 0;
    float lowerleglength = 0;
    float footlength = 0;
    float totallegLength = 0;

    public LayerMask layerMask;

    Vector3 IdleStandingTargetPositionVariableLength;
    Vector3 IdleStandingTargetPositionMax;
    Vector3 IdleStandingTargetPositionMin;

    public int swtcForTypeOfInteract = 0;
    //0 == 

    public Transform footTarget;


    public Transform planetmanager;

    public byte activeBlockType = 0;
    public Transform retAdd, retDel;

    Mesh mesh;

    float planeSize = 0.25f;

    public Transform sphere;
    Vector3 yoMan;
    Vector3 yoMan1;

    float diameter;
    float fraction;
    float radius;
    float whatever;

    int roundedX;
    int roundedY;
    int roundedZ;

    private Camera cam;


    int multiplicator = 3;
    int multiplicatorReticle = 5;
    int tileSize = 4;
    int suppressorPos = 4;

    Vector3 originDirection = Vector3.zero;
    float originDirectionLength = 0;

    void Start()
    {

        initialPivotPosition = this.transform.position;
        lastFramePosition = footTarget.position;
        originDirection = initialPivotPosition - legstaticpivot.position;
        originDirectionLength = originDirection.magnitude;
        originDirection.Normalize();



        if (swtcForTypeOfInteract == 2)
        {
            upperleglength = upperleg.localScale.z;
            lowerleglength = lowerleg.localScale.z;
            footlength = foot.localScale.z;
            totallegLength = upperleglength + lowerleglength + footlength;

            IdleStandingTargetPositionMax = transform.position + ((transform.forward * upperleglength) + (transform.forward * lowerleglength) + (transform.forward * footlength));
            IdleStandingTargetPositionMin = transform.position + ((transform.forward * (upperleglength)) + (transform.forward * (lowerleglength)) + (transform.forward * (footlength)) * 0.5f);

        }

        //retAdd.localScale = retAdd.localScale * planeSize;
        //retDel.localScale = retDel.localScale * planeSize;
        fraction = 1 / planeSize;
        radius = planeSize / 2;
        diameter = 0.1f;
        whatever = 1 / (diameter * 2);
        cam = Camera.main;
    }


    private bool RaycastCallback(Jitter.Dynamics.RigidBody body, JVector normal, float fraction)
    {
        if (body.IsStatic)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public static JRaycastHit Raycast(Ray ray, float maxDistance, RaycastCallback callback = null)
    {
        RigidBody hitBody;
        JVector hitNormal;
        float hitFraction;

        var origin = ray.origin.ToJVector();
        var direction = ray.direction.ToJVector();

        if (JPhysics.collisionSystem.Raycast(origin, direction, callback, out hitBody, out hitNormal, out hitFraction))
        {
            if (hitFraction <= maxDistance)
            {
                return new JRaycastHit(hitBody, hitNormal.ToVector3(), ray.origin, ray.direction, hitFraction);
            }
        }
        else
        {
            direction *= maxDistance;
            if (JPhysics.collisionSystem.Raycast(origin, direction, callback, out hitBody, out hitNormal, out hitFraction))
            {
                return new JRaycastHit(hitBody, hitNormal.ToVector3(), ray.origin, direction.ToVector3(), hitFraction);
            }
        }
        return null;
    }

    void OnGUI()
    {
        /*Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();*/
    }

    int counterForByteChange = 0;
    int counterForByteChangeMax = 1;

    Ray ray;
    Vector3 positionThisObject;
    Vector3 directionForwardOfThisObject;

    void Update()
    {


        if (swtcForTypeOfInteract == 0)
        {
            ray = new Ray(transform.position, transform.forward);//ray = Camera.main.ScreenPointToRay(Input.mousePosition);



            RaycastHit hit;
            //Debug.DrawRay(transform.position, transform.forward * 5, Color.green, 0.001f);

            //var someTouch0 = Input.GetTouch(0);
            ////Debug.Log(""+ someTouch0);

            /*bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

            if (buttonPressedLeft)
            {
                //Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
            }
            if (buttonPressedRight)
            {
                //Debug.Log("buttonPressedRight:" + buttonPressedRight);
            }*/

            //if (counterForByteChange >= counterForByteChangeMax)
            {
                //if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
                {


                    var raycasthit = Raycast(ray, 0.1f, null);

                    if (raycasthit.Rigidbody != null)//Physics.Raycast(ray, out hit, 0.1f))
                    {
                        if ((JPhysics.BodyTag)raycasthit.Rigidbody.Tag == (JPhysics.BodyTag.jitterCollisionObject))//if (hit.transform.tag == "collisionObject")
                        {
                            if (GetComponent<Fracture4>() != null)
                            {

                            }
                            else
                            {
                                var chunkX = (int)(Mathf.Round(raycasthit.Rigidbody.Position.X * tileSize) / tileSize);
                                var chunkY = (int)(Mathf.Round(raycasthit.Rigidbody.Position.Y * tileSize) / tileSize);
                                var chunkZ = (int)(Mathf.Round(raycasthit.Rigidbody.Position.Z * tileSize) / tileSize);

                                ////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                                if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)raycasthit.Rigidbody.Position.X, (int)raycasthit.Rigidbody.Position.Y, (int)raycasthit.Rigidbody.Position.Z) != null)
                                {
                                    ////Debug.Log("==count==");
                                    mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)raycasthit.Rigidbody.Position.X, (int)raycasthit.Rigidbody.Position.Y, (int)raycasthit.Rigidbody.Position.Z);

                                    ////Debug.Log("x: " +raycasthit.Normal.x + " y: " +raycasthit.Normal.y + " z: " +raycasthit.Normal.z);
                                    if (raycasthit.Normal.x == 0 && raycasthit.Normal.y == 0 && raycasthit.Normal.z == -1) //FRONT FACE
                                    {
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                                        //retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);

                                    }
                                    else if (raycasthit.Normal.x == 0 && raycasthit.Normal.y == 0 && raycasthit.Normal.z == 1) //BACK FACE
                                    {
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);


                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (raycasthit.Normal.x == -1 && raycasthit.Normal.y == 0 && raycasthit.Normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (raycasthit.Normal.x == 1 && raycasthit.Normal.y == 0 && raycasthit.Normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (raycasthit.Normal.x == 0 && raycasthit.Normal.y == -1 && raycasthit.Normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y, (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (raycasthit.Normal.x == 0 && raycasthit.Normal.y == 1 && raycasthit.Normal.z == 0) //TOP FACE
                                    {
                                        //Debug.Log("top face");
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), (y - (planeSize * 0.5f)), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }
                counterForByteChange = 0;
            }
            counterForByteChange++;
        }
        else if (swtcForTypeOfInteract == 1)
        {
            ray = new Ray(transform.position, transform.forward);// Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            //Debug.DrawRay(transform.position, transform.forward * 25, Color.green, 0.001f);

            //var someTouch0 = Input.GetTouch(0);
            ////Debug.Log(""+ someTouch0);

            bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

            if (buttonPressedLeft)
            {
                //Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
            }
            if (buttonPressedRight)
            {
                //Debug.Log("buttonPressedRight:" + buttonPressedRight);
            }

            if (counterForByteChange >= counterForByteChangeMax)
            {
                if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
                {
                    var raycasthit = Raycast(ray, 100, null);

                    if (raycasthit.Rigidbody != null)//Physics.Raycast(ray, out hit, 0.1f))
                    //if (Physics.Raycast(ray, out hit, 100))
                    {
                        if ((JPhysics.BodyTag)raycasthit.Rigidbody.Tag == (JPhysics.BodyTag.jitterCollisionObject)) //if (hit.transform.tag == "collisionObject")
                        {
                            if (GetComponent<Fracture4>() != null)
                            {

                            }
                            else
                            {
                                var chunkX = (int)(Mathf.Round(raycasthit.Rigidbody.Position.X * tileSize) / tileSize);
                                var chunkY = (int)(Mathf.Round(raycasthit.Rigidbody.Position.Y * tileSize) / tileSize);
                                var chunkZ = (int)(Mathf.Round(raycasthit.Rigidbody.Position.Z * tileSize) / tileSize);

                                ////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                                if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)raycasthit.Rigidbody.Position.X, (int)raycasthit.Rigidbody.Position.Y, (int)raycasthit.Rigidbody.Position.Z) != null)
                                {
                                    ////Debug.Log("==count==");
                                    mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)raycasthit.Rigidbody.Position.X, (int)raycasthit.Rigidbody.Position.Y, (int)raycasthit.Rigidbody.Position.Z);

                                    ////Debug.Log("x: " +raycasthit.Normal.x + " y: " +raycasthit.Normal.y + " z: " +raycasthit.Normal.z);
                                    if (raycasthit.Normal.x == 0 && raycasthit.Normal.y == 0 && raycasthit.Normal.z == -1) //FRONT FACE
                                    {
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                                        //retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);

                                    }
                                    else if (raycasthit.Normal.x == 0 && raycasthit.Normal.y == 0 && raycasthit.Normal.z == 1) //BACK FACE
                                    {
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);


                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (raycasthit.Normal.x == -1 && raycasthit.Normal.y == 0 && raycasthit.Normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (raycasthit.Normal.x == 1 && raycasthit.Normal.y == 0 && raycasthit.Normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (raycasthit.Normal.x == 0 && raycasthit.Normal.y == -1 && raycasthit.Normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y, (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (raycasthit.Normal.x == 0 && raycasthit.Normal.y == 1 && raycasthit.Normal.z == 0) //TOP FACE
                                    {
                                        //Debug.Log("top face");
                                        Vector3 p = raycasthit.Point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), (y - (planeSize * 0.5f)), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(raycasthit.Rigidbody.Position.X - retAddPos.x);
                                        var remainsy = Mathf.Abs(raycasthit.Rigidbody.Position.Y - retAddPos.y);
                                        var remainsz = Mathf.Abs(raycasthit.Rigidbody.Position.Z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                        float itx = 0.0f;
                                        float ity = 0.0f;
                                        float itz = 0.0f;

                                        int indexX = 0;
                                        int indexY = 0;
                                        int indexZ = 0;

                                        for (itx = 0; itx < remainsx; itx += planeSize)
                                        {
                                            if (itx >= remainsx)
                                            {
                                                break;
                                            }
                                            indexX++;
                                        }
                                        for (ity = 0; ity < remainsy; ity += planeSize)
                                        {
                                            if (ity >= remainsy)
                                            {
                                                break;
                                            }
                                            indexY++;
                                        }
                                        for (itz = 0; itz < remainsz; itz += planeSize)
                                        {
                                            if (itz >= remainsz)
                                            {
                                                break;
                                            }
                                            indexZ++;
                                        }

                                        indexX -= 1;
                                        indexY -= 1;
                                        indexZ -= 1;

                                        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, JitterExtensions.ToVector3(raycasthit.Rigidbody.Position), indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }
                counterForByteChange = 0;
            }
            counterForByteChange++;
        }
        else if (swtcForTypeOfInteract == 2)
        {
            if (counterForIkFootPlacement >= counterForIkFootPlacementMax)
            {
                //var ray = new Ray(transform.position, -transform.up);


                //Debug.DrawRay(transform.position, -transform.up * 3, Color.green, 0.001f);

                /*if (Physics.Raycast(ray, out hit, 0.25f))
                {

                    //footTarget.transform.position = raycasthit.Point;

                    /*if (hit.transform.tag == "collisionObject")
                    {
                        if (GetComponent<Fracture4>() != null)
                        {

                        }
                        else
                        {

                        }
                    }
                }*/

                /*
                if (raycounterSwtc == 0)
                {
                    for (raylength = 0; raylength < raycounterLoopMax; raylength++)
                    {
                        ray = new Ray(transform.position, -transform.up * (raylength * 0.1f));
                        var raycasthit = Raycast(ray, 10, null);

                        if (raycasthit != null)
                        {
                            //RaycastHitVisualObject.position = raycasthit.Point;

                            if (raycasthit.Rigidbody != null)//Physics.Raycast(ray, out hit, 0.1f))
                            {
                                /*if (raycasthit.Rigidbody.rIndex != this.transform.GetComponent<JRigidBody>().rigidIndex)
                                {

                                }

                                Debug.Log(this.transform.GetComponent<JRigidBody>().rigidIndex);



                                if (lastFrameHitRigidBody == raycasthit.Rigidbody)
                                {
                                    if (lastFrameHitNormal == raycasthit.Normal)
                                    {
                                        Debug.Log("lastFrameHitNormal == raycasthit.Normal");
                                    }
                                    else
                                    {
                                        Debug.Log("lastFrameHitNormal != raycasthit.Normal");
                                    }
                                    if (lastFrameHitPoint == raycasthit.Point)
                                    {
                                        Debug.Log("lastFrameHitPoint == raycasthit.Point");
                                    }
                                    else
                                    {
                                        Debug.Log("lastFrameHitPoint != raycasthit.Point");
                                    }
                                }
                                else
                                {
                                    Debug.Log("lastFrameHitRigidBody != raycasthit.Rigidbody");
                                    RaycastHitVisualObject.position = raycasthit.Point;
                                    break;
                                }

                                lastFrameHitRigidBody = raycasthit.Rigidbody;
                                lastFrameHitNormal = raycasthit.Normal;
                                lastFrameHitPoint = raycasthit.Point;
                                lastFrameHitrIndex = raycasthit.Rigidbody.rIndex;
                            }
                        }
                    }
                    raycounterSwtc = 1;
                }

                if (raycounterSwtc == 1)
                {
                    ray = new Ray(transform.position, -transform.up * (raylength * 0.1f));
                    var raycasthit = Raycast(ray, 10, null);

                    if (raycasthit != null)
                    {
                        //RaycastHitVisualObject.position = raycasthit.Point;

                        if (raycasthit.Rigidbody != null)//Physics.Raycast(ray, out hit, 0.1f))
                        {
                            if (lastFrameHitRigidBody == raycasthit.Rigidbody)
                            {

                            }
                        }
                    }
                }*/











                /*RaycastHit hit;

                var raycasthit = Raycast(ray, 10, null);

                if (raycasthit != null)
                {
                    //RaycastHitVisualObject.position = raycasthit.Point;

                    if (raycasthit.Rigidbody != null)//Physics.Raycast(ray, out hit, 0.1f))
                    {

                        if (lastFrameHitRigidBody== raycasthit.Rigidbody)
                        {
                            if (lastFrameHitNormal == raycasthit.Normal)
                            {
                                Debug.Log("lastFrameHitNormal == raycasthit.Normal");
                            }
                            else
                            {
                                Debug.Log("lastFrameHitNormal != raycasthit.Normal");
                            }
                            if (lastFrameHitPoint == raycasthit.Point)
                            {
                                Debug.Log("lastFrameHitPoint == raycasthit.Point");
                            }
                            else
                            {
                                Debug.Log("lastFrameHitPoint != raycasthit.Point");
                            }
                        }
                        else
                        {
                            Debug.Log("lastFrameHitRigidBody != raycasthit.Rigidbody");
                        }

                        //Debug.Log("index0:" + raycasthit.Rigidbody.Shape.rIndex);
                        if (raycasthit.Distance < 0.1f && raycasthit.Distance != 0 &&
                            raycasthit.Point.x != footTarget.position.x &&
                            raycasthit.Point.y != footTarget.position.y&&
                            raycasthit.Point.z != footTarget.position.z)
                        {
                            Vector3 tempDir = lastFramePosition - legstaticpivot.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                            //IdleStandingTargetPositionVariableLength
                            //Debug.Log("index1:" + raycasthit.Rigidbody.Shape.rIndex);

                            if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                            {

                                Debug.Log("test2");
                                //var originDirection = initialPivotPosition - legstaticpivot.position;
                                //var originDirectionLength = originDirection.magnitude;
                                //tempDir.Normalize();
                                //Vector3 tempVect = (legstaticpivot.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
                                //footTarget.position = tempVect; // raycasthit.Point;
                                //footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);



                                //foottarget.position = IdleStandingTargetPositionMax;

                                //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                                //MOVINGPOINTER.X = tempVect.X;
                                //MOVINGPOINTER.Y = tempVect.Y;
                                //MOVINGPOINTER.Z = tempVect.Z;

                            }
                            else
                            {
                                //if (tempDir.magnitude > 0.5f)
                                //{
                                //    var forwardx = legstaticpivot.forward.x * (tempDir.x * foot.localScale.y);
                                //    var forwardy = legstaticpivot.forward.y * (tempDir.y * foot.localScale.y);
                                //    var forwardz = legstaticpivot.forward.z * (tempDir.z * foot.localScale.y);
                                //    foottarget.position = legstaticpivot.position - (new Vector3(forwardx, forwardy, forwardz));// raycasthit.Point + (tempDir * foot.localScale.y);
                                //}
                                //var originDirection = initialPivotPosition - legstaticpivot.position;
                                //var originDirectionLength = originDirection.magnitude;



                                //var forwardx = legstaticpivot.forward.x + (tempDir.x);
                                //var forwardy = legstaticpivot.forward.y + (tempDir.y);
                                //var forwardz = legstaticpivot.forward.z + (tempDir.z);

                                //footTarget.position = legstaticpivot.position + (new Vector3(forwardx, forwardy, forwardz));
                                var distanceToHitPoint = (raycasthit.Point - legstaticpivot.position).magnitude;


                                //Debug.Log("distanceToHitPoint:" + distanceToHitPoint + " raycasthit.Distance:" + raycasthit.Distance);
                                if (distanceToHitPoint < 0.5f)
                                {
                                    Debug.Log("test0");
                                    var forwardx = legstaticpivot.forward.x * (distanceToHitPoint);
                                    var forwardy = legstaticpivot.forward.y * (distanceToHitPoint);
                                    var forwardz = legstaticpivot.forward.z * (distanceToHitPoint);
                                    footTarget.position = legstaticpivot.position + (new Vector3(forwardx, forwardy, forwardz));
                                }
                                else
                                {
                                    Debug.Log("test1");
                                    footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
                                 
                                }

                                //var distanceToHitPoint = (raycasthit.Point - legstaticpivot.position).magnitude;
                                //var forwardx = legstaticpivot.forward.x * (distanceToHitPoint);
                                //var forwardy = legstaticpivot.forward.y * (distanceToHitPoint);
                                //var forwardz = legstaticpivot.forward.z * (distanceToHitPoint);
                                //footTarget.position = legstaticpivot.position + (new Vector3(forwardx, forwardy, forwardz));
                                //Debug.Log("distanceToHitPoint:" + distanceToHitPoint);
                            }
                        }
                        else
                        {
                            //if (raycasthit.Point.x == footTarget.position.x &&
                            //    raycasthit.Point.y == footTarget.position.y &&
                            //    raycasthit.Point.z == footTarget.position.z)
                            //{
                            //    Debug.Log("***TEST***");
                            //}

                            var distanceToHitPoint = (raycasthit.Point - legstaticpivot.position).magnitude;
                            

                            //Debug.Log("1distanceToHitPoint:" + distanceToHitPoint + " raycasthit.Distance:" + raycasthit.Distance);
                            if (distanceToHitPoint < 0.5f)
                            {
                       
                                Debug.Log("test00");//* foot.localScale.y
                                                    //Debug.DrawRay(raycasthit.Rigidbody.Position + raycasthit.Point, -legstaticpivot.forward, Color.cyan, 0.001f);


                                var camp = footTarget.position;

                                float fraction;
                                Jitter.Dynamics.RigidBody body;
                                JVector normal;

                                bool someResult = JPhysics.collisionSystem.Raycast(JitterExtensions.ToJVector(footTarget.position), JitterExtensions.ToJVector(-transform.up), RaycastCallback, out body, out normal, out fraction);

                                var hitPoint = camp + fraction * -transform.up;

                                RaycastHitVisualObject.position = hitPoint;
                                Debug.DrawRay(footTarget.position, hitPoint * 5, Color.cyan, 0.001f);

                    

                                //var norm = raycasthit.Normal;
                                //var someProjVec = Vector3.ProjectOnPlane(norm, (raycasthit.Point - legstaticpivot.position));
                                //Debug.DrawRay(footTarget.position, someProjVec * 5, Color.cyan, 0.001f);


                                //var forwardx = legstaticpivot.forward.x + (distanceToHitPoint);
                                //var forwardy = legstaticpivot.forward.y + (distanceToHitPoint);
                                //var forwardz = legstaticpivot.forward.z + (distanceToHitPoint);
                                //footTarget.position = legstaticpivot.position + (new Vector3(forwardx, forwardy, forwardz));
                            }
                            else
                            {
                                //Debug.Log("distanceToHitPoint: " + distanceToHitPoint);
                                footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);

                            }

                            //Debug.Log("test2");
                            //footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
                        }
                    }
                    else
                    {
                        Debug.Log("test12");
                        footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
                    }



                    lastFrameHitRigidBody = raycasthit.Rigidbody;
                    lastFrameHitNormal = raycasthit.Normal;
                    lastFrameHitPoint = raycasthit.Point;
                    lastFrameHitrIndex = raycasthit.Rigidbody.rIndex;
}
                else
                {
                    Debug.Log("test12");
                    footTarget.position = legstaticpivot.position + (originDirection * originDirectionLength);
                }*/





                counterForIkFootPlacement = 0;
            }
            counterForIkFootPlacement++;
        }
        else if (swtcForTypeOfInteract == 3)
        {
            /*ray = new Ray(transform.position, transform.forward);//ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * 5, Color.green, 0.001f);

            var raycasthit = Raycast(ray, 0.1f, null);

            if (raycasthit.Rigidbody != null)//Physics.Raycast(ray, out hit, 0.1f))
            {
                if ((JPhysics.BodyTag)raycasthit.Rigidbody.Tag == (JPhysics.BodyTag.jitterCollisionObject))//if (hit.transform.tag == "collisionObject")
                {
                    var sccsfracturescript = raycasthit.Rigidbody.GetComponent<Fracture4>();

                    if (sccsfracturescript != null)
                    {
                        sccsfracturescript.enabled = true;
                    }
                }
                else
                {
                    if (raycasthit.transform.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        if (raycasthit.transform.gameObject.GetComponent<Rigidbody>().isKinematic)
                        {
                            raycasthit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        }
                    }
                }
            }*/
        }
        lastFramePosition = footTarget.position;
    }

    int counterForIkFootPlacement = 0;
    int counterForIkFootPlacementMax = 10;
    int counterForIkFootPlacementSwtc = 0;

    /*||
  raycasthit.Normal.x == 1 &&raycasthit.Normal.y == 0 &&raycasthit.Normal.z == 0 ||
  raycasthit.Normal.x == -1 &&raycasthit.Normal.y == 0 &&raycasthit.Normal.z == 0 ||
  raycasthit.Normal.x == 0 &&raycasthit.Normal.y == 0 &&raycasthit.Normal.z == 1 ||
  raycasthit.Normal.x == 0 &&raycasthit.Normal.y == 1 &&raycasthit.Normal.z == 0 ||
  raycasthit.Normal.x == 0 &&raycasthit.Normal.y == -1 &&raycasthit.Normal.z == 0*/
    public void setAdjacentChunks(mainChunk currentChunk, Vector3 pos, int indexX, int indexY, int indexZ)
    {
        int width = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().width;
        int height = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().height;
        int depth = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().depth;

        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

        if (indexX == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x - 4, (int)pos.y, (int)pos.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x - 4, (int)pos.y, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)width - 1, (int)indexY, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)width - 1, (int)indexY, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexX == width - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x + 4, (int)pos.y, (int)pos.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x + 4, (int)pos.y, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)0, (int)indexY, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk right exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)0, (int)indexY, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexY == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y - 4, (int)pos.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y - 4, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)height - 1, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)height - 1, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexY == height - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y + 4, (int)pos.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y + 4, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)0, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)0, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexZ == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y, (int)pos.z - 4) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y, (int)pos.z - 4);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)indexY, (int)depth - 1) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)depth - 1, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexZ == depth - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y, (int)pos.z + 4) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y, (int)pos.z + 4);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)indexY, (int)0) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)0, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider triggercollision)
    {

        if (swtcForTypeOfInteract == 3)
        {
            //Debug.Log("collider trigger hit:" + triggercollision.transform.name);
            if (triggercollision.transform.gameObject.GetComponent<Rigidbody>() != null)
            {

                if (triggercollision.transform.gameObject.GetComponent<Rigidbody>().isKinematic)
                {
                    triggercollision.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
            if (triggercollision.transform.tag == "collisionObject")
            {
                var sccsfracturescript = triggercollision.transform.gameObject.GetComponent<Fracture4>();

                if (sccsfracturescript != null)
                {
                    sccsfracturescript.enabled = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*Debug.Log("object collision:" + collision.transform.name);
        if (collision.transform.tag == "collisionObject")
        {
            var sccsfracturescript = collision.transform.gameObject.GetComponent<Fracture4>();

            if (sccsfracturescript != null)
            {
                sccsfracturescript.enabled = true;
            }
        }*/

    }

}


//by robertbu
/*//https://answers.unity.com/questions/540888/converting-mouse-position-to-world-stationary-came.html 
if (Input.GetMouseButtonDown(0))
{
    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit))
    {
        //hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y,(int)hit.transform.position.z) != null)
        {
            var chunkX = (int)(Mathf.Round(raycasthit.Point.x * planeSize) / planeSize);
            var chunkY = (int)(Mathf.Round(raycasthit.Point.y * planeSize) / planeSize);
            var chunkZ = (int)(Mathf.Round(raycasthit.Point.z * planeSize) / planeSize);

            //Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);
            retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
            //yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * -2);
            //retDel.position = p;
        }
    }
}*/



/*Vector3 p = raycasthit.Point -raycasthit.Normal / 4;
float offset = planeSize / 2;
//float offset = 0;


float offset2 = planeSize / 2;

if (raycasthit.Normal.x == 0 &&raycasthit.Normal.y == 1 &&raycasthit.Normal.z == 0)
{
    ////Debug.Log("yo0");

    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset * multiplicator, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    //retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Round(p.y * suppressorPos) / suppressorPos) + offset * multiplicator, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset * multiplicatorReticle, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Round(p.y * suppressorPos) / suppressorPos) + offset * 5, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);
}

if (raycasthit.Normal.x == 0 &&raycasthit.Normal.y == 0 &&raycasthit.Normal.z == -1)
{
    ////Debug.Log("yo1");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 5);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
}

if (raycasthit.Normal.x == 1 &&raycasthit.Normal.y == 0 &&raycasthit.Normal.z == 0)
{
    ////Debug.Log("yo2");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
}


if (raycasthit.Normal.x == -1 &&raycasthit.Normal.y == 0 &&raycasthit.Normal.z == 0)
{
    ////Debug.Log("yo2");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
}

if (raycasthit.Normal.x == 0 &&raycasthit.Normal.y == 0 &&raycasthit.Normal.z == 1)
{
    ////Debug.Log("yo1");

    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 3);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 6);

}


if (raycasthit.Normal.x == 0 &&raycasthit.Normal.y == -1 &&raycasthit.Normal.z == 0)
{
    ////Debug.Log("yo1");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 3, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

}



var chunkX = (int)(Mathf.Round(hit.transform.position.x * tileSize) / tileSize);
var chunkY = (int)(Mathf.Round(hit.transform.position.y * tileSize) / tileSize);
var chunkZ = (int)(Mathf.Round(hit.transform.position.z * tileSize) / tileSize);

////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
{

    ////Debug.Log("==count==");
    mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z);

    if (Input.GetMouseButtonDown(0))
    {
        int x = (int)(((yoMan1.x * 1) / 1) / 1); //WORKING
        int y = (int)(((yoMan1.y * 1) / 1) / 1);//WORKING
        int z = (int)(((yoMan1.z * 1) / 1) / 1);//WORKING
                                           //terrain1.GetChunk(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);

        var planetchunk = hit.transform;
        planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)x, (int)y, (int)z, activeBlockType);

        planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
        planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

        setAdjacentChunks(currentChunk, hit, x, y, z);
    }


    if (Input.GetMouseButtonDown(1))
    {
        ////Debug.Log(raycasthit.Normal);
        //Debug.DrawRay(raycasthit.Point, Vector3.up * 10, Color.red, 0.1f);

        int x = (int)(((yoMan.x * 1) / 1) / 1); //WORKING
        int y = (int)(((yoMan.y * 1) / 1) / 1);//WORKING
        int z = (int)(((yoMan.z * 1) / 1) / 1);//WORKING

        //terrain1.GetChunk(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
        var planetchunk = hit.transform;
        planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)x, (int)y, (int)z, activeBlockType);

        planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
        planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

        setAdjacentChunks(currentChunk, hit, x, y, z);
    }

}*/
























/*
private void OnTriggerEnter(Collider other)
{
    Debug.Log("object hit:" + other.transform.name);
}

private void OnCollisionEnter(Collision collision)
{
    //var closestPointOnBounds = other.ClosestPointOnBounds(transform.position);


    ////Debug.Log("col:" + other.transform.name);
    var sccsfracturescript = other.transform.gameObject.GetComponent<Fracture4>();

    if (sccsfracturescript != null)
    {
        sccsfracturescript.enabled = true;
    }

    //Ray ray = new Ray(transform.position, transform.forward);// Camera.main.ScreenPointToRay(Input.mousePosition);
    //RaycastHit hit;
    //Debug.DrawRay(transform.position, transform.forward * 25, Color.green, 0.001f);

    //var someTouch0 = Input.GetTouch(0);
    ////Debug.Log(""+ someTouch0);

    //bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
    //bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

    //if (buttonPressedLeft)
    {
        //Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
    }
    //if (buttonPressedRight)
    {
        //Debug.Log("buttonPressedRight:" + buttonPressedRight);
    }

    //if (counterForByteChange >= counterForByteChangeMax)
    {
        //if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
        {
            //if (Physics.Raycast(ray, out hit))
            {
                if (collision.transform.tag == "collisionObject")
                {
                    var chunkX = (int)(Mathf.Round(collision.transform.position.x * tileSize) / tileSize);
                    var chunkY = (int)(Mathf.Round(collision.transform.position.y * tileSize) / tileSize);
                    var chunkZ = (int)(Mathf.Round(collision.transform.position.z * tileSize) / tileSize);

                    ////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                    if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z) != null)
                    {
                        ////Debug.Log("==count==");
                        mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z);

                        ////Debug.Log("x: " + collision.normal.x + " y: " + collision.normal.y + " z: " + collision.normal.z);
                        if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == -1) //FRONT FACE
                        {
                            Vector3 p = collision.contacts[0].point;

                            var x = (Mathf.Round(p.x * tileSize) / tileSize);
                            var y = (Mathf.Round(p.y * tileSize) / tileSize);
                            var z = (Mathf.Round(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                            ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                            float itx = 0.0f;
                            float ity = 0.0f;
                            float itz = 0.0f;

                            int indexX = 0;
                            int indexY = 0;
                            int indexZ = 0;

                            for (itx = 0; itx < remainsx; itx += planeSize)
                            {
                                if (itx >= remainsx)
                                {
                                    break;
                                }
                                indexX++;
                            }
                            for (ity = 0; ity < remainsy; ity += planeSize)
                            {
                                if (ity >= remainsy)
                                {
                                    break;
                                }
                                indexY++;
                            }
                            for (itz = 0; itz < remainsz; itz += planeSize)
                            {
                                if (itz >= remainsz)
                                {
                                    break;
                                }
                                indexZ++;
                            }

                            indexX -= 1;
                            indexY -= 1;
                            indexZ -= 1;

                            ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                            setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                            var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                            UnityTutorialPooledObject.transform.position = retAddPos;

                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                            UnityTutorialPooledObject.SetActive(true);

                        }
                        else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 1) //BACK FACE
                        {
                            Vector3 p = collision.contacts[0].point;

                            var x = (Mathf.Round(p.x * tileSize) / tileSize);
                            var y = (Mathf.Round(p.y * tileSize) / tileSize);
                            var z = (Mathf.Round(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                            ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                            float itx = 0.0f;
                            float ity = 0.0f;
                            float itz = 0.0f;

                            int indexX = 0;
                            int indexY = 0;
                            int indexZ = 0;

                            for (itx = 0; itx < remainsx; itx += planeSize)
                            {
                                if (itx >= remainsx)
                                {
                                    break;
                                }
                                indexX++;
                            }
                            for (ity = 0; ity < remainsy; ity += planeSize)
                            {
                                if (ity >= remainsy)
                                {
                                    break;
                                }
                                indexY++;
                            }
                            for (itz = 0; itz < remainsz; itz += planeSize)
                            {
                                if (itz >= remainsz)
                                {
                                    break;
                                }
                                indexZ++;
                            }

                            indexX -= 1;
                            indexY -= 1;
                            indexZ -= 1;

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);
                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                            setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);


                            var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                            UnityTutorialPooledObject.transform.position = retAddPos;

                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                            UnityTutorialPooledObject.SetActive(true);
                        }
                        else if (collision.contacts[0].normal.x == -1 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 0) //BACK FACE
                        {
                            Vector3 p = collision.contacts[0].point;

                            var x = (Mathf.Round(p.x * tileSize) / tileSize);
                            var y = (Mathf.Round(p.y * tileSize) / tileSize);
                            var z = (Mathf.Round(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                            ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                            float itx = 0.0f;
                            float ity = 0.0f;
                            float itz = 0.0f;

                            int indexX = 0;
                            int indexY = 0;
                            int indexZ = 0;

                            for (itx = 0; itx < remainsx; itx += planeSize)
                            {
                                if (itx >= remainsx)
                                {
                                    break;
                                }
                                indexX++;
                            }
                            for (ity = 0; ity < remainsy; ity += planeSize)
                            {
                                if (ity >= remainsy)
                                {
                                    break;
                                }
                                indexY++;
                            }
                            for (itz = 0; itz < remainsz; itz += planeSize)
                            {
                                if (itz >= remainsz)
                                {
                                    break;
                                }
                                indexZ++;
                            }

                            indexX -= 1;
                            indexY -= 1;
                            indexZ -= 1;

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                            setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                            var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                            UnityTutorialPooledObject.transform.position = retAddPos;

                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                            UnityTutorialPooledObject.SetActive(true);
                        }
                        else if (collision.contacts[0].normal.x == 1 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 0) //BACK FACE
                        {
                            Vector3 p = collision.contacts[0].point;

                            var x = (Mathf.Round(p.x * tileSize) / tileSize);
                            var y = (Mathf.Round(p.y * tileSize) / tileSize);
                            var z = (Mathf.Round(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                            ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                            float itx = 0.0f;
                            float ity = 0.0f;
                            float itz = 0.0f;

                            int indexX = 0;
                            int indexY = 0;
                            int indexZ = 0;

                            for (itx = 0; itx < remainsx; itx += planeSize)
                            {
                                if (itx >= remainsx)
                                {
                                    break;
                                }
                                indexX++;
                            }
                            for (ity = 0; ity < remainsy; ity += planeSize)
                            {
                                if (ity >= remainsy)
                                {
                                    break;
                                }
                                indexY++;
                            }
                            for (itz = 0; itz < remainsz; itz += planeSize)
                            {
                                if (itz >= remainsz)
                                {
                                    break;
                                }
                                indexZ++;
                            }

                            indexX -= 1;
                            indexY -= 1;
                            indexZ -= 1;

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                            setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                            var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                            UnityTutorialPooledObject.transform.position = retAddPos;

                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                            UnityTutorialPooledObject.SetActive(true);
                        }
                        else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == -1 && collision.contacts[0].normal.z == 0) //BACK FACE
                        {
                            Vector3 p = collision.contacts[0].point;

                            var x = (Mathf.Round(p.x * tileSize) / tileSize);
                            var y = (Mathf.Round(p.y * tileSize) / tileSize);
                            var z = (Mathf.Round(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y, (z - (planeSize * 0.5f)));

                            retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                            ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                            float itx = 0.0f;
                            float ity = 0.0f;
                            float itz = 0.0f;

                            int indexX = 0;
                            int indexY = 0;
                            int indexZ = 0;

                            for (itx = 0; itx < remainsx; itx += planeSize)
                            {
                                if (itx >= remainsx)
                                {
                                    break;
                                }
                                indexX++;
                            }
                            for (ity = 0; ity < remainsy; ity += planeSize)
                            {
                                if (ity >= remainsy)
                                {
                                    break;
                                }
                                indexY++;
                            }
                            for (itz = 0; itz < remainsz; itz += planeSize)
                            {
                                if (itz >= remainsz)
                                {
                                    break;
                                }
                                indexZ++;
                            }

                            indexX -= 1;
                            indexY -= 1;
                            indexZ -= 1;

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                            setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                            var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                            UnityTutorialPooledObject.transform.position = retAddPos;

                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                            UnityTutorialPooledObject.SetActive(true);
                        }
                        else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 1 && collision.contacts[0].normal.z == 0) //TOP FACE
                        {
                            //Debug.Log("top face");
                            Vector3 p = collision.contacts[0].point;

                            var x = (Mathf.Round(p.x * tileSize) / tileSize);
                            var y = (Mathf.Round(p.y * tileSize) / tileSize);
                            var z = (Mathf.Round(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), (y - (planeSize * 0.5f)), (z - (planeSize * 0.5f)));

                            retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                            ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                            float itx = 0.0f;
                            float ity = 0.0f;
                            float itz = 0.0f;

                            int indexX = 0;
                            int indexY = 0;
                            int indexZ = 0;

                            for (itx = 0; itx < remainsx; itx += planeSize)
                            {
                                if (itx >= remainsx)
                                {
                                    break;
                                }
                                indexX++;
                            }
                            for (ity = 0; ity < remainsy; ity += planeSize)
                            {
                                if (ity >= remainsy)
                                {
                                    break;
                                }
                                indexY++;
                            }
                            for (itz = 0; itz < remainsz; itz += planeSize)
                            {
                                if (itz >= remainsz)
                                {
                                    break;
                                }
                                indexZ++;
                            }

                            indexX -= 1;
                            indexY -= 1;
                            indexZ -= 1;

                            ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                            setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                            var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                            UnityTutorialPooledObject.transform.position = retAddPos;

                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                            UnityTutorialPooledObject.SetActive(true);
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
        counterForByteChange = 0;
    }
    counterForByteChange++;
}*/






//Debug.Log("index:" + raycasthit.Rigidbody.rIndex);
//Debug.Log("index:" + raycasthit.Rigidbody.Shape.rIndex);

/*JPhysics.BodyTag arrayOfTag = (JPhysics.BodyTag)raycasthit.Rigidbody.Tag;

if ((JPhysics.BodyTag)arrayOfTag == (JPhysics.BodyTag.jitterCollisionObject))
{
    Debug.Log("touch down distance: " + raycasthit.Distance);

    Vector3 tempDir = initialPivotPosition - legstaticpivot.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

    //IdleStandingTargetPositionVariableLength


    if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
    {
        footTarget.position = IdleStandingTargetPositionMax;
        tempDir.Normalize();
        //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
        Vector3 tempVect = (legstaticpivot.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
        //MOVINGPOINTER.X = tempVect.X;
        //MOVINGPOINTER.Y = tempVect.Y;
        //MOVINGPOINTER.Z = tempVect.Z;
        footTarget.position = tempVect; // raycasthit.Point;
    }
    else
    {
        if (tempDir.magnitude > 0.5f)
        {
            var forwardx = legstaticpivot.forward.x * (tempDir.x * foot.localScale.y);
            var forwardy = legstaticpivot.forward.y * (tempDir.y * foot.localScale.y);
            var forwardz = legstaticpivot.forward.z * (tempDir.z * foot.localScale.y);


            footTarget.position = legstaticpivot.position + (new Vector3(forwardx, forwardy, forwardz));// raycasthit.Point + (tempDir * foot.localScale.y);

        }
    }



    /*if (tempDir.magnitude < (totallegLength * 0.5f))
    {
        footTarget.position = IdleStandingTargetPositionMin;
    }
    else
    {
        footTarget.position = raycasthit.Point;
    }

}*/
