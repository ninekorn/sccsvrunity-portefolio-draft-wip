using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimplexNoise;
using System.Linq;
using System;

[RequireComponent(typeof(MeshFilter))]
//[RequireComponent(typeof(MeshCollider))]


public class OldFloorTiles : MonoBehaviour
{
    public GameObject _sphereVisualOtherColorOrange;
    List<Vector3> currentVerts = new List<Vector3>();
    List<Vector3> middleTerrainHeight = new List<Vector3>();

    public static List<OldFloorTiles> chunks = new List<OldFloorTiles>();
    public static Dictionary<OldFloorTiles, Vector3> chunkz = new Dictionary<OldFloorTiles, Vector3>();

    public int width = 10;
    public int height = 25;
    public int depth = 10;

    public static int chunkWidth = 10;

    public float noiseDivider;

    //public byte[,,] map;

    public int[,,] leftExtremity;
    public int[,,] rightExtremity;
    public int[,,] frontExtremity;
    public int[,,] backExtremity;

    public int[,,] leftInsideCornerExtremity;
    public int[,,] rightInsideCornerExtremity;
    public int[,,] frontInsideCornerExtremity;
    public int[,,] backInsideCornerExtremity;

    public int[,,] leftOutsideCornerExtremity;
    public int[,,] rightOutsideCornerExtremity;
    public int[,,] frontOutsideCornerExtremity;
    public int[,,] backOutsideCornerExtremity;


    Mesh mesh;
    List<Vector3> verts = new List<Vector3>();

    List<int> tris = new List<int>();
    List<Vector2> uv = new List<Vector2>();

    List<int> triangles = new List<int>();
    List<Vector2> uvz = new List<Vector2>();

    MeshCollider meshCollider;

    //public string seed;
    public int seed = 3420;
    float seed0;

    public Dictionary<List<Vector3>, List<int>> TopFaceVerts = new Dictionary<List<Vector3>, List<int>>();

    float updateTime;

    public bool useRandomSeed;

    //[Range(0, 100)]
    //public int randomFillPercent;

    //int[,] map;

    int neighbourWallTiles;
    public float planeSize = 1;

    //public Texture rocks;
    //public Material weed;


    float noiseValue;
    int widthWorld;

    int counter = 0;



    List<Color> couleur = new List<Color>();
    List<Vector3> topFaceVertices = new List<Vector3>();
    List<Vector3> leftFaceVertices = new List<Vector3>();
    List<Vector3> rightFaceVertices = new List<Vector3>();
    List<Vector3> frontFaceVertices = new List<Vector3>();
    List<Vector3> backFaceVertices = new List<Vector3>();

    Vector3 middleTerrainVerts;
    //public Transform sphere;

    byte brick;

    int counter999 = 0;
    int counter777 = 0;

    int counting999 = 0;
    int counting777 = 0;

    public float tileHeight = 1;

    public static float xChunkPos;
    public static float yChunkPos;
    public static float zChunkPos;

    public static List<Vector3> wallPos = new List<Vector3>();

    public static Vector3 cpos;


    //List<Vector3> adjacentWalls = new List<Vector3>();
    //List<Vector3> totalTiles = new List<Vector3>();

    Dictionary<Vector3, Vector3> totalTiles = new Dictionary<Vector3, Vector3>();
    Dictionary<Vector3, Vector3> adjacentWalls = new Dictionary<Vector3, Vector3>();

    List<Vector3> leftFrontCornerInside = new List<Vector3>();
    List<Vector3> rightFrontCornerInside = new List<Vector3>();
    List<Vector3> leftBackCornerInside = new List<Vector3>();
    List<Vector3> rightBackCornerInside = new List<Vector3>();

    List<Vector3> leftWall = new List<Vector3>();
    List<Vector3> rightWall = new List<Vector3>();
    List<Vector3> frontWall = new List<Vector3>();
    List<Vector3> backWall = new List<Vector3>();

    List<Vector3> leftFrontCornerOutside = new List<Vector3>();
    List<Vector3> rightFrontCornerOutside = new List<Vector3>();
    List<Vector3> leftBackCornerOutside = new List<Vector3>();
    List<Vector3> rightBackCornerOutside = new List<Vector3>();


    List<Vector3> threeWayWallLeft = new List<Vector3>();
    List<Vector3> threeWayWallRight = new List<Vector3>();
    List<Vector3> threeWayWallFront = new List<Vector3>();
    List<Vector3> threeWayWallBack = new List<Vector3>();




    List<Vector3> toRemove = new List<Vector3>();

    public float blockSize;
    //public GameObject sphere;

    public float floorHeight = 20;

    int vertexlistWidth = 0;
    int vertexlistHeight = 0;
    int vertexlistDepth = 0;




    int[] _perlinChunkArray;


    int[] _chunkArray;


    int[] _tempChunkArrayRightFace;
    int[] _tempChunkArrayRightFaceXAxISPLUS;
    int[] _tempChunkArrayRightFaceXAxISMINUS;

    int[] _tempChunkArrayLeftFace;
    int[] _tempChunkArrayLeftFaceXAxISPLUS;
    int[] _tempChunkArrayLeftFaceXAxISMINUS;







    int[] _tempChunkArrayTopFace;
    int[] _tempChunkArrayTopFaceYAxISPLUS;
    int[] _tempChunkArrayTopFaceYAxISMINUS;

    int[] _tempChunkArrayBottomFace;
    int[] _tempChunkArrayBottomFaceYAxISPLUS;
    int[] _tempChunkArrayBottomFaceYAxISMINUS;









    int[] _tempChunkArrayFrontFace;
    int[] _tempChunkArrayFrontFaceZAxISPLUS;
    int[] _tempChunkArrayFrontFaceZAxISMINUS;

    int[] _tempChunkArrayBackFace;
    int[] _tempChunkArrayBackFaceZAxISPLUS;
    int[] _tempChunkArrayBackFaceZAxISMINUS;


    int[] _chunkVertexArray;
    int[] _testVertexArray;

    Vector3 _chunkPos;
    int _newVertzCounter = 0;

    List<Vector3> vertexlist = new List<Vector3>();
    List<Vector3> _normals = new List<Vector3>();
    List<Vector2> _uvs = new List<Vector2>();

    int _index0 = 0;
    int _index1 = 0;
    int _index2 = 0;
    int _index3 = 0;


    int typeOfTile = 0;



    void Start()
    {
        //leftWall = LevelGenerator4.currentLevelGen.leftWall;

        //transform.localScale = transform.localScale * blockSize;
        transform.tag = "chunks";
        //chunks.Add(this);
        chunkz.Add(this, transform.position);
        chunks.Add(this);

        xChunkPos = transform.position.x;
        yChunkPos = transform.position.y;
        zChunkPos = transform.position.z;

        //totalTiles = LevelGenerator4.currentLevelGen.tilesCreated;
        //adjacentWalls = LevelGenerator4.currentLevelGen.adjacentWall;


        vertexlistWidth = width + 1;
        vertexlistHeight = height + 1;
        vertexlistDepth = depth + 1;
        _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

        _chunkPos = this.transform.position;



        seed0 = 3420;
        _index0 = 0;
        _index1 = 0;
        _index2 = 0;
        _index3 = 0;
        //_normals = new List<Vector3>();
        //_uvs = new List<Vector2>();

        _newVertzCounter = 0;





        vertexlist = new List<Vector3>();
        triangles = new List<int>();




        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshCollider = GetComponent<MeshCollider>();
        GenerateMap();
        //Regenerate();

    }

    void GenerateMap()
    {
        //map = new int[width, height, width];

        leftExtremity = new int[width, height, width];
        rightExtremity = new int[width, height, width];
        frontExtremity = new int[width, height, width];
        backExtremity = new int[width, height, width];

        leftInsideCornerExtremity = new int[width, height, width];
        rightInsideCornerExtremity = new int[width, height, width];
        frontInsideCornerExtremity = new int[width, height, width];
        backInsideCornerExtremity = new int[width, height, width];

        leftOutsideCornerExtremity = new int[width, height, width];
        rightOutsideCornerExtremity = new int[width, height, width];
        frontOutsideCornerExtremity = new int[width, height, width];
        backOutsideCornerExtremity = new int[width, height, width];

        RandomFillMap();
    }


