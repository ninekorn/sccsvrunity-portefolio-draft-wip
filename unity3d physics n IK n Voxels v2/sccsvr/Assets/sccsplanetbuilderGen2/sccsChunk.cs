using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct sccsChunk
{
    public sccsChunkUpdate somemonobehaviorupdateattachedtothischunk;
    public Mesh mesh;

    public int activeBlockType;
    public float planeSize;
    public float realplanetwidth;
    public int width;
    public int height;
    public int depth;
    public Vector3 chunkPos;
    public sccsproceduralplanetbuilderGen2 componentParent;
    public int addfracturedcubeonimpact;
    NewObjectPoolerScript UnityTutorialGameObjectPool;

    public int[] map;
    public List<Vector3> vertexlist;
    public List<int> triangles;
    public int[] _chunkArray;
    public int[] _tempChunkArray;
    public int[] _tempChunkArrayRightFace;
    public int[] _tempChunkArrayLeftFace;
    public int[] _tempChunkArrayFrontFace;
    public int[] _tempChunkArrayBackFace;
    public int[] _tempChunkArrayBottomFace;
    public int[] _chunkVertexArray;
    public int[] _testVertexArray;

    int _index0;// = 0;
    int _index1;// = 0;
    int _index2;// = 0;
    int _index3;// = 0;
    int _newVertzCounter;// = 0;

    int oneVertIndexX;// = 0;
    int oneVertIndexY;// = 0;
    int oneVertIndexZ;// = 0;

    int twoVertIndexX;// = 0;
    int twoVertIndexY;// = 0;
    int twoVertIndexZ;// = 0;

    int threeVertIndexX;// = 0;
    int threeVertIndexY;//= 0;
    int threeVertIndexZ;// = 0;

    int fourVertIndexX;// = 0;
    int fourVertIndexY;// = 0;
    int fourVertIndexZ;// = 0;

    int _maxWidth;// = 0;
    int _maxHeight;
    int _maxDepth;// = 0;

    int rowIterateX;// = 0;
    int rowIterateY;
    int rowIterateZ;// = 0;

    bool foundVertOne;// = false;
    bool foundVertTwo;// = false;
    bool foundVertThree;// = false;
    bool foundVertFour;// = false;




    int total;
    int totalBytes;

    int vertexlistWidth;
    int vertexlistHeight;
    int vertexlistDepth;

    int counterCreateChunkObjectFaces;//  = 0;

    int t;//  = 0;
    int posx;//  = 0;
    int posy;//  = 0;
    int posz;//  = 0;
    int xx;// 
    int yy;// 
    int zz;// 
    int xi;// 
    int yi;// 
    int zi;// 

    int swtchx;// 
    int swtchy;// 
    public int swtchz;// 



    int counterCreateChunkObjectFacesBytes;// 
    int tBytes;// 
    int posxBytes;// 
    int posyBytes;// 
    int poszBytes;// 
    int xxBytes;// 
    int yyBytes;// 
    int zzBytes;// 
    int xiBytes;// 
    int yiBytes;// 
    int ziBytes;// 

    int swtchxBytes;// 
    int swtchyBytes;// 
    public int swtchzBytes;// 

    int rowIterateXBytes;// 
    int rowIterateZBytes;// 
    int rowIterateYBytes;// 
    //public int chunkbuildingswtc;
    public Transform planetchunk;
    public Material hitmaterial;

    public void sccsCustomStart(Transform planetchunk_, Vector3 chunkpos_, float planeSize_, float realplanetwidth_, int width_, int height_, int depth_, sccsproceduralplanetbuilderGen2 componentParent_, int addfracturedcubeonimpact_, NewObjectPoolerScript UnityTutorialGameObjectPool_)
    {

        activeBlockType = 0;

        planetchunk = planetchunk_;
        chunkPos = chunkpos_;
        planeSize = planeSize_;
        realplanetwidth = realplanetwidth_;
        width = width_;
        height = height_;
        depth = depth_;

        componentParent = componentParent_;
        addfracturedcubeonimpact = addfracturedcubeonimpact_;
        UnityTutorialGameObjectPool = UnityTutorialGameObjectPool_;

        // this.GameObject.position;

        /*
        this.gameObject.tag = "collisionObject";
        this.gameObject.layer = 8; //"collisionObject"
        UnityTutorialGameObjectPool = this.GameObject.GetComponent<NewObjectPoolerScript>();

        parentObject = this.GameObject.parent;
        //componentParent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilderGen2>().currentplanetbuilder;
        
        mesh = new Mesh();
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
        this.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        */

        total = width * height * depth;
        totalBytes = width * height * depth;

        vertexlistWidth = width + 1;
        vertexlistHeight = height + 1;
        vertexlistDepth = depth + 1;
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

        vertexlist = new List<Vector3>();
        triangles = new List<int>();

        realplanetwidth = planeSize * width;
    }

    public void sccsSetMap()
    {
        _tempChunkArrayBottomFace = new int[width * height * depth];
        _tempChunkArrayBackFace = new int[width * height * depth];
        _tempChunkArrayFrontFace = new int[width * height * depth];
        _tempChunkArrayLeftFace = new int[width * height * depth];
        _tempChunkArrayRightFace = new int[width * height * depth];
        _tempChunkArray = new int[width * height * depth];

        _chunkArray = new int[width * height * depth];

        _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
        _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

        vertexlist = new List<Vector3>();
        triangles = new List<int>();

        for (int t = 0; t < vertexlistWidth * vertexlistHeight * vertexlistDepth; t++) //total
        {
            if (t < total)
            {
                if (map[t] == 1)
                {
                    _chunkArray[t] = 1;

                    _tempChunkArray[t] = 1;
                    _tempChunkArrayRightFace[t] = 1;
                    _tempChunkArrayLeftFace[t] = 1;

                    _tempChunkArrayBottomFace[t] = 1;
                    _tempChunkArrayBackFace[t] = 1;
                    _tempChunkArrayFrontFace[t] = 1;
                }
                else
                {
                    _chunkArray[t] = 0;

                    _tempChunkArray[t] = 0;
                    _tempChunkArrayRightFace[t] = 0;
                    _tempChunkArrayLeftFace[t] = 0;

                    _tempChunkArrayBottomFace[t] = 0;
                    _tempChunkArrayBackFace[t] = 0;
                    _tempChunkArrayFrontFace[t] = 0;

                }
            }

            if (t < vertexlistWidth * vertexlistHeight * vertexlistDepth)
            {
                _chunkVertexArray[t] = 0;
                _testVertexArray[t] = 0;
            }
        }
    }

    public void Regenerate()
    {
        vertexlist.Clear();
        triangles.Clear();
        /*vertexlist.Clear();
        triangles.Clear();

        for (int t = 0; t < vertexlistWidth * vertexlistHeight * vertexlistDepth; t++) //total
        {
            if (t < total)
            {
                if (map[t] == 1)
                {
                    _chunkArray[t] = 1;

                    _tempChunkArray[t] = 1;
                    _tempChunkArrayRightFace[t] = 1;
                    _tempChunkArrayLeftFace[t] = 1;

                    _tempChunkArrayBottomFace[t] = 1;
                    _tempChunkArrayBackFace[t] = 1;
                    _tempChunkArrayFrontFace[t] = 1;
                }
                else
                {
                    _chunkArray[t] = 0;

                    _tempChunkArray[t] = 0;
                    _tempChunkArrayRightFace[t] = 0;
                    _tempChunkArrayLeftFace[t] = 0;

                    _tempChunkArrayBottomFace[t] = 0;
                    _tempChunkArrayBackFace[t] = 0;
                    _tempChunkArrayFrontFace[t] = 0;

                }
            }

            if (t < vertexlistWidth * vertexlistHeight * vertexlistDepth)
            {
                _chunkVertexArray[t] = 0;
                _testVertexArray[t] = 0;
            }
        }*/

        /*
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
        counterCreateChunkObjectFacesBytes = 0;*/
    }







    int _block;
    int index;


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


        if (swtchz == 1)
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
            _maxHeight = 0;
            _maxDepth = 0;

            rowIterateX = 0;
            rowIterateZ = 0;

            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;



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



            for (int t = 0; t < vertexlistWidth * vertexlistHeight * vertexlistDepth; t++) //total
            {
                if (t < total)
                {
                    if (map[t] == 1)
                    {
                        _chunkArray[t] = 1;

                        _tempChunkArray[t] = 1;
                        _tempChunkArrayRightFace[t] = 1;
                        _tempChunkArrayLeftFace[t] = 1;

                        _tempChunkArrayBottomFace[t] = 1;
                        _tempChunkArrayBackFace[t] = 1;
                        _tempChunkArrayFrontFace[t] = 1;
                    }
                    else
                    {
                        _chunkArray[t] = 0;

                        _tempChunkArray[t] = 0;
                        _tempChunkArrayRightFace[t] = 0;
                        _tempChunkArrayLeftFace[t] = 0;

                        _tempChunkArrayBottomFace[t] = 0;
                        _tempChunkArrayBackFace[t] = 0;
                        _tempChunkArrayFrontFace[t] = 0;

                    }
                }

                if (t < vertexlistWidth * vertexlistHeight * vertexlistDepth)
                {
                    _chunkVertexArray[t] = 0;
                    _testVertexArray[t] = 0;
                }
            }
        }
    }


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


                //var neighboorindexx = Mathf.FloorToInt((chunkPos.x / planeSize) / fractionOf); //4.654321/0.2 = 23.271605 => 23.271605/fractionOf = floor(2.3f)
                //var neighboorindexy = Mathf.FloorToInt((chunkPos.y / planeSize) / fractionOf);
                //var neighboorindexz = Mathf.FloorToInt((chunkPos.z / planeSize) / fractionOf);
                /*
                var somevalueforTopx = neighboorindexx;
                var somevalueforTopy = neighboorindexy + realplanetwidth;
                var somevalueforTopz = neighboorindexz;

                var somevalueforFrontx = neighboorindexx;
                var somevalueforFronty = neighboorindexy;
                var somevalueforFrontz = neighboorindexz + realplanetwidth;

                var somevalueforBackx = neighboorindexx;
                var somevalueforBacky = neighboorindexy;
                var somevalueforBackz = neighboorindexz - realplanetwidth;

                var somevalueforLeftx = neighboorindexx - realplanetwidth;
                var somevalueforLefty = neighboorindexy;
                var somevalueforLeftz = neighboorindexz;

                var somevalueforRightx = neighboorindexx + realplanetwidth;
                var somevalueforRighty = neighboorindexy;
                var somevalueforRightz = neighboorindexz;

                var somevalueforBottomx = neighboorindexx;
                var somevalueforBottomy = neighboorindexy - realplanetwidth;
                var somevalueforBottomz = neighboorindexz;
                */




                var fractionOf = realplanetwidth / planeSize;



                var somevalueforTopx = 0;
                var somevalueforTopy = 0;
                var somevalueforTopz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforTopx = Mathf.FloorToInt((chunkPos.x / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.x / realplanetwidth);
                }
                else
                {
                    somevalueforTopx = Mathf.FloorToInt((chunkPos.x / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.x / realplanetwidth);
                }

                if (chunkPos.y < 0)
                {
                    somevalueforTopy = Mathf.FloorToInt(((chunkPos.y + (planeSize * width)) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.y + (planeSize * width)) / realplanetwidth);
                }
                else
                {
                    somevalueforTopy = Mathf.FloorToInt(((chunkPos.y + (planeSize * width)) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.y + (planeSize * width)) / realplanetwidth);
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforTopz = Mathf.FloorToInt(((chunkPos.z ) / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.z / realplanetwidth);
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforTopz = Mathf.FloorToInt(((chunkPos.z ) / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.z / realplanetwidth);
                }

                var somevalueforBottomx = 0;
                var somevalueforBottomy = 0;
                var somevalueforBottomz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforBottomx = Mathf.FloorToInt(((chunkPos.x ) / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.x / realplanetwidth);
                }
                else
                {
                    somevalueforBottomx = Mathf.FloorToInt(((chunkPos.x ) / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.x / realplanetwidth);
                }

                if (chunkPos.y < 0)
                {
                    somevalueforBottomy = Mathf.FloorToInt(((chunkPos.y - (planeSize * width)) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.y - (planeSize * width)) / realplanetwidth);
                }
                else
                {
                    somevalueforBottomy = Mathf.FloorToInt(((chunkPos.y - (planeSize * width)) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.y - (planeSize * width)) / realplanetwidth);
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforBottomz = Mathf.FloorToInt(((chunkPos.z ) / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.z / realplanetwidth);
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforBottomz = Mathf.FloorToInt(((chunkPos.z ) / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.z / realplanetwidth);
                }


                var somevalueforRightx = 0;
                var somevalueforRighty = 0;
                var somevalueforRightz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforRightx = Mathf.FloorToInt(((chunkPos.x + (planeSize * width)) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.x + (planeSize * width)) / realplanetwidth);
                }
                else
                {
                    somevalueforRightx = Mathf.FloorToInt(((chunkPos.x + (planeSize * width)) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.x + (planeSize * width)) / realplanetwidth);
                }

                if (chunkPos.y < 0)
                {
                    somevalueforRighty = Mathf.FloorToInt(((chunkPos.y ) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.y) / realplanetwidth);
                }
                else
                {
                    somevalueforRighty = Mathf.FloorToInt(((chunkPos.y ) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.y) / realplanetwidth);
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforRightz = Mathf.FloorToInt(((chunkPos.z ) / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.z / realplanetwidth);
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforRightz = Mathf.FloorToInt(((chunkPos.z ) / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.z / realplanetwidth);
                }

                var somevalueforLeftx = 0;
                var somevalueforLefty = 0;
                var somevalueforLeftz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforLeftx = Mathf.FloorToInt(((chunkPos.x - (planeSize * width)) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.x - (planeSize * width)) / realplanetwidth);
                }
                else
                {
                    somevalueforLeftx = Mathf.FloorToInt(((chunkPos.x - (planeSize * width)) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.x - (planeSize * width)) / realplanetwidth);
                }

                if (chunkPos.y < 0)
                {
                    somevalueforLefty = Mathf.FloorToInt(((chunkPos.y ) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.y) / realplanetwidth);
                }
                else
                {
                    somevalueforLefty = Mathf.FloorToInt(((chunkPos.y ) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.y) / realplanetwidth);
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforLeftz = Mathf.FloorToInt(((chunkPos.z ) / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.z / realplanetwidth);
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforLeftz = Mathf.FloorToInt(((chunkPos.z ) / planeSize) / fractionOf); //Mathf.FloorToInt(chunkPos.z / realplanetwidth);
                }





                var somevalueforFrontx = 0;
                var somevalueforFronty = 0;
                var somevalueforFrontz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforFrontx = Mathf.FloorToInt(((chunkPos.x ) / planeSize) / fractionOf); // Mathf.FloorToInt((chunkPos.x) / realplanetwidth);
                }
                else
                {
                    somevalueforFrontx = Mathf.FloorToInt(((chunkPos.x ) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.x) / realplanetwidth);
                }

                if (chunkPos.y < 0)
                {
                    somevalueforFronty = Mathf.FloorToInt(((chunkPos.y ) / planeSize) / fractionOf); // Mathf.FloorToInt((chunkPos.y) / realplanetwidth);
                }
                else
                {
                    somevalueforFronty = Mathf.FloorToInt(((chunkPos.y ) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.y) / realplanetwidth);
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforFrontz = Mathf.FloorToInt(((chunkPos.z + (planeSize * width)) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.z + (planeSize * width)) / realplanetwidth);
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforFrontz = Mathf.FloorToInt(((chunkPos.z + (planeSize * width)) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.z + (planeSize * width)) / realplanetwidth);
                }





                var somevalueforBackx = 0;
                var somevalueforBacky = 0;
                var somevalueforBackz = 0;

                if (chunkPos.x < 0)
                {
                    somevalueforBackx = Mathf.FloorToInt(((chunkPos.x ) / planeSize) / fractionOf); // Mathf.FloorToInt((chunkPos.x) / realplanetwidth);
                }
                else
                {
                    somevalueforBackx = Mathf.FloorToInt(((chunkPos.x ) / planeSize) / fractionOf); // Mathf.FloorToInt((chunkPos.x) / realplanetwidth);
                }

                if (chunkPos.y < 0)
                {
                    somevalueforBacky = Mathf.FloorToInt(((chunkPos.y ) / planeSize) / fractionOf); //Mathf.FloorToInt((chunkPos.y) / realplanetwidth);
                }
                else
                {
                    somevalueforBacky = Mathf.FloorToInt(((chunkPos.y ) / planeSize) / fractionOf); // Mathf.FloorToInt((chunkPos.y) / realplanetwidth);
                    //posnot0roundedy -= 1;
                }

                if (chunkPos.z < 0)
                {
                    somevalueforBackz = Mathf.FloorToInt(((chunkPos.z - (planeSize * width)) / planeSize) / fractionOf); // Mathf.FloorToInt((chunkPos.z - (planeSize * width)) / realplanetwidth);
                    //posnot0roundedz += 1;
                }
                else
                {
                    somevalueforBackz = Mathf.FloorToInt(((chunkPos.z - (planeSize * width)) / planeSize) / fractionOf); // Mathf.FloorToInt((chunkPos.z - (planeSize * width)) / realplanetwidth);
                }










                /*
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
                }*/



                //BOTTOM FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi, yi - 1, zi))
                {
                    int someswtcBottom = 0;
                    
                    if (componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz) != null)
                    {
                        sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz);

                        if (yi == 0 && someChunk.map != null)
                        {
                            if (someChunk.map != null)
                            {
                                if (someChunk.IsTransparent(xi, height - 1, zi))
                                {
                                    someswtcBottom = 1;
                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                    //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
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
                            /*if (componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz) == null)
                            {
                                someswtcBottom = 1;
                            }*/
                            someswtcBottom = 1;
                        }
                    }
                    else
                    {
                        someswtcBottom = 1;
                    }
                    if (someswtcBottom == 1)
                    {
                        buildBottomFace();
                    }
                    //buildBottomFace();
                    //if (componentParent.getChunk(somevalueforBottomx, somevalueforBottomy, somevalueforBottomz) != null)
                    //{
                    //
                    //}
                }
















                //LEFT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi - 1, yi, zi))
                {
                    int someswtcLeft = 0;
                    


                    if (componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) != null)
                    {
                        sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz);

                        if (xi == 0 && someChunk.map != null)
                        {
                            if (someChunk.map != null)
                            {
                                if (someChunk.IsTransparent(width - 1, yi, zi))
                                {
                                    someswtcLeft = 1;
                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                    //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
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
                            /*if (componentParent.getChunk(somevalueforLeftx, somevalueforLefty, somevalueforLeftz) == null)
                            {
                                someswtcLeft = 1;
                            }*/
                            someswtcLeft = 1;
                        }
                    }
                    else
                    {
                        someswtcLeft = 1;
                    }

                    if (someswtcLeft == 1)
                    {
                        buildTopLeft();
                    }
                    //buildTopLeft();
                }




                //BACK FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi, yi, zi - 1))
                {

                    int someswtcBack = 0;

                    if (componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz) != null)
                    {
                        sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforBackx, somevalueforBacky, somevalueforBackz);

                        if (zi == 0 && someChunk.map != null)
                        {

                            if (someChunk.map != null)
                            {
                                if (someChunk.IsTransparent(xi, yi, depth - 1))
                                {
                                    someswtcBack = 1;
                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                    //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
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
                            //someswtcBack = 1;
                        }
                    }
                    else
                    {
                        someswtcBack = 1;
                    }

                    if (someswtcBack == 1)
                    {
                        buildBackFace();
                    }
                    //buildBackFace();
                }





                //TOP FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi, yi + 1, zi))
                {

                    int someswtcTop = 0;


                    if (componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) != null)
                    {

                        sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz);


                        if (yi == height - 1 && someChunk.map != null)
                        {
                            if (someChunk.map != null)
                            {
                                if (someChunk.IsTransparent(xi, 0, zi))
                                {
                                    someswtcTop = 1;

                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                    //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
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
                            /*if (componentParent.getChunk(somevalueforTopx, somevalueforTopy, somevalueforTopz) == null)
                            {
                                someswtcTop = 1;
                            }*/
                            someswtcTop = 1;
                        }

                    }
                    else
                    {
                        someswtcTop = 1;
                    }

                    if (someswtcTop == 1)
                    {
                        buildTopFace();
                    }
                    //buildTopFace();
                }

                //RIGHT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi + 1, yi, zi))
                {

                    int someswtcRight = 0;

                    if (componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) != null)
                    {

                        sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz);
                        if (xi == width - 1 && someChunk.map != null)
                        {
                            if (someChunk.map != null)
                            {
                                if (someChunk.IsTransparent(0, yi, zi))
                                {
                                    someswtcRight = 1;
                                }
                                else
                                {
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                    //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
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
                            /*if (componentParent.getChunk(somevalueforRightx, somevalueforRighty, somevalueforRightz) == null)
                            {
                                someswtcRight = 1;
                            }
                            else
                            {
                                someswtcRight = 1;
                            }*/
                            someswtcRight = 1;
                        }
                    }
                    else
                    {
                        someswtcRight = 1;
                    }

                    if (someswtcRight == 1)
                    {
                        buildTopRight();
                    }
                    //buildTopRight();
                }

                //FRONT FACE GENERATION WITH NEIGHBOOR CHECK. NEIGHBOOR CHECK BYTES NOT WORKING ENTIRELY.
                if (IsTransparent(xi, yi, zi + 1))
                {
                    int someswtcFront = 0;

                    if (componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) != null)
                    {

                        sccsChunk someChunk = (sccsChunk)componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz);

                        if (zi == depth - 1 && someChunk.map != null)
                        {

                            if (someChunk.map != null)
                            {
                                if (someChunk.IsTransparent(xi, yi, 0))
                                {

                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position + new Vector3(xi * planeSize, yi*planeSize,0), Quaternion.identity);
                                    //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;

                                    someswtcFront = 1;
                                }
                                else
                                {
                                    //someswtcFront = 1;
                                    //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                    //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
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
                            //if (componentParent.getChunk(somevalueforFrontx, somevalueforFronty, somevalueforFrontz) == null)
                            //{
                            //    someswtcFront = 1;
                            //}
                            someswtcFront = 1;
                        }
                    }
                    else
                    {
                        someswtcFront = 1;
                    }

                    if (someswtcFront == 1)
                    {
                        buildFrontFace();
                    }
                    //buildFrontFace();
                }

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
                            if (yi == 0 && (sccsChunk)componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y - realplanetwidth), Mathf.RoundToInt(chunkPos.z)) != null ||
                                yiBytes == 0 && (sccsChunk)componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y - realplanetwidth), Mathf.RoundToInt(chunkPos.z)) != null)
                            {
                                sccsChunk chunkdata = (sccsChunk)componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y - realplanetwidth), Mathf.RoundToInt(chunkPos.z));

                                if (chunkdata != null)
                                {
                                    var comp = chunkdata.planetchunk.GetComponent<sccslodchunkfinal>();

                                    if (comp.map != null)
                                    {
                                        if (comp.IsTransparent(rowIterateX, height - 1, rowIterateZ) || comp.IsTransparent(xiBytes, height - 1, ziBytes) || comp.IsTransparent(xi, height - 1, zi))
                                        {
                                            someswtc = 1;
                                        }
                                        else
                                        {
                                            //GameObject someObject = Instantiate(someVisualGameObject, chunkdata.planetchunk.GameObject.position, Quaternion.identity);
                                            //someObject.GameObject.parent = chunkdata.planetchunk.GameObject;
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
                                if ((sccsChunk)componentParent.getChunk(Mathf.RoundToInt(chunkPos.x), Mathf.RoundToInt(chunkPos.y - realplanetwidth), Mathf.RoundToInt(chunkPos.z)) == null)
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
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

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
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
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
                                                twoVertIndexY = yi + 1;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
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
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
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
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                        {
                            _tempChunkArray[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
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
                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

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
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
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
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi;
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
                                            twoVertIndexX = xi;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
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
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
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
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayLeftFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
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
                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

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
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
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
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
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
                                            twoVertIndexX = xi + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
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
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
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
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayRightFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
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
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

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
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
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
                                            twoVertIndexZ = zi + 1;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
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
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
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
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                        {
                            _tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
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
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

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
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
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
                                            twoVertIndexZ = zi;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
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
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
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
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
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
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                        {
                            _tempChunkArrayBackFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
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
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

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
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                        {
                            _tempChunkArrayBottomFace[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
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

    public void setAdjacentChunks(Vector3 pos, int indexx, int indexy, int indexz)
    {
        //int width = currentChunk.sccsChunk.width;
        //int height = currentChunk.sccsChunk.height;
        //int depth = currentChunk.sccsChunk.depth;

        //////Debug.Log("x: " + (indexx) + " y: " + (indexy) + " z: " + (indexz));

        int useonlyunitOneForNeighboorIndexPlease = 1;

        if (indexx == 0)
        {
            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z);

                if (adjacentChunk.map != null)
                {


                    if (adjacentChunk.GetByte((int)width - 1, (int)indexy, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)indexy, (int)indexz, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }

        if (indexx == width - 1)
        {
            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)indexy, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk right exists");
                        adjacentChunk.SetByte((int)0, (int)indexy, (int)indexz, activeBlockType, pos);
                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }

        if (indexy == 0)
        {
            if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)indexx, (int)height - 1, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)indexx, (int)height - 1, (int)indexz, activeBlockType, pos);
                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }

        if (indexy == height - 1)
        {
            if (componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)indexx, (int)0, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)indexx, (int)0, (int)indexz, activeBlockType, pos);
                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }

        if (indexz == 0)
        {
            if (componentParent.getChunk((int)pos.x, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)indexx, (int)indexy, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)indexx, (int)indexy, (int)depth - 1, activeBlockType, pos);
                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }

        if (indexz == depth - 1)
        {
            if (componentParent.getChunk((int)pos.x, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)indexx, (int)indexy, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)indexx, (int)indexy, (int)0, activeBlockType, pos);
                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }
















        //neighboorTiles
        if (indexx == 0 && indexy == 0 && indexz > 0 && indexz < depth - 1)
        {
            //already checked
            /*if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z);

                if (adjacentChunk.GetByte((int)width - 1, (int)indexy, (int)indexz) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.SetByte((int)width - 1, (int)indexy, (int)indexz, activeBlockType, pos);

                    adjacentChunk.sccsSetMap();
                    adjacentChunk.Regenerate();
                    //adjacentChunk.chunkbuildingswtc = 1;
                    adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                }
            }*/

            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)indexz, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            /*if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z);

                if (adjacentChunk.GetByte((int)indexx, (int)height - 1, (int)indexz) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.SetByte((int)indexx, (int)height - 1, (int)indexz, activeBlockType, pos);

                    adjacentChunk.sccsSetMap();
                    adjacentChunk.Regenerate();
                    //adjacentChunk.chunkbuildingswtc = 1;
                    adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                }
            }*/
        }
        if (indexx == 0 && indexy == 0 && indexz == 0)
        {
            /*if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x , (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z);

                if (adjacentChunk.GetByte((int)width-1, (int)height - 1, (int)depth-1) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                    adjacentChunk.sccsSetMap();
                    adjacentChunk.Regenerate();
                    //adjacentChunk.chunkbuildingswtc = 1;
                    adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                }
            }*/


            if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
            /*
            if (componentParent.getChunk((int)pos.x, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);

                if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                    adjacentChunk.sccsSetMap();
                    adjacentChunk.Regenerate();
                    //adjacentChunk.chunkbuildingswtc = 1;
                    adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                }
            }*/

            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }










        if (indexx == 0 && indexy == 0 && indexz == depth - 1)
        {
            /*if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z);

                if (adjacentChunk.GetByte((int)indexx, (int)height - 1, (int)0) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.SetByte((int)indexx, (int)height - 1, (int)0, activeBlockType, pos);

                    adjacentChunk.sccsSetMap();
                    adjacentChunk.Regenerate();
                    //adjacentChunk.chunkbuildingswtc = 1;
                    adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                }
            }*/

            if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
            /*
            if (componentParent.getChunk((int)pos.x, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);

                if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
                    adjacentChunk.sccsSetMap();
                    adjacentChunk.Regenerate();
                    //adjacentChunk.chunkbuildingswtc = 1;
                    adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                }
            }*/

            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }




        if (indexx == 0 && indexz == 0 && indexy > 0 && indexy < height - 1)
        {
            /*if (componentParent.getChunk((int)pos.x, (int)pos.y, (int)pos.z- useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);

                if (adjacentChunk.GetByte((int)width - 1, (int)indexz, (int)depth-1) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.SetByte((int)width - 1, (int)indexz, (int)depth - 1, activeBlockType, pos);

                    adjacentChunk.sccsSetMap();
                    adjacentChunk.Regenerate();
                    //adjacentChunk.chunkbuildingswtc = 1;
                    adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                }
            }*/

            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)indexy, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)indexy, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }
        /*if (indexx == 0 && indexz == 0 && indexy == 0)
        {

        }*/
        if (indexx == 0 && indexz == 0 && indexy == height - 1)
        {
            if (componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }

        if (indexz == 0 && indexy == 0 && indexx > 0 && indexx < width - 1)
        {

            if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)indexx, (int)height - 1, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)indexx, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

        }
        /*if (indexz == 0 && indexy == 0 && indexx == 0)
        {

        }*/
        if (indexz == 0 && indexy == 0 && indexx == width - 1)
        {
            if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }



            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }

        if (indexx == width - 1 && indexy == 0 && indexz > 0 && indexz < depth - 1)
        {
            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)height - 1, (int)indexz, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }
        /*if (indexx == width - 1 && indexy == 0 && indexz == 0)
        {

        }*/
        if (indexx == width - 1 && indexy == 0 && indexz == depth - 1)
        {
            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }


            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }

        if (indexx == 0 && indexz == depth - 1 && indexy > 0 && indexy < height - 1)
        {

            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)indexy, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)indexy, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }
        if (indexx == 0 && indexz == depth - 1 && indexy == 0)
        {
            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
            if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }




        }
        if (indexx == 0 && indexz == depth - 1 && indexy == height - 1)
        {
            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)0, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
            if (componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)0, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)0, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

        }
        if (indexz == 0 && indexy == height - 1 && indexx > 0 && indexx < width - 1)
        {
            if (componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)indexx, (int)0, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)indexx, (int)0, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }
        if (indexz == 0 && indexy == height - 1 && indexx == 0)
        {
            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
            if (componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
            if (componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x - useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)width - 1, (int)0, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }
        if (indexz == 0 && indexy == height - 1 && indexx == width - 1)
        {
            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)0, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
            if (componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)0, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z - useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)0, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }

        if (indexx == width - 1 && indexy == height - 1 && indexz > 0 && indexz < depth - 1)
        {
            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)0, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)0, (int)indexz, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }
        /*if (indexx == width - 1 && indexy == height - 1 && indexz == 0)
        {
            
        }*/
        if (indexx == width - 1 && indexy == height - 1 && indexz == depth - 1)
        {
            if (componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)0, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)0, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)0, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)0, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)0, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)0, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }


        if (indexx == width - 1 && indexz == depth - 1 && indexy > 0 && indexy < height - 1)
        {
            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)indexy, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)indexy, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }
        if (indexx == width - 1 && indexz == depth - 1 && indexy == 0)
        {
            if (componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }

            if (componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x + useonlyunitOneForNeighboorIndexPlease, (int)pos.y - useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)0, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }
        /*if (indexx == width - 1 && indexz == depth - 1 && indexy == height - 1)
        {

        }*/


        if (indexz == depth - 1 && indexy == height - 1 && indexx > 0 && indexx < width - 1)
        {
            if (componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease) != null)
            {
                sccsChunk adjacentChunk = (sccsChunk)componentParent.getChunk((int)pos.x, (int)pos.y + useonlyunitOneForNeighboorIndexPlease, (int)pos.z + useonlyunitOneForNeighboorIndexPlease);
                if (adjacentChunk.map != null)
                {

                    if (adjacentChunk.GetByte((int)indexx, (int)0, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.SetByte((int)indexx, (int)0, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        //adjacentChunk.chunkbuildingswtc = 1;
                        if (adjacentChunk.vertexlist.Count > 0)
                        {
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                            adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                            adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                        }
                    }
                }
            }
        }

        /*if (indexz == depth - 1 && indexy == height - 1 && indexx == 0)
        {

        }*/
        /*if (indexz == depth - 1 && indexy == height - 1 && indexx == width - 1)
        {

        }*/

        /*for (int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                for (int z = -1; z < 1; z++)
                {

                }
            }
        }*/
    }

    public bool IsTransparent(int _x, int _y, int _z)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth)) return true;
        return map[_x + width * (_y + height * _z)] == 0; //_chunkArray
    }

    int getChunkByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
        {
            return map[_x + width * (_y + height * _z)]; //_chunkArray
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

    public bool blockExistsInArray(int _x, int _y, int _z)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth))
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public void SetByte(int x, int y, int z, int block, Vector3 chunkbytepos_)
    {
        if (addfracturedcubeonimpact == 1)
        {
            //var unityTutorialObjectPool = this.GameObject.GetComponent<NewObjectPoolerScript>();
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

}
