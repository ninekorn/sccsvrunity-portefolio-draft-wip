using UnityEngine;
using System.Collections;
//using SPINACH.iSCentralDispatch;
using System.Collections.Generic;
using System.Threading;

public class Terrain22 : MonoBehaviour
{

    Thread t0;
    Thread t1;
    Thread t2;
    Thread t3;

    public static Terrain22 currentTerrain22;

    /*public Transform spawnPoint0;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;*/

    public GameObject emptyObject;



    //public Chunk7 chunkFabLowDetail;


    public GameObject chunkFabDetail2;
    public GameObject chunkFabDetail4;
    public GameObject chunkFabDetail6;
    public GameObject chunkFabDetail8;
    public GameObject chunkFabDetail10;
    public GameObject chunkFabDetail12;


    int threadID0 = 0;
    int threadID1 = -1;
    int threadID2 = -2;
    int threadID3 = -3;
    int threadID4 = -4;
    int threadID5 = -5;
    int threadID7 = -7;


    public int viewSize0 = 10;
    public int viewSize1 = 10;
    public int viewSize2 = 10;
    public int viewSize3 = 10;
    public int viewSize4 = 10;
    public int viewSize5 = 10;

    float planeSize = 0.1f; //125f

    public int chunkSizeVERYHIGHDETAIL = 10;
    public int chunkSizeLOWDETAIL = 10;
    public int chunkSizeLOWERDETAIL = 10;
    public int chunkSizeLOWESTDETAIL = 10;



    int counter999 = 0;
    public int counter777 = 0;
    int counter888 = 0;
    int counter111 = 1;
    int counter222 = 0;
    int counter333 = 0;
    int counter444 = 0;
    int counter555 = 0;
    int counter000 = 0;
    int counter666 = 0;










    float chunkX0;
    float chunkZ0;

    float chunkX1;
    float chunkZ1;

    float chunkX2;
    float chunkZ2;


    float chunkX3;
    float chunkZ3;

    float chunkX4;
    float chunkZ4;

    float chunkX5;
    float chunkZ5;

    float chunkX6;
    float chunkZ6;

    float chunkX7;
    float chunkZ7;

    Vector3 chunkPos0;
    Vector3 chunkPos1;
    Vector3 chunkPos2;
    Vector3 chunkPos3;
    Vector3 chunkPos4;
    Vector3 chunkPos5;
    Vector3 chunkPos6;
    Vector3 chunkPos7;


    Vector3 currentPos;


    //List<GameObject> chunks = new List<GameObject>();
    List<Vector3> chunks = new List<Vector3>();



    List<Vector3> chunksToRemove0 = new List<Vector3>();
    List<Vector3> chunksToRemove1 = new List<Vector3>();
    List<Vector3> chunksToRemove2 = new List<Vector3>();
    List<Vector3> chunksToRemove3 = new List<Vector3>();







    List<GameObject> chunksToPlace = new List<GameObject>();


    Dictionary<Vector3, int> chunkaToRemove = new Dictionary<Vector3, int>();







    List<Vector3> chunkyToRemove = new List<Vector3>();


    Dictionary<Vector3, int> tempChunk0 = new Dictionary<Vector3, int>();
    Dictionary<Vector3, int> tempChunk1 = new Dictionary<Vector3, int>();
    Dictionary<Vector3, int> tempChunk2 = new Dictionary<Vector3, int>();
    Dictionary<Vector3, int> tempChunk3 = new Dictionary<Vector3, int>();
    Dictionary<Vector3, int> tempChunk4 = new Dictionary<Vector3, int>();
    Dictionary<Vector3, int> tempChunk5 = new Dictionary<Vector3, int>();







    List<Vector3> tempChunk00 = new List<Vector3>();
    List<Vector3> tempChunk10 = new List<Vector3>();
    List<Vector3> tempChunk20 = new List<Vector3>();
    List<Vector3> tempChunk30 = new List<Vector3>();
    List<Vector3> tempChunk40 = new List<Vector3>();
    List<Vector3> tempChunk50 = new List<Vector3>();







    List<Vector3> newChunk0 = new List<Vector3>();
    List<Vector3> newChunk1 = new List<Vector3>();
    List<Vector3> newChunk2 = new List<Vector3>();
    List<Vector3> newChunk3 = new List<Vector3>();
    List<Vector3> newChunk4 = new List<Vector3>();
    List<Vector3> newChunk5 = new List<Vector3>();








    //public List<Chunk> testingChunks = new List<Chunk>();
    //public Hashtable testingChunks = new Hashtable();




    GameObject parent;

    string tilename0;
    string tilename1;
    string tilename2;
    string tilename3;

    Vector3 startPos;
    string chunkName;

    bool jobDone0 = false;
    bool jobDone1 = false;
    bool jobDone2 = false;
    bool jobDone3 = false;
    bool jobDone4 = false;
    bool jobDone5 = false;
    bool jobDone6 = false;
    bool jobDone7 = false;