    void RandomFillMap()
    {
        _tempChunkArrayBottomFace = new int[width * height * depth];
        _tempChunkArrayBottomFaceYAxISPLUS = new int[width * height * depth];

        _tempChunkArrayBackFace = new int[width * height * depth];
        _tempChunkArrayBackFaceZAxISPLUS = new int[width * height * depth];

        _tempChunkArrayFrontFace = new int[width * height * depth];
        _tempChunkArrayFrontFaceZAxISPLUS = new int[width * height * depth];

        _tempChunkArrayLeftFace = new int[width * height * depth];
        _tempChunkArrayLeftFaceXAxISPLUS = new int[width * height * depth];

        _tempChunkArrayRightFace = new int[width * height * depth];
        _tempChunkArrayRightFaceXAxISPLUS = new int[width * height * depth];

        _tempChunkArrayTopFace = new int[width * height * depth];
        _tempChunkArrayTopFaceYAxISPLUS = new int[width * height * depth];

        _chunkArray = new int[width * height * depth];
        _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
        _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

        /*if (useRandomSeed)
        {
            seed = DateTime.Now.Ticks.ToString();
        }*/
        /*
        /////////////////////////////////////////////////FLOOR TILES//////////////////////////////////////////////
        for (int x = 0; x < width; x++)
        {
            float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
            for (int y = 0; y < height; y++)
            {
                float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                for (int z = 0; z < width; z++)
                {
                    float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);

                    float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                    noiseValue += (10 - (float)y) / 10;
                    noiseValue /= (float)y / 5;

                    if (noiseValue > 0.2f && y < floorHeight)
                    {
                        _chunkArray[x + width * (y + height * z)] = 1;
                        _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                        _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                        _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                        _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                        _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                        _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;

                    }
                    else
                    {
                        _chunkArray[x + width * (y + height * z)] = 0;
                        _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                        _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                        _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                        _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                        _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                        _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                    }
                }
            }
        }*/
        float detailScale = 10;
        float heightScale = 10;

        var center = Vector3.zero;

        var radiusplanetmountainend = width; //+ offsetDist

        for (int x = 0; x < width; x++)
        {
            float noiseX = Mathf.Abs(((float)(x * planeSize + transform.position.x + seed) / detailScale) * heightScale);

            for (int y = 0; y < height; y++)
            {
                float noiseY = Mathf.Abs(((float)(y * planeSize + transform.position.y + seed) / detailScale) * heightScale);

                for (int z = 0; z < depth; z++)
                {
                    float noiseZ = Mathf.Abs(((float)(z * planeSize + transform.position.z + seed) / detailScale) * heightScale);

                    float posX = x * planeSize + transform.position.x;
                    float posY = y * planeSize + transform.position.y;
                    float posZ = z * planeSize + transform.position.z;

                    Vector3 pos = new Vector3(posX, posY, posZ);

                    float distance = Vector3.Distance(pos, center);


                    if (distance < radiusplanetmountainend)
                    {
                        _chunkArray[x + width * (y + height * z)] = 1;
                        _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                        _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                        _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                        _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                        _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                        _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                    }
                    else
                    {
                        _chunkArray[x + width * (y + height * z)] = 0;
                        _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                        _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                        _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                        _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                        _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                        _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                    }
                }
            }
        }















                    Regenerate();
        /*

        for (int j = 0; j < LevelGenerator4.currentLevelGen.createdTilesList.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.createdTilesList[j])
            {
                /////////////////////////////////////////////////FLOOR TILES//////////////////////////////////////////////
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                                _chunkArray[x + width * (y + height * z)] = 1;
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;

                            }
                            else
                            {
                                _chunkArray[x + width * (y + height * z)] = 0;
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }
                        }
                    }
                }
                typeOfTile = 0;
            }
        }*/


        /*

        for (int j = 0; j < LevelGenerator4.currentLevelGen.adjacentWall.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.adjacentWall[j])
            {
                /////////////////////////////////////////////////FLOOR TILES//////////////////////////////////////////////
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;

                            }
                            else
                            {
                                _chunkArray[x + width * (y + height * z)] = 0;
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }
                        }
                    }
                }
                typeOfTile = 0;
            }
        }*/



        /*

        for (int j = 0; j < LevelGenerator4.currentLevelGen.createdTilesListRoof.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.createdTilesListRoof[j])
            {
                /////////////////////////////////////////////////FLOOR TILES//////////////////////////////////////////////
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                            noiseValue += (((10 + 100)) - (float)y) / (10 + 100);
                            noiseValue /= (float)y / (5);

                            if (noiseValue < 0.2f && y < height)
                            {
                                _chunkArray[x + width * (y + height * z)] = 1;
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                _chunkArray[x + width * (y + height * z)] = 0;
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }
                        }
                    }
                }
                typeOfTile = 0;
            }
        }





        for (int j = 0; j < LevelGenerator4.currentLevelGen.adjacentWallRoof.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.adjacentWallRoof[j])
            {
                /////////////////////////////////////////////////FLOOR TILES//////////////////////////////////////////////
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                            noiseValue += ((10 + 100) - (float)y) / (10 + 100);
                            noiseValue /= (float)y / 5;

                            if (noiseValue < 0.2f && y < height)
                            {
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;

                            }
                            else
                            {
                                _chunkArray[x + width * (y + height * z)] = 0;
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }
                        }
                    }
                }
                typeOfTile = 0;
            }
        }*/







        /*
        /////////////////////////////////////RIGHT WALL RIGHT FACE/////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.rightWall.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.rightWall[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    float noiseX3 = Mathf.Abs((float)(x * planeSize + (transform.position.x ) + seed0) / 100);

                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        float noiseY3 = Mathf.Abs((float)(y * planeSize + (transform.position.y) + seed0) / 100);

                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);
                            float noiseZ3 = Mathf.Abs((float)(z * planeSize + (transform.position.z) + seed0) / 100);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);          

                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }

                            float noiseValue3 = Noise.Generate(noiseX3, noiseY3, noiseZ3);
                            noiseValue3 += (10 - (float)y) / 10;
                            noiseValue3 /= (float)y / 5;

                            float noiseValue3i = noiseValue2;

                            noiseValue3i += (5 - (float)x) / 5;
                            noiseValue3i /= (float)x / 5;

                            if (noiseValue3i < 0.2f && y < height)
                            {

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;


                            }

                        }
                    }
                }
                typeOfTile = 1;
            }
        }
        /*
        /////////////////////////////////////RIGHT WALL RIGHT FACE/////////////////////////////////
        for (int j = 0; j < LevelGenerator4.currentLevelGen.rightWallROOF.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.rightWallROOF[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / (100));
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    float noiseX3 = Mathf.Abs((float)(x * planeSize + (transform.position.x) + seed0) / 100);

                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        float noiseY3 = Mathf.Abs((float)(y * planeSize + (transform.position.y) + seed0) / 100);

                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);
                            float noiseZ3 = Mathf.Abs((float)(z * planeSize + (transform.position.z) + seed0) / 100);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                            noiseValue += (((10 + 100)) - (float)y) / (10 + 100);
                            noiseValue /= (float)y / 5;

                            if (noiseValue < 0.2f && y < (height))// if (noiseValue > 0.2f && y > (height / 2))
                            {
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }

                            float noiseValue3 = Noise.Generate(noiseX3, noiseY3, noiseZ3);
                            noiseValue3 += (10 - (float)y) / 10;
                            noiseValue3 /= (float)y / 5;

                            float noiseValue3i = noiseValue2;

                            noiseValue3i += (5 - (float)x) / 5;
                            noiseValue3i /= (float)x / 5;

                            if (noiseValue3i < 0.2f && y < height)
                            {
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;

                            }
                        }
                    }
                }
                typeOfTile = 1;
            }
        }*/






        /*
        
        ///////////////////////////////////// LEFT WALL RIGHT FACE /////////////////////////////
        for (int j = 0; j < LevelGenerator4.currentLevelGen.leftWall.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.leftWall[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                              
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;


                            }
                            else
                            {
                    
                                _chunkArray[x + width * (y + height * z)] = 0;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }

                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                            float noiseValue1i = noiseValue2;

                            noiseValue1i += (5 - (float)x) / 5;
                            noiseValue1i /= (float)x / 5;

                            if (noiseValue1i > 0.2f)
                            {
                                

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }
                        }
                    }
                }
                typeOfTile = 1;
            }
        }
        

        













        
        /////////////////////////////////////FRONT WALL/////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.frontWall.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.frontWall[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX5 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY5 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ5 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                            float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                               
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                _chunkArray[x + width * (y + height * z)] = 0;
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }

                            float noiseValue6i = noiseValue5;

                            noiseValue6i += (5 - (float)z) / 5;
                            noiseValue6i /= (float)z / 5;

                            if (noiseValue6i > 0.2f) // && y < floorHeight
                            {
                               
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                                //frontExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                            
                        }
                    }
                }
                typeOfTile = 1;
            }
        }


        
        /////////////////////////////////////BACK WALL////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.backWall.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.backWall[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX5 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY5 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ5 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                            float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                               

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;

                                //frontExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                            else
                            {
                               

                                _chunkArray[x + width * (y + height * z)] = 0;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }

                            float noiseValue4i = noiseValue5;

                            noiseValue4i += (5 - (float)z) / 5;
                            noiseValue4i /= (float)z / 5;

                            if (noiseValue4i < 0.2f)
                            {
                               

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                                //frontExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                        }
                    }
                }
                typeOfTile = 1;
            }
        }


















        
        /////////////////////////////////////LEFT FRONT INSIDE CORNER////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.builtLeftFrontInsideCorner.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.builtLeftFrontInsideCorner[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    float noiseX5 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        float noiseY5 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);
                            float noiseZ5 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                            float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                               

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                               

                                _chunkArray[x + width * (y + height * z)] = 0;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }
                            float noiseValue2i = noiseValue2;
                            noiseValue2i += (5 - (float)x) / 5;
                            noiseValue2i /= (float)x / 5;

                            float noiseValue5i = noiseValue5;

                            noiseValue5i += (5 - (float)z) / 5;
                            noiseValue5i /= (float)z / 5;


                            if (noiseValue2i > 0.2f || noiseValue5i < 0.2f)
                            {
                                _chunkArray[x + width * (y + height * z)] = 1;                          
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;


                                //leftInsideCornerExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                        }
                    }
                }
                typeOfTile = 1;
            }
        }






        /////////////////////////////////////RIGHT FRONT INSIDE CORNER////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.builtRightFrontInsideCorner.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.builtRightFrontInsideCorner[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    float noiseX5 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        float noiseY5 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);
                            float noiseZ5 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                            float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                                

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                               
                                _chunkArray[x + width * (y + height * z)] = 0;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }
                            float noiseValue7i = noiseValue2;
                            noiseValue7i += (5 - (float)x) / 5;
                            noiseValue7i /= (float)x / 5;

                            float noiseValue8i = noiseValue5;
                            noiseValue8i += (5 - (float)z) / 5;
                            noiseValue8i /= (float)z / 5;

                            if (noiseValue7i < 0.2f || noiseValue8i < 0.2f)
                            {
                                
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;

                                //rightInsideCornerExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                        }
                    }
                }
                typeOfTile = 1;
            }
        }




        /////////////////////////////////////LEFT BACK INSIDE CORNER////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.builtLeftBackInsideCorner.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.builtLeftBackInsideCorner[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    float noiseX5 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        float noiseY5 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);
                            float noiseZ5 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                            float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                              

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                               
                                _chunkArray[x + width * (y + height * z)] = 0;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }
                            float noiseValue9i = noiseValue2;

                            noiseValue9i += (5 - (float)x) / 5;
                            noiseValue9i /= (float)x / 5;

                            float noiseValue10i = noiseValue5;
                            noiseValue10i += (5 - (float)z) / 5;
                            noiseValue10i /= (float)z / 5;



                            if (noiseValue9i > 0.2f || noiseValue10i > 0.2f)
                            {
                               
        _chunkArray[x + width * (y + height * z)] = 0;
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;


                                //backInsideCornerExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                        }
                    }
                }
                typeOfTile = 1;
            }
        }





        /////////////////////////////////////RIGHT BACK INSIDE CORNER////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.builtRightBackInsideCorner.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.builtRightBackInsideCorner[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    float noiseX5 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        float noiseY5 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);
                            float noiseZ5 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                            float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                                                               _chunkArray[x + width * (y + height * z)] = 1;
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                              
                                _chunkArray[x + width * (y + height * z)] = 0;
                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }

                            float noiseValue11i = noiseValue5;
                            noiseValue11i += (5 - (float)z) / 5;
                            noiseValue11i /= (float)z / 5;

                            float noiseValue12i = noiseValue2;

                            noiseValue12i += (5 - (float)x) / 5;
                            noiseValue12i /= (float)x / 5;


                            if (noiseValue11i > 0.2f || noiseValue12i < 0.2f)
                            {
                              
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;


                                //rontInsideCornerExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                        }
                    }
                }
                typeOfTile = 1;
            }
        }


        
        /////////////////////////////////////LEFT FRONT OUTSIDE CORNER////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.builtLeftFrontOutsideCorner.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.builtLeftFrontOutsideCorner[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    float noiseX5 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        float noiseY5 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);
                            float noiseZ5 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                            float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                                                               _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;

                            }
                            else
                            {
                              

                                _chunkArray[x + width * (y + height * z)] = 0;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }

                            float noiseValue13i = noiseValue2;

                            noiseValue13i += (5 - (float)x) / 5;
                            noiseValue13i /= (float)x / 5;

                            float noiseValue14i = noiseValue5;

                            noiseValue14i += (5 - (float)z) / 5;
                            noiseValue14i /= (float)z / 5;


                            if (noiseValue13i > 0.2f && noiseValue14i < 0.2f)
                            {
                              
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;


                                //leftOutsideCornerExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                        }
                    }
                }
                typeOfTile = 1;
            }
        }



        /////////////////////////////////////RIGHT FRONT OUTSIDE CORNER////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.builtRightFrontOutsideCorner.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.builtRightFrontOutsideCorner[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    float noiseX5 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        float noiseY5 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);
                            float noiseZ5 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                            float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                                                              _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                
                                _chunkArray[x + width * (y + height * z)] = 0;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }

                            float noiseValue15i = noiseValue2;

                            noiseValue15i += (5 - (float)x) / 5;
                            noiseValue15i /= (float)x / 5;

                            float noiseValue16i = noiseValue5;

                            noiseValue16i += (5 - (float)z) / 5;
                            noiseValue16i /= (float)z / 5;


                            if (noiseValue15i < 0.2f && noiseValue16i < 0.2f)
                            {
                               
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;

                                //rightOutsideCornerExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                        }
                    }
                }
                typeOfTile = 1;
            }
        }



        /////////////////////////////////////LEFT BACK OUTSIDE CORNER////////////////////////////////
        
        for (int j = 0; j < LevelGenerator4.currentLevelGen.builtLeftBackOutsideCorner.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.builtLeftBackOutsideCorner[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    float noiseX5 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        float noiseY5 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);
                            float noiseZ5 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                            float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                                

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }
                            else
                            {
                                

                                _chunkArray[x + width * (y + height * z)] = 0;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;
                            }

                            float noiseValue17i = noiseValue2;

                            noiseValue17i += (5 - (float)x) / 5;
                            noiseValue17i /= (float)x / 5;

                            float noiseValue18i = noiseValue5;

                            noiseValue18i += (5 - (float)z) / 5;
                            noiseValue18i /= (float)z / 5;

                            if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                            {
                               

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;


                                //backOutsideCornerExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                        }
                    }
                }
                typeOfTile = 1;
            }
        }




        /////////////////////////////////////RIGHT BACK OUTSIDE CORNER////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.builtRightBackOutsideCorner.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.builtRightBackOutsideCorner[j])
            {
                for (int x = 0; x < width; x++)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);
                    float noiseX2 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    float noiseX5 = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 25);
                    for (int y = 0; y < height; y++)
                    {
                        float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                        float noiseY2 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        float noiseY5 = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 25);
                        for (int z = 0; z < width; z++)
                        {
                            float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 100);
                            float noiseZ2 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);
                            float noiseZ5 = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 25);

                            float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);


                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                            float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                               

                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                            }
                            
                            float noiseValue19i = noiseValue5;
                            noiseValue19i += (5 - (float)z) / 5;
                            noiseValue19i /= (float)z / 5;

                            float noiseValue20i = noiseValue2;
                            noiseValue20i += (5 - (float)x) / 5;
                            noiseValue20i /= (float)x / 5;


                            if (noiseValue19i > 0.2f && noiseValue20i < 0.2f)
                            {
                                _chunkArray[x + width * (y + height * z)] = 1;

                                _tempChunkArrayTopFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;


                                //frontOutsideCornerExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                            }
                        }
                    }
           
                }
                typeOfTile = 1;
            }
        }*/

    }


