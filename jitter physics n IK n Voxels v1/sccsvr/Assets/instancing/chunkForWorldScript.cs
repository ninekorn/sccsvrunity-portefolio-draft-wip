using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEditor;

public class chunkForWorldScript : MonoBehaviour
{

    public int _width = 30;
    public int _height = 30;
    public int _depth = 30;

    int[] _chunkArray;
    int[] _chunkVertexArray;
    int[] _testVertexArray;

    int _seed = 3425;//3425 //3420 //3441
    int _block;

    public int _vertexWidth = 0;
    public int _vertexHeight = 0;
    public int _vertexDepth = 0;

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

    private void Start()
    {
      
        _shader = _mat.shader;
        _vertexWidth = _width + 1;
        _vertexHeight = _height + 1;
        _vertexDepth = _depth + 1;
        _testVertexArray = new int[_vertexWidth * _vertexHeight * _vertexDepth];

        //_vertices = new Vector3[1];
        _testChunk = this.gameObject;
        //StartCoroutine(buildTerrain());
        buildingTerrain(new Vector3(0, 0, 0));
    }




    /*
    IEnumerator buildTerrain()
    {
        for (float xx = 0; xx < _width * 20 * _planeSize; xx += _width * _planeSize)
        {
            //for (int yy = 0; yy < _height * 10; yy += _height)
            {
                for (float zz = 0; zz < _depth * 20 * _planeSize; zz += _depth * _planeSize)
                {
                    _chunkPos = new Vector3(xx, 0, zz);
                    _testChunk = new GameObject();
                    _testChunk.transform.position = _chunkPos;

                    buildingTerrain(_chunkPos);
                    //StartCoroutine(buildingTerrain(_chunkPos));
                    yield return new WaitForSeconds(0.000001f);
                }
            }
        }
    }*/






    public void buildingTerrain(Vector3 _chunkPos) //unsafe
    {
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

        //_stopWatch.Stop();
        //_stopWatch.Reset();
        //_stopWatch.Start();

        _chunkArray = new int[_width * _height * _depth];
        _chunkVertexArray = new int[_vertexWidth * _vertexHeight * _vertexDepth];

        //fixed (byte* _array = _chunkArray)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    for (int z = 0; z < _depth; z++)
                    {                     
                        float temporaryY = 10f;
                        //float temporaryZ = 10f;
                        //float temporaryX = 10f;

                        temporaryY *= Mathf.PerlinNoise((x * _planeSize + _chunkPos.x + _seed) / _detailScale, (z * _planeSize + _chunkPos.z + _seed) / _detailScale) * _heightScale;
                        //temporaryX *= Mathf.PerlinNoise((y * _planeSize + _chunkPos.y + _seed) / _detailScale, (z * _planeSize + _chunkPos.z + _seed) / _detailScale) * _heightScale;
                        //temporaryZ *= Mathf.PerlinNoise((x * _planeSize + _chunkPos.x + _seed) / _detailScale, (y * _planeSize + _chunkPos.y + _seed) / _detailScale) * _heightScale;

                        float size0 = (1 / _planeSize) * _chunkPos.y;
                        temporaryY -= size0;

                        /*float size1 = (1 / _planeSize) * _chunkPos.x;
                        temporaryX -= size1;

                        float size2 = (1 / _planeSize) * _chunkPos.z;
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

                        //_array[x + _width * (y + _height * z)] = 1;

                        /*if (_chunkPos.y ==0)
                        {
                            _chunkArray[x + _width * (y + _height * z)] = 1;
                        }
                        else
                        {
                            if ((int)Math.Round(temporaryY) >= y)
                            {
                                _chunkArray[x + _width * (y + _height * z)] = 1;
                            }
                        }  */
                        if ((int)Math.Round(temporaryY) >= y)
                        {
                            _chunkArray[x + _width * (y + _height * z)] = 1;
                        }
                        else
                        {
                            _chunkArray[x + _width * (y + _height * z)] = 0;
                        }
                    }
                }
            }
        }

        _mesh = new Mesh();

        //_testChunk.AddComponent<MeshRenderer>();
        //_testChunk.AddComponent<MeshFilter>().mesh = _mesh;
        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        /*for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int z = 0; z < _depth; z++)
                {
                    _block = _chunkArray[x + _width * (y + _height * z)];
                    if (_block == 0) continue;
                    {
                        calculateVertex(x, y, z);
                    }
                }
            }
        }*/

        //_vertices = new Vector3[_vertexCounter];
        //Array.Resize(ref _vertices, _vertexCounter);
        //_triangles = new int[_vertexCounter * 6];


        for (int x = 0; x < _width; x++)
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
        }

        _mesh.vertices = _vertex.ToArray();
        _mesh.triangles = _trigz.ToArray();

        //_mesh.vertices = _vertices;
        //_mesh.triangles = _triangles;

        //_mesh.normals = _normals.ToArray();
        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;

        //AssetDatabase.CreateAsset(_mesh, "Assets/Resources/mesh.prefab");
        //AssetDatabase.AddObjectToAsset(_mesh, "Assets/Resources/mesh.prefab");
        //AssetDatabase.SaveAssets();

        //_testChunk.AddComponent<MeshCollider>();

        //_mat.SetVectorArray("_chunkArray", _toShaderArray);

        //_stopWatch.Stop();
        //int _milli = _stopWatch.Elapsed.Milliseconds;
        //UnityEngine.Debug.Log(_milli);
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
                _vertex.Add(new Vector3(_x * _planeSize + 1* _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize));
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
            }
        }

        //LEFTFACE
        if (IsTransparent(_x - 1, _y, _z))
        {
            if (getChunkVertexByte(_x, _y + 1, _z + 1) == 0)
            {
                _vertex.Add(new Vector3(_x * _planeSize, _y * _planeSize + 1 * _planeSize, _z * _planeSize + 1 * _planeSize));
                //_normals.Add(new Vector3(-1, 0, 0));
                //_uvs.Add(new Vector2(0f, 0f));
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y, _z), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z), Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y + 1, _z + 1),Quaternion.identity);
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
                //Instantiate(_sphereVisual, new Vector3(_x, _y, _z + 1), Quaternion.identity);
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
        }
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

