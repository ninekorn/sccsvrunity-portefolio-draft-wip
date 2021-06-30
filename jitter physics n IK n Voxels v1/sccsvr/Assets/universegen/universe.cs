using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using SPINACH.iSCentralDispatch;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.IO;

public class regionChunker
{
    regionChunker regionChunku;
    public areaChunker[] areaChunkyArray;
    public Vector3 worldPosition;
    public int regionX;
    public int regionY;
    public int regionZ;
    public GameObject currentRegionChunk = null;
    public regionChunker[] worldChunky;
    public Vector3 worldScenePos;

    public regionChunker()
    {

    }
}

public class areaChunker
{
    areaChunker areaChunku;
    //public bigChunker[,,] bigChunky;
    public bigChunker[] bigChunkyArray;
    //public bigChunk[,,] bigChunkerArray { get; set; }
    //public areaChunker[,,] areaChunkyList = new areaChunker[20, 20, 20];
    public Vector3 worldPosition;
    public int regionX;
    public int regionY;
    public int regionZ;
    public int areaX;
    public int areaY;
    public int areaZ;
    public GameObject currentChunk = null;

    public areaChunker()
    {

    }
}

public class bigChunker
{
    public Vector3 worldAreaPosition;
    public smallChunker[] smallChunkerList;
    areaChunker areaChunky;
    //public bigChunker[,,] bigChunky;
    //public bigChunker[,,] bigChunkyArray;
    public int regionX;
    public int regionY;
    public int regionZ;
    public int areaX;
    public int areaY;
    public int areaZ;
    public int bigX;
    public int bigY;
    public int bigZ;
    public GameObject currentChunk = null;

    public bigChunker()
    {

    }
}

public class smallChunker
{
    public int regionX;
    public int regionY;
    public int regionZ;
    public int areaX;
    public int areaY;
    public int areaZ;
    public int bigX;
    public int bigY;
    public int bigZ;
    public int smallX;
    public int smallY;
    public int smallZ;

    public Vector3 worldPosition;

    public byte[] map;
    smallChunker chuk;

    public Vector3[] positionz;
    public Vector3[] normalz;
    public Vector2[] textureCoordinatez;
    public int[] triangleIndicez;
    //public static smallChunker[] arrayOfSmallChunks = new smallChunker[2 * 2 * 2];
    public static int counter = 0;
    public GameObject currentChunk = null;
    public meshData currentMeshData;
    public bool hasBeenSpawned = false;
    public bool hasMesh = false;
    public bool hasMap = false;

    public bool distanceRejected = false;

    public smallChunker()
    {
        //this.map = mapper;
    }
}



public class testerOfNumber
{
    private ReaderWriterLock rwl = new ReaderWriterLock();
    private ReaderWriterLock rwlock = new ReaderWriterLock();
    private ReaderWriterLock rwlRegionChunkor = new ReaderWriterLock();
    private ReaderWriterLock rwlAreaChunkor = new ReaderWriterLock();
    private ReaderWriterLock rwlBigChunkor = new ReaderWriterLock();
    private ReaderWriterLock rwlSmallChunkor = new ReaderWriterLock();

    private areaChunker[] currentAreaChunk;

    public Vector3 viewerPosition;
    public Vector3 realScenePos;
    public Vector3 currentRegionPos;
    public Vector3 currentAreaPos;
    public Vector3 currentBigPos;
    public Vector3 currentSmallPos;
    public Vector3 maxWorldChunkVision;
    //public int chunkArraySize = 3;

    //static regionChunker[] worldChunky;


    public int[] arrayOfRegionPos;
    public int currentFuckingInt;
    public static Dictionary<Vector3, smallChunker[]> sceneList = new Dictionary<Vector3, smallChunker[]>();
    public int[] regionArrayPos;

    Queue<meshData> testingThreads = new Queue<meshData>();

    private static Mutex mut = new Mutex();

    //public int regionX;
    //public int regionY;
    //public int regionZ;

    /*public int areaX;
    public int areaY;
    public int areaZ;

    public int bigX;
    public int bigY;
    public int bigZ;

    public int smallatorX;
    public int smallatorY;
    public int smallatorZ;*/

    object tempRegionRead = new object();
    object tempRegionWrite = new object();

    object tempAreaRead = new object();
    object tempAreaWrite = new object();