    //Dictionary<Vector3, Vector3> chunkerPos0 = new Dictionary<Vector3, Vector3>();
    //Dictionary<Vector3, string> chunkerPos0 = new Dictionary<Vector3, string>();
    Dictionary<GameObject, Vector3> chunkerPos0 = new Dictionary<GameObject, Vector3>();
    //Dictionary<Vector3, Vector3> chunkerPos0 = new Dictionary<Vector3, Vector3>();

    //Dictionary<Chunk, Vector3> testingChunks = new Dictionary<Chunk, Vector3>();


    Dictionary<GameObject, Vector3> chunkerPos1 = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> chunkerPos2 = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> chunkerPos3 = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> chunkerPos4 = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> chunkerPos5 = new Dictionary<GameObject, Vector3>();





    Dictionary<GameObject, Vector3> chunkerPosLowDetail = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> chunkerPosHighDetail = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> chunkerPosLOWERDetail = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> chunkerPosLOWESTDetail = new Dictionary<GameObject, Vector3>();





    //Dictionary<GameObject, Vector3> chunkObjects = new Dictionary<GameObject, Vector3>();

    Dictionary<GameObject, Vector3> chunktoLODManageLowDetail = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> chunktoLODManageHighDetail = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> chunktoLODManageLOWERDetail = new Dictionary<GameObject, Vector3>();
    Dictionary<GameObject, Vector3> chunktoLODManageLOWESTDetail = new Dictionary<GameObject, Vector3>();


    //Hashtable chunkerPos = new Hashtable();

    GameObject chunky0;


    GameObject chunky1;
    GameObject chunky2;
    GameObject chunky3;
    GameObject chunky4;
    GameObject chunky5;


    WaitForSeconds wait = new WaitForSeconds(0f);

    public GameObject sphere1;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;





    bool canSpawn0;
    bool canSpawn1;
    bool canSpawn2;
    bool canSpawn3;
    bool canSpawn4;
    bool canSpawn5;
    bool canSpawn6;

    public int seed;


    List<GameObject> chunksToLowerDetail = new List<GameObject>();
    List<GameObject> chunksToIncreaseDetail = new List<GameObject>();
    List<GameObject> AddchunksToLowerDetail = new List<GameObject>();
    List<GameObject> AddchunksToIncreaseDetail = new List<GameObject>();
    List<GameObject> chunksWithDetailLowered = new List<GameObject>();
    List<GameObject> refreshedChunks = new List<GameObject>();

    List<GameObject> chunksToLowerDetail1 = new List<GameObject>();
    List<GameObject> chunksToIncreaseDetail1 = new List<GameObject>();
    List<GameObject> AddchunksToLowerDetail1 = new List<GameObject>();
    List<GameObject> AddchunksToIncreaseDetail1 = new List<GameObject>();
    List<GameObject> chunksWithDetailLowered1 = new List<GameObject>();
    List<GameObject> refreshedChunks1 = new List<GameObject>();

    /*Dictionary<GameObject, GameObject> chunksToLowerDetail = new Dictionary<GameObject, GameObject>();
    Dictionary<GameObject, GameObject> chunksToIncreaseDetail = new Dictionary<GameObject, GameObject>();
    Dictionary<GameObject, GameObject> AddchunksToLowerDetail = new Dictionary<GameObject, GameObject>();
    List<GameObject> AddchunksToIncreaseDetail = new Dictionary<GameObject, GameObject>();
    List<GameObject> chunksWithDetailLowered = new Dictionary<GameObject, GameObject>();*/


    List<Vector3> Vector3ChunksToLODManage = new List<Vector3>();

    public int chunkmaxLODdistance;
    public int chunkminLODdistance;
    public int chunkDeactivationDistance;

    public int levelOfDetail;

    GameObject chunkLODManage;

    bool isMoving = false;

    int timerThreadCounter0 = 0;




    int counter0 = 0;
    int counter1 = 0;
    int counter2 = 0;
    int counter3 = 0;
    int counter4 = 0;
    int counter5 = 0;






    float timer0 = 0;
    float timer1 = 0;
    float timer2 = 0;
    float timer3 = 0;
    float timer4 = 0;
    float timer5 = 0;

    Hashtable newShit = new Hashtable();

    bool addChunk0 = false;
    bool addChunk1 = false;
    bool addChunk2 = false;
    bool addChunk3 = false;
    bool addChunk4 = false;
    bool addChunk5 = false;



    int chunkPosCounter0 = 0;



    Vector3 yo1;
    Vector3 yo2;


    //Chunk chunk;


    float startthreadTimerCounter0 = 0;
    float endthreadTimerCounter0 = 0;

    float startthreadTimerCounter1 = 0;
    float endthreadTimerCounter1 = 0;

    float theTime;

