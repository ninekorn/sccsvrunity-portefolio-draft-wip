using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class sccsBulletInteraction : MonoBehaviour
{
    //public GameObject somePointer0;
    //GameObject somePointer1;
    //GameObject somePointer2;

    float angularvelocitymax = 0.00000001f;
    float velocitymax = 0.00000001f;


    bool somespherecasthitbool = false;
    bool someraycasthitbool = false;

    public Transform pickaxetiptransform;
    int addfracturedcubeonimpact = 0;
    public float raycounterLoopMax = 20;

    int InitcounterForIkFootPlacement = 0;
    public int InitcounterForIkFootPlacementMax = 10;
    int InitcounterForIkFootPlacementSwtc = 0;
    float raylength = 0;
    float raycounterSwtc = 0;

    int counterForByteChangeMax = 1;

    /*public Transform upperleg;
    public Transform lowerleg;
    public Transform foot;
    public Transform foottarget;
    public Transform legstaticpivot;*/

    float upperleglength = 0;
    float lowerleglength = 0;
    float footlength = 0;
    float totallegLength = 0;

    public LayerMask layerMask;

    Vector3 IdleStandingTargetPositionVariableLength;
    Vector3 IdleStandingTargetPositionMax;
    Vector3 IdleStandingTargetPositionMin;
    RaycastHit spherecasthit;
    public int swtcForTypeOfInteract = 0;
    //0 == 
    Vector3 thepos;
    //public Transform footTarget;


    public Transform planetmanager;

    public byte activeBlockType = 0;
    public Transform retAdd, retDel;

    Mesh mesh;

    //public Transform sphere;
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

    /*float planeSize = 0.25f;
    int multiplicator = 1;
    int multiplicatorReticle = 3;
    int tileSize = 4;
    int suppressorPos = 4;*/

    float planeSize = 0.1f;
    int multiplicator = 1;
    int multiplicatorReticle = 3;
    int tileSize = 1;
    int suppressorPos = 1;
    int chunkWidth = 10;
    int counterForByteChange = 0;

    Stopwatch stopwatch = new Stopwatch();

    Stopwatch stopwatchspherecast = new Stopwatch();
    Stopwatch stopwatchraycast = new Stopwatch();

    Rigidbody currentrigidbody;

    Rigidbody currentBulletrigidbody;

    public float projectileimpulse = 1000;

    void Start()
    {
        currentrigidbody = this.GetComponent<Rigidbody>();
        currentBulletrigidbody = this.GetComponent<Rigidbody>();
        //somePointer1 = Instantiate(somePointer0, this.transform.position, Quaternion.identity);
        //somePointer2 = Instantiate(somePointer0, this.transform.position, Quaternion.identity);

        /*if (swtcForTypeOfInteract == 2)
        {
            upperleglength = upperleg.localScale.z;
            lowerleglength = lowerleg.localScale.z;
            footlength = foot.localScale.z;
            totallegLength = upperleglength + lowerleglength + footlength;

            IdleStandingTargetPositionMax = transform.position + ((transform.forward * upperleglength) + (transform.forward * lowerleglength) + (transform.forward * footlength));
            IdleStandingTargetPositionMin = transform.position + ((transform.forward * (upperleglength)) + (transform.forward * (lowerleglength)) + (transform.forward * (footlength)) * 0.5f);

        }*/

        //retAdd.localScale = retAdd.localScale * planeSize;
        //retDel.localScale = retDel.localScale * planeSize;
        fraction = 1 / planeSize;
        radius = planeSize / 2;
        diameter = 0.1f;
        whatever = 1 / (diameter * 2);
        cam = Camera.main;
        stopwatch.Start();
        tippickaxestopwatch.Start();
        stopwatchspherecast.Start();
        stopwatchraycast.Start();
    }

    int ontriggerstaycounter = 0;
    int ontriggerstaycounterMax = 50;
    int ontriggerstaycounterSwtc = 0;

    int ontriggerentercounter = 0;
    int ontriggerentercounterMax = 50;
    int ontriggerentercounterSwtc = 0;

    int ontriggerexitcounter = 0;
    int ontriggerexitcounterMax = 50;
    int ontriggerexitcounterSwtc = 0;


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("object hit OnTriggerEnter:" + other.transform.name);

        if (ontriggerentercounter >= ontriggerentercounterMax)
        {


            if (GetComponent<Fracture4>() != null)
            {

            }
            else
            {
                var closestPointOnBounds = other.ClosestPointOnBounds(transform.position);

                //retAdd.transform.position = closestPointOnBounds;


                float posx = (closestPointOnBounds.x);
                float posy = (closestPointOnBounds.y);
                float posz = (closestPointOnBounds.z);


                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                //retAdd.position = chunkbytepos;

                int indexX = 0;
                int indexY = 0;
                int indexZ = 0;


                if (posx < 0)
                {
                    posx *= -1;
                }

                if (posx >= 1.0f)
                {
                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                }
                else
                {
                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                }



                if (posy < 0)
                {
                    posy *= -1;
                }

                if (posy >= 1.0f)
                {
                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                }
                else
                {
                    //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                    indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                }


                if (posz < 0)
                {
                    posz *= -1;
                }


                //Debug.Log("posz:" + posz);
                if (posz >= 1.0f)
                {

                    indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                }
                else
                {
                    indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                }

                if (indexX < 0)
                {
                    indexX *= -1;
                }


                if (indexY < 0)
                {
                    indexY *= -1;
                }

                if (indexZ < 0)
                {
                    indexZ *= -1;
                    indexZ = chunkWidth - indexZ;
                }


                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                mainChunkGen2 currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)other.transform.position.x, (int)other.transform.position.y, (int)other.transform.position.z);

                currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

            }
            ontriggerentercounter = 0;
        }
        ontriggerentercounter++;
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


    private void OnCollisionEnterNot(Collision collision)
    {
        Debug.Log("object hit OnCollisionEnter:" + collision.transform.name);

        if (GetComponent<Fracture4>() != null)
        {

        }
        else
        {
            var closestPointOnBounds = collision.contacts[0].point;// (transform.position);

            //retAdd.transform.position = closestPointOnBounds;


            float posx = (closestPointOnBounds.x);
            float posy = (closestPointOnBounds.y);
            float posz = (closestPointOnBounds.z);


            Vector3 chunkbytepos = new Vector3(posx, posy, posz);

            //retAdd.position = chunkbytepos;

            int indexX = 0;
            int indexY = 0;
            int indexZ = 0;


            if (posx < 0)
            {
                posx *= -1;
            }

            if (posx >= 1.0f)
            {
                indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
            }
            else
            {
                indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
            }



            if (posy < 0)
            {
                posy *= -1;
            }

            if (posy >= 1.0f)
            {
                indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
            }
            else
            {
                //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
            }


            if (posz < 0)
            {
                posz *= -1;
            }


            //Debug.Log("posz:" + posz);
            if (posz >= 1.0f)
            {

                indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
            }
            else
            {
                indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
            }

            if (indexX < 0)
            {
                indexX *= -1;
            }


            if (indexY < 0)
            {
                indexY *= -1;
            }

            if (indexZ < 0)
            {
                indexZ *= -1;
                indexZ = chunkWidth - indexZ;
            }


            ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
            ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

            mainChunkGen2 currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z);

            currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

            currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
            currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

        }
    }



    RaycastHit hit;

    Ray ray;
    Vector3 positionThisObject;
    Vector3 directionForwardOfThisObject;

    Stopwatch tippickaxestopwatch = new Stopwatch();

    void Update()
    {

        /*Vector3 framevelocity = currentrigidbody.velocity;
        Vector3 frameangularVelocityShadow = currentrigidbody.angularVelocity;

        if (framevelocity.magnitude < velocitymax && frameangularVelocityShadow.magnitude < angularvelocitymax)
        {
            //this.enabled = false;
        }*/




        /*
        bool thumbstickleft = OVRInput.Get(OVRInput.Touch.SecondaryThumbstick, OVRInput.Controller.LTouch);
        bool thumbstickright = OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight, OVRInput.Controller.LTouch);
        bool thumbstickdown = OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown, OVRInput.Controller.LTouch);
        bool thumbstickup = OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp, OVRInput.Controller.LTouch);*/

        if (InitcounterForIkFootPlacementSwtc == 0)
        {
            if (InitcounterForIkFootPlacement >= InitcounterForIkFootPlacementMax)
            {
                //Debug.Log("***INIT COUNTER REACHED. can start ray***");
                InitcounterForIkFootPlacementSwtc = 1;
                InitcounterForIkFootPlacement = 0;
            }
            InitcounterForIkFootPlacement++;
        }



        if (counterForByteChange == 1)
        {
            if (stopwatch.ElapsedTicks >= counterForByteChangeMax)
            {
                stopwatch.Restart();
                counterForByteChange = 0;
            }
        }

        if (swtcForTypeOfInteract == 0)
        {
            ray = new Ray(transform.position, currentBulletrigidbody.velocity.normalized);//ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            //Debug.DrawRay(transform.position, transform.forward * 0.05f, Color.red, 0.001f);

            //var someTouch0 = Input.GetTouch(0);
            //////Debug.Log(""+ someTouch0);

            /*bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

            if (buttonPressedLeft)
            {
                ////Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
            }
            if (buttonPressedRight)
            {
                ////Debug.Log("buttonPressedRight:" + buttonPressedRight);
            }*/

            if (counterForByteChange == 0)// counterForByteChangeMax)
            {
                if (InitcounterForIkFootPlacementSwtc == 1)
                {
                    //Debug.Log("test0");
                    if (raycounterSwtc == 0 || raycounterSwtc == 1)
                    {
                        //Debug.Log("test1");
                        if (raylength < raycounterLoopMax)
                        {
                            //Debug.Log("test2");
                            if (tippickaxestopwatch.Elapsed.Ticks >= 1)
                            {
                                //Debug.Log("test3");
                                for (int r = 0; r < 1; r++)
                                {
                                    //Debug.Log("test4");
                                    var ray = new Ray(pickaxetiptransform.transform.position + (currentBulletrigidbody.velocity.normalized * (raylength * 0.025f)*10), currentBulletrigidbody.velocity.normalized * 0.005f*10);
                                    Debug.DrawRay(pickaxetiptransform.transform.position + (currentBulletrigidbody.velocity.normalized * (raylength * 0.025f)*10), currentBulletrigidbody.velocity.normalized * 0.005f*10, Color.white, 0.001f);

                               
                      


                                    if (stopwatchspherecast.Elapsed.Ticks > 1)
                                    {
                                        somespherecasthitbool = Physics.SphereCast(pickaxetiptransform.transform.position, 0.05f, pickaxetiptransform.forward, out spherecasthit, 0.05f, layerMask);
                                        stopwatchspherecast.Restart();
                                    }

                                    if (stopwatchraycast.Elapsed.Ticks > 1)
                                    {
                                        someraycasthitbool = Physics.Raycast(ray, out hit, (raylength * 0.05f), layerMask);
                                        stopwatchraycast.Restart();
                                    }
                               

                                    //spherecasthit.triangleIndex                      

                                    if (somespherecasthitbool)
                                    {
                                        //Debug.Log("test5");
                                        MeshCollider meshCollider = spherecasthit.collider as MeshCollider;
                                        if (meshCollider != null || meshCollider.sharedMesh != null)
                                        {
                                            //Debug.Log("drawing mesh hit face triangles");
                                            Mesh somemesh = meshCollider.sharedMesh;
                                            Vector3[] vertices = somemesh.vertices;
                                            int[] triangles = somemesh.triangles;

                                            Vector3 p0 = vertices[triangles[spherecasthit.triangleIndex * 3 + 0]];
                                            Vector3 p1 = vertices[triangles[spherecasthit.triangleIndex * 3 + 1]];
                                            Vector3 p2 = vertices[triangles[spherecasthit.triangleIndex * 3 + 2]];

                                            Transform hitTransform = spherecasthit.collider.transform;
                                            p0 = hitTransform.TransformPoint(p0);
                                            p1 = hitTransform.TransformPoint(p1);
                                            p2 = hitTransform.TransformPoint(p2);

                                            //Debug.DrawRay(p0, (p1 - p0).normalized, Color.red, 1000);
                                            //Debug.DrawRay(p1, (p2 - p1).normalized, Color.red, 1000);
                                            //Debug.DrawRay(p2, (p0 - p2).normalized, Color.red, 1000);

                                            Debug.DrawLine(p0, (p1), Color.red, 1);
                                            Debug.DrawLine(p1, (p2), Color.red, 1);
                                            Debug.DrawLine(p2, (p0), Color.red, 1);

                                            //somePointer0.transform.position = p0;
                                            //somePointer1.transform.position = p1;
                                            //somePointer2.transform.position = p2;

                                            var perp = Vector3.Cross((p1 - p0).normalized, (p2 - p1).normalized);
                                            perp.Normalize();
                                            Debug.DrawLine(hitTransform.transform.position, (hitTransform.transform.position + perp), Color.magenta, 5);
                                            var barycentriccoord = spherecasthit.barycentricCoordinate;



                                            thepos = spherecasthit.point;// barycentriccoord;



                                            //Debug.Log("test6");
                                            var chunkX = (int)(Mathf.Round(thepos.x * tileSize) / tileSize);
                                            var chunkY = (int)(Mathf.Round(thepos.y * tileSize) / tileSize);
                                            var chunkZ = (int)(Mathf.Round(thepos.z * tileSize) / tileSize);

                                            //////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                                            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)thepos.x, (int)thepos.y, (int)thepos.z) != null)
                                            {
                                                //Debug.Log("==count==");

                                                mainChunkGen2 currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)thepos.x, (int)thepos.y, (int)thepos.z);

                                                //Debug.Log("x: " + perp.x + " y: " + perp.y + " z: " + perp.z);
                                                if (perp.x == 0 && perp.y == 0 && perp.z == -1) //BACK FACE
                                                {
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                    }



                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                        indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                    }


                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }


                                                    //Debug.Log("posz:" + posz);
                                                    if (posz >= 1.0f)
                                                    {

                                                        indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                                                    }

                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }


                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                        indexZ = chunkWidth - indexZ;
                                                    }


                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));


                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();


                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                    if (addfracturedcubeonimpact == 1)
                                                    {
                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                        UnityTutorialPooledObject.SetActive(true);
                                                    }

                                                    Vector3 velocity = currentrigidbody.velocity;
                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                    {
                                                        //this.enabled = false;
                                                    }

                                                }
                                                else if (perp.x == 0 && perp.y == 0 && perp.z == 1) //FRONT FACE
                                                {
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                    }



                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                    }


                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    ////Debug.Log("posz:" + posz);
                                                    if (posz >= 1.0f)
                                                    {
                                                        indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                                                        ////Debug.Log("0indexZ:" + indexZ);
                                                        if (indexZ < 0)
                                                        {
                                                            indexZ *= -1;
                                                            if (indexZ >= 5)
                                                            {
                                                                ////Debug.Log("2indexZ:" + indexZ);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexZ:" + indexZ);
                                                            }
                                                            indexZ = (chunkWidth - 1) - indexZ;
                                                            ////Debug.Log("4indexZ:" + indexZ);

                                                        }
                                                        else
                                                        {
                                                            if (indexZ == 0)
                                                            {
                                                                indexZ = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexZ:" + indexZ);
                                                                if (indexZ >= 5)
                                                                {
                                                                    indexZ -= 1;
                                                                    ////Debug.Log("2indexZ:" + indexZ);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexZ:" + indexZ);
                                                                    indexZ -= 1;
                                                                }
                                                            }
                                                        }

                                                        ////Debug.Log("11indexZ:" + indexZ);
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                                                        ////Debug.Log("0indexZ:" + indexZ);
                                                        if (indexZ < 0)
                                                        {

                                                            indexZ *= -1;
                                                            if (indexZ >= 5)
                                                            {
                                                                ////Debug.Log("2indexZ:" + indexZ);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexZ:" + indexZ);
                                                            }
                                                            indexZ = (chunkWidth - 1) - indexZ;
                                                            ////Debug.Log("4indexZ:" + indexZ);

                                                        }
                                                        else
                                                        {
                                                            if (indexZ == 0)
                                                            {
                                                                indexZ = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexZ:" + indexZ);
                                                                if (indexZ >= 5)
                                                                {
                                                                    indexZ -= 1;
                                                                    ////Debug.Log("2indexZ:" + indexZ);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexZ:" + indexZ);
                                                                    indexZ -= 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }

                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                    }

                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));


                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                    if (addfracturedcubeonimpact == 1)
                                                    {
                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                        UnityTutorialPooledObject.SetActive(true);
                                                    }
                                                    Vector3 velocity = currentrigidbody.velocity;
                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                    {
                                                        //this.enabled = false;
                                                    }
                                                }
                                                else if (perp.x == -1 && perp.y == 0 && perp.z == 0) //LEFT FACE
                                                {
                                                    /*Vector3 p = spherecasthit.point;// spherecasthit.point; // this.transform.TransformPoint(thepointinquestion);

                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    if (posx > 1.0f)
                                                    {
                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));

                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                    }



                                                    /*if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    if (posy > 1.0f)
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexY = Mathf.FloorToInt((Mathf.Round(posy * chunkWidth))); //indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }




                                                    /*if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    if (posz > 1.0f)
                                                    {
                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                    }



                                                    //indexZ = (chunkWidth - 1) - indexZ;


                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                        //indexX = (chunkWidth - 1)- indexX;
                                                    }


                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                        //indexY = (chunkWidth - 1) - indexY;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                        //indexZ = (chunkWidth - 1) - indexZ;
                                                    }*/
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    ////Debug.Log("posz:" + posz);
                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.RoundToInt(((((posx - Mathf.Round(posx))) * chunkWidth)));


                                                        //Debug.Log("0indexX:" + indexX);
                                                        if (indexX < 0)
                                                        {

                                                            if (indexX >= -4)
                                                            {
                                                                //indexX += 1;
                                                                indexX *= -1;
                                                                indexX = (chunkWidth - 1) - indexX;
                                                                indexX += 1;
                                                                ////Debug.Log("2indexX:" + indexX);
                                                            }
                                                            else
                                                            {
                                                                //indexX += 1;
                                                                //indexX -= 1;
                                                                ////Debug.Log("3indexX:" + indexX);
                                                            }
                                                            //indexX = (chunkWidth - 1) - indexX;
                                                            ////Debug.Log("4indexX:" + indexX);

                                                        }
                                                        else
                                                        {
                                                            if (indexX == 0)
                                                            {
                                                                //indexX = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                /*////Debug.Log("1indexX:" + indexX);
                                                                if (indexX >= 5)
                                                                {
                                                                    indexX -= 1;
                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                    indexX -= 1;
                                                                }*/
                                                            }
                                                        }

                                                        ////Debug.Log("11indexX:" + indexX);
                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                        //Debug.Log("0indexX:" + indexX);
                                                        if (indexX < 0)
                                                        {

                                                            indexX *= -1;
                                                            if (indexX >= 5)
                                                            {
                                                                ////Debug.Log("2indexX:" + indexX);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexX:" + indexX);
                                                            }
                                                            indexX = (chunkWidth - 1) - indexX;
                                                            ////Debug.Log("4indexX:" + indexX);

                                                        }
                                                        else
                                                        {
                                                            if (indexX == 0)
                                                            {
                                                                //indexX = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexX:" + indexX);
                                                                /*if (indexX >= 5)
                                                                {
                                                                    indexX -= 1;
                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                    indexX -= 1;
                                                                }*/
                                                            }
                                                        }
                                                    }


                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    if (posz >= 1.0f)
                                                    {
                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                    }



                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                        indexY = Mathf.FloorToInt((Mathf.Round(posy * chunkWidth)));
                                                    }



                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }

                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                    }


                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));


                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                    if (addfracturedcubeonimpact == 1)
                                                    {
                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                        UnityTutorialPooledObject.SetActive(true);
                                                    }
                                                    Vector3 velocity = currentrigidbody.velocity;
                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                    {
                                                        //this.enabled = false;
                                                    }
                                                }
                                                else if (perp.x == 1 && perp.y == 0 && perp.z == 0) //RIGHT FACE
                                                {
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    ////Debug.Log("posz:" + posz);
                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.RoundToInt(((((posx - Mathf.Round(posx))) * chunkWidth)));
                                                        ////Debug.Log("0indexX:" + indexX);
                                                        if (indexX < 0)
                                                        {
                                                            indexX *= -1;
                                                            if (indexX >= 5)
                                                            {
                                                                ////Debug.Log("2indexX:" + indexX);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexX:" + indexX);
                                                            }
                                                            indexX = (chunkWidth - 1) - indexX;
                                                            ////Debug.Log("4indexX:" + indexX);

                                                        }
                                                        else
                                                        {
                                                            if (indexX == 0)
                                                            {
                                                                indexX = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexX:" + indexX);
                                                                if (indexX >= 5)
                                                                {
                                                                    indexX -= 1;
                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                    indexX -= 1;
                                                                }
                                                            }
                                                        }

                                                        ////Debug.Log("11indexX:" + indexX);
                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                        ////Debug.Log("0indexX:" + indexX);
                                                        if (indexX < 0)
                                                        {

                                                            indexX *= -1;
                                                            if (indexX >= 5)
                                                            {
                                                                ////Debug.Log("2indexX:" + indexX);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexX:" + indexX);
                                                            }
                                                            indexX = (chunkWidth - 1) - indexX;
                                                            ////Debug.Log("4indexX:" + indexX);

                                                        }
                                                        else
                                                        {
                                                            if (indexX == 0)
                                                            {
                                                                indexX = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexX:" + indexX);
                                                                if (indexX >= 5)
                                                                {
                                                                    indexX -= 1;
                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                    indexX -= 1;
                                                                }
                                                            }
                                                        }
                                                    }









                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    if (posz >= 1.0f)
                                                    {
                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                    }



                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                    }



                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }

                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                    }

                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));


                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                    if (addfracturedcubeonimpact == 1)
                                                    {
                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                        UnityTutorialPooledObject.SetActive(true);
                                                    }
                                                    Vector3 velocity = currentrigidbody.velocity;
                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                    {
                                                        //this.enabled = false;
                                                    }
                                                }
                                                else if (perp.x == 0 && perp.y == -1 && perp.z == 0) //BOTTOM FACE
                                                {
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    //Debug.Log("posz:" + posz);
                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.RoundToInt(((((posy - Mathf.Round(posy))) * chunkWidth)));
                                                        //Debug.Log("0indexY:" + indexY);
                                                        if (indexY < 0)
                                                        {
                                                            //indexY *= -1;
                                                            if (indexY > -5)
                                                            {
                                                                indexY *= -1;
                                                                indexY = (chunkWidth - 1) - indexY;
                                                                indexY += 1;
                                                                ////Debug.Log("2indexY:" + indexY);
                                                            }
                                                            else
                                                            {
                                                                indexY *= -1;
                                                                ////Debug.Log("3indexY:" + indexY);
                                                            }
                                                            //indexY = (chunkWidth - 1) - indexY;
                                                            ////Debug.Log("4indexY:" + indexY);

                                                        }
                                                        else
                                                        {
                                                            if (indexY == 0)
                                                            {
                                                                //indexY = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                //Debug.Log("1indexY:" + indexY);
                                                                if (indexY >= 5)
                                                                {
                                                                    //indexY -= 1;
                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                    //indexY -= 1;
                                                                }
                                                            }
                                                        }

                                                        ////Debug.Log("11indexY:" + indexY);
                                                    }
                                                    else
                                                    {
                                                        indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                        //Debug.Log("0indexY:" + indexY);
                                                        if (indexY < 0)
                                                        {

                                                            indexY *= -1;
                                                            if (indexY >= 5)
                                                            {
                                                                ////Debug.Log("2indexY:" + indexY);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexY:" + indexY);
                                                            }
                                                            indexY = (chunkWidth - 1) - indexY;
                                                            ////Debug.Log("4indexY:" + indexY);

                                                        }
                                                        else
                                                        {
                                                            if (indexY == 0)
                                                            {
                                                                //indexY = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                //indexY = chunkWidth - 1;
                                                                //Debug.Log("1indexY:" + indexY);
                                                                if (indexY >= 5)
                                                                {
                                                                    //indexY -= 1;
                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                    //indexY -= 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    if (posz >= 1.0f)
                                                    {
                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));

                                                    }

                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));

                                                    }
                                                    else
                                                    {
                                                        //indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                    }

                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }

                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                    }






                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));


                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                                    if (addfracturedcubeonimpact == 1)
                                                    {
                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                        UnityTutorialPooledObject.SetActive(true);
                                                    }
                                                    Vector3 velocity = currentrigidbody.velocity;
                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                    {
                                                        //this.enabled = false;
                                                    }
                                                }
                                                else if (perp.x == 0 && perp.y == 1 && perp.z == 0) //TOP FACE
                                                {
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    ////Debug.Log("posz:" + posz);
                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.RoundToInt(((((posy - Mathf.Round(posy))) * chunkWidth)));
                                                        ////Debug.Log("0indexY:" + indexY);
                                                        if (indexY < 0)
                                                        {
                                                            indexY *= -1;
                                                            if (indexY >= 5)
                                                            {
                                                                ////Debug.Log("2indexY:" + indexY);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexY:" + indexY);
                                                            }
                                                            indexY = (chunkWidth - 1) - indexY;
                                                            ////Debug.Log("4indexY:" + indexY);

                                                        }
                                                        else
                                                        {
                                                            if (indexY == 0)
                                                            {
                                                                indexY = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexY:" + indexY);
                                                                if (indexY >= 5)
                                                                {
                                                                    indexY -= 1;
                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                    indexY -= 1;
                                                                }
                                                            }
                                                        }

                                                        ////Debug.Log("11indexY:" + indexY);
                                                    }
                                                    else
                                                    {
                                                        indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                        ////Debug.Log("0indexY:" + indexY);
                                                        if (indexY < 0)
                                                        {

                                                            indexY *= -1;
                                                            if (indexY >= 5)
                                                            {
                                                                ////Debug.Log("2indexY:" + indexY);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexY:" + indexY);
                                                            }
                                                            indexY = (chunkWidth - 1) - indexY;
                                                            ////Debug.Log("4indexY:" + indexY);

                                                        }
                                                        else
                                                        {
                                                            if (indexY == 0)
                                                            {
                                                                indexY = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexY:" + indexY);
                                                                if (indexY >= 5)
                                                                {
                                                                    indexY -= 1;
                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                    indexY -= 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    if (posz >= 1.0f)
                                                    {
                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                    }

                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                    }

                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }

                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                    }





                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();
                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                                    if (addfracturedcubeonimpact == 1)
                                                    {
                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                        UnityTutorialPooledObject.SetActive(true);
                                                    }
                                                    Vector3 velocity = currentrigidbody.velocity;
                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                    {
                                                        //this.enabled = false;
                                                    }
                                                }
                                            }
                                            else
                                            {

                                            }

                                        }




                                    }


                                    /*if (somespherecasthitbool)
                                    {
                                        thepos = this.transform.position;

                                      
                                    }*/

                                    if (someraycasthitbool && !somespherecasthitbool || someraycasthitbool && somespherecasthitbool)//if (Physics.Raycast(ray, out hit, (raylength * 0.01f), layerMask) && !somespherecasthitbool)
                                    {
                                        //Debug.Log("test6");
                                        if (this.transform.name == "tip")//if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
                                        {
                                            if (hit.rigidbody != null)
                                            {
                                                Debug.Log("someraycasthitbool && !somespherecasthitbool");

                                                if (hit.transform.tag == "collisionObject")
                                                {

                                                    if (hit.rigidbody != null)
                                                    {

                                                        thepos = hit.point;
                                                    }
                                                    else
                                                    {
                                                        thepos = this.transform.position;
                                                    }

                                                    if (GetComponent<Fracture4>() != null)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        MeshCollider meshCollider = hit.collider as MeshCollider;
                                                        if (meshCollider != null || meshCollider.sharedMesh != null)
                                                        {
                                                            //Debug.Log("drawing mesh hit face triangles");
                                                            Mesh somemesh = meshCollider.sharedMesh;
                                                            Vector3[] vertices = somemesh.vertices;
                                                            int[] triangles = somemesh.triangles;

                                                            Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
                                                            Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
                                                            Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];

                                                            Transform hitTransform = hit.collider.transform;
                                                            p0 = hitTransform.TransformPoint(p0);
                                                            p1 = hitTransform.TransformPoint(p1);
                                                            p2 = hitTransform.TransformPoint(p2);

                                                            //Debug.DrawRay(p0, (p1 - p0).normalized, Color.red, 1000);
                                                            //Debug.DrawRay(p1, (p2 - p1).normalized, Color.red, 1000);
                                                            //Debug.DrawRay(p2, (p0 - p2).normalized, Color.red, 1000);

                                                            Debug.DrawLine(p0, (p1), Color.magenta, 1);
                                                            Debug.DrawLine(p1, (p2), Color.magenta, 1);
                                                            Debug.DrawLine(p2, (p0), Color.magenta, 1);

                                                            //somePointer0.transform.position = p0;
                                                            //somePointer1.transform.position = p1;
                                                            //somePointer2.transform.position = p2;

                                                            var perp = Vector3.Cross((p1 - p0).normalized, (p2 - p1).normalized);
                                                            perp.Normalize();
                                                            Debug.DrawLine(hitTransform.transform.position, (hitTransform.transform.position + perp), Color.magenta, 5);
                                                            var barycentriccoord = hit.barycentricCoordinate;



                                                            thepos = hit.point;// barycentriccoord;



                                                            //Debug.Log("test6");
                                                            var chunkX = (int)(Mathf.Round(thepos.x * tileSize) / tileSize);
                                                            var chunkY = (int)(Mathf.Round(thepos.y * tileSize) / tileSize);
                                                            var chunkZ = (int)(Mathf.Round(thepos.z * tileSize) / tileSize);

                                                            //////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                                                            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)thepos.x, (int)thepos.y, (int)thepos.z) != null)
                                                            {
                                                                //////Debug.Log("==count==");
                                                                mainChunkGen2 currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)thepos.x, (int)thepos.y, (int)thepos.z);

                                                                //////Debug.Log("x: " + hit.normal.x + " y: " + hit.normal.y + " z: " + hit.normal.z);
                                                                if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1) //BACK FACE
                                                                {
                                                                    Vector3 p = hit.point;


                                                                    float posx = (p.x);
                                                                    float posy = (p.y);
                                                                    float posz = (p.z);


                                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                    retAdd.position = chunkbytepos;

                                                                    int indexX = 0;
                                                                    int indexY = 0;
                                                                    int indexZ = 0;


                                                                    if (posx < 0)
                                                                    {
                                                                        posx *= -1;
                                                                    }

                                                                    if (posx >= 1.0f)
                                                                    {
                                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                                    }



                                                                    if (posy < 0)
                                                                    {
                                                                        posy *= -1;
                                                                    }

                                                                    if (posy >= 1.0f)
                                                                    {
                                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                                        indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                                    }


                                                                    if (posz < 0)
                                                                    {
                                                                        posz *= -1;
                                                                    }


                                                                    //Debug.Log("posz:" + posz);
                                                                    if (posz >= 1.0f)
                                                                    {

                                                                        indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                                                                    }

                                                                    if (indexX < 0)
                                                                    {
                                                                        indexX *= -1;
                                                                    }


                                                                    if (indexY < 0)
                                                                    {
                                                                        indexY *= -1;
                                                                    }

                                                                    if (indexZ < 0)
                                                                    {
                                                                        indexZ *= -1;
                                                                        indexZ = chunkWidth - indexZ;
                                                                    }


                                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                    ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));


                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();


                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);
                                                                    }

                                                                    Vector3 velocity = currentrigidbody.velocity;
                                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                                    {
                                                                        //this.enabled = false;
                                                                    }

                                                                }
                                                                else if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1) //FRONT FACE
                                                                {
                                                                    Vector3 p = hit.point;


                                                                    float posx = (p.x);
                                                                    float posy = (p.y);
                                                                    float posz = (p.z);


                                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                    retAdd.position = chunkbytepos;

                                                                    int indexX = 0;
                                                                    int indexY = 0;
                                                                    int indexZ = 0;


                                                                    if (posx < 0)
                                                                    {
                                                                        posx *= -1;
                                                                    }

                                                                    if (posx >= 1.0f)
                                                                    {
                                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                                    }



                                                                    if (posy < 0)
                                                                    {
                                                                        posy *= -1;
                                                                    }

                                                                    if (posy >= 1.0f)
                                                                    {
                                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                                    }


                                                                    if (posz < 0)
                                                                    {
                                                                        posz *= -1;
                                                                    }

                                                                    ////Debug.Log("posz:" + posz);
                                                                    if (posz >= 1.0f)
                                                                    {
                                                                        indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                                                                        ////Debug.Log("0indexZ:" + indexZ);
                                                                        if (indexZ < 0)
                                                                        {
                                                                            indexZ *= -1;
                                                                            if (indexZ >= 5)
                                                                            {
                                                                                ////Debug.Log("2indexZ:" + indexZ);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexZ:" + indexZ);
                                                                            }
                                                                            indexZ = (chunkWidth - 1) - indexZ;
                                                                            ////Debug.Log("4indexZ:" + indexZ);

                                                                        }
                                                                        else
                                                                        {
                                                                            if (indexZ == 0)
                                                                            {
                                                                                indexZ = chunkWidth - 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("1indexZ:" + indexZ);
                                                                                if (indexZ >= 5)
                                                                                {
                                                                                    indexZ -= 1;
                                                                                    ////Debug.Log("2indexZ:" + indexZ);
                                                                                }
                                                                                else
                                                                                {
                                                                                    ////Debug.Log("3indexZ:" + indexZ);
                                                                                    indexZ -= 1;
                                                                                }
                                                                            }
                                                                        }

                                                                        ////Debug.Log("11indexZ:" + indexZ);
                                                                    }
                                                                    else
                                                                    {
                                                                        indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                                                                        ////Debug.Log("0indexZ:" + indexZ);
                                                                        if (indexZ < 0)
                                                                        {

                                                                            indexZ *= -1;
                                                                            if (indexZ >= 5)
                                                                            {
                                                                                ////Debug.Log("2indexZ:" + indexZ);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexZ:" + indexZ);
                                                                            }
                                                                            indexZ = (chunkWidth - 1) - indexZ;
                                                                            ////Debug.Log("4indexZ:" + indexZ);

                                                                        }
                                                                        else
                                                                        {
                                                                            if (indexZ == 0)
                                                                            {
                                                                                indexZ = chunkWidth - 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("1indexZ:" + indexZ);
                                                                                if (indexZ >= 5)
                                                                                {
                                                                                    indexZ -= 1;
                                                                                    ////Debug.Log("2indexZ:" + indexZ);
                                                                                }
                                                                                else
                                                                                {
                                                                                    ////Debug.Log("3indexZ:" + indexZ);
                                                                                    indexZ -= 1;
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    if (indexX < 0)
                                                                    {
                                                                        indexX *= -1;
                                                                    }

                                                                    if (indexY < 0)
                                                                    {
                                                                        indexY *= -1;
                                                                    }

                                                                    if (indexZ < 0)
                                                                    {
                                                                        indexZ *= -1;
                                                                    }

                                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                    //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));


                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);
                                                                    }
                                                                    Vector3 velocity = currentrigidbody.velocity;
                                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                                    {
                                                                        //this.enabled = false;
                                                                    }
                                                                }
                                                                else if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0) //LEFT FACE
                                                                {
                                                                    /*Vector3 p = hit.point;// hit.point; // this.transform.TransformPoint(thepointinquestion);

                                                                    float posx = (p.x);
                                                                    float posy = (p.y);
                                                                    float posz = (p.z);


                                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                    retAdd.position = chunkbytepos;

                                                                    int indexX = 0;
                                                                    int indexY = 0;
                                                                    int indexZ = 0;


                                                                    if (posx < 0)
                                                                    {
                                                                        posx *= -1;
                                                                    }

                                                                    if (posx > 1.0f)
                                                                    {
                                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));

                                                                    }
                                                                    else
                                                                    {
                                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                                    }



                                                                    /*if (posy < 0)
                                                                    {
                                                                        posy *= -1;
                                                                    }

                                                                    if (posy > 1.0f)
                                                                    {
                                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexY = Mathf.FloorToInt((Mathf.Round(posy * chunkWidth))); //indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                    }




                                                                    /*if (posz < 0)
                                                                    {
                                                                        posz *= -1;
                                                                    }

                                                                    if (posz > 1.0f)
                                                                    {
                                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                                    }



                                                                    //indexZ = (chunkWidth - 1) - indexZ;


                                                                    if (indexX < 0)
                                                                    {
                                                                        indexX *= -1;
                                                                        //indexX = (chunkWidth - 1)- indexX;
                                                                    }


                                                                    if (indexY < 0)
                                                                    {
                                                                        indexY *= -1;
                                                                        //indexY = (chunkWidth - 1) - indexY;
                                                                    }

                                                                    if (indexZ < 0)
                                                                    {
                                                                        indexZ *= -1;
                                                                        //indexZ = (chunkWidth - 1) - indexZ;
                                                                    }*/
                                                                    Vector3 p = hit.point;


                                                                    float posx = (p.x);
                                                                    float posy = (p.y);
                                                                    float posz = (p.z);


                                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                    retAdd.position = chunkbytepos;

                                                                    int indexX = 0;
                                                                    int indexY = 0;
                                                                    int indexZ = 0;


                                                                    if (posx < 0)
                                                                    {
                                                                        posx *= -1;
                                                                    }

                                                                    ////Debug.Log("posz:" + posz);
                                                                    if (posx >= 1.0f)
                                                                    {
                                                                        indexX = Mathf.RoundToInt(((((posx - Mathf.Round(posx))) * chunkWidth)));


                                                                        //Debug.Log("0indexX:" + indexX);
                                                                        if (indexX < 0)
                                                                        {

                                                                            if (indexX >= -4)
                                                                            {
                                                                                //indexX += 1;
                                                                                indexX *= -1;
                                                                                indexX = (chunkWidth - 1) - indexX;
                                                                                indexX += 1;
                                                                                ////Debug.Log("2indexX:" + indexX);
                                                                            }
                                                                            else
                                                                            {
                                                                                //indexX += 1;
                                                                                //indexX -= 1;
                                                                                ////Debug.Log("3indexX:" + indexX);
                                                                            }
                                                                            //indexX = (chunkWidth - 1) - indexX;
                                                                            ////Debug.Log("4indexX:" + indexX);

                                                                        }
                                                                        else
                                                                        {
                                                                            if (indexX == 0)
                                                                            {
                                                                                //indexX = chunkWidth - 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                /*////Debug.Log("1indexX:" + indexX);
                                                                                if (indexX >= 5)
                                                                                {
                                                                                    indexX -= 1;
                                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                                }
                                                                                else
                                                                                {
                                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                                    indexX -= 1;
                                                                                }*/
                                                                            }
                                                                        }

                                                                        ////Debug.Log("11indexX:" + indexX);
                                                                    }
                                                                    else
                                                                    {
                                                                        indexX = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                                        //Debug.Log("0indexX:" + indexX);
                                                                        if (indexX < 0)
                                                                        {

                                                                            indexX *= -1;
                                                                            if (indexX >= 5)
                                                                            {
                                                                                ////Debug.Log("2indexX:" + indexX);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexX:" + indexX);
                                                                            }
                                                                            indexX = (chunkWidth - 1) - indexX;
                                                                            ////Debug.Log("4indexX:" + indexX);

                                                                        }
                                                                        else
                                                                        {
                                                                            if (indexX == 0)
                                                                            {
                                                                                //indexX = chunkWidth - 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("1indexX:" + indexX);
                                                                                /*if (indexX >= 5)
                                                                                {
                                                                                    indexX -= 1;
                                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                                }
                                                                                else
                                                                                {
                                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                                    indexX -= 1;
                                                                                }*/
                                                                            }
                                                                        }
                                                                    }


                                                                    if (posz < 0)
                                                                    {
                                                                        posz *= -1;
                                                                    }

                                                                    if (posz >= 1.0f)
                                                                    {
                                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                                    }



                                                                    if (posy < 0)
                                                                    {
                                                                        posy *= -1;
                                                                    }

                                                                    if (posy >= 1.0f)
                                                                    {
                                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                                        indexY = Mathf.FloorToInt((Mathf.Round(posy * chunkWidth)));
                                                                    }



                                                                    if (indexX < 0)
                                                                    {
                                                                        indexX *= -1;
                                                                    }

                                                                    if (indexY < 0)
                                                                    {
                                                                        indexY *= -1;
                                                                    }

                                                                    if (indexZ < 0)
                                                                    {
                                                                        indexZ *= -1;
                                                                    }


                                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                    //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));


                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);
                                                                    }
                                                                    Vector3 velocity = currentrigidbody.velocity;
                                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                                    {
                                                                        //this.enabled = false;
                                                                    }
                                                                }
                                                                else if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0) //RIGHT FACE
                                                                {
                                                                    Vector3 p = hit.point;


                                                                    float posx = (p.x);
                                                                    float posy = (p.y);
                                                                    float posz = (p.z);


                                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                    retAdd.position = chunkbytepos;

                                                                    int indexX = 0;
                                                                    int indexY = 0;
                                                                    int indexZ = 0;


                                                                    if (posx < 0)
                                                                    {
                                                                        posx *= -1;
                                                                    }

                                                                    ////Debug.Log("posz:" + posz);
                                                                    if (posx >= 1.0f)
                                                                    {
                                                                        indexX = Mathf.RoundToInt(((((posx - Mathf.Round(posx))) * chunkWidth)));
                                                                        ////Debug.Log("0indexX:" + indexX);
                                                                        if (indexX < 0)
                                                                        {
                                                                            indexX *= -1;
                                                                            if (indexX >= 5)
                                                                            {
                                                                                ////Debug.Log("2indexX:" + indexX);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexX:" + indexX);
                                                                            }
                                                                            indexX = (chunkWidth - 1) - indexX;
                                                                            ////Debug.Log("4indexX:" + indexX);

                                                                        }
                                                                        else
                                                                        {
                                                                            if (indexX == 0)
                                                                            {
                                                                                indexX = chunkWidth - 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("1indexX:" + indexX);
                                                                                if (indexX >= 5)
                                                                                {
                                                                                    indexX -= 1;
                                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                                }
                                                                                else
                                                                                {
                                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                                    indexX -= 1;
                                                                                }
                                                                            }
                                                                        }

                                                                        ////Debug.Log("11indexX:" + indexX);
                                                                    }
                                                                    else
                                                                    {
                                                                        indexX = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                                        ////Debug.Log("0indexX:" + indexX);
                                                                        if (indexX < 0)
                                                                        {

                                                                            indexX *= -1;
                                                                            if (indexX >= 5)
                                                                            {
                                                                                ////Debug.Log("2indexX:" + indexX);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexX:" + indexX);
                                                                            }
                                                                            indexX = (chunkWidth - 1) - indexX;
                                                                            ////Debug.Log("4indexX:" + indexX);

                                                                        }
                                                                        else
                                                                        {
                                                                            if (indexX == 0)
                                                                            {
                                                                                indexX = chunkWidth - 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("1indexX:" + indexX);
                                                                                if (indexX >= 5)
                                                                                {
                                                                                    indexX -= 1;
                                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                                }
                                                                                else
                                                                                {
                                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                                    indexX -= 1;
                                                                                }
                                                                            }
                                                                        }
                                                                    }









                                                                    if (posz < 0)
                                                                    {
                                                                        posz *= -1;
                                                                    }

                                                                    if (posz >= 1.0f)
                                                                    {
                                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                                    }



                                                                    if (posy < 0)
                                                                    {
                                                                        posy *= -1;
                                                                    }

                                                                    if (posy >= 1.0f)
                                                                    {
                                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                                    }



                                                                    if (indexX < 0)
                                                                    {
                                                                        indexX *= -1;
                                                                    }

                                                                    if (indexY < 0)
                                                                    {
                                                                        indexY *= -1;
                                                                    }

                                                                    if (indexZ < 0)
                                                                    {
                                                                        indexZ *= -1;
                                                                    }

                                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                    ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));


                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);
                                                                    }
                                                                    Vector3 velocity = currentrigidbody.velocity;
                                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                                    {
                                                                        //this.enabled = false;
                                                                    }
                                                                }
                                                                else if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0) //BOTTOM FACE
                                                                {
                                                                    Vector3 p = hit.point;


                                                                    float posx = (p.x);
                                                                    float posy = (p.y);
                                                                    float posz = (p.z);


                                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                    retAdd.position = chunkbytepos;

                                                                    int indexX = 0;
                                                                    int indexY = 0;
                                                                    int indexZ = 0;


                                                                    if (posy < 0)
                                                                    {
                                                                        posy *= -1;
                                                                    }

                                                                    //Debug.Log("posz:" + posz);
                                                                    if (posy >= 1.0f)
                                                                    {
                                                                        indexY = Mathf.RoundToInt(((((posy - Mathf.Round(posy))) * chunkWidth)));
                                                                        //Debug.Log("0indexY:" + indexY);
                                                                        if (indexY < 0)
                                                                        {
                                                                            //indexY *= -1;
                                                                            if (indexY > -5)
                                                                            {
                                                                                indexY *= -1;
                                                                                indexY = (chunkWidth - 1) - indexY;
                                                                                indexY += 1;
                                                                                ////Debug.Log("2indexY:" + indexY);
                                                                            }
                                                                            else
                                                                            {
                                                                                indexY *= -1;
                                                                                ////Debug.Log("3indexY:" + indexY);
                                                                            }
                                                                            //indexY = (chunkWidth - 1) - indexY;
                                                                            ////Debug.Log("4indexY:" + indexY);

                                                                        }
                                                                        else
                                                                        {
                                                                            if (indexY == 0)
                                                                            {
                                                                                //indexY = chunkWidth - 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                //Debug.Log("1indexY:" + indexY);
                                                                                if (indexY >= 5)
                                                                                {
                                                                                    //indexY -= 1;
                                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                                }
                                                                                else
                                                                                {
                                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                                    //indexY -= 1;
                                                                                }
                                                                            }
                                                                        }

                                                                        ////Debug.Log("11indexY:" + indexY);
                                                                    }
                                                                    else
                                                                    {
                                                                        indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                                        //Debug.Log("0indexY:" + indexY);
                                                                        if (indexY < 0)
                                                                        {

                                                                            indexY *= -1;
                                                                            if (indexY >= 5)
                                                                            {
                                                                                ////Debug.Log("2indexY:" + indexY);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexY:" + indexY);
                                                                            }
                                                                            indexY = (chunkWidth - 1) - indexY;
                                                                            ////Debug.Log("4indexY:" + indexY);

                                                                        }
                                                                        else
                                                                        {
                                                                            if (indexY == 0)
                                                                            {
                                                                                //indexY = chunkWidth - 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                //indexY = chunkWidth - 1;
                                                                                //Debug.Log("1indexY:" + indexY);
                                                                                if (indexY >= 5)
                                                                                {
                                                                                    //indexY -= 1;
                                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                                }
                                                                                else
                                                                                {
                                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                                    //indexY -= 1;
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    if (posz < 0)
                                                                    {
                                                                        posz *= -1;
                                                                    }

                                                                    if (posz >= 1.0f)
                                                                    {
                                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));

                                                                    }

                                                                    if (posx < 0)
                                                                    {
                                                                        posx *= -1;
                                                                    }

                                                                    if (posx >= 1.0f)
                                                                    {
                                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));

                                                                    }
                                                                    else
                                                                    {
                                                                        //indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                                    }

                                                                    if (indexX < 0)
                                                                    {
                                                                        indexX *= -1;
                                                                    }

                                                                    if (indexY < 0)
                                                                    {
                                                                        indexY *= -1;
                                                                    }

                                                                    if (indexZ < 0)
                                                                    {
                                                                        indexZ *= -1;
                                                                    }






                                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                    //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));


                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);
                                                                    }
                                                                    Vector3 velocity = currentrigidbody.velocity;
                                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                                    {
                                                                        //this.enabled = false;
                                                                    }
                                                                }
                                                                else if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0) //TOP FACE
                                                                {
                                                                    Vector3 p = hit.point;


                                                                    float posx = (p.x);
                                                                    float posy = (p.y);
                                                                    float posz = (p.z);


                                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                    retAdd.position = chunkbytepos;

                                                                    int indexX = 0;
                                                                    int indexY = 0;
                                                                    int indexZ = 0;


                                                                    if (posy < 0)
                                                                    {
                                                                        posy *= -1;
                                                                    }

                                                                    ////Debug.Log("posz:" + posz);
                                                                    if (posy >= 1.0f)
                                                                    {
                                                                        indexY = Mathf.RoundToInt(((((posy - Mathf.Round(posy))) * chunkWidth)));
                                                                        ////Debug.Log("0indexY:" + indexY);
                                                                        if (indexY < 0)
                                                                        {
                                                                            indexY *= -1;
                                                                            if (indexY >= 5)
                                                                            {
                                                                                ////Debug.Log("2indexY:" + indexY);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexY:" + indexY);
                                                                            }
                                                                            indexY = (chunkWidth - 1) - indexY;
                                                                            ////Debug.Log("4indexY:" + indexY);

                                                                        }
                                                                        else
                                                                        {
                                                                            if (indexY == 0)
                                                                            {
                                                                                indexY = chunkWidth - 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("1indexY:" + indexY);
                                                                                if (indexY >= 5)
                                                                                {
                                                                                    indexY -= 1;
                                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                                }
                                                                                else
                                                                                {
                                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                                    indexY -= 1;
                                                                                }
                                                                            }
                                                                        }

                                                                        ////Debug.Log("11indexY:" + indexY);
                                                                    }
                                                                    else
                                                                    {
                                                                        indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                                        ////Debug.Log("0indexY:" + indexY);
                                                                        if (indexY < 0)
                                                                        {

                                                                            indexY *= -1;
                                                                            if (indexY >= 5)
                                                                            {
                                                                                ////Debug.Log("2indexY:" + indexY);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexY:" + indexY);
                                                                            }
                                                                            indexY = (chunkWidth - 1) - indexY;
                                                                            ////Debug.Log("4indexY:" + indexY);

                                                                        }
                                                                        else
                                                                        {
                                                                            if (indexY == 0)
                                                                            {
                                                                                indexY = chunkWidth - 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("1indexY:" + indexY);
                                                                                if (indexY >= 5)
                                                                                {
                                                                                    indexY -= 1;
                                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                                }
                                                                                else
                                                                                {
                                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                                    indexY -= 1;
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    if (posz < 0)
                                                                    {
                                                                        posz *= -1;
                                                                    }

                                                                    if (posz >= 1.0f)
                                                                    {
                                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                                    }

                                                                    if (posx < 0)
                                                                    {
                                                                        posx *= -1;
                                                                    }

                                                                    if (posx >= 1.0f)
                                                                    {
                                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                                    }
                                                                    else
                                                                    {
                                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                                    }

                                                                    if (indexX < 0)
                                                                    {
                                                                        indexX *= -1;
                                                                    }

                                                                    if (indexY < 0)
                                                                    {
                                                                        indexY *= -1;
                                                                    }

                                                                    if (indexZ < 0)
                                                                    {
                                                                        indexZ *= -1;
                                                                    }





                                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                    ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();
                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);
                                                                    }
                                                                    Vector3 velocity = currentrigidbody.velocity;
                                                                    Vector3 angularVelocityShadow = currentrigidbody.angularVelocity;

                                                                    if (velocity.magnitude < velocitymax && angularVelocityShadow.magnitude < angularvelocitymax)
                                                                    {
                                                                        //this.enabled = false;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    somespherecasthitbool = false;
                                    someraycasthitbool = false;
                                    raylength++;
                                }
                                tippickaxestopwatch.Restart();
                            }
                        }
                        else
                        {
                            raylength = 0;
                            tippickaxestopwatch.Restart();

                        }

                        /*else
                        {
                            if (tippickaxestopwatch.Elapsed.Ticks >= 50)
                            {
                                for (int r = 0; r < 1; r++)
                                {
                                    var ray = new Ray(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f);
                                    Debug.DrawRay(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f, Color.white, 0.001f);
                                }
                                tippickaxestopwatch.Restart();
                                raylength = 0;
                            }
                        }*/

                        /*
                        if (raylength <= raycounterLoopMax)
                        {
                            for (int r = 0; r < 1; r++)
                            {
                                var ray = new Ray(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f);
                                Debug.DrawRay(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f, Color.white, 0.001f);
                            }
                  
                        }
                        else
                        {
                            for (int r = 0; r < 1; r++)
                            {
                                var ray = new Ray(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f);
                                Debug.DrawRay(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f, Color.white, 0.001f);
                            }
                            raylength = 0;
                        }
                        raylength++;*/
                    }

                }
                counterForByteChange = 0;
            }
            counterForByteChange++;
        }
    }

    int counterForIkFootPlacement = 0;
    int counterForIkFootPlacementMax = 100;
    int counterForIkFootPlacementSwtc = 0;




    /*||
   hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0 ||
   hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0 ||
   hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1 ||
   hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0 ||
   hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0*/
    public void setAdjacentChunks(mainChunkGen2 currentChunk, Vector3 pos, int indexX, int indexY, int indexZ)
    {
        int width = currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().width;
        int height = currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().height;
        int depth = currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().depth;

        //////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

        if (indexX == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x - 4, (int)pos.y, (int)pos.z) != null)
            {
                mainChunkGen2 adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x - 4, (int)pos.y, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().GetByte((int)width - 1, (int)indexY, (int)indexZ) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)width - 1, (int)indexY, (int)indexZ, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();
                }
            }
        }

        if (indexX == width - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x + 4, (int)pos.y, (int)pos.z) != null)
            {
                mainChunkGen2 adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x + 4, (int)pos.y, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().GetByte((int)0, (int)indexY, (int)indexZ) == 1)
                {
                    //////Debug.Log("adjacent chunk right exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)0, (int)indexY, (int)indexZ, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();
                }
            }
        }

        if (indexY == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x, (int)pos.y - 4, (int)pos.z) != null)
            {
                mainChunkGen2 adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x, (int)pos.y - 4, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().GetByte((int)indexX, (int)height - 1, (int)indexZ) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)height - 1, (int)indexZ, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();
                }
            }
        }

        if (indexY == height - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x, (int)pos.y + 4, (int)pos.z) != null)
            {
                mainChunkGen2 adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x, (int)pos.y + 4, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().GetByte((int)indexX, (int)0, (int)indexZ) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)0, (int)indexZ, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();
                }
            }
        }

        if (indexZ == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x, (int)pos.y, (int)pos.z - 4) != null)
            {
                mainChunkGen2 adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x, (int)pos.y, (int)pos.z - 4);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().GetByte((int)indexX, (int)indexY, (int)depth - 1) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)depth - 1, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();
                }
            }
        }

        if (indexZ == depth - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x, (int)pos.y, (int)pos.z + 4) != null)
            {
                mainChunkGen2 adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)pos.x, (int)pos.y, (int)pos.z + 4);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().GetByte((int)indexX, (int)indexY, (int)0) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)0, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();
                }
            }
        }
    }












    private void OnTriggerStayNot(Collider other)
    {

        //Debug.Log("object hit OnTriggerEnter:" + other.transform.name);

        if (ontriggerstaycounter >= ontriggerstaycounterMax)
        {


            if (GetComponent<Fracture4>() != null)
            {

            }
            else
            {
                var closestPointOnBounds = other.ClosestPointOnBounds(transform.position);

                //retAdd.transform.position = closestPointOnBounds;


                float posx = (closestPointOnBounds.x);
                float posy = (closestPointOnBounds.y);
                float posz = (closestPointOnBounds.z);


                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                //retAdd.position = chunkbytepos;

                int indexX = 0;
                int indexY = 0;
                int indexZ = 0;


                if (posx < 0)
                {
                    posx *= -1;
                }

                if (posx >= 1.0f)
                {
                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                }
                else
                {
                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                }



                if (posy < 0)
                {
                    posy *= -1;
                }

                if (posy >= 1.0f)
                {
                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                }
                else
                {
                    //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                    indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                }


                if (posz < 0)
                {
                    posz *= -1;
                }


                //Debug.Log("posz:" + posz);
                if (posz >= 1.0f)
                {

                    indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                }
                else
                {
                    indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                }

                if (indexX < 0)
                {
                    indexX *= -1;
                }


                if (indexY < 0)
                {
                    indexY *= -1;
                }

                if (indexZ < 0)
                {
                    indexZ *= -1;
                    indexZ = chunkWidth - indexZ;
                }


                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                mainChunkGen2 currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)other.transform.position.x, (int)other.transform.position.y, (int)other.transform.position.z);

                currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

            }
            ontriggerstaycounter = 0;
        }
        ontriggerstaycounter++;
    }

    private void OnTriggerExitNot(Collider other)
    {

        //Debug.Log("object hit OnTriggerEnter:" + other.transform.name);

        if (ontriggerexitcounter >= ontriggerexitcounterMax)
        {


            if (GetComponent<Fracture4>() != null)
            {

            }
            else
            {
                var closestPointOnBounds = other.ClosestPointOnBounds(transform.position);

                retAdd.transform.position = closestPointOnBounds;


                float posx = (closestPointOnBounds.x);
                float posy = (closestPointOnBounds.y);
                float posz = (closestPointOnBounds.z);


                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                retAdd.position = chunkbytepos;

                int indexX = 0;
                int indexY = 0;
                int indexZ = 0;


                if (posx < 0)
                {
                    posx *= -1;
                }

                if (posx >= 1.0f)
                {
                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                }
                else
                {
                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                }



                if (posy < 0)
                {
                    posy *= -1;
                }

                if (posy >= 1.0f)
                {
                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                }
                else
                {
                    //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                    indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                }


                if (posz < 0)
                {
                    posz *= -1;
                }


                //Debug.Log("posz:" + posz);
                if (posz >= 1.0f)
                {

                    indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                }
                else
                {
                    indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                }

                if (indexX < 0)
                {
                    indexX *= -1;
                }


                if (indexY < 0)
                {
                    indexY *= -1;
                }

                if (indexZ < 0)
                {
                    indexZ *= -1;
                    indexZ = chunkWidth - indexZ;
                }


                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                mainChunkGen2 currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)other.transform.position.x, (int)other.transform.position.y, (int)other.transform.position.z);

                currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                currentChunk.planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

            }
            ontriggerexitcounter = 0;
        }
        ontriggerexitcounter++;
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
        if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)hit.transform.position.x, (int)hit.transform.position.y,(int)hit.transform.position.z) != null)
        {
            var chunkX = (int)(Mathf.Round(hit.point.x * planeSize) / planeSize);
            var chunkY = (int)(Mathf.Round(hit.point.y * planeSize) / planeSize);
            var chunkZ = (int)(Mathf.Round(hit.point.z * planeSize) / planeSize);

            ////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);
            retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
            //yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * -2);
            //retDel.position = p;
        }
    }
}*/



