using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using SimplexNoise;


public class sccsproceduralplanetbuilderGen2 : MonoBehaviour
{
    public static sccsproceduralplanetbuilderGen2 sccsproceduralplanetbuilderGen2staticscriptlock;


    int posx = 0;
    int posy = 0;
    int posz = 0;


    int swtchx = 0;
    int swtchy = 0;
    int swtchz = 0;

    int index = 0;


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
    static mainChunkFinal[] blockers;
    //static byte[][] chunkByteMaps;
    //byte block;
    public int realplanetwidth = 4;
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
    public GameObject hingerelease;
    bool cancelFlag = false;

    int _max = 0;
    Vector3 _endPosition;
    Vector3 _startPosition;
    Vector3 direction = new Vector3(1, 0, 0);
    Vector3 nextPosition;
    Vector3 current;
    int tileCount = 0;


    private void OnDisable()
    {
        CancelInvoke();
    }


    float delayOrTime = 2.5f;
    float repeatrate = 2.5f;
    int total = -1;

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

    object[] someobjArray;
    private void Start()
    {
        someobjArray = new object[1];
        pooledObjects = new List<GameObject>();

        total = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);

        pooledAmount = Mathf.FloorToInt(total * 0.25f); //* 0.25f

        blockers = new mainChunkFinal[total];
        //chunkByteMaps = new byte[total][];


        /*for (int i = 0;i < total;i++)
        {
            chunkByteMaps[i] = new byte[16*16*16];
        }*/

        waitforseconds = new WaitForSeconds(0);

        _main_received_messages = new somemessage[1];
        _main_received_messages[0].testswtc = 0;

        _main_received_messages[0].chunkByteMaps = new int[total][];


        for (int i = 0; i < total; i++)
        {
            _main_received_messages[0].chunkByteMaps[i] = new int[16 * 16 * 16];
        }
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


    public struct somemessage
    {
        public int testswtc;
        public int[][] chunkByteMaps;
        public int currentIndex;
    }

    static somemessage[] _main_received_messages;//
    Task sccssometask;

    int taskcancelFlagTwo = 0;

