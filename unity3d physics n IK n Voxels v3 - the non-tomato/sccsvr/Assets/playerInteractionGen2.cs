using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class playerInteractionGen2 : MonoBehaviour
{
    int addfracturedcubeonimpact = 0;

    Vector3 lastFrameRayInitDirForward = Vector3.zero;
    int someswtc = 0;
    Ray someray;
    Vector3 lastFrameRayPos = Vector3.zero;
    Vector3 lastFrameRayDirForward = Vector3.zero;
    Vector3 lastFrameRayInitDirUp = Vector3.zero;
    int lastFrameRayPosSwtc = 0;
    Vector3 currentRayPosition = Vector3.zero;
    Vector3 gunbarrelbulletinitlocation = Vector3.zero;

    int currentFrameRayPosSwtc = 0;

    sccsInstancesunitypool.instancedata instancedata;
    public GameObject somePointer0;
    GameObject somePointer1;
    GameObject somePointer2;
    RaycastHit hit;

    public Transform pickaxetiptransform;

    public float raycounterLoopMax = 20;

    int InitcounterForIkFootPlacement = 0;
    public int InitcounterForIkFootPlacementMax = 10;
    int InitcounterForIkFootPlacementSwtc = 0;
    float raylength = 0;
    float raycounterSwtc = 0;

    int counterForByteChangeMax = 1;

    public Transform upperleg;
    public Transform lowerleg;
    public Transform foot;
    //public Transform footTarget;
    public Transform legstaticpivot;

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
    public Transform footTarget;


    public Transform planetmanager;

    public byte activeBlockType = 0;
    public Transform retAdd, retDel;

    Mesh mesh;

    public Transform rayvisual;
    public Transform someblock;
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
    int realplanetwidth = 4;
    int suppressorPos = 4;*/

    float planeSize = 0.1f;
    int multiplicator = 1;
    int multiplicatorReticle = 3;
    float realplanetwidth = 1;
    int suppressorPos = 1;
    public int chunkWidth = 20;
    int counterForByteChange = 0;

    Stopwatch stopwatch = new Stopwatch();

    public Material hitmaterial;

    public sccsproceduralplanetbuilderGen2 mainscriptplanetgen;


    void Start()
    {
    

        somePointer1 = Instantiate(somePointer0, this.transform.position, Quaternion.identity);
        somePointer2 = Instantiate(somePointer0, this.transform.position, Quaternion.identity);

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
        stopwatch.Start();
        tippickaxestopwatch.Start();



        lastFrameRayInitDirUp = pickaxetiptransform.transform.up;
        lastFrameRayDirForward = pickaxetiptransform.transform.forward;
        lastFrameRayPos = pickaxetiptransform.position;
        //currentRayPosition = footTarget.transform.position;

        currentRayPosition = rayvisual.position;
        //gunbarrelbulletinitlocation = rayvisual.position;
        gunbarrelbulletinitlocation = foot.position;


        realplanetwidth = mainscriptplanetgen.realplanetwidth;// Mathf.FloorToInt(planeSize * mainscriptplanetgen.width);
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



    Ray ray;
    Vector3 positionThisObject;
    Vector3 directionForwardOfThisObject;

    Stopwatch tippickaxestopwatch = new Stopwatch();

    void Update()
    {

        gunbarrelbulletinitlocation = foot.position;
        /*
        bool thumbstickleft = OVRInput.Get(OVRInput.Touch.SecondaryThumbstick, OVRInput.Controller.LTouch);
        bool thumbstickright = OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight, OVRInput.Controller.LTouch);
        bool thumbstickdown = OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown, OVRInput.Controller.LTouch);
        bool thumbstickup = OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp, OVRInput.Controller.LTouch);*/

        if (InitcounterForIkFootPlacementSwtc == 0)
        {
            if (InitcounterForIkFootPlacement >= InitcounterForIkFootPlacementMax)
            {
                Debug.Log("***INIT COUNTER REACHED. can start ray***");
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

        }
        else if (swtcForTypeOfInteract == 1)
        {
            //Debug.Log("taskcancelFlagTwo == 2");
            //playerInteraction(somemsg[0]);

            //blockers[indexcreateface].somesccsplanetchunkFinal.sccsCustomStart(this);

            if (swtcForTypeOfInteract == 1)
            {
                int somemaxframecounterforprojectile = 1500;
                int projectilespeed = 1;
                raycounterLoopMax = somemaxframecounterforprojectile; //750

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

                if (buttonPressedLeft || buttonPressedRight)
                {

                }
                else
                {
                    //gottarget = 0;
                    raylength = 0;
                    someswtc = 0;
                    lastFrameRayPosSwtc = 0;
                    currentFrameRayPosSwtc = 0;
                    tippickaxestopwatch.Restart();
                }

                //if (counterForByteChange == 0)// counterForByteChangeMax)
                {
                    if (InitcounterForIkFootPlacementSwtc == 1)
                    {
                        if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
                        {
                            if (raycounterSwtc == 0 || raycounterSwtc == 1)
                            {
                                //if (tippickaxestopwatch.Elapsed.Ticks >= 1)
                                {
                                    for (int r = 0; r < projectilespeed; r++)
                                    {
                                        //loopagain:

                                        if (currentFrameRayPosSwtc == 0)
                                        {
                                            currentRayPosition = gunbarrelbulletinitlocation;// foot.transform.position;// somemsg[0].foottransformposition;
                                            //lastFrameRayDirForward = pickaxetiptransform.transform.forward;
                                            currentFrameRayPosSwtc = 1;
                                        }

                                        if (raylength < raycounterLoopMax)
                                        {

                                            Vector3 rayFrameInitPos0 = currentRayPosition + (footTarget.forward * (raylength * someRayLength));
                                            
                                            var fractionOf = realplanetwidth / planeSize;

                                            int neighboorindexx = Mathf.FloorToInt((rayFrameInitPos0.x / planeSize) / fractionOf); //4.654321/0.2 = 23.271605 => 23.271605/fractionOf = floor(2.3f)
                                            int neighboorindexy = Mathf.FloorToInt((rayFrameInitPos0.y / planeSize) / fractionOf);
                                            int neighboorindexz = Mathf.FloorToInt((rayFrameInitPos0.z / planeSize) / fractionOf);

                                            var tempx = neighboorindexx;
                                            var tempy = neighboorindexy;
                                            var tempz = neighboorindexz;
                                           
                                            someblock.transform.position = new Vector3(tempx, tempy, tempz);

                                            if (mainscriptplanetgen.getChunk(neighboorindexx, neighboorindexy, neighboorindexz) != null)
                                            {
                                                sccsChunk somechunk = (sccsChunk)mainscriptplanetgen.getChunk(neighboorindexx, neighboorindexy, neighboorindexz);
                                                if (somechunk.planetchunk != null)
                                                {
                                                    somechunk.planetchunk.GetComponent<MeshRenderer>().material = hitmaterial;
                                                }
                                            }

                                            if (lastFrameRayPosSwtc == 0)
                                            {
                                                lastFrameRayInitDirUp = footTarget.up;
                                                lastFrameRayInitDirForward = pickaxetiptransform.forward;// _main_received_messages[0].pickaxetiptransformforward;
                                                lastFrameRayPosSwtc = 1;
                                            }

                                            rayvisual.transform.position = new Vector3(rayFrameInitPos0.x, rayFrameInitPos0.y, rayFrameInitPos0.z);
                                            rayvisual.transform.rotation = Quaternion.LookRotation(lastFrameRayInitDirForward, lastFrameRayInitDirUp);

                                            //Debug.Log("projectile is supposed to be moving forward");

                                            //Debug.Log("nx:" + neighboorindexx + "/ny: " + neighboorindexy + "/nz:" + neighboorindexz);

                                            int gottarget = 0;

                                            if (gottarget == 0)
                                            {
                                                Vector3 chunkbytepos = new Vector3(neighboorindexx, neighboorindexy, neighboorindexz);

                                                //int indexmain = neighboorindexx + (mainscriptplanetgen.ChunkWidth_L + mainscriptplanetgen.ChunkWidth_R + 1) * (neighboorindexy + (mainscriptplanetgen.ChunkHeight_L + mainscriptplanetgen.ChunkHeight_R + 1) * neighboorindexz);

                                                if (mainscriptplanetgen.getChunk(neighboorindexx, neighboorindexy, neighboorindexz) != null)
                                                {
                                                    sccsChunk somechunk = (sccsChunk)mainscriptplanetgen.getChunk(neighboorindexx, neighboorindexy, neighboorindexz);
                                                    /*if (somechunk.planetchunk != null)
                                                    {
                                                        somechunk.planetchunk.GetComponent<MeshRenderer>().material = hitmaterial;
                                                    }*/


                                                    if (somechunk.map != null)
                                                    {
                                                        //Debug.Log("found neighbour chunk");
                                                        //mainChunkFinal currentChunk = somemsg[0].somechunkarray[index];// this.getChunk(neighboorindexx, neighboorindexy, neighboorindexz);// posnotroundedx, posnotroundedy, posnotroundedz);
                                                        int indexx = 0;
                                                        int indexy = 0;
                                                        int indexz = 0;

                                                        //indexx = Mathf.FloorToInt((rayFrameInitPos0.x - Mathf.FloorToInt(rayFrameInitPos0.x)) * planeSize * mainscriptplanetgen.width); 
                                                        //indexy = Mathf.FloorToInt((rayFrameInitPos0.y - Mathf.FloorToInt(rayFrameInitPos0.y)) * planeSize * mainscriptplanetgen.width);                                                                                                    
                                                        //indexz = Mathf.FloorToInt((rayFrameInitPos0.z - Mathf.FloorToInt(rayFrameInitPos0.z)) * planeSize * mainscriptplanetgen.width);
                                                        float someremainsx = 0;
                                                        float someremainsy = 0;
                                                        float someremainsz = 0;

                                                        if (rayFrameInitPos0.x < 0)
                                                        {
                                                            someremainsx = Mathf.Floor(rayFrameInitPos0.x) - (Mathf.Floor(rayFrameInitPos0.x * 10) / 10);
                                                        }
                                                        else
                                                        {
                                                            someremainsx = (Mathf.Floor(rayFrameInitPos0.x * 10) / 10) - Mathf.Floor(rayFrameInitPos0.x);
                                                        }


                                                        if (rayFrameInitPos0.y < 0)
                                                        {
                                                            someremainsy = Mathf.Floor(rayFrameInitPos0.y) - (Mathf.Floor(rayFrameInitPos0.y * 10) / 10);
                                                        }
                                                        else
                                                        {
                                                            someremainsy = (Mathf.Floor(rayFrameInitPos0.y * 10) / 10) - Mathf.Floor(rayFrameInitPos0.y);
                                                        }


                                                        if (rayFrameInitPos0.z < 0)
                                                        {
                                                            someremainsz = Mathf.Floor(rayFrameInitPos0.z) - (Mathf.Floor(rayFrameInitPos0.z * 10) / 10); //
                                                        }
                                                        else
                                                        {
                                                            someremainsz = (Mathf.Floor(rayFrameInitPos0.z * 10) / 10) - Mathf.Floor(rayFrameInitPos0.z);
                                                        }


                                                        indexx = Mathf.FloorToInt(someremainsx * 10);
                                                        indexy = Mathf.FloorToInt(someremainsy * 10);
                                                        indexz = Mathf.FloorToInt(someremainsz * 10);

                                                        //Debug.Log("x:" + indexx + "/y:" + indexy + "/z:" + indexz);
                                                        if (indexx < 0)
                                                        {
                                                            indexx *= -1;
                                                        }

                                                        if (indexy < 0)
                                                        {
                                                            indexy *= -1;
                                                        }

                                                        if (indexz < 0)
                                                        {
                                                            indexz *= -1;
                                                        }
                                                        
                                                        int indexOfChunkMapByte = indexx + mainscriptplanetgen.width * (indexy + mainscriptplanetgen.height * indexz);

                                                        if (somechunk.map != null)
                                                        {

                                                            //Debug.Log(indexOfChunkMapByte);


                                                            if (indexOfChunkMapByte >= 0 && indexOfChunkMapByte < mainscriptplanetgen.width * mainscriptplanetgen.height * mainscriptplanetgen.depth)//((int)indexx < 0) || ((int)indexy < 0) || ((int)indexz < 0) || ((int)indexy >= width) || ((int)indexx >= height) || ((int)indexz >= depth))
                                                            {
                                                                if (somechunk.map[indexOfChunkMapByte] == 1)//currentChunk.somesccsplanetchunkFinal.GetByte((int)indexx, (int)indexy, (int)indexz) == 1)
                                                                {
                                                                    somechunk.SetByte(indexx, indexy, indexz, activeBlockType, chunkbytepos);
                                                                   

                                                                    //Debug.Log("nx:" + neighboorindexx + "/ny:" + neighboorindexy + "/nz:" + neighboorindexz);

                                                                    if (somechunk.vertexlist != null)
                                                                    {

                                                                        if (somechunk.vertexlist.Count > 0)
                                                                        {
                                                                            //currentChunk.somesccsplanetchunkFinal.buildMesh();

                                                                            //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexy, indexz);
                                                                            /*if (addfracturedcubeonimpact == 1)
                                                                            {
                                                                                instancedata = new sccsInstancesunitypool.instancedata();
                                                                                chunkbytepos.x += planeSizetask * 0.5f;
                                                                                chunkbytepos.y += planeSizetask * 0.5f;
                                                                                chunkbytepos.z += planeSizetask * 0.5f;
                                                                                instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                                                instancedata.currentInstanceGameObject = null;
                                                                                instancedata.instanceindex = 0;
                                                                                instancedata.enabled = -1;
                                                                                instancedata.swap = -1;
                                                                                instancedata.instanceenabledcounter = 0;
                                                                                instancedata.instanceenabledcounterSwtc = -1;
                                                                                instancedata.instanceenabledcounterMax = 1;
                                                                                sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                                            }*/
                                                                            gottarget = 1;
                                                                            someswtc++;
                                                                        }
                                                                        else
                                                                        {
                                                                            int maxraylengthinverysmallunitswhenrayhitschunk = somemaxframecounterforprojectile;
                                                                            if (someswtc < maxraylengthinverysmallunitswhenrayhitschunk)
                                                                            {
                                                                                /*if (someswtc == 0)
                                                                                {
                                                                                    for (int rever = 0; rever < 25; rever++)
                                                                                    {
                                                                                        currentRayPosition += (-footTarget.forward * (raylength * someRayLength));
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    //currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                                                                }
                                                                                //currentRayPosition += (footTarget.forward * (raylength * someRayLength));

                                                                                //Debug.Log("byte == 0");
                                                                                //currentFrameRayPosSwtc = 0;

                                                                                //lastFrameRayPosSwtc = 1;
                                                                                //Debug.Log("someswtc:" + someswtc);
                                                                                //tippickaxestopwatch.Restart();



                                                                                //currentRayPosition += (somemsg[0].footTargetforward * (raylength * someRayLength));
                                                                                //lastFrameRayPos = currentRayPosition;*/
                                                                                gottarget = 0;
                                                                                someswtc++;
                                                                                tippickaxestopwatch.Restart();


                                                                                //goto loopagain;
                                                                            }
                                                                            else
                                                                            {
                                                                                gottarget = 0;
                                                                                raylength = 0;
                                                                                someswtc = 0;
                                                                                tippickaxestopwatch.Restart();

                                                                                lastFrameRayPosSwtc = 0;
                                                                                currentFrameRayPosSwtc = 0;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        gottarget = 0;
                                                                        someswtc++;
                                                                        tippickaxestopwatch.Restart();
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    gottarget = 0;
                                                                    someswtc++;
                                                                    tippickaxestopwatch.Restart();
                                                                }

                                                            }
                                                            else
                                                            {
                                                                int maxraylengthinverysmallunitswhenrayhitschunk = somemaxframecounterforprojectile;
                                                                if (someswtc < maxraylengthinverysmallunitswhenrayhitschunk)
                                                                {
                                                                    /*if (someswtc == 0)
                                                                    {
                                                                        for (int rever = 0; rever < 25; rever++)
                                                                        {
                                                                            currentRayPosition += (-footTarget.forward * (raylength * someRayLength));
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                                                    }*/
                                                                    //currentRayPosition += (footTarget.forward * (raylength * someRayLength));

                                                                    //Debug.Log("byte == 0");
                                                                    //currentFrameRayPosSwtc = 0;

                                                                    //lastFrameRayPosSwtc = 1;
                                                                    //Debug.Log("someswtc:" + someswtc);
                                                                    //tippickaxestopwatch.Restart();



                                                                    //currentRayPosition += (somemsg[0].footTargetforward * (raylength * someRayLength));
                                                                    //lastFrameRayPos = currentRayPosition;
                                                                    gottarget = 0;
                                                                    someswtc++;
                                                                    tippickaxestopwatch.Restart();


                                                                    //goto loopagain;
                                                                }
                                                                else
                                                                {
                                                                    gottarget = 0;
                                                                    raylength = 0;
                                                                    someswtc = 0;
                                                                    tippickaxestopwatch.Restart();

                                                                    lastFrameRayPosSwtc = 0;
                                                                    currentFrameRayPosSwtc = 0;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            int maxraylengthinverysmallunitswhenrayhitschunk = somemaxframecounterforprojectile;
                                                            if (someswtc < maxraylengthinverysmallunitswhenrayhitschunk)
                                                            {
                                                                /*if (someswtc == 0)
                                                                {
                                                                    for (int rever = 0; rever < 25; rever++)
                                                                    {
                                                                        currentRayPosition += (-footTarget.forward * (raylength * someRayLength));
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    //currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                                                }*/
                                                                //currentRayPosition += (footTarget.forward * (raylength * someRayLength));

                                                                //Debug.Log("byte == 0");
                                                                //currentFrameRayPosSwtc = 0;

                                                                //lastFrameRayPosSwtc = 1;
                                                                //Debug.Log("someswtc:" + someswtc);
                                                                //tippickaxestopwatch.Restart();



                                                                //currentRayPosition += (somemsg[0].footTargetforward * (raylength * someRayLength));
                                                                //lastFrameRayPos = currentRayPosition;
                                                                gottarget = 0;
                                                                someswtc++;
                                                                tippickaxestopwatch.Restart();


                                                                //goto loopagain;
                                                            }
                                                            else
                                                            {
                                                                gottarget = 0;
                                                                raylength = 0;
                                                                someswtc = 0;
                                                                tippickaxestopwatch.Restart();

                                                                lastFrameRayPosSwtc = 0;
                                                                currentFrameRayPosSwtc = 0;
                                                            }
                                                        }
                                                    }

                                                }

                                                currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                                lastFrameRayPos = currentRayPosition;
                                                raylength++;
                                                tippickaxestopwatch.Restart();
                                            }
                                            else
                                            {
                                                gottarget = 0;
                                                raylength = 0;
                                                someswtc = 0;
                                                tippickaxestopwatch.Restart();
                                                lastFrameRayPosSwtc = 0;
                                                currentFrameRayPosSwtc = 0;
                                            }
                                        }
                                        else
                                        {
                                            someswtc = 0;
                                            raylength = 0;
                                            lastFrameRayPosSwtc = 0;
                                            currentFrameRayPosSwtc = 0;
                                            tippickaxestopwatch.Restart();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else if (swtcForTypeOfInteract == 2)
        {
            if (counterForIkFootPlacement <= counterForIkFootPlacementMax)
            {
                //var ray = new Ray(transform.position, transform.forward);

                //RaycastHit hit;
                //Debug.DrawRay(transform.position, transform.forward * totallegLength, Color.green, someRayLength);

                /*if (Physics.Raycast(ray, out hit, 0.25f))
                {

                    //footTarget.transform.position = hit.point;

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

                var ray = new Ray(legstaticpivot.position, transform.forward);

                //RaycastHit hittwo;
                Debug.DrawRay(transform.position, transform.forward * (upperleglength), Color.green, someRayLength);

                if (Physics.Raycast(ray, out hit, totallegLength, layerMask))
                {
                    if (hit.transform.tag == "collisionObject")
                    {
                        Vector3 tempDir = legstaticpivot.position - footTarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

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
                            footTarget.position = tempVect;// hit.point;
                        }
                        else
                        {
                            footTarget.position = hit.point + (tempDir * foot.localScale.y);
                        }



                        /*if (tempDir.magnitude < (totallegLength * 0.5f))
                        {
                            footTarget.position = IdleStandingTargetPositionMin;
                        }
                        else
                        {
                            footTarget.position = hit.point;
                        }*/

                    }
                }
                counterForIkFootPlacement = 0;
            }
            counterForIkFootPlacement++;
        }
    }

    int counterForIkFootPlacement = 0;
    int counterForIkFootPlacementMax = 100;
    int counterForIkFootPlacementSwtc = 0;

    float someRayLength = 0.00001f;

}