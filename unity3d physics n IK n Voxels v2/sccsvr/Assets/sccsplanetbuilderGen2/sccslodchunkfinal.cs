using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class sccslodchunkfinal : MonoBehaviour
{
    int counterCreateChunkObjectFaces = 0;

    int t = 0;
    int total = 0;
    int posx = 0;
    int posy = 0;
    int posz = 0;
    int xx = 0;
    int yy = 0;
    int zz = 0;
    int xi = 0;
    int yi = 0;
    int zi = 0;

    int swtchx = 0;
    int swtchy = 0;
    public int swtchz = 0;

    public int realplanetwidth = 1;


    int counterCreateChunkObjectFacesBytes = 0;
    int tBytes = 0;
    int totalBytes = 0;
    int posxBytes = 0;
    int posyBytes = 0;
    int poszBytes = 0;
    int xxBytes = 0;
    int yyBytes = 0;
    int zzBytes = 0;
    int xiBytes = 0;
    int yiBytes = 0;
    int ziBytes = 0;

    int swtchxBytes = 0;
    int swtchyBytes = 0;
    public int swtchzBytes = 0;

    int rowIterateXBytes = 0;
    int rowIterateZBytes = 0;
    int rowIterateYBytes = 0;


    int[] _chunkArray;
    int[] _tempChunkArray;
    int[] _perlinChunkArray;

    int[] _tempChunkArrayRightFace;
    int[] _tempChunkArrayLeftFace;

    int[] _tempChunkArrayFrontFace;
    int[] _tempChunkArrayBackFace;
    int[] _tempChunkArrayBottomFace;


    int[] _chunkVertexArray;
    int[] _testVertexArray;

    int _seed = 3425;//3425 //3420 //3441
    int _block;

    int vertexlistWidth = 0;
    int vertexlistHeight = 0;
    int vertexlistDepth = 0;

    Vector3[] _vertices;
    List<int> triangles;

    //int _totalVertex = 0;


    int _detailScale = 10;


    public float __planeSize = 0.1f;
    public Vector3 chunkPos;

    public List<Vector3> vertexlist = new List<Vector3>();
    Mesh mesh;

    int _index0 = 0;
    int _index1 = 0;
    int _index2 = 0;
    int _index3 = 0;

    int _newVertzCounter = 0;

    float size0 = 0;
    float temporaryY = 0;

    public int[] map;
    public int width = 10;
    public int height = 10;
    public int depth = 10;

    public Vector3 realIndexedPosition = Vector3.zero;

    public static float _planeSize = 0.1f;

    float seed;
    int block;

    float nodeDiameter;
    float chunkRadius;
    float fraction;
    float chunkSize;

    public float detailScale = 5;
    public float heightScale = 5;
    public int heightScale1 = 5;
    public int detailScale1 = 5;

    sccsproceduralplanetbuilderGen2 componentParent;
    Transform parentObject;

    NewObjectPoolerScript UnityTutorialGameObjectPool; //this.transform.GetComponent<NewObjectPoolerScript>();

    public int chunkbuildingswtc = 0;


    private void Start()
    {
        chunkPos = this.transform.position;

        this.gameObject.tag = "collisionObject";
        this.gameObject.layer = 8; //"collisionObject"
        UnityTutorialGameObjectPool = this.transform.GetComponent<NewObjectPoolerScript>();

        parentObject = this.transform.parent;
        //componentParent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilderGen2>().currentplanetbuilder;


        mesh = new Mesh();
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
        this.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;

    }

    public void sccsCustomStart(sccsproceduralplanetbuilderGen2 sccsproceduralplanetbuilderGen2_)
    {
        total = width * height * depth;
        totalBytes = width * height * depth;


        vertexlistWidth = width + 1;
        vertexlistHeight = height + 1;
        vertexlistDepth = depth + 1;

        _index0 = 0;
        _index1 = 0;
        _index2 = 0;
        _index3 = 0;

        _newVertzCounter = 0;


        vertexlist = new List<Vector3>();
        triangles = new List<int>();
        //_normals = new List<Vector3>();
        //_uvs = new List<Vector2>();


        //vertexlistCounter = 0;
        map = new int[width * height * depth];
        _tempChunkArrayBottomFace = new int[width * height * depth];
        _tempChunkArrayBackFace = new int[width * height * depth];
        _tempChunkArrayFrontFace = new int[width * height * depth];
        _tempChunkArrayLeftFace = new int[width * height * depth];
        _tempChunkArrayRightFace = new int[width * height * depth];
        _tempChunkArray = new int[width * height * depth];
        _chunkArray = new int[width * height * depth];
        _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
        _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
        _perlinChunkArray = new int[width * height * depth];

        componentParent = sccsproceduralplanetbuilderGen2_;// sccsproceduralplanetbuilderGen2.sccsproceduralplanetbuilderGen2staticscriptlock;



        nodeDiameter = _planeSize;
        chunkRadius = _planeSize / realplanetwidth;
        fraction = (int)(1 / (_planeSize));
        chunkSize = 1f;
        seed = 3420;
        radius = 5;
        center = Vector3.zero;

    }
    int radius = 5;

    Vector3 center;

    public void sccsSetMap()
    {
        _index0 = 0;
        _index1 = 0;
        _index2 = 0;
        _index3 = 0;

        _newVertzCounter = 0;

        oneVertIndexX = 0;
        oneVertIndexY = 0;
        oneVertIndexZ = 0;

        twoVertIndexX = 0;
        twoVertIndexY = 0;
        twoVertIndexZ = 0;

        threeVertIndexX = 0;
        threeVertIndexY = 0;
        threeVertIndexZ = 0;

        fourVertIndexX = 0;
        fourVertIndexY = 0;
        fourVertIndexZ = 0;

        _maxWidth = 0;
        _maxDepth = 0;
        rowIterateX = 0;
        rowIterateZ = 0;

        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;



        _tempChunkArrayBottomFace = new int[width * height * depth];
        _tempChunkArrayBackFace = new int[width * height * depth];
        _tempChunkArrayFrontFace = new int[width * height * depth];
        _tempChunkArrayLeftFace = new int[width * height * depth];
        _tempChunkArrayRightFace = new int[width * height * depth];
        _tempChunkArray = new int[width * height * depth];
        _chunkArray = new int[width * height * depth];


        _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
        _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
        //_perlinChunkArray = new int[width * height * depth];

        for (int t = 0; t < total; t++) //total
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
                xi = (width) + xi;
            }
            if (yi < 0)
            {
                yi *= -1;
                yi = (height) + yi;
            }
            if (zi < 0)
            {
                zi *= -1;
                zi = (depth) + zi;
            }

            //zi = (depth - 1) - zi;

            index = xi + (width) * (yi + (height) * zi);

            if (index < total)
            {
                if (map[index] == 1)
                {
                    _chunkArray[index] = 1;
                    _tempChunkArray[index] = 1;
                    _tempChunkArrayRightFace[index] = 1;
                    _tempChunkArrayLeftFace[index] = 1;

                    _tempChunkArrayBottomFace[index] = 1;
                    _tempChunkArrayBackFace[index] = 1;
                    _tempChunkArrayFrontFace[index] = 1;
                }
                else
                {
                    _chunkArray[index] = 0;
                    _tempChunkArray[index] = 0;
                    _tempChunkArrayRightFace[index] = 0;
                    _tempChunkArrayLeftFace[index] = 0;


                    _tempChunkArrayBottomFace[index] = 0;
                    _tempChunkArrayBackFace[index] = 0;
                    _tempChunkArrayFrontFace[index] = 0;

                }
            }
            else
            {
                //t = total;
            }

            zz++;
            if (zz >= (depth))
            {
                xx++;
                zz = 0;
                swtchx = 1;
            }
            if (xx >= (width) && swtchx == 1)
            {
                //faceStart = 0;
                yy++;
                xx = 0;
                swtchx = 0;
                swtchy = 1;
            }
            if (yy >= (height) && swtchy == 1)
            {
                //yy = -ChunkHeight_L;
                swtchy = 0;
                swtchx = 0;
                swtchz = 1;
            }
            counterCreateChunkObjectFaces++;

        }
















        /*
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    var index = x + width * (y + height * z);

                    if (map[index] == 1)
                    {
                        _chunkArray[index] = 1;
                        _tempChunkArray[index] = 1;
                        _tempChunkArrayRightFace[index] = 1;
                        _tempChunkArrayLeftFace[index] = 1;

                        _tempChunkArrayBottomFace[index] = 1;
                        _tempChunkArrayBackFace[index] = 1;
                        _tempChunkArrayFrontFace[index] = 1;
                    }
                    else
                    {
                        _chunkArray[index] = 0;
                        _tempChunkArray[index] = 0;
                        _tempChunkArrayRightFace[index] = 0;
                        _tempChunkArrayLeftFace[index] = 0;


                        _tempChunkArrayBottomFace[index] = 0;
                        _tempChunkArrayBackFace[index] = 0;
                        _tempChunkArrayFrontFace[index] = 0;

                    }
                }
            }
        }*/
    }

    public void Regenerate()
    {
        vertexlist.Clear();
        triangles.Clear();

        t = 0;

        xx = 0;
        yy = 0;
        zz = 0;

        swtchx = 0;
        swtchy = 0;
        swtchz = 0;

        counterCreateChunkObjectFaces = 0;

        tBytes = 0;

        posxBytes = 0;
        posyBytes = 0;
        poszBytes = 0;

        xxBytes = 0;
        yyBytes = 0;
        zzBytes = 0;

        xiBytes = 0;
        yiBytes = 0;
        ziBytes = 0;

        swtchxBytes = 0;
        swtchyBytes = 0;
        swtchzBytes = 0;
        counterCreateChunkObjectFacesBytes = 0;


    }


    float speeding = 0.05f;
    private void Update()
    {

        if (chunkbuildingswtc == 1)
        {
            //Debug.Log("faces generation");
            if (swtchz == 0)
            {

                /*for (int i = 0; i < total; i++)
                {
                    CreateChunkFaces();
                }*/


                if (swtchzBytes == 0)
                {
                    InvokeRepeating("CreateChunkFaces", 0, speeding);
                }
                else
                {
                    //CancelInvoke();
                }

               

                //StartCoroutine("CreateChunkFacesCoroutine");
                //StartCoroutine("CreateChunkFaces");
                //buildTopRight(xi, yi, zi, planetchunkpos);
            }
            else
            {
                CancelInvoke();
                buildMesh();
                //Debug.Log("ended faces generation");
                swtchz = 2;
                chunkbuildingswtc = -1;
            }
        }
    }

    public void buildMesh()
    {
        this.gameObject.GetComponent<MeshFilter>().mesh.Clear();
        this.gameObject.GetComponent<MeshFilter>().mesh.vertices = vertexlist.ToArray();
        this.gameObject.GetComponent<MeshFilter>().mesh.triangles = triangles.ToArray();
        this.gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        this.gameObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();
    }

    public float _iterateSpeed = 0.5f;

    int oneVertIndexX = 0;
    int oneVertIndexY = 0;
    int oneVertIndexZ = 0;

    int twoVertIndexX = 0;
    int twoVertIndexY = 0;
    int twoVertIndexZ = 0;

    int threeVertIndexX = 0;
    int threeVertIndexY = 0;
    int threeVertIndexZ = 0;

    int fourVertIndexX = 0;
    int fourVertIndexY = 0;
    int fourVertIndexZ = 0;

    int _maxWidth = 0;
    int _maxDepth = 0;
    int rowIterateX = 0;
    int rowIterateZ = 0;

    bool foundVertOne = false;
    bool foundVertTwo = false;
    bool foundVertThree = false;
    bool foundVertFour = false;



    int index = 0;


    public void CreateChunkFaces()
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
                    xi = (width) + xi;
                }
                if (yi < 0)
                {
                    yi *= -1;
                    yi = (height) + yi;
                }
                if (zi < 0)
                {
                    zi *= -1;
                    zi = (depth) + zi;
                }

                //zi = (depth - 1) - zi;

                index = xi + (width) * (yi + (height) * zi);

                if (index < total)
                {
                    _block = _chunkArray[index]; //map[x, y, z];_tempChunkArrayRightFace[index];

                    if (_block == 1)
                    {
                        counterCreateChunkObjectFacesBytes = 0;

                        tBytes = 0;

                        posxBytes = 0;
                        posyBytes = 0;
                        poszBytes = 0;

                        xxBytes = 0;
                        yyBytes = 0;
                        zzBytes = 0;

                        xiBytes = 0;
                        yiBytes = 0;
                        ziBytes = 0;

                        swtchxBytes = 0;
                        swtchyBytes = 0;
                        swtchzBytes = 0;

                        rowIterateXBytes = 0;
                        rowIterateYBytes = 0;
                        rowIterateZBytes = 0;

                        _maxWidth = width;
                        _maxHeight = height;
                        _maxDepth = depth;

                        foundVertOne = false;
                        foundVertTwo = false;
                        foundVertThree = false;
                        foundVertFour = false;


                        temptopfacexi = xi;
                        temptopfaceyi = yi;// (height - 1) - yi;
                        temptopfacezi = zi;

                        if (swtchzBytes == 0)
                        {
                            CalculateBytesForFaces();
                        }

                        /*for (int i = 0; i < totalBytes; i++)
                        {
                            if (swtchzBytes == 0)
                            {
                                CalculateBytesForFaces();
                            }
                            else
                            {
                                i = totalBytes;
                                //break;
                            }
                        }*/
                    }
                }
                else
                {
                    //t = total;
                }

                zz++;
                if (zz >= (depth))
                {
                    xx++;
                    zz = 0;
                    swtchx = 1;
                }
                if (xx >= (width) && swtchx == 1)
                {
                    //faceStart = 0;
                    yy++;
                    xx = 0;
                    swtchx = 0;
                    swtchy = 1;
                }
                if (yy >= (height) && swtchy == 1)
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

        /*mesh.vertices = vertexlist.ToArray();
        mesh.triangles = triangles.ToArray();

        //mesh.uv = uv.ToArray();
        //mesh.uv = uv.ToArray();

        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = mesh;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();*/

    }




    IEnumerator CreateChunkFacesCoroutine()
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
                    xi = (width) + xi;
                }
                if (yi < 0)
                {
                    yi *= -1;
                    yi = (height) + yi;
                }
                if (zi < 0)
                {
                    zi *= -1;
                    zi = (depth) + zi;
                }

                //zi = (depth - 1) - zi;

                index = xi + (width) * (yi + (height) * zi);

                if (index < total)
                {
                    _block = _chunkArray[index]; //map[x, y, z];_tempChunkArrayRightFace[index];

                    if (_block == 1)
                    {
                        counterCreateChunkObjectFacesBytes = 0;

                        tBytes = 0;

                        posxBytes = 0;
                        posyBytes = 0;
                        poszBytes = 0;

                        xxBytes = 0;
                        yyBytes = 0;
                        zzBytes = 0;

                        xiBytes = 0;
                        yiBytes = 0;
                        ziBytes = 0;

                        swtchxBytes = 0;
                        swtchyBytes = 0;
                        swtchzBytes = 0;

                        //rowIterateXBytes = 0;
                        //rowIterateYBytes = 0;
                        //rowIterateZBytes = 0;

                        _maxWidth = width;
                        _maxHeight = height;
                        _maxDepth = depth;

                        foundVertOne = false;
                        foundVertTwo = false;
                        foundVertThree = false;
                        foundVertFour = false;


                        if (swtchzBytes == 0)
                        {
                            CalculateBytesForFaces();
                        }


                        /*for (int i = 0; i < totalBytes; i++)
                        {
                            if (swtchzBytes == 0)
                            {
                                CalculateBytesForFaces();
                            }
                            else
                            {
                                i = totalBytes;
                                //break;
                            }
                        }*/
                    }
                }
                else
                {
                    //t = total;
                }


                zz++;
                if (zz >= (depth))
                {
                    xx++;
                    zz = 0;
                    swtchx = 1;
                }
                if (xx >= (width) && swtchx == 1)
                {
                    //faceStart = 0;
                    yy++;
                    xx = 0;
                    swtchx = 0;
                    swtchy = 1;
                }
                if (yy >= (height) && swtchy == 1)
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

        /*mesh.vertices = vertexlist.ToArray();
        mesh.triangles = triangles.ToArray();

        //mesh.uv = uv.ToArray();
        //mesh.uv = uv.ToArray();

        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = mesh;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();*/

        yield return waitforsec;
    }

    WaitForSeconds waitforsec = new WaitForSeconds(0);


    int temptopfacexi = 0;
    int temptopfaceyi = 0;
    int temptopfacezi = 0;

    /*
    int temptopfacexi = 0;
    int temptopfaceyi = 0;
    int temptopfacezi = 0;

    int tempbottomfacexi = 0;
    int tempbottomfaceyi = 0;
    int tempbottomfacezi = 0;

    int templeftfacexi = 0;
    int templeftfaceyi = 0;
    int templeftfacezi = 0;

    int temprightfacexi = 0;
    int temprightfaceyi = 0;
    int temprightfacezi = 0;

    int tempbackfacexi = 0;
    int tempbackfaceyi = 0;
    int tempbackfacezi = 0;

    int tempfrontfacexi = 0;
    int tempfrontfaceyi = 0;
    int tempfrontfacezi = 0;*/




    void CalculateBytesForFaces()
    {
        if (tBytes < totalBytes)
        {
            posxBytes = (xxBytes);
            posyBytes = (yyBytes);
            poszBytes = (zzBytes);

            Vector3 somepos = new Vector3(posxBytes, posyBytes, poszBytes);

            xiBytes = xxBytes;
            yiBytes = yyBytes;
            ziBytes = zzBytes;

            rowIterateXBytes = xiBytes + xi;
            rowIterateYBytes = yiBytes + yi;
            rowIterateZBytes = ziBytes + zi;

            var indexBytes = rowIterateXBytes + (width) * (rowIterateYBytes + (height) * rowIterateZBytes);

            //Debug.Log("xiBytes:" + xiBytes + "/yiBytes:" + yiBytes + "/ziBytes:" + ziBytes);
            if (indexBytes < totalBytes)
            {
                /*
                var somevalueforTopx = 0;
                var somevalueforTopy = 0;
                var somevalueforTopz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforTopx = Mathf.CeilToInt(chunkPos.x ) / realplanetwidth;
                }
                else
                {
                    somevalueforTopx = Mathf.FloorToInt(chunkPos.x ) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforTopy = Mathf.CeilToInt((chunkPos.y + realplanetwidth) ) / realplanetwidth;
                }
                else
                {
                    somevalueforTopy = Mathf.FloorToInt((chunkPos.y + realplanetwidth) ) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforTopz = Mathf.CeilToInt(chunkPos.z ) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforTopz = Mathf.FloorToInt(chunkPos.z ) / realplanetwidth;
                }







                var somevalueforBottomx = 0;
                var somevalueforBottomy = 0;
                var somevalueforBottomz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforBottomx = Mathf.CeilToInt(chunkPos.x ) / realplanetwidth;
                }
                else
                {
                    somevalueforBottomx = Mathf.FloorToInt(chunkPos.x ) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforBottomy = Mathf.CeilToInt((chunkPos.y - realplanetwidth) ) / realplanetwidth;
                }
                else
                {
                    somevalueforBottomy = Mathf.FloorToInt((chunkPos.y - realplanetwidth) ) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforBottomz = Mathf.CeilToInt(chunkPos.z ) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforBottomz = Mathf.FloorToInt(chunkPos.z ) / realplanetwidth;
                }








                var somevalueforRightx = 0;
                var somevalueforRighty = 0;
                var somevalueforRightz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforRightx = Mathf.CeilToInt((chunkPos.x + realplanetwidth) ) / realplanetwidth;
                }
                else
                {
                    somevalueforRightx = Mathf.FloorToInt((chunkPos.x+realplanetwidth) ) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforRighty = Mathf.CeilToInt((chunkPos.y) ) / realplanetwidth;
                }
                else
                {
                    somevalueforRighty = Mathf.FloorToInt((chunkPos.y) ) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforRightz = Mathf.CeilToInt(chunkPos.z ) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforRightz = Mathf.FloorToInt(chunkPos.z ) / realplanetwidth;
                }







                var somevalueforLeftx = 0;
                var somevalueforLefty = 0;
                var somevalueforLeftz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforLeftx = Mathf.CeilToInt((chunkPos.x - realplanetwidth) ) / realplanetwidth;
                }
                else
                {
                    somevalueforLeftx = Mathf.FloorToInt((chunkPos.x - realplanetwidth) ) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforLefty = Mathf.CeilToInt((chunkPos.y) ) / realplanetwidth;
                }
                else
                {
                    somevalueforLefty = Mathf.FloorToInt((chunkPos.y) ) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforLeftz = Mathf.CeilToInt(chunkPos.z ) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforLeftz = Mathf.FloorToInt(chunkPos.z ) / realplanetwidth;
                }





                var somevalueforFrontx = 0;
                var somevalueforFronty = 0;
                var somevalueforFrontz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforFrontx = Mathf.CeilToInt((chunkPos.x) ) / realplanetwidth;
                }
                else
                {
                    somevalueforFrontx = Mathf.FloorToInt((chunkPos.x) ) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforFronty = Mathf.CeilToInt((chunkPos.y) ) / realplanetwidth;
                }
                else
                {
                    somevalueforFronty = Mathf.FloorToInt((chunkPos.y) ) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforFrontz = Mathf.CeilToInt((chunkPos.z + realplanetwidth) ) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforFrontz = Mathf.FloorToInt((chunkPos.z + realplanetwidth) ) / realplanetwidth;
                }





                var somevalueforBackx = 0;
                var somevalueforBacky = 0;
                var somevalueforBackz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforBackx = Mathf.CeilToInt((chunkPos.x) ) / realplanetwidth;
                }
                else
                {
                    somevalueforBackx = Mathf.FloorToInt((chunkPos.x) ) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforBacky = Mathf.CeilToInt((chunkPos.y) ) / realplanetwidth;
                }
                else
                {
                    somevalueforBacky = Mathf.FloorToInt((chunkPos.y) ) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforBackz = Mathf.CeilToInt((chunkPos.z - realplanetwidth) ) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforBackz = Mathf.FloorToInt((chunkPos.z - realplanetwidth) ) / realplanetwidth;
                }*/



                /*
                var somevalueforTopx = 0;
                var somevalueforTopy = 0;
                var somevalueforTopz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforTopx = Mathf.RoundToInt(chunkPos.x) / realplanetwidth;
                }
                else
                {
                    somevalueforTopx = Mathf.RoundToInt(chunkPos.x) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforTopy = Mathf.RoundToInt((chunkPos.y + realplanetwidth)) / realplanetwidth;
                }
                else
                {
                    somevalueforTopy = Mathf.RoundToInt((chunkPos.y + realplanetwidth)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforTopz = Mathf.RoundToInt(chunkPos.z) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforTopz = Mathf.RoundToInt(chunkPos.z) / realplanetwidth;
                }







                var somevalueforBottomx = 0;
                var somevalueforBottomy = 0;
                var somevalueforBottomz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforBottomx = Mathf.RoundToInt(chunkPos.x) / realplanetwidth;
                }
                else
                {
                    somevalueforBottomx = Mathf.RoundToInt(chunkPos.x) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforBottomy = Mathf.RoundToInt((chunkPos.y - realplanetwidth)) / realplanetwidth;
                }
                else
                {
                    somevalueforBottomy = Mathf.RoundToInt((chunkPos.y - realplanetwidth)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforBottomz = Mathf.RoundToInt(chunkPos.z) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforBottomz = Mathf.RoundToInt(chunkPos.z) / realplanetwidth;
                }








                var somevalueforRightx = 0;
                var somevalueforRighty = 0;
                var somevalueforRightz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforRightx = Mathf.RoundToInt((chunkPos.x + realplanetwidth)) / realplanetwidth;
                }
                else
                {
                    somevalueforRightx = Mathf.RoundToInt((chunkPos.x + realplanetwidth)) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforRighty = Mathf.RoundToInt((chunkPos.y)) / realplanetwidth;
                }
                else
                {
                    somevalueforRighty = Mathf.RoundToInt((chunkPos.y)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforRightz = Mathf.RoundToInt(chunkPos.z) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforRightz = Mathf.RoundToInt(chunkPos.z) / realplanetwidth;
                }







                var somevalueforLeftx = 0;
                var somevalueforLefty = 0;
                var somevalueforLeftz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforLeftx = Mathf.RoundToInt((chunkPos.x - realplanetwidth)) / realplanetwidth;
                }
                else
                {
                    somevalueforLeftx = Mathf.RoundToInt((chunkPos.x - realplanetwidth)) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforLefty = Mathf.RoundToInt((chunkPos.y)) / realplanetwidth;
                }
                else
                {
                    somevalueforLefty = Mathf.RoundToInt((chunkPos.y)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforLeftz = Mathf.RoundToInt(chunkPos.z) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforLeftz = Mathf.RoundToInt(chunkPos.z) / realplanetwidth;
                }





                var somevalueforFrontx = 0;
                var somevalueforFronty = 0;
                var somevalueforFrontz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforFrontx = Mathf.RoundToInt((chunkPos.x)) / realplanetwidth;
                }
                else
                {
                    somevalueforFrontx = Mathf.RoundToInt((chunkPos.x)) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforFronty = Mathf.RoundToInt((chunkPos.y)) / realplanetwidth;
                }
                else
                {
                    somevalueforFronty = Mathf.RoundToInt((chunkPos.y)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforFrontz = Mathf.RoundToInt((chunkPos.z + realplanetwidth)) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforFrontz = Mathf.RoundToInt((chunkPos.z + realplanetwidth)) / realplanetwidth;
                }





                var somevalueforBackx = 0;
                var somevalueforBacky = 0;
                var somevalueforBackz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforBackx = Mathf.RoundToInt((chunkPos.x)) / realplanetwidth;
                }
                else
                {
                    somevalueforBackx = Mathf.RoundToInt((chunkPos.x)) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforBacky = Mathf.RoundToInt((chunkPos.y)) / realplanetwidth;
                }
                else
                {
                    somevalueforBacky = Mathf.RoundToInt((chunkPos.y)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforBackz = Mathf.RoundToInt((chunkPos.z - realplanetwidth)) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforBackz = Mathf.RoundToInt((chunkPos.z - realplanetwidth)) / realplanetwidth;
                }*/


                var somevalueforTopx = 0;
                var somevalueforTopy = 0;
                var somevalueforTopz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforTopx = Mathf.FloorToInt(chunkPos.x) / realplanetwidth;
                }
                else
                {
                    somevalueforTopx = Mathf.FloorToInt(chunkPos.x) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforTopy = Mathf.FloorToInt((chunkPos.y + realplanetwidth)) / realplanetwidth;
                }
                else
                {
                    somevalueforTopy = Mathf.FloorToInt((chunkPos.y + realplanetwidth)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforTopz = Mathf.FloorToInt(chunkPos.z) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforTopz = Mathf.FloorToInt(chunkPos.z) / realplanetwidth;
                }







                var somevalueforBottomx = 0;
                var somevalueforBottomy = 0;
                var somevalueforBottomz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforBottomx = Mathf.FloorToInt(chunkPos.x) / realplanetwidth;
                }
                else
                {
                    somevalueforBottomx = Mathf.FloorToInt(chunkPos.x) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforBottomy = Mathf.FloorToInt((chunkPos.y - realplanetwidth)) / realplanetwidth;
                }
                else
                {
                    somevalueforBottomy = Mathf.FloorToInt((chunkPos.y - realplanetwidth)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforBottomz = Mathf.FloorToInt(chunkPos.z) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforBottomz = Mathf.FloorToInt(chunkPos.z) / realplanetwidth;
                }








                var somevalueforRightx = 0;
                var somevalueforRighty = 0;
                var somevalueforRightz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforRightx = Mathf.FloorToInt((chunkPos.x + realplanetwidth)) / realplanetwidth;
                }
                else
                {
                    somevalueforRightx = Mathf.FloorToInt((chunkPos.x + realplanetwidth)) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforRighty = Mathf.FloorToInt((chunkPos.y)) / realplanetwidth;
                }
                else
                {
                    somevalueforRighty = Mathf.FloorToInt((chunkPos.y)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforRightz = Mathf.FloorToInt(chunkPos.z) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforRightz = Mathf.FloorToInt(chunkPos.z) / realplanetwidth;
                }







                var somevalueforLeftx = 0;
                var somevalueforLefty = 0;
                var somevalueforLeftz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforLeftx = Mathf.FloorToInt((chunkPos.x - realplanetwidth)) / realplanetwidth;
                }
                else
                {
                    somevalueforLeftx = Mathf.FloorToInt((chunkPos.x - realplanetwidth)) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforLefty = Mathf.FloorToInt((chunkPos.y)) / realplanetwidth;
                }
                else
                {
                    somevalueforLefty = Mathf.FloorToInt((chunkPos.y)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforLeftz = Mathf.FloorToInt(chunkPos.z) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforLeftz = Mathf.FloorToInt(chunkPos.z) / realplanetwidth;
                }





                var somevalueforFrontx = 0;
                var somevalueforFronty = 0;
                var somevalueforFrontz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforFrontx = Mathf.FloorToInt((chunkPos.x)) / realplanetwidth;
                }
                else
                {
                    somevalueforFrontx = Mathf.FloorToInt((chunkPos.x)) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforFronty = Mathf.FloorToInt((chunkPos.y)) / realplanetwidth;
                }
                else
                {
                    somevalueforFronty = Mathf.FloorToInt((chunkPos.y)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforFrontz = Mathf.FloorToInt((chunkPos.z + realplanetwidth)) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforFrontz = Mathf.FloorToInt((chunkPos.z + realplanetwidth)) / realplanetwidth;
                }





                var somevalueforBackx = 0;
                var somevalueforBacky = 0;
                var somevalueforBackz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforBackx = Mathf.FloorToInt((chunkPos.x)) / realplanetwidth;
                }
                else
                {
                    somevalueforBackx = Mathf.FloorToInt((chunkPos.x)) / realplanetwidth;
                }

                if (chunkPos.y < 0)
                {
                    somevalueforBacky = Mathf.FloorToInt((chunkPos.y)) / realplanetwidth;
                }
                else
                {
                    somevalueforBacky = Mathf.FloorToInt((chunkPos.y)) / realplanetwidth;
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforBackz = Mathf.FloorToInt((chunkPos.z - realplanetwidth)) / realplanetwidth;
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforBackz = Mathf.FloorToInt((chunkPos.z - realplanetwidth)) / realplanetwidth;
                }












                //BOTTOM FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi, yi - 1, zi))
                {
                    int someswtcBottom = 0;

                    if (yi == 0 && componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz) != null)
                    {
                        mainChunkFinal chunkdata = componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz);

                        if (chunkdata != null)
                        {
                            var comp = chunkdata.planetchunk.GetComponent<sccslodchunkfinal>();

                            if (comp != null)
                            {
                                if (comp.IsTransparent(xi, height - 1, zi))
                                {
                                    someswtcBottom = 1;


                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.transform.position, Quaternion.identity);
                                    //someObject.transform.parent = chunkdata.planetchunk.transform;
                                }
                            }
                            else
                            {
                                someswtcBottom = 1;

                            }
                        }
                        else
                        {
                            someswtcBottom = 1;
                        }
                    }
                    else if (yi != 0)
                    {
                        someswtcBottom = 1;
                    }
                    else
                    {
                        if (componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz) == null)
                        {
                            someswtcBottom = 1;
                        }
                    }

                    if (someswtcBottom == 1)
                    {
                        buildBottomFace();
                    }
                }
















                //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi - 1, yi, zi))
                {
                    int someswtcLeft = 0;

                    if (xi == 0 && componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) != null)
                    {
                        mainChunkFinal chunkdata = componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);

                        if (chunkdata != null)
                        {
                            var comp = chunkdata.planetchunk.GetComponent<sccslodchunkfinal>();

                            if (comp != null)
                            {
                                if (comp.IsTransparent(width - 1, yi, zi))
                                {
                                    someswtcLeft = 1;


                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.transform.position, Quaternion.identity);
                                    //someObject.transform.parent = chunkdata.planetchunk.transform;
                                }
                            }
                            else
                            {
                                someswtcLeft = 1;

                            }
                        }
                        else
                        {
                            someswtcLeft = 1;
                        }
                    }
                    else if (xi != 0)
                    {
                        someswtcLeft = 1;
                    }
                    else
                    {
                        if (componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) == null)
                        {
                            someswtcLeft = 1;
                        }
                    }

                    if (someswtcLeft == 1)
                    {
                        buildTopLeft();
                    }
                }




                //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi, yi, zi - 1))
                {
                    int someswtcBack = 0;

                    if (zi == 0 && componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) != null)
                    {
                        mainChunkFinal chunkdata = componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz);

                        if (chunkdata != null)
                        {
                            var comp = chunkdata.planetchunk.GetComponent<sccslodchunkfinal>();

                            if (comp != null)
                            {
                                if (comp.IsTransparent(xi, yi, depth - 1))
                                {
                                    someswtcBack = 1;
                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.transform.position, Quaternion.identity);
                                    //someObject.transform.parent = chunkdata.planetchunk.transform;
                                }
                            }
                            else
                            {
                                someswtcBack = 1;

                            }
                        }
                        else
                        {
                            someswtcBack = 1;
                        }
                    }
                    else if (zi != 0)
                    {
                        someswtcBack = 1;
                    }
                    else
                    {
                        if (componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) == null)
                        {
                            someswtcBack = 1;
                        }
                    }

                    if (someswtcBack == 1)
                    {
                        buildBackFace();
                    }
                }





                //TOP FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi, yi + 1, zi))
                {
                    int someswtcTop = 0;
                    if (yi == height - 1 && componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) != null)
                    {
                        mainChunkFinal chunkdata = componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz);

                        if (chunkdata != null)
                        {
                            var comp = chunkdata.planetchunk.GetComponent<sccslodchunkfinal>();

                            if (comp != null)
                            {
                                if (comp.IsTransparent(xi, 0, zi))
                                {
                                    someswtcTop = 1;

                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.transform.position, Quaternion.identity);
                                    //someObject.transform.parent = chunkdata.planetchunk.transform;
                                }
                            }
                            else
                            {
                                someswtcTop = 1;
                            }
                        }
                        else
                        {
                            someswtcTop = 1;
                        }
                    }
                    else if (yi != height - 1)
                    {
                        someswtcTop = 1;
                    }
                    else
                    {
                        if (componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                        {
                            someswtcTop = 1;
                        }
                    }

                    if (someswtcTop == 1)
                    {
                        buildTopFace();
                    }
                }

                //RIGHT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi + 1, yi, zi))
                {
                    int someswtcRight = 0;

                    if (xi == width - 1 && componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) != null)
                    {
                        mainChunkFinal chunkdata = componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz);

                        if (chunkdata != null)
                        {
                            var comp = chunkdata.planetchunk.GetComponent<sccslodchunkfinal>();

                            if (comp != null)
                            {
                                if (comp.IsTransparent(0, yi, zi))
                                {
                                    someswtcRight = 1;


                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.transform.position, Quaternion.identity);
                                    //someObject.transform.parent = chunkdata.planetchunk.transform;
                                }
                            }
                            else
                            {
                                someswtcRight = 1;

                            }
                        }
                        else
                        {
                            someswtcRight = 1;
                        }
                    }
                    else if (xi != width - 1)
                    {
                        someswtcRight = 1;
                    }
                    else
                    {
                        if (componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) == null)
                        {
                            someswtcRight = 1;
                        }
                    }

                    if (someswtcRight == 1)
                    {
                        buildTopRight();
                    }
                }



                //FRONT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi, yi, zi + 1))
                {
                    int someswtcFront = 0;

                    if (zi == depth - 1 && componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) != null)
                    {
                        mainChunkFinal chunkdata = componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz);

                        if (chunkdata != null)
                        {
                            var comp = chunkdata.planetchunk.GetComponent<sccslodchunkfinal>();

                            if (comp != null)
                            {
                                if (comp.IsTransparent(xi, yi, 0))
                                {

                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.transform.position + new Vector3(xi * _planeSize, yi*_planeSize,0), Quaternion.identity);
                                    //someObject.transform.parent = chunkdata.planetchunk.transform;

                                    someswtcFront = 1;
                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.transform.position, Quaternion.identity);
                                    //someObject.transform.parent = chunkdata.planetchunk.transform;
                                }
                            }
                            else
                            {
                                someswtcFront = 1;

                            }
                        }
                        else
                        {
                            someswtcFront = 1;
                        }
                    }
                    else if (zi != depth - 1)
                    {
                        someswtcFront = 1;
                    }
                    else
                    {
                        if (componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) == null)
                        {
                            someswtcFront = 1;
                        }
                    }

                    if (someswtcFront == 1)
                    {
                        buildFrontFace();
                    }
                }







                /*
                if (IsTransparent(xiBytes, yiBytes, ziBytes + 1))
                {
                    int someswtcFront = 0;

                    if (ziBytes == depth - 1 && componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y), Mathf.RoundToInt(chunkPos.z + realplanetwidth)) != null)
                    {
                        mainChunkFinal chunkdata = componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y), Mathf.RoundToInt(chunkPos.z + realplanetwidth));

                        if (chunkdata != null)
                        {
                            var comp = chunkdata.planetchunk.GetComponent<sccslodchunkfinal>();

                            if (comp != null)
                            {
                                if (comp.IsTransparent(xiBytes, yiBytes, 0))
                                {

                                    GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.transform.position + new Vector3(xi * _planeSize, yi * _planeSize, 0), Quaternion.identity);
                                    someObject.transform.parent = chunkdata.planetchunk.transform;

                                    someswtcFront = 1;
                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.transform.position, Quaternion.identity);
                                    //someObject.transform.parent = chunkdata.planetchunk.transform;
                                }
                            }
                            else
                            {
                                someswtcFront = 1;

                            }
                        }
                        else
                        {
                            someswtcFront = 1;
                        }
                    }
                    else if (ziBytes != depth - 1)
                    {
                        someswtcFront = 1;
                    }
                    else
                    {
                        if (componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y), Mathf.RoundToInt(chunkPos.z + realplanetwidth)) == null)
                        {
                            //someswtcFront = 1;
                        }
                    }

                    if (someswtcFront == 1)
                    {
                        buildFrontFace();
                    }
                }
                */













                //_block = _tempChunkArray[temptopfacexi + width * (temptopfaceyi + height * temptopfacezi)];
                //buildTopFace();
                //buildBottomFace();
                /*buildTopLeft();
                buildTopRight();
                buildFrontFace();
                buildBackFace();*/


                zzBytes++;
                if (zzBytes >= (depth))
                {
                    yyBytes++;
                    zzBytes = 0;
                    swtchyBytes = 1;
                }
                if (yyBytes >= (height) && swtchyBytes == 1)
                {
                    xxBytes++;
                    yyBytes = 0;
                    swtchyBytes = 0;
                    swtchxBytes = 1;
                }
                if (xxBytes >= (width) && swtchxBytes == 1)
                {
                    swtchyBytes = 0;
                    swtchxBytes = 0;
                    swtchzBytes = 1;
                }

                tBytes++;
                counterCreateChunkObjectFacesBytes++;
            }
        }
    }

    //int _milli = 0;
    int _maxHeight = 0;


    //UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWidth: " + _maxWidth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
    void buildTopFace() //int _x, int _y, int _z, Vector3 chunkPos
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArray[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {

            //if (IsTransparent(temptopfacexi, temptopfaceyi + 1, temptopfacezi))
            {
                for (int _xx = 0; _xx < _maxWidth; _xx++)
                {
                    rowIterateX = xi + _xx;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = zi + _zz;

                        if (rowIterateX < width && rowIterateZ < depth)
                        {



                            /*
                            if (yi == 0 && componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y - realplanetwidth), Mathf.RoundToInt(chunkPos.z)) != null ||
                                yiBytes == 0 && componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y - realplanetwidth), Mathf.RoundToInt(chunkPos.z)) != null)
                            {
                                mainChunkFinal chunkdata = componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y - realplanetwidth), Mathf.RoundToInt(chunkPos.z));

                                if (chunkdata != null)
                                {
                                    var comp = chunkdata.planetchunk.GetComponent<sccslodchunkfinal>();

                                    if (comp != null)
                                    {
                                        if (comp.IsTransparent(rowIterateX, height - 1, rowIterateZ) || comp.IsTransparent(xiBytes, height - 1, ziBytes) || comp.IsTransparent(xi, height - 1, zi))
                                        {
                                            someswtc = 1;
                                        }
                                        else
                                        {
                                            //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.transform.position, Quaternion.identity);
                                            //someObject.transform.parent = chunkdata.planetchunk.transform;
                                        }
                                    }
                                    else
                                    {
                                        someswtc = 1;
                                    }
                                }
                                else
                                {
                                    someswtc = 1;
                                }
                            }
                            else if (yi != 0 || yiBytes != 0)
                            {
                                someswtc = 1;
                            }
                            else
                            {
                                if (componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y - realplanetwidth), Mathf.RoundToInt(chunkPos.z)) == null)
                                {
                                    someswtc = 1;
                                }
                            }*/









                            //if (someswtc == 1)
                            {
                                if (_xx == 0 && _zz == 0)
                                {
                                    oneVertIndexX = rowIterateX;
                                    oneVertIndexY = yi + 1;
                                    oneVertIndexZ = rowIterateZ;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);

                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi + 1;
                                                    threeVertIndexZ = rowIterateZ;
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                                {
                                                    _block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];

                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = yi + 1;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi + 1;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_xx == 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                                {
                                                    _block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = yi + 1;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi + 1;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_xx > 0 && _zz == 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                        {
                                            //*****************************************************************************
                                            _block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_xx > 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWidth = _xx;

                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                        {
                            _tempChunkArray[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                            ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * _planeSize, oneVertIndexY * _planeSize, oneVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * _planeSize, twoVertIndexY * _planeSize, twoVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * _planeSize, threeVertIndexY * _planeSize, threeVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*_planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * _planeSize, fourVertIndexY * _planeSize, fourVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
    }

    int rowIterateY = 0;





    void buildTopLeft() //int _x, int _y, int _z, Vector3 chunkPos
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayLeftFace[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(xi - 1, yi, zi))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = yi + _yy;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = zi + _zz;

                        if (rowIterateY < height && rowIterateZ < depth)
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = xi;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = rowIterateZ;
                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = xi;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = xi;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = xi;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = xi;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = xi;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = xi;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = xi;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = xi;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = xi;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = xi;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = xi;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = xi;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = xi;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ - _zz;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = xi;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayLeftFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * _planeSize, oneVertIndexY * _planeSize, oneVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * _planeSize, twoVertIndexY * _planeSize, twoVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * _planeSize, threeVertIndexY * _planeSize, threeVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*_planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * _planeSize, fourVertIndexY * _planeSize, fourVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = _trigz.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/

        /*_mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }

    void buildTopRight() //int xi, int _y, int _z, Vector3 chunkPos
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayRightFace[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(xi + 1, yi, zi))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = yi + _yy;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = zi + _zz;

                        if (rowIterateY < height && rowIterateZ < depth)
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = xi + 1;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = rowIterateZ;
                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = xi + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = xi + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = xi + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = xi + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = xi + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = xi + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = xi + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = xi + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = xi + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = xi + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ - _zz;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayRightFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * _planeSize, oneVertIndexY * _planeSize, oneVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * _planeSize, twoVertIndexY * _planeSize, twoVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * _planeSize, threeVertIndexY * _planeSize, threeVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*_planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * _planeSize, fourVertIndexY * _planeSize, fourVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
    }




    void buildFrontFace() // int _x, int _y, int _z, Vector3 chunkPos
    {

        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayFrontFace[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(xi, yi, zi + 1))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = yi + _yy;
                    for (int _xx = 0; _xx < _maxWidth; _xx++)
                    {
                        rowIterateX = xi + _xx;

                        if (rowIterateY < height && rowIterateX < width)
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = zi + 1;
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi + 1;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi + 1;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = zi + 1;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi + 1;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = zi + 1;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = zi + 1;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi + 1;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = zi + 1;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi + 1;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi + 1;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX - _xx;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi + 1;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi + 1;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                        {
                            _tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3(oneVertIndexX * _planeSize, oneVertIndexY * _planeSize, oneVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3(twoVertIndexX * _planeSize, twoVertIndexY * _planeSize, twoVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3(threeVertIndexX * _planeSize, threeVertIndexY * _planeSize, threeVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*_planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3(fourVertIndexX * _planeSize, fourVertIndexY * _planeSize, fourVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    /*if (map[x, y, z] == leftExtremity[x, y, z]
                     || map[x, y, z] == backExtremity[x, y, z]
                     || map[x, y, z] == rightExtremity[x, y, z]
                     || map[x, y, z] == frontExtremity[x, y, z]
                     || map[x, y, z] == leftInsideCornerExtremity[x, y, z]
                     || map[x, y, z] == rightInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == backInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == frontInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == leftOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == rightOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == backOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == frontOutsideCornerExtremity[x, y, z])
                    {
                        uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                        uv.Add(new Vector2(0.0625f, 0.9375f));
                        uv.Add(new Vector2(0, 0.875f));
                        uv.Add(new Vector2(0.0625f, 0.875f));
                    }
                    else
                    {
                        uv.Add(new Vector2(0, 1)); //// dis is weed
                        uv.Add(new Vector2(0.0625f, 1));
                        uv.Add(new Vector2(0, 0.9375f));
                        uv.Add(new Vector2(0.0625f, 0.9375f));
                    }*/


                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/


                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = _trigz.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
    }


    void buildBackFace() //int _x, int _y, int zi, Vector3 chunkPos
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayBackFace[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(xi, yi, zi - 1))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = yi + _yy;
                    for (int _xx = 0; _xx < _maxWidth; _xx++)
                    {
                        rowIterateX = xi + _xx;

                        if (rowIterateY < height && rowIterateX < width)
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = zi;
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = zi;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = zi;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = zi;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = zi;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX - _xx;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                        {
                            _tempChunkArrayBackFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3(oneVertIndexX * _planeSize, oneVertIndexY * _planeSize, oneVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3(twoVertIndexX * _planeSize, twoVertIndexY * _planeSize, twoVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3(threeVertIndexX * _planeSize, threeVertIndexY * _planeSize, threeVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*_planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3(fourVertIndexX * _planeSize, fourVertIndexY * _planeSize, fourVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    /*if (map[x, y, z] == leftExtremity[x, y, z]
                     || map[x, y, z] == backExtremity[x, y, z]
                     || map[x, y, z] == rightExtremity[x, y, z]
                     || map[x, y, z] == frontExtremity[x, y, z]
                     || map[x, y, z] == leftInsideCornerExtremity[x, y, z]
                     || map[x, y, z] == rightInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == backInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == frontInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == leftOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == rightOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == backOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == frontOutsideCornerExtremity[x, y, z])
                    {
                        uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                        uv.Add(new Vector2(0.0625f, 0.9375f));
                        uv.Add(new Vector2(0, 0.875f));
                        uv.Add(new Vector2(0.0625f, 0.875f));
                    }
                    else
                    {
                        uv.Add(new Vector2(0, 1)); //// dis is weed
                        uv.Add(new Vector2(0.0625f, 1));
                        uv.Add(new Vector2(0, 0.9375f));
                        uv.Add(new Vector2(0.0625f, 0.9375f));
                    }*/


                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/


                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                }
            }
        }
    }




    void buildBottomFace() //int _x, int _y, int _z, Vector3 chunkPos
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayBottomFace[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(xi, yi - 1, zi))
            {
                for (int _xx = 0; _xx < _maxWidth; _xx++)
                {
                    rowIterateX = xi + _xx;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = zi + _zz;

                        if (rowIterateX < width && rowIterateZ < depth)
                        {
                            if (_xx == 0 && _zz == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = yi;
                                oneVertIndexZ = rowIterateZ;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ) * __planeSize + chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * __planeSize + chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * __planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = yi;
                                    threeVertIndexZ = rowIterateZ;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * __planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * __planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = yi;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * __planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * __planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = yi;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * __planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * __planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = yi;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * __planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * __planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = yi;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                    }
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * __planeSize + chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * __planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * __planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_xx > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * __planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * __planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = yi;
                                    threeVertIndexZ = rowIterateZ - _zz;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * __planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, yi + 1, rowIterateZ - _zz) * __planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;

                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * __planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * __planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                        {
                            _tempChunkArrayBottomFace[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * __planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * _planeSize, oneVertIndexY * _planeSize, oneVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * _planeSize, twoVertIndexY * _planeSize, twoVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * _planeSize, threeVertIndexY * _planeSize, threeVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*_planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * _planeSize, fourVertIndexY * _planeSize, fourVertIndexZ * _planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
    }







    /*







    /*
    void buildVertex(int _x, int _y, int _z)
    {
        //TOPFACE
        if (IsTransparent(_x, _y + 1, _z))
        {
            if (getChunkVertexByte(_x, _y + 1, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = 1;
                _testVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x + 1, _y + 1, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = _newVertzCounter;
                //_vertices[vertexlistCounter] = new Vector3(_x + 1, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = 1;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x, _y + 1, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                _testVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z + 1);
                _chunkVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = 1;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                //_vertices[vertexlistCounter] = new Vector3(_x + 1, _y + 1, _z + 1);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = 1;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y + 1, _z) == 1 && getChunkVertexByte(_x + 1, _y + 1, _z) == 1 && getChunkVertexByte(_x, _y + 1, _z + 1) == 1 && getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 1)
            {
                _index0 = _testVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)];
                _index1 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)];
                _index2 = _testVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))];
                _index3 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))];

                triangles.Add(_index2);
                triangles.Add(_index1);
                triangles.Add(_index0);
                triangles.Add(_index1);
                triangles.Add(_index2);
                triangles.Add(_index3);

                /*mesh.vertices = vertexlist.ToArray();
                mesh.triangles = triangles.ToArray();
                _testChunk.GetComponent<MeshFilter>().mesh = mesh;
                meshRend = _testChunk.GetComponent<MeshRenderer>();
            }
        }

        //LEFTFACE
        if (IsTransparent(_x - 1, _y, _z))
        {
            if (getChunkVertexByte(_x, _y + 1, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(-1, 0, 0));
                //_uvs.Add(new Vector2(0f, 0f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = 1;
                _testVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y + 1, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize));
                //_normals.Add(new Vector3(-1, 0, 0));
                //_uvs.Add(new Vector2(0f, 0f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = 1;
                _testVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(-1, 0, 0));
                //_uvs.Add(new Vector2(0f, 0f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))] = 1;
                _testVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize, _z * __planeSize));
                //_normals.Add(new Vector3(-1, 0, 0));
                ////_uvs.Add(new Vector2(0f, 0f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))] = 1;
                _testVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y + 1, _z + 1) == 1 && getChunkVertexByte(_x, _y + 1, _z) == 1 && getChunkVertexByte(_x, _y, _z + 1) == 1 && getChunkVertexByte(_x, _y, _z) == 1)
            {
                _index0 = _testVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))];
                _index1 = _testVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)];
                _index2 = _testVertexArray[_x + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))];
                _index3 = _testVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))];

                triangles.Add(_index0);
                triangles.Add(_index1);
                triangles.Add(_index2);
                triangles.Add(_index3);
                triangles.Add(_index2);
                triangles.Add(_index1);
            }
        }

        //RIGHTFACE
        if (IsTransparent(_x + 1, _y, _z))
        {
            if (getChunkVertexByte(_x + 1, _y, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize, _z * __planeSize));
                //_normals.Add(new Vector3(+1, 0, 0));
                //_uvs.Add(new Vector2(1f, 0f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))] = 1;
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y + 1, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize));
                //_normals.Add(new Vector3(+1, 0, 0));
                //_uvs.Add(new Vector2(1f, 0f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = 1;
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(+1, 0, 0));
                //_uvs.Add(new Vector2(1f, 0f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))] = 1;
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(+1, 0, 0));
                //_uvs.Add(new Vector2(0f, 1f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = 1;
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y, _z) == 1 && getChunkVertexByte(_x + 1, _y + 1, _z) == 1 && getChunkVertexByte(_x + 1, _y, _z + 1) == 1 && getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 1)
            {
                _index0 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))];
                _index1 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)];
                _index2 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))];
                _index3 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))];

                triangles.Add(_index0);
                triangles.Add(_index1);
                triangles.Add(_index2);
                triangles.Add(_index3);
                triangles.Add(_index2);
                triangles.Add(_index1);
            }
        }
  
        //FRONTFACE
        if (IsTransparent(_x , _y, _z-1))
        {
            if (getChunkVertexByte(_x + 1, _y, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize, _z * __planeSize));

                //_normals.Add(new Vector3(0, 0, -1));
                //_uvs.Add(new Vector2(1f, 0f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))] = 1;
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize, _z * __planeSize));

                //_normals.Add(new Vector3(0, 0, -1));
                //_uvs.Add(new Vector2(1f, 1f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x ) + vertexlistWidth * ((_y) + vertexlistHeight * _z)] = 1;
                _testVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y+1, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize));

                //_normals.Add(new Vector3(0, 0, -1));
                //_uvs.Add(new Vector2(1f, 0f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y+1) + vertexlistHeight * (_z ))] = 1;
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y+1) + vertexlistHeight * (_z ))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y + 1, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize));

                //_normals.Add(new Vector3(0, 0, -1));
                //_uvs.Add(new Vector2(0f, 1f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z))] = 1;
                _testVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y, _z) == 1 && getChunkVertexByte(_x, _y, _z) == 1 && getChunkVertexByte(_x + 1, _y+1, _z) == 1 && getChunkVertexByte(_x, _y + 1, _z) == 1)
            {
                _index0 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))];
                _index1 = _testVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * _z)];
                _index2 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z))];
                _index3 = _testVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z))];

                triangles.Add(_index0);
                triangles.Add(_index1);
                triangles.Add(_index2);
                triangles.Add(_index3);
                triangles.Add(_index2);
                triangles.Add(_index1);
            }
        }

        //BACKFACE
        if (IsTransparent(_x, _y, _z + 1))
        {
            if (getChunkVertexByte(_x, _y, _z+1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(0, 0, +1));
                //_uvs.Add(new Vector2(1f, 1f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x ) + vertexlistWidth * ((_y) + vertexlistHeight * (_z+1))] = 1;
                _testVertexArray[(_x ) + vertexlistWidth * ((_y) + vertexlistHeight * (_z+1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(0, 0, +1));
                //_uvs.Add(new Vector2(1f, 0f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))] = 1;
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y + 1, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(0, 0, +1));
                //_uvs.Add(new Vector2(1f, 1f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = 1;
                _testVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize + 1 * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(0, 0, +1));
                //_uvs.Add(new Vector2(0f, 1f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = 1;
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y, _z + 1) == 1 && getChunkVertexByte(_x + 1, _y, _z + 1) == 1 && getChunkVertexByte(_x, _y + 1, _z + 1) == 1 && getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 1)
            {
                _index0 = _testVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))];
                _index1 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))];
                _index2 = _testVertexArray[(_x) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))];
                _index3 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))];

                triangles.Add(_index0);
                triangles.Add(_index1);
                triangles.Add(_index2);
                triangles.Add(_index3);
                triangles.Add(_index2);
                triangles.Add(_index1);
            }
        }

     
        //BOTTOMFACE
        if (IsTransparent(_x, _y-1, _z))
        {
            if (getChunkVertexByte(_x, _y, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize, _z * __planeSize));
                //_normals.Add(new Vector3(0, -1, 0));
                //_uvs.Add(new Vector2(0f, 1f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * _z)] = 1;
                _testVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize, _z * __planeSize));
                //_normals.Add(new Vector3(0, -1, 0));
                ///_uvs.Add(new Vector2(0f, 1f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))] = 1;
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z))] = _newVertzCounter;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x, _y, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize, _y * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(0, -1, 0));
                //_uvs.Add(new Vector2(0f, 1f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))] = 1;
                _testVertexArray[(_x) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x + 1, _y, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * __planeSize + 1 * __planeSize, _y * __planeSize, _z * __planeSize + 1 * __planeSize));
                //_normals.Add(new Vector3(0, -1, 0));
                //_uvs.Add(new Vector2(0f, 1f));
                ////////Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))] = 1;
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x, _y, _z) == 1 && getChunkVertexByte(_x + 1, _y, _z) == 1 && getChunkVertexByte(_x, _y, _z + 1) == 1 && getChunkVertexByte(_x + 1, _y, _z + 1) == 1)
            {
                _index0 = _testVertexArray[_x + vertexlistWidth * ((_y) + vertexlistHeight * _z)];
                _index1 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * _z)];
                _index2 = _testVertexArray[_x + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))];
                _index3 = _testVertexArray[(_x + 1) + vertexlistWidth * ((_y) + vertexlistHeight * (_z + 1))];

                triangles.Add(_index0);
                triangles.Add(_index1);
                triangles.Add(_index2);
                triangles.Add(_index3);
                triangles.Add(_index2);
                triangles.Add(_index1);
            }
        }



        //meshRend.material = _mat;

    }*/


    int addfracturedcubeonimpact = 0;

    public void SetByte(int x, int y, int z, byte block, Vector3 chunkbytepos_)
    {
        if (addfracturedcubeonimpact == 1)
        {
            //var unityTutorialObjectPool = this.transform.GetComponent<NewObjectPoolerScript>();
            var UnityTutorialPooledObject = UnityTutorialGameObjectPool.GetPooledObject();
            UnityTutorialPooledObject.transform.position = chunkbytepos_;
            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
            UnityTutorialPooledObject.SetActive(true);
        }

        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            //Debug.Log("out of range");
            return;
        }

        int indexOf = x + width * (y + depth * z);
        map[indexOf] = block;


        //sccsSetMap();

        /*if (this.gameObject.GetComponent<MeshCollider>() != null)
        {
            Destroy(this.gameObject.GetComponent<MeshCollider>());
        }*/
        //Destroy(this.gameObject.GetComponent<MeshFilter>());

        //verts.Clear();
        //tris.Clear();

        //Regenerate();

        //return map[x + width * (y + depth * z)];
    }


    public int GetByte(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            return 0;
        }

        int indexOf = x + width * (y + depth * z);
        return map[indexOf];
        //return map[x + width * (y + depth * z)];
    }

























    //LEFTFACE
    //positions[0 + vertzIndex] = start; //(x,y+1,z+1)
    //positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
    //positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
    //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)



    //RIGHTFACE
    //positions[0 + vertzIndex] = start; // (x+1,y,z)
    //positions[1 + vertzIndex] = start + offset1; // (x+1,y+1,z)
    //positions[2 + vertzIndex] = start + offset2; // // (x+1,y,z+1)
    //positions[3 + vertzIndex] = start + offset1 + offset2; //(x+1,y+1,z+1)


    //FRONTFACE
    //positions[0 + vertzIndex] = start; //(x+1,y,z)
    //positions[1 + vertzIndex] = start + offset1;//(x,y,z)
    //positions[2 + vertzIndex] = start + offset2;//(x+1,y+1,z)
    //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y+1,z)

    //BACKFACE
    //positions[0 + vertzIndex] = start; //(x,y,z+1)
    //positions[1 + vertzIndex] = start + offset1;//(x+1,y,z+1)
    //positions[2 + vertzIndex] = start + offset2;//(x,y+1,z+1)
    //positions[3 + vertzIndex] = start + offset1 + offset2;//(x+1,y+1,z+1)

    //BOTTOMFACE
    //positions[0 + vertzIndex] = start; //(x,y,z)
    //positions[1 + vertzIndex] = start + offset1; //(x+1,y,z)
    //positions[2 + vertzIndex] = start + offset2;//(x,y,z+1)
    //positions[3 + vertzIndex] = start + offset1 + offset2;//(x+1,y,z+1)


    /*private Vector3 forward = new Vector3(0, 0, 1);
    private Vector3 back = new Vector3(0, 0, -1);
    private Vector3 right = new Vector3(1, 0, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 up = new Vector3(0, 1, 0);
    private Vector3 down = new Vector3(0, -1, 0);*/

    public bool blockExistsInArray(int _x, int _y, int _z)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth))
        {
            return false;
        }
        /*else if (_chunkArray[_x + width * (_y + height * _z)]==0)
        {
            return false;
        }*/
        else
        {
            return true;
        }
        //return _chunkArray[_x + width * (_y + height * _z)] == 0;
    }

    public bool IsTransparent(int _x, int _y, int _z)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth)) return true;
        return _chunkArray[_x + width * (_y + height * _z)] == 0;
    }

    int getChunkByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
        {
            return _chunkArray[_x + width * (_y + height * _z)];
        }
        return 0;
    }


    int getTempArrayByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
        {
            return _tempChunkArray[_x + width * (_y + height * _z)];
        }
        return 0;
    }



    int getChunkVertexByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
        {
            return _chunkVertexArray[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
        }
        return 0;
    }


    /*byte ChunkVertexByteExists(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
        {
            return _chunkVertexArray[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
        }
        return 0;
    }*/

    /*bool byteExists(int _x, int _y, int _z)
    {
        if (_x < 0 || _y < 0 || _z < 0 || _x > width || _y > height || _z > depth) return false;
        {
            return true;
        }
    }*/

    /* private void OnDrawGizmos()
     {
         if (_vertices == null)
         {
             return;
         }
         else
         {
             for (int i = 0; i < _vertices.Length; i++)
             {
                 Gizmos.color = Color.black;
                 Gizmos.DrawSphere(_vertices[i],0.1f);
             }
         }
     }*/

    /*private void OnDrawGizmos()
    {
        if (vertexlist == null)
        {
            return;
        }
        for (int i = 0; i < vertexlist.Count; i++)
        {
            //Gizmos.color = Color.black;
            //Gizmos.DrawSphere(vertexlist[i], 0.1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(vertexlist[i], _normals[i]);
        }
    }*/
}




/*int _currentMaxIndex = _lastCorner3;
//UnityEngine.Debug.Log(1 ^ _currentMaxIndex);

int y = 0;
int total = 0;
int _threshold = _currentMaxIndex;

for (int x = 0; x<_currentMaxIndex; x++)
{
if (y > (_threshold - 1))
{
y = 0; //reset
total += x;
}
y += 1;
}
UnityEngine.Debug.Log(y);
UnityEngine.Debug.Log(_currentMaxIndex + "__currentMaxIndex ");
*/


/*var _numberOfVerticesThatAreSharedVertices = _faceCount * 2; // because 2 is the minimum for new faces. whatever = 18 + 4 + 4 so 
float _vertsPerFace = 4;
int x = 0;
int _numberOfFacesThatUseFourNewVertices = 0;
for (; x < _currentMaxIndex; x++)
{
_numberOfFacesThatUseFourNewVertices++;
if (_numberOfFacesThatUseFourNewVertices * _vertsPerFace + _numberOfVerticesThatAreSharedVertices - _currentMaxIndex == 0)
{
break;
}
else
{
_numberOfVerticesThatAreSharedVertices -= 2;
}
}
UnityEngine.Debug.Log(_numberOfFacesThatUseFourNewVertices);
UnityEngine.Debug.Log(_numberOfVerticesThatAreSharedVertices);
*/

