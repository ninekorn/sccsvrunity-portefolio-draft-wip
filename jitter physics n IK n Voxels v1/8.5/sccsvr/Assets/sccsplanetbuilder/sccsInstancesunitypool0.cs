using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class sccsInstancesunitypool0 : MonoBehaviour
{


    public static sccsInstancesunitypool0 current;
    public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = true;
    List<GameObject> pooledObjects;

    public Transform unitytutorialpoolworldposition;

    public int instances;
    public Vector3 maxPos;
    public Mesh objMesh;
    public Material objMat;

    //private List<List<instancedata>> batches = new List<List<instancedata>>();
    instancedata[][] batches;// = new instancedata[][];
    //instancedata[][] someinstancedata;


    private static int width = 10;
    private static int height = 10;
    private static int depth = 10;

    Vector3 _chunkPos;

    float planeSize = 0.1f;

    public int maininstanceindexarraypointerlocation = 0;
    instancedata[] currBatch;

    public int batchobjectindexouttodraw = 0;
    public int batchinstanceindexouttodraw = 0;

    public int batchobjectindexdatain = 0;
    public int batchinstanceindexdatain = 0;


    public void Awake()
    {
        current = this;
        unitytutorialpoolworldposition = this.transform;

    }


    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

        //someinstancedata = new instancedata[pooledAmount][];
        batches = new instancedata[pooledAmount][];

        //objMesh = pooledObject.transform.gameObject.GetComponent<MeshFilter>().mesh;




        int batchIndexNum = 0;

        //List<instancedata> currBatch = new List<instancedata>();

        // = new instancedata[];

        //AddObj(currBatch, 1);
        //batches.Add(currBatch);
        //currBatch = BuildNewBatch();

        Vector3 instanceposition = this.transform.position;
        someInstances = new instancedata[batches.Length][];

        int changebatch = 0;
        for (int i = 0; i < instances; i++)
        {
            var xi = 0;
            var yi = 0;
            var zi = 0;
            currBatchIndex = 0;
            currBatch = new instancedata[width * height * depth];
            someInstances[i] = new instancedata[width * height * depth];


            //for (float zz = 0; zz < depth * 10 * planeSize; zz += depth * planeSize)
            for (int xx = 0; xx < width; xx++)
            {
                for (int yy = 0; yy < height; yy++)
                {
                    for (int zz = 0; zz < depth; zz++)
                    {
                        changebatch = 0;
                        _chunkPos = new Vector3(xx + instanceposition.x, (yy + (i * height * 10 * planeSize)) + instanceposition.y, zz + instanceposition.z);

                        int indexOfinstancedObject = xx + width * (yy + depth * zz);

                        if (batchIndexNum < width * height * depth)
                        {
                            someInstances[i][indexOfinstancedObject] = new instancedata();
                            AddObj(currBatch, i, indexOfinstancedObject, _chunkPos);

                            batchIndexNum++;
                            if (batchIndexNum >= currBatch.Length)
                            {
                                batches[i] = currBatch;
                                currBatch = BuildNewBatch(currBatch.Length);

                                /*if (changebatch == 1)
                                {
                                    //AddObj(currBatch, i, indexOfinstancedObject, _chunkPos);
                                }*/

                                currBatchIndex = 0;
                                batchIndexNum = 0;
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                            changebatch = 1;
                        }

                        /*batchIndexNum++;
                        if (batchIndexNum >= currBatch.Length)
                        {
                            batches[i] = currBatch;
                            currBatch = BuildNewBatch(currBatch.Length);

                            /*if (changebatch == 1)
                            {
                                //AddObj(currBatch, i, indexOfinstancedObject, _chunkPos);
                            }

                            currBatchIndex = 0;
                            batchIndexNum = 0;
                        }
                        else
                        {

                        }*/

                        //zi++;
                    }
                    //yi++;
                }
                //xi++;
            }

        }




    }


    private void Update()
    {
        RenderBatches();
    }

    int currBatchIndex = 0;
    private void AddObj(instancedata[] currBatch, int meshgameobjecttoinstanceindex, int meshgameobjectinstanceindex, Vector3 _chunkPos)
    {
        //Vector3 _chunkPos = new Vector3(Random.Range(-maxPos.x, maxPos.x), Random.Range(-maxPos.y, maxPos.y), Random.Range(-maxPos.z, maxPos.z));

        //var instancedata = new instancedata(_chunkPos, new Vector3(1, 1, 1), Quaternion.identity, meshgameobjecttoinstanceindex, instanceindex)

        var instancedata = new instancedata();
        instancedata.pos = _chunkPos;
        instancedata.rot = Quaternion.identity;
        instancedata.scale = new Vector3(1, 1, 1);
        instancedata.gameobjectoinstanceindex = meshgameobjecttoinstanceindex;
        instancedata.instanceindex = meshgameobjectinstanceindex;
        instancedata.currentInstanceGameObject = null;
        instancedata.enabled = -1;
        instancedata.swap = -1;
        instancedata.initGametime = -1;
        instancedata.InitInstancePositionToLinkTo = _chunkPos;
        instancedata.instanceenabledcounter = 0;
        instancedata.instanceenabledcounterMax = 1;
        instancedata.instanceenabledcounterSwtc = 0;

        currBatch[currBatchIndex] = instancedata;
        currBatchIndex++;







        //batches.Add(currBatch);
        /*for (float xx = 0; xx < width * 10 * planeSize; xx += width * planeSize)
        {
            for (float yy = 0; yy < height * 10 * planeSize; yy += height * planeSize)
            {
                for (float zz = 0; zz < depth * 10 * planeSize; zz += depth * planeSize)
                {
                    _chunkPos = new Vector3(xx, yy, zz);
                    currBatch.Add(new instancedata(_chunkPos, new Vector3(1, 1, 1), Quaternion.identity));               
                }
            }
        }
        batches.Add(currBatch);*/
    }



    private instancedata[] BuildNewBatch(int arrayLength)
    {
        return new instancedata[arrayLength];
    }

    Vector3 positionthisframe = Vector3.zero;
    Vector3 positionlastframe = Vector3.zero;
    Matrix4x4 matrixtemp = Matrix4x4.identity;

    int somecounter = 0;
    private void RenderBatches()
    {
        somecounter = 0;
        positionthisframe = this.transform.position;
        //for (int i = 0; i < batches.Count; i++)
        //{
        //    Graphics.DrawMeshInstanced(objMesh,0,objMat,batches[i].Select((a) => a.matrix).ToList());
        //}

        Matrix4x4 somemat = Matrix4x4.TRS(this.transform.position, this.transform.rotation, Vector3.one);
        //somemat.

        Vector3 diff = positionlastframe - positionthisframe;


        //if ()
        {
            for (int i = 0; i < someInstances.Length; i++)
            {
                if (someInstances[i] != null)
                {
                    for (int j = 0; j < someInstances[i].Length; j++)
                    {

                    }
                }
            }
        }
      


        /*
        foreach (var batch in batches)
        {
            if (somecounter < maininstanceindexarraypointerlocation)
            {
                var listOfInstances = batch.Select((a) => a.matrix).ToList();
                for (int i = 0; i < listOfInstances.Count; i++)
                {


                    matrixtemp = listOfInstances[i];

                    matrixtemp *= somemat;

                    //matrixtemp.m30 = diff.x;
                    //matrixtemp.m31 = diff.y;
                    //matrixtemp.m32 = diff.z;
                    listOfInstances[i] = matrixtemp;
                }


                Graphics.DrawMeshInstanced(objMesh, 0, objMat, listOfInstances); //batch.Select((a) => a.matrix).ToList()
            }
            else
            {

            }
            somecounter++;
        }*/

        positionlastframe = this.transform.position;
        //Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
    }



    public void addObjectInstanceToDrawingList(instancedata instancedata_)
    {
        //Debug.Log("added instance data to draw");
        if (batchobjectindexdatain < someInstances.Length)
        {
            if (someInstances[batchobjectindexdatain] != null)
            {
                if (instancedata_.enabled == 0)
                {
                    //Debug.Log("0length:" + someInstances.Length + "/batchobjectindex:" + batchobjectindexdatain);
                    instancedata instancedata0 = new sccsInstancesunitypool0.instancedata();
                    instancedata0.initGametime = UnityEngine.Time.time;
                    instancedata0.currentInstanceGameObject = null;
                    instancedata0.instanceindex = batchinstanceindexdatain;
                    instancedata0.enabled = 1;
                    instancedata0.swap = -1;
                    instancedata0.instanceenabledcounter = 0;
                    instancedata0.instanceenabledcounterSwtc = 0;
                    instancedata0.instanceenabledcounterMax = 1;
                    instancedata0.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;
                    //current.addObjectInstanceToDrawingList(instancedata0);

                    someInstances[batchobjectindexdatain][batchinstanceindexdatain].enabled = 1;
                }
            }
        }
        else
        {

        }
        batchobjectindexdatain++;
    }


    /*
    public void addObjectInstanceToDrawingList(instancedata instancedata_)
    {
        /*sccsInstancesunitypool0.instancedata instancedata0 = new sccsInstancesunitypool0.instancedata();
        instancedata0.initGametime = UnityEngine.Time.time;
        instancedata0.currentInstanceGameObject = null;
        instancedata0.instanceindex = sccsInstancesunitypool0.maininstanceindexarraypointerlocation;
        instancedata0.enabled = -1;
        instancedata0.swap = -1;
        instancedata0.instanceenabledcounter = -1;
        instancedata0.instanceenabledcounterSwtc = -1;
        instancedata0.instanceenabledcounterMax = -1;
        instancedata0.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;
        sccsInstancesunitypool0.current.addObjectInstanceToDrawingList(instancedata0);

        someInstances[maininstanceindexarraypointerlocation][sccsInstancesunitypool0.maininstanceindexarraypointerlocation].enabled = 0;*/





        /*Debug.Log("length:" + someInstances.Length + "/maininstanceindexarraypointerlocation:" + maininstanceindexarraypointerlocation);
        if (maininstanceindexarraypointerlocation < someInstances.Length)
        {

            if (someInstances[maininstanceindexarraypointerlocation] != null)
            {
                if (someInstances[maininstanceindexarraypointerlocation][sccsInstancesunitypool0.maininstanceindexarraypointerlocation].enabled == -1)
                {
                    sccsInstancesunitypool0.instancedata instancedata0 = new sccsInstancesunitypool0.instancedata();
                    instancedata0.initGametime = UnityEngine.Time.time;
                    instancedata0.currentInstanceGameObject = null;
                    instancedata0.instanceindex = sccsInstancesunitypool0.maininstanceindexarraypointerlocation;
                    instancedata0.enabled = 0;
                    instancedata0.swap = -1;
                    instancedata0.instanceenabledcounter = 0;
                    instancedata0.instanceenabledcounterSwtc = -1;
                    instancedata0.instanceenabledcounterMax = 1;
                    instancedata0.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;
                    sccsInstancesunitypool0.current.addObjectInstanceToDrawingList(instancedata0);

                    someInstances[maininstanceindexarraypointerlocation][sccsInstancesunitypool0.maininstanceindexarraypointerlocation].enabled = 0;
                }
            }
        }
        else
        {

        }
        maininstanceindexarraypointerlocation++;
    }*/



    instancedata[][] someInstances;
    /*if (someInstances[maininstanceindexarraypointerlocation][sccsInstancesunitypool0.maininstanceindexarraypointerlocation].enabled == -1)
    {
        instancedata_.initGametime = UnityEngine.Time.time;
        //instancedata_.currentInstanceGameObject = GetPooledObject();
        //instancedata_.currentInstanceGameObject.SetActive(true);
        instancedata_.instanceindex = maininstanceindexarraypointerlocation;
        instancedata_.enabled = 1;
        //calculate if we need to swap with another instance lower in hierarchy.
        instancedata_.swap = 0;
        instancedata_.instanceenabledcounter = -1;
        instancedata_.instanceenabledcounterSwtc = -1;
        instancedata_.instanceenabledcounterMax = -1;

        sccsInstancesunitypool0.instancedata instancedata0 = new sccsInstancesunitypool0.instancedata();
        instancedata0.initGametime = UnityEngine.Time.time;
        instancedata0.currentInstanceGameObject = null;
        instancedata0.instanceindex = sccsInstancesunitypool0.maininstanceindexarraypointerlocation;
        instancedata0.enabled = -1;
        instancedata0.swap = -1;
        instancedata0.instanceenabledcounter = -1;
        instancedata0.instanceenabledcounterSwtc = -1;
        instancedata0.instanceenabledcounterMax = -1;
        instancedata0.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;
        sccsInstancesunitypool0.current.addObjectInstanceToDrawingList(instancedata0);

        someInstances[maininstanceindexarraypointerlocation][sccsInstancesunitypool0.maininstanceindexarraypointerlocation].enabled = 0;
    }*/

    /*
    sccsInstancesunitypool1.instancedata instancedata1 = new sccsInstancesunitypool1.instancedata();
    instancedata1.initGametime = UnityEngine.Time.time;
    instancedata1.currentInstanceGameObject = sccsInstancesunitypool1.GetPooledObject();
    instancedata1.currentInstanceGameObject.SetActive(true);
    instancedata1.instanceindex = sccsInstancesunitypool1.maininstanceindexarraypointerlocation;
    instancedata1.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata1.swap = 0;
    instancedata1.instanceenabledcounter = -1;
    instancedata1.instanceenabledcounterSwtc = -1;
    instancedata1.instanceenabledcounterMax = -1;
    instancedata1.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool1.current.addObjectInstanceToDrawingList(instancedata1);




    sccsInstancesunitypool2.instancedata instancedata2 = new sccsInstancesunitypool2.instancedata();
    instancedata2.initGametime = UnityEngine.Time.time;
    instancedata2.currentInstanceGameObject = sccsInstancesunitypool2.GetPooledObject();
    instancedata2.currentInstanceGameObject.SetActive(true);
    instancedata2.instanceindex = sccsInstancesunitypool2.maininstanceindexarraypointerlocation;
    instancedata2.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata2.swap = 0;
    instancedata2.instanceenabledcounter = -1;
    instancedata2.instanceenabledcounterSwtc = -1;
    instancedata2.instanceenabledcounterMax = -1;
    instancedata2.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool2.current.addObjectInstanceToDrawingList(instancedata2);


    sccsInstancesunitypool3.instancedata instancedata3 = new sccsInstancesunitypool3.instancedata();
    instancedata3.initGametime = UnityEngine.Time.time;
    instancedata3.currentInstanceGameObject = sccsInstancesunitypool3.GetPooledObject();
    instancedata3.currentInstanceGameObject.SetActive(true);
    instancedata3.instanceindex = sccsInstancesunitypool3.maininstanceindexarraypointerlocation;
    instancedata3.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata3.swap = 0;
    instancedata3.instanceenabledcounter = -1;
    instancedata3.instanceenabledcounterSwtc = -1;
    instancedata3.instanceenabledcounterMax = -1;
    instancedata3.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool3.current.addObjectInstanceToDrawingList(instancedata3);


    sccsInstancesunitypool4.instancedata instancedata4 = new sccsInstancesunitypool4.instancedata();
    instancedata4.initGametime = UnityEngine.Time.time;
    instancedata4.currentInstanceGameObject = sccsInstancesunitypool4.GetPooledObject();
    instancedata4.currentInstanceGameObject.SetActive(true);
    instancedata4.instanceindex = sccsInstancesunitypool4.maininstanceindexarraypointerlocation;
    instancedata4.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata4.swap = 0;
    instancedata4.instanceenabledcounter = -1;
    instancedata4.instanceenabledcounterSwtc = -1;
    instancedata4.instanceenabledcounterMax = -1;
    instancedata4.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool4.current.addObjectInstanceToDrawingList(instancedata4);


    sccsInstancesunitypool5.instancedata instancedata5 = new sccsInstancesunitypool5.instancedata();
    instancedata5.initGametime = UnityEngine.Time.time;
    instancedata5.currentInstanceGameObject = sccsInstancesunitypool5.GetPooledObject();
    instancedata5.currentInstanceGameObject.SetActive(true);
    instancedata5.instanceindex = sccsInstancesunitypool5.maininstanceindexarraypointerlocation;
    instancedata5.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata5.swap = 0;
    instancedata5.instanceenabledcounter = -1;
    instancedata5.instanceenabledcounterSwtc = -1;
    instancedata5.instanceenabledcounterMax = -1;
    instancedata5.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool5.current.addObjectInstanceToDrawingList(instancedata5);


    sccsInstancesunitypool6.instancedata instancedata6 = new sccsInstancesunitypool6.instancedata();
    instancedata6.initGametime = UnityEngine.Time.time;
    instancedata6.currentInstanceGameObject = sccsInstancesunitypool6.GetPooledObject();
    instancedata6.currentInstanceGameObject.SetActive(true);
    instancedata6.instanceindex = sccsInstancesunitypool6.maininstanceindexarraypointerlocation;
    instancedata6.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata6.swap = 0;
    instancedata6.instanceenabledcounter = -1;
    instancedata6.instanceenabledcounterSwtc = -1;
    instancedata6.instanceenabledcounterMax = -1;
    instancedata6.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool6.current.addObjectInstanceToDrawingList(instancedata6);


    sccsInstancesunitypool7.instancedata instancedata7 = new sccsInstancesunitypool7.instancedata();
    instancedata7.initGametime = UnityEngine.Time.time;
    instancedata7.currentInstanceGameObject = sccsInstancesunitypool7.GetPooledObject();
    instancedata7.currentInstanceGameObject.SetActive(true);
    instancedata7.instanceindex = sccsInstancesunitypool7.maininstanceindexarraypointerlocation;
    instancedata7.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata7.swap = 0;
    instancedata7.instanceenabledcounter = -1;
    instancedata7.instanceenabledcounterSwtc = -1;
    instancedata7.instanceenabledcounterMax = -1;
    instancedata7.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool7.current.addObjectInstanceToDrawingList(instancedata7);


    sccsInstancesunitypool8.instancedata instancedata8 = new sccsInstancesunitypool8.instancedata();
    instancedata8.initGametime = UnityEngine.Time.time;
    instancedata8.currentInstanceGameObject = sccsInstancesunitypool8.GetPooledObject();
    instancedata8.currentInstanceGameObject.SetActive(true);
    instancedata8.instanceindex = sccsInstancesunitypool8.maininstanceindexarraypointerlocation;
    instancedata8.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata8.swap = 0;
    instancedata8.instanceenabledcounter = -1;
    instancedata8.instanceenabledcounterSwtc = -1;
    instancedata8.instanceenabledcounterMax = -1;
    instancedata8.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool8.current.addObjectInstanceToDrawingList(instancedata8);


    sccsInstancesunitypool9.instancedata instancedata9 = new sccsInstancesunitypool9.instancedata();
    instancedata9.initGametime = UnityEngine.Time.time;
    instancedata9.currentInstanceGameObject = sccsInstancesunitypool9.GetPooledObject();
    instancedata9.currentInstanceGameObject.SetActive(true);
    instancedata9.instanceindex = sccsInstancesunitypool9.maininstanceindexarraypointerlocation;
    instancedata9.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata9.swap = 0;
    instancedata9.instanceenabledcounter = -1;
    instancedata9.instanceenabledcounterSwtc = -1;
    instancedata9.instanceenabledcounterMax = -1;
    instancedata9.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool9.current.addObjectInstanceToDrawingList(instancedata9);


    sccsInstancesunitypool10.instancedata instancedata10 = new sccsInstancesunitypool10.instancedata();
    instancedata10.initGametime = UnityEngine.Time.time;
    instancedata10.currentInstanceGameObject = sccsInstancesunitypool10.GetPooledObject();
    instancedata10.currentInstanceGameObject.SetActive(true);
    instancedata10.instanceindex = sccsInstancesunitypool10.maininstanceindexarraypointerlocation;
    instancedata10.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata10.swap = 0;
    instancedata10.instanceenabledcounter = -1;
    instancedata10.instanceenabledcounterSwtc = -1;
    instancedata10.instanceenabledcounterMax = -1;
    instancedata10.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool10.current.addObjectInstanceToDrawingList(instancedata10);


    sccsInstancesunitypool11.instancedata instancedata11 = new sccsInstancesunitypool11.instancedata();
    instancedata11.initGametime = UnityEngine.Time.time;
    instancedata11.currentInstanceGameObject = sccsInstancesunitypool11.GetPooledObject();
    instancedata11.currentInstanceGameObject.SetActive(true);
    instancedata11.instanceindex = sccsInstancesunitypool11.maininstanceindexarraypointerlocation;
    instancedata11.enabled = 1;
    //calculate if we need to swap with another instance lower in hierarchy.
    instancedata11.swap = 0;
    instancedata11.instanceenabledcounter = -1;
    instancedata11.instanceenabledcounterSwtc = -1;
    instancedata11.instanceenabledcounterMax = -1;
    instancedata11.InitInstancePositionToLinkTo = instancedata_.InitInstancePositionToLinkTo;

    sccsInstancesunitypool11.current.addObjectInstanceToDrawingList(instancedata11);*/






    /*public struct instancedata
    {
        public Vector3 InitInstancePositionToLinkTo;
        public GameObject currentInstanceGameObject;
        public int instanceindex;
        public int enabled;
        public int swap;
        public int instanceenabledcounter;
        public int instanceenabledcounterSwtc;
        public int instanceenabledcounterMax;
        public float initGametime;
    }*/


    public GameObject GetPooledObject()
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
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }



    public struct instancedata
    {
        public Vector3 pos;
        public Vector3 scale;
        public Quaternion rot;
        //public int instanceindex;
        public int gameobjectoinstanceindex;

        /*public Matrix4x4 matrix
        {
            get
            {
                return Matrix4x4.TRS(pos, rot, scale);
            }
        }*/
        public Vector3 InitInstancePositionToLinkTo;
        public GameObject currentInstanceGameObject;
        public int instanceindex;
        public int enabled;
        public int swap;
        public int instanceenabledcounter;
        public int instanceenabledcounterSwtc;
        public int instanceenabledcounterMax;
        public float initGametime;


        /*public instancedata(Vector3 pos, Vector3 scale, Quaternion rot, int gameobjectoinstanceindex_, int instanceindex_)
        {
            this.instanceindex = instanceindex_;
            this.gameobjectoinstanceindex = gameobjectoinstanceindex_;

            this.pos = pos;
            this.scale = scale;
            this.rot = rot;
        }*/
    }
}