    int threadTimer = 0;

    bool threadAlive0 = false;
    private volatile bool cancelFlag = false;


    bool startThread0 = false;
    bool startThread1 = false;
    bool startThread2 = false;
    bool startThread3 = false;

    int posExists = 0;
    int posDontExists = 1;

    Vector3 currentPerfectChunkPos0;
    Vector3 currentPerfectChunkPos1;
    Vector3 currentPerfectChunkPos2;




    void Start()
    {

        if (seed == 0)
        {
            seed = Random.Range(0, int.MaxValue);
        }
        currentTerrain22 = this;

        startPos = transform.position;





        /*if (jobDone7 == false)
        {
            t0 = new Thread(Func0) { Name = "Thread 0" };

            if (!t0.IsAlive)
            {
                //counter222 = 0;
                t0.Start();
                jobDone7 = true;
            }
        }

        if (jobDone6 == false)
        {

            t1 = new Thread(Func1) { Name = "Thread 1" };

            if (!t1.IsAlive)
            {
                //counter333 = 0;
                t1.Start();
                jobDone6 = true;
            }
        }

        if (jobDone5 == false)
        {

            t2 = new Thread(Func2) { Name = "Thread 2" };

            if (!t2.IsAlive)
            {
                //counter333 = 0;
                t2.Start();
                jobDone5 = true;
            }
        }*/




    }





