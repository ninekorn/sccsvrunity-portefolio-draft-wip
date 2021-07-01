using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Collections.Generic;
using SPINACH.iSCentralDispatch;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

//mucho stuff from Sebastian Lague terrain mixed with Holistic3D mixed with my chunk index knowledge



public class regionChunker
{
    regionChunker regionChunku;
    public areaChunker[,,] areaChunky;
    public areaChunker[,,] areaChunkyArray;
    //public bigChunk[,,] bigChunkerArray { get; set; }
    public regionChunker[,,] regionChunkyList = new regionChunker[20, 20, 20];
    public Vector3 worldPosition;

    public regionChunker()
    {

    }
}

public class areaChunker
{
    areaChunker areaChunku;
    public bigChunker[,,] bigChunky;
    public bigChunker[,,] bigChunkyArray;
    //public bigChunk[,,] bigChunkerArray { get; set; }
    public areaChunker[,,] areaChunkyList = new areaChunker[20, 20, 20];
    public Vector3 worldPosition;

    public areaChunker()
    {

    }
}

public class bigChunker
{

    public Vector3 worldAreaPosition;
    public smallChunker[,,] smallChunkerList;
    areaChunker areaChunky;
    public bigChunker[,,] bigChunky;
    public bigChunker[,,] bigChunkyArray;

    public bigChunker()
    {

    }
}

public class smallChunker
{
    public Vector3 worldPosition;
    private int width = 10;
    private int height = 1;
    private int depth = 10;
    public byte[,,] map;
    smallChunker chuk;

    public Vector3[] positionz;
    public Vector3[] normalz;
    public Vector2[] textureCoordinatez;
    public int[] triangleIndicez;
    public static smallChunker[] arrayOfSmallChunks = new smallChunker[2 * 2 * 2];
    public static int counter = 0;
    public GameObject currentChunk;

    public smallChunker()
    {

    }
}

public class universe : MonoBehaviour
{
    Queue<meshThreadInfo<meshData>> meshDataThreadInfoQueue = new Queue<meshThreadInfo<meshData>>();
    float planeSize = 0.1f;
    public GameObject chunker;
    Vector3 viewerPosition;
    public Transform viewer;
    int chunkRealSize = 5;
    int chunksVisibleInViewDst = 10;

    //OculusProjektV0.bigChunk bigChunker;
    //OculusProjektV0.areaChunk areaChunker;
    //OculusProjektV0.regionChunk regionChunk;

    bigChunker bigChunky;
    areaChunker areaChunky;
    regionChunker regionChunky;

    public GameObject targetAreaChunkPos;
    public GameObject targetBigChunkPos;
    public GameObject targetSmallChunkPos;
    public GameObject sceneLight;


    int chunkWidth = 10;
    int threadID0;

    Dictionary<Vector3, Dictionary<Scene, regionChunker>> sceneList = new Dictionary<Vector3, Dictionary<Scene, regionChunker>>();
    public GameObject terrainGenerator;

    int regionChunkWidth = 8;
    int areaChunkWidth = 4;
    int bigChunkWidth = 2;
    int smallChunkWidth = 1;






