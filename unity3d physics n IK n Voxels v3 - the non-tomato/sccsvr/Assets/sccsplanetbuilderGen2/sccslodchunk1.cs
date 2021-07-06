using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEditor;

public class sccslodchunk1 : MonoBehaviour
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
    int swtchz = 0;



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
    int swtchzBytes = 0;

    int rowIterateXBytes = 0;
    int rowIterateZBytes = 0;
    int rowIterateYBytes = 0;


    //int _mapWidth = 20;
    //int _mapHeight = 10;
    //int _mapDepth = 20;

    //int _indexOfChunk = 0;

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
    //Vector3[] _normals;
    //Vector2[] _uvs;
    int[] _triangles;

    List<int> triangles;

    //int _totalVertex = 0;

    int vertexlistCounter = 0;

    int _detailScale = 10;

    Stopwatch _stopWatch = new Stopwatch();

    public float _planeSize = 0.1f;
    GameObject _testChunk;
    public Vector3 chunkPos;

    public List<Vector3> vertexlist = new List<Vector3>();
    List<Vector3> _normals = new List<Vector3>();
    List<Vector2> _uvs = new List<Vector2>();

    List<Vector4> _toShaderArray = new List<Vector4>();

    Mesh mesh;

    public GameObject _sphereVisual;
    public GameObject _sphereVisualOtherColorOrange;
    public Shader _shader;
    public Material _mat;
    MeshRenderer meshRend;

    int _index0 = 0;
    int _index1 = 0;
    int _index2 = 0;
    int _index3 = 0;

    int _newVertzCounter = 0;
    //MeshCombineUtility meshCombineUtility;
    chunkData[] _arrayOfChunks;


    private void OldStart()
    {
        //_arrayOfChunks = SC_Terrain._arrayOfChunks;// new chunk[width * _mapWidth * width * _mapHeight * width * _mapDepth];
        //_indexOfChunk = SC_Terrain._indexInArrayOfChunks;
        //meshCombineUtility = new MeshCombineUtility();

        _shader = _mat.shader;
        vertexlistWidth = width + 1;
        vertexlistHeight = height + 1;
        vertexlistDepth = depth + 1;
        _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

        //_vertices = new Vector3[1];

        //meshFilters = new MeshFilter[_combineMax];
        //_testChunk = this.gameObject;

        //StartCoroutine(buildTerrain(chunkPos));
        chunkPos = this.transform.position;
        buildTerrain(chunkPos);

    }



    chunkData[] _arrayOfChunkDataCHUNK;
    int[] _arrayOfChunkCHUNK;

    public void buildTerrain(Vector3 chunkPos)
    {
        //_arrayOfChunks = SC_Terrain._arrayOfChunks;// new chunk[width * _mapWidth * width * _mapHeight * width * _mapDepth];
        //_indexOfChunk = SC_Terrain._indexInArrayOfChunks;

        //width = SC_Terrain.width;
        //width = SC_Terrain.height;
        //width = SC_Terrain.depth;

        //_arrayOfChunkDataCHUNK = SC_Terrain._arrayOfChunkData;
        //_arrayOfChunkCHUNK = SC_Terrain._arrayOfChunk;

        vertexlistWidth = width + 1;
        vertexlistHeight = height + 1;
        vertexlistDepth = depth + 1;
        _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

        //_vertices = new Vector3[1];

        //meshFilters = new MeshFilter[_combineMax];

        //_chunkData = buildingTerrain(chunkPos);
        //_chunkData = new chunkData();
        //UnityEngine.Debug.Log("test");
    }





    int _indexOfInstance = 0;

    //MeshInstance[] combines;
    MeshFilter[] meshFilters;

    MeshRenderer meshRenderer;

    bool _stop = true;

    GameObject _newChunk;

    bool _dontAdd = true;



    bool resultsOne = false;
    bool resultsTwo = false;
    bool resultsThree = false;

    public GameObject _vertVisual;
    public GameObject _sphereVisualOtherColor;
    public GameObject _sphereVisualOtherColorRed;
    public GameObject _sphereVisualOtherColorBlue;
    public GameObject _sphereVisualOtherColorBlack;



    float size0 = 0;
    float temporaryY = 0;
    chunkData _chunkData;

    chunkData buildingTerrain(Vector3 chunkPos) //unsafe
    {
        _chunkData = new chunkData();


        _stopWatch.Stop();
        _stopWatch.Reset();
        _stopWatch.Start();


        //fixed (byte* _array = _chunkArray)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        temporaryY = 10f;
                        //float temporaryZ = 10f;
                        //float temporaryX = 10f;

                        temporaryY *= Mathf.PerlinNoise((x * _planeSize + chunkPos.x + _seed) / _detailScale, (z * _planeSize + chunkPos.z + _seed) / _detailScale) * heightScale;
                        //temporaryX *= Mathf.PerlinNoise((y * _planeSize + chunkPos.y + _seed) / _detailScale, (z * _planeSize + chunkPos.z + _seed) / _detailScale) * heightScale;
                        //temporaryZ *= Mathf.PerlinNoise((x * _planeSize + chunkPos.x + _seed) / _detailScale, (y * _planeSize + chunkPos.y + _seed) / _detailScale) * heightScale;

                        size0 = (1 / _planeSize) * chunkPos.y;
                        temporaryY -= size0;

                        //_chunkArray[x + width * (y + height * z)] = 1;

                        /*if ((int)Math.Round(temporaryY) >= y)
                        {
                            _chunkArray[x + width * (y + height * z)] = 1;
                            _tempChunkArray[x + width * (y + height * z)] = 1;
                            _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                            _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                            _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                            _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                            _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                        }
                        else
                        {
                            _chunkArray[x + width * (y + height * z)] = 0;
                            _tempChunkArray[x + width * (y + height * z)] = 0;
                            _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                            _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;


                            _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                            _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                            _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;

                        }*/

                        /*if (Math.Floor(temporaryY) >= y + 1)
                        {
                            _perlinChunkArray[x + width * (y + height * z)] = 2;
                        }

                        else if (Math.Floor(temporaryY) < y - 1)
                        {
                            _perlinChunkArray[x + width * (y + height * z)] = 1;
                        }
                        else
                        {
                            _perlinChunkArray[x + width * (y + height * z)] = 0;
                        }*/

                        /*float size1 = (1 / _planeSize) * chunkPos.x;
                        temporaryX -= size1;

                        float size2 = (1 / _planeSize) * chunkPos.z;
                        temporaryZ -= size2;
                        */
                        /*if ((int)Math.Round(temporaryY) >= y && (int)Math.Round(temporaryX) >= x && (int)Math.Round(temporaryZ) >= z)
                        {
                            map[x, y, z] = 1;
                        }*/

                        /*if (currentPosition.y < 1)
                        {
                            map[x, 0, z] = 1;
                        }*/

                        //_array[x + width * (y + height * z)] = 1;

                        /*if (y ==0)
                        {
                            _chunkArray[x + width * (y + height * z)] = 1;
                        }
                        else
                        {
                            if ((int)Math.Round(temporaryY) >= y)
                            {
                                _chunkArray[x + width * (y + height * z)] = 1;
                            }
                        }*/
                        //_chunkArray[x + width * (y + height * z)] = 1;
                    }
                }
            }
        }

        //resultsOne = Array.TrueForAll(_perlinChunkArray, s => s == 0);
        //resultsTwo = Array.TrueForAll(_perlinChunkArray, s => s == 1);
        //resultsThree = Array.TrueForAll(_perlinChunkArray, s => s == 2);

        /*for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    _block = _chunkArray[x + width * (y + height * z)];
                    if (_block == 0) continue;
                    {
                        buildVertex(x, y, z);
                        //buildVertexArray(x, y, z);
                    }
                }
            }
        }*/






        /*for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    _maxWidth = width;
                    _maxDepth = depth;

                    //_lastRowX = 0;
                    //_lastRowZ = 0;

                    foundVertOne = false;
                    foundVertTwo = false;
                    foundVertThree = false;
                    foundVertFour = false;

                    _block = _tempChunkArray[x + width * (y + height * z)];

                    if (_block == 0 || _block == 2) continue; //|| _block == 2
                    {
                        //buildTopFace(x, y, z, chunkPos);
                        //buildTopLeft(x, y, z, chunkPos);
                        //yield return new WaitForSeconds(_iterateSpeed);
                        ////////Instantiate(_sphereVisual, new Vector3(_x + 0.5f, y, _z + 0.5f), Quaternion.identity);                       
                    }
                }
            }
        }*/

        /*_testChunk = new GameObject();
        _testChunk.transform.position = chunkPos;
        mesh = new Mesh();
        _testChunk.AddComponent<MeshFilter>();
        _testChunk.AddComponent<MeshRenderer>();

        //StartCoroutine(_buildFaces(chunkPos));
        _buildFaces(chunkPos);

        //_stopWatch.Stop();
        //_milli = _stopWatch.Elapsed.Milliseconds;
        //UnityEngine.Debug.Log(_milli);

        //_testChunk.AddComponent<MeshRenderer>();

        mesh.vertices = vertexlist.ToArray();
        mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = mesh;
        _testChunk.GetComponent<MeshRenderer>().material = _mat;
        //meshRend.material = _mat;

        _chunkData._chunkBlockArray = _chunkArray;
        _chunkData._chunkVerticesArray = _chunkVertexArray;

        _chunkData._chunkVertices = vertexlist;
        _chunkData._chunkTriangles = triangles;

        _chunkData.chunkPosition = chunkPos;

        _chunkData._trueForAll = false;

        _chunkData.resultsOne = resultsOne;
        _chunkData.resultsTwo = resultsTwo;
        _chunkData.resultsThree = resultsThree;*/

        return _chunkData;
    }

    public int[] map;
    public int width = 10;
    public int height = 10;
    public int depth = 10;

    //public int ChunkWidth_L = 6;
    //public int ChunkWidth_R = 5;
    //public int ChunkHeight_L = 6;
    //public int ChunkHeight_R = 5;
    //public int ChunkDepth_L = 6;
    //public int ChunkDepth_R = 5;

    public Vector3 realIndexedPosition = Vector3.zero;

    //public Mesh mesh;
    //public List<Vector3> verts = new List<Vector3>();
    //public List<int> tris = new List<int>();
    //public List<Vector2> uv = new List<Vector2>();
    public static float planeSize = 0.1f;

    float seed;
    int block;

    float nodeDiameter;
    float chunkRadius;
    float fraction;
    float chunkSize;

    int divider = 10;
    float noiseValue0;

    public float detailScale = 5;
    public float heightScale = 5;
    public int heightScale1 = 5;
    public int detailScale1 = 5;

    sccsproceduralplanetbuilderGen2 componentParent;
    Transform parentObject;
    Vector3 position;

    NewObjectPoolerScript UnityTutorialGameObjectPool; //this.transform.GetComponent<NewObjectPoolerScript>();



    public void sccsCustomStart(sccsproceduralplanetbuilderGen2 sccsproceduralplanetbuilderGen2_)
    {

        _shader = _mat.shader;
        vertexlistWidth = width + 1;
        vertexlistHeight = height + 1;
        vertexlistDepth = depth + 1;



        //Vector3 chunkPos = this.gameObject.transform.position;
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



        //_vertices = new Vector3[1];

        //meshFilters = new MeshFilter[_combineMax];
        //_testChunk = this.gameObject;

        //StartCoroutine(buildTerrain(chunkPos));
        chunkPos = this.transform.position;
        //buildTerrain(chunkPos);


        this.gameObject.tag = "collisionObject";
        this.gameObject.layer = 8; //"collisionObject"
        UnityTutorialGameObjectPool = this.transform.GetComponent<NewObjectPoolerScript>();

        parentObject = this.transform.parent;
        //componentParent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilderGen2>().currentplanetbuilder;

        componentParent = sccsproceduralplanetbuilderGen2_;// sccsproceduralplanetbuilderGen2.sccsproceduralplanetbuilderGen2staticscriptlock;

        mesh = new Mesh();
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
        this.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;

        nodeDiameter = planeSize;
        chunkRadius = planeSize / 2;
        fraction = (int)(1 / (planeSize));
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
        _perlinChunkArray = new int[width * height * depth];


        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    /*temporaryY = 10f;
                    //float temporaryZ = 10f;
                    //float temporaryX = 10f;

                    temporaryY *= Mathf.PerlinNoise((x * _planeSize + chunkPos.x + _seed) / _detailScale, (z * _planeSize + chunkPos.z + _seed) / _detailScale) * heightScale;
                    //temporaryX *= Mathf.PerlinNoise((y * _planeSize + chunkPos.y + _seed) / _detailScale, (z * _planeSize + chunkPos.z + _seed) / _detailScale) * heightScale;
                    //temporaryZ *= Mathf.PerlinNoise((x * _planeSize + chunkPos.x + _seed) / _detailScale, (y * _planeSize + chunkPos.y + _seed) / _detailScale) * heightScale;

                    size0 = (1 / _planeSize) * chunkPos.y;
                    temporaryY -= size0;
                    */
                    //_chunkArray[x + width * (y + height * z)] = 1;

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
        }
    }

    public void Regenerate()
    {



        t = 0;

        xx = 0;
        yy = 0;
        zz = 0;

        swtchx = 0;
        swtchy = 0;
        swtchz = 0;
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


        Vector3 chunkPos = this.transform.position;

        vertexlist.Clear();
        triangles.Clear();

        /*for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    //block = map[x + width * (y + depth * z)];

                    int indexOf = x + width * (y + depth * z);

                    block = map[indexOf];

                    if (block == 0) continue;
                    {
                        buildTopFace(x, y, z, chunkPos);
                        buildBottomFace(x, y, z, chunkPos);
                    }
                }
            }
        }

        for (int x = width - 1; x >= 0; x--)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {

                    int indexOf = x + width * (y + depth * z);

                    block = map[indexOf];

                    if (block == 0) continue;
                    {
                        buildTopRight(x, y, z, chunkPos);
                        buildTopLeft(x, y, z, chunkPos);
                    }
                }
            }
        }




        //for (int z = 0; z < depth; z++)
        for (int z = depth - 1; z >= 0; z--)
        {
            //for (int y = height - 1; y >= 0; y--)
            for (int y = 0; y < height; y++)
            {
                //for (int x = width-1; x >= 0; x--)
                for (int x = 0; x < width; x++)
                {
                    int indexOf = x + width * (y + depth * z);

                    block = map[indexOf];

                    if (block == 0) continue;
                    {
                        buildFrontFace(x, y, z, chunkPos);
                        buildBackFace(x, y, z, chunkPos);
                    }
                }
            }
        }*/

    }




    int index = 0;
    Vector3 planetchunkpos = Vector3.zero;

    IEnumerator CreateChunkFaces()
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

                    /*if (_block == 0)
                    {

                    }*/

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

                        for (int i = 0; i < totalBytes; i++)
                        {
                            if (swtchzBytes == 0)
                            {
                                if (IsTransparent(xi + 1, yi, zi))
                                {
                                    CalculateBytesForTopFace();
                                }
                            }
                            else
                            {
                                i = totalBytes;
                                //break;
                            }
                        }

                        if (foundVertOne && foundVertTwo)
                        {
                            //xxBytes = _maxWidth;
                            //yyBytes = _maxHeight;
                            //zzBytes = _maxDepth;
                            //zz = _maxDepth;
                            //yy = _maxHeight;
                            //foundVertTwo = false;
                            //foundVertOne = false;
                        }


                        //buildTopRight(xi, yi, zi, planetchunkpos, out iterswapx, out iterswapy, out iterswapz);
                        //StartCoroutine("CreateChunkBytes");
                        //CreateChunkBytes
                        //xx = iterswapx;
                        //yy = iterswapy;
                        //zz = iterswapz;
                    }
                    else if (_block == 0)
                    {
                        //Instantiate(_sphereVisualOtherColorCube, new Vector3(xi + 1, yi, zi) * planeSize + _chunkPos, Quaternion.identity);
                    }

                    /*if (blockers[index] != null)
                    {
                        if (blockers[index].planetchunk != null)
                        {
                            if (blockers[index].somesccsplanetchunkGen2 != null)
                            {
                                blockers[index].somesccsplanetchunkGen2.sccsSetMap();
                                blockers[index].somesccsplanetchunkGen2.Regenerate();

                                if (blockers[index].somesccsplanetchunkGen2.vertexlist.Count > 0)
                                {
                                    blockers[index].somesccsplanetchunkGen2.buildMesh();
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("null");
                    }*/
                }
                else
                {
                    //t = total;
                }
                //Debug.Log("yy0:" + yy);
                //Debug.Log("xx:" + xx);

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

    void CalculateBytesForTopFace()
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
                _block = _tempChunkArray[xi + width * (yi + height * zi)];



                //buildTopFace(x, y, z, chunkPos);










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

    /*
    public void Regenerate()
    {
        Vector3 chunkPos = this.transform.position;

        for (int y = height - 1; y >= 0; y--)
        {
            for (int _x = 0; _x < width; _x++)
            {
                for (int _z = 0; _z < depth; _z++)
                {
                    _block = _chunkArray[_x + width * (y + height * _z)];
                    if (_block == 0) continue; //|| _block == 2
                    {

                        //StartCoroutine(buildFaces(_x, y, _z));                  
                        buildTopFace(_x, y, _z, chunkPos);
                        buildTopRight(_x, y, _z, chunkPos);
                        buildTopLeft(_x, y, _z, chunkPos);
                        buildFrontFace(_x, y, _z, chunkPos);
                        buildBackFace(_x, y, _z, chunkPos);

                        //yield return new WaitForSeconds(_iterateSpeed);
                        //////////////Instantiate(_sphereVisual, new Vector3(_x + 0.5f, y, _z + 0.5f), Quaternion.identity);                      
                    }

                    /*_block = _tempChunkArrayRightFace[_x + width * (y + height * _z)];
                    if (_block == 0 || _block == 2) continue; //|| _block == 2
                    {
                        buildTopRight(_x, y, _z, chunkPos);
                    }
                }
            }
        }
    }*/

    public void buildMesh()
    {

        this.gameObject.GetComponent<MeshFilter>().mesh.Clear();
        this.gameObject.GetComponent<MeshFilter>().mesh.vertices = vertexlist.ToArray();
        this.gameObject.GetComponent<MeshFilter>().mesh.triangles = triangles.ToArray();
        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = mesh;
        this.gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        this.gameObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();

        //to readd
        //to readd
        //to readd
        /*if (this.gameObject.GetComponent<MeshCollider>() == null)
        {
            this.gameObject.AddComponent<MeshCollider>();
            //this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        }
        else
        {
            Destroy(this.gameObject.GetComponent<MeshCollider>());
            this.gameObject.AddComponent<MeshCollider>();
            //this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        }*/
        //to readd
        //to readd
        //to readd
        /*
        _index0 = 0;
        _index1 = 0;
        _index2 = 0;
        _index3 = 0;
        _totalVertex = 0;
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
        foundVertFour = false;*/
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


    //UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWidth: " + _maxWidth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
    void buildTopFace(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArray[_x + width * (_y + height * _z)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x, _y + 1, _z))
            {
                for (int _xx = 0; _xx < _maxWidth; _xx++)
                {
                    rowIterateX = _x + _xx;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = _z + _zz;

                        if (rowIterateX <= width && rowIterateZ <= depth)
                        {
                            if (_xx == 0 && _zz == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = _y + 1;
                                oneVertIndexZ = rowIterateZ;
                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArray[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = _y + 1;
                                    threeVertIndexZ = rowIterateZ;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArray[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = _y + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArray[(rowIterateX) + width * ((_y + 1) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = _y + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = _y + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = _y + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = _y + 1;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArray[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = _y + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArray[(rowIterateX) + width * ((_y + 1) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = _y + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = _y + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            twoVertIndexY = _y + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = _y + 1;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArray[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = _y + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_xx > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArray[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = _y + 1;
                                    threeVertIndexZ = rowIterateZ - _zz;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArray[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArray[(rowIterateX) + width * ((_y + 1) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArray[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;

                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = _y + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, _y, rowIterateZ))
                        {
                            _tempChunkArray[(rowIterateX) + width * (_y + height * (rowIterateZ))] = 2;
                            ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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





    void buildTopLeft(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayLeftFace[_x + width * (_y + height * _z)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x - 1, _y, _z))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = _y + _yy;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = _z + _zz;

                        if (rowIterateY <= height && rowIterateZ <= depth)
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = _x;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = rowIterateZ;
                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(_x, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = _x;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = _x;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = _x;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = _x;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = _x;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = _x;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = _x;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = _x;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(_x, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = _x;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(_x - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = _x;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexX = _x;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = _x;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = _x;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ - _zz;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = _x;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = _x;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = _x;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = _x;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(_x - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = _x;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexX = _x;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(_x, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayLeftFace[(_x) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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

    void buildTopRight(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayRightFace[_x + width * (_y + height * _z)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x + 1, _y, _z))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = _y + _yy;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = _z + _zz;

                        if (rowIterateY <= height && rowIterateZ <= depth)
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = _x + 1;
                                oneVertIndexY = rowIterateY ;
                                oneVertIndexZ = rowIterateZ;
                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(_x, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x+1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = _x + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = _x + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = _x + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = _x + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = _x + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = _x + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = _x + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = _x + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(_x, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = _x + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(_x + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = _x + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexX = _x + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = _x + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = _x + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ - _zz;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = _x + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = _x + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(_x ) + width * ((rowIterateY +1 ) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = _x + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = _x + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(_x + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = _x + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexX = _x + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = _x + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(_x, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayRightFace[(_x) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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




    void buildFrontFace(int _x, int _y, int _z, Vector3 chunkPos)
    {

        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayFrontFace[_x + width * (_y + height * _z)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x, _y, _z + 1))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = _y + _yy;
                    for (int _xx = 0; _xx < _maxWidth; _xx++)
                    {
                        rowIterateX = _x + _xx;

                        if (rowIterateY <= height && rowIterateX <= width)
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = _z + 1;
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = _z + 1;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX ;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z ))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = _z + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = _z + 1;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = _z + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = _z + 1;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = _z + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z + 1;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX+1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX+1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = _z + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = _z + 1;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = _z + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexZ = _z + 1;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = _z + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z + 1;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX - _xx ;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = _z + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = _z + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        fourVertIndexZ = _z + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = _z + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z + 1 ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = _z + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX - _xx;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = _z + 1;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z ))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = _z + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = _z + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        fourVertIndexZ = _z + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = _z + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = _z + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        fourVertIndexZ = _z + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, _z))
                        {
                            _tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (_z))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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


    void buildBackFace(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayBackFace[_x + width * (_y + height * _z)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x, _y, _z - 1))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = _y + _yy;
                    for (int _xx = 0; _xx < _maxWidth; _xx++)
                    {
                        rowIterateX = _x + _xx;

                        if (rowIterateY <= height && rowIterateX <= width)
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = _z;
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z - 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = _z;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = _z;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = _z;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = _z;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = _z;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = _z;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = _z;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = _z;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = _z;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexZ = _z;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = _z;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = _z;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = _z;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        fourVertIndexZ = _z;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = _z;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = _z;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX - _xx;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = _z;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = _z;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = _z;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        fourVertIndexZ = _z;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = _z;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, _z - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = _z;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        fourVertIndexZ = _z;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = _z;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, _z))
                        {
                            _tempChunkArrayBackFace[(rowIterateX) + width * (rowIterateY + height * (_z))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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




    void buildBottomFace(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayBottomFace[_x + width * (_y + height * _z)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x, _y - 1, _z))
            {
                for (int _xx = 0; _xx < _maxWidth; _xx++)
                {
                    rowIterateX = _x + _xx;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = _z + _zz;

                        if (rowIterateX <= width && rowIterateZ <= depth)
                        {
                            if (_xx == 0 && _zz == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = _y;
                                oneVertIndexZ = rowIterateZ;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, _y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, _y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, _y - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((_y - 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, _y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = _y;
                                    threeVertIndexZ = rowIterateZ;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, _y + 1, rowIterateZ) * _planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = _y;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, _y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, _y - 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((_y - 1) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = _y;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, _y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = _y;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = _y;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, _y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = _y;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, _y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = _y;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, _y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, _y - 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((_y - 1) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = _y;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, _y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = _y;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
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
                                            twoVertIndexY = _y;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, _y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = _y;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, _y + 1, rowIterateZ + 1) * _planeSize + chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, _y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, _y - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((_y - 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = _y;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexY = _y;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_xx > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, _y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, _y - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((_y - 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, _y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = _y;
                                    threeVertIndexZ = rowIterateZ - _zz;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, _y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, _y - 1, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((_y - 1) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexY = _y;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, _y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, _y - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((_y - 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;

                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, rowIterateZ - _zz) * _planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = _y;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexY = _y;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, _y + 1, twoVertIndexZ) * _planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, _y, rowIterateZ))
                        {
                            _tempChunkArrayBottomFace[(rowIterateX) + width * (_y + height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = 1;
                _testVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x + 1, _y + 1, _z) == 0)
            {
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                _testVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = _newVertzCounter;
                //_vertices[vertexlistCounter] = new Vector3(_x + 1, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + vertexlistWidth * ((_y + 1) + vertexlistHeight * _z)] = 1;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x, _y + 1, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                _testVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = _newVertzCounter;
                //_vertices[vertexlistCounter] = new Vector3(_x, _y + 1, _z + 1);
                _chunkVertexArray[_x + vertexlistWidth * ((_y + 1) + vertexlistHeight * (_z + 1))] = 1;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 0)
            {
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize));

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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize));

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
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));

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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));

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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
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
                vertexlist.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
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

