using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class sccsInstancesunitypool8 : MonoBehaviour
{


    public static new sccsInstancesunitypool8 current;
    public GameObject pooledObject;
    static GameObject pooledobject;
    public int pooledAmount = 20;
    public static bool willGrow = true;
    static List<GameObject> pooledObjects;

    public Transform unitytutorialpoolworldposition;

    public int instances;
    public Vector3 maxPos;
    Mesh objMesh;
    public Material objMat;

    private List<List<ObjData>> batches = new List<List<ObjData>>();

    public int width = 10;
    public int height = 10;
    public int depth = 10;

    Vector3 _chunkPos;

    float planeSize = 0.1f;

    public static int maininstanceindexarraypointerlocation = -1;

    public void Awake()
    {
        current = this;
        unitytutorialpoolworldposition = this.transform;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }



        objMesh = pooledObject.transform.gameObject.GetComponent<MeshFilter>().mesh;

        pooledobject = pooledObject;

        int batchIndexNum = 0;
        List<ObjData> currBatch = new List<ObjData>();

        //AddObj(currBatch, 1);
        //batches.Add(currBatch);
        //currBatch = BuildNewBatch();

        Vector3 instanceposition = this.transform.position;

        for (int i = 0; i < instances; i++)
        {

            var xi = 0;
            var yi = 0;
            var zi = 0;


            for (float xx = 0; xx < width * 10 * planeSize; xx += width * planeSize)
            {
                for (float yy = 0; yy < height * 10 * planeSize; yy += height * planeSize)
                {
                    for (float zz = 0; zz < depth * 10 * planeSize; zz += depth * planeSize)
                    {
                        _chunkPos = new Vector3(xx + instanceposition.x, (yy + (i * height * 10 * planeSize)) + instanceposition.y, zz + instanceposition.z);

                        int indexOfinstancedObject = xi + width * (yi + depth * zi);


                        AddObj(currBatch, i, indexOfinstancedObject, _chunkPos);
                        batchIndexNum++;

                        if (batchIndexNum >= 100)
                        {
                            batches.Add(currBatch);
                            currBatch = BuildNewBatch();
                            batchIndexNum = 0;
                        }
                        zi++;
                    }
                    yi++;
                }
                xi++;
            }
        }


        someInstances = new instancedata[batches.Count][];

        for (int i = 0; i < instances; i++)
        {
            someInstances[i] = new instancedata[width * height * depth];

            var xi = 0;
            var yi = 0;
            var zi = 0;

            for (float xx = 0; xx < width * 10 * planeSize; xx += width * planeSize)
            {
                for (float yy = 0; yy < height * 10 * planeSize; yy += height * planeSize)
                {
                    for (float zz = 0; zz < depth * 10 * planeSize; zz += depth * planeSize)
                    {
                        _chunkPos = new Vector3(xx + instanceposition.x, (yy + (i * height * 10 * planeSize)) + instanceposition.y, zz + instanceposition.z);

                        int indexOfinstancedObject = xi + width * (yi + depth * zi);

                        someInstances[i][indexOfinstancedObject] = new instancedata();

                    }
                }
            }
        }
    }

    private void Update()
    {
        RenderBatches();
    }

    private void AddObj(List<ObjData> currBatch, int meshgameobjecttoinstanceindex, int meshgameobjectinstanceindex, Vector3 _chunkPos)
    {
        //Vector3 _chunkPos = new Vector3(Random.Range(-maxPos.x, maxPos.x), Random.Range(-maxPos.y, maxPos.y), Random.Range(-maxPos.z, maxPos.z));

        currBatch.Add(new ObjData(_chunkPos, new Vector3(1, 1, 1), Quaternion.identity, meshgameobjecttoinstanceindex, meshgameobjectinstanceindex));
        //batches.Add(currBatch);
        /*for (float xx = 0; xx < width * 10 * planeSize; xx += width * planeSize)
        {
            for (float yy = 0; yy < height * 10 * planeSize; yy += height * planeSize)
            {
                for (float zz = 0; zz < depth * 10 * planeSize; zz += depth * planeSize)
                {
                    _chunkPos = new Vector3(xx, yy, zz);
                    currBatch.Add(new ObjData(_chunkPos, new Vector3(1, 1, 1), Quaternion.identity));               
                }
            }
        }
        batches.Add(currBatch);*/
    }



    private List<ObjData> BuildNewBatch()
    {
        return new List<ObjData>();
    }

    Vector3 positionthisframe = Vector3.zero;
    Vector3 positionlastframe = Vector3.zero;
    Matrix4x4 matrixtemp = Matrix4x4.identity;

    int somecounter = 0;
    private void RenderBatches()
    {
        somecounter = 0;
        positionthisframe = this.transform.position;
        /*for (int i = 0; i < batches.Count; i++)
        {
            Graphics.DrawMeshInstanced(objMesh,0,objMat,batches[i].Select((a) => a.matrix).ToList());
        }*/

        Matrix4x4 somemat = Matrix4x4.TRS(this.transform.position, this.transform.rotation, Vector3.one);
        //somemat.

        Vector3 diff = positionlastframe - positionthisframe;
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
        }

        positionlastframe = this.transform.position;
        //Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
    }

    public void addObjectInstanceToDrawingList(instancedata instancedata_)
    {
        instancedata_.initGametime = UnityEngine.Time.time;
        instancedata_.currentInstanceGameObject = GetPooledObject();
        instancedata_.currentInstanceGameObject.SetActive(true);

        instancedata_.instanceindex = maininstanceindexarraypointerlocation;
        instancedata_.enabled = 1;

        //calculate if we need to swap with another instance lower in hierarchy.
        instancedata_.swap = 0;

        instancedata_.instanceenabledcounter = -1;
        instancedata_.instanceenabledcounterSwtc = -1;
        instancedata_.instanceenabledcounterMax = -1;



        maininstanceindexarraypointerlocation++;
    }





    instancedata[][] someInstances;

    public struct instancedata
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
    }


    public static GameObject GetPooledObject()
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
            GameObject obj = (GameObject)Instantiate(pooledobject);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }



    public class ObjData
    {
        public Vector3 pos;
        public Vector3 scale;
        public Quaternion rot;
        public int instanceindex;
        public int gameobjectoinstanceindex;

        public Matrix4x4 matrix
        {
            get
            {
                return Matrix4x4.TRS(pos, rot, scale);
            }
        }

        public ObjData(Vector3 pos, Vector3 scale, Quaternion rot, int gameobjectoinstanceindex_, int instanceindex_)
        {
            this.instanceindex = instanceindex_;
            this.gameobjectoinstanceindex = gameobjectoinstanceindex_;

            this.pos = pos;
            this.scale = scale;
            this.rot = rot;
        }
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