    object tempBigRead = new object();
    object tempBigWrite = new object();

    object tempSmallRead = new object();
    object tempSmallWrite = new object();

    object testRegionObject = new object();

    object testRegionObjectThread0 = new object();
    object testRegionObjectThread1 = new object();
    object testRegionObjectThread2 = new object();

    int worldChunkWidth = 81;
    int regionChunkWidth = 27;
    int areaChunkWidth = 9;
    int bigChunkWidth = 3;
    int smallChunkWidth = 1;

    /*float worldChunkWidth = 8.1f;
    float regionChunkWidth = 2.7f;
    float areaChunkWidth = 0.9f;
    float bigChunkWidth = 0.3f;
    float smallChunkWidth = 0.1f;*/

    /*float worldChunkWidth = 16.2f;
    float regionChunkWidth = 5.4f;
    float areaChunkWidth = 1.8f;
    float bigChunkWidth = 0.6f;
    float smallChunkWidth = .2f;*/

    /*float worldChunkWidth = 0.81f;
    float regionChunkWidth = 0.27f;
    float areaChunkWidth = 0.09f;
    float bigChunkWidth = 0.03f;
    float smallChunkWidth = 0.01f;*/

    /*int worldChunkWidth = 405;
    int regionChunkWidth = 135;
    int areaChunkWidth = 45;
    int bigChunkWidth = 15;
    int smallChunkWidth = 5;*/

    /*int worldChunkWidth = 162;
    int regionChunkWidth = 54;
    int areaChunkWidth = 18;
    int bigChunkWidth = 6;
    int smallChunkWidth = 2;*/

    /*int worldChunkWidth = 324;
    int regionChunkWidth = 108;
    int areaChunkWidth = 36;
    int bigChunkWidth = 12;
    int smallChunkWidth = 4;*/

    //return map[x + width * (y + depth * z)];

    /*float worldChunkWidthY = 8.1f;
    float regionChunkWidthY = 2.7f;
    float areaChunkWidthY = 0.9f;
    float bigChunkWidthY = 0.3f;
    float smallChunkWidthY = 0.1f;*/

    /*float worldChunkWidthY = .81f;
    float regionChunkWidthY = 0.27f;
    float areaChunkWidthY = 0.09f;
    float bigChunkWidthY = 0.03f;
    float smallChunkWidthY = 0.01f;*/

    //int maxWorldChunkWidth = 486;


    float worldChunkWidthY = 81f;
    float regionChunkWidthY = 27f;
    float areaChunkWidthY = 9f;
    float bigChunkWidthY = 3f;
    float smallChunkWidthY = 1f;

    /*float worldChunkWidthY = 16.2f;
    float regionChunkWidthY = 5.4f;
    float areaChunkWidthY = 1.8f;
    float bigChunkWidthY = 0.6f;
    float smallChunkWidthY = .2f;*/

    /*float worldChunkWidthY = 162f;
    float regionChunkWidthY = 54f;
    float areaChunkWidthY = 18f;
    float bigChunkWidthY = 6f;
    float smallChunkWidthY = 2f;*/

    /*float worldChunkWidthY = 162f;
    float regionChunkWidthY = 54f;
    float areaChunkWidthY = 18f;
    float bigChunkWidthY = 6f;
    float smallChunkWidthY = 2f;*/

    /*int worldChunkWidthY = 405;
    int regionChunkWidthY = 135;
    int areaChunkWidthY = 45;
    int bigChunkWidthY = 15;
    float smallChunkWidthY = 5;*/

    private int realChunkWidth = 10;
    private int realChunkHeight = 10;
    private int realChunkDepth = 10;

    public Vector3 realWorldScenePos;

    float planeSize = 0.1f;
    int seed = 3420;
    int detailScale = 5;
    int heightScale = 5;
    public static Queue<checkingBytes> firstQueue = new Queue<checkingBytes>();
    public static Queue<checkingBytes> nextQueue = new Queue<checkingBytes>();

    public static int width = 3;
    public static int height = 3;
    public static int depth = 3;
    public class newScenePos
    {
        public Vector3 scenePos;
        public Vector3 viewerPos;

        public newScenePos(Vector3 scenePoser, Vector3 viewerPos)
        {
            this.scenePos = scenePoser;
            this.viewerPos = viewerPos;
        }
    }
    object sceneObject = new object();