    public bool ChunkPosExists0(Vector3 chunkPos)
    {
        var enumerator0 = chunkerPos0.GetEnumerator();

        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;

            if (chunkPos.x < tls0.Value.x || (chunkPos.z < tls0.Value.z) || (chunkPos.x >= tls0.Value.x + chunkSizeLOWDETAIL) || (chunkPos.z >= tls0.Value.z + chunkSizeLOWDETAIL))
            {
                continue;
            }
            return true;
        }
        return false;
    }


    public bool ChunkPosExists1(Vector3 chunkPos)
    {
        var enumerator0 = chunkerPos1.GetEnumerator();

        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;

            if (chunkPos.x < tls0.Value.x || (chunkPos.z < tls0.Value.z) || (chunkPos.x >= tls0.Value.x + chunkSizeLOWDETAIL) || (chunkPos.z >= tls0.Value.z + chunkSizeLOWDETAIL))
            {
                continue;
            }
            return true;
        }
        return false;
    }

    public bool ChunkPosExists2(Vector3 chunkPos)
    {
        var enumerator0 = chunkerPos2.GetEnumerator();

        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;

            if (chunkPos.x < tls0.Value.x || (chunkPos.z < tls0.Value.z) || (chunkPos.x >= tls0.Value.x + chunkSizeLOWDETAIL) || (chunkPos.z >= tls0.Value.z + chunkSizeLOWDETAIL))
            {
                continue;
            }
            return true;
        }
        return false;
    }


















    private void MyThread0()
    {
        while (cancelFlag == false)
        {
            do
            {
                for (float x = (currentPos.x) - viewSize0; x <= (currentPos.x) + viewSize0; x += chunkSizeLOWDETAIL)
                {
                    for (float z = (currentPos.z) - viewSize0; z <= (currentPos.z) + viewSize0; z += chunkSizeLOWDETAIL)
                    {
                        float chunkX0 = Mathf.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        float chunkZ0 = Mathf.FloorToInt(z / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        chunkPos0 = new Vector3(chunkX0, 0, chunkZ0);

                        if (chunkPos0 == chunkPos1 || chunkPos0 == chunkPos2 || chunkPos0 == chunkPos3 || chunkPos0 == chunkPos4 || chunkPos0 == chunkPos5)
                        {
             
                        }
                        if (chunkPos0 != chunkPos1 && chunkPos0 != chunkPos2 && chunkPos0 != chunkPos3 && chunkPos0 != chunkPos4 && chunkPos0 != chunkPos5)
                        {

                            if (tempChunk0.ContainsKey(chunkPos0) || tempChunk1.ContainsKey(chunkPos0) || tempChunk2.ContainsKey(chunkPos0) || tempChunk3.ContainsKey(chunkPos0) || tempChunk4.ContainsKey(chunkPos0) || tempChunk5.ContainsKey(chunkPos0))
                            {
                                continue;
                            }
                            if (!tempChunk0.ContainsKey(chunkPos0) && !tempChunk1.ContainsKey(chunkPos0) && !tempChunk2.ContainsKey(chunkPos0) && !tempChunk3.ContainsKey(chunkPos0) && !tempChunk4.ContainsKey(chunkPos0) && !tempChunk5.ContainsKey(chunkPos0))
                            {

                                for (int u = 0; u < 1; u++)
                                {
                                    
                                    chunkPosCounter0 += 1;
                                    tempChunk0.Add(chunkPos0, posDontExists);
                                    //tempChunk00.Add(chunkPos0);

                                    //iSCentralDispatch.Instantiate(sphere1, chunkPos0, Quaternion.identity);

                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH
                                    //chunkerPos0.Add((GameObject)iSCentralDispatch.Instantiate(chunkFabDetail2, chunkPos0, Quaternion.identity), chunkPos0);
                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH

                                    chunkPosCounter0 -= 1;

                                }
            
                            }
                        }
                    }
                }
                Thread.Sleep(0);
            } while (jobDone0 == true);
        }
    }




    private void MyThread1()
    {
        while (cancelFlag == false)
        {
            do
            {
                for (float x = ((currentPos.x) - (viewSize0 + viewSize1)); x <= ((currentPos.x) + (viewSize0 + viewSize1)); x += chunkSizeLOWDETAIL)
                {
                    for (float z = ((currentPos.z) - (viewSize0 + viewSize1)); z <= ((currentPos.z) + (viewSize0 + viewSize1)); z += chunkSizeLOWDETAIL)
                    {
                        float chunkX1 = Mathf.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        float chunkZ1 = Mathf.FloorToInt(z / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        chunkPos1 = new Vector3(chunkX1, 0, chunkZ1);

                        if (chunkPos0 == chunkPos1 || chunkPos1 == chunkPos2 || chunkPos1 == chunkPos3 || chunkPos1 == chunkPos4 || chunkPos1 == chunkPos5)
                        {
  
                        }
                        if (chunkPos0 != chunkPos1 && chunkPos1 != chunkPos2 && chunkPos1 != chunkPos3 && chunkPos1 != chunkPos4 && chunkPos1 != chunkPos5)
                        {

                            if (tempChunk0.ContainsKey(chunkPos1) || tempChunk1.ContainsKey(chunkPos1) || tempChunk2.ContainsKey(chunkPos1) || tempChunk3.ContainsKey(chunkPos1) || tempChunk4.ContainsKey(chunkPos1) || tempChunk5.ContainsKey(chunkPos1))
                            {
                                continue;
                            }
                            if (!tempChunk0.ContainsKey(chunkPos1) && !tempChunk1.ContainsKey(chunkPos1) && !tempChunk2.ContainsKey(chunkPos1) && !tempChunk3.ContainsKey(chunkPos1) && !tempChunk4.ContainsKey(chunkPos1) && !tempChunk5.ContainsKey(chunkPos1))
                            {
                                for (int u = 0; u < 1; u++)
                                {
                                    chunkPosCounter0 += 1;
                                    tempChunk1.Add(chunkPos1, posDontExists);
                                    //iSCentralDispatch.Instantiate(cube1, chunkPos1, Quaternion.identity);

                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH
                                    //chunkerPos1.Add((GameObject)iSCentralDispatch.Instantiate(chunkFabDetail4, chunkPos1, Quaternion.identity), chunkPos1);
                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH


                                    chunkPosCounter0 -= 1;
                                }                    
                            }
                        }
                        /*if (x <= ((currentPos.x) - viewSize0) && z <= ((currentPos.z) - viewSize0))
                        {

                        }
                        if (x <= ((currentPos.x) - viewSize0) && z >= ((currentPos.z) + viewSize0))
                        {

                        }
                        if (x >= ((currentPos.x) + viewSize0) && z >= ((currentPos.z) + viewSize0))
                        {

                        }
                        if (x >= ((currentPos.x) + viewSize0) && z <= ((currentPos.z) - viewSize0))
                        {

                        }*/

                        if (x <= ((currentPos.x) - viewSize0) && z <=((currentPos.z) - viewSize0) ||
                            x >= ((currentPos.x) + viewSize0) && z >= ((currentPos.z) + viewSize0) ||
                            x >= ((currentPos.x) + viewSize0) && z <= ((currentPos.z) - viewSize0) ||
                            x <= ((currentPos.x) - viewSize0) && z >= ((currentPos.z) + viewSize0))
                        {

                        }
                    }
                }
                Thread.Sleep(0);
            } while (jobDone1 == true);
        }
    }



    private void MyThread2()
    {
        while (cancelFlag == false)
        {
            do
            {
                for (float x = ((currentPos.x) - (viewSize0 + viewSize1 + viewSize2)); x <= ((currentPos.x) + (viewSize0 + viewSize1 + viewSize2)); x += chunkSizeLOWDETAIL)
                {
                    for (float z = ((currentPos.z) - (viewSize0 + viewSize1 + viewSize2)); z <= ((currentPos.z) + (viewSize0 + viewSize1 + viewSize2)); z += chunkSizeLOWDETAIL)
                    {
                        float chunkX2 = Mathf.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        float chunkZ2 = Mathf.FloorToInt(z / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        chunkPos2 = new Vector3(chunkX2, 0, chunkZ2);

                        if (chunkPos0 == chunkPos2 || chunkPos1 == chunkPos2 || chunkPos2 == chunkPos3 || chunkPos2 == chunkPos4 || chunkPos2 == chunkPos5)
                        {
   
                        }
                        if (chunkPos0 != chunkPos2 && chunkPos1 != chunkPos2 && chunkPos2 != chunkPos3 && chunkPos2 != chunkPos4 && chunkPos2 != chunkPos5)
                        {
                            if (tempChunk0.ContainsKey(chunkPos2) || tempChunk1.ContainsKey(chunkPos2) || tempChunk2.ContainsKey(chunkPos2) || tempChunk3.ContainsKey(chunkPos2) || tempChunk4.ContainsKey(chunkPos2) || tempChunk5.ContainsKey(chunkPos2))
                            {
                                continue;
                            }
                            if (!tempChunk0.ContainsKey(chunkPos2) && !tempChunk1.ContainsKey(chunkPos2) && !tempChunk2.ContainsKey(chunkPos2) && !tempChunk3.ContainsKey(chunkPos2) && !tempChunk4.ContainsKey(chunkPos2) && !tempChunk5.ContainsKey(chunkPos2))
                            {
                                for (int u = 0; u < 1; u++)
                                {
                                    chunkPosCounter0 += 1;
                                    tempChunk2.Add(chunkPos2, posDontExists);

                                    //iSCentralDispatch.Instantiate(cube2, chunkPos2, Quaternion.identity);

                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH
                                    //chunkerPos2.Add((GameObject)iSCentralDispatch.Instantiate(chunkFabDetail6, chunkPos2, Quaternion.identity), chunkPos2);
                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH

                                    chunkPosCounter0 -= 1;                          
                                }             
                            }
                        }
                    }
                }
                Thread.Sleep(0);
            } while (jobDone2 == true);
        }
    }



    private void MyThread3()
    {
        while (cancelFlag == false)
        {
            do
            {
                for (float x = ((currentPos.x) - (viewSize0 + viewSize1 + viewSize2 + viewSize3)); x <= ((currentPos.x) + (viewSize0 + viewSize1 + viewSize2 + viewSize3)); x += chunkSizeLOWDETAIL)
                {
                    for (float z = ((currentPos.z) - (viewSize0 + viewSize1 + viewSize2 + viewSize3)); z <= ((currentPos.z) + (viewSize0 + viewSize1 + viewSize2 + viewSize3)); z += chunkSizeLOWDETAIL)
                    {
                        float chunkX3 = Mathf.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        float chunkZ3 = Mathf.FloorToInt(z / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        chunkPos3 = new Vector3(chunkX3, 0, chunkZ3);


                        if (chunkPos0 == chunkPos3 || chunkPos1 == chunkPos3 || chunkPos2 == chunkPos3 || chunkPos3 == chunkPos4 || chunkPos3 == chunkPos5)
                        {

                        }
                        if (chunkPos0 != chunkPos3 && chunkPos1 != chunkPos3 && chunkPos2 != chunkPos3 && chunkPos3 != chunkPos4 && chunkPos3 != chunkPos5)
                        {

                            if (tempChunk0.ContainsKey(chunkPos3) || tempChunk1.ContainsKey(chunkPos3) || tempChunk2.ContainsKey(chunkPos3) || tempChunk3.ContainsKey(chunkPos3) || tempChunk4.ContainsKey(chunkPos3) || tempChunk5.ContainsKey(chunkPos3))
                            {
                                continue;
                            }
                            if (!tempChunk0.ContainsKey(chunkPos3) && !tempChunk1.ContainsKey(chunkPos3) && !tempChunk2.ContainsKey(chunkPos3) && !tempChunk3.ContainsKey(chunkPos3) && !tempChunk4.ContainsKey(chunkPos3) && !tempChunk5.ContainsKey(chunkPos3))
                            {
                                for (int u = 0; u < 1; u++)
                                {
                                    chunkPosCounter0 += 1;
                                    tempChunk3.Add(chunkPos3, posDontExists);

                                    //iSCentralDispatch.Instantiate(cube3, chunkPos3, Quaternion.identity);

                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH
                                    //chunkerPos3.Add((GameObject)iSCentralDispatch.Instantiate(chunkFabDetail8, chunkPos3, Quaternion.identity), chunkPos3);
                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH

                                    chunkPosCounter0 -= 1;
                                }
                   
                            }
                        }
                    }
                }
                Thread.Sleep(0);
            } while (jobDone3 == true);
        }
    }

    private void MyThread4()
    {
        while (cancelFlag == false)
        {
            do
            {
                for (float x = ((currentPos.x) - (viewSize0 + viewSize1 + viewSize2 + viewSize3 + viewSize4)); x <= ((currentPos.x) + (viewSize0 + viewSize1 + viewSize2 + viewSize3 + viewSize4)); x += chunkSizeLOWDETAIL)
                {
                    for (float z = ((currentPos.z) - (viewSize0 + viewSize1 + viewSize2 + viewSize3 + viewSize4)); z <= ((currentPos.z) + (viewSize0 + viewSize1 + viewSize2 + viewSize3 + viewSize4)); z += chunkSizeLOWDETAIL)
                    {
                        float chunkX4 = Mathf.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        float chunkZ4 = Mathf.FloorToInt(z / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        chunkPos4 = new Vector3(chunkX4, 0, chunkZ4);

                        if (chunkPos0 == chunkPos4 || chunkPos1 == chunkPos4 || chunkPos2 == chunkPos4 || chunkPos3 == chunkPos4 || chunkPos4 == chunkPos5)
                        {
                   
                        }
                        if (chunkPos0 != chunkPos4 && chunkPos1 != chunkPos4 && chunkPos2 != chunkPos4 && chunkPos3 != chunkPos4 && chunkPos4 != chunkPos5)
                        {

                            if (tempChunk0.ContainsKey(chunkPos4) || tempChunk1.ContainsKey(chunkPos4) || tempChunk2.ContainsKey(chunkPos4) || tempChunk3.ContainsKey(chunkPos4) || tempChunk4.ContainsKey(chunkPos4) || tempChunk5.ContainsKey(chunkPos4))
                            {
                                continue;
                            }
                            if (!tempChunk0.ContainsKey(chunkPos4) && !tempChunk1.ContainsKey(chunkPos4) && !tempChunk2.ContainsKey(chunkPos4) && !tempChunk3.ContainsKey(chunkPos4) && !tempChunk4.ContainsKey(chunkPos4) && !tempChunk5.ContainsKey(chunkPos4))
                            {
                                for (int u = 0; u < 1; u++)
                                {
                                    chunkPosCounter0 += 1;
                                    tempChunk4.Add(chunkPos4, posDontExists);

                                    //iSCentralDispatch.Instantiate(cube4, chunkPos4, Quaternion.identity);

                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH
                                    //chunkerPos4.Add((GameObject)iSCentralDispatch.Instantiate(chunkFabDetail10, chunkPos4, Quaternion.identity), chunkPos4);
                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH

                                    chunkPosCounter0 -= 1;
                                }
        
                            }
                        }
                    }
                }
                Thread.Sleep(0);
            } while (jobDone4 == true);
        }
    }


    private void MyThread5()
    {
        while (cancelFlag == false)
        {
            do
            {
                for (float x = ((currentPos.x) - (viewSize0 + viewSize1 + viewSize2 + viewSize3 + viewSize4 + viewSize5)); x <= ((currentPos.x) + (viewSize0 + viewSize1 + viewSize2 + viewSize3 + viewSize4 + viewSize5)); x += chunkSizeLOWDETAIL)
                {
                    for (float z = ((currentPos.z) - (viewSize0 + viewSize1 + viewSize2 + viewSize3 + viewSize4 + viewSize5)); z <= ((currentPos.z) + (viewSize0 + viewSize1 + viewSize2 + viewSize3 + viewSize4 + viewSize5)); z += chunkSizeLOWDETAIL)
                    {
                        float chunkX5 = Mathf.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        float chunkZ5 = Mathf.FloorToInt(z / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        chunkPos5= new Vector3(chunkX5, 0, chunkZ5);


                        if (chunkPos0 == chunkPos5|| chunkPos1 == chunkPos5 || chunkPos2 == chunkPos5|| chunkPos3 == chunkPos5 || chunkPos4 == chunkPos5)
                        {
         
                        }
                        if (chunkPos0 != chunkPos5 && chunkPos1 != chunkPos5 && chunkPos2 != chunkPos5 && chunkPos3 != chunkPos5 && chunkPos4 != chunkPos5)
                        {

                            if (tempChunk0.ContainsKey(chunkPos5) || tempChunk1.ContainsKey(chunkPos5) || tempChunk2.ContainsKey(chunkPos5) || tempChunk3.ContainsKey(chunkPos5) || tempChunk4.ContainsKey(chunkPos5) || tempChunk5.ContainsKey(chunkPos5))
                            {
                                continue;
                            }
                            if (!tempChunk0.ContainsKey(chunkPos5) && !tempChunk1.ContainsKey(chunkPos5) && !tempChunk2.ContainsKey(chunkPos5) && !tempChunk3.ContainsKey(chunkPos5) && !tempChunk4.ContainsKey(chunkPos5) && !tempChunk5.ContainsKey(chunkPos5))
                            {
                                for (int u = 0; u < 1; u++)
                                {
                                    chunkPosCounter0 += 1;
                                    tempChunk5.Add(chunkPos5, posDontExists);

                                    //iSCentralDispatch.Instantiate(cube5, chunkPos5, Quaternion.identity);

                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH
                                    //chunkerPos5.Add((GameObject)iSCentralDispatch.Instantiate(chunkFabDetail12, chunkPos5, Quaternion.identity), chunkPos5);
                                    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH

                                    chunkPosCounter0 -= 1;
                                }
                    
                            }
                        }
                    }
                }
                Thread.Sleep(0);
            } while (jobDone5 == true);
        }
    }




































    void Update()
    {




        if (Input.GetKeyDown("p"))
        {


            /*if (t1.IsAlive)
            {
                Debug.Log("yo1");

            }

            if (t2.IsAlive)
            {

                Debug.Log("yo2");
            }*/

        }











        /*for (int i = 0; i < tempChunk0.Count; i++)
        {
            Debug.DrawRay(tempChunk0[i], Vector3.down * 10, Color.green, 0.1f);
        }
        for (int i = 0; i < tempChunk1.Count; i++)
        {
            Debug.DrawRay(tempChunk1[i], Vector3.up * 10, Color.red, 0.1f);
        }
        for (int i = 0; i < tempChunk2.Count; i++)
        {
            Debug.DrawRay(tempChunk2[i], Vector3.up * 10, Color.blue, 0.1f);
        }*/





        /*var enumerator0 = tempChunk0.GetEnumerator();

        while (enumerator0.MoveNext())
        {
            var temp0 = enumerator0.Current;
            Debug.DrawRay(temp0.Key, Vector3.up * 10, Color.blue, 0.1f);
        }
        var enumerator1 = tempChunk1.GetEnumerator();

        while (enumerator1.MoveNext())
        {
            var temp1 = enumerator1.Current;
            Debug.DrawRay(temp1.Key, Vector3.up * 15, Color.red, 0.1f);
        }
        var enumerator2 = tempChunk2.GetEnumerator();

        while (enumerator2.MoveNext())
        {
            var temp2 = enumerator2.Current;
            Debug.DrawRay(temp2.Key, Vector3.down * 10, Color.green, 0.1f);
        }*/








        currentPos = transform.position;

        int xMove = (int)(transform.position.x - startPos.x);
        int zMove = (int)(transform.position.z - startPos.z);
        int yMove = (int)(transform.position.y - startPos.y);

        if (Input.GetKeyDown("t"))
        {
            Debug.Log(startthreadTimerCounter0);
            Debug.Log(endthreadTimerCounter0);
        }






        //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH
        /*
        if (jobDone0 == false)
        {
            threadID0 = iSCentralDispatch.DispatchNewThread("ChunkSpawnLOWERDetail0", MyThread0);
            //iSCentralDispatch.SetPriorityForThread(threadID2, iSCDThreadPriority.High);
            iSCentralDispatch.SetTargetFramerate(1);
            //counter222 = 0;
            jobDone0 = true;
        }

        if (jobDone1 == false)
        {
            threadID1 = iSCentralDispatch.DispatchNewThread("ChunkSpawnLOWERDetail1", MyThread1);
            //iSCentralDispatch.SetPriorityForThread(threadID2, iSCDThreadPriority.High);
            iSCentralDispatch.SetTargetFramerate(1);
            //counter333 = 0;
            jobDone1 = true;
        }

        if (jobDone2 == false)
        {
            threadID2 = iSCentralDispatch.DispatchNewThread("ChunkSpawnLOWERDetail2", MyThread2);
            //iSCentralDispatch.SetPriorityForThread(threadID2, iSCDThreadPriority.High);
            iSCentralDispatch.SetTargetFramerate(1);
            //counter333 = 0;
            jobDone2 = true;
        }
        if (jobDone3 == false)
        {
            threadID3 = iSCentralDispatch.DispatchNewThread("ChunkSpawnLOWERDetail3", MyThread3);
            //iSCentralDispatch.SetPriorityForThread(threadID2, iSCDThreadPriority.High);
            iSCentralDispatch.SetTargetFramerate(1);
            //counter333 = 0;
            jobDone3 = true;
        }
        if (jobDone4 == false)
        {
            threadID4 = iSCentralDispatch.DispatchNewThread("ChunkSpawnLOWERDetail4", MyThread4);
            //iSCentralDispatch.SetPriorityForThread(threadID2, iSCDThreadPriority.High);
            iSCentralDispatch.SetTargetFramerate(1);
            //counter333 = 0;
            jobDone4 = true;
        }

        if (jobDone5 == false)
        {
            threadID5 = iSCentralDispatch.DispatchNewThread("ChunkSpawnLOWERDetail5", MyThread5);
            //iSCentralDispatch.SetPriorityForThread(threadID2, iSCDThreadPriority.High);
            iSCentralDispatch.SetTargetFramerate(1);
            //counter333 = 0;
            jobDone5 = true;
        }*/














        if (Mathf.Abs(xMove) >= chunkSizeVERYHIGHDETAIL / 10 || Mathf.Abs(zMove) >= chunkSizeVERYHIGHDETAIL / 10 || Mathf.Abs(yMove) >= chunkSizeVERYHIGHDETAIL / 10)
        {
            startPos = transform.position;
        }
    }





    WaitForSeconds wait2 = new WaitForSeconds(0f);


    /*void MyThread0()
    {
        while (cancelFlag == false)
        {
            do
            {
                for (int i = 0; i < tempChunk00.Count; i++)
                {
                    if (ChunkPosExists0(tempChunk00[i]))
                    {
                        continue;
                    }

                    if (!ChunkPosExists0(tempChunk00[i]))
                    {
                        iSCentralDispatch.Instantiate(sphere1, tempChunk00[i], Quaternion.identity);
                        chunkerPos0.Add((GameObject)iSCentralDispatch.Instantiate(chunkFabLowDetail, tempChunk00[i], Quaternion.identity), tempChunk00[i]);
                        chunkPosCounter0 -= 1;
                    }
                }

            } while (startThread0 == true);
        }
    }


    void MyThread1()
    {
        while (cancelFlag == false)
        {
            do
            {
                for (int i = 0; i < tempChunk10.Count; i++)
                {
                    if (ChunkPosExists1(tempChunk10[i]))
                    {
                        continue;
                    }

                    if (!ChunkPosExists1(tempChunk10[i]))
                    {
                        iSCentralDispatch.Instantiate(cube1, tempChunk10[i], Quaternion.identity);
                        chunkerPos1.Add((GameObject)iSCentralDispatch.Instantiate(chunkFabLowDetail, tempChunk10[i], Quaternion.identity), tempChunk10[i]);
                        chunkPosCounter0 -= 1;
                    }
                }

            } while (startThread1 == true);
        }
    }



    void MyThread2()
    {
        while (cancelFlag == false)
        {
            do
            {
                for (int i = 0; i < tempChunk20.Count; i++)
                {
                    if (ChunkPosExists2(tempChunk20[i]))
                    {
                        continue;
                    }

                    if (!ChunkPosExists2(tempChunk20[i]))
                    {
                        iSCentralDispatch.Instantiate(cube2, tempChunk20[i], Quaternion.identity);
                        chunkerPos2.Add((GameObject)iSCentralDispatch.Instantiate(chunkFabLowDetail, tempChunk20[i], Quaternion.identity), tempChunk20[i]);
                        chunkPosCounter0 -= 1;
                    }
                }

            } while (startThread2 == true);
        }
    }*/






























    /*IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(0.01f);
        Vector3 finalPos = transform.position;
        if (startPos.x != finalPos.x || startPos.y != finalPos.y
            || startPos.z != finalPos.z)
        {
            isMoving = true;
        }

        else if (startPos.x == finalPos.x && startPos.y == finalPos.y
             && startPos.z == finalPos.z)
        {

            isMoving = false;
        }
    }*/



    /* IEnumerator Timing0()
     {
         if (timer0 < 0)
         {
             timer0 = 0;
         }

         if (timer0 == 0)
         {

         }
         if (counter0 == 1)
         {
             if (timer0 != 0 && timer0 > 0)
             {
                 timer0 += Time.deltaTime;
             }
         }
         yield return wait2;
     }*/




    //TO READD IF PEOPLE OWN THE ASSET ISCENTRALDISPATCH
    
    /*void OnDisable()
    {
        cancelFlag = true;
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID0);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID1);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID2);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID3);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID4);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID5);

        if (t1 != null)
        {
            if (t1.IsAlive)
            {
                t1.Abort();
            }
        }
        if (t2 != null)
        {

            if (t2.IsAlive)
            {
                t2.Abort();
            }
        }
        if (t0 != null)
        {
            if (t0.IsAlive)
            {
                t0.Abort();
            }
        }
        //if (!t2.IsAlive)
        //{
        //    t2.Abort();
        //}

    }


void OnApplicationQuit()
    {
        cancelFlag = true;
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID0);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID1);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID2);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID3);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID4);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID5);
        //iSCentralDispatch.AbortThread(threadID);
        if (t1 != null)
        {
            if (t1.IsAlive)
            {
                t1.Abort();
            }
        }
        if (t2 != null)
        {

            if (t2.IsAlive)
            {
                t2.Abort();
            }
        }
        if (t0 != null)
        {
            if (t0.IsAlive)
            {
                t0.Abort();
            }
        }

    }

    void OnDestroy()
    {
        cancelFlag = true;
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID0);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID1);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID2);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID3);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID4);
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID5);
        //iSCentralDispatch.AbortThread(threadID);

        if (t1!= null)
        {
            if (t1.IsAlive)
            {
                t1.Abort();
            }
        }
        if (t2 != null)
        {

            if (t2.IsAlive)
            {
                t2.Abort();
            }
        }
        if (t0 != null)
        {
            if (t0.IsAlive)
            {
                t0.Abort();
            }
        }

    }*/

}





