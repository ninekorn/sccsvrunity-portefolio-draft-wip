using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEditor;

public class chunkSCTerrain
{
    int _combineMax = 10;

    int _width = 30;
    int _height = 30;
    int _depth = 30;

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

    int _vertexWidth = 0;
    int _vertexHeight = 0;
    int _vertexDepth = 0;

    Vector3[] _vertices;
    //Vector3[] _normals;
    //Vector2[] _uvs;
    int[] _triangles;

    List<int> _trigz;

    int _totalVertex = 0;

    int _vertexCounter = 0;

    int _detailScale = 10;
    int _heightScale = 3;

    Stopwatch _stopWatch = new Stopwatch();

    float _planeSize = 0.1f;
    GameObject _testChunk;
    Vector3 _chunkPos;

    List<Vector3> _vertex = new List<Vector3>();
    List<Vector3> _normals = new List<Vector3>();
    List<Vector2> _uvs = new List<Vector2>();

    List<Vector4> _toShaderArray = new List<Vector4>();

    Mesh _mesh;

    public GameObject _sphereVisual;

    public Shader _shader;
    public Material _mat;
    MeshRenderer _meshRend;

    int _index0 = 0;
    int _index1 = 0;
    int _index2 = 0;
    int _index3 = 0;

    int _newVertzCounter = 0;
    //MeshCombineUtility _meshCombineUtility;
    chunkData[] _arrayOfChunks;
    chunkData[] _arrayOfChunkDataCHUNK;
    int[] _arrayOfChunkCHUNK;

    public chunkSCTerrain(Vector3 _chunkPos, out chunkData _chunkData)
    {
        _vertexWidth = _width + 1;
        _vertexHeight = _height + 1;
        _vertexDepth = _depth + 1;
        _testVertexArray = new int[_vertexWidth * _vertexHeight * _vertexDepth];
        _chunkData = buildingTerrain(_chunkPos);
    }


    bool resultsOne = false;
    bool resultsTwo = false;
    bool resultsThree = false;

    float size0 = 0;
    float temporaryY = 0;
    chunkData _chunkData;