    private void Start()
    {
        //bigChunky = new bigChunker();
        //bigChunky.smallChunkerList = new smallChunker[10, 10, 10];

        areaChunky = new areaChunker();
        areaChunky.bigChunkyArray = new bigChunker[20, 20, 20];

        regionChunky = new regionChunker();
        regionChunky.areaChunkyArray = new areaChunker[20, 20, 20];


        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x);// FOR ARRAY
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y);// FOR ARRAY
        int currentChunkCoordZ = Mathf.RoundToInt(viewerPosition.z);// FOR ARRAY

        int sceneX = currentChunkCoordX / regionChunkWidth;
        int sceneY = currentChunkCoordY / regionChunkWidth;
        int sceneZ = currentChunkCoordZ / regionChunkWidth;

        Vector3 scenePos = new Vector3(sceneX * regionChunkWidth, sceneY * regionChunkWidth, sceneZ * regionChunkWidth);
        Scene currentSceneName = SceneManager.GetActiveScene();
        Dictionary<Scene, regionChunker> tempDictionary = new Dictionary<Scene, regionChunker>();

        if (!tempDictionary.ContainsKey(currentSceneName))
        {
            tempDictionary.Add(currentSceneName, regionChunky);
        }
        if (!sceneList.ContainsValue(tempDictionary))
        {
            sceneList.Add(scenePos, tempDictionary);
        }
        lastPosition = new Vector3(currentChunkCoordX, currentChunkCoordY, currentChunkCoordZ);
        fakePosition = Vector3.zero;
    }

    void Update()
    {
        viewerPosition = new Vector3(viewer.position.x, viewer.position.y, viewer.position.z);




        RequestMapData(OnMapDataReceived, viewerPosition); // position, 

        if (meshDataThreadInfoQueue.Count > 0)
        {
            meshThreadInfo<meshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
            threadInfo.callback(threadInfo.parameter);
        }
    }


    public void RequestMapData(Action<meshData> callback, Vector3 viewerPosition) // Vector2 centre
    {
        /*ThreadStart threadStart = delegate {
            MapDataThread(callback, viewerPosition); // centre
        };
        new Thread(threadStart).Start();*/

        /*threadID0 = iSCentralDispatch.DispatchNewThread(() =>
        {
            MapDataThread(callback, viewerPosition);
        });*/
        MapDataThread(callback, viewerPosition);
    }

    void OnMapDataReceived(meshData mapData)
    {
        StartCoroutine(UpdateTerrainChunk(mapData));
    }

    IEnumerator UpdateTerrainChunk(meshData meshdata)
    {
        if (meshdata.lengthOfArray > 0)
        {
            GameObject currentChunk = (GameObject)Instantiate(chunker, new Vector3(meshdata.fakePosition.x, meshdata.fakePosition.y, meshdata.fakePosition.z), Quaternion.identity);
            //currentChunk.SetActive(false);

            ////////////AREA
            int areaPosX = (int)meshdata.fakePosition.x / areaChunkWidth;
            int areaPosY = (int)meshdata.fakePosition.y / areaChunkWidth;
            int areaPosZ = (int)meshdata.fakePosition.z / areaChunkWidth;
            Vector3 realPos5 = new Vector3(areaPosX * areaChunkWidth, areaPosY * areaChunkWidth, areaPosZ * areaChunkWidth);
            int areaX;
            int areaY;
            int areaZ;
            getPos((int)meshdata.fakePosition.x, (int)meshdata.fakePosition.y, (int)meshdata.fakePosition.z, out areaX, out areaY, out areaZ, areaChunkWidth);
            ////////////AREA

            //////////////BIGCHUNK
            int bigChunkPosX = (int)meshdata.fakePosition.x / bigChunkWidth;
            int bigChunkPosY = (int)meshdata.fakePosition.y / bigChunkWidth;
            int bigChunkPosZ = (int)meshdata.fakePosition.z / bigChunkWidth;
            Vector3 realPos4 = new Vector3(bigChunkPosX * bigChunkWidth, bigChunkPosY * bigChunkWidth, bigChunkPosZ * bigChunkWidth);

            int bigXX;
            int bigYY;
            int bigZZ;
            getPos((int)meshdata.fakePosition.x, (int)meshdata.fakePosition.y, (int)meshdata.fakePosition.z, out bigXX, out bigYY, out bigZZ, bigChunkWidth);
            ///////////////BIGCHUNK

            //////////////SMALLCHUNK
            int smallChunkPosX = (int)meshdata.fakePosition.x / 1;
            int smallChunkPosY = (int)meshdata.fakePosition.y / 1;
            int smallChunkPosZ = (int)meshdata.fakePosition.z / 1;
            Vector3 realPos3 = new Vector3(smallChunkPosX * 1, smallChunkPosY * 1, smallChunkPosZ * 1);

            int smallXX;
            int smallYY;
            int smallZZ;
            getPos((int)meshdata.fakePosition.x, (int)meshdata.fakePosition.y, (int)meshdata.fakePosition.z, out smallXX, out smallYY, out smallZZ, 1);
            //////////SMALLCHUNK

            //regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ].currentChunk = currentChunk;

            //iSCentralDispatch.DispatchMainThread(() =>
            //{
            Mesh mesh = currentChunk.GetComponent<MeshFilter>().mesh;
            mesh.vertices = meshdata.positions.ToArray();
            mesh.triangles = meshdata.triangleIndices.ToArray();
            mesh.RecalculateNormals();
            currentChunk.AddComponent<MeshCollider>();
            currentChunk.GetComponent<MeshCollider>().sharedMesh = mesh;
            //});
        }

        yield return new WaitForSeconds(1);
    }

    void MapDataThread(Action<meshData> callback, Vector3 viewerPosition)
    {
        //meshDataThread(callback, viewerPosition);
        buildAreaChunk(callback, viewerPosition);
        //StartCoroutine(meshDataThread(callback, viewerPosition));
    }

    bool getOnce = false;


   



    bool getCurrentScenePos(Vector3? scenePos)
    {
        Vector3 currentPos = new Vector3(scenePos.Value.x, scenePos.Value.y, scenePos.Value.z);

        Scene currentSceneName = SceneManager.GetActiveScene();

        if (sceneList.ContainsKey(currentPos))
        {          
            Vector3 value = currentPos;
            //Debug.Log(value);
            //sceneList.TryGetValue(currentSceneName,out regionChunk.worldPosition);

            return true;
        }
        return false;
    }

    Vector3 getCurrentScene() // this function doesnt work at all.....
    {
        Scene currentSceneName = SceneManager.GetActiveScene();

        var tempDictionary = new Dictionary<Scene, regionChunker>();
        if (!tempDictionary.ContainsKey(currentSceneName))
        {
            tempDictionary.Add(currentSceneName, regionChunky);
        }

        if (sceneList.ContainsValue(tempDictionary))
        {
            var myKey = sceneList.FirstOrDefault(x => x.Value == tempDictionary).Key;
            return myKey;
        }
        return new Vector3(9,9,9);
    }

    int currentChunkCoordX;
    int currentChunkCoordY;
    int currentChunkCoordZ;
    Vector3 tempPosition;

    void resetPlayerPosition()
    {
        if (viewer.position != Vector3.zero)
        {
            viewer.transform.position = Vector3.zero;// Vector3.zero;
        }
    }

    Vector3 lastPosition;




    bool getLastPosData = false;

    int addonX;
    int addonY;
    int addonZ;

    float xi;
    float yi;
    float zi;
    Vector3 fakePosition;

    void setRealPosition(float x, float y, float z)
    {
        xi += Mathf.Round(x * 1);
        yi += Mathf.Round(y * 1);
        zi += Mathf.Round(z * 1);
        fakePosition = new Vector3(xi, yi, zi);
    }


    void buildAreaChunk(Action<meshData> callback, Vector3 viewerPosition)
    {
        currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x);// FOR ARRAY
        currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y);// FOR ARRAY
        currentChunkCoordZ = Mathf.RoundToInt(viewerPosition.z);// FOR ARRAY

        int sceneX = (int)currentChunkCoordX / regionChunkWidth;
        int sceneY = (int)currentChunkCoordY / regionChunkWidth;
        int sceneZ = (int)currentChunkCoordZ / regionChunkWidth;

        Vector3 scenePos = new Vector3(sceneX * regionChunkWidth, sceneY * regionChunkWidth, sceneZ * regionChunkWidth);

        if (scenePos.x >= regionChunkWidth || scenePos.x <= -regionChunkWidth
         || scenePos.y >= regionChunkWidth || scenePos.y <= -regionChunkWidth
         || scenePos.z >= regionChunkWidth || scenePos.z <= -regionChunkWidth)
        {
            setRealPosition(scenePos.x, scenePos.y, scenePos.z);
            viewerPosition = Vector3.zero;
            resetPlayerPosition();
        }

        if (getCurrentScenePos(fakePosition) == false)
        {
            if (!sceneList.ContainsKey(fakePosition))
            {
                //resetPlayerPosition();
                Scene newScene = SceneManager.CreateScene(fakePosition.ToString());

                var tempDictionary = new Dictionary<Scene, regionChunker>();
                if (!tempDictionary.ContainsKey(newScene))
                {
                    tempDictionary.Add(newScene, regionChunky);
                }

                regionChunky = new regionChunker();
                regionChunky.areaChunkyArray = new areaChunker[20, 20, 20];
                sceneList.Add(fakePosition, tempDictionary);

                


                SceneManager.MoveGameObjectToScene(viewer.gameObject, newScene);
                SceneManager.MoveGameObjectToScene(terrainGenerator, newScene);
                SceneManager.MoveGameObjectToScene(sceneLight, newScene);
                //EditorApplication.SaveScene("Asset/scene.unity", true);
                SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
                //SceneManager.LoadScene("criss",LoadSceneMode.Additive);
                //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("criss");
                SceneManager.SetActiveScene(newScene);
                viewerPosition = Vector3.zero;
                //resetPlayerPosition();
                Debug.Log("test0");
            }
            else if (sceneList.ContainsKey(fakePosition)) //yessssss
            {
                Debug.Log("test1");
                //currentChunkCoordX += (int)fakePosition.x;
                //currentChunkCoordY += (int)fakePosition.y;
                //currentChunkCoordZ += (int)fakePosition.z;
                //viewerPosition = Vector3.zero;
                //resetPlayerPosition();
            }
        }


        currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x);// FOR ARRAY
        currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y);// FOR ARRAY
        currentChunkCoordZ = Mathf.RoundToInt(viewerPosition.z);// FOR ARRAY

        //currentChunkCoordX = (int)fakePosition.x;
        //currentChunkCoordY = (int)fakePosition.y;
        //currentChunkCoordZ = (int)fakePosition.z;

        //currentChunkCoordX += (int)fakePosition.x;
        //currentChunkCoordY += (int)fakePosition.y;
        //currentChunkCoordZ += (int)fakePosition.z;

        for (int x = currentChunkCoordX - 2; x < currentChunkCoordX + 2; x++)
        {
            for (int y = currentChunkCoordY; y < currentChunkCoordY + 2; y++)
            {
                for (int z = currentChunkCoordZ - 2; z < currentChunkCoordZ + 2; z++)
                {
                   
                    int smallX = x;
                    int smallY = y;
                    int smallZ = z;

                    /////////////////////////////AREA///////////////////
                    int areaPosX = x / areaChunkWidth;
                    int areaPosY = y / areaChunkWidth;
                    int areaPosZ = z / areaChunkWidth;
                    Vector3 realPos5 = new Vector3(areaPosX * areaChunkWidth, areaPosY * areaChunkWidth, areaPosZ * areaChunkWidth);
                    int areaX;
                    int areaY;
                    int areaZ;
                    getPos(x, y, z, out areaX, out areaY, out areaZ, areaChunkWidth);
                    /////////////////////////////AREA///////////////////


                    if (regionChunky.areaChunkyArray[areaX, areaY, areaZ] == null)
                    {                  
                        //Debug.Log(areaX + " " + areaY + " " + areaZ);
                        regionChunky.areaChunkyArray[areaX, areaY, areaZ] = new areaChunker();
                        regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray = new bigChunker[20,20,20];
                        regionChunky.areaChunkyArray[areaX, areaY, areaZ].worldPosition = realPos5;
                        Instantiate(targetAreaChunkPos, realPos5, Quaternion.identity);
                    }
                    else
                    {
                        /////////////////////////////BIGCHUNK///////////////////
                        int bigChunkPosX = x / bigChunkWidth;
                        int bigChunkPosY = y / bigChunkWidth;
                        int bigChunkPosZ = z / bigChunkWidth;
                        Vector3 realPos4 = new Vector3(bigChunkPosX * bigChunkWidth, bigChunkPosY * bigChunkWidth, bigChunkPosZ * bigChunkWidth);

                        int bigXX;
                        int bigYY;
                        int bigZZ;
                        getPos(x, y, z, out bigXX, out bigYY, out bigZZ, bigChunkWidth);
                        /////////////////////////////BIGCHUNK///////////////////
                        if (regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ] == null)
                        {
                            regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ] = new bigChunker();
                            regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList = new smallChunker[20, 20, 20];
                            regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].worldAreaPosition = realPos4;
                            Instantiate(targetBigChunkPos, realPos4, Quaternion.identity);
                        }

                        else
                        {
                            /////////////////////////////SMALLCHUNK///////////////////
                            int testerX = x - (int)fakePosition.x;
                            int testerY = y - (int)fakePosition.y;
                            int testerZ = z - (int)fakePosition.z;

                            int smallChunkPosX = x / 1;
                            int smallChunkPosY = y / 1;
                            int smallChunkPosZ = z / 1;

                            Vector3 fakePos = new Vector3(smallChunkPosX * 1, smallChunkPosY * 1, smallChunkPosZ * 1);
                            Vector3 truePos = new Vector3(testerX * 1, testerY * 1, testerZ * 1);

                            int smallXX;
                            int smallYY;
                            int smallZZ;
                            getPos(x, y, z, out smallXX, out smallYY, out smallZZ, 1);
                            /////////////////////////////SMALLCHUNK///////////////////

                            if (regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ] == null)
                            {
                                regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ] = new smallChunker();
                                regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ].worldPosition = fakePos;
                                regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ].currentChunk = null;
                                chunk chunki = new chunk();
                                //Debug.Log(truePos);
                                meshData meshDator = chunki.startBuildingArray(truePos, fakePos);

                                if (meshDator.lengthOfArray > 0)
                                {
                                    lock (meshDataThreadInfoQueue)
                                    {
                                        meshDataThreadInfoQueue.Enqueue(new meshThreadInfo<meshData>(callback, meshDator));
                                    }
                                }
                            }

                            /*else if (Vector3.Distance(viewer.position, realPos3) >= 2)
                            {
                                if (regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ].currentChunk != null)
                                {
                                    if (regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ].currentChunk.gameObject.active == true)
                                    {
                                        regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ].currentChunk.gameObject.SetActive(false);
                                    }
                                }
                            }
                            else if (Vector3.Distance(viewer.position, realPos3) < 2)
                            {
                                if (regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ].currentChunk != null)
                                {
                                    if (regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ].currentChunk.gameObject.active == false)
                                    {
                                        regionChunky.areaChunkyArray[areaX, areaY, areaZ].bigChunkyArray[bigXX, bigYY, bigZZ].smallChunkerList[smallXX, smallYY, smallZZ].currentChunk.gameObject.SetActive(true);
                                    }
                                }
                            }*/
                        }
                    }
                }
            }
        }
        //lastPosition = new Vector3(currentChunkCoordX, currentChunkCoordY, currentChunkCoordZ);
    }

    int xxx;
    int yyy;
    int zzz;

    void getPos(int x, int y, int z, out int xer, out int yer, out int zer, int divider)
    {
        x /= divider;
        y /= divider;
        z /= divider;

        if (x < 0)
        {
            int yo = Mathf.Abs(x) % 10;
            int yo1 = yo + 10;
            xxx = yo1;

        }
        else
        {
            xxx = x % 10;
        }


        if (y < 0)
        {
            int yo = Mathf.Abs(y) % 10;
            int yo1 = yo + 10;
            yyy = yo1;

        }
        else
        {
            yyy = y % 10;
        }

        if (z < 0)
        {
            int yo = Mathf.Abs(z) % 10;
            int yo1 = yo + 10;
            zzz = yo1;

        }
        else
        {
            zzz = z % 10;
        }

        xer = xxx;
        yer = yyy;
        zer = zzz;
    }

    








    public bool getDaBigChunkInAreaChunk(bigChunker chuk, int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= chunkWidth) || (y >= chunkWidth) || (z >= chunkWidth))
        {
            return false;
        }
        if (chuk == null)
        {
            return false;
        }
        return true;
    }

    public bool getDaAreaChunk(areaChunker chuk, int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= chunkWidth) || (y >= chunkWidth) || (z >= chunkWidth))
        {
            return false;
        }
        if (chuk == null)
        {
            return false;
        }
        return true;
    }
    /*public smallChunk getDaChunk(smallChunk[,,] chuk, int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= chunkTinyWidth) || (y >= chunkTinyHeight) || (z >= chunkTinyDepth))
        {
            Debug.Log("outside range");
            return null;
        }
        return chuk[x, y, z];
    }*/












    /*void createArray(int sizeX, int sizeY, int sizeZ)
     {

     }*/


    struct meshThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T parameter;

        public meshThreadInfo(Action<T> callback, T parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
        }
    }
}