//batches.Add(currBatch);
//currBatch = BuildNewBatch();
/*AddObj(currBatch, i);
batchIndexNum++;
if (batchIndexNum >= 1000)
{
    batches.Add(currBatch);
    currBatch = BuildNewBatch();
    batchIndexNum = 0;
}*/

/*public Mesh cubeMesh;
public Material cubeMaterial;

public GameObject chunk;

void Start ()
{

    GameObject _newChunk = Instantiate(chunk, new Vector3(0,0,0), Quaternion.identity);

    cubeMesh = _newChunk.GetComponent<MeshFilter>().mesh;
    cubeMaterial = _newChunk.GetComponent<MeshRenderer>().material;


}

void Update ()
{
    List<Matrix4x4> transformList = new List<Matrix4x4>();

    //These for loops create offsets from the position at which you want to draw your cube built from cubes.
    for (int x = -1; x < 1; x++)
    {
        for (int y = -1; y < 1; y++)
        {
            for (int z = -1; x < 1; z++)
            {
                //We will assume you want to create your cube of cubes at 0,0,0
                Vector3 position = new Vector3(0, 0, 0);

                //Take the origin position, and apply the offsets
                position.x += x;
                position.y += y;
                position.z += z;

                //Create a matrix for the position created from this iteration of the loop
                Matrix4x4 matrix = new Matrix4x4();

                //Set the position/rotation/scale for this matrix
                matrix.SetTRS(position, Quaternion.Euler(Vector3.zero), Vector3.one);

                //Add the matrix to the list, which will be used when we use DrawMeshInstanced.
                transformList.Add(matrix);
            }
        }
    }
    //After the for loops are finished, and transformList has several matrices in it, simply pass DrawMeshInstanced the mesh, a material, and the list of matrices containing all positional info.
    Graphics.DrawMeshInstanced(cubeMesh, 0, cubeMaterial, transformList);
}*/