    public void Regenerate()
    {
        //vertexlist.Clear();
        //triangles.Clear();
        //uv.Clear();
        //mesh.triangles = triangles.ToArray();

        int hasNeighBoor = 0;




        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                    if (block == 0) continue;
                    {
                        if (x == 0)
                        {
                            hasNeighBoor = 0;

                            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

                            for (int i = 0; i < colliders.Length; i++)
                            {
                                if (colliders[i].transform.position == new Vector3(transform.position.x - chunkWidth, transform.position.y, transform.position.z))
                                {
                                    hasNeighBoor++;

                                    //if (!IsTransparent(x - 1, y, z))
                                    {
                                        if (colliders[i].transform.GetComponent<OldFloorTiles>().GetByte(x + chunkWidth - 1, y, z) == 0)
                                        {
                                            //Instantiate(sphere, new Vector3(x, y, z) + transform.position, Quaternion.identity);
                                            //offset1 = Vector3.up * tileHeight;
                                            //offset2 = Vector3.back;
                                            //DrawLeftFace(start, offset1, offset2, x, y, z);
                                            //buildTopLeft(x, y, z, _chunkPos, colliders[i]);
                                            //_chunkArray[x + width * (y + height * z)] = 0;
                                            //_tempChunkArrayTopFace[x + width * (y + height * z)] = 0;

                                            //_tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                            /*if (_tempChunkArrayLeftFace[x + width * (y + height * z)] == 0)
                                            {
                                                _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;
                                            }*/
                                            //_tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                                            //_tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                                            //_tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                                            //_tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                                            //leftExtremity[x, y, z] = _chunkArray[x + width * (y + height * z)];
                                        }
                                        else
                                        {
                                            /*if (_tempChunkArrayLeftFace[x + width * (y + height * z)] == 1)
                                            {

                                            }*/
                                            /*if (IsTransparent(x - 1, y, z))
                                            {
                                                
                                            }*/
                                            //_tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;
                                        }
                                    }
                                }
                            }

                            if (hasNeighBoor == 0)
                            {
                                //_tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;
                            }
                        }
                    }
                }
            }
        }

        if (typeOfTile == 0)
        {
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                        if (block == 0) continue;
                        {
                            //StartCoroutine(drawBrick(x, y, z, block));
                            ///drawBrick(x, y, z, block);
                            buildTopFace(x, y, z, _chunkPos);
                            //buildFrontFace(x, y, z, _chunkPos);
                            //buildBackFace(x, y, z, _chunkPos);
                            buildBottomFace(x, y, z, _chunkPos);
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
                        int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                        if (block == 0) continue;
                        {
                            buildTopRight(x, y, z, _chunkPos);
                            //buildTopLeft(x, y, z, _chunkPos);
                        }
                    }
                }
            }
        }




        //+AXIS - RIGHT WALL
        if (typeOfTile == 1)
        {
            //for (int y = 0; y < height; y++)
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                        if (block == 0) continue;
                        {
                            //buildFrontFace4Wall(x, y, z, _chunkPos);
                            //buildBackFace4Wall(x, y, z, _chunkPos);

                            //buildTopLeft4Wall(x, y, z, _chunkPos);
                            //buildTopLeft4Wall(x, y, z, _chunkPos);
                            //buildTopLeft4Wall(x, y, z, _chunkPos);
                            //buildTopLeft4Wall(x, y, z, _chunkPos);
                            //buildTopLeft4Wall(x, y, z, _chunkPos);


                            //buildTopRightTEST(x, y, z, _chunkPos);

                            //buildTopRight4WallXAXISPLUS(x, y, z, _chunkPos, 1);
                            //buildTopLeft4WallXAXISPLUS(x, y, z, _chunkPos, 1);
                            //buildFrontFace4Wall(x, y, z, _chunkPos);

                            //buildTopRight4WallXAXISPLUS(x, y, z, _chunkPos);
                            //buildTopLeft4WallXAXISPLUS(x, y, z, _chunkPos);
                            //buildTopLeft4WallXAXISPLUS(x, y, z, _chunkPos);
                            //buildTopLeft4Wall(x, y, z, _chunkPos);
                            //buildFrontFace4Wall(x, y, z, _chunkPos);
                        }
                    }
                }
            }
        }

        /*//+AXIS - RIGHT WALL
        if (typeOfTile == 1)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++) //for (int x = width - 1; x >= 0; x--)
                {
                    for (int z = depth - 1; z >= 0; z--)
                    {
                        int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                        if (block == 0) continue;
                        {
                            buildFrontFace4Wall(x, y, z, _chunkPos);
                            buildBackFace4Wall(x, y, z, _chunkPos);

                            //buildTopRight4WallXAXISPLUS(x, y, z, _chunkPos);
                            //buildTopLeft4WallXAXISPLUS(x, y, z, _chunkPos);
                            //buildTopLeft4WallXAXISPLUS(x, y, z, _chunkPos);
                            //buildTopLeft4Wall(x, y, z, _chunkPos);
                            //buildFrontFace4Wall(x, y, z, _chunkPos);
                        }
                    }
                }
            }
        }*/


        /* //+AXIS - RIGHT WALL
         if (typeOfTile == 1)
         {
             int invY = 0;
             for (int y = height - 1; y >= 0; y--) //for (int y = 0; y < height; y++) 
             {
                 for (int x = 0; x < width; x++) //for (int x = width - 1; x >= 0; x--)
                 {
                     for (int z = 0; z < depth; z++)
                     {
                         int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                         if (block == 0) continue;
                         {
                             buildTopFace4Wall(x, y, z, _chunkPos);
                             buildBottomFace4Wall(x, y, z, _chunkPos);




                             //buildTopRight4WallXAXISPLUS(x, y, z, _chunkPos);
                             //buildTopLeft4WallXAXISPLUS(x, y, z, _chunkPos);
                             //buildTopLeft4WallXAXISPLUS(x, y, z, _chunkPos);
                             //buildTopLeft4Wall(x, y, z, _chunkPos);
                             //buildFrontFace4Wall(x, y, z, _chunkPos);
                         }
                     }
                 }
             }
         }*/









        /*
        //+AXIS - LEFT WALL
        if (typeOfTile == 2)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = width - 1; x >= 0; x--)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                        if (block == 0) continue;
                        {
                            buildTopLEFT4WallXAXISPLUS(x, y, z, _chunkPos);
                            //buildTopLeft4WallXAXISPLUS(x, y, z, _chunkPos);
                            //buildTopLeft4Wall(x, y, z, _chunkPos);
                            //buildFrontFace4Wall(x, y, z, _chunkPos);
                        }
                    }
                }
            }
        }*/







        /*
        //+AXIS - RIGHT WALL
        if (typeOfTile == 3)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = width - 1; x >= 0; x--)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                        if (block == 0) continue;
                        {
                            buildTopLeft4WallXAXISPLUS(x, y, z, _chunkPos);
                            //buildTopLeft4Wall(x, y, z, _chunkPos);
                            //buildFrontFace4Wall(x, y, z, _chunkPos);
                        }
                    }
                }
            }
        }


        //+AXIS - LEFT WALL
        if (typeOfTile == 4)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = width - 1; x >= 0; x--)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                        if (block == 0) continue;
                        {
                            buildTopLeft4WallXAXISPLUS(x, y, z, _chunkPos);
                            //buildTopLeft4Wall(x, y, z, _chunkPos);
                            //buildFrontFace4Wall(x, y, z, _chunkPos);
                        }
                    }
                }
            }
        }*/



























        /*
        if (typeOfTile == 1 || typeOfTile == 2 || typeOfTile == 3)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = width - 1; x >= 0; x--)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                        if (block == 0) continue;
                        {
                            buildTopRight4Wall(x, y, z, _chunkPos);
                            //buildTopLeft4Wall(x, y, z, _chunkPos);
                            //buildFrontFace4Wall(x, y, z, _chunkPos);
                        }
                    }
                }
            }
        }*/



        /*
        if (typeOfTile == 1)
        {
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int block = _chunkArray[x + width * (y + height * z)]; //map[x, y, z];

                        if (block == 0) continue;
                        {
                            buildTopLeft4Wall(x, y, z, _chunkPos);
                        }
                    }
                }
            }
        }*/








        /*
        mesh.vertices = vertexlist.ToArray();
        mesh.triangles = triangles.ToArray();
        //mesh.uv = uv.ToArray();

        //mesh.uv = uv.ToArray();

        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();*/
        //gameObject.GetComponent<MeshCollider>().enabled = false;

        //this.GetComponent<MeshRenderer>().material = _);

    }

    void drawBrick(int x, int y, int z, int block)
    {
        Vector3 start = new Vector3(x, y, z);
        Vector3 offset1, offset2;

        start.y *= tileHeight;

        ///TOPFACE
        if (IsTransparent(x, y + 1, z))
        {
            offset1 = Vector3.right;
            offset2 = Vector3.back;
            DrawTopFace(new Vector3(start.x, start.y, start.z) + Vector3.up * tileHeight, offset1, offset2, x, y, z);
            //buildTopFace(new Vector3(start.x, start.y, start.z) + Vector3.up * tileHeight, offset1, offset2, x, y, z);
            //buildTopFace(x, y, z, _chunkPos);
        }

        //LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            offset1 = Vector3.up * tileHeight;
            offset2 = Vector3.back;
            DrawLeftFace(start, offset1, offset2, x, y, z);
            //buildTopLeft(x, y, z, _chunkPos);
        }

        //RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            offset1 = Vector3.down * tileHeight;
            offset2 = Vector3.back;
            DrawRightFace(start + Vector3.right + Vector3.up * tileHeight, offset1, offset2, x, y, z);
            //buildTopRight(x, y, z, _chunkPos);
        }

        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            offset1 = Vector3.right;
            offset2 = Vector3.up * tileHeight;
            DrawBackFace(start, offset1, offset2, x, y, z);
            //buildBackFace(x, y, z, _chunkPos);
        }

        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            offset1 = Vector3.left;
            offset2 = Vector3.up * tileHeight;
            DrawFrontFace(start + Vector3.right + Vector3.back, offset1, offset2, x, y, z);
            //buildFrontFace(x, y, z, _chunkPos);
        }

        //BOTTOM FACE
        if (IsTransparent(x, y - 1, z))
        {
            offset1 = Vector3.back;//(new Vector3(1, 0, 0));
            offset2 = Vector3.right;//(new Vector3(0, 0, 1));
            DrawBottomFace(start + Vector3.down + Vector3.up, offset1, offset2, x, y, z);//
        }


        /*if (x == 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == new Vector3(transform.position.x - chunkWidth, transform.position.y, transform.position.z))
                {
                    if (IsTransparent(x - 1, y, z))
                    {
                        if (colliders[i].GetComponent<OldFloorTiles>().GetByte(x + chunkWidth - 1, y, z) != 1)
                        {
                            //Instantiate(sphere, new Vector3(x, y, z) + transform.position, Quaternion.identity);
                            //offset1 = Vector3.up * tileHeight;
                            //offset2 = Vector3.back;
                            //DrawLeftFace(start, offset1, offset2, x, y, z);
                            buildTopLeft(x, y, z, _chunkPos,colliders[i]);
                        }
                    }
                }
            }
        }*/









        /*//LEFTFACE EXTREMITY
        if (x == 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == new Vector3(transform.position.x - chunkWidth, transform.position.y, transform.position.z))
                {
                    if (IsTransparent(x - 1, y, z))
                    {
                        if (colliders[i].GetComponent<OldFloorTiles>().GetByte(x + chunkWidth - 1, y, z) != 1)
                        {
                            //Instantiate(sphere, new Vector3(x, y, z) + transform.position, Quaternion.identity);
                            offset1 = Vector3.up * tileHeight;
                            offset2 = Vector3.back;
                            //DrawLeftFace(start, offset1, offset2, x, y, z);
                            buildTopLeft(x, y, z, _chunkPos);
                        }
                    }
                }
            }
        }


        //LEFTFACE
        if (x > 0 && x <= chunkWidth - 1)
        {
            if (IsTransparent(x - 1, y, z))
            {
                offset1 = Vector3.up * tileHeight;
                offset2 = Vector3.back;
                //DrawLeftFace(start, offset1, offset2, x, y, z);
                buildTopLeft(x, y, z, _chunkPos);
            }
        }*/








        /*
        //RIGHTFACE EXTREMITY

        if (x == chunkWidth - 1)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == new Vector3(transform.position.x + chunkWidth, transform.position.y, transform.position.z))
                {

                    //Instantiate(sphere, colliders[i].transform.position, Quaternion.identity);
                    if (IsTransparent(x + 1, y, z))
                    {
                        if (colliders[i].GetComponent<OldFloorTiles>().GetByte(x - chunkWidth + 1, y, z) != 1)
                        {
                            offset1 = Vector3.down * tileHeight;
                            offset2 = Vector3.back;
                            //DrawRightFace(start + Vector3.right + Vector3.up * tileHeight, offset1, offset2, x, y, z);
                            buildTopRight(x, y, z, _chunkPos);

                        }
                    }
                }
            }
        }

        //RIGHTFACE
        if (x >= 0 && x < width - 1)
        {
            if (IsTransparent(x + 1, y, z))
            {
                offset1 = Vector3.down * tileHeight;
                offset2 = Vector3.back;
                //DrawRightFace(start + Vector3.right + Vector3.up * tileHeight, offset1, offset2, x, y, z);
                buildTopRight(x, y, z, _chunkPos);
            }
        }




        //BACKFACE EXTREMITY

        if (z == chunkWidth - 1)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == new Vector3(transform.position.x, transform.position.y, transform.position.z + chunkWidth))
                {
                    //Instantiate(sphere, colliders[i].transform.position, Quaternion.identity);
                    if (IsTransparent(x, y, z + 1))
                    {
                        if (colliders[i].GetComponent<OldFloorTiles>().GetByte(x, y, z - chunkWidth + 1) != 1)
                        {
                            offset1 = Vector3.right;
                            offset2 = Vector3.up * tileHeight;
                            //DrawBackFace(start, offset1, offset2, x, y, z);
                            buildBackFace(x, y, z, _chunkPos);

                        }
                    }
                }
            }
        }
        //BACKFACE
        if (z >= 0 && z < chunkWidth - 1)
        {
            if (IsTransparent(x, y, z + 1))
            {
                offset1 = Vector3.right;
                offset2 = Vector3.up * tileHeight;
                //DrawBackFace(start, offset1, offset2, x, y, z);
                buildBackFace(x, y, z, _chunkPos);
            }

        }






        //FRONTFACE EXTREMITY

        if (z == 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == new Vector3(transform.position.x, transform.position.y, transform.position.z - chunkWidth))
                {
                    //Instantiate(sphere, colliders[i].transform.position, Quaternion.identity);
                    if (IsTransparent(x, y, z - 1))
                    {
                        if (colliders[i].GetComponent<OldFloorTiles>().GetByte(x, y, z + chunkWidth - 1) != 1)
                        {
                            offset1 = Vector3.left;
                            offset2 = Vector3.up * tileHeight;
                            //DrawFrontFace(start + Vector3.right + Vector3.back, offset1, offset2, x, y, z);
                            buildFrontFace(x, y, z, _chunkPos);

                        }
                    }
                }
            }
        }

        //FRONTFACE
        if (z > 0 && z <= chunkWidth - 1)
        {
            if (IsTransparent(x, y, z - 1))
            {
                offset1 = Vector3.left;
                offset2 = Vector3.up * tileHeight;
                //DrawFrontFace(start + Vector3.right + Vector3.back, offset1, offset2, x, y, z);
                buildFrontFace(x, y, z, _chunkPos);
            }
        }


        //BOTTOM FACE EXTREMITY

        /*if (z == 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == new Vector3(transform.position.x, transform.position.y - chunkWidth, transform.position.z))
                {
                    //Instantiate(sphere, colliders[i].transform.position, Quaternion.identity);
                    if (IsTransparent(x, y - 1, z))
                    {
                        if (colliders[i].GetComponent<OldFloorTiles>().GetByte(x, y - chunkWidth + 1, z) != 1)
                        {
                            offset1 = Vector3.back;//(new Vector3(1, 0, 0));
                            offset2 = Vector3.right;//(new Vector3(0, 0, 1));
                            DrawBottomFace(start + Vector3.down + Vector3.up, offset1, offset2, x, y, z);

                        }
                    }
                }
            }
        }

        //BOTTOM FACE
        if (y > 0 && y <= height - 1)
        {
            if (IsTransparent(x, y - 1, z))
            {
                offset1 = Vector3.back;//(new Vector3(1, 0, 0));
                offset2 = Vector3.right;//(new Vector3(0, 0, 1));
                DrawBottomFace(start + Vector3.down + Vector3.up, offset1, offset2, x, y, z);
            }
        }*/

        //yield return new WaitForSeconds(0.5f);
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
    int _maxHeight = 0;

    int _block = 0;

    //  UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWidth: " + _maxWidth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
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

        _block = _tempChunkArrayTopFace[_x + width * (_y + height * _z)];
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
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayTopFace[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayTopFace[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayTopFace[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = _y + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayTopFace[(rowIterateX) + width * ((_y + 1) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = _y + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = _y + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayTopFace[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = _y + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayTopFace[(rowIterateX) + width * ((_y + 1) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = _y + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = _y + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
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
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = _y + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayTopFace[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayTopFace[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = _y + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_xx > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayTopFace[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayTopFace[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayTopFace[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayTopFace[(rowIterateX) + width * ((_y + 1) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayTopFace[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayTopFace[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;

                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = _y + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, _y, rowIterateZ))
                        {
                            _tempChunkArrayTopFace[(rowIterateX) + width * (_y + height * (rowIterateZ))] = 2;
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
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
    }



    //  UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWidth: " + _maxWidth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
    void buildTopFace4Wall(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayTopFaceYAxISPLUS[_x + width * (_y + height * _z)];
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
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = _y + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX) + width * ((_y + 1) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = _y + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = _y + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = _y + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX) + width * ((_y + 1) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = _y + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = _y + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
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
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = _y + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = _y + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_xx > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX) + width * ((_y) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX) + width * ((_y + 1) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX + 1) + width * ((_y) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX + 1) + width * ((_y + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;

                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = _y + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, _y, rowIterateZ))
                        {
                            _tempChunkArrayTopFaceYAxISPLUS[(rowIterateX) + width * (_y + height * (rowIterateZ))] = 2;
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
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
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

        _block = _tempChunkArrayLeftFace[_x + width * (_y + height * _z)];

        if (_block == 1) //|| _block == 2
        {
            //LEFTFACE
            if (IsTransparent(_x - 1, _y, _z))
            //if (collider.GetComponent<OldFloorTiles>().GetByte(_x + chunkWidth - 1, _y, _z) != 1)
            {
                for (int _zz = 0; _zz < _maxDepth; _zz++)
                {
                    rowIterateZ = _z + _zz;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y + _yy;
                        //rowIterateX = _x + _xx;
                        if (rowIterateY <= height && rowIterateY >= 0 && rowIterateZ <= depth) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = _x;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = rowIterateZ;
                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorOrange, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))/************************/
                                            {
                                                _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = _x;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy == 0 && _zz > 0)
                            {
                                /*if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        if (!foundVertTwo)
                                        {
                                            twoVertIndexX = _x;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                        }



                                    }
                                }*/



















                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                    {

                                    }

                                    if (_block == 0)
                                    {
                                        if (foundVertTwo)
                                        {
                                            threeVertIndexX = _x;
                                            threeVertIndexY = rowIterateY + _yy + 1;
                                            threeVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz;
                                            foundVertThree = true;
                                            //Instantiate(_sphereVisualOtherColorOrange, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                            if (foundVertThree)
                                            {
                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorOrange, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                Instantiate(_sphereVisualOtherColorOrange, new Vector3(_x, rowIterateY + _yy + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;

                                                Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);

                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x;
                                    threeVertIndexY = rowIterateY + _yy + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = _x;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            ////Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorOrange, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        Instantiate(_sphereVisualOtherColorOrange, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(_x, rowIterateY, rowIterateZ)) //if (colliders[i].GetComponent<OldFloorTiles>().GetByte(x + chunkWidth - 1, y, z) != 1)
                        {
                            _tempChunkArrayLeftFace[(_x) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }




                /*oneVertIndexX = rowIterateX;
                oneVertIndexY = y + 1;
                oneVertIndexZ = rowIterateZ;

                threeVertIndexX = rowIterateX + 1;
                threeVertIndexY = y + 1;
                threeVertIndexZ = rowIterateZ;

                fourVertIndexX = threeVertIndexX;
                fourVertIndexY = y + 1;
                fourVertIndexZ = twoVertIndexZ;

                twoVertIndexX = rowIterateX;
                twoVertIndexY = y + 1;
                twoVertIndexZ = rowIterateZ + 1;*/

                /*vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, (oneVertIndexZ + 1) * planeSize));
                vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, twoVertIndexY * planeSize, (twoVertIndexZ - 1) * planeSize));
                vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY - 1) * planeSize, (threeVertIndexZ) * planeSize));
                vertexlist.Add(new Vector3((fourVertIndexX - 1) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ - 1) * planeSize));
                */

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3((oneVertIndexX) * planeSize, (oneVertIndexY) * planeSize, (oneVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, (twoVertIndexY) * planeSize, (twoVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY) * planeSize, (threeVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3((fourVertIndexX) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))];


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
                        uv.Add(new Vector2(0.1875f, 0.9375f));
                        uv.Add(new Vector2(0.1875f, 1));
                        uv.Add(new Vector2(0.25f, 0.9375f));
                        uv.Add(new Vector2(0.25f, 1));
                    }*/

                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/

                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                }
            }
        }

        /*_mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }


    void buildRIGHTWALL(int _x, int _y, int _z, Vector3 chunkPos, int typeOfTile)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;

        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;

        if (typeOfTile == 1)
        {
            _block = _tempChunkArrayLeftFaceXAxISPLUS[_x + width * (_y + height * _z)];
        }
        else if (typeOfTile == 2)
        {
            _block = _tempChunkArrayRightFaceXAxISPLUS[_x + width * (_y + height * _z)];
        }

        if (_block == 1) //|| _block == 2
        {
            //LEFTFACE
            if (IsTransparent(_x + 1, _y, _z))
            //if (collider.GetComponent<OldFloorTiles>().GetByte(_x + chunkWidth - 1, _y, _z) != 1)
            {
                for (int _zz = 0; _zz < _maxDepth; _zz++)
                {
                    rowIterateZ = _z + _zz;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y - _yy;
                        //rowIterateX = _x + _xx;                

                        if (rowIterateY <= height && rowIterateY >= 0 && rowIterateZ <= depth) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = _x;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = rowIterateZ;
                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            if (typeOfTile == 1)
                                            {
                                                _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }
                                            else if (typeOfTile == 2)
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];


                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    }

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))/************************/
                                            {
                                                if (typeOfTile == 1)
                                                {
                                                    _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                }
                                                else if (typeOfTile == 2)
                                                {
                                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                }

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = _x;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0) 
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            if (typeOfTile == 1)
                                            {
                                                _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }
                                            else if (typeOfTile == 2)
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }



                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x;
                                    threeVertIndexY = rowIterateY + _yy + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    }

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))
                                    {
                                        //*****************************************************************************
                                        if (typeOfTile == 1)
                                        {
                                            _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                        }
                                        else if (typeOfTile == 2)
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                        }

                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    }

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))
                                            {
                                                if (typeOfTile == 1)
                                                {
                                                    _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                }
                                                else if (typeOfTile == 2)
                                                {
                                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                }


                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = _x;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            ////Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            if (typeOfTile == 1)
                                            {
                                                _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }
                                            else if (typeOfTile == 2)
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }



                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            if (typeOfTile == 1)
                                            {
                                                _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }
                                            else if (typeOfTile == 2)
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }


                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(_x, rowIterateY, rowIterateZ)) //if (colliders[i].GetComponent<OldFloorTiles>().GetByte(x + chunkWidth - 1, y, z) != 1)
                        {
                            _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }




                /*oneVertIndexX = rowIterateX;
                oneVertIndexY = y + 1;
                oneVertIndexZ = rowIterateZ;

                threeVertIndexX = rowIterateX + 1;
                threeVertIndexY = y + 1;
                threeVertIndexZ = rowIterateZ;

                fourVertIndexX = threeVertIndexX;
                fourVertIndexY = y + 1;
                fourVertIndexZ = twoVertIndexZ;

                twoVertIndexX = rowIterateX;
                twoVertIndexY = y + 1;
                twoVertIndexZ = rowIterateZ + 1;*/

                /*vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, (oneVertIndexZ + 1) * planeSize));
                vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, twoVertIndexY * planeSize, (twoVertIndexZ - 1) * planeSize));
                vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY - 1) * planeSize, (threeVertIndexZ) * planeSize));
                vertexlist.Add(new Vector3((fourVertIndexX - 1) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ - 1) * planeSize));
                */

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3((oneVertIndexX) * planeSize, (oneVertIndexY) * planeSize, (oneVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, (twoVertIndexY) * planeSize, (twoVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY) * planeSize, (threeVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3((fourVertIndexX) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))];


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
                        uv.Add(new Vector2(0.1875f, 0.9375f));
                        uv.Add(new Vector2(0.1875f, 1));
                        uv.Add(new Vector2(0.25f, 0.9375f));
                        uv.Add(new Vector2(0.25f, 1));
                    }*/

                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/

                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                }
            }
        }

        /*_mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }

    void buildTopLeft4WallXAXISPLUS(int _x, int _y, int _z, Vector3 chunkPos, int typeOfTile)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;

        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;

        if (typeOfTile == 1)
        {
            _block = _tempChunkArrayLeftFaceXAxISPLUS[_x + width * (_y + height * _z)];
        }
        else if (typeOfTile == 2)
        {
            _block = _tempChunkArrayRightFaceXAxISPLUS[_x + width * (_y + height * _z)];
        }

        if (_block == 1) //|| _block == 2
        {
            //LEFTFACE
            if (IsTransparent(_x - 1, _y, _z))
            //if (collider.GetComponent<OldFloorTiles>().GetByte(_x + chunkWidth - 1, _y, _z) != 1)
            {
                for (int _zz = 0; _zz < _maxDepth; _zz++)
                {
                    rowIterateZ = _z + _zz;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y - _yy;
                        //rowIterateX = _x + _xx;                

                        if (rowIterateY <= height && rowIterateY >= 0 && rowIterateZ <= depth) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = _x;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = rowIterateZ;
                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            if (typeOfTile == 1)
                                            {
                                                _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }
                                            else if (typeOfTile == 2)
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];


                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    }

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))/************************/
                                            {
                                                if (typeOfTile == 1)
                                                {
                                                    _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                }
                                                else if (typeOfTile == 2)
                                                {
                                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                }

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = _x;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0) 
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            if (typeOfTile == 1)
                                            {
                                                _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }
                                            else if (typeOfTile == 2)
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }



                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x;
                                    threeVertIndexY = rowIterateY + _yy + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    }

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))
                                    {
                                        //*****************************************************************************
                                        if (typeOfTile == 1)
                                        {
                                            _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                        }
                                        else if (typeOfTile == 2)
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                        }

                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    }

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))
                                            {
                                                if (typeOfTile == 1)
                                                {
                                                    _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                }
                                                else if (typeOfTile == 2)
                                                {
                                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                }


                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = _x;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            ////Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            if (typeOfTile == 1)
                                            {
                                                _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }
                                            else if (typeOfTile == 2)
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }



                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                {
                                    if (typeOfTile == 1)
                                    {
                                        _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }
                                    else if (typeOfTile == 2)
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    }

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            if (typeOfTile == 1)
                                            {
                                                _block = _tempChunkArrayLeftFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }
                                            else if (typeOfTile == 2)
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            }


                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(_x, rowIterateY, rowIterateZ)) //if (colliders[i].GetComponent<OldFloorTiles>().GetByte(x + chunkWidth - 1, y, z) != 1)
                        {
                            _tempChunkArrayLeftFaceXAxISPLUS[(_x) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }




                /*oneVertIndexX = rowIterateX;
                oneVertIndexY = y + 1;
                oneVertIndexZ = rowIterateZ;

                threeVertIndexX = rowIterateX + 1;
                threeVertIndexY = y + 1;
                threeVertIndexZ = rowIterateZ;

                fourVertIndexX = threeVertIndexX;
                fourVertIndexY = y + 1;
                fourVertIndexZ = twoVertIndexZ;

                twoVertIndexX = rowIterateX;
                twoVertIndexY = y + 1;
                twoVertIndexZ = rowIterateZ + 1;*/

                /*vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, (oneVertIndexZ + 1) * planeSize));
                vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, twoVertIndexY * planeSize, (twoVertIndexZ - 1) * planeSize));
                vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY - 1) * planeSize, (threeVertIndexZ) * planeSize));
                vertexlist.Add(new Vector3((fourVertIndexX - 1) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ - 1) * planeSize));
                */

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3((oneVertIndexX) * planeSize, (oneVertIndexY) * planeSize, (oneVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, (twoVertIndexY) * planeSize, (twoVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY) * planeSize, (threeVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3((fourVertIndexX) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))];


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
                        uv.Add(new Vector2(0.1875f, 0.9375f));
                        uv.Add(new Vector2(0.1875f, 1));
                        uv.Add(new Vector2(0.25f, 0.9375f));
                        uv.Add(new Vector2(0.25f, 1));
                    }*/

                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/

                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                }
            }
        }

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

        //RIGHTFACE
        _block = _tempChunkArrayRightFace[_x + width * (_y + height * _z)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x + 1, _y, _z))
            {
                for (int _zz = 0; _zz < _maxDepth; _zz++)
                {
                    rowIterateZ = _z + _zz;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y + _yy;
                        //rowIterateX = _x + _xx;                

                        if (rowIterateY <= height && rowIterateY >= 0 && rowIterateZ <= depth) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = _x + 1;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = rowIterateZ;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorOrange, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorOrange, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorOrange, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))/************************/
                                            {
                                                _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = _x + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorOrange, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }


                                        /*if (blockExistsInArray(_x, rowIterateY + 1, rowIterateZ))
                                        {

                                            _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY+1) + height * (rowIterateZ))];

                                            if (_block == 0)
                                            {
                                                
                                            }
                                        }*/
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x + 1;
                                    threeVertIndexY = rowIterateY + _yy + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))
                                    {
                                        _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = _x + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        if (blockExistsInArray(_x, rowIterateY + 1, rowIterateZ))
                                        {

                                            _block = _tempChunkArrayRightFace[(_x) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                            if (_block == 0)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                Instantiate(_sphereVisualOtherColorOrange, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (foundVertTwo)
                                                {
                                                    if (foundVertThree)
                                                    {
                                                        //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = rowIterateY;
                                                            fourVertIndexZ = threeVertIndexZ;
                                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    /*if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }*/
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(_x, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayRightFace[(_x) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }

                /*oneVertIndexX = rowIterateX;
                oneVertIndexY = y + 1;
                oneVertIndexZ = rowIterateZ;

                threeVertIndexX = rowIterateX + 1;
                threeVertIndexY = y + 1;
                threeVertIndexZ = rowIterateZ;

                fourVertIndexX = threeVertIndexX;
                fourVertIndexY = y + 1;
                fourVertIndexZ = twoVertIndexZ;

                twoVertIndexX = rowIterateX;
                twoVertIndexY = y + 1;
                twoVertIndexZ = rowIterateZ + 1;*/

                /*vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, (oneVertIndexZ + 1) * planeSize));
                vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, twoVertIndexY * planeSize, (twoVertIndexZ - 1) * planeSize));
                vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY - 1) * planeSize, (threeVertIndexZ) * planeSize));
                vertexlist.Add(new Vector3((fourVertIndexX - 1) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ - 1) * planeSize));
                */

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3((oneVertIndexX) * planeSize, (oneVertIndexY) * planeSize, (oneVertIndexZ) * planeSize));


                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, (twoVertIndexY) * planeSize, (twoVertIndexZ) * planeSize));


                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY) * planeSize, (threeVertIndexZ) * planeSize));


                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3((fourVertIndexX) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ) * planeSize));

                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {

                    _index0 = _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))];

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

                        uv.Add(new Vector2(0.25f, 1));
                        uv.Add(new Vector2(0.25f, 0.9375f));
                        uv.Add(new Vector2(0.1875f, 1));
                        uv.Add(new Vector2(0.1875f, 0.9375f));
                    }*/

                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/

                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                }
            }
        }

        //_mesh.vertices = vertexlist.ToArray();
        //_mesh.triangles = triangles.ToArray();

        //_testChunk.GetComponent<MeshFilter>().mesh = _mesh;
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }




    void buildTopRightTEST(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //RIGHTFACE
        _block = _tempChunkArrayRightFaceXAxISPLUS[_x + width * (_y + height * _z)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x + 1, _y, _z))
            {
                for (int _zz = 0; _zz < _maxDepth; _zz++)
                {
                    rowIterateZ = _z + _zz;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y - _yy;
                        //rowIterateX = _x + _xx;                

                        if (rowIterateY <= height && rowIterateY >= 0 && rowIterateZ <= depth) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = _x + 1;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = rowIterateZ;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))/************************/
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = _x + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0) 
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x + 1;
                                    threeVertIndexY = rowIterateY + _yy + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = _x + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(_x, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }

                /*oneVertIndexX = rowIterateX;
                oneVertIndexY = y + 1;
                oneVertIndexZ = rowIterateZ;

                threeVertIndexX = rowIterateX + 1;
                threeVertIndexY = y + 1;
                threeVertIndexZ = rowIterateZ;

                fourVertIndexX = threeVertIndexX;
                fourVertIndexY = y + 1;
                fourVertIndexZ = twoVertIndexZ;

                twoVertIndexX = rowIterateX;
                twoVertIndexY = y + 1;
                twoVertIndexZ = rowIterateZ + 1;*/

                /*vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, (oneVertIndexZ + 1) * planeSize));
                vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, twoVertIndexY * planeSize, (twoVertIndexZ - 1) * planeSize));
                vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY - 1) * planeSize, (threeVertIndexZ) * planeSize));
                vertexlist.Add(new Vector3((fourVertIndexX - 1) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ - 1) * planeSize));
                */

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3((oneVertIndexX) * planeSize, (oneVertIndexY) * planeSize, (oneVertIndexZ) * planeSize));


                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, (twoVertIndexY) * planeSize, (twoVertIndexZ) * planeSize));


                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY) * planeSize, (threeVertIndexZ) * planeSize));


                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3((fourVertIndexX) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ) * planeSize));

                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {

                    _index0 = _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))];

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

                        uv.Add(new Vector2(0.25f, 1));
                        uv.Add(new Vector2(0.25f, 0.9375f));
                        uv.Add(new Vector2(0.1875f, 1));
                        uv.Add(new Vector2(0.1875f, 0.9375f));
                    }*/

                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/

                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                }
            }
        }

        //_mesh.vertices = vertexlist.ToArray();
        //_mesh.triangles = triangles.ToArray();

        //_testChunk.GetComponent<MeshFilter>().mesh = _mesh;
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }

    void buildTopRight4WallXAXISPLUS(int _x, int _y, int _z, Vector3 chunkPos, int typeOfTile)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;

        //RIGHTFACE
        _block = _tempChunkArrayRightFaceXAxISPLUS[_x + width * (_y + height * _z)];

        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x + 1, _y, _z))
            {
                for (int _zz = 0; _zz < _maxDepth; _zz++)
                {
                    rowIterateZ = _z + _zz;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)

                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y - _yy;

                        //rowIterateX = _x + _xx;                

                        if (rowIterateY <= height && rowIterateY >= 0 && rowIterateZ <= depth) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = _x + 1;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = rowIterateZ;

                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))/************************/
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = _x + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = _x + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;

                                    _maxHeight = _yy + 1;

                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy == 0 && _zz > 0) 
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = _x + 1;
                                    threeVertIndexY = rowIterateY + _yy + 1;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (!blockExistsInArray(_x, rowIterateY + 1, rowIterateZ)) //rowIterateY + 1
                                {
                                    twoVertIndexX = _x + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;

                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;

                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ ) * planeSize + _chunkPos, Quaternion.identity);
                                    }





                                }







                                /*
                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = _x + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                   
                                    twoVertIndexX = _x + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;

                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ ) * planeSize + _chunkPos, Quaternion.identity);
                                    }

                                }*/

                                /*if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];



                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(_x, rowIterateY + 1, _z))
                                {
                                    twoVertIndexX = _x + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ;
                                    foundVertTwo = true;
                                }*/
                            }
                            else if (_yy > 0 && _zz > 0)
                            {
                                if (foundVertTwo)
                                {
                                    if (!blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                    {
                                        if (!blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                        {
                                            _maxDepth = _zz;

                                            foundVertThree = true;

                                            threeVertIndexX = _x + 1;
                                            threeVertIndexY = rowIterateY + _yy;
                                            threeVertIndexZ = rowIterateZ + 1;


                                            if (!blockExistsInArray(_x, rowIterateY + 1, rowIterateZ)) //rowIterateY + 1
                                            {
                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY) // && rowIterateY == twoVertIndexY
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY + 1;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }

                                        }
                                    }
                                }









                                /*
                                if (!blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                {
                                    if (!blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ))];


                                        //if (_block == 1 || _block == 2)
                                        {
                                            //if (rowIterateY + _yy > _y)
                                            //{
                                            //    threeVertIndexX = _x + 1;
                                            //    threeVertIndexY = rowIterateY + _yy;
                                            //    threeVertIndexZ = rowIterateZ + 1;
                                            //
                                            //}

                                            threeVertIndexX = _x + 1;
                                            threeVertIndexY = rowIterateY + _yy;
                                            threeVertIndexZ = rowIterateZ + 1;

                                            _maxDepth = _zz;

                                            foundVertThree = true;

                                            //fourVertIndexX = threeVertIndexX;
                                            //fourVertIndexY = _y;
                                            //fourVertIndexZ = threeVertIndexZ;

                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY) // && rowIterateY == twoVertIndexY
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY+1;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }*/






                                /*
                                _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                if (_block == 1 || _block == 2)
                                {
                                    threeVertIndexX = _x + 1;
                                    threeVertIndexY = rowIterateY + _yy;
                                    threeVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }*/








                                /*else if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {




                                    if (((_x) + width * ((rowIterateY + _yy) + height * (rowIterateZ+1))) > width * height * depth)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                    }

                                }
                                */



                                /*else if (blockExistsInArray(_x, rowIterateY, rowIterateZ))
                                {
                                    if (((_x) + width * ((rowIterateY + _yy) + height * (rowIterateZ))) > width * height * depth)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                    }

                                }*/






                                /*
                                if (((_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))) < (width * height * depth))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = oneVertIndexY;
                                        threeVertIndexZ = rowIterateZ;
                                    }
                                }*/

                                /*
                                if (((_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))) < (width * height * depth))
                                {
                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(rowIterateX) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (!blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = oneVertIndexY;
                                        threeVertIndexZ = rowIterateZ;
                                    }
                                }*/

















                                /*if (foundVertTwo)
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }*/

                                /*
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {







                                    _block = _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (foundVertTwo)
                                    {
                                        /*
                                        if (rowIterateY == twoVertIndexY) //rowIterateZ + 1 == threeVertIndexZ && 
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (!blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }



                                    /*
                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFaceXAxISPLUS[(_x + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (foundVertTwo)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }   
                                }*/

                                /*if (!blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }*/
                            }
                        }

                        if (blockExistsInArray(_x, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayRightFaceXAxISPLUS[(_x) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }




                /*oneVertIndexX = rowIterateX;
                oneVertIndexY = y + 1;
                oneVertIndexZ = rowIterateZ;

                threeVertIndexX = rowIterateX + 1;
                threeVertIndexY = y + 1;
                threeVertIndexZ = rowIterateZ;

                fourVertIndexX = threeVertIndexX;
                fourVertIndexY = y + 1;
                fourVertIndexZ = twoVertIndexZ;

                twoVertIndexX = rowIterateX;
                twoVertIndexY = y + 1;
                twoVertIndexZ = rowIterateZ + 1;*/

                /*vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, (oneVertIndexZ + 1) * planeSize));
                vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, twoVertIndexY * planeSize, (twoVertIndexZ - 1) * planeSize));
                vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY - 1) * planeSize, (threeVertIndexZ) * planeSize));
                vertexlist.Add(new Vector3((fourVertIndexX - 1) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ - 1) * planeSize));
                */

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3((oneVertIndexX) * planeSize, (oneVertIndexY) * planeSize, (oneVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, (twoVertIndexY) * planeSize, (twoVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY) * planeSize, (threeVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.875f));

                    vertexlist.Add(new Vector3((fourVertIndexX) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {

                    _index0 = _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))];

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

                       uv.Add(new Vector2(0.25f, 1));
                       uv.Add(new Vector2(0.25f, 0.9375f));
                       uv.Add(new Vector2(0.1875f, 1));
                       uv.Add(new Vector2(0.1875f, 0.9375f));
                   }*/

                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/

                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                }
            }
        }

        //_mesh.vertices = vertexlist.ToArray();
        //_mesh.triangles = triangles.ToArray();

        //_testChunk.GetComponent<MeshFilter>().mesh = _mesh;
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
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
        //RIGHTFACE

        _block = _tempChunkArrayFrontFace[_x + width * (_y + height * _z)];

        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x, _y, _z - 1))
            {
                for (int _xx = 0; _xx < _maxWidth; _xx++)
                {
                    rowIterateX = _x + _xx;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y - _yy;
                        //rowIterateX = _x + _xx;                

                        if (rowIterateY <= height && rowIterateY >= 0 && rowIterateX <= width) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = _z;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = _z;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x, rowIterateY - 1, _z - 1))/************************/
                                            {
                                                _block = _tempChunkArrayFrontFace[(_x) + width * ((rowIterateY - 1) + height * (_z - 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = _z;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = _z;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_vertVisual, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = rowIterateY + _yy + 1;
                                    threeVertIndexZ = _z;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(_x) + width * ((rowIterateY - 1) + height * (_z))]; //////////////////////////////////////////////////////////

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY - 1, _z))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayFrontFace[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z - 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z - 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = _z;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = _z;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                    uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3((oneVertIndexX) * planeSize, (oneVertIndexY) * planeSize, (oneVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, (twoVertIndexY) * planeSize, (twoVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY) * planeSize, (threeVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3((fourVertIndexX) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))];


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
                        uv.Add(new Vector2(0.25f, 0.9375f));
                        uv.Add(new Vector2(0.1875f, 0.9375f));
                        uv.Add(new Vector2(0.25f, 1));
                        uv.Add(new Vector2(0.1875f, 1));
                    }*/


                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/

                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                }
            }
        }

        /*_mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }






    void buildFrontFace4Wall(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //RIGHTFACE

        _block = _tempChunkArrayFrontFaceZAxISPLUS[_x + width * (_y + height * _z)];

        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x, _y, _z - 1))
            {
                for (int _xx = 0; _xx < _maxWidth; _xx++)
                {
                    rowIterateX = _x + _xx;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y - _yy;
                        //rowIterateX = _x + _xx;                

                        if (rowIterateY <= height && rowIterateY >= 0 && rowIterateX <= width) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = _z;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = _z;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x, rowIterateY - 1, _z - 1))/************************/
                                            {
                                                _block = _tempChunkArrayFrontFaceZAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (_z - 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = _z;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = _z;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_vertVisual, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = rowIterateY + _yy + 1;
                                    threeVertIndexZ = _z;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFaceZAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (_z))]; //////////////////////////////////////////////////////////

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY - 1, _z))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayFrontFaceZAxISPLUS[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z - 1))
                                            {
                                                _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z - 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = _z;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = _z;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, _z))
                        {
                            _tempChunkArrayFrontFaceZAxISPLUS[(rowIterateX) + width * (rowIterateY + height * (_z))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }


                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3((oneVertIndexX) * planeSize, (oneVertIndexY) * planeSize, (oneVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, (twoVertIndexY) * planeSize, (twoVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY) * planeSize, (threeVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3((fourVertIndexX) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))];


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
                        uv.Add(new Vector2(0.25f, 0.9375f));
                        uv.Add(new Vector2(0.1875f, 0.9375f));
                        uv.Add(new Vector2(0.25f, 1));
                        uv.Add(new Vector2(0.1875f, 1));
                    }*/


                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/

                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                }
            }
        }

        /*_mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }



    void buildBackFace4Wall(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //RIGHTFACE

        _block = _tempChunkArrayBackFaceZAxISPLUS[_x + width * (_y + height * _z)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x, _y, _z + 1))
            {
                for (int _xx = 0; _xx < _maxWidth; _xx++)
                {
                    rowIterateX = _x + _xx;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y - _yy;
                        //rowIterateX = _x + _xx;                

                        if (rowIterateY <= height && rowIterateY >= 0 && rowIterateX <= width) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = _z + 1;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, rowIterateY + 1, _z+1) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = _z + 1;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))    /***********************************************************/
                                {
                                    _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z + 1;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x, rowIterateY - 1, _z + 1))
                                            {
                                                _block = _tempChunkArrayBackFaceZAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (_z + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = _z + 1;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = _z + 1;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_vertVisual, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z + 1;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + -1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = rowIterateY + _yy + 1;
                                    threeVertIndexZ = _z + 1;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayBackFaceZAxISPLUS[(_x) + width * ((rowIterateY - 1) + height * (_z))]; //////////////////////////////////////////////////////////

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX - 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY - 1, _z))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayBackFaceZAxISPLUS[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z + 1;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z + 1))
                                            {
                                                _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ + 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = _z + 1;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = _z + 1;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z + 1;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z + 1;

                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, _z))
                        {
                            _tempChunkArrayBackFaceZAxISPLUS[(rowIterateX) + width * (rowIterateY + height * (_z))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }


                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3((oneVertIndexX) * planeSize, (oneVertIndexY) * planeSize, (oneVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, (twoVertIndexY) * planeSize, (twoVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY) * planeSize, (threeVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3((fourVertIndexX) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))];

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
                        uv.Add(new Vector2(0.25f, 0.9375f));
                        uv.Add(new Vector2(0.1875f, 0.9375f));
                        uv.Add(new Vector2(0.25f, 1));
                        uv.Add(new Vector2(0.1875f, 1));
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

        /*_mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
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
        //RIGHTFACE

        _block = _tempChunkArrayBackFace[_x + width * (_y + height * _z)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(_x, _y, _z + 1))
            {
                for (int _xx = 0; _xx < _maxWidth; _xx++)
                {
                    rowIterateX = _x + _xx;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y - _yy;
                        //rowIterateX = _x + _xx;                

                        if (rowIterateY <= height && rowIterateY >= 0 && rowIterateX <= width) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = _z + 1;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, rowIterateY + 1, _z+1) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = _z + 1;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))    /***********************************************************/
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z + 1;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x, rowIterateY - 1, _z + 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(_x) + width * ((rowIterateY - 1) + height * (_z + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = _z + 1;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = _z + 1;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_vertVisual, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z + 1;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + -1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = rowIterateY + _yy + 1;
                                    threeVertIndexZ = _z + 1;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(_x) + width * ((rowIterateY - 1) + height * (_z))]; //////////////////////////////////////////////////////////

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX - 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY - 1, _z))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayBackFace[(_x + 1) + width * ((rowIterateY - 1) + height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z + 1;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z + 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY - 1) + height * (_z + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ + 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = _z + 1;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = _z + 1;
                                            _maxHeight = _yy + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = _z + 1;
                                    _maxHeight = _yy + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z + 1;

                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                    uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3((oneVertIndexX) * planeSize, (oneVertIndexY) * planeSize, (oneVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3((twoVertIndexX) * planeSize, (twoVertIndexY) * planeSize, (twoVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3((threeVertIndexX) * planeSize, (threeVertIndexY) * planeSize, (threeVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3((fourVertIndexX) * planeSize, fourVertIndexY * planeSize, (fourVertIndexZ) * planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * (fourVertIndexZ))];

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
                        uv.Add(new Vector2(0.25f, 0.9375f));
                        uv.Add(new Vector2(0.1875f, 0.9375f));
                        uv.Add(new Vector2(0.25f, 1));
                        uv.Add(new Vector2(0.1875f, 1));
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

        /*_mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }

    public bool blockExistsInArray(int _x, int _y, int _z)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth))
        {
            return false;
        }
        /*else if (_chunkArray[_x + _width * (_y + _height * _z)]==0)
        {
            return false;
        }*/
        else
        {
            return true;
        }
        //return _chunkArray[_x + _width * (_y + _height * _z)] == 0;
    }




    int getChunkVertexByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
        {
            return _chunkVertexArray[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
        }
        return 0;
    }









































    public void DrawBottomFace(Vector3 start, Vector3 offset1, Vector3 offset2, int x, int y, int z)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        if (_chunkArray[x + width * (y + height * z)] == leftExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == backExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == rightExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == frontExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == leftInsideCornerExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == rightInsideCornerExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == backInsideCornerExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == frontInsideCornerExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == leftOutsideCornerExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == rightOutsideCornerExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == backOutsideCornerExtremity[x, y, z]
          || _chunkArray[x + width * (y + height * z)] == frontOutsideCornerExtremity[x, y, z])
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
        }

        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);

    }
    public void DrawTopFace(Vector3 start, Vector3 offset1, Vector3 offset2, int x, int y, int z)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        if (_chunkArray[x + width * (y + height * z)] == leftExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == backExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == rightExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == frontExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == leftInsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == rightInsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == backInsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == frontInsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == leftOutsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == rightOutsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == backOutsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == frontOutsideCornerExtremity[x, y, z])
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
        }


        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);

    }



    public void DrawBackFace(Vector3 start, Vector3 offset1, Vector3 offset2, int x, int y, int z)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        //uv.Add(new Vector2(0, 0));
        //uv.Add(new Vector2(1, 0));
        //uv.Add(new Vector2(0, 1));
        //uv.Add(new Vector2(1, 1));


        if (_chunkArray[x + width * (y + height * z)] == leftExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == backExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == rightExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == frontExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == leftInsideCornerExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == rightInsideCornerExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == backInsideCornerExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == frontInsideCornerExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == leftOutsideCornerExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == rightOutsideCornerExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == backOutsideCornerExtremity[x, y, z]
         || _chunkArray[x + width * (y + height * z)] == frontOutsideCornerExtremity[x, y, z])
        {
            uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
            uv.Add(new Vector2(0.0625f, 0.9375f));
            uv.Add(new Vector2(0, 0.875f));
            uv.Add(new Vector2(0.0625f, 0.875f));
        }
        else
        {
            uv.Add(new Vector2(0.25f, 0.9375f));
            uv.Add(new Vector2(0.1875f, 0.9375f));
            uv.Add(new Vector2(0.25f, 1));
            uv.Add(new Vector2(0.1875f, 1));
        }












        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);
    }


    public void DrawFrontFace(Vector3 start, Vector3 offset1, Vector3 offset2, int x, int y, int z)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        //uv.Add(new Vector2(0, 0));
        //uv.Add(new Vector2(1, 0));
        //uv.Add(new Vector2(0, 1));
        //uv.Add(new Vector2(1, 1));

        if (_chunkArray[x + width * (y + height * z)] == leftExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == backExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == rightExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == frontExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == leftInsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == rightInsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == backInsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == frontInsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == leftOutsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == rightOutsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == backOutsideCornerExtremity[x, y, z]
            || _chunkArray[x + width * (y + height * z)] == frontOutsideCornerExtremity[x, y, z])
        {
            uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
            uv.Add(new Vector2(0.0625f, 0.9375f));
            uv.Add(new Vector2(0, 0.875f));
            uv.Add(new Vector2(0.0625f, 0.875f));
        }
        else
        {
            uv.Add(new Vector2(0.25f, 0.9375f));
            uv.Add(new Vector2(0.1875f, 0.9375f));
            uv.Add(new Vector2(0.25f, 1));
            uv.Add(new Vector2(0.1875f, 1));
        }








        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);
    }

    public void DrawLeftFace(Vector3 start, Vector3 offset1, Vector3 offset2, int x, int y, int z)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        if (_chunkArray[x + width * (y + height * z)] == leftExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == backExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == rightExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == frontExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == leftInsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == rightInsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == backInsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == frontInsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == leftOutsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == rightOutsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == backOutsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == frontOutsideCornerExtremity[x, y, z])
        {
            uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
            uv.Add(new Vector2(0.0625f, 0.9375f));
            uv.Add(new Vector2(0, 0.875f));
            uv.Add(new Vector2(0.0625f, 0.875f));
        }
        else
        {
            uv.Add(new Vector2(0.1875f, 0.9375f));
            uv.Add(new Vector2(0.1875f, 1));
            uv.Add(new Vector2(0.25f, 0.9375f));
            uv.Add(new Vector2(0.25f, 1));
        }


        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);
    }



    public void DrawRightFace(Vector3 start, Vector3 offset1, Vector3 offset2, int x, int y, int z)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        if (_chunkArray[x + width * (y + height * z)] == leftExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == backExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == rightExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == frontExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == leftInsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == rightInsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == backInsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == frontInsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == leftOutsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == rightOutsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == backOutsideCornerExtremity[x, y, z]
             || _chunkArray[x + width * (y + height * z)] == frontOutsideCornerExtremity[x, y, z])
        {
            uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
            uv.Add(new Vector2(0.0625f, 0.9375f));
            uv.Add(new Vector2(0, 0.875f));
            uv.Add(new Vector2(0.0625f, 0.875f));
        }
        else
        {

            uv.Add(new Vector2(0.25f, 1));
            uv.Add(new Vector2(0.25f, 0.9375f));
            uv.Add(new Vector2(0.1875f, 1));
            uv.Add(new Vector2(0.1875f, 0.9375f));
        }


        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);
    }


    public bool IsTransparent(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= width)) return true;
        {

            return _chunkArray[x + width * (y + height * z)] == 0;// map[(int)x, (int)y, (int)z] == 0;
        }
    }





    public virtual int GetByte(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (y >= height) || (x >= width) || (z >= width))
        {
            return 0;
        }
        return _chunkArray[x + width * (y + height * z)];
    }

    public static OldFloorTiles GetChunk(float x, float y, float z)
    {
        var enumerator0 = chunkz.GetEnumerator();

        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;

            if ((x < tls0.Value.x) || y < tls0.Value.y || (z < tls0.Value.z) || (x >= (tls0.Value.x) + chunkWidth) || (y >= (tls0.Value.y) + chunkWidth) || (z >= tls0.Value.z + chunkWidth))
            {
                continue;
            }
            return tls0.Key;
        }
        return null;
    }


    public static OldFloorTiles GetChunkAt(float x, float y, float z)
    {
        var enumerator0 = chunkz.GetEnumerator();

        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;

            if ((x < tls0.Value.x) || y < tls0.Value.y || (z < tls0.Value.z) || (x >= (tls0.Value.x) + chunkWidth) || (y >= (tls0.Value.y) + chunkWidth) || (z >= tls0.Value.z + chunkWidth))
            {
                continue;
            }
            return tls0.Key;
        }
        return null;
    }













    /*public static OldFloorTiles GetChunk(float x, float y, float z)
    {
        var enumerator0 = chunkz.GetEnumerator();

        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;

            if ((new Vector3(x,y,z) != tls0.Value))
            {
                continue;
            }
            return tls0.Key;
        }
        return null;
    }*/






}