public struct meshData
{
    //public readonly smallChunk smallChunk;
    //public readonly bigChunk bigChunk;
    public readonly Vector3 fakePosition;
    public readonly int lengthOfArray;
    public Vector3[] positions;
    public int[] triangleIndices;
    public Vector3 trueRealPos;
    public meshData(Vector3 fakePos,Vector3 realPos, int lengthOfArray, Vector3[] positions, int[] triangleIndices)
    {
        //this.smallChunk = smallChunk;
        //this.bigChunk = bigChunk;
        this.fakePosition = fakePos;
        this.lengthOfArray = lengthOfArray;
        this.positions = positions;
        this.triangleIndices = triangleIndices;
        this.trueRealPos = realPos;
    }
}













/*if (realPos3.x > 10)
               {
                   if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName("criss").IsValid())
                   {
                       Debug.Log("yo");
                       Scene newScene = SceneManager.CreateScene("criss");
SceneManager.MoveGameObjectToScene(viewer.gameObject, newScene);

                       //EditorApplication.SaveScene("Asset/criss.unity", true);
                       SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
                       //SceneManager.LoadScene("criss",LoadSceneMode.Additive);
                       //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("criss");
                       SceneManager.SetActiveScene(newScene);
                   }
               }

               if (realPos3.x< 10 && realPos3.x> 0)
               {
                   if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName("criss").isLoaded)
                   {
                       Scene scene = SceneManager.GetSceneAt(0);
SceneManager.LoadScene("VR-Projekt", LoadSceneMode.Additive);
                       SceneManager.MoveGameObjectToScene(viewer.gameObject, scene);
                       //EditorApplication.SaveScene("Asset/criss.unity", true);
                       SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
                       //SceneManager.LoadScene("criss",LoadSceneMode.Additive);
                       //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("criss");
                       SceneManager.SetActiveScene(scene);
                   }
               }*/








/*bool transitionIsDone = false;
IEnumerator loadNextScene()
{
    string name = "ALegitSceneName";
    AsyncOperation _async = new AsyncOperation();
    _async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    _async.allowSceneActivation = false;

    while (!transitionIsDone)
    {
        yield return null;
    }

    _async.allowSceneActivation = true;

    while (!_async.isDone)
    {
        yield return null;
    }

    Scene nextScene = SceneManager.GetSceneByName(name);
    if (nextScene.IsValid())
    {
        SceneManager.SetActiveScene(nextScene);
        SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
    }
}*/




//int index = sceneList.Values.ToList().IndexOf(tempDictionary);

//sceneList[index]

/*var enumerator = sceneList.GetEnumerator();
while (enumerator.MoveNext())
{
    var current = enumerator.Current;
    var keyer = current.Key;
    var valuer = current.Value;
}*/

//Vector3 key = sceneList;
//Debug.Log(value);
//sceneList.TryGetValue(currentSceneName,out regionChunk.worldPosition);