    //public static smallChunker[] smallChunkArray;


    public void AccessGlobalResource(object stateInfo)
    {
        try
        {
            lock (sceneObject)
            {
                newScenePos newScene = (newScenePos)stateInfo;

                float sizerX = bigChunkWidth;
                float sizerY = bigChunkWidth;
                float sizerZ = bigChunkWidth;

                /*float sizerX = bigChunkWidth;
                float sizerY = bigChunkWidth;
                float sizerZ = bigChunkWidth;*/

                for (float x = -sizerX; x < sizerX + sizerX; x += bigChunkWidth)
                {
                    for (float y = -sizerY; y < sizerY + sizerY ; y += bigChunkWidth)
                    {
                        for (float z = -sizerZ; z < sizerZ + sizerZ; z += bigChunkWidth)
                        {
                            Vector3 whatever = new Vector3(x, y, z);
                            realWorldScenePos = whatever + new Vector3(newScene.scenePos.x, newScene.scenePos.y, newScene.scenePos.z);
                            //realWorldScenePos =  new Vector3(newScene.scenePos.x, newScene.scenePos.y, newScene.scenePos.z);

                            if (!sceneList.ContainsKey(realWorldScenePos))
                            {
                                //Debug.Log(realWorldScenePos);
                                smallChunker[] worldChunky = new smallChunker[width * height * depth];
                                sceneList.Add(realWorldScenePos, worldChunky);
                                smallChunkCreator(realWorldScenePos, worldChunky);
                            }
                            else
                            {
                                smallChunker[] valuer;
                                sceneList.TryGetValue(realWorldScenePos, out valuer);
                                smallChunker[] worldChunky = valuer;
                                smallChunkCreator(realWorldScenePos, worldChunky);
                            }
                        }
                    }
                }
            }
        }
        catch
        {

        }
        finally
        {

        }
    }



