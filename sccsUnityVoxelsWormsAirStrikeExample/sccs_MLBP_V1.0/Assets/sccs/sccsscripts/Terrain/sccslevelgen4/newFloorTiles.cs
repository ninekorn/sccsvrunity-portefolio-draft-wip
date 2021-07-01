using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimplexNoise;
using System.Linq;
using System;

[RequireComponent(typeof(MeshFilter))]
//[RequireComponent(typeof(MeshCollider))]


public class newFloorTiles : MonoBehaviour
{

    List<Vector3> currentVerts = new List<Vector3>();
    List<Vector3> middleTerrainHeight = new List<Vector3>();

    public static List<newFloorTiles> chunks = new List<newFloorTiles>();
    public static Dictionary<newFloorTiles, Vector3> chunkz = new Dictionary<newFloorTiles, Vector3>();

    public static int width = 10;
    public static int chunkWidth = 10;

    public float noiseDivider;
    public int height = 10;
    public byte[,,] map;

    public byte[,,] leftExtremity;
    public byte[,,] rightExtremity;
    public byte[,,] frontExtremity;
    public byte[,,] backExtremity;

    public byte[,,] leftInsideCornerExtremity;
    public byte[,,] rightInsideCornerExtremity;
    public byte[,,] frontInsideCornerExtremity;
    public byte[,,] backInsideCornerExtremity;

    public byte[,,] leftOutsideCornerExtremity;
    public byte[,,] rightOutsideCornerExtremity;
    public byte[,,] frontOutsideCornerExtremity;
    public byte[,,] backOutsideCornerExtremity;


    Mesh mesh;
    List<Vector3> verts = new List<Vector3>();

    List<int> tris = new List<int>();
    List<Vector2> uv = new List<Vector2>();

    List<int> triangles = new List<int>();
    List<Vector2> uvz = new List<Vector2>();

    MeshCollider meshCollider;

    //public string seed;
    public string seed;
    float seed0;


    public Dictionary<List<Vector3>, List<int>> TopFaceVerts = new Dictionary<List<Vector3>, List<int>>();

    float updateTime;

    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    //int[,] map;

    int neighbourWallTiles;
    public float planeSize = 1;

    public Texture rocks;
    public Material weed;


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
    public GameObject sphere;

    public float floorHeight;


    void Start()
    {



        leftWall = LevelGenerator4.currentLevelGen.leftWall;

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

        seed0 = 3420;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshCollider = GetComponent<MeshCollider>();
        GenerateMap();
        //Regenerate();
    }