    private void Update()
    {
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
                        InvokeRepeating("CreateEmptyObjects", 0, 0.005f);
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
                        InvokeRepeating("CreateClassOfChunkGameObject", 0, 0.01f);
                    }
                    else
                    {
                        CancelInvoke();
                        Debug.Log("ended CreateClassOfChunkGameObject");
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






            //_main_received_messages[0].testswtc = -1;
            someobjArray[0] = _main_received_messages;

            if (startOnceSwtc == 2)
            {
                sccssometask = Task<object[]>.Factory.StartNew((tester0001) =>
                {
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
                    //somechunkBy

                    //////CONSOLE WRITER=>
                    //_thread_loop_console:
                    sometempstruct somestruct = new sometempstruct();

                    somestruct.someindex = 0;
                    somestruct.somemap = new int[16 * 16 * 16];

                    int[][] somestructTwo = new int[total][];

                    //for (int i = 0; i < total; i++)
                    //{
                    //    somestructTwo[i] = 0;
                    //    //somestructTwo[i].someindex = 0;
                    //    //somestructTwo[i].somemap = new byte[16 * 16 * 16];
                    //}

                    int someCounter = 0;
                    while (taskcancelFlagTwo == 0)
                    {
                        somemessage[] somemsg = (somemessage[])someobjArray[0];
                        taskcancelFlagTwo = somemsg[0].testswtc;




                        if (counterswtchForTask == 0)
                        {
                            if (t0 < total)
                            {
                                posx0 = (xx0);
                                posy0 = (yy0);
                                posz0 = (zz0);

                                //planetchunkpos = new Vector3(posx * realplanetwidth, posy * realplanetwidth, posz * realplanetwidth);
                                planetchunkpos = new Vector3(posx0, posy0, posz0);

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

                                index = xi0 + (ChunkWidth_L + ChunkWidth_R + 1) * (yi0 + (ChunkHeight_L + ChunkHeight_R + 1) * zi0);

                                if (index < total)
                                {
                                    somemsg[0].chunkByteMaps[index] = buildchunkmap(planetchunkpos);
                                    somemsg[0].currentIndex = index;
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
                                    counterswtchForTask = 1;
                                }
                                t0++;
                            }

                            //Debug.Log("total:" + total + "/t:" + t);
                        }

                        /*
                        if (t < total)
                        {
                            posX = (xx);
                            posY = (yy);
                            posZ = (zz);

                            planetchunkpos = new Vector3(posX, posY, posZ);

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

                            index = xi + (ChunkWidth_L + ChunkWidth_R + 1) * (yi + (ChunkHeight_L + ChunkHeight_R + 1) * zi);

                            if (index < total)
                            {
                                //Transform yo = Instantiate(cube, planetchunkpos, Quaternion.identity);

                                somemsg[0].chunkByteMaps[index] = buildchunkmap(planetchunkpos);
                                somemsg[0].currentIndex = index;
                            }
                            else
                            {

                            }

                            zz++;//  realplanetwidth;
                            if (zz >= (ChunkDepth_R))
                            {
                                yy++;// realplanetwidth;
                                zz = -ChunkDepth_L;
                                swtchy = 1;
                            }
                            if (yy >= (ChunkHeight_R) && swtchy == 1)
                            {
                                xx++;// realplanetwidth;
                                yy = -ChunkHeight_L;
                                swtchy = 0;
                                swtchx = 1;
                            }
                            if (xx >= (ChunkWidth_R) && swtchx == 1)
                            {
                                xx = -ChunkWidth_L;
                                swtchx = 0;
                            }
                            t++;
                            counterCreateChunkObjectByteMap++;
                        }
                        else
                        {

                        }*/
                        //somemsg[0].chunkByteMaps = somestructTwo;

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

            //Debug.Log("swtc:" + _main_received_messages[0].testswtc);


            // checking that the task has successfully ended the job by checking if it looped through from zero to the total. The task can be idle if there is no need to calculate any bytes and reactivated for chunks breaking
            if (_main_received_messages[0].currentIndex >= total)
            {
                //Debug.Log("0pasting arrays");

                if (startOnceSwtc == 3)
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
                        if (blockers[i] != null)
                        {
                            if (blockers[i].planetchunk != null)
                            {
                                if (blockers[i].somesccsplanetchunkFinal != null)
                                {
                                    blockers[i].somesccsplanetchunkFinal.map = _main_received_messages[0].chunkByteMaps[i];
                                }
                            }
                        }
                    }
                    Debug.Log("pasted arrays");
                    _main_received_messages[0].testswtc = 1;

                    startOnceSwtc = 4;
                }
            }


            if (startOnceSwtc == 4)
            {
                Debug.Log("started CreateChunkFaces");


                for (int i = 0; i < 1; i++)
                {

                    if (swtchz == 0)
                    {
                        // CreateChunkFaces();

                        InvokeRepeating("CreateChunkFaces", 0, 0.05f);
                        //Debug.Log("trying to build face");
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
                        swtchz = 0;
                        startOnceSwtc = 5;
                        break;
                    }
                }
            }
        }
    }

    int lastFrameCreateChunk = 0;
    int lastFrameCreateChunkFaces = 0;
    float posX = 0;
    float posY = 0;
    float posZ = 0;
    int xi = 0;
    int yi = 0;
    int zi = 0;
    Vector3 planetchunkpos;
    int t = 0;
    int xx = 0;
    int yy = 0;
    int zz = 0;

    int someLoopBreaker = 0;



    int counterCreateChunkObjectByteMap = 0;
    int counterCreateChunkObjectByteMapMax = 0;
    int counterCreateChunkObjectFaces = 0;
    int counterCreateChunkObjectFacesMax = 0;

    int counterCreateChunkObjectClass = 0;
    int counterCreateChunkObjectClassSwtch = 0;
    int counterCreateChunkObjectClassMax = 0;

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

                //planetchunkpos = new Vector3(posx * realplanetwidth, posy * realplanetwidth, posz * realplanetwidth);
                planetchunkpos = new Vector3(posx, posy, posz);


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

                index = xi + (ChunkWidth_L + ChunkWidth_R + 1) * (yi + (ChunkHeight_L + ChunkHeight_R + 1) * zi);

                if (index < total)
                {
                    var somePooledObject = GetPooledObject();

                    if (somePooledObject != null)
                    {
                        somePooledObject.transform.parent = this.transform;
                        somePooledObject.transform.position = planetchunkpos;
                        somePooledObject.SetActive(true);
                        blockers[index] = new mainChunkFinal(planetchunkpos, somePooledObject.gameObject, somePooledObject.GetComponent<sccslodchunkfinal>(), index, this);

                        blockers[index].somesccsplanetchunkFinal.sccsCustomStart(this);



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


    public struct sometempstruct
    {
        public int someindex;
        public int[] somemap;
    }


    int CreateChunkObjectByteMapIndex = 0;

    sometempstruct CreateChunkObjectByteMap(sometempstruct sometempstruct_)
    {
        //Debug.Log("test");
        //unroll
        //[loop]
        //in order to not use those in hlsl because i never completely understood how they worked then, i decided to learn how to build flat loops (per frame or not). i upgraded this today working a bit on it to a flat loop 
        //with negative and positive indexes so that it works as it was with my negative/positive planets chunks. by steve chassé aka ninekorn 2021-mai-08th.


        if (t < total)
        {
            posX = (xx);
            posY = (yy);
            posZ = (zz);

            planetchunkpos = new Vector3(posX, posY, posZ);

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

            index = xi + (ChunkWidth_L + ChunkWidth_R + 1) * (yi + (ChunkHeight_L + ChunkHeight_R + 1) * zi);

            if (index < total)
            {
                //Transform yo = Instantiate(cube, planetchunkpos, Quaternion.identity);

                sometempstruct_.somemap = buildchunkmap(planetchunkpos);
                sometempstruct_.someindex = index;
            }
            else
            {

            }

            zz++;//  realplanetwidth;
            if (zz >= (ChunkDepth_R))
            {
                yy++;// realplanetwidth;
                zz = -ChunkDepth_L;
                swtchy = 1;
            }
            if (yy >= (ChunkHeight_R) && swtchy == 1)
            {
                xx++;// realplanetwidth;
                yy = -ChunkHeight_L;
                swtchy = 0;
                swtchx = 1;
            }
            if (xx >= (ChunkWidth_R) && swtchx == 1)
            {
                xx = -ChunkWidth_L;
                swtchx = 0;
            }
            t++;
            counterCreateChunkObjectByteMap++;
        }
        return sometempstruct_;
    }


    void CreateChunkFaces()
    {


        if (swtchz == 0)
        {
            if (t < total)
            {
                posx = (xx);
                posy = (yy);
                posz = (zz);

                //planetchunkpos = new Vector3(posx * realplanetwidth, posy * realplanetwidth, posz * realplanetwidth);
                planetchunkpos = new Vector3(posx, posy, posz);


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

                index = xi + (ChunkWidth_L + ChunkWidth_R + 1) * (yi + (ChunkHeight_L + ChunkHeight_R + 1) * zi);

                if (index < total)
                {
                    if (blockers[index] != null)
                    {

                        if (blockers[index].planetchunk != null)
                        {
                            if (blockers[index].somesccsplanetchunkFinal != null)
                            {
                                blockers[index].somesccsplanetchunkFinal.sccsSetMap();
                                blockers[index].somesccsplanetchunkFinal.Regenerate();
                                blockers[index].somesccsplanetchunkFinal.chunkbuildingswtc = 1;



                                /*if (blockers[index].somesccsplanetchunkFinal.vertexlist.Count > 0)
                                {
                                    blockers[index].somesccsplanetchunkFinal.buildMesh();

                                }*/
                            }
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





























    // Update is called once per frame
    void ShootTheChunk() //FireShadowBullet
    {
        /*hingerelease = sccshingereleasepooler.current.GetPooledObject(); //this.transform.gameObject;// 

        if (hingerelease == null) return;

        hingerelease.transform.position = this.transform.position;// gunEnd.transform.position;// transform.position;
        hingerelease.transform.rotation = this.transform.rotation;//shadowProjectile.transform.rotation;// transform.rotation;
        hingerelease.transform.gameObject.SetActive(true);*/
    }



    WaitForSeconds waitforseconds;// = new WaitForSeconds();
    GameObject unityTutorialPooledGameObject;


    IEnumerator buildplanetchunk()
    {
        //unroll
        //[loop]
        //in order to not use those in hlsl because i never completely understood how they worked then, i decided to learn how to build flat loops (per frame or not). i upgraded this today working a bit on it to a flat loop 
        //with negative and positive indexes so that it works as it was with my negative/positive planets chunks. by steve chassé aka ninekorn 2021-mai-08th.



        int xx = -ChunkWidth_L;
        int yy = -ChunkHeight_L;
        int zz = -ChunkDepth_L;

        int swtchx = 0;
        int swtchy = 0;

        for (int t = 0; t < total; t++)
        {
            float posX = (xx);
            float posY = (yy);
            float posZ = (zz);

            Vector3 planetchunkpos = new Vector3(posX, posY, posZ);

            var xi = xx;
            var yi = yy;
            var zi = zz;

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

            int index = xi + (ChunkWidth_L + ChunkWidth_R + 1) * (yi + (ChunkHeight_L + ChunkHeight_R + 1) * zi);

            if (index < total)
            {
                //Transform yo = Instantiate(cube, planetchunkpos, Quaternion.identity);

                unityTutorialPooledGameObject = NewObjectPoolerScript.current.GetPooledObject();

                unityTutorialPooledGameObject.transform.parent = transform;

                if (unityTutorialPooledGameObject.GetComponent<sccslodchunkfinal>() != null)
                {
                    //Debug.Log("!null");

                    blockers[index] = new mainChunkFinal(planetchunkpos, unityTutorialPooledGameObject.gameObject, unityTutorialPooledGameObject.GetComponent<sccslodchunkfinal>(), index,this);
                    //blockers[index].somesccsplanetchunkFinal.buildchunkmap(planetchunkpos);
                    unityTutorialPooledGameObject.transform.position = planetchunkpos;
                    unityTutorialPooledGameObject.SetActive(true);
                }
                else
                {
                    //Debug.Log("null");
                }

            }
            else
            {
                break;
            }

            zz += realplanetwidth;
            if (zz >= (ChunkDepth_R))
            {
                yy += realplanetwidth;
                zz = -ChunkDepth_L;
                swtchy = 1;
            }
            if (yy >= (ChunkHeight_R) && swtchy == 1)
            {
                xx += realplanetwidth;
                yy = -ChunkHeight_L;
                swtchy = 0;
                swtchx = 1;
            }
            if (xx >= (ChunkWidth_R) && swtchx == 1)
            {
                xx = -ChunkWidth_L;
                swtchx = 0;
                break;
            }
        }


        xx = -ChunkWidth_L;
        yy = -ChunkHeight_L;
        zz = -ChunkDepth_L;

        swtchx = 0;
        swtchy = 0;
        swtchz = 0;

        for (int t = 0; t < total; t++)
        {
            float posX = (xx);
            float posY = (yy);
            float posZ = (zz);

            Vector3 planetchunkpos = new Vector3(posX, posY, posZ);

            var xi = xx;
            var yi = yy;
            var zi = zz;

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

            int index = xi + (ChunkWidth_L + ChunkWidth_R + 1) * (yi + (ChunkHeight_L + ChunkHeight_R + 1) * zi);

            if (index < total)
            {
                blockers[index].planetchunk.GetComponent<sccsplanetchunkGen2>().Regenerate();
                blockers[index].planetchunk.GetComponent<sccsplanetchunkGen2>().buildMesh();
            }
            else
            {
                break;
            }

            zz += realplanetwidth;
            if (zz >= (ChunkDepth_R))
            {
                yy += realplanetwidth;
                zz = -ChunkDepth_L;
                swtchy = 1;
            }
            if (yy >= (ChunkHeight_R) && swtchy == 1)
            {
                xx += realplanetwidth;
                yy = -ChunkHeight_L;
                swtchy = 0;
                swtchx = 1;
            }
            if (xx >= (ChunkWidth_R) && swtchx == 1)
            {
                xx = -ChunkWidth_L;
                swtchx = 0;
                break;
            }
        }

        yield return waitforseconds;
    }



    float seed;
    byte block;

    float nodeDiameter;
    float chunkRadius;
    float fraction;
    float chunkSize;

    public float planeSize = 0.25f;


    public int width = 16;
    public int height = 16;
    public int depth = 16;

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
                        //map[indexOf] = 1;

                        if (distance <= radiusplanetmountainend)
                        {
                            map[indexOf] = 1;
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



    public mainChunkFinal getChunk(int x, int y, int z)
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


    public mainChunkFinal getChunkmainChunkFinal(int x, int y, int z)
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

        return null;//blockers[index];

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