/*Vector3 p = hit.point - hit.normal / 4;
float offset = planeSize / 2;
//float offset = 0;


float offset2 = planeSize / 2;

if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0)
{
    //////Debug.Log("yo0");

    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset * multiplicator, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    //retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Round(p.y * suppressorPos) / suppressorPos) + offset * multiplicator, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset * multiplicatorReticle, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Round(p.y * suppressorPos) / suppressorPos) + offset * 5, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);
}

if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1)
{
    //////Debug.Log("yo1");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 5);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
}

if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0)
{
    //////Debug.Log("yo2");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
}


if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0)
{
    //////Debug.Log("yo2");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
}

if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1)
{
    //////Debug.Log("yo1");

    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 3);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 6);

}


if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0)
{
    //////Debug.Log("yo1");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 3, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

}



var chunkX = (int)(Mathf.Round(hit.transform.position.x * tileSize) / tileSize);
var chunkY = (int)(Mathf.Round(hit.transform.position.y * tileSize) / tileSize);
var chunkZ = (int)(Mathf.Round(hit.transform.position.z * tileSize) / tileSize);

//////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
{

    //////Debug.Log("==count==");
    mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunkmainChunkGen2((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z);

    if (Input.GetMouseButtonDown(0))
    {
        int x = (int)(((yoMan1.x * 1) / 1) / 1); //WORKING
        int y = (int)(((yoMan1.y * 1) / 1) / 1);//WORKING
        int z = (int)(((yoMan1.z * 1) / 1) / 1);//WORKING
                                           //terrain1.getChunkmainChunkGen2(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);

        var planetchunk = hit.transform;
        planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)x, (int)y, (int)z, activeBlockType);

        planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
        planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

        setAdjacentChunks(currentChunk, hit, x, y, z);
    }


    if (Input.GetMouseButtonDown(1))
    {
        //////Debug.Log(hit.normal);
        //Debug.DrawRay(hit.point, Vector3.up * 10, Color.red, 0.1f);

        int x = (int)(((yoMan.x * 1) / 1) / 1); //WORKING
        int y = (int)(((yoMan.y * 1) / 1) / 1);//WORKING
        int z = (int)(((yoMan.z * 1) / 1) / 1);//WORKING

        //terrain1.getChunkmainChunkGen2(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
        var planetchunk = hit.transform;
        planetchunk.GetComponent<sccsplanetchunkGen2>().SetByte((int)x, (int)y, (int)z, activeBlockType);

        planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
        planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();

        setAdjacentChunks(currentChunk, hit, x, y, z);
    }

}*/