    void GenerateMap()
    {
        map = new byte[width, height, width];

        leftExtremity = new byte[width, height, width];
        rightExtremity = new byte[width, height, width];
        frontExtremity = new byte[width, height, width];
        backExtremity = new byte[width, height, width];

        leftInsideCornerExtremity = new byte[width, height, width];
        rightInsideCornerExtremity = new byte[width, height, width];
        frontInsideCornerExtremity = new byte[width, height, width];
        backInsideCornerExtremity = new byte[width, height, width];

        leftOutsideCornerExtremity = new byte[width, height, width];
        rightOutsideCornerExtremity = new byte[width, height, width];
        frontOutsideCornerExtremity = new byte[width, height, width];
        backOutsideCornerExtremity = new byte[width, height, width];

        RandomFillMap();
    }


    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = DateTime.Now.Ticks.ToString();
        }

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
                        map[x, y, z] = 1;
                    }
                }
            }
        }


        ///////////////////////////////////// LEFT WALL/////////////////////////////

        for (int j = 0; j < leftWall.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == leftWall[j])
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                            float noiseValue1i = noiseValue2;

                            noiseValue1i += (5 - (float)x) / 5;
                            noiseValue1i /= (float)x / 5;


                            if (noiseValue1i > 0.2f)
                            {
                                map[x, y, z] = 1;
                                leftExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
            }
        }





        /////////////////////////////////////RIGHT WALL/////////////////////////////////

        for (int j = 0; j < LevelGenerator4.currentLevelGen.rightWall.Count; j++)
        {
            if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == LevelGenerator4.currentLevelGen.rightWall[j])
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

                            float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                            noiseValue += (10 - (float)y) / 10;
                            noiseValue /= (float)y / 5;

                            if (noiseValue > 0.2f && y < floorHeight)
                            {
                                map[x, y, z] = 1;
                            }

                            float noiseValue3i = noiseValue2;

                            noiseValue3i += (5 - (float)x) / 5;
                            noiseValue3i /= (float)x / 5;

                            if (noiseValue3i < 0.2f)
                            {
                                map[x, y, z] = 1;
                                rightExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue6i = noiseValue5;

                            noiseValue6i += (5 - (float)z) / 5;
                            noiseValue6i /= (float)z / 5;

                            if (noiseValue6i > 0.2f)
                            {
                                map[x, y, z] = 1;
                                frontExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue4i = noiseValue5;

                            noiseValue4i += (5 - (float)z) / 5;
                            noiseValue4i /= (float)z / 5;


                            if (noiseValue4i < 0.2f)
                            {
                                map[x, y, z] = 1;
                                backExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue2i = noiseValue2;
                            noiseValue2i += (5 - (float)x) / 5;
                            noiseValue2i /= (float)x / 5;

                            float noiseValue5i = noiseValue5;

                            noiseValue5i += (5 - (float)z) / 5;
                            noiseValue5i /= (float)z / 5;


                            if (noiseValue2i > 0.2f || noiseValue5i < 0.2f)
                            {
                                map[x, y, z] = 1;
                                leftInsideCornerExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue7i = noiseValue2;
                            noiseValue7i += (5 - (float)x) / 5;
                            noiseValue7i /= (float)x / 5;

                            float noiseValue8i = noiseValue5;
                            noiseValue8i += (5 - (float)z) / 5;
                            noiseValue8i /= (float)z / 5;

                            if (noiseValue7i < 0.2f || noiseValue8i < 0.2f)
                            {
                                map[x, y, z] = 1;
                                rightInsideCornerExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue9i = noiseValue2;

                            noiseValue9i += (5 - (float)x) / 5;
                            noiseValue9i /= (float)x / 5;

                            float noiseValue10i = noiseValue5;
                            noiseValue10i += (5 - (float)z) / 5;
                            noiseValue10i /= (float)z / 5;



                            if (noiseValue9i > 0.2f || noiseValue10i > 0.2f)
                            {
                                map[x, y, z] = 1;
                                backInsideCornerExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue11i = noiseValue5;
                            noiseValue11i += (5 - (float)z) / 5;
                            noiseValue11i /= (float)z / 5;

                            float noiseValue12i = noiseValue2;

                            noiseValue12i += (5 - (float)x) / 5;
                            noiseValue12i /= (float)x / 5;


                            if (noiseValue11i > 0.2f || noiseValue12i < 0.2f)
                            {
                                map[x, y, z] = 1;
                                frontInsideCornerExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue13i = noiseValue2;

                            noiseValue13i += (5 - (float)x) / 5;
                            noiseValue13i /= (float)x / 5;

                            float noiseValue14i = noiseValue5;

                            noiseValue14i += (5 - (float)z) / 5;
                            noiseValue14i /= (float)z / 5;


                            if (noiseValue13i > 0.2f && noiseValue14i < 0.2f)
                            {
                                map[x, y, z] = 1;
                                leftOutsideCornerExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue15i = noiseValue2;

                            noiseValue15i += (5 - (float)x) / 5;
                            noiseValue15i /= (float)x / 5;

                            float noiseValue16i = noiseValue5;

                            noiseValue16i += (5 - (float)z) / 5;
                            noiseValue16i /= (float)z / 5;


                            if (noiseValue15i < 0.2f && noiseValue16i < 0.2f)
                            {
                                map[x, y, z] = 1;
                                rightOutsideCornerExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue17i = noiseValue2;

                            noiseValue17i += (5 - (float)x) / 5;
                            noiseValue17i /= (float)x / 5;

                            float noiseValue18i = noiseValue5;

                            noiseValue18i += (5 - (float)z) / 5;
                            noiseValue18i /= (float)z / 5;

                            if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                            {
                                map[x, y, z] = 1;
                                backOutsideCornerExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }
                }
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
                                map[x, y, z] = 1;
                            }

                            float noiseValue19i = noiseValue5;
                            noiseValue19i += (5 - (float)z) / 5;
                            noiseValue19i /= (float)z / 5;

                            float noiseValue20i = noiseValue2;
                            noiseValue20i += (5 - (float)x) / 5;
                            noiseValue20i /= (float)x / 5;


                            if (noiseValue19i > 0.2f && noiseValue20i < 0.2f)
                            {
                                map[x, y, z] = 1;
                                frontOutsideCornerExtremity[x, y, z] = map[x, y, z];
                            }
                        }
                    }

                }
            }
        }
    }


    public void Regenerate()
    {
        verts.Clear();
        tris.Clear();
        uv.Clear();
        mesh.triangles = tris.ToArray();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    byte block = map[x, y, z];

                    if (block == 0) continue;
                    {
                        StartCoroutine(drawBrick(x, y, z, block));
                    }

                }
            }
        }
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uv.ToArray();

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
        //gameObject.GetComponent<MeshCollider>().enabled = false;

    }



    IEnumerator drawBrick(int x, int y, int z, byte block)
    {
        Vector3 start = new Vector3(x, y, z);
        Vector3 offset1, offset2;


        start.y *= tileHeight;

        ///TOPFACE
        if (isTransparent(x, y + 1, z))
        {
            offset1 = Vector3.right;
            offset2 = Vector3.back;
            DrawTopFace(new Vector3(start.x, start.y, start.z) + Vector3.up * tileHeight, offset1, offset2, x, y, z);
        }


        //LEFTFACE EXTREMITY
        if (x == 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == new Vector3(transform.position.x - chunkWidth, transform.position.y, transform.position.z))
                {
                    if (isTransparent(x - 1, y, z))
                    {
                        if (colliders[i].GetComponent<newFloorTiles>().GetByte(x + chunkWidth - 1, y, z) != 1)
                        {
                            //Instantiate(sphere, new Vector3(x, y, z) + transform.position, Quaternion.identity);
                            offset1 = Vector3.up * tileHeight;
                            offset2 = Vector3.back;
                            DrawLeftFace(start, offset1, offset2, x, y, z);
                        }
                    }
                }
            }
        }

        //LEFTFACE
        if (x > 0 && x <= chunkWidth - 1)
        {
            if (isTransparent(x - 1, y, z))
            {
                offset1 = Vector3.up * tileHeight;
                offset2 = Vector3.back;
                DrawLeftFace(start, offset1, offset2, x, y, z);
            }
        }





        //RIGHTFACE EXTREMITY

        if (x == chunkWidth - 1)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == new Vector3(transform.position.x + chunkWidth, transform.position.y, transform.position.z))
                {

                    //Instantiate(sphere, colliders[i].transform.position, Quaternion.identity);
                    if (isTransparent(x + 1, y, z))
                    {
                        if (colliders[i].GetComponent<newFloorTiles>().GetByte(x - chunkWidth + 1, y, z) != 1)
                        {
                            offset1 = Vector3.down * tileHeight;
                            offset2 = Vector3.back;
                            DrawRightFace(start + Vector3.right + Vector3.up * tileHeight, offset1, offset2, x, y, z);

                        }
                    }
                }
            }
        }

        //RIGHTFACE
        if (x >= 0 && x < width - 1)
        {
            if (isTransparent(x + 1, y, z))
            {
                offset1 = Vector3.down * tileHeight;
                offset2 = Vector3.back;
                DrawRightFace(start + Vector3.right + Vector3.up * tileHeight, offset1, offset2, x, y, z);
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
                    if (isTransparent(x, y, z + 1))
                    {
                        if (colliders[i].GetComponent<newFloorTiles>().GetByte(x, y, z - chunkWidth + 1) != 1)
                        {
                            offset1 = Vector3.right;
                            offset2 = Vector3.up * tileHeight;
                            DrawBackFace(start, offset1, offset2, x, y, z);

                        }
                    }
                }
            }
        }
        //BACKFACE
        if (z >= 0 && z < chunkWidth - 1)
        {
            if (isTransparent(x, y, z + 1))
            {
                offset1 = Vector3.right;
                offset2 = Vector3.up * tileHeight;
                DrawBackFace(start, offset1, offset2, x, y, z);
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
                    if (isTransparent(x, y, z - 1))
                    {
                        if (colliders[i].GetComponent<newFloorTiles>().GetByte(x, y, z + chunkWidth - 1) != 1)
                        {
                            offset1 = Vector3.left;
                            offset2 = Vector3.up * tileHeight;
                            DrawFrontFace(start + Vector3.right + Vector3.back, offset1, offset2, x, y, z);

                        }
                    }
                }
            }
        }

        //FRONTFACE
        if (z > 0 && z <= chunkWidth - 1)
        {
            if (isTransparent(x, y, z - 1))
            {
                offset1 = Vector3.left;
                offset2 = Vector3.up * tileHeight;
                DrawFrontFace(start + Vector3.right + Vector3.back, offset1, offset2, x, y, z);
            }
        }


        //BOTTOM FACE EXTREMITY

        if (z == 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.position == new Vector3(transform.position.x, transform.position.y - chunkWidth, transform.position.z))
                {
                    //Instantiate(sphere, colliders[i].transform.position, Quaternion.identity);
                    if (isTransparent(x, y - 1, z))
                    {
                        if (colliders[i].GetComponent<newFloorTiles>().GetByte(x, y - chunkWidth + 1, z) != 1)
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
            if (isTransparent(x, y - 1, z))
            {
                offset1 = Vector3.back;//(new Vector3(1, 0, 0));
                offset2 = Vector3.right;//(new Vector3(0, 0, 1));
                DrawBottomFace(start + Vector3.down + Vector3.up, offset1, offset2, x, y, z);
            }
        }

        yield return new WaitForSeconds(0.5f);
    }




    public void DrawBottomFace(Vector3 start, Vector3 offset1, Vector3 offset2, int x, int y, int z)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        if (map[x, y, z] == leftExtremity[x, y, z]
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

        if (map[x, y, z] == leftExtremity[x, y, z]
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

        /*uv.Add(new Vector2(0, 0));
        uv.Add(new Vector2(1, 0));
        uv.Add(new Vector2(0, 1));
        uv.Add(new Vector2(1, 1));*/


        if (map[x, y, z] == leftExtremity[x, y, z]
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

        /*uv.Add(new Vector2(0, 0));
        uv.Add(new Vector2(1, 0));
        uv.Add(new Vector2(0, 1));
        uv.Add(new Vector2(1, 1));*/

        if (map[x, y, z] == leftExtremity[x, y, z]
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

        if (map[x, y, z] == leftExtremity[x, y, z]
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

        if (map[x, y, z] == leftExtremity[x, y, z]
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
        }


        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);
    }






    public bool isTransparent(float x, float y, float z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= width))
            return true;
        {

            return map[(int)x, (int)y, (int)z] == 0;
        }
    }




    public virtual byte GetByte(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (y >= height) || (x >= width) || (z >= width))
        {
            return 0;
        }
        return map[x, y, z];
    }





    public static newFloorTiles GetChunk(float x, float y, float z)
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


    public static newFloorTiles GetChunkAt(float x, float y, float z)
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













    /*public static newFloorTiles GetChunk(float x, float y, float z)
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







