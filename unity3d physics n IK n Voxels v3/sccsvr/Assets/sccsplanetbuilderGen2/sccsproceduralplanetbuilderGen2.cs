using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
//using SPINACH.iSCentralDispatch;
using System.Threading.Tasks;
using SimplexNoise;

using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class sccsproceduralplanetbuilderGen2 : MonoBehaviour
{
    public struct somemessage
    {
        public int testswtc;
        public int[][] chunkByteMaps;
        public int currentIndex;
        public Vector3 pickaxetiptransformforward;
        public Vector3 footTargetup;
        public Vector3 footTargetforward;
        public Vector3 foottransformposition;
        public sccsChunk[] somechunkarray;
        public int[] arrayOfDrawSwitches;
    }

    //pickaxetiptransform.transform.forward
    //footTarget.up
    //footTarget.forward
    //foot.transform.position

    public int somemaxframecounterforprojectile = 1500;

    public float planeSize = 0.1f;

    static somemessage[] _main_received_messages;//
    Task sccssometask;

    int taskcancelFlagTwo = 0;
    public static sccsproceduralplanetbuilderGen2 sccsproceduralplanetbuilderGen2staticscriptlock;

    int xi = 0;
    int yi = 0;
    int zi = 0;
    Vector3 planetchunkpos;
    int t = 0;
    int xx = 0;
    int yy = 0;
    int zz = 0;
    int counterCreateChunkObjectFaces = 0;
    int counterCreateChunkObjectClass = 0;
    int counterCreateChunkObjectClassSwtch = 0;
    int posx = 0;
    int posy = 0;
    int posz = 0;


    int swtchx = 0;
    int swtchy = 0;
    int swtchz = 0;

    //int index = 0;


    int startOnceSwtc = 0;

    int initcounter = 0;
    int initcounterMax = 50;
    int initcounterSwtc = 0;

    int framecounterForCreateChunkObject = 0;
    int framecounterForCreateChunkObjectMax = 1;


    int framecounterForCreateChunkObjectFaces = 0;
    int framecounterForCreateChunkObjectFacesMax = 1;

    int framecounterForCreateEmptyObjectFaces = 0;
    int framecounterForCreateEmptyObjectFacesMax = 1;

    //byte[,,] blocks;
    public sccsChunk[] blockers;
    static mainChunkGen2[] blockersgen2;
    //static byte[][] chunkByteMaps;
    //byte block;
    public float realplanetwidth = 1;
    //public Transform cube;
    Vector3[] myArray;

    //static int planetwidth = 16;
    //static int planetheight = 16;
    //static int planetdepth = 16;

    public int ChunkWidth_L = 4;
    public int ChunkWidth_R = 3;

    public int ChunkHeight_L = 4;
    public int ChunkHeight_R = 3;

    public int ChunkDepth_L = 4;
    public int ChunkDepth_R = 3;

    public static float noiseX;
    public static float noiseY;
    public static float noiseZ;

    //int planetwidth = 32;
    //int planetheight = 32;
    //int planetdepth = 32;
    bool cancelFlag = false;

    int tileCount = 0;


    private void OnDisable()
    {
        CancelInvoke();
        taskcancelFlagTwo = 1;
    }

    public int total = -1;

    //UnityTutorialPool unityTutorialGameObjectPool;
    public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = true;
    public List<GameObject> pooledObjects;
    public float multiplier = 1;
    public float speeding = 0.01f;
    public float repeatRate = 0.001f;

    void Awake()
    {


        //buildplanetchunk();

        //StartCoroutine(buildplanetchunk());

        //InvokeRepeating("buildplanetchunk", delayOrTime, repeatrate);
        //unitytutorialgameobjectpool = Unitytutorialgameobjectpool;
    }

    NewObjectPoolerScript UnityTutorialGameObjectPool;

    object[] someobjArray;
    private void Start()
    {
        someobjArray = new object[1];
        pooledObjects = new List<GameObject>();

        total = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);

        pooledAmount = Mathf.FloorToInt(total * 0.25f); //* 0.25f

        blockers = new sccsChunk[total];
        UnityTutorialGameObjectPool = this.transform.GetComponent<NewObjectPoolerScript>();
        //blockersgen2 = new mainChunkGen2[total];



        //chunkByteMaps = new byte[total][];


        /*for (int i = 0;i < total;i++)
        {
            chunkByteMaps[i] = new byte[16*16*16];
        }*/

        _main_received_messages = new somemessage[1];
        _main_received_messages[0].testswtc = 0;

        _main_received_messages[0].chunkByteMaps = new int[total][];


        for (int i = 0; i < total; i++)
        {
            _main_received_messages[0].chunkByteMaps[i] = new int[width * height * depth];
        }

        _main_received_messages[0].somechunkarray = blockers;

        //arrayOfChunkMapOfBytes





        //Debug.Log("sccsproceduralplanetbuilder.cs");

        /*if (UnityTutorialPool.current == null)
        {
            Debug.Log("null");
        }
        else
        {
            Debug.Log("!null");
        }*/

        /*var threadID0 = iSCentralDispatch.DispatchNewThread("thread00", MyThread0);
        iSCentralDispatch.SetPriorityForThread(threadID0, iSCDThreadPriority.Normal);
        iSCentralDispatch.SetTargetFramerate(60);*/

        //var _t1 = new Thread(MyThread0);
        //_t1.Start();






        /*
                for (int i = 0; i < total; i++)
                {
                    //InvokeRepeating("CreateChunkFaces", 0, speeding);
                    CreateChunkFaces();
                }*/

    }

    int counterCreateEmptyObjects = 0;
    public void CreateEmptyObjects()
    {
        if (counterCreateEmptyObjects < total)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            obj.transform.parent = this.transform;
            pooledObjects.Add(obj);
        }
        counterCreateEmptyObjects++;
    }




    // Update is called once per frame
    public GameObject GetPooledObject()
    {
        if (pooledObjects != null)
        {

            if (pooledObjects.Count > 0)
            {
                for (int i = 0; i < pooledObjects.Count; i++)
                {
                    if (!pooledObjects[i].activeInHierarchy)
                    {
                        return pooledObjects[i];
                    }
                }

                if (willGrow)
                {
                    GameObject obj = (GameObject)Instantiate(pooledObject);
                    //obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
            else
            {
                GameObject obj = (GameObject)Instantiate(pooledObject);
                //obj.SetActive(false);
                pooledObjects.Add(obj);
                return obj;
            }
        }

        return null;
    }








    sccsInstancesunitypool.instancedata instancedata;
    public Transform pickaxetiptransform;
    public Transform foot;
    public Transform footTarget;



    public void playerInteraction(somemessage somemsg)
    {

        /*
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

        if (swtcForTypeOfInteract == 1)
        {
            raycounterLoopMax = 750; //750

            bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

            if (buttonPressedLeft)
            {
                Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
            }
            if (buttonPressedRight)
            {
                Debug.Log("buttonPressedRight:" + buttonPressedRight);
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

            if (counterForByteChange == 0)// counterForByteChangeMax)
            {
                if (InitcounterForIkFootPlacementSwtc == 1)
                {
                    if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
                    {
                        if (raycounterSwtc == 0 || raycounterSwtc == 1)
                        {
                            if (tippickaxestopwatch.Elapsed.Ticks >= 1)
                            {
                                for (int r = 0; r < projectilespeed; r++)
                                {
                                //loopagain:

                                    if (currentFrameRayPosSwtc == 0)
                                    {
                                        currentRayPosition = somemsg.foottransformposition;
                                        //lastFrameRayDirForward = pickaxetiptransform.transform.forward;
                                        currentFrameRayPosSwtc = 1;
                                    }

                              



                                    if (raylength < raycounterLoopMax)
                                    {
                                        Vector3 rayFrameInitPos = somemsg.foottransformposition;
                                        Vector3 rayInityDir = somemsg.footTargetforward * someRayLength;

                                        Vector3 rayFrameInitPos0 = currentRayPosition + (somemsg.footTargetforward * (raylength * someRayLength));
                                   
                                        //float someremainsx = rayFrameInitPos0.x - Mathf.Floor(rayFrameInitPos0.x); // 23.5432f - 23.0f
                                        int neighboorindexx = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.x) / realplanetwidth); //   23.0f/2 = 11
                                        int neighboorindexy = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.y) / realplanetwidth); //   23.0f/2 = 11
                                        int neighboorindexz = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.z) / realplanetwidth); //   23.0f/2 = 11

                                        if (lastFrameRayPosSwtc == 0)
                                        {
                                            lastFrameRayInitDirUp = somemsg.footTargetup;
                                            lastFrameRayInitDirForward = _main_received_messages[0].pickaxetiptransformforward;
                                            lastFrameRayPosSwtc = 1;
                                        }




                                        //sphere.transform.position = new Vector3(rayFrameInitPos0.x, rayFrameInitPos0.y, rayFrameInitPos0.z);
                                        //sphere.transform.rotation = Quaternion.LookRotation(lastFrameRayInitDirForward, lastFrameRayInitDirUp);

                                        int gottarget = 0;

                                        if (gottarget == 0)
                                        {
                                            if (this.getChunk(neighboorindexx, neighboorindexy, neighboorindexz) != null)
                                            {
                                                mainChunkFinal currentChunk = this.getChunk(neighboorindexx, neighboorindexy, neighboorindexz);// posnotroundedx, posnotroundedy, posnotroundedz);

                                                int somechunkdivx = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.x) / realplanetwidth); //   23.0f/2 = 11
                                                int somechunkworldindexx = somechunkdivx * realplanetwidth; //11 * 2 = 22
                                                int indexx = (Mathf.FloorToInt((rayFrameInitPos0.x - somechunkworldindexx) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15

                                                int somechunkdivy = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.y) / realplanetwidth); //   23.0f/2 = 11
                                                int somechunkworldindexy = somechunkdivy * realplanetwidth; //11 * 2 = 22
                                                int indexy = (Mathf.FloorToInt((rayFrameInitPos0.y - somechunkworldindexy) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15

                                                int somechunkdivz = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.z) / realplanetwidth); //   23.0f/2 = 11
                                                int somechunkworldindexz = somechunkdivz * realplanetwidth; //11 * 2 = 22
                                                int indexz = (Mathf.FloorToInt((rayFrameInitPos0.z - somechunkworldindexz) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15
                                                
                                                Vector3 chunkbytepos = new Vector3(indexx, indexy, indexz);

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

                                                //retAdd.position = rayFrameInitPos0;

                                                //////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                //Debug.Log("indexx: " + (indexx) + " indexy: " + (indexy) + " indexz: " + (indexz));

                                                if (currentChunk.somesccsplanetchunkFinal.GetByte((int)indexx, (int)indexy, (int)indexz) == 1)
                                                {
                                                    currentChunk.somesccsplanetchunkFinal.SetByte((int)indexx, (int)indexy, (int)indexz, activeBlockType, chunkbytepos);
                                                    currentChunk.somesccsplanetchunkFinal.sccsSetMap();
                                                    currentChunk.somesccsplanetchunkFinal.Regenerate();
                                                    currentChunk.somesccsplanetchunkFinal.chunkbuildingswtc = 1;

                                                    if (currentChunk.somesccsplanetchunkFinal.vertexlist.Length > 0)
                                                    {
                                                        //currentChunk.somesccsplanetchunkFinal.buildMesh();

                                                        //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexy, indexz);
                                                        if (addfracturedcubeonimpact == 1)
                                                        {
                                                            instancedata = new sccsInstancesunitypool.instancedata();
                                                            chunkbytepos.x += planeSize * 0.5f;
                                                            chunkbytepos.y += planeSize * 0.5f;
                                                            chunkbytepos.z += planeSize * 0.5f;
                                                            instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                            instancedata.currentInstanceGameObject = null;
                                                            instancedata.instanceindex = 0;
                                                            instancedata.enabled = -1;
                                                            instancedata.swap = -1;
                                                            instancedata.instanceenabledcounter = 0;
                                                            instancedata.instanceenabledcounterSwtc = -1;
                                                            instancedata.instanceenabledcounterMax = 1;
                                                            sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);

                                                            //float somexx = Mathf.Floor(currentChunk.somesccsplanetchunkFinal.chunkPos.x);
                                                            //float someyy = Mathf.Floor(currentChunk.somesccsplanetchunkFinal.chunkPos.y);
                                                            //float somezz = Mathf.Floor(currentChunk.somesccsplanetchunkFinal.chunkPos.z);
                                                            //new Vector3(somexx, someyy, somezz) 

                                                            //var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                            //var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                            //UnityTutorialPooledObject.transform.position = chunkbytepos;// + new Vector3((indexX * 0.1f), (indexY * 0.1f), (indexZ * 0.1f)); ;// retAdd.position; new Vector3((int)indexX, (int)indexY, (int)indexZ);

                                                            //chunkbytepos.x += planeSize * 0.5f;
                                                            //chunkbytepos.y += planeSize * 0.5f;
                                                            //chunkbytepos.z += planeSize * 0.5f;

                                                            //UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                            //UnityTutorialPooledObject.SetActive(true);
                                                        }
                                                        gottarget = 1;
                                                        someswtc++;
                                                    }
                                                    else
                                                    {
                                                        gottarget = 0;
                                                        someswtc++;
                                                    }
                                                }
                                                else
                                                {
                                                    int maxraylengthinverysmallunitswhenrayhitschunk = 750;
                                                    if (someswtc < maxraylengthinverysmallunitswhenrayhitschunk)
                                                    {
                                                        //if (someswtc == 0)
                                                        //{
                                                        //    for (int rever = 0; rever < 25; rever++)
                                                        //    {
                                                        //        currentRayPosition += (-footTarget.forward * (raylength * someRayLength));
                                                        //    }
                                                        //}
                                                        //else
                                                        //{
                                                        //    //currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                                        //}
                                                        //currentRayPosition += (footTarget.forward * (raylength * someRayLength));

                                                        //Debug.Log("byte == 0");
                                                        //currentFrameRayPosSwtc = 0;

                                                        //lastFrameRayPosSwtc = 1;
                                                        //Debug.Log("someswtc:" + someswtc);
                                                        //tippickaxestopwatch.Restart();



                                                        //currentRayPosition += (somemsg.footTargetforward * (raylength * someRayLength));
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
                                                raylength++;
                                                tippickaxestopwatch.Restart();

                                            }

                                            currentRayPosition += (somemsg.footTargetforward * (raylength * someRayLength));
                                            lastFrameRayPos = currentRayPosition;
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
        }*/
    }

    float someRayLength = 0.000001f;

    private void Update()
    {
        //playerInteraction();




        if (initcounterSwtc == 0)
        {
            if (initcounter >= initcounterMax)
            {
                initcounterSwtc = 1;
                initcounter = 0;
            }
            initcounter++;
        }

        if (initcounterSwtc == 1)
        {
            if (startOnceSwtc == 0)
            {

                for (int i = 0; i < 1; i++)
                {
                    if (counterCreateEmptyObjects < total && swtchz == 0)
                    {
                        InvokeRepeating("CreateEmptyObjects", 0, 0.001f);
                    }
                    else
                    {
                        CancelInvoke();
                        Debug.Log("ended CreateEmptyObjects");
                        t = 0;
                        xx = -ChunkWidth_L;
                        yy = -ChunkHeight_L;
                        zz = -ChunkDepth_L;
                        swtchx = 0;
                        swtchy = 0;
                        swtchz = 0;
                        startOnceSwtc = 1;
                        break;
                    }
                }
            }


            if (startOnceSwtc == 1)
            {

                for (int i = 0; i < 1; i++)
                {
                    if (counterCreateChunkObjectClassSwtch == 0)
                    {

                        //CreateClassOfChunkGameObject();
                        InvokeRepeating("CreateClassOfChunkGameObject", 0, 0.05f);
                    }
                    else
                    {
                        CancelInvoke();
                        Debug.Log("ended CreateClassOfChunkGameObject");


                        /*Debug.Log("ended CreateClassOfChunkGameObject");
                        if (blockers != null)
                        {
                            for (int j = 0; j < total; j++)
                            {
                                if (blockers[j] != null)
                                {
                                    _main_received_messages[0].somechunkarray[j] = blockers[j];//blockers[i].arrayOfChunkMapOfBytes;
                                }
                            }
                        }*/

                        t = 0;
                        xx = -ChunkWidth_L;
                        yy = -ChunkHeight_L;
                        zz = -ChunkDepth_L;
                        swtchx = 0;
                        swtchy = 0;
                        swtchz = 0;
                        startOnceSwtc = 2;
                        break;
                    }
                }
            }

            /*public Vector3 pickaxetiptransformforward;
             public Vector3 footTargetup;
             public Vector3 footTargetforward;
             public Vector3 foottransformposition;*/


            _main_received_messages[0].pickaxetiptransformforward = pickaxetiptransform.forward;
            _main_received_messages[0].footTargetup = foot.up;
            _main_received_messages[0].footTargetforward = footTarget.forward;
            _main_received_messages[0].foottransformposition = foot.position;


            /*
            if (blockers != null)
            {
                for (int i = 0; i < total; i++)
                {
                    if (blockers[i] != null)
                    {
                        if (blockers[i].somesccsplanetchunkFinal.map != null)
                        {
                            _main_received_messages[0].chunkByteMaps[i] = blockers[i].somesccsplanetchunkFinal.map;//blockers[i].arrayOfChunkMapOfBytes;
                        }
                    }
                }
            }*/


            /*
            //_main_received_messages[0].testswtc = -1;
            // checking that the task has successfully ended the job by checking if it looped through from zero to the total. The task can be idle if there is no need to calculate any bytes and reactivated for chunks breaking
            if (_main_received_messages[0].currentIndex == 0)
            {
                //if (somecounter >= somecounterMax)
                {
                    //if (startOnceSwtc == 3 || _main_received_messages[0].testswtc == 4)
                    {
                        t = 0;
                        xx = -ChunkWidth_L;
                        yy = -ChunkHeight_L;
                        zz = -ChunkDepth_L;
                        swtchx = 0;
                        swtchy = 0;
                        swtchz = 0;

                        for (int i = 0; i < total; i++)
                        {
                            if (_main_received_messages[0].somechunkarray[i] != null)
                            {
                                if (_main_received_messages[0].somechunkarray[i].planetchunk != null)
                                {
                                    if (_main_received_messages[0].somechunkarray[i].somesccsplanetchunkFinal != null)
                                    {
                                        _main_received_messages[0].somechunkarray[i].somesccsplanetchunkFinal.map = blockers[i].somesccsplanetchunkFinal.map;

                                        //_main_received_messages[0].somechunkarray[i].somesccsplanetchunkFinal.sccsSetMap();
                                        //_main_received_messages[0].somechunkarray[i].somesccsplanetchunkFinal.Regenerate();
                                        //_main_received_messages[0].somechunkarray[i].somesccsplanetchunkFinal.chunkbuildingswtc = 1;
                                    }
                                }
                            }
                        }
                        Debug.Log("finished creating bytes for the chunks and finished pasting those byte map arrays back to the UI thread from the task.");
                        //_main_received_messages[0].testswtc = 1;

                        _main_received_messages[0].currentIndex = 0;
                        _main_received_messages[0].testswtc = 2;
                        //startOnceSwtc = 4;
                    }
                    //somecounter = 0;
                }
                //somecounter++;
                //Debug.Log("0pasting arrays");

            }*/




            someobjArray[0] = _main_received_messages;

            if (startOnceSwtc == 2)
            {
                sccssometask = Task<object[]>.Factory.StartNew((tester0001) =>
                {
                    Debug.Log("creating byte maps for the chunk");
                    //Debug.Log("checking thread alive");
                    //byte[][] somechunkByteMaps;


                    int xx0 = -ChunkWidth_L;
                    int yy0 = -ChunkHeight_L;
                    int zz0 = -ChunkDepth_L;
                    int swtchx0 = 0;
                    int swtchy0 = 0;
                    int swtchz0 = 0;
                    int t0 = 0;
                    int xi0 = 0;
                    int yi0 = 0;
                    int zi0 = 0;
                    int posx0 = 0;
                    int posy0 = 0;
                    int posz0 = 0;

                    int counterswtchForTask = 0;

                    int addfracturedcubeonimpact = 0;
                    int swtcForTypeOfInteract = 1;
                    float raycounterLoopMax = 20;
                    float raylength = 0;
                    float raycounterSwtc = 0;
                    int lastFrameRayPosSwtc = 0;
                    int currentFrameRayPosSwtc = 0;
                    int someswtc = 0;
                    int counterForByteChange = 1;
                    int InitcounterForIkFootPlacement = 0;
                    int InitcounterForIkFootPlacementMax = 10;
                    int InitcounterForIkFootPlacementSwtc = 0;
                    int counterForByteChangeMax = 1;
                    int projectilespeed = 1;
                    Vector3 currentRayPosition = Vector3.zero;
                    Vector3 lastFrameRayInitDirUp = Vector3.zero;
                    Vector3 lastFrameRayInitDirForward = Vector3.zero;
                    int activeBlockType = 0;
                    float planeSizetask = 0.1f;
                    Vector3 lastFrameRayPos = Vector3.zero;

                    Stopwatch tippickaxestopwatch = new Stopwatch();
                    Stopwatch stopwatch = new Stopwatch();

                    int someCounter = 0;

                    tippickaxestopwatch.Start();
                    stopwatch.Start();

                    int counterswtchForTaskinit = 0;


                    while (taskcancelFlagTwo == 0 || taskcancelFlagTwo == 2 || taskcancelFlagTwo == 4)
                    {
                        somemessage[] somemsg = (somemessage[])someobjArray[0];
                        taskcancelFlagTwo = somemsg[0].testswtc;
                        //Debug.Log(taskcancelFlagTwo);

                        try
                        {
                            if (counterswtchForTaskinit == 0)
                            {

                                somemsg[0].arrayOfDrawSwitches = new int[total];

                                for (int i = 0; i < total; i++)
                                {
                                    //if (t0 < total)
                                    {
                                        posx0 = (xx0);
                                        posy0 = (yy0);
                                        posz0 = (zz0);

                                        planetchunkpos = new Vector3(posx0 * realplanetwidth, posy0 * realplanetwidth, posz0 * realplanetwidth);
                                        //planetchunkpos = new Vector3(posx0, posy0, posz0);

                                        xi0 = xx0;
                                        yi0 = yy0;
                                        zi0 = zz0;

                                        if (xi0 < 0)
                                        {
                                            xi0 *= -1;
                                            xi0 = (ChunkWidth_R) + xi0;
                                        }
                                        if (yi0 < 0)
                                        {
                                            yi0 *= -1;
                                            yi0 = (ChunkHeight_R) + yi0;
                                        }
                                        if (zi0 < 0)
                                        {
                                            zi0 *= -1;
                                            zi0 = (ChunkDepth_R) + zi0;
                                        }

                                        var someindexmain = xi0 + (ChunkWidth_L + ChunkWidth_R + 1) * (yi0 + (ChunkHeight_L + ChunkHeight_R + 1) * zi0);

                                        if (someindexmain < total)
                                        {
                                            //somemsg[0].somechunkarray[someindexmain].chunkPos = planetchunkpos;

                                            //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.Regenerate();

                                            //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.Regenerate();

                                            //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.map = somemsg[0].chunkByteMaps[someindexmain];

                                            /*somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.sccsSetMap();
                                            somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.Regenerate();
                                            somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.chunkbuildingswtc = 1;*/
                                            somemsg[0].arrayOfDrawSwitches[someindexmain] = 1;
                                            somemsg[0].chunkByteMaps[someindexmain] = buildchunkmap(planetchunkpos);
                                            somemsg[0].somechunkarray[someindexmain].map = somemsg[0].chunkByteMaps[someindexmain];




                                            //somemsg[0].somechunkarray[someindexmain].sccsSetMap();
                                            //somemsg[0].somechunkarray[someindexmain].Regenerate();
                                            /*somemsg[0].somechunkarray[someindexmain].sccsSetMap();
                                            somemsg[0].somechunkarray[someindexmain].Regenerate();
                                            for (int j = 0; j < width * height * depth; j++)
                                            {
                                                if (somemsg[0].somechunkarray[someindexmain].map != null)
                                                {
                                                    somemsg[0].somechunkarray[someindexmain].CreateChunkFaces();
                                                }
                                            }*/

                                            //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.sccsSetMap();
                                            somemsg[0].currentIndex = someindexmain;
                                            somemsg[0].testswtc = 4;
                                        }
                                        else
                                        {
                                            ////t = total;
                                            //taskcancelFlagTwo = 1;
                                        }

                                        zz0++;
                                        if (zz0 >= (ChunkDepth_R))
                                        {
                                            xx0++;
                                            zz0 = -ChunkDepth_L;
                                            swtchx0 = 1;
                                        }
                                        if (xx0 >= (ChunkWidth_R) && swtchx0 == 1)
                                        {
                                            yy0++;
                                            xx0 = -ChunkWidth_L;
                                            swtchx0 = 0;
                                            swtchy0 = 1;
                                        }
                                        if (yy0 >= (ChunkHeight_R) && swtchy0 == 1)
                                        {
                                            //yy = -ChunkHeight_L;
                                            swtchy0 = 0;
                                            swtchx0 = 0;
                                            swtchz0 = 1;
                                            //taskcancelFlagTwo = 1;
                                            counterswtchForTaskinit = 1;
                                        }
                                        //t0++;
                                    }
                                }
                                //Debug.Log("total:" + total + "/t:" + t);
                            }


                            if (taskcancelFlagTwo == 2 || taskcancelFlagTwo == 4)
                            {
                                //Debug.Log("taskcancelFlagTwo == 2");
                                //playerInteraction(somemsg[0]);

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

                                //blockers[indexcreateface].somesccsplanetchunkFinal.sccsCustomStart(this);

                                if (swtcForTypeOfInteract == 1)
                                {
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
                                        someswtc = 0;
                                        raylength = 0;
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
                                                                currentRayPosition = somemsg[0].foottransformposition;// foot.transform.position;// somemsg[0].foottransformposition;
                                                                                                                      //lastFrameRayDirForward = pickaxetiptransform.transform.forward;
                                                                currentFrameRayPosSwtc = 1;
                                                            }


                                                            /*
                                                            var fractionOf = realplanetwidth / planeSize;

                                                            int neighboorindexx = Mathf.FloorToInt((rayFrameInitPos0.x / planeSize) / fractionOf); //4.654321/0.2 = 23.271605 => 23.271605/fractionOf = floor(2.3f)
                                                            int neighboorindexy = Mathf.FloorToInt((rayFrameInitPos0.y / planeSize) / fractionOf);
                                                            int neighboorindexz = Mathf.FloorToInt((rayFrameInitPos0.z / planeSize) / fractionOf);

                                                        
                                                            if (neighboorindexx < 0)
                                                            {
                                                                neighboorindexx *= -1;
                                                                neighboorindexx = (ChunkWidth_R) + neighboorindexx;
                                                            }
                                                            if (neighboorindexy < 0)
                                                            {
                                                                neighboorindexy *= -1;
                                                                neighboorindexy = (ChunkHeight_R) + neighboorindexy;
                                                            }
                                                            if (neighboorindexz < 0)
                                                            {
                                                                neighboorindexz *= -1;
                                                                neighboorindexz = (ChunkDepth_R) + neighboorindexz;
                                                            }





                                                            Vector3 chunkbytepos = new Vector3(neighboorindexx, neighboorindexy, neighboorindexz);

                                                            int indexmain = neighboorindexx + (ChunkWidth_L + ChunkWidth_R + 1) * (neighboorindexy + (ChunkHeight_L + ChunkHeight_R + 1) * neighboorindexz);

                                                            if (indexmain < total)
                                                            {
                                                                int indexx = 0;
                                                                int indexy = 0;
                                                                int indexz = 0;

                                                                var fractionOftemp = realplanetwidth / planeSize;

                                                                int neighboorindextempx = Mathf.FloorToInt((rayFrameInitPos0.x / planeSize)); //54
                                                                int neighboorindextempy = Mathf.FloorToInt((rayFrameInitPos0.y / planeSize));
                                                                int neighboorindextempz = Mathf.FloorToInt((rayFrameInitPos0.z / planeSize)); // 0.54321 / 0.01 = 54.321 floored = 54 * realplanetwidth = 5.4

                                                                //54

                                                                //Debug.Log(neighboorindexz);

                                                                indexx = Mathf.FloorToInt(neighboorindextempx - (Mathf.Floor(neighboorindextempx / width) * width)); //54-50
                                                                indexy = Mathf.FloorToInt(neighboorindextempy - (Mathf.Floor(neighboorindextempy / width) * width));
                                                                indexz = Mathf.FloorToInt(neighboorindextempz - (Mathf.Floor(neighboorindextempz / width) * width));




                                                                if (indexx < 0)
                                                                {
                                                                    indexx *= -1;
                                                                    indexx = (width - 1) - indexx;
                                                                }
                                                                if (indexy < 0)
                                                                {
                                                                    indexy *= -1;
                                                                    indexy = (width - 1) - indexy;
                                                                }
                                                                if (indexz < 0)
                                                                {
                                                                    indexz *= -1;
                                                                    indexz = (width - 1) - indexz;
                                                                }





                                                                */







                                                            if (raylength < raycounterLoopMax)
                                                            {

                                                                Vector3 rayFrameInitPos0 = currentRayPosition + (somemsg[0].footTargetforward * (raylength * someRayLength));

                                                                var fractionOf = realplanetwidth / planeSize;

                                                                int neighboorindexx = Mathf.FloorToInt((rayFrameInitPos0.x / planeSize) / fractionOf); //4.654321/0.2 = 23.271605 => 23.271605/fractionOf = floor(2.3f)
                                                                int neighboorindexy = Mathf.FloorToInt((rayFrameInitPos0.y / planeSize) / fractionOf);
                                                                int neighboorindexz = Mathf.FloorToInt((rayFrameInitPos0.z / planeSize) / fractionOf);

                                                                var tempx = neighboorindexx;
                                                                var tempy = neighboorindexy;
                                                                var tempz = neighboorindexz;

                                                                //someblock.transform.position = new Vector3(tempx, tempy, tempz);

                                                                /*if (mainscriptplanetgen.getChunk(neighboorindexx, neighboorindexy, neighboorindexz) != null)
                                                                {
                                                                    sccsChunk somechunk = (sccsChunk)mainscriptplanetgen.getChunk(neighboorindexx, neighboorindexy, neighboorindexz);
                                                                    if (somechunk.planetchunk != null)
                                                                    {
                                                                        somechunk.planetchunk.GetComponent<MeshRenderer>().material = hitmaterial;
                                                                    }
                                                                }*/

                                                                if (lastFrameRayPosSwtc == 0)
                                                                {
                                                                    lastFrameRayInitDirUp = somemsg[0].footTargetup;
                                                                    lastFrameRayInitDirForward = somemsg[0].pickaxetiptransformforward;// _main_received_messages[0].pickaxetiptransformforward;
                                                                    lastFrameRayPosSwtc = 1;
                                                                }

                                                                //rayvisual.transform.position = new Vector3(rayFrameInitPos0.x, rayFrameInitPos0.y, rayFrameInitPos0.z);
                                                                //rayvisual.transform.rotation = Quaternion.LookRotation(lastFrameRayInitDirForward, lastFrameRayInitDirUp);

                                                                //Debug.Log("projectile is supposed to be moving forward");

                                                                //Debug.Log("nx:" + neighboorindexx + "/ny: " + neighboorindexy + "/nz:" + neighboorindexz);

                                                                int gottarget = 0;

                                                                if (gottarget == 0)
                                                                {
                                                                    Vector3 chunkbytepos = new Vector3(neighboorindexx, neighboorindexy, neighboorindexz);

                                                                    //int indexmain = neighboorindexx + (mainscriptplanetgen.ChunkWidth_L + mainscriptplanetgen.ChunkWidth_R + 1) * (neighboorindexy + (mainscriptplanetgen.ChunkHeight_L + mainscriptplanetgen.ChunkHeight_R + 1) * neighboorindexz);


                                                                    if ((neighboorindexx >= -ChunkWidth_L) && (neighboorindexy >= -ChunkHeight_L) && (neighboorindexz >= -ChunkDepth_L) && (neighboorindexx < ChunkWidth_R + 1) && (neighboorindexy < (ChunkHeight_R + 1)) && (neighboorindexz < (ChunkDepth_R + 1)))
                                                                    {                                                        
                                                                        //Debug.Log("checking for neighbour chunk");
                                                                        if (neighboorindexx < 0)
                                                                        {
                                                                            neighboorindexx *= -1;
                                                                            neighboorindexx = (ChunkWidth_R) + neighboorindexx;
                                                                        }
                                                                        if (neighboorindexy < 0)
                                                                        {
                                                                            neighboorindexy *= -1;
                                                                            neighboorindexy = (ChunkHeight_R) + neighboorindexy;
                                                                        }
                                                                        if (neighboorindexz < 0)
                                                                        {
                                                                            neighboorindexz *= -1;
                                                                            neighboorindexz = (ChunkDepth_R) + neighboorindexz;
                                                                        }


                                                                        int indexx = 0;
                                                                        int indexy = 0;
                                                                        int indexz = 0;

                                                                        var fractionOftemp = realplanetwidth / planeSize;

                                                                        int neighboorindextempx = Mathf.FloorToInt((rayFrameInitPos0.x / planeSize)); //54
                                                                        int neighboorindextempy = Mathf.FloorToInt((rayFrameInitPos0.y / planeSize));
                                                                        int neighboorindextempz = Mathf.FloorToInt((rayFrameInitPos0.z / planeSize)); // 0.54321 / 0.01 = 54.321 floored = 54 * realplanetwidth = 5.4

                                                                        //54

                                                                        //Debug.Log(neighboorindexz);

                                                                        indexx = Mathf.FloorToInt(neighboorindextempx - (Mathf.Floor(neighboorindextempx / width) * width)); //54-50
                                                                        indexy = Mathf.FloorToInt(neighboorindextempy - (Mathf.Floor(neighboorindextempy / width) * width));
                                                                        indexz = Mathf.FloorToInt(neighboorindextempz - (Mathf.Floor(neighboorindextempz / width) * width));

                                                                        if (indexx < 0)
                                                                        {
                                                                            indexx *= -1;
                                                                            indexx = (width - 1) - indexx;
                                                                        }
                                                                        if (indexy < 0)
                                                                        {
                                                                            indexy *= -1;
                                                                            indexy = (width - 1) - indexy;
                                                                        }
                                                                        if (indexz < 0)
                                                                        {
                                                                            indexz *= -1;
                                                                            indexz = (width - 1) - indexz;
                                                                        }

                                                                        int indexOfChunkMapByte = indexx + width * (indexy + height * indexz);
                                                                        int indexmain = neighboorindexx + (ChunkWidth_L + ChunkWidth_R + 1) * (neighboorindexy + (ChunkHeight_L + ChunkHeight_R + 1) * neighboorindexz);

                                                                        if (somemsg[0].somechunkarray[indexmain].map != null)
                                                                        {

                                                                            //Debug.Log(indexOfChunkMapByte);


                                                                            if (indexOfChunkMapByte >= 0 && indexOfChunkMapByte < width * height * depth)//((int)indexx < 0) || ((int)indexy < 0) || ((int)indexz < 0) || ((int)indexy >= width) || ((int)indexx >= height) || ((int)indexz >= depth))
                                                                            {
                                                                                if (somemsg[0].somechunkarray[indexmain].map[indexOfChunkMapByte] == 1)//currentChunk.somesccsplanetchunkFinal.GetByte((int)indexx, (int)indexy, (int)indexz) == 1)
                                                                                {
                                                                                    //somechunk.SetByte(indexx, indexy, indexz, activeBlockType, chunkbytepos);

                                                                                    somemsg[0].somechunkarray[indexmain].SetByte((int)indexx, (int)indexy, (int)indexz, activeBlockType, chunkbytepos);

                                                                                    somemsg[0].arrayOfDrawSwitches[indexmain] = 1;

                                                                                    somemsg[0].testswtc = 4;

                                                                                    counterswtchForTask = 1;

                                                                                    t0 = 0;
                                                                                    swtchx0 = 0;
                                                                                    swtchy0 = 0;
                                                                                    swtchz0 = 0;
                                                                                    xx0 = -ChunkWidth_L;
                                                                                    yy0 = -ChunkHeight_L;
                                                                                    zz0 = -ChunkDepth_L;

                                                                                    t = 0;
                                                                                    swtchx = 0;
                                                                                    swtchy = 0;
                                                                                    swtchz = 0;
                                                                                    xx = -ChunkWidth_L;
                                                                                    yy = -ChunkHeight_L;
                                                                                    zz = -ChunkDepth_L;

                                                                                    startOnceSwtc = 3;



                                                                                    //Debug.Log("nx:" + neighboorindexx + "/ny:" + neighboorindexy + "/nz:" + neighboorindexz);

                                                                                    if (somemsg[0].somechunkarray[indexmain].vertexlist != null)
                                                                                    {

                                                                                        if (somemsg[0].somechunkarray[indexmain].vertexlist.Count > 0)
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




                                                                        //Debug.Log("neighbour tile is null");
                                                                        //return null;
                                                                        //raylength++;
                                                                        //tippickaxestopwatch.Restart();
                                                                    }
                                                                    else //(neighboorindexx < -ChunkWidth_L) || (neighboorindexy < -ChunkHeight_L) || (neighboorindexz < -ChunkDepth_L) || (neighboorindexx >= ChunkWidth_R + 1) || (neighboorindexy >= (ChunkHeight_R + 1)) || (neighboorindexz >= (ChunkDepth_R + 1))
                                                                    {
                                                                        

                                                                    }




                                                                    if (getChunk(neighboorindexx, neighboorindexy, neighboorindexz) != null)
                                                                    {
                                                                        sccsChunk somechunk = (sccsChunk)getChunk(neighboorindexx, neighboorindexy, neighboorindexz);
                                                                        /*if (somechunk.planetchunk != null)
                                                                        {
                                                                            somechunk.planetchunk.GetComponent<MeshRenderer>().material = hitmaterial;
                                                                        }*/


                                                                        if (somechunk.map != null)
                                                                        {
                                                                            //Debug.Log("found neighbour chunk");
                                                                            //mainChunkFinal currentChunk = somemsg[0].somechunkarray[index];// this.getChunk(neighboorindexx, neighboorindexy, neighboorindexz);// posnotroundedx, posnotroundedy, posnotroundedz);
                                                                            /*int indexx = 0;
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
                                                                            }*/

                                                                            
                                                                        }

                                                                    }

                                                                    currentRayPosition += (somemsg[0].footTargetforward * (raylength * someRayLength));
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
                        }
                        catch (UnityException ex)
                        {
                            Debug.Log(ex.ToString());
                        }










                        /*
                        if (counterswtchForTask == 1)
                        {
                            for (int i = 0; i < total; i++)
                            {
                                if (t0 < total)
                                {
                                    posx0 = (xx0);
                                    posy0 = (yy0);
                                    posz0 = (zz0);

                                    planetchunkpos = new Vector3(posx0 * realplanetwidth, posy0 * realplanetwidth, posz0 * realplanetwidth);
                                    //planetchunkpos = new Vector3(posx0, posy0, posz0);

                                    xi0 = xx0;
                                    yi0 = yy0;
                                    zi0 = zz0;

                                    if (xi0 < 0)
                                    {
                                        xi0 *= -1;
                                        xi0 = (ChunkWidth_R) + xi0;
                                    }
                                    if (yi0 < 0)
                                    {
                                        yi0 *= -1;
                                        yi0 = (ChunkHeight_R) + yi0;
                                    }
                                    if (zi0 < 0)
                                    {
                                        zi0 *= -1;
                                        zi0 = (ChunkDepth_R) + zi0;
                                    }

                                    var someindexmain = xi0 + (ChunkWidth_L + ChunkWidth_R + 1) * (yi0 + (ChunkHeight_L + ChunkHeight_R + 1) * zi0);

                                    if (someindexmain < total)
                                    {


                                        if (somemsg[0].arrayOfDrawSwitches[someindexmain] == 1) //if (somemsg[0].somechunkarray[someindexmain].chunkbuildingswtc == 1)
                                        {
                                            //somemsg[0].somechunkarray[someindexmain].map = somemsg[0].chunkByteMaps[someindexmain];

                                            //somemsg[0].somechunkarray[someindexmain].sccsSetMap();
                                            /*somemsg[0].somechunkarray[someindexmain].Regenerate();

                                            for (int j = 0; j < 10 * 10 * 10; j++)
                                            {
                                                if (somemsg[0].somechunkarray[someindexmain].map != null)
                                                {
                                                    somemsg[0].somechunkarray[someindexmain].CreateChunkFaces();
                                                }
                                            }
                                            //Debug.Log("creating vertex map in task");
                                            somemsg[0].arrayOfDrawSwitches[someindexmain] = 2;// somemsg[0].somechunkarray[someindexmain].chunkbuildingswtc = 2;
                                        }

                                        //somemsg[0].somechunkarray[someindexmain].chunkPos = planetchunkpos;

                                        //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.Regenerate();

                                        //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.Regenerate();

                                        //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.map = somemsg[0].chunkByteMaps[someindexmain];

                                        //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.sccsSetMap();
                                        //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.Regenerate();
                                        //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.chunkbuildingswtc = 1;

                                        //somemsg[0].chunkByteMaps[someindexmain] = buildchunkmap(planetchunkpos);
                                        //somemsg[0].somechunkarray[someindexmain].map = somemsg[0].chunkByteMaps[someindexmain];
                                        //somemsg[0].somechunkarray[someindexmain].sccsSetMap();
                                        //somemsg[0].somechunkarray[someindexmain].Regenerate();


                                        //somemsg[0].somechunkarray[someindexmain].somesccsplanetchunkFinal.sccsSetMap();
                                        //somemsg[0].currentIndex = someindexmain;
                                    }
                                    else
                                    {
                                        ////t = total;
                                        //taskcancelFlagTwo = 1;
                                    }

                                    zz0++;
                                    if (zz0 >= (ChunkDepth_R))
                                    {
                                        xx0++;
                                        zz0 = -ChunkDepth_L;
                                        swtchx0 = 1;
                                    }
                                    if (xx0 >= (ChunkWidth_R) && swtchx0 == 1)
                                    {
                                        yy0++;
                                        xx0 = -ChunkWidth_L;
                                        swtchx0 = 0;
                                        swtchy0 = 1;
                                    }
                                    if (yy0 >= (ChunkHeight_R) && swtchy0 == 1)
                                    {
                                        //yy = -ChunkHeight_L;
                                        swtchy0 = 0;
                                        swtchx0 = 0;
                                        swtchz0 = 1;
                                        //taskcancelFlagTwo = 1;
                                        counterswtchForTask = 0;
                                    }
                                    t0++;
                                }
                            }

                        }*/











                        //someobjArray[0] = somemsg;
                        somemsg[0].currentIndex = someCounter;
                        someCounter++;
                        Thread.Sleep(1);
                    }
                    return someobjArray;
                    //////CONSOLE WRITER <=
                }, someobjArray);

                startOnceSwtc = 3;
            }

            _main_received_messages = (somemessage[])someobjArray[0];

            //blockers = _main_received_messages[0].somechunkarray;



            //Debug.Log("swtc:" + _main_received_messages[0].testswtc);

            /*
            // checking that the task has successfully ended the job by checking if it looped through from zero to the total. The task can be idle if there is no need to calculate any bytes and reactivated for chunks breaking
            if (_main_received_messages[0].currentIndex >= total)
            {
                //if (somecounter >= somecounterMax)
                {

                    if (startOnceSwtc == 3 || _main_received_messages[0].testswtc == 4)
                    {
                        t = 0;
                        xx = -ChunkWidth_L;
                        yy = -ChunkHeight_L;
                        zz = -ChunkDepth_L;
                        swtchx = 0;
                        swtchy = 0;
                        swtchz = 0;

                        for (int i = 0; i < blockers.Length; i++)
                        {
                            //blockers[i] = _main_received_messages[0].somechunkarray[i];

                            if (blockers[i].planetchunk != null)
                            {
                                if (_main_received_messages[0].chunkByteMaps[i] != null)
                                {
                                    blockers[i].map = _main_received_messages[0].chunkByteMaps[i];

                                    


                                    //blockers[i].somesccsplanetchunkFinal.buildMesh();

                                    /*for (int j = 0;j < total;j++)
                                    {
                                        blockers[i].somesccsplanetchunkFinal.CreateChunkFaces();
                                    }

                                    blockers[i].somesccsplanetchunkFinal.buildMesh();
                                    //blockers[i].somesccsplanetchunkFinal.chunkbuildingswtc = 1;
                                }
                            }

                        }
                        Debug.Log("finished creating bytes for the chunks and finished pasting those byte map arrays back to the UI thread from the task.");
                        //_main_received_messages[0].testswtc = 1;

                        _main_received_messages[0].currentIndex = 0;
                        _main_received_messages[0].testswtc = 2;
                        startOnceSwtc = 4;
                    }
                    //somecounter = 0;
                }
                //somecounter++;
                //Debug.Log("0pasting arrays");

            }*/

            //_main_received_messages[0].somechunkarray = blockers;
            //_main_received_messages[0].pickaxetiptransformforward = pickaxetiptransform.forward;
            //_main_received_messages[0].footTargetup = foot.up;
            //_main_received_messages[0].footTargetforward = footTarget.forward;
            //_main_received_messages[0].foottransformposition = foot.position;






            if (_main_received_messages[0].testswtc == 4)
            {
                //Debug.Log("started CreateChunkFaces");

                if (somecounterccf >= somecounterccfMax)
                {
                    /*for (int i = 0; i < blockers.Length; i++)
                    {
                    
                    }*/

                    for (int i = 0; i < 25; i++)
                    {
                        if (swtchz == 0)//counterCreateChunkObjectFaces < total)
                        {
                            //CreateChunkFaces();

                            //calculate average of cost for this type of task and start the invoke repeating at the necessary interval for no lag to happen. code this later.
                            InvokeRepeating("CreateChunkFaces", 0, 0.001f);
                            //Debug.Log("trying to build face");
                        }
                        else
                        {
                            CancelInvoke();
                            //Debug.Log("ended CreateChunkFaces");
                            somestartswtc = 1;

                            _main_received_messages[0].currentIndex = 0;
                            _main_received_messages[0].testswtc = 2;

                            t = 0;

                            xx = -ChunkWidth_L;
                            yy = -ChunkHeight_L;
                            zz = -ChunkDepth_L;

                            swtchx = 0;
                            swtchy = 0;
                            swtchz = 0;

                            //startOnceSwtc = 4;
                            //break;
                        }
                    }
                    somecounterccf = 0;
                }
                somecounterccf++;
            }
        }
    }

    int somecounterccf = 0;
    int somecounterccfMax = 1;

    int somecounter = 0;
    int somecounterMax = 0;

    void CreateClassOfChunkGameObject()
    {
        //Debug.Log("test");
        //unroll
        //[loop]
        //in order to not use those in hlsl because i never completely understood how they worked then, i decided to learn how to build flat loops (per frame or not). i upgraded this today working a bit on it to a flat loop 
        //with negative and positive indexes so that it works as it was with my negative/positive planets chunks. by steve chassé aka ninekorn 2021-mai-08th.

        if (counterCreateChunkObjectClassSwtch == 0)
        {
            if (t < total)
            {
                posx = (xx);
                posy = (yy);
                posz = (zz);

                xi = xx;
                yi = yy;
                zi = zz;

                if (xi < 0)
                {
                    xi *= -1;
                    xi = (ChunkWidth_R) + xi;
                }
                if (yi < 0)
                {
                    yi *= -1;
                    yi = (ChunkHeight_R) + yi;
                }
                if (zi < 0)
                {
                    zi *= -1;
                    zi = (ChunkDepth_R) + zi;
                }

                var indexcreateface = xi + (ChunkWidth_L + ChunkWidth_R + 1) * (yi + (ChunkHeight_L + ChunkHeight_R + 1) * zi);

                if (indexcreateface < total)
                {
                    var somePooledObject = GetPooledObject();

                    if (somePooledObject != null)
                    {
                        //var somechunk = new sccslodchunkfinal();

                        /*planetchunkpos = new Vector3(posx * realplanetwidth, posy * realplanetwidth, posz * realplanetwidth);
                        //planetchunkpos = new Vector3(posx, posy, posz);
                        somePooledObject.transform.parent = this.transform;
                        somePooledObject.transform.position = planetchunkpos;
                        somePooledObject.SetActive(true);
                        //somePooledObject.GetComponent<sccslodchunkfinal>();
                        var somescriptcomp = somePooledObject.GetComponent<sccslodchunkfinal>();
                        //planetchunkpos = new Vector3(posx, posy, posz);
                        blockers[indexcreateface] = new mainChunkFinal(planetchunkpos, somePooledObject.gameObject, somescriptcomp, indexcreateface, this);
                        blockers[indexcreateface].somesccsplanetchunkFinal = somescriptcomp;
                        //blockers[indexcreateface].somesccsplanetchunkFinal.sccsCustomStart(this);
                        _main_received_messages[0].somechunkarray = blockers;
                        */

                        planetchunkpos = new Vector3(posx * (planeSize * width), posy * (planeSize * width), posz * (planeSize * width));

                        somePooledObject.transform.parent = this.transform;
                        somePooledObject.transform.position = planetchunkpos;
                        somePooledObject.SetActive(true);

                        //planetchunkpos = new Vector3(posx, posy, posz);
                        blockers[indexcreateface] = new sccsChunk();
                        blockers[indexcreateface].sccsCustomStart(somePooledObject.transform, planetchunkpos, planeSize, realplanetwidth, width, height, depth, this, 0, UnityTutorialGameObjectPool);
                        blockers[indexcreateface].mesh = new Mesh();

                        blockers[indexcreateface].planetchunk = somePooledObject.transform;
                        blockers[indexcreateface].planetchunk.GetComponent<MeshFilter>().mesh = blockers[indexcreateface].mesh;

                        //blockers[indexcreateface].chunkbuildingswtc = 1;

                        //_main_received_messages[0].somechunkarray = blockers;


                        /*if (blockers != null)
                        {
                            for (int j = 0; j < total; j++)
                            {
                                if (blockers[j] != null)
                                {
                                    _main_received_messages[0].somechunkarray[j] = blockers[j];//blockers[i].arrayOfChunkMapOfBytes;
                                }
                            }
                        }*/

                        //blockers[index].arrayOfChunkMapOfBytes = _main_received_messages[0].chunkByteMaps[index];

                        //unityTutorialPooledGameObject = NewObjectPoolerScript.current.GetPooledObject();

                        //unityTutorialPooledGameObject.transform.parent = transform;

                        //unityTutorialPooledGameObject.transform.position = planetchunkpos;
                        //unityTutorialPooledGameObject.SetActive(true);
                    }
                    else
                    {
                        //Debug.Log("null somePooledObject");
                    }
                }
                else
                {
                    ////t = total;
                }

                zz++;
                if (zz >= (ChunkDepth_R))
                {
                    xx++;
                    zz = -ChunkDepth_L;
                    swtchx = 1;
                }
                if (xx >= (ChunkWidth_R) && swtchx == 1)
                {
                    yy++;
                    xx = -ChunkWidth_L;
                    swtchx = 0;
                    swtchy = 1;
                }
                if (yy >= (ChunkHeight_R) && swtchy == 1)
                {
                    //yy = -ChunkHeight_L;
                    swtchy = 0;
                    swtchx = 0;
                    swtchz = 1;
                    counterCreateChunkObjectClassSwtch = 1;
                }
                t++;
                counterCreateChunkObjectClass++;
            }

            //Debug.Log("total:" + total + "/t:" + t);
        }
    }



    int somestartswtc = 0;


    void CreateChunkFaces()
    {
        if (swtchz == 0)
        {
            if (t < total)
            {
                posx = (xx);
                posy = (yy);
                posz = (zz);

                xi = xx;
                yi = yy;
                zi = zz;

                if (xi < 0)
                {
                    xi *= -1;
                    xi = (ChunkWidth_R) + xi;
                }
                if (yi < 0)
                {
                    yi *= -1;
                    yi = (ChunkHeight_R) + yi;
                }
                if (zi < 0)
                {
                    zi *= -1;
                    zi = (ChunkDepth_R) + zi;
                }

                var indexCreateChunkFaces = xi + (ChunkWidth_L + ChunkWidth_R + 1) * (yi + (ChunkHeight_L + ChunkHeight_R + 1) * zi);

                if (indexCreateChunkFaces < total)
                {
                    if (blockers[indexCreateChunkFaces].map != null)
                    {

                        if (blockers[indexCreateChunkFaces].planetchunk != null)
                        {
                            /*
                            if (blockers[indexCreateChunkFaces].somesccsplanetchunkFinal != null)
                            {
                                //blockers[index].somesccsplanetchunkFinal.sccsSetMap();
                                //blockers[index].somesccsplanetchunkFinal.Regenerate();

                                //if (blockers[index].somesccsplanetchunkFinal.vertexlist.Count > 0)
                                //{
                                //    blockers[index].somesccsplanetchunkFinal.buildMesh();
                                //}
                            }*/

                            if (somestartswtc == 0)
                            {
                                blockers[indexCreateChunkFaces].sccsSetMap();
                                blockers[indexCreateChunkFaces].Regenerate();

                                for (int j = 0; j < width * height * depth; j++)
                                {
                                    if (blockers[indexCreateChunkFaces].map != null)
                                    {
                                        blockers[indexCreateChunkFaces].CreateChunkFaces();
                                    }
                                }
                                if (blockers[indexCreateChunkFaces].vertexlist.Count > 0)
                                {
                                    /*blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                    blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh.vertices = blockers[indexCreateChunkFaces].vertexlist.ToArray();
                                    blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh.triangles = blockers[indexCreateChunkFaces].triangles.ToArray();
                                    blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                    blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();
                                    */

                                    blockers[indexCreateChunkFaces].mesh.Clear();
                                    blockers[indexCreateChunkFaces].mesh.vertices = blockers[indexCreateChunkFaces].vertexlist.ToArray();
                                    blockers[indexCreateChunkFaces].mesh.triangles = blockers[indexCreateChunkFaces].triangles.ToArray();
                                    blockers[indexCreateChunkFaces].mesh.RecalculateBounds();
                                    blockers[indexCreateChunkFaces].mesh.RecalculateNormals();
                                    //blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh = blockers[indexCreateChunkFaces].mesh;

                                }

                            }
                            else
                            {
                                //somemsg[0].arrayOfDrawSwitches[indexmain]
                                //blockers[index].chunkbuildingswtc = 1;
                                if (_main_received_messages[0].arrayOfDrawSwitches[indexCreateChunkFaces] == 1)
                                {
                                    blockers[indexCreateChunkFaces].sccsSetMap();
                                    blockers[indexCreateChunkFaces].Regenerate();

                                    for (int j = 0; j < width * height * depth; j++)
                                    {
                                        if (blockers[indexCreateChunkFaces].map != null)
                                        {
                                            blockers[indexCreateChunkFaces].CreateChunkFaces();
                                        }
                                    }

                                    if (blockers[indexCreateChunkFaces].vertexlist.Count > 0)
                                    {
                                        /*blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                          blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh.vertices = blockers[indexCreateChunkFaces].vertexlist.ToArray();
                                          blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh.triangles = blockers[indexCreateChunkFaces].triangles.ToArray();
                                          blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                          blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();
                                          */

                                        blockers[indexCreateChunkFaces].mesh.Clear();
                                        blockers[indexCreateChunkFaces].mesh.vertices = blockers[indexCreateChunkFaces].vertexlist.ToArray();
                                        blockers[indexCreateChunkFaces].mesh.triangles = blockers[indexCreateChunkFaces].triangles.ToArray();
                                        blockers[indexCreateChunkFaces].mesh.RecalculateBounds();
                                        blockers[indexCreateChunkFaces].mesh.RecalculateNormals();
                                        //blockers[indexCreateChunkFaces].planetchunk.GetComponent<MeshFilter>().mesh = blockers[indexCreateChunkFaces].mesh;
                                    }

                                    //blockers[indexCreateChunkFaces].chunkbuildingswtc = 0;
                                    _main_received_messages[0].arrayOfDrawSwitches[indexCreateChunkFaces] = 0;
                                }
                            }



                            /*if (blockers[indexCreateChunkFaces].somesccsplanetchunkFinal.chunkbuildingswtc == 0)
                            {
                                blockers[indexCreateChunkFaces].somesccsplanetchunkFinal.chunkbuildingswtc = 1;
                                //blockers[index].somesccsplanetchunkFinal.CreateChunkFaces();
                            }
                            else
                            {
                                //blockers[index].somesccsplanetchunkFinal.buildMesh();
                            }*/

                        }
                    }
                    else
                    {
                        Debug.Log("null");
                    }
                }
                else
                {
                    //t = total;
                }

                zz++;
                if (zz >= (ChunkDepth_R))
                {
                    xx++;
                    zz = -ChunkDepth_L;
                    swtchx = 1;
                }
                if (xx >= (ChunkWidth_R) && swtchx == 1)
                {
                    yy++;
                    xx = -ChunkWidth_L;
                    swtchx = 0;
                    swtchy = 1;
                }
                if (yy >= (ChunkHeight_R) && swtchy == 1)
                {
                    //yy = -ChunkHeight_L;
                    swtchy = 0;
                    swtchx = 0;
                    swtchz = 1;
                }
                t++;
                counterCreateChunkObjectFaces++;
            }

            //Debug.Log("total:" + total + "/t:" + t);
        }
    }
















    WaitForSeconds waitforseconds;// = new WaitForSeconds();
    GameObject unityTutorialPooledGameObject;



    float seed;
    byte block;

    float nodeDiameter;
    float chunkRadius;
    float fraction;
    float chunkSize;


    public int width = 10;
    public int height = 10;
    public int depth = 10;

    public float detailScale = 5;
    public float heightScale = 5;
    public int heightScale1 = 5;
    public int detailScale1 = 5;

    private float radiusplanetcorestart = 0.0f;
    private float radiusplanetcoreend = 0.5f;
    private float radiusplanetcavesstart = 0.5f;
    private float radiusplanetcavesend = 1.25f;
    private float radiusplanetcruststart = 1.25f;
    private float radiusplanetcrustend = 1.5f;
    private float radiusplanetmountainstart = 1.5f;
    private float radiusplanetmountainend = 2.25f;
    /*private float radiusplanetcorestart = 0.0f;
    private float radiusplanetcoreend = 5.0f;
    private float radiusplanetcavesstart = 5.0f;
    private float radiusplanetcavesend = 9.0f;
    private float radiusplanetcruststart = 9.0f;
    private float radiusplanetcrustend = 11.0f;
    private float radiusplanetmountainstart = 11.0f;
    private float radiusplanetmountainend = 20.0f;*/



    public int[] buildchunkmap(Vector3 position_) //sccsproceduralplanetbuilder componentParent_, 
    {

        nodeDiameter = planeSize;
        chunkRadius = planeSize / 2;
        fraction = (int)(1 / (planeSize));
        chunkSize = 1f;

        //meshCollider = GetComponent<MeshCollider>();
        //transform.localScale *= planeSize;

        //map = new byte[width*height*depth];
        seed = 3420;
        //seed = Random.Range(3000, 4000);

        //seed = 0;
        //checkBytePos();
        int radius = 5;
        Vector3 center = Vector3.zero;

        //if (position.y >= 3)
        //{
        int[] map = new int[width * height * depth];


        float offsetDist = 0;

        Vector3 position1 = center;//// transform.parent.position;
        float distance1 = Vector3.Distance(position1, center);

        if (position_.x < 0 || position_.y < 0 || position_.z < 0)
        {
            offsetDist = distance1;
        }



        /**/
        //radiusplanetcrustend = ChunkWidth_R;


        radiusplanetmountainend = ChunkWidth_R; //+ offsetDist

        for (int x = 0; x < width; x++)
        {
            float noiseX = Mathf.Abs(((float)(x * planeSize + position_.x + seed) / detailScale) * heightScale);

            for (int y = 0; y < height; y++)
            {
                float noiseY = Mathf.Abs(((float)(y * planeSize + position_.y + seed) / detailScale) * heightScale);

                for (int z = 0; z < depth; z++)
                {
                    float noiseZ = Mathf.Abs(((float)(z * planeSize + position_.z + seed) / detailScale) * heightScale);

                    float posX = x * planeSize + position_.x;
                    float posY = y * planeSize + position_.y;
                    float posZ = z * planeSize + position_.z;

                    Vector3 pos = new Vector3(posX, posY, posZ);

                    float distance = Vector3.Distance(pos, center);

                    int indexOf = x + width * (y + depth * z);

                    /*float temporaryY = 0.1f;
                    float temporaryZ = 0.1f;
                    float temporaryX = 0.1f;

                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);
                    float size0 = (1 / planeSize) * position_.y;
                    temporaryY -= size0;


                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + position_.y + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);
                    float size1 = (1 / planeSize) * position_.x;
                    temporaryX -= size1;

                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (y * planeSize + position_.y + seed) / detailScale1) * heightScale1);
                    float size2 = (1 / planeSize) * position_.z;
                    temporaryZ -= size2;


                    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                    {
                        map[x, y, z] = 1;
                    }*/
                    //map[x, y, z] = 1;

                    /*float temporaryY = 1f;
                    float temporaryZ = 0.1f;
                    float temporaryX = 0.1f;


                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                    float size0 = (1 / planeSize) * position_.y;
                    temporaryY -= size0;


                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + position_.y + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                    float size1 = (1 / planeSize) * position_.x;
                    temporaryX -= size1;

                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (y * planeSize + position_.y + seed) / detailScale1) * heightScale1);

                    float size2 = (1 / planeSize) * position_.z;
                    temporaryZ -= size2;*/


                    /*if ((int)Mathf.Round(temporaryY) >= y )
                    {
                        map[x, y, z] = 1;
                    }*/

                    /*if ((int)Mathf.Round(temporaryY) >= 0)
                    {
                        map[x, y, z] = 1;
                    }*/

                    //map[indexOf] = 1;

                    //if (distance1 >= 0 && distance1 < 19 )
                    {
                        map[indexOf] = 1;

                        if (distance <= radiusplanetmountainend + offsetDist)
                        {
                            //map[indexOf] = 1;
                        }

                        /*else if (distance > radiusplanetcoreend && distance <= radiusplanetcavesend)
                        {
                            float noiseValue0 = Noise.Generate(noiseX, noiseY, noiseZ);
                            if (noiseValue0 > 0.2f)
                            {
                                map[indexOf] = 1;
                            }
                        }

                        else if (distance >= radiusplanetcavesend && distance <= radiusplanetcrustend)
                        {
                            map[indexOf] = 1;
                        }

                        else if (distance > radiusplanetcrustend && distance < radiusplanetmountainend)
                        {


                            float temporaryY = 10;
                            float temporaryZ = 10;
                            float temporaryX = 10;

                            if (position_.y < 0 && position_.x < 0 && position_.z < 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);
                                float size0 = (1 / planeSize) * position_.y;
                                temporaryY -= size0;

                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position_.y + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);
                                float size1 = (1 / planeSize) * position_.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (y * planeSize + position_.y + seed) / detailScale1) * heightScale1);
                                float size2 = (1 / planeSize) * position_.z;
                                temporaryZ -= size2;

                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[indexOf] = 1;
                                }
                            }

                            else if (position_.y >= 0 && position_.x >= 0 && position_.z >= 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position_.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + position_.y + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position_.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (y * planeSize + position_.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position_.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[indexOf] = 1;
                                }
                            }

                            else if (position_.y >= 0 && position_.x < 0 && position_.z >= 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position_.y;
                                temporaryY -= size0;

                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position_.y + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position_.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (y * planeSize + position_.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position_.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[indexOf] = 1;
                                }
                            }


                            else if (position_.y >= 0 && position_.x >= 0 && position_.z < 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position_.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + position_.y + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position_.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (y * planeSize + position_.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position_.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[indexOf] = 1;
                                }
                            }





                            else if (position_.y >= 0 && position_.x < 0 && position_.z < 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position_.y;
                                temporaryY -= size0;


                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position_.y + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position_.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (y * planeSize + position_.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position_.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[indexOf] = 1;
                                }
                            }



                            else if (position_.y < 0 && position_.x >= 0 && position_.z < 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position_.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + position_.y + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position_.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (y * planeSize + position_.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position_.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[indexOf] = 1;
                                }
                            }





                            else if (position_.y < 0 && position_.x >= 0 && position_.z >= 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);
                                float size0 = (1 / planeSize) * position_.y;
                                temporaryY -= size0;

                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + position_.y + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);
                                float size1 = (1 / planeSize) * position_.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (y * planeSize + position_.y + seed) / detailScale1) * heightScale1);
                                float size2 = (1 / planeSize) * position_.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[indexOf] = 1;
                                }
                            }





                            else if (position_.y < 0 && position_.x < 0 && position_.z >= 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position_.y;
                                temporaryY -= size0;


                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position_.y + seed) / detailScale1, (z * planeSize + position_.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position_.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position_.x + seed) / detailScale1, (y * planeSize + position_.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position_.z;
                                temporaryZ -= size2;

                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[indexOf] = 1;
                                }
                            }
                            else
                            {
                                map[indexOf] = 0;

                            }
                        }*/
                    }
                }
            }
        }
        return map;
        //GetComponent<sccsplanetchunkGen2>().enabled = false;
    }




    public object getChunk(int x, int y, int z)
    {
        if ((x < -ChunkWidth_L) || (y < -ChunkHeight_L) || (z < -ChunkDepth_L) || (x >= ChunkWidth_R + 1) || (y >= (ChunkHeight_R + 1)) || (z >= (ChunkDepth_R + 1)))
        {
            return null;
        }

        if (x < 0)
        {
            x *= -1;
            x = (ChunkWidth_R) + x;
        }
        if (y < 0)
        {
            y *= -1;
            y = (ChunkHeight_R) + y;
        }
        if (z < 0)
        {
            z *= -1;
            z = (ChunkDepth_R) + z;
        }

        int index = x + (ChunkWidth_L + ChunkWidth_R + 1) * (y + (ChunkHeight_L + ChunkHeight_R + 1) * z);

        return blockers[index];

        //return map[index] == 0;
        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
		{
			return null;
		}

		return blockers[x, y, z];*/




        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
        {
            return null;
        }
        if (blockers[x, y, z] == null)
        {
            return null;
        }
        return blockers[x, y, z];*/
    }


    public mainChunkGen2 getChunkmainChunkGen2(int x, int y, int z)
    {
        if ((x < -ChunkWidth_L) || (y < -ChunkHeight_L) || (z < -ChunkDepth_L) || (x >= ChunkWidth_R + 1) || (y >= (ChunkHeight_R + 1)) || (z >= (ChunkDepth_R + 1)))
        {
            return null;
        }

        if (x < 0)
        {
            x *= -1;
            x = (ChunkWidth_R) + x;
        }
        if (y < 0)
        {
            y *= -1;
            y = (ChunkHeight_R) + y;
        }
        if (z < 0)
        {
            z *= -1;
            z = (ChunkDepth_R) + z;
        }

        int index = x + (ChunkWidth_L + ChunkWidth_R + 1) * (y + (ChunkHeight_L + ChunkHeight_R + 1) * z);

        return blockersgen2[index];

        //return map[index] == 0;
        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
		{
			return null;
		}

		return blockers[x, y, z];*/




        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
        {
            return null;
        }
        if (blockers[x, y, z] == null)
        {
            return null;
        }
        return blockers[x, y, z];*/
    }

    public void drawBrick(int x, int y, int z)
    {
        //Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
        Debug.Log("this function is supposed to instantiate a brick. fix this or not. sccsproceduralplanetbuilder.cs");
    }

    /*public byte GetByte(mainChunkFinal chuk,int x, int y, int z)
    {
		chuk.chunker.get

        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            return 0;
        }
        return blocks[x, y, z];
    }*/


    void MyThread0()
    {
        while (cancelFlag == false)
        {
            iSCentralDispatch.DispatchMainThread(() =>
            {
                if (tileCount < total)
                {
                    InvokeRepeating("CreateChunk", 0, speeding);
                }
                else
                {
                    cancelFlag = true;
                }


            });
            cancelFlag = true;
        }
    }

    void nextPos()
    {
        /*//Instantiate(smallCube, _endPosition, Quaternion.identity);
        current = _endPosition;
        _endPosition = new Vector3(current.x + 2, current.y, current.z);
        transform.position = _endPosition;
        //Instantiate(smallCube, _endPosition, Quaternion.identity);

        var somePooledObject = UnityTutorialPool.current.GetPooledObject();
        somePooledObject.transform.position = _endPosition;
        somePooledObject.SetActive(true);
        tileCount++;*/
    }

    /*
    void nextPos()
    {
        //Instantiate(smallCube, _endPosition, Quaternion.identity);
        current = _endPosition;
        _endPosition = new Vector3(current.x + 2, current.y, current.z);
        transform.position = _endPosition;
        Instantiate(smallCube, _endPosition, Quaternion.identity);
        tileCount++;
    }*/






}




/*public NewObjectPoolerScript unitytutorialgameobjectpool;
internal NewObjectPoolerScript Unitytutorialgameobjectpool
{
    get
    {
        if (unitytutorialgameobjectpool == null)
        {
            unitytutorialgameobjectpool = new NewObjectPoolerScript();
        }
        return unitytutorialgameobjectpool;
    }
}*/














/*
int swtchXi = 0;
int swtchYi = 0;
int swtchZi = 0;


int Xi = -ChunkWidth_L;
int Yi = -ChunkHeight_L;
int Zi = -ChunkDepth_L;

for (int m = 0; m < 1; m++) // leaving it at 1 so that i ask myself the question wtf later. ive coded this already. im not doing it again. i need to find where i put it. peace of shit code. for brain grinders only
{
}
blockers = new mainChunkFinal[_max];

//blockers = new mainChunkFinal[(planetwidth * planetheight * planetdepth) + (planetwidth * planetheight * planetdepth)];
//blockers = new mainChunkFinal[(planetwidth + planetwidth) * (planetheight + planetheight) * (planetdepth + planetdepth)];

Vector3 center = Vector3.zero;

for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x += 4)
{
    for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y += 4)
    {
        for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z += 4)
        {
            float posX = (x);
            float posY = (y);
            float posZ = (z);

            Vector3 planetchunkpos = new Vector3(posX, posY, posZ);

            var xx = x;
            var yy = y;
            var zz = z;

            if (xx < 0)
            {
                xx *= -1;
                xx = (ChunkWidth_R) + xx;
            }
            if (yy < 0)
            {
                yy *= -1;
                yy = (ChunkHeight_R) + yy;
            }
            if (zz < 0)
            {
                zz *= -1;
                zz = (ChunkDepth_R) + zz;
            }

            int index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

            Transform yo = Instantiate(cube, planetchunkpos, Quaternion.identity);

            yo.transform.parent = transform;

            if (yo.GetComponent<sccsplanetchunkGen2>() != null)
            {
                //Debug.Log("!null");
                yo.GetComponent<sccsplanetchunkGen2>().buildchunkmap();
                blockers[index] = new mainChunkFinal(planetchunkpos, yo.gameObject);
            }
            else
            {
                //Debug.Log("null");
            }
            blockers[index].planetchunk.GetComponent<sccsplanetchunkGen2>().buildchunkmap();
            yield return waitforseconds;
        }
    }
}

// yield return waitforseconds;

for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x += 4)
{
    for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y += 4)
    {
        for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z += 4)
        {
            var xx = x;
            var yy = y;
            var zz = z;

            if (xx < 0)
            {
                xx *= -1;
                xx = (ChunkWidth_R) + xx;
            }
            if (yy < 0)
            {
                yy *= -1;
                yy = (ChunkHeight_R) + yy;
            }
            if (zz < 0)
            {
                zz *= -1;
                zz = (ChunkDepth_R) + zz;
            }

            int index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);


            blockers[index].planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
            blockers[index].planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();
            //yield return new WaitForSeconds(0);
            yield return waitforseconds;
        }
    }
}


try
{
}
catch (Exception ex)
{
    Debug.Log(ex.ToString());
}*/




/*_max = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);

int swtchXi = 0;
int swtchYi = 0;
int swtchZi = 0;


int Xi = -ChunkWidth_L;
int Yi = -ChunkHeight_L;
int Zi = -ChunkDepth_L;


for (int t = 0; t < _max; t++)
{
    var xx = Xi;
    var yy = Yi;
    var zz = Zi;

    if (xx < 0)
    {
        xx *= -1;
        xx = (ChunkWidth_R) + xx;
    }
    if (yy < 0)
    {
        yy *= -1;
        yy = (ChunkHeight_R) + yy;
    }
    if (zz < 0)
    {
        zz *= -1;
        zz = (ChunkDepth_R) + zz;
    }

    int index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);


    Debug.Log("index:" + index);



    if (swtchZi == 0)
    {
        if (Zi <= ChunkDepth_R)
        {
            Zi += realplanetwidth;
        }

        if (Zi > ChunkDepth_R) // prob an else if here instead
        {
            Zi = 0;
            swtchZi = 1;
            swtchYi = 1;
        }
    }
    if (swtchYi == 1)
    {
        if (Yi <= ChunkHeight_R)
        {
            Yi += realplanetwidth;
            swtchYi = 0;
            swtchZi = 0;
        }

        if (Yi > ChunkHeight_R) // prob an else if here instead
        {
            Yi = 0;
            swtchYi = 0;
            swtchXi = 1;
        }
    }

    if (swtchXi == 1)
    {
        if (Xi <= ChunkWidth_R)
        {
            Xi += realplanetwidth;
            swtchXi = 0;
            swtchYi = 0;
            swtchZi = 0;
        }

        if (Xi > ChunkWidth_R) // prob an else if here instead
        {
            swtchYi = 0;
            swtchXi = 1;
        }
    }
}*/





/*
void Fire()
{
GameObject obj = NewObjectPoolerScript.current.GetPooledObject();

if (obj == null) return;

obj.transform.position = transform.position;
obj.transform.rotation = transform.rotation;
obj.SetActive(true);

/*GetComponent<shadowBullet>().bullseyedirection = gunEnd.transform.forward;
GetComponent<shadowBullet>().firing_ship = player.gameObject;
GetComponent<shadowBullet>().gunEnd = gunEnd.gameObject;
GetComponent<shadowBullet>().enabled = true;
GetComponent<shadowBullet>().shadowObject = obj;// this.transform.FindChild("shadowbullet").gameObject;
}*/






/*var xx = Xi;
var yy = Yi;
var zz = Zi;

if (xx < 0)
{
    xx *= -1;
    xx = (ChunkWidth_R) + xx;
}
if (yy < 0)
{
    yy *= -1;
    yy = (ChunkHeight_R) + yy;
}
if (zz < 0)
{
    zz *= -1;
    zz = (ChunkDepth_R) + zz;
}

int index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);


if (swtchZi == 0)
{
    if (Zi <= ChunkDepth_R)
    {
        Zi+=4;
    }

    if (Zi > ChunkDepth_R) // prob an else if here instead
    {
        Zi = 0;
        swtchZi = 1;
        swtchYi = 1;
    }
}
if (swtchYi == 1)
{
    if (Yi <= ChunkHeight_R)
    {
        Yi += 4;
        swtchYi = 0;
        swtchZi = 0;
    }

    if (Yi > ChunkHeight_R) // prob an else if here instead
    {
        Yi = 0;
        swtchYi = 0;
        swtchXi = 1;
    }
}*/
/*if (swtchXi == 1)
{
    if (Xi <= ChunkWidth_R)
    {
        Xi += 4;
        swtchXi = 0;
        swtchYi = 0;
        swtchZi = 0;       
    }

    if (Xi > ChunkWidth_R) // prob an else if here instead
    {
        swtchYi = 0;
        swtchXi = 1;
    }
}*/






/*if (swtchXi == 0)
{
    if(Xi<= ChunkWidth_R)
    {
        Xi+=4;
    }

    if (Xi > ChunkWidth_R)
    {
        Xi = 0;
        swtchXi = 1;
        swtchYi = 1;
    }
}
if (swtchYi == 1)
{
    if (Yi <= ChunkHeight_R)
    {
        Yi+=4;
    }

    if (Yi > ChunkHeight_R)
    {
        Yi = 0;
        swtchYi = 2;
        swtchZi = 1;
    }
}

if (swtchZi == 1)
{
    if (Zi <= ChunkDepth_R)
    {
        Zi+=4;
    }

    if (Zi > ChunkDepth_R)
    {
        Zi = 0;
        swtchZi = 0;
        swtchXi = 0;
    }
}*/

//Debug.Log("max: " + _max + " x: " + Xi + " y: " + Yi + " z: " + Zi);





























//yield return waitforseconds;


/*
for (int x = -planetwidth; x < planetwidth; x += 4)
{
    for (int y = -planetheight; y < planetheight; y += 4)
    {
        for (int z = -planetdepth; z < planetdepth; z += 4)
        {
            Vector3 position = new Vector3(x, y, z);
            Transform yo = Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);

            yo.transform.parent = transform;

            if (x < 0)
            {
                x *= -1;
                x = (planetwidth - 1) + x;
            }
            if (y < 0)
            {
                y *= -1;
                y = (planetheight - 1) + y;
            }
            if (z < 0)
            {
                z *= -1;
                z = (planetdepth - 1) + z;
            }

            int index = x + (planetwidth + planetwidth) * (y + (planetheight + planetheight) * z);



            blockers[index] = new mainChunkFinal(new Vector3(x, y, z), yo.gameObject);
        }
    }
}*/




/*
if (framecounterForCreateEmptyObjectFaces >= framecounterForCreateEmptyObjectFacesMax)
{
    for (int i = 0; i < total; i++)
    {
        if (counterCreateEmptyObjects < total)
        {
            InvokeRepeating("CreateEmptyObjects", 0, speeding);

        }
        else
        {
            t = 0;
            xx = -ChunkWidth_L;
            yy = -ChunkHeight_L;
            zz = -ChunkDepth_L;
            swtchx = 0;
            swtchy = 0;
            startOnceSwtc = 1;
            break;
        }
    }

    framecounterForCreateEmptyObjectFaces = 0;
}
framecounterForCreateEmptyObjectFaces++;*/



//CreateClassOfChunkGameObject();
//if (taskcancelFlagTwo == 0)
//{
//    goto _thread_loop_console;
//}
//CreateChunkFaces();
//InvokeRepeating("CreateChunkGameObjects", 0, speeding);
//iSCentralDispatch.DispatchMainThread(() =>
//{                       
//InvokeRepeating("CreateChunkGameObjects", 0, speeding);
//});
//CreateChunkObject();







/*
if (startOnceSwtc == 2)
{

    for (int i = 0; i < 1; i++)
    {
        if (counterCreateChunkObjectByteMap < total)
        {
            InvokeRepeating("CreateChunkObjectByteMap", 0, 0.005f);
        }
        else
        {
            Debug.Log("ended CreateChunkObjectByteMap");
            t = 0;
            xx = -ChunkWidth_L;
            yy = -ChunkHeight_L;
            zz = -ChunkDepth_L;
            swtchx = 0;
            swtchy = 0;
            startOnceSwtc = 3;
            break;
        }
    }
}*/

/*
if (startOnceSwtc == 4)
{
    //Debug.Log("started CreateChunkFaces");


    for (int i = 0; i < 1; i++)
    {

        if (counterCreateChunkObjectFaces < total)
        {
            InvokeRepeating("CreateChunkFaces", 0, 0.005f);
            Debug.Log("trying to build face");
        }
        else
        {
            Debug.Log("ended CreateChunkFaces");
            t = 0;
            xx = -ChunkWidth_L;
            yy = -ChunkHeight_L;
            zz = -ChunkDepth_L;
            swtchx = 0;
            swtchy = 0;
            startOnceSwtc = 5;
            break;
        }

    }
}*/


/*
if (startOnceSwtc == 3)
{
    if (counterCreateChunkObjectFaces < total)
    {
        InvokeRepeating("CreateChunkFaces", 3, 0.05f);
    }
    else
    {
        CancelInvoke();
        t = 0;
        xx = -ChunkWidth_L;
        yy = -ChunkHeight_L;
        zz = -ChunkDepth_L;
        swtchx = 0;
        swtchy = 0;
        startOnceSwtc = 4;
    }
}*/



/*
if (startOnceSwtc == 2)
{
    if (framecounterForCreateChunkObjectFaces >= framecounterForCreateChunkObjectFacesMax)
    {

        if (counterCreateChunkObjectFaces < total)
        {
            //Debug.Log("test");
            CreateChunkFaces();

        }
        else
        {
            t = 0;
            xx = -ChunkWidth_L;
            yy = -ChunkHeight_L;
            zz = -ChunkDepth_L;
            swtchx = 0;
            swtchy = 0;
            startOnceSwtc = 3;
        }

        framecounterForCreateChunkObjectFaces = 0;
    }
    framecounterForCreateChunkObjectFaces++;
}*/


/*
if (startOnceSwtc == 0)
{
    t = 0;
    xx = -ChunkWidth_L;
    yy = -ChunkHeight_L;
    zz = -ChunkDepth_L;
    swtchx = 0;
    swtchy = 0;
    startOnceSwtc = 1;
}

if (startOnceSwtc == 1)
{
    if (counterCreateChunkObjectFaces < total)
    {
        CreateChunkFaces();
        counterForCreateFaces++;
    }
    else
    {
        startOnceSwtc = 2;
    }
}*/






/*
if (UnityTutorialPool.current != null)
{
    Debug.Log("UnityTutorialPool.current != null");
}
if (UnityTutorialPool.current.pooledObjects.Count > 0)
{
   Debug.Log("pool is growing");
}



if (UnityTutorialPool.current.pooledObjects.Count > 1000)
{
    //Debug.Log("pool is growing");
    if (startOnceSwtc == 0)
    {

        //Debug.Log("pool is growing");

        for (int i = 0; i < total; i++)
        {
            CreateChunk();
        }

        t = 0;
        xx = -ChunkWidth_L;
        yy = -ChunkHeight_L;
        zz = -ChunkDepth_L;

        swtchx = 0;
        swtchy = 0;

        someLoopBreaker = 0;
        for (int i = 0; i < total; i++)
        {
            CreateChunkFaces();
        }


        startOnceSwtc = 1;
        //Debug.Log("total:" + t);


    }
}
else
{
         //Debug.Log("pool is growing");
}*/





//Debug.Log("found neighbour chunk");
//mainChunkFinal currentChunk = somemsg[0].somechunkarray[index];// this.getChunk(neighboorindexx, neighboorindexy, neighboorindexz);// posnotroundedx, posnotroundedy, posnotroundedz);
/*int indexx = 0;
int indexy = 0;
int indexz = 0;

if (realplanetwidth == 1)
{
    indexx = Mathf.FloorToInt((rayFrameInitPos0.x - Mathf.FloorToInt(rayFrameInitPos0.x)) * 10); //   1.5 - 1.0 = 0.5
    //int somechunkworldindexx = somechunkdivx * 10; //11 * 2 = 22
    //indexx = (Mathf.FloorToInt((rayFrameInitPos0.x - somechunkworldindexx) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15

    indexy = Mathf.FloorToInt((rayFrameInitPos0.y - Mathf.FloorToInt(rayFrameInitPos0.y)) * 10); //   1.5 - 1.0 = 0.5
    //int somechunkworldindexy = somechunkdivy * realplanetwidth; //11 * 2 = 22
    //indexy = (Mathf.FloorToInt((rayFrameInitPos0.y - somechunkworldindexy) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15

    indexz = Mathf.FloorToInt((rayFrameInitPos0.z - Mathf.FloorToInt(rayFrameInitPos0.z)) * 10); //   0.5-0 = 0.5*10 = 5
    //int somechunkworldindexz = somechunkdivz * realplanetwidth; //11 * 2 = 22
    //indexz = (Mathf.FloorToInt((rayFrameInitPos0.z - somechunkworldindexz) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15


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
}
else
{
    int somechunkdivx = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.x) / realplanetwidth); //   23.0f/2 = 11
    int somechunkworldindexx = somechunkdivx * realplanetwidth; //11 * 2 = 22
    indexx = (Mathf.FloorToInt((rayFrameInitPos0.x - somechunkworldindexx) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15

    int somechunkdivy = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.y) / realplanetwidth); //   23.0f/2 = 11
    int somechunkworldindexy = somechunkdivy * realplanetwidth; //11 * 2 = 22
    indexy = (Mathf.FloorToInt((rayFrameInitPos0.y - somechunkworldindexy) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15

    int somechunkdivz = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.z) / realplanetwidth); //   23.0f/2 = 11
    int somechunkworldindexz = somechunkdivz * realplanetwidth; //11 * 2 = 22
    indexz = (Mathf.FloorToInt((rayFrameInitPos0.z - somechunkworldindexz) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15

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
}*/





/*float somex = 0;
float somey = 0;
float somez = 0;

if (rayFrameInitPos0.x < 0)
{
    somex = Mathf.Floor(rayFrameInitPos0.x);
}
else
{
    somex = Mathf.Floor(rayFrameInitPos0.x);
}

if (rayFrameInitPos0.y < 0)
{
    somey = Mathf.Floor(rayFrameInitPos0.y);
}
else
{
    somey = Mathf.Floor(rayFrameInitPos0.y);
}


if (rayFrameInitPos0.z < 0)
{
    somez = Mathf.Floor(rayFrameInitPos0.z);
}
else
{
    somez = Mathf.Floor(rayFrameInitPos0.z);
}*/


/*

//indexx = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
//indexy = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
//indexz = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));

float somex = Mathf.Floor(rayFrameInitPos0.x * 10) / 10;
float somey = Mathf.Floor(rayFrameInitPos0.y * 10) / 10;
float somez = Mathf.Floor(rayFrameInitPos0.z * 10) / 10;

Vector3 p = new Vector3(somex, somey, somez);

float posx = (p.x);
float posy = (p.y);
float posz = (p.z);

Vector3 chunkbytepos = new Vector3(posx, posy, posz);

//retAdd.position = chunkbytepos;



//indexx = Mathf.RoundToInt((Mathf.Round(posx * 10)));
//indexy = Mathf.RoundToInt((Mathf.Round(posy * 10)));
//indexz = Mathf.RoundToInt((Mathf.Round(posz * 10)));

//Debug.Log("posx:" + posx);

if (posx < 0)
{
    //indexx = Mathf.RoundToInt((Mathf.Round(posx * 10)));
    indexx = Mathf.RoundToInt(((((posx - Mathf.Floor(posx))) * 10)));

    //Debug.Log("01indexx:" + indexx);
    if (indexx == 0)
    {
        indexx = 0;
    }
    else
    {
        //Debug.Log("01indexx:" + indexx);

        if (indexx < 0)
        {
            //indexx *= -1;

            if (indexx >= (-(10 / 2) + 1) && indexx < 0)
            {
                //Debug.Log("2indexx:" + indexx);
                indexx *= -1;
                indexx = (10) - indexx;
            }
            else
            {

            }
        }
        else
        {

        }
    }
}
else
{
    //indexx = Mathf.RoundToInt((Mathf.Round(posx * 10)));
    indexx = Mathf.RoundToInt(((((posx - Mathf.Floor(posx))) * 10)));
    //Debug.Log("00indexx:" + indexx);
    if (indexx >= 0 && indexx < (10 / 2))
    {
        //Debug.Log("0");
        //indexx = ((10 / 2) - indexx);
    }
    else if (indexx >= (10 / 2))
    {
        //Debug.Log("1");
    }
    else if (indexx < 0 && indexx >= -(10 / 2))
    {
        //Debug.Log("2");
    }
    else if (indexx < -(10 / 2))
    {
        //Debug.Log("3");
    }
}






if (posy < 0)
{
    //indexy = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
    indexy = Mathf.RoundToInt(((((posy - Mathf.Floor(posy))) * 10)));

    //Debug.Log("01indexy:" + indexy);
    if (indexy == 0)
    {
        indexy = 0;
    }
    else
    {
        //Debug.Log("01indexy:" + indexy);

        if (indexy < 0)
        {
            //indexy *= -1;

            if (indexy >= (-(10 / 2) + 1) && indexy < 0)
            {
                //Debug.Log("2indexy:" + indexy);
                indexy *= -1;
                indexy = (10) - indexy;
            }
            else
            {

            }
        }
        else
        {

        }
    }
}
else
{
    //indexy = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
    indexy = Mathf.RoundToInt(((((posy - Mathf.Floor(posy))) * 10)));
    //Debug.Log("00indexy:" + indexy);
    if (indexy >= 0 && indexy < (10 / 2))
    {
        //Debug.Log("0");
        //indexy = ((chunkWidth / 2) - indexy);
    }
    else if (indexy >= (10 / 2))
    {
        //Debug.Log("1");
    }
    else if (indexy < 0 && indexy >= -(10 / 2))
    {
        //Debug.Log("2");
    }
    else if (indexy < -(10 / 2))
    {
        //Debug.Log("3");
    }
}




//Debug.Log("posz:" + posz);
if (posz < 0)
{
    indexz = Mathf.RoundToInt(((((posz - Mathf.Floor(posz))) * 10)));

    //Debug.Log("01indexz:" + indexz);
    if (indexz == 0)
    {
        indexz = 0;
    }
    else
    {
        //Debug.Log("01indexz:" + indexz);

        if (indexz < 0)
        {
            //indexz *= -1;

            if (indexz >= (-(10 / 2) + 1) && indexz < 0)
            {
                //Debug.Log("2indexz:" + indexz);
                indexz *= -1;
                indexz = (10) - indexz;
            }
            else
            {
                //Debug.Log("3indexz:" + indexz);


                if (indexz == 0)
                {
                    indexz = 0;
                }
                //else if (indexz == (chunkWidth - 1))
                //{
                //    indexz = (chunkWidth - 1) - indexz;
                //}
                //else
                //{
                //    indexz *= -1;
                //    indexz = (chunkWidth - 1) - indexz;
                //}
            }
        }
        else
        {

        }
    }
}
else
{
    //indexz = Mathf.FloorToInt((Mathf.Floor(posz * chunkWidth)));
    indexz = Mathf.RoundToInt(((((posz - Mathf.Floor(posz))) * 10)));

    //Debug.Log("00indexz:" + indexz);
    if (indexz >= 0 && indexz < (10 / 2))
    {
        //Debug.Log("0");
        //indexz = ((chunkWidth / 2) - indexz);
    }
    else if (indexz >= (10 / 2))
    {
        //Debug.Log("1");
    }
    else if (indexz < 0 && indexz >= -(10 / 2))
    {
        //Debug.Log("2");
    }
    else if (indexz < -(10 / 2))
    {
        //Debug.Log("3");
    }
}*/
//Vector3 chunkbytepos = new Vector3(indexx, indexy, indexz);
































//retAdd.position = rayFrameInitPos0;

//////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
//Debug.Log("indexx: " + (indexx) + " indexy: " + (indexy) + " indexz: " + (indexz));


//_main_received_messages[0].chunkByteMaps[i]


//blockers[index].arrayOfChunkMapOfBytes = _main_received_messages[0].chunkByteMaps[index];
//somemsg[0].chunkByteMaps[someindex]


/*if (somemsg[0].somechunkarray[indexmain].vertexlist.Count > 0)
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
    }
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
}*/






//Debug.Log("checking for neighbour chunk");



//Debug.Log("found neighbour chunk");
//mainChunkFinal currentChunk = somemsg[0].somechunkarray[index];// this.getChunk(neighboorindexx, neighboorindexy, neighboorindexz);// posnotroundedx, posnotroundedy, posnotroundedz);
/*int indexx = 0;
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
}*/
/*
float somex = Mathf.Floor(rayFrameInitPos0.x * 10) / 10;
float somey = Mathf.Floor(rayFrameInitPos0.y * 10) / 10;
float somez = Mathf.Floor(rayFrameInitPos0.z * 10) / 10;

Vector3 p = new Vector3(somex, somey, somez);

float posx = (p.x);
float posy = (p.y);
float posz = (p.z);

//Vector3 chunkbytepos = new Vector3(posx, posy, posz);

//retAdd.position = chunkbytepos;

int indexx = 0;
int indexy = 0;
int indexz = 0;

//indexx = Mathf.RoundToInt((Mathf.Round(posx * 10)));
//indexy = Mathf.RoundToInt((Mathf.Round(posy * 10)));
//indexz = Mathf.RoundToInt((Mathf.Round(posz * 10)));

//Debug.Log("posx:" + posx);

if (posx < 0)
{
    //indexx = Mathf.RoundToInt((Mathf.Round(posx * 10)));
    indexx = Mathf.RoundToInt(((((posx - Mathf.Floor(posx))) * 10)));

    //Debug.Log("01indexx:" + indexx);
    if (indexx == 0)
    {
        indexx = 0;
    }
    else
    {
        //Debug.Log("01indexx:" + indexx);

        if (indexx < 0)
        {
            //indexx *= -1;

            if (indexx >= (-(10 / 2) + 1) && indexx < 0)
            {
                //Debug.Log("2indexx:" + indexx);
                indexx *= -1;
                indexx = (10) - indexx;
            }
            else
            {

            }
        }
        else
        {

        }
    }
}
else
{
    //indexx = Mathf.RoundToInt((Mathf.Round(posx * 10)));
    indexx = Mathf.RoundToInt(((((posx - Mathf.Floor(posx))) * 10)));
    //Debug.Log("00indexx:" + indexx);
    if (indexx >= 0 && indexx < (10 / 2))
    {
        //Debug.Log("0");
        //indexx = ((10 / 2) - indexx);
    }
    else if (indexx >= (10 / 2))
    {
        //Debug.Log("1");
    }
    else if (indexx < 0 && indexx >= -(10 / 2))
    {
        //Debug.Log("2");
    }
    else if (indexx < -(10 / 2))
    {
        //Debug.Log("3");
    }
}






if (posy < 0)
{
    //indexy = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
    indexy = Mathf.RoundToInt(((((posy - Mathf.Floor(posy))) * 10)));

    //Debug.Log("01indexy:" + indexy);
    if (indexy == 0)
    {
        indexy = 0;
    }
    else
    {
        //Debug.Log("01indexy:" + indexy);

        if (indexy < 0)
        {
            //indexy *= -1;

            if (indexy >= (-(10 / 2) + 1) && indexy < 0)
            {
                //Debug.Log("2indexy:" + indexy);
                indexy *= -1;
                indexy = (10) - indexy;
            }
            else
            {

            }
        }
        else
        {

        }
    }
}
else
{
    //indexy = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
    indexy = Mathf.RoundToInt(((((posy - Mathf.Floor(posy))) * 10)));
    //Debug.Log("00indexy:" + indexy);
    if (indexy >= 0 && indexy < (10 / 2))
    {
        //Debug.Log("0");
        //indexy = ((chunkWidth / 2) - indexy);
    }
    else if (indexy >= (10 / 2))
    {
        //Debug.Log("1");
    }
    else if (indexy < 0 && indexy >= -(10 / 2))
    {
        //Debug.Log("2");
    }
    else if (indexy < -(10 / 2))
    {
        //Debug.Log("3");
    }
}




//Debug.Log("posz:" + posz);
if (posz < 0)
{
    indexz = Mathf.RoundToInt(((((posz - Mathf.Floor(posz))) * 10)));

    //Debug.Log("01indexz:" + indexz);
    if (indexz == 0)
    {
        indexz = 0;
    }
    else
    {
        //Debug.Log("01indexz:" + indexz);

        if (indexz < 0)
        {
            //indexz *= -1;

            if (indexz >= (-(10 / 2) + 1) && indexz < 0)
            {
                //Debug.Log("2indexz:" + indexz);
                indexz *= -1;
                indexz = (10) - indexz;
            }
            else
            {
                //Debug.Log("3indexz:" + indexz);


                if (indexz == 0)
                {
                    indexz = 0;
                }
                //else if (indexz == (chunkWidth - 1))
                //{
                //    indexz = (chunkWidth - 1) - indexz;
                //}
                //else
                //{
                //    indexz *= -1;
                //    indexz = (chunkWidth - 1) - indexz;
                //}
            }
        }
        else
        {

        }
    }
}
else
{
    //indexz = Mathf.FloorToInt((Mathf.Floor(posz * chunkWidth)));
    indexz = Mathf.RoundToInt(((((posz - Mathf.Floor(posz))) * 10)));

    //Debug.Log("00indexz:" + indexz);
    if (indexz >= 0 && indexz < (10 / 2))
    {
        //Debug.Log("0");
        //indexz = ((chunkWidth / 2) - indexz);
    }
    else if (indexz >= (10 / 2))
    {
        //Debug.Log("1");
    }
    else if (indexz < 0 && indexz >= -(10 / 2))
    {
        //Debug.Log("2");
    }
    else if (indexz < -(10 / 2))
    {
        //Debug.Log("3");
    }
}*/