////////////////////////////////////////////////////FILING A CORNER OF A MAP
/*
 * void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = DateTime.Now.Ticks.ToString();
        }
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    if (x > 0 && x <= width -30 && z > 0 || z <= width - 30)
                    {
                        //map[x, y, z] = 1;
                        map[x, y, z] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? (byte)1 : (byte)0;
                    }
                    //else
                    //{
                        //map[x, y, z] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? (byte)1 : (byte)0;
                    //}
                }
            }
        }
    }*/



////////////////////////////////////ANOTHER STUPID CORNER/////////////////////////////////
/*
 *    if (transform.position.x <= -widthWorld && transform.position.z <= -widthWorld)
                {
                    float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 40);
                    float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 40);
                    float noiseZ = Mathf.Abs((float)(z * planeSize + transform.position.z + seed0) / 40);

                    float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                    noiseValue += (10f - (float)y) / 10;
                    noiseValue /= (float)y / 5;

                    if (x > 0 && x <= width - 10 && z > 0 && z <= width - 10 && noiseValue > 0.01f)
                    {
                        map[x, y, z] = 1;
                    }

                    else //if (noiseValue> 0.01f)
                    {
                        map[x, y, z] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? (byte)1 : (byte)0;
                    }
                }
                */




/*var plane = GetComponent<MeshFilter>();
                Texture2D t2 = new Texture2D(10, 10);

                float minColorRange0 = .05f;
                float minColorRange1 = .05f;
                float minColorRange2 = .05f;

                float maxColorRange0 = 0.100000f;
                float maxColorRange1 = 0.100000f;
                float maxColorRange2 = 0.100000f;

                float r0 = UnityEngine.Random.Range(minColorRange0, maxColorRange0);
                float g0 = UnityEngine.Random.Range(minColorRange1, maxColorRange1);
                float b0 = UnityEngine.Random.Range(minColorRange2, maxColorRange2);
                float a0 = 1f;

                t2.SetPixel(x, y, new Color(r0, g0, b0, a0));
                t2.Apply();
                plane.GetComponent<Renderer>().material.mainTexture = t2;*/