/*
//someinstancedata[i] = new instancedata[width*height*depth];
for (float xx = 0; xx < width * 10 * planeSize; xx += width * planeSize)
{
    for (float yy = 0; yy < height * 10 * planeSize; yy += height * planeSize)
    {
        for (float zz = 0; zz < depth * 10 * planeSize; zz += depth * planeSize)
        {
            changebatch = 0;
            _chunkPos = new Vector3(xx + instanceposition.x, (yy + (i * height * 10 * planeSize)) + instanceposition.y, zz + instanceposition.z);

            int indexOfinstancedObject = xi + width * (yi + depth * zi);
            //someinstancedata[i][indexOfinstancedObject] =

            if (batchIndexNum < currBatch.Length)
            {
                someInstances[i][indexOfinstancedObject] = new instancedata();
                AddObj(currBatch, i, indexOfinstancedObject, _chunkPos);
            }
            else
            {

                changebatch = 1;
            }

            batchIndexNum++;
            if (batchIndexNum >= currBatch.Length)
            {
                batches[i] = currBatch;
                currBatch = BuildNewBatch(currBatch.Length);

                if (changebatch == 1)
                {
                    AddObj(currBatch, i, indexOfinstancedObject, _chunkPos);
                }

                currBatchIndex = 0;
                batchIndexNum = 0;
            }
            else
            {

            }

            zi++;
        }
        yi++;
    }
    xi++;
}*/

/*
int sometotalcounter = 0;

for (int i = 0; i < instances; i++)
{
    someInstances[i] = new instancedata[width*height*depth];

    var xi = 0;
    var yi = 0;
    var zi = 0;
    sometotalcounter = 0;
    for (float xx = 0; xx < width * 10 * planeSize; xx += width * planeSize)
    {
        for (float yy = 0; yy < height * 10 * planeSize; yy += height * planeSize)
        {
            for (float zz = 0; zz < depth * 10 * planeSize; zz += depth * planeSize)
            {
                _chunkPos = new Vector3(xx + instanceposition.x, (yy + (i * height * 10 * planeSize)) + instanceposition.y, zz + instanceposition.z);
                //int indexOfinstancedObject = xi + width * (yi + depth * zi);

                if (sometotalcounter < currBatch.Length)
                {


                    someInstances[i][sometotalcounter] = new instancedata();
                    sometotalcounter++;
                }
                else
                {
                    sometotalcounter = 0;

                }


            }
        }
    }
}*/