    chunkData buildingTerrain(Vector3 _chunkPos) //unsafe
    {
        _chunkData = new chunkData();
        //Vector3 _chunkPos = this.gameObject.transform.position;
        _index0 = 0;
        _index1 = 0;
        _index2 = 0;
        _index3 = 0;

        _vertex = new List<Vector3>();
        _trigz = new List<int>();
        //_normals = new List<Vector3>();
        //_uvs = new List<Vector2>();

        _totalVertex = 0;
        _newVertzCounter = 0;
        //_vertexCounter = 0;

        _stopWatch.Stop();
        _stopWatch.Reset();
        _stopWatch.Start();

        _tempChunkArrayBottomFace = new int[_width * _height * _depth];
        _tempChunkArrayBackFace = new int[_width * _height * _depth];
        _tempChunkArrayFrontFace = new int[_width * _height * _depth];
        _tempChunkArrayLeftFace = new int[_width * _height * _depth];
        _tempChunkArrayRightFace = new int[_width * _height * _depth];
        _tempChunkArray = new int[_width * _height * _depth];
        _chunkArray = new int[_width * _height * _depth];
        _chunkVertexArray = new int[_vertexWidth * _vertexHeight * _vertexDepth];
        _testVertexArray = new int[_vertexWidth * _vertexHeight * _vertexDepth];
        _perlinChunkArray = new int[_width * _height * _depth];

        //fixed (byte* _array = _chunkArray)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    for (int z = 0; z < _depth; z++)
                    {
                        temporaryY = 10f;
                        //float temporaryZ = 10f;
                        //float temporaryX = 10f;

                        temporaryY *= Mathf.PerlinNoise((x * _planeSize + _chunkPos.x + _seed) / _detailScale, (z * _planeSize + _chunkPos.z + _seed) / _detailScale) * _heightScale;
                        //temporaryX *= Mathf.PerlinNoise((y * _planeSize + _chunkPos.y + _seed) / _detailScale, (z * _planeSize + _chunkPos.z + _seed) / _detailScale) * _heightScale;
                        //temporaryZ *= Mathf.PerlinNoise((x * _planeSize + _chunkPos.x + _seed) / _detailScale, (y * _planeSize + _chunkPos.y + _seed) / _detailScale) * _heightScale;

                        size0 = (1 / _planeSize) * _chunkPos.y;
                        temporaryY -= size0;

                        //_chunkArray[x + _width * (y + _height * z)] = 1;

                        if ((int)Math.Round(temporaryY) >= y)
                        {
                            _chunkArray[x + _width * (y + _height * z)] = 1;
                            _tempChunkArray[x + _width * (y + _height * z)] = 1;
                            _tempChunkArrayRightFace[x + _width * (y + _height * z)] = 1;
                            _tempChunkArrayLeftFace[x + _width * (y + _height * z)] = 1;

                            _tempChunkArrayBottomFace[x + _width * (y + _height * z)] = 1;
                            _tempChunkArrayBackFace[x + _width * (y + _height * z)] = 1;
                            _tempChunkArrayFrontFace[x + _width * (y + _height * z)] = 1;
                        }
                        else
                        {
                            _chunkArray[x + _width * (y + _height * z)] = 0;
                            _tempChunkArray[x + _width * (y + _height * z)] = 0;
                            _tempChunkArrayRightFace[x + _width * (y + _height * z)] = 0;
                            _tempChunkArrayLeftFace[x + _width * (y + _height * z)] = 0;


                            _tempChunkArrayBottomFace[x + _width * (y + _height * z)] = 0;
                            _tempChunkArrayBackFace[x + _width * (y + _height * z)] = 0;
                            _tempChunkArrayFrontFace[x + _width * (y + _height * z)] = 0;

                        }

                        if (Math.Floor(temporaryY) >= y + 1)
                        {
                            _perlinChunkArray[x + _width * (y + _height * z)] = 2;
                        }

                        else if (Math.Floor(temporaryY) < y - 1)
                        {
                            _perlinChunkArray[x + _width * (y + _height * z)] = 1;
                        }
                        else
                        {
                            _perlinChunkArray[x + _width * (y + _height * z)] = 0;
                        }
                    }
                }
            }
        }

        resultsOne = Array.TrueForAll(_perlinChunkArray, s => s == 0);
        resultsTwo = Array.TrueForAll(_perlinChunkArray, s => s == 1);
        resultsThree = Array.TrueForAll(_perlinChunkArray, s => s == 2);

        /*for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int z = 0; z < _depth; z++)
                {
                    _block = _chunkArray[x + _width * (y + _height * z)];
                    if (_block == 0) continue;
                    {
                        buildVertex(x, y, z);
                        //buildVertexArray(x, y, z);
                    }
                }
            }
        }*/






        

        if (resultsOne || resultsTwo || resultsThree)////!_chunkData._trueForAll //!_chunkData.resultsOne && !_chunkData.resultsTwo && !_chunkData.resultsThree
        {
            _chunkData._chunkBlockArray = _chunkArray;
            _chunkData._chunkVerticesArray = _chunkVertexArray;

            _chunkData._chunkVertices = _vertex;
            _chunkData._chunkTriangles = _trigz;

            _chunkData._chunkPosition = _chunkPos;

            _chunkData._trueForAll = true;

            _chunkData.resultsOne = resultsOne;
            _chunkData.resultsTwo = resultsTwo;
            _chunkData.resultsThree = resultsThree;

            return _chunkData;
        }
        else
        {
            _buildFaces(_chunkPos);

            //_stopWatch.Stop();
            //_milli = _stopWatch.Elapsed.Milliseconds;
            //UnityEngine.Debug.Log(_milli);

            _chunkData._chunkBlockArray = _chunkArray;
            _chunkData._chunkVerticesArray = _chunkVertexArray;
            _chunkData._chunkVertices = _vertex;
            _chunkData._chunkTriangles = _trigz;
            _chunkData._chunkPosition = _chunkPos;
            _chunkData._trueForAll = false;
            _chunkData.resultsOne = resultsOne;
            _chunkData.resultsTwo = resultsTwo;
            _chunkData.resultsThree = resultsThree;

            return _chunkData;
        }
    }















    int _milli = 0;
    int _maxHeight = 0;

    void _buildFaces(Vector3 _chunkPos)
    {
        for (int y = _height - 1; y >= 0; y--)
        {
            for (int _x = 0; _x < _width; _x++)
            {
                for (int _z = 0; _z < _depth; _z++)
                {
                    _block = _chunkArray[_x + _width * (y + _height * _z)];
                    if (_block == 0) continue; //|| _block == 2
                    {
                        buildTopFace(_x, y, _z, _chunkPos);
                        buildTopRight(_x, y, _z, _chunkPos);
                        buildTopLeft(_x, y, _z, _chunkPos);
                        buildFrontFace(_x, y, _z, _chunkPos);
                        buildBackFace(_x, y, _z, _chunkPos);                     
                    }
                }
            }
        }
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

    //  UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWidth: " + _maxWidth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
    void buildTopFace(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = _width;
        _maxDepth = _depth;
        _maxHeight = _height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArray[_x + _width * (_y + _height * _z)];
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

                        if (rowIterateX <= _width && rowIterateZ <= _depth)
                        {
                            if (_xx == 0 && _zz == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = _y + 1;
                                oneVertIndexZ = rowIterateZ;
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArray[(rowIterateX + 1) + _width * ((_y) + _height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + _width * ((_y + 1) + _height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArray[(rowIterateX) + _width * ((_y) + _height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = _y + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArray[(rowIterateX) + _width * ((_y + 1) + _height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = _y + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = _y + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArray[(rowIterateX) + _width * ((_y) + _height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = _y + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArray[(rowIterateX) + _width * ((_y + 1) + _height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = _y + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = _y + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
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
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = _y + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArray[(rowIterateX + 1) + _width * ((_y) + _height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + _width * ((_y + 1) + _height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = _y + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_xx > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArray[(rowIterateX + 1) + _width * ((_y) + _height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + _width * ((_y + 1) + _height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArray[(rowIterateX) + _width * ((_y) + _height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, _y + 1, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArray[(rowIterateX) + _width * ((_y + 1) + _height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = _y + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, _y, rowIterateZ))
                                {
                                    _block = _tempChunkArray[(rowIterateX + 1) + _width * ((_y) + _height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = _y + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * _planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = _y + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, _y + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArray[(rowIterateX + 1) + _width * ((_y + 1) + _height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = _y + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;

                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = _y + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, _y, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = _y + 1;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, _y, rowIterateZ))
                        {
                            _tempChunkArray[(rowIterateX) + _width * (_y + _height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3(oneVertIndexX * _planeSize, oneVertIndexY * _planeSize, oneVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + _vertexWidth * ((oneVertIndexY) + _vertexHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + _vertexWidth * ((oneVertIndexY) + _vertexHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3(twoVertIndexX * _planeSize, twoVertIndexY * _planeSize, twoVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + _vertexWidth * ((twoVertIndexY) + _vertexHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + _vertexWidth * ((twoVertIndexY) + _vertexHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3(threeVertIndexX * _planeSize, threeVertIndexY * _planeSize, threeVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*_planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + _vertexWidth * ((threeVertIndexY) + _vertexHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + _vertexWidth * ((threeVertIndexY) + _vertexHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3(fourVertIndexX * _planeSize, fourVertIndexY * _planeSize, fourVertIndexZ * _planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + _vertexWidth * ((fourVertIndexY) + _vertexHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + _vertexWidth * ((fourVertIndexY) + _vertexHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + _vertexWidth * ((oneVertIndexY) + _vertexHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + _vertexWidth * ((twoVertIndexY) + _vertexHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + _vertexWidth * ((threeVertIndexY) + _vertexHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + _vertexWidth * ((fourVertIndexY) + _vertexHeight * fourVertIndexZ)];

                    _trigz.Add(_index0);
                    _trigz.Add(_index1);
                    _trigz.Add(_index2);
                    _trigz.Add(_index3);
                    _trigz.Add(_index2);
                    _trigz.Add(_index1);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = _vertex.ToArray();
        _mesh.triangles = _trigz.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
    }

    int rowIterateY = 0;

    void buildTopLeft(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = _width;
        _maxDepth = _depth;
        _maxHeight = _height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;

        _block = _tempChunkArrayLeftFace[_x + _width * (_y + _height * _z)];
        if (_block == 1) //|| _block == 2
        {
            //LEFTFACE
            if (IsTransparent(_x - 1, _y, _z))
            {
                for (int _zz = 0; _zz < _maxDepth; _zz++)
                {
                    rowIterateZ = _z + _zz;

                    //for (int _yy = _maxWidth - 1; _yy >= 0; _yy--)
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = _y - _yy;
                        //rowIterateX = _x + _xx;                

                        if (rowIterateY <= _height && rowIterateY >= 0 && rowIterateZ <= _depth) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = _x;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = rowIterateZ;
                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))/************************/
                                            {
                                                _block = _tempChunkArrayLeftFace[(_x - 1) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0) /****************************************/
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                }
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
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayLeftFace[(_x - 1) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x - 1, rowIterateY - 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayLeftFace[(_x - 1) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = _x;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        ////Instantiate(_sphereVisualOtherColorRed, new Vector3(_x, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                            ////Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    ////Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(_x) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(_x - 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayLeftFace[(_x - 1) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(_x, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayLeftFace[(_x) + _width * (rowIterateY + _height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + _chunkPos, Quaternion.identity);
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

                /*_vertex.Add(new Vector3(oneVertIndexX * _planeSize, oneVertIndexY * _planeSize, (oneVertIndexZ + 1) * _planeSize));
                _vertex.Add(new Vector3((twoVertIndexX) * _planeSize, twoVertIndexY * _planeSize, (twoVertIndexZ - 1) * _planeSize));
                _vertex.Add(new Vector3((threeVertIndexX) * _planeSize, (threeVertIndexY - 1) * _planeSize, (threeVertIndexZ) * _planeSize));
                _vertex.Add(new Vector3((fourVertIndexX - 1) * _planeSize, fourVertIndexY * _planeSize, (fourVertIndexZ - 1) * _planeSize));
                */

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((oneVertIndexX) * _planeSize, (oneVertIndexY) * _planeSize, (oneVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((twoVertIndexX) * _planeSize, (twoVertIndexY) * _planeSize, (twoVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((threeVertIndexX) * _planeSize, (threeVertIndexY) * _planeSize, (threeVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *_planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((fourVertIndexX) * _planeSize, fourVertIndexY * _planeSize, (fourVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))];


                    _trigz.Add(_index3);
                    _trigz.Add(_index2);
                    _trigz.Add(_index1);
                    _trigz.Add(_index0);
                    _trigz.Add(_index1);
                    _trigz.Add(_index2);
                }
            }
        }

        /*_mesh.vertices = _vertex.ToArray();
        _mesh.triangles = _trigz.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }

    void buildTopRight(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = _width;
        _maxDepth = _depth;
        _maxHeight = _height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //RIGHTFACE
        _block = _tempChunkArrayRightFace[_x + _width * (_y + _height * _z)];
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

                        if (rowIterateY <= _height && rowIterateY >= 0 && rowIterateZ <= _depth) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = _x + 1;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = rowIterateZ;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))/************************/
                                            {
                                                _block = _tempChunkArrayRightFace[(_x + 1) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = _x + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0) /****************************************/
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                }
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + 1, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayRightFace[(_x + 1) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = _x + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x + 1, rowIterateY - 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArrayRightFace[(_x + 1) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = _x + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(_x + 1, rowIterateY, rowIterateZ) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(_x, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(_x) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = _x + 1;
                                        threeVertIndexY = rowIterateY + _yy;
                                        threeVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(_x + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            _block = _tempChunkArrayRightFace[(_x + 1) + _width * ((rowIterateY) + _height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = _x + 1;
                                                threeVertIndexY = rowIterateY + _yy;
                                                threeVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(_x + 1, rowIterateY + _yy, rowIterateZ + 1) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(_x, rowIterateY - 1, rowIterateZ))
                                {
                                    if (rowIterateZ + 1 == threeVertIndexZ && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(_x, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayRightFace[(_x) + _width * (rowIterateY + _height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + _chunkPos, Quaternion.identity);
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

                /*_vertex.Add(new Vector3(oneVertIndexX * _planeSize, oneVertIndexY * _planeSize, (oneVertIndexZ + 1) * _planeSize));
                _vertex.Add(new Vector3((twoVertIndexX) * _planeSize, twoVertIndexY * _planeSize, (twoVertIndexZ - 1) * _planeSize));
                _vertex.Add(new Vector3((threeVertIndexX) * _planeSize, (threeVertIndexY - 1) * _planeSize, (threeVertIndexZ) * _planeSize));
                _vertex.Add(new Vector3((fourVertIndexX - 1) * _planeSize, fourVertIndexY * _planeSize, (fourVertIndexZ - 1) * _planeSize));
                */

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((oneVertIndexX) * _planeSize, (oneVertIndexY) * _planeSize, (oneVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((twoVertIndexX) * _planeSize, (twoVertIndexY) * _planeSize, (twoVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((threeVertIndexX) * _planeSize, (threeVertIndexY) * _planeSize, (threeVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *_planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((fourVertIndexX) * _planeSize, fourVertIndexY * _planeSize, (fourVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))];


                    _trigz.Add(_index1);
                    _trigz.Add(_index2);
                    _trigz.Add(_index3);
                    _trigz.Add(_index2);
                    _trigz.Add(_index1);
                    _trigz.Add(_index0);
                }
            }
        }

        /*_mesh.vertices = _vertex.ToArray();
        _mesh.triangles = _trigz.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }






    void buildFrontFace(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = _width;
        _maxDepth = _depth;
        _maxHeight = _height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //RIGHTFACE

        _block = _tempChunkArrayFrontFace[_x + _width * (_y + _height * _z)];
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

                        if (rowIterateY <= _height && rowIterateY >= 0 && rowIterateX <= _width) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = _z;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, rowIterateY + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z - 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + _width * ((rowIterateY - 1) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x, rowIterateY - 1, _z - 1))/************************/
                                            {
                                                _block = _tempChunkArrayFrontFace[(_x) + _width * ((rowIterateY - 1) + _height * (_z - 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = _z;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                            //Instantiate(_vertVisual, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(_x) + _width * ((rowIterateY - 1) + _height * (_z))]; //////////////////////////////////////////////////////////

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY - 1, _z))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayFrontFace[(_x + 1) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + _width * ((rowIterateY - 1) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z - 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX) + _width * ((rowIterateY - 1) + _height * (_z - 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ - 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = _z;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z - 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, _z))
                        {
                            _tempChunkArrayFrontFace[(rowIterateX) + _width * (rowIterateY + _height * (_z))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }


                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((oneVertIndexX) * _planeSize, (oneVertIndexY) * _planeSize, (oneVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((twoVertIndexX) * _planeSize, (twoVertIndexY) * _planeSize, (twoVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((threeVertIndexX) * _planeSize, (threeVertIndexY) * _planeSize, (threeVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *_planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((fourVertIndexX) * _planeSize, fourVertIndexY * _planeSize, (fourVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))];


                    _trigz.Add(_index1);
                    _trigz.Add(_index2);
                    _trigz.Add(_index3);
                    _trigz.Add(_index2);
                    _trigz.Add(_index1);
                    _trigz.Add(_index0);
                }
            }
        }

        /*_mesh.vertices = _vertex.ToArray();
        _mesh.triangles = _trigz.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }












    void buildBackFace(int _x, int _y, int _z, Vector3 chunkPos)
    {
        _maxWidth = _width;
        _maxDepth = _depth;
        _maxHeight = _height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //RIGHTFACE

        _block = _tempChunkArrayBackFace[_x + _width * (_y + _height * _z)];
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

                        if (rowIterateY <= _height && rowIterateY >= 0 && rowIterateX <= _width) // maybe add rowIterateY >= 0
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY + 1;
                                oneVertIndexZ = _z + 1;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, rowIterateY + 1, _z+1) * _planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))    /***********************************************************/
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + _width * ((rowIterateY - 1) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z + 1;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                        //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(_x, rowIterateY - 1, _z + 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(_x) + _width * ((rowIterateY - 1) + _height * (_z + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = _z + 1;
                                                    _maxHeight = _yy + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " _maxHeight: " + _maxHeight + " _maxDepth: " + _maxDepth + " rowIterateY: " + rowIterateY + " rowIterateZ: " + rowIterateZ);
                                                    //UnityEngine.Debug.Log("_yy: " + _yy + " _zz: " + _zz + " rowIterateZ: " + rowIterateZ + " rowIterateY: " + rowIterateY+ " threeVertIndexZ: " + threeVertIndexZ + " twoVertIndexY: " + twoVertIndexY);

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                            //Instantiate(_vertVisual, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + -1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                                //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(_x) + _width * ((rowIterateY - 1) + _height * (_z))]; //////////////////////////////////////////////////////////

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX - 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY - 1, _z))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayBackFace[(_x + 1) + _width * ((rowIterateY - 1) + _height * (rowIterateZ))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + _width * ((rowIterateY - 1) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = _z + 1;
                                        _maxHeight = _yy + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY - 1, _z + 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX) + _width * ((rowIterateY - 1) + _height * (_z + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    if (twoVertIndexZ + 1 == oneVertIndexZ && twoVertIndexX == oneVertIndexX)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = _z + 1;
                                                        _maxHeight = _yy + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);
                                                    }

                                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = rowIterateY;
                                                        fourVertIndexZ = threeVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                            //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                            if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = rowIterateY;
                                                fourVertIndexZ = threeVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                    //Instantiate(_sphereVisualOtherColorRed, new Vector3(rowIterateX, rowIterateY, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = rowIterateY + _yy + 1;
                                        threeVertIndexZ = _z + 1;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                        //if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = rowIterateY;
                                            fourVertIndexZ = threeVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, _z + 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX + 1) + _width * ((rowIterateY) + _height * (_z + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = rowIterateY + _yy + 1;
                                                threeVertIndexZ = _z + 1;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, rowIterateY + _yy + 1, _z) * _planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = rowIterateY;
                                                    fourVertIndexZ = threeVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
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
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, rowIterateY - 1, _z))
                                {
                                    if (rowIterateX + 1 == threeVertIndexX && rowIterateY == twoVertIndexY)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = rowIterateY;
                                        fourVertIndexZ = threeVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(threeVertIndexX, rowIterateY, threeVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, _z))
                        {
                            _tempChunkArrayBackFace[(rowIterateX) + _width * (rowIterateY + _height * (_z))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * _planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }


                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((oneVertIndexX) * _planeSize, (oneVertIndexY) * _planeSize, (oneVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))] = 1;
                    _testVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((twoVertIndexX) * _planeSize, (twoVertIndexY) * _planeSize, (twoVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))] = 1;
                    _testVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((threeVertIndexX) * _planeSize, (threeVertIndexY) * _planeSize, (threeVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ) *_planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))] = 1;
                    _testVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    _vertex.Add(new Vector3((fourVertIndexX) * _planeSize, fourVertIndexY * _planeSize, (fourVertIndexZ) * _planeSize));
                    //////Instantiate(_vertVisual, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * _planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))] = 1;
                    _testVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[(oneVertIndexX) + _vertexWidth * ((oneVertIndexY) + _vertexHeight * (oneVertIndexZ))];
                    _index1 = _testVertexArray[(twoVertIndexX) + _vertexWidth * ((twoVertIndexY) + _vertexHeight * (twoVertIndexZ))];
                    _index2 = _testVertexArray[(threeVertIndexX) + _vertexWidth * ((threeVertIndexY) + _vertexHeight * (threeVertIndexZ))];
                    _index3 = _testVertexArray[(fourVertIndexX) + _vertexWidth * ((fourVertIndexY) + _vertexHeight * (fourVertIndexZ))];


                    _trigz.Add(_index0);
                    _trigz.Add(_index1);
                    _trigz.Add(_index2);
                    _trigz.Add(_index3);
                    _trigz.Add(_index2);
                    _trigz.Add(_index1);
                }
            }
        }

        /*_mesh.vertices = _vertex.ToArray();
        _mesh.triangles = _trigz.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }
































    void buildVertex(int _x, int _y, int _z)
    {
        //TOPFACE
        if (IsTransparent(_x, _y + 1, _z))
        {
            if (getChunkVertexByte(_x, _y + 1, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[_x + _vertexWidth * ((_y + 1) + _vertexHeight * _z)] = 1;
                _testVertexArray[_x + _vertexWidth * ((_y + 1) + _vertexHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x + 1, _y + 1, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * _z)] = _newVertzCounter;
                //_vertices[_vertexCounter] = new Vector3(_x + 1, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * _z)] = 1;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x, _y + 1, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                _testVertexArray[_x + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z + 1);
                _chunkVertexArray[_x + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = 1;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(0, 1, 0));
                //_uvs.Add(new Vector2(1f, 1f));
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                //_vertices[_vertexCounter] = new Vector3(_x + 1, _y + 1, _z + 1);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = 1;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y + 1, _z) == 1 && getChunkVertexByte(_x + 1, _y + 1, _z) == 1 && getChunkVertexByte(_x, _y + 1, _z + 1) == 1 && getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 1)
            {
                _index0 = _testVertexArray[_x + _vertexWidth * ((_y + 1) + _vertexHeight * _z)];
                _index1 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * _z)];
                _index2 = _testVertexArray[_x + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))];
                _index3 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))];

                _trigz.Add(_index2);
                _trigz.Add(_index1);
                _trigz.Add(_index0);
                _trigz.Add(_index1);
                _trigz.Add(_index2);
                _trigz.Add(_index3);

                /*_mesh.vertices = _vertex.ToArray();
                _mesh.triangles = _trigz.ToArray();
                _testChunk.GetComponent<MeshFilter>().mesh = _mesh;
                _meshRend = _testChunk.GetComponent<MeshRenderer>();*/
            }
        }

        /*//LEFTFACE
        if (IsTransparent(_x - 1, _y, _z))
        {
            if (getChunkVertexByte(_x, _y + 1, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(-1, 0, 0));
                //_uvs.Add(new Vector2(0f, 0f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = 1;
                _testVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y + 1, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));
                //_normals.Add(new Vector3(-1, 0, 0));
                //_uvs.Add(new Vector2(0f, 0f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * _z)] = 1;
                _testVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(-1, 0, 0));
                //_uvs.Add(new Vector2(0f, 0f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))] = 1;
                _testVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize));
                //_normals.Add(new Vector3(-1, 0, 0));
                ////_uvs.Add(new Vector2(0f, 0f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * (_z))] = 1;
                _testVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * (_z))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y + 1, _z + 1) == 1 && getChunkVertexByte(_x, _y + 1, _z) == 1 && getChunkVertexByte(_x, _y, _z + 1) == 1 && getChunkVertexByte(_x, _y, _z) == 1)
            {
                _index0 = _testVertexArray[_x + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))];
                _index1 = _testVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * _z)];
                _index2 = _testVertexArray[_x + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))];
                _index3 = _testVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * (_z))];

                _trigz.Add(_index0);
                _trigz.Add(_index1);
                _trigz.Add(_index2);
                _trigz.Add(_index3);
                _trigz.Add(_index2);
                _trigz.Add(_index1);
            }
        }

        //RIGHTFACE
        if (IsTransparent(_x + 1, _y, _z))
        {
            if (getChunkVertexByte(_x + 1, _y, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize));
                //_normals.Add(new Vector3(+1, 0, 0));
                //_uvs.Add(new Vector2(1f, 0f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z))] = 1;
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y + 1, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));
                //_normals.Add(new Vector3(+1, 0, 0));
                //_uvs.Add(new Vector2(1f, 0f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * _z)] = 1;
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(+1, 0, 0));
                //_uvs.Add(new Vector2(1f, 0f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))] = 1;
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(+1, 0, 0));
                //_uvs.Add(new Vector2(0f, 1f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = 1;
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y, _z) == 1 && getChunkVertexByte(_x + 1, _y + 1, _z) == 1 && getChunkVertexByte(_x + 1, _y, _z + 1) == 1 && getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 1)
            {
                _index0 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z))];
                _index1 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * _z)];
                _index2 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))];
                _index3 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))];

                _trigz.Add(_index0);
                _trigz.Add(_index1);
                _trigz.Add(_index2);
                _trigz.Add(_index3);
                _trigz.Add(_index2);
                _trigz.Add(_index1);
            }
        }
  
        //FRONTFACE
        if (IsTransparent(_x , _y, _z-1))
        {
            if (getChunkVertexByte(_x + 1, _y, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize));

                //_normals.Add(new Vector3(0, 0, -1));
                //_uvs.Add(new Vector2(1f, 0f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z))] = 1;
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize));

                //_normals.Add(new Vector3(0, 0, -1));
                //_uvs.Add(new Vector2(1f, 1f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x ) + _vertexWidth * ((_y) + _vertexHeight * _z)] = 1;
                _testVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y+1, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));

                //_normals.Add(new Vector3(0, 0, -1));
                //_uvs.Add(new Vector2(1f, 0f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y+1) + _vertexHeight * (_z ))] = 1;
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y+1) + _vertexHeight * (_z ))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y + 1, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));

                //_normals.Add(new Vector3(0, 0, -1));
                //_uvs.Add(new Vector2(0f, 1f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z))] = 1;
                _testVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y, _z) == 1 && getChunkVertexByte(_x, _y, _z) == 1 && getChunkVertexByte(_x + 1, _y+1, _z) == 1 && getChunkVertexByte(_x, _y + 1, _z) == 1)
            {
                _index0 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z))];
                _index1 = _testVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * _z)];
                _index2 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z))];
                _index3 = _testVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z))];

                _trigz.Add(_index0);
                _trigz.Add(_index1);
                _trigz.Add(_index2);
                _trigz.Add(_index3);
                _trigz.Add(_index2);
                _trigz.Add(_index1);
            }
        }

        //BACKFACE
        if (IsTransparent(_x, _y, _z + 1))
        {
            if (getChunkVertexByte(_x, _y, _z+1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(0, 0, +1));
                //_uvs.Add(new Vector2(1f, 1f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x ) + _vertexWidth * ((_y) + _vertexHeight * (_z+1))] = 1;
                _testVertexArray[(_x ) + _vertexWidth * ((_y) + _vertexHeight * (_z+1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(0, 0, +1));
                //_uvs.Add(new Vector2(1f, 0f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))] = 1;
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y + 1, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(0, 0, +1));
                //_uvs.Add(new Vector2(1f, 1f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = 1;
                _testVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(0, 0, +1));
                //_uvs.Add(new Vector2(0f, 1f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = 1;
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x, _y, _z + 1) == 1 && getChunkVertexByte(_x + 1, _y, _z + 1) == 1 && getChunkVertexByte(_x, _y + 1, _z + 1) == 1 && getChunkVertexByte(_x + 1, _y + 1, _z + 1) == 1)
            {
                _index0 = _testVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))];
                _index1 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))];
                _index2 = _testVertexArray[(_x) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))];
                _index3 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y + 1) + _vertexHeight * (_z + 1))];

                _trigz.Add(_index0);
                _trigz.Add(_index1);
                _trigz.Add(_index2);
                _trigz.Add(_index3);
                _trigz.Add(_index2);
                _trigz.Add(_index1);
            }
        }

     
        //BOTTOMFACE
        if (IsTransparent(_x, _y-1, _z))
        {
            if (getChunkVertexByte(_x, _y, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize));
                //_normals.Add(new Vector3(0, -1, 0));
                //_uvs.Add(new Vector2(0f, 1f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * _z)] = 1;
                _testVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * _z)] = _newVertzCounter;
                _newVertzCounter++;
            }

            if (getChunkVertexByte(_x + 1, _y, _z) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize));
                //_normals.Add(new Vector3(0, -1, 0));
                ///_uvs.Add(new Vector2(0f, 1f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z))] = 1;
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z))] = _newVertzCounter;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x, _y, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(0, -1, 0));
                //_uvs.Add(new Vector2(0f, 1f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))] = 1;
                _testVertexArray[(_x) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x + 1, _y, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize + 1 * _planeSize, _y * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(0, -1, 0));
                //_uvs.Add(new Vector2(0f, 1f));
                //////Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
                //_vertices[_vertexCounter] = new Vector3(_x, _y + 1, _z);
                _chunkVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))] = 1;
                _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))] = _newVertzCounter;
                _newVertzCounter++;
            }
            if (getChunkVertexByte(_x, _y, _z) == 1 && getChunkVertexByte(_x + 1, _y, _z) == 1 && getChunkVertexByte(_x, _y, _z + 1) == 1 && getChunkVertexByte(_x + 1, _y, _z + 1) == 1)
            {
                _index0 = _testVertexArray[_x + _vertexWidth * ((_y) + _vertexHeight * _z)];
                _index1 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * _z)];
                _index2 = _testVertexArray[_x + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))];
                _index3 = _testVertexArray[(_x + 1) + _vertexWidth * ((_y) + _vertexHeight * (_z + 1))];

                _trigz.Add(_index0);
                _trigz.Add(_index1);
                _trigz.Add(_index2);
                _trigz.Add(_index3);
                _trigz.Add(_index2);
                _trigz.Add(_index1);
            }
        }*/



        //_meshRend.material = _mat;

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
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= _width) || (_y >= _height) || (_z >= _depth))
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

    public bool IsTransparent(int _x, int _y, int _z)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= _width) || (_y >= _height) || (_z >= _depth)) return true;
        return _chunkArray[_x + _width * (_y + _height * _z)] == 0;
    }

    int getChunkByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < _width && _y < _height && _z < _depth)
        {
            return _chunkArray[_x + _width * (_y + _height * _z)];
        }
        return 0;
    }


    int getTempArrayByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < _width && _y < _height && _z < _depth)
        {
            return _tempChunkArray[_x + _width * (_y + _height * _z)];
        }
        return 0;
    }



    int getChunkVertexByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < _vertexWidth && _y < _vertexHeight && _z < _vertexDepth)
        {
            return _chunkVertexArray[_x + _vertexWidth * (_y + _vertexHeight * _z)];
        }
        return 0;
    }


    /*byte ChunkVertexByteExists(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < _vertexWidth && _y < _vertexHeight && _z < _vertexDepth)
        {
            return _chunkVertexArray[_x + _vertexWidth * (_y + _vertexHeight * _z)];
        }
        return 0;
    }*/

    /*bool byteExists(int _x, int _y, int _z)
    {
        if (_x < 0 || _y < 0 || _z < 0 || _x > _width || _y > _height || _z > _depth) return false;
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
        if (_vertex == null)
        {
            return;
        }
        for (int i = 0; i < _vertex.Count; i++)
        {
            //Gizmos.color = Color.black;
            //Gizmos.DrawSphere(_vertex[i], 0.1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(_vertex[i], _normals[i]);
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