    void smallChunkCreator(Vector3 viewerPosition, smallChunker[] worldChunk)
    {
        //viewerPosition = data.viewerPos;

        //smallChunkArray = new smallChunker[3 * 3 * 3];

        float sizerX = smallChunkWidth;
        float sizerY = smallChunkWidthY;
        float sizerZ = smallChunkWidth;

        for (float xx = -sizerX; xx < sizerX + sizerX; xx += smallChunkWidth)
        {
            for (float yy = -sizerY; yy < sizerY + sizerY; yy += smallChunkWidthY)
            {
                for (float zz = -sizerZ; zz < sizerZ + sizerZ; zz += smallChunkWidth)
                {
                    Vector3 currentPosition = new Vector3(xx, yy, zz);

                    Vector3 realPos = currentPosition + viewerPosition;

                    int smallX;
                    int smallY;
                    int smallZ;

                    getPosSmall(xx, yy, zz, out smallX, out smallY, out smallZ, smallChunkWidth, smallChunkWidthY, smallChunkWidth, 2);

                    int smallerX;
                    int smallerY;
                    int smallerZ;

                    getPosSmall(xx, yy, zz, out smallerX, out smallerY, out smallerZ, smallChunkWidth, smallChunkWidthY, smallChunkWidth, 2);

                    /*GameObject areaChunkPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    areaChunkPoint.transform.position = realPos;
                    Vector3 scaler = areaChunkPoint.transform.localScale;
                    scaler.x *= 2;
                    scaler.z *= 2;
                    scaler.y *= 0.1f;
                    areaChunkPoint.transform.localScale = scaler;*/
                    lock (tempSmallWrite)
                    {
                        if (worldChunk[smallX + width * (smallerY + height * smallZ)] == null)
                        {
                            //Debug.Log(smallerY);

                            worldChunk[smallX + width * (smallerY + height * smallZ)] = new smallChunker();

                            /*GameObject areaChunkPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            areaChunkPoint.transform.position = realPos;
                            Vector3 scaler = areaChunkPoint.transform.localScale;
                            scaler.x *= 0.1f;
                            scaler.z *= 0.1f;
                            scaler.y *= 0.1f;
                            areaChunkPoint.transform.localScale = scaler;*/

                            bool resultsOne = false;
                            bool resultsTwo = false;
                            bool resultsThree = false;

                            checkingBytes checkBytes = new checkingBytes(realPos, smallX, smallerY, smallZ, resultsOne, resultsTwo, resultsThree, worldChunk,null);

                            lock (firstQueue)
                            {
                                firstQueue.Enqueue(checkBytes);
                            }
                        }
                    }    
                }
            }
        }
        //lock (firstQueue)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(checkLastBytes), firstQueue);
        }
        //checkLastBytes(firstQueue);
    }





    public class checkingBytes
    {
        public Vector3 smallChunkPos;

        public int smallX;
        public int smallY;
        public int smallZ;
        //public regionChunker regionChunko;
        //public regionChunker[] worldChunk;
        public Vector3 worldScenePos;
        public bool resultsOne;
        public bool resultsTwo;
        public bool resultsThree;
        public smallChunker[] worldChunk;
        public byte[] map; 

        public checkingBytes(Vector3 smallChunkPos, int smallX, int smallY, int smallZ, bool resultsOne, bool resultsTwo, bool resultsThree,smallChunker[] worldChunk,byte[] map)
        {
            this.smallChunkPos = smallChunkPos;
            this.resultsOne = resultsOne;
            this.resultsTwo = resultsTwo;
            this.resultsThree = resultsThree;

            this.smallX = smallX;
            this.smallY = smallY;
            this.smallZ = smallZ;

            this.worldChunk = worldChunk;
            this.map = map;

        }
    }



    void checkLastBytes(object stateInfo)
    {
        Queue<checkingBytes> queuer = (Queue<checkingBytes>)stateInfo;
        lock (queuer)
        {
            for (int i = 0; i < queuer.Count; i++)
            {
                checkingBytes byteCheck = queuer.Dequeue();

                Vector3 smallChunkPos = byteCheck.smallChunkPos;

                byte[] map = new byte[realChunkWidth * realChunkHeight * realChunkDepth];
                byte[] realMap = new byte[realChunkWidth * realChunkHeight * realChunkDepth];
                for (int xx = 0; xx < realChunkWidth; xx++)
                {
                    //float noiseX = Math.Abs(((float)(xx * planeSize + smallChunkPos.x + seed) / detailScale) * heightScale);

                    for (int yy = 0; yy < realChunkHeight; yy++)
                    {
                        //float noiseY = Math.Abs(((float)(yy * planeSize + smallChunkPos.y + seed) / detailScale) * heightScale);

                        for (int zz = 0; zz < realChunkDepth; zz++)
                        {
                            //float noiseZ = Math.Abs(((float)(zz * planeSize + smallChunkPos.z + seed) / detailScale) * heightScale);

                            float temporaryY = 10f;

                            temporaryY *= Mathf.PerlinNoise((xx * planeSize + smallChunkPos.x + seed) / detailScale, (zz * planeSize + smallChunkPos.z + seed) / detailScale) * heightScale;

                            float size0 = (1 / planeSize) * smallChunkPos.y;
                            temporaryY -= size0;

                            if ((int)Math.Round(temporaryY) >= yy)
                            {
                                realMap[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 1;

                            }
                            else
                            {
                                realMap[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 0;
                            }

                            if ((int)Math.Floor(temporaryY) >= yy + 1)
                            {
                                map[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 2;

                            }

                            else if ((int)Math.Floor(temporaryY) < yy - 1)
                            {
                                map[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 1;
                                //realMap[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 0;
                            }
                            else
                            {
                                map[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 0;
                                
                            }
                        }
                    }
                }

                bool resultsOne = Array.TrueForAll(map, s => s == 0);
                bool resultsTwo = Array.TrueForAll(map, s => s == 1);
                bool resultsThree = Array.TrueForAll(map, s => s == 2);

                byteCheck.resultsOne = resultsOne;
                byteCheck.resultsTwo = resultsTwo;
                byteCheck.resultsThree = resultsThree;
                byteCheck.map = realMap;

                lock (nextQueue)
                {
                    nextQueue.Enqueue(byteCheck);
                }
            }
        }       
    }
















    int xxx;
    int yyy;
    int zzz;

    public void getPosSmall(float xi, float yi, float zi, out int xer, out int yer, out int zer, float dividerX, float dividerY, float dividerZ, int chunkSize)
    {
        int x = (Mathf.RoundToInt(xi / dividerX));
        int y = (Mathf.RoundToInt(yi / dividerY));
        int z = (Mathf.RoundToInt(zi / dividerZ));

        //Debug.Log(y);



        if (x < 0)
        {
            int yo = Mathf.Abs(x) % chunkSize;
            int yo1 = yo + (chunkSize - 1);
            xxx = yo1;
        }
        else
        {
            xxx = x % chunkSize;
        }

        if (y < 0)
        {
            int yo = Mathf.Abs(y) % chunkSize;
            //Debug.Log(yo);
            int yo1 = yo + (chunkSize - 1);
            yyy = yo1;
        }
        else
        {
            yyy = y % chunkSize;
        }

        if (z < 0)
        {
            int yo = Mathf.Abs(z) % chunkSize;
            int yo1 = yo + (chunkSize - 1);
            zzz = yo1;
        }
        else
        {
            zzz = z % chunkSize;
        }
        xer = xxx;
        yer = yyy;
        zer = zzz;
    }


}

public class universe : MonoBehaviour
{
    public GameObject viewer;
    Vector3 viewerPosition;
    public GameObject chunker;

    public static int worldChunkWidth = 81;
    public static int regionChunkWidth = 27;
    public static int areaChunkWidth = 9;
    public static int bigChunkWidth = 3;
    public static int smallChunkWidth = 1;


    /*public static float worldChunkWidth = 8.1f;
    public static float regionChunkWidth = 2.7f;
    public static float areaChunkWidth = 0.9f;
    public static float bigChunkWidth = 0.3f;
    public static float smallChunkWidth = 0.1f;*/


    /*public static int worldChunkWidth = 162;
    public static int regionChunkWidth = 54;
    public static int areaChunkWidth = 18;
    public static int bigChunkWidth = 6;
    public static int smallChunkWidth = 2;*/

    /*public static int worldChunkWidth = 324;
    public static int regionChunkWidth = 108;
    public static int areaChunkWidth = 36;
    public static int bigChunkWidth = 12;
    public static int smallChunkWidth = 4;*/


    Vector3 realScenePos;
    public static bool startCheckingChunkData = false;
    int currentChunkCoordX;
    int currentChunkCoordY;
    int currentChunkCoordZ;
    Vector3 currentPosition;

    bool isMoving = false;

    void Start()
    {
        viewerPosition = viewer.transform.position;
    }

    testerOfNumber Myclass = new testerOfNumber();
    void Update()
    {
        viewerPosition = viewer.transform.position;

        StartCoroutine(CheckMoving());

        int realSceneX = (int)Mathf.Round(viewerPosition.x / bigChunkWidth);
        int realSceneY = (int)Mathf.Round(viewerPosition.y / bigChunkWidth);
        int realSceneZ = (int)Mathf.Round(viewerPosition.z / bigChunkWidth);
        realScenePos = new Vector3(realSceneX * bigChunkWidth, realSceneY * bigChunkWidth, realSceneZ * bigChunkWidth);

        if (isMoving == true)
        {
            //Debug.Log("test");
            Myclass.realScenePos = realScenePos;
            Myclass.viewerPosition = viewerPosition;
            testerOfNumber.newScenePos yo = new testerOfNumber.newScenePos(realScenePos, viewerPosition);
            Myclass.AccessGlobalResource(yo);

            /*Myclass.realScenePos = realScenePos;
            Myclass.viewerPosition = viewerPosition;
            testerOfNumber.newScenePos yo = new testerOfNumber.newScenePos(realScenePos, viewerPosition);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Myclass.AccessGlobalResource), yo);*/
        }

        if (testerOfNumber.nextQueue.Count > 0)
        {
            StartCoroutine(buildChunks());
        }
    }


    IEnumerator buildChunks()
    {
        for (int i = 0; i < testerOfNumber.nextQueue.Count;i++)
        {
            testerOfNumber.checkingBytes byteCheck = testerOfNumber.nextQueue.Dequeue();
            if (byteCheck != null)
            {
                bool resultsOne = byteCheck.resultsOne;
                bool resultsTwo = byteCheck.resultsTwo;
                bool resultsThree = byteCheck.resultsThree;

                if (!resultsOne && !resultsThree && !resultsTwo)
                {
                    //if (byteCheck.worldChunk[byteCheck.smallX + realChunkWidth * (byteCheck.smallY + realChunkHeight * byteCheck.smallZ)] == null)
                    {
                        //byteCheck.worldChunk[byteCheck.smallX + realChunkWidth * (byteCheck.smallY + realChunkHeight * byteCheck.smallZ)] = new smallChunker();
                        chunk chunky = new chunk();
                        meshData meshDator = chunky.startBuildingArray(byteCheck.smallChunkPos, byteCheck.smallX, byteCheck.smallY, byteCheck.smallZ,byteCheck.map);

                        GameObject colissdeCriss = GameObject.FindGameObjectWithTag("chunk");
                        GameObject currentChunk = (GameObject)Instantiate(colissdeCriss, new Vector3(meshDator.trueRealPos.x, meshDator.trueRealPos.y, meshDator.trueRealPos.z), Quaternion.identity);

                        currentChunk.transform.parent = (Transform)GameObject.FindGameObjectWithTag("terrain").transform;

                        Mesh mesh = currentChunk.GetComponent<MeshFilter>().mesh;
                        mesh.vertices = meshDator.positions.ToArray();
                        mesh.triangles = meshDator.triangleIndices.ToArray();
                        mesh.RecalculateNormals();

                        yield return new WaitForSeconds(0.01f);
                    }
                }
            }          
        }      
    }








   


    IEnumerator CheckMoving()
    {
        currentChunkCoordX = Mathf.RoundToInt(viewer.transform.position.x / (smallChunkWidth * 0.5f));
        currentChunkCoordY = Mathf.RoundToInt(viewer.transform.position.y / (smallChunkWidth * 0.5f));
        currentChunkCoordZ = Mathf.RoundToInt(viewer.transform.position.z / (smallChunkWidth * 0.5f));

        //float currentChunkCoordX = viewer.transform.position.x;
        //float currentChunkCoordY = viewer.transform.position.y ;
        //float currentChunkCoordZ = viewer.transform.position.z ;

        currentPosition = new Vector3(currentChunkCoordX, currentChunkCoordY, currentChunkCoordZ);

        Vector3 startPos = currentPosition;
        yield return new WaitForSeconds(0.001f);
        Vector3 finalPos = currentPosition;

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
    }
}


















public struct meshData
{
    //public readonly smallChunk smallChunk;
    //public readonly bigChunk bigChunk;
    //public readonly Vector3 fakePosition;
    public readonly int lengthOfArray;
    public Vector3[] positions;
    public int[] triangleIndices;
    public Vector3 trueRealPos;
    //public GameObject currentChunkObject;
    //public byte[] mapByte;
    /*public int regionX;
    public int regionY;
    public int regionZ;
    public int areaX;
    public int areaY;
    public int areaZ;
    public int bigX;
    public int bigY;
    public int bigZ;*/
    public int smallX;
    public int smallY;
    public int smallZ;
    //public regionChunker regionChunko;
    //public regionChunker[] worldChunk;
    //public areaChunker areaChunko;
    //public bigChunker bigChunko;
    /*public bool hasMapGenerated;
    public bool hasMeshGenerated;
    public bool hasBeenSpawned;
    public bool distanceRejected;
    public Vector3 worldScenePos;*/


    public meshData(Vector3 realPos, int lengthOfArray, Vector3[] positions, int[] triangleIndices, int smallX, int smallY, int smallZ)
    {
        //this.smallChunk = smallChunk;
        //this.bigChunk = bigChunk;
        //this.fakePosition = fakePos;
        this.lengthOfArray = lengthOfArray;
        this.positions = positions;
        this.triangleIndices = triangleIndices;
        this.trueRealPos = realPos;
        //this.currentChunkObject = currentChunk;
        //this.mapByte = meshData;
        //this.regionX = regionX;
        //this.regionY = regionY;
        //this.regionZ = regionZ;
        //this.areaX = areaX;
        /*this.areaY = areaY;
        this.areaZ = areaZ;
        this.bigX = bigX;
        this.bigY = bigY;
        this.bigZ = bigZ;*/
        this.smallX = smallX;
        this.smallY = smallY;
        this.smallZ = smallZ;
        /*this.regionChunko = regionChunko;
        this.hasBeenSpawned = hasSpawned;
        this.hasMapGenerated = hasMap;
        this.hasMeshGenerated = hasMesh;
        this.distanceRejected = distReject;
        this.worldChunk = worldChunk;
        this.worldScenePos = worldScenePos;*/

        //this.areaChunko = areaChunko;
        //this.bigChunko = bigChunko;
    }
}