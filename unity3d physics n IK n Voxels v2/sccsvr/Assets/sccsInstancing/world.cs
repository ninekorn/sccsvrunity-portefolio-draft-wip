using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;




public class world : MonoBehaviour {

    public class ObjData
    {
        public Vector3 pos;
        public Vector3 scale;
        public Quaternion rot;

        public Matrix4x4 matrix
        {
            get
            {
                return Matrix4x4.TRS(pos, rot, scale);
            }
        }

        public ObjData(Vector3 pos, Vector3 scale, Quaternion rot)
        {
            this.pos = pos;
            this.scale = scale;
            this.rot = rot;
        }
    }

    public int instances;
    public Vector3 maxPos;
    public Mesh objMesh;
    public Material objMat;

    private List<List<ObjData>> batches = new List<List<ObjData>>();

    public int _width = 10;
    public int _height = 10;
    public int _depth = 10;

    Vector3 _chunkPos;

    float _planeSize = 0.1f;

    public void Start()
    {
        int batchIndexNum = 0;
        List<ObjData> currBatch = new List<ObjData>();

        //AddObj(currBatch, 1);
        //batches.Add(currBatch);
        //currBatch = BuildNewBatch();

        Vector3 instanceposition = this.transform.position;

        for (int i = 0;i < instances;i++)
        {
            for (float xx = 0; xx < _width * 10 * _planeSize; xx += _width * _planeSize)
            {
                for (float yy = 0; yy < _height * 10 * _planeSize; yy += _height * _planeSize)
                {
                    for (float zz = 0; zz < _depth * 10 * _planeSize; zz += _depth * _planeSize)
                    {
                        _chunkPos = new Vector3(xx+ instanceposition.x, (yy + (i* _height * 10 * _planeSize)) + instanceposition.y, zz + instanceposition.z);

                        AddObj(currBatch, i, _chunkPos);
                        batchIndexNum++;

                        if (batchIndexNum >= 100)
                        {
                            batches.Add(currBatch);
                            currBatch = BuildNewBatch();
                            batchIndexNum = 0;
                        }
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
        }
    }

    private void Update()
    {
        RenderBatches();
    }

    private void AddObj(List<ObjData> currBatch, int i, Vector3 _chunkPos)
    {
        //Vector3 _chunkPos = new Vector3(Random.Range(-maxPos.x, maxPos.x), Random.Range(-maxPos.y, maxPos.y), Random.Range(-maxPos.z, maxPos.z));

        currBatch.Add(new ObjData(_chunkPos, new Vector3(1, 1, 1), Quaternion.identity));
        //batches.Add(currBatch);
        /*for (float xx = 0; xx < _width * 10 * _planeSize; xx += _width * _planeSize)
        {
            for (float yy = 0; yy < _height * 10 * _planeSize; yy += _height * _planeSize)
            {
                for (float zz = 0; zz < _depth * 10 * _planeSize; zz += _depth * _planeSize)
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

    private void RenderBatches ()
    {
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

        positionlastframe = this.transform.position;
        //Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
    }







































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
}

