using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.ComponentModel;
using System;

//using SPINACH.iSCentralDispatch;
//using SPINACH;


public class _areaChunkData
{
    public _bigChunkData[] _arrayOfBigChunks;
    Vector3 _position;

    public _areaChunkData(_bigChunkData[] arrayOfBigChunks, Vector3 position)
    {
        this._arrayOfBigChunks = arrayOfBigChunks;
        this._position = position;
    }
}

public class _bigChunkData
{
    public _smallChunkData[] _arrayOfsmallChunks;
    Vector3 _position;

    public _bigChunkData(_smallChunkData[] arrayOfsmallChunks, Vector3 position)
    {
        this._arrayOfsmallChunks = arrayOfsmallChunks;
        this._position = position;
    }
}

public class _smallChunkData
{
    public chunkData[] _arrayOfIndividualChunks;
    Vector3 _position;
    public bool resultsOne;
    public bool resultsTwo;
    public bool resultsThree;
    public byte _byteCheck;


    public _smallChunkData(chunkData[] arrayOfIndividualChunks, Vector3 position)
    {
        this._arrayOfIndividualChunks = arrayOfIndividualChunks;
        this._position = position;
    }
}

public class SC_Terrain : MonoBehaviour
{
    public int _combineMax = 10;

    public int _mapWidth = 2;
    public int _mapHeight = 2;
    public int _mapDepth = 2;

    public float _planeSize = 0.1f;

    bool _stop = true;
    Stopwatch _stopWatch = new Stopwatch();

    Vector3 _chunkPos = new Vector3(0, 0, 0);
    GameObject _newChunk;

    

    public static int _indexInArrayOfChunks = 0;

    //MeshInstance[] combines;
    MeshFilter[] meshFilters;
    public Material _mat;

    int _indexOfChunk = 0;
    int _indexOfInstance = 0;

    chunkSCTerrain _currentChunk;
    MeshFilter _meshFilter;
    MeshRenderer _meshRenderer;
    GameObject _testChunk;

    public Transform _player;

    BackgroundWorker _backgroundWorker;
    static bool _killThread = false;
    int counter = 0;

    float _floatX = 0;
    float _floatY = 0;
    float _floatZ = 0;

    int _totalWidth = 0;
    int _totalHeight = 0;
    int _totalDepth = 0;

    public GameObject _regionChunkObject;
    public GameObject _areaChunkObject;
    public GameObject _bigChunkObject;
    public GameObject _smallChunkerObject;

    int _smallChunkWidth = 30;
    int _smallChunkHeight = 30;
    int _smallChunkDepth = 30;

    int _bigChunkWidth = 0;
    int _bigChunkHeight = 0;
    int _bigChunkDepth = 0;

    int _areaChunkWidth = 0;
    int _areaChunkHeight = 0;
    int _areaChunkDepth = 0;

    //Vector3 _bigChunkPos;
    //Vector3 _areaChunkPos;
    //_smallChunkData[] _bigChunkArrayOfData;
    //int[] _bigChunkArray;

    _bigChunkData[] _areaChunkArrayOfData;
    int[] _areaChunkArray;

    public static chunkData[] _arrayOfChunkData;
    public static int[] _arrayOfChunk;

    int _detailScale = 10;
    int _heightScale = 3;
    int _seed = 3425;//3425 //3420 //3441

    void Start()
    {      
        _bigChunkWidth = (_smallChunkWidth + _smallChunkWidth)* _mapWidth;
        _bigChunkHeight = (_smallChunkHeight + _smallChunkHeight) * _mapHeight;
        _bigChunkDepth = (_smallChunkDepth + _smallChunkDepth) * _mapDepth;

        _areaChunkWidth = (_bigChunkWidth + _bigChunkWidth) * _mapWidth;
        _areaChunkHeight = (_bigChunkHeight + _bigChunkHeight) * _mapHeight;
        _areaChunkDepth = (_bigChunkDepth + _bigChunkDepth) * _mapDepth;

        _totalWidth = _mapWidth + _mapWidth + _mapWidth;
        _totalHeight = _mapHeight + _mapHeight + _mapHeight;
        _totalDepth = _mapDepth + _mapDepth + _mapDepth;

        _arrayOfChunkData = new chunkData[_totalWidth * _totalHeight * _totalDepth];
        _arrayOfChunk = new int[_totalWidth * _totalHeight * _totalDepth];

        //_bigChunkArrayOfData = new _smallChunkData[_totalWidth * _totalHeight * _totalDepth];
        //_bigChunkArray = new int[_totalWidth * _totalHeight * _totalDepth];

        _areaChunkArrayOfData = new _bigChunkData[_totalWidth * _totalHeight * _totalDepth];
        _areaChunkArray = new int[_totalWidth * _totalHeight * _totalDepth];


        meshFilters = new MeshFilter[_combineMax];


        checkForAreaChunks();
        //checkForBigChunks();

        _backgroundWorker = new BackgroundWorker();
     
        _backgroundWorker.DoWork += (object sender, DoWorkEventArgs args) =>
        {
            for (int i = 0; i < 1; i++)
            {
                _threadLoop:

                if (_killThread)
                {
                    break;
                }
               
                //checkForAreaChunks();
                //checkForBigChunks();
                //checkForSmallChunk(_arrayOfChunkData, _arrayOfChunk, new Vector3(0,0,0), new Vector3(0, 0, 0));

                System.Threading.Thread.Sleep(1);
                goto _threadLoop;
              
            }
        };

        _backgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
        {
            //UnityEngine.Debug.Log("counter" +"_" +  counter);
        };

        _backgroundWorker.RunWorkerAsync();
    }
    Vector3 _currentPlayerPosition;

    _areaChunkData checkForAreaChunks()
    {
        var xxx = Mathf.RoundToInt(_playerPosition.x / (_areaChunkWidth * _planeSize));
        var yyy = Mathf.RoundToInt(_playerPosition.y / (_areaChunkHeight * _planeSize));
        var zzz = Mathf.RoundToInt(_playerPosition.z / (_areaChunkDepth * _planeSize));

        for (int xx = -_mapWidth; xx < _mapWidth * 2; xx++)
        {
            for (int yy = -_mapHeight; yy < _mapHeight * 2; yy++)
            {
                for (int zz = -_mapDepth; zz < _mapDepth * 2; zz++)
                {
                    if (_killThread)
                    {
                        break;
                    }

                    //_areaChunkPos = new Vector3(((xx+ xxx) * _bigChunkWidth) * _planeSize, ((yy+yyy) * _bigChunkHeight) * _planeSize, ((zz+zzz) * _bigChunkDepth) * _planeSize);
                    //_areaChunkPos = new Vector3(xx + _areaChunkWidth, yy + _areaChunkHeight, zz + _areaChunkDepth);
                    //_areaChunkPos = new Vector3((xx) * _areaChunkWidth * _planeSize , (yy) * _areaChunkHeight * _planeSize, (zz) * _areaChunkDepth * _planeSize);
                    //_areaChunkPos = (new Vector3(xx, yy, zz) * _areaChunkWidth * _planeSize);
                    Vector3 _areaChunkPos = (new Vector3(xx, yy, zz) * _areaChunkWidth * _planeSize);

                    if (Vector3.Distance(_areaChunkPos,_playerPosition) <= 75)
                    {
                        int _xValue = xx;
                        int _yValue = yy;
                        int _zValue = zz;

                        if (_xValue < 0)
                        {
                            _xValue += (_mapWidth * 3);
                        }
                        if (_yValue < 0)
                        {
                            _yValue += (_mapHeight * 3);
                        }
                        if (_zValue < 0)
                        {
                            _zValue += (_mapDepth * 3);
                        }

                        if (_areaChunkArray[_xValue + _totalWidth * (_yValue + _totalHeight * _zValue)] == 0)
                        {
                            Instantiate(_areaChunkObject, _areaChunkPos, Quaternion.identity);
                            _smallChunkData[] _bigChunkArrayOfData = new _smallChunkData[_totalWidth * _totalHeight * _totalDepth];
                            int[] _bigChunkArray = new int[_totalWidth * _totalHeight * _totalDepth];
                            _areaChunkArrayOfData[_xValue + _totalWidth * (_yValue + _totalHeight * _zValue)] = checkForBigChunks(_bigChunkArrayOfData, _bigChunkArray, _areaChunkPos);
                            _areaChunkArray[_xValue + _totalWidth * (_yValue + _totalHeight * _zValue)] = 1;
                        }
                    }                  
                }
            }
        }

        _areaChunkData _areaChunk = new _areaChunkData(_areaChunkArrayOfData, new Vector3(xxx, yyy, zzz));
        return _areaChunk;
    }

    _bigChunkData checkForBigChunks(_smallChunkData[] _bigChunkArrayOfData,int[] _bigChunkArray, Vector3 _areaChunkPos)
    {

        //var xxx = Mathf.RoundToInt(_playerPosition.x / ((_bigChunkWidth) * _planeSize));
        //var yyy = Mathf.RoundToInt(_playerPosition.y / ((_bigChunkHeight) * _planeSize));
        //var zzz = Mathf.RoundToInt(_playerPosition.z / ((_bigChunkDepth) * _planeSize));

        var areaX = Mathf.RoundToInt(_areaChunkPos.x);
        var areaY = Mathf.RoundToInt(_areaChunkPos.y);
        var areaZ = Mathf.RoundToInt(_areaChunkPos.z);
        //UnityEngine.Debug.Log(areaX + "_"+ areaY + "_"+ areaZ + "_");

        //UnityEngine.Debug.Log("POS: " + xxx + "_" + yyy + "_" + zzz);
        //if (Vector3.Distance(_areaChunkPos, _playerPosition) <= 10)
        {
            for (int xx = -_mapWidth; xx < _mapWidth * 2; xx++)
            {
                for (int yy = -_mapHeight; yy < _mapHeight * 2; yy++)
                {
                    for (int zz = -_mapDepth; zz < _mapDepth * 2; zz++)
                    {
                        if (_killThread)
                        {
                            break;
                        }

                        var _pos = _bigChunkWidth * _planeSize;
                        //UnityEngine.Debug.Log(_pos + "_");

                        Vector3 _bigChunkPos = ((new Vector3(xx, yy, zz))) + _areaChunkPos;

                        //_bigChunkPos = (new Vector3(xx+ _bigChunkWidth, yy+ _bigChunkWidth, zz+ _bigChunkWidth)) + _areaChunkPos;
                        //_bigChunkPos = new Vector3((xx) * _bigChunkWidth * _planeSize + (areaX), (yy) * _bigChunkHeight * _planeSize + (areaY), (zz) * _bigChunkDepth * _planeSize + (areaZ));
                        //_bigChunkPos = new Vector3(((xx + xxx) * _bigChunkWidth) * _planeSize, ((yy + yyy) * _bigChunkHeight) * _planeSize, ((zz + zzz) * _bigChunkDepth) * _planeSize);
                        if (Vector3.Distance(_bigChunkPos, _playerPosition) <= 24)
                        {
                            int _xValue = xx;
                            int _yValue = yy;
                            int _zValue = zz;

                            if (_xValue < 0)
                            {
                                _xValue += _mapWidth * 3;
                            }
                            if (_yValue < 0)
                            {
                                _yValue += _mapHeight * 3;
                            }
                            if (_zValue < 0)
                            {
                                _zValue += _mapDepth * 3;
                            }

                            if (_bigChunkArray[_xValue + _totalWidth * (_yValue + _totalHeight * _zValue)] == 0)
                            {
                                Instantiate(_bigChunkObject, _bigChunkPos, Quaternion.identity);
                                chunkData[] _arrayOfChunkData = new chunkData[_totalWidth * _totalHeight * _totalDepth];
                                int[] _arrayOfChunk = new int[_totalWidth * _totalHeight * _totalDepth];
                                //_smallChunkData _smallChunker = _bigChunkArrayOfData[_xValue + _totalWidth * (_yValue + _totalHeight * _zValue)];
                                //_bigChunkArrayOfData[_xValue + _totalWidth * (_yValue + _totalHeight * _zValue)] = checkForSmallChunk(_arrayOfChunkData, _arrayOfChunk, _bigChunkPos, _areaChunkPos); //_smallChunker
                                _bigChunkArray[_xValue + _totalWidth * (_yValue + _totalHeight * _zValue)] = 1;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
        }
        //UnityEngine.Debug.Log(areaX + "_"+ areaY + "_"+ areaZ + "_");

        _bigChunkData _bigChunk = new _bigChunkData(_bigChunkArrayOfData, new Vector3(areaX, areaY, areaZ));
        return _bigChunk;
    }

    //Queue<_bigChunkData> _dispatcherToMainThreadBigChunk = new Queue<_bigChunkData>();

    _smallChunkData[] _arrayChunkArrayOfDataTEMP;
    byte[] _arrayChunkArrayTEMP;

    _smallChunkData checkForSmallChunk(chunkData[] _arrayOfChunkData, int[] _arrayOfChunk, Vector3 _bigChunkPos, Vector3 _areaChunkPos) //, _smallChunkData _smallChunker
    {
        //Vector3 _currentPlayerPosition = _playerPosition;

        var xxx = Mathf.RoundToInt(_playerPosition.x / ((_smallChunkWidth) * _planeSize));
        var yyy = Mathf.RoundToInt(_playerPosition.y / ((_smallChunkHeight) * _planeSize));
        var zzz = Mathf.RoundToInt(_playerPosition.z / ((_smallChunkDepth) * _planeSize));

        /*var bigX = Mathf.RoundToInt(_bigChunkPos.x);
        var bigY = Mathf.RoundToInt(_bigChunkPos.y);
        var bigZ = Mathf.RoundToInt(_bigChunkPos.z);

        var areaX = Mathf.RoundToInt(_areaChunkPos.x);
        var areaY = Mathf.RoundToInt(_areaChunkPos.y);
        var areaZ = Mathf.RoundToInt(_areaChunkPos.z);*/

        //UnityEngine.Debug.Log("POS: " + bigX + "_" + bigY + "_" + bigZ);

        _arrayChunkArrayOfDataTEMP = new _smallChunkData[_totalWidth * _totalHeight * _totalDepth];
        _arrayChunkArrayTEMP = new byte[_totalWidth * _totalHeight * _totalDepth];

        for (int xx = -_mapWidth; xx < _mapWidth * 2; xx++)
        {
            for (int yy = -_mapHeight; yy < _mapHeight * 2; yy++)
            {
                for (int zz = -_mapDepth; zz < _mapDepth * 2; zz++)
                {
                    if (_killThread)
                    {
                        break;
                    }

                    int _xValue = xx;
                    int _yValue = yy;
                    int _zValue = zz;

                    if (_xValue < 0)
                    {
                        _xValue += _mapWidth * 3;
                    }
                    if (_yValue < 0)
                    {
                        _yValue += _mapHeight * 3;
                    }
                    if (_zValue < 0)
                    {
                        _zValue += _mapDepth * 3;
                    }


                    _chunkPos = (new Vector3(xx,yy,zz) *_smallChunkHeight * _planeSize) + (_bigChunkPos + _areaChunkPos);


                    //_chunkPos = new Vector3((xx) * _smallChunkWidth * _planeSize + (bigX), (yy) * _smallChunkHeight * _planeSize + (bigY + areaY), (zz) * _smallChunkDepth * _planeSize + (bigZ + areaZ));

                    if (Vector3.Distance(_chunkPos, _playerPosition) <= 15)
                    {
                        //UnityEngine.Debug.Log("Data: " + _xValue + "_" + _yValue + "_" + _zValue);
                        if (_arrayOfChunk[_xValue + _totalWidth * (_yValue + _totalHeight * _zValue)] == 0)
                        {
                            //Instantiate(_smallChunker, _chunkPos,Quaternion.identity);
                            chunkData _chunkData;
                            _currentChunk = new chunkSCTerrain(_chunkPos, out _chunkData);
                            _arrayOfChunkData[_xValue + _totalWidth * (_yValue + _totalHeight * _zValue)] = _chunkData;
                            _arrayOfChunk[_xValue + _totalWidth * (_yValue + _totalHeight * _zValue)] = 1;
                            counter++;
                            if (!_chunkData.resultsOne && !_chunkData.resultsTwo && !_chunkData.resultsThree && !_chunkData._trueForAll)////!_chunkData._trueForAll //!_chunkData.resultsOne && !_chunkData.resultsTwo && !_chunkData.resultsThree
                            {
                                //lock (_dummyObject)                    
                                //{
                                _dispatcherToMainThread.Enqueue(_chunkData);
                                //}
                            }
                            /*else
                            {
                                continue;
                            }*/


                            //System.Threading.Thread.Sleep(1);
                        }
                    }
                }
            }
        }
        _smallChunkData _smallChunk = new _smallChunkData(_arrayOfChunkData, new Vector3(xxx, yyy, zzz));
        return _smallChunk;
    }
    //UnityEngine.Debug.Log("Data: " + _xValue + "_" + _yValue + "_" + _zValue);


    Queue<chunkData> _dispatcherToMainThread = new Queue<chunkData>();
    object _dummyObject = new object();

    Vector3 _playerPosition;
    private void Update()
    {
        _playerPosition = _player.transform.position;

        if (_dispatcherToMainThread.Count > 0)
        {
            //InvokeRepeating("_spawnChunk", 0.001f, 0.001f);
            //_stopWatch.Stop();
            //_stopWatch.Reset();
            //_stopWatch.Start();

            _stop = true;
            var _newChunker = _dispatcherToMainThread.Dequeue();

            if (_newChunker != null)
            {
                _testChunk = new GameObject();
                _testChunk.transform.position = _newChunker._chunkPosition;

                _meshFilter = _testChunk.AddComponent<MeshFilter>();
                _meshRenderer = _testChunk.AddComponent<MeshRenderer>();

                _meshFilter.mesh = new Mesh();
                _meshFilter.mesh.vertices = _newChunker._chunkVertices.ToArray();
                _meshFilter.mesh.triangles = _newChunker._chunkTriangles.ToArray();

                //_meshFilter.mesh = _chunkData._chunkMesh;
                _meshRenderer.material = _mat;
                      



                //MeshOptimizeTool _tool = new MeshOptimizeTool();
                //_tool.StartSimplify(_meshFilter.mesh);
                //MeshOptimizeTool.Simplify(_meshFilter.mesh);


                //_arrayOfChunks[_indexInArrayOfChunks] = _testChunk.GetComponent<chunk>();
                //_indexInArrayOfChunks++;

                //float quality = 0.1f;
                //var meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
                //meshSimplifier.Initialize(_meshFilter.mesh);
                //meshSimplifier.SimplifyMesh(quality);
                //var destMesh = meshSimplifier.ToMesh();
                //_meshFilter.mesh = destMesh;

                //float quality = 0.5f;
                //var meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
                //meshSimplifier.Vertices = vertices;
                //meshSimplifier.AddSubMeshTriangles(indices);
                //meshSimplifier.SimplifyMesh(quality);
                //var newVertices = meshSimplifier.Vertices;
                //var newIndices = meshSimplifier.GetSubMeshTriangles(0);




                /*meshFilters[_indexOfInstance] = _testChunk.GetComponentInChildren<MeshFilter>();

                if (_indexOfInstance > _combineMax - 2)
                {
                    _newChunk = new GameObject();

                    CombineInstance[] combine = new CombineInstance[meshFilters.Length];

                    for (int m = 0; m < meshFilters.Length; m++)
                    {
                        combine[m].mesh = meshFilters[m].sharedMesh;
                        combine[m].transform = meshFilters[m].transform.localToWorldMatrix;
                        meshFilters[m].gameObject.active = false;
                    }

                    _meshRenderer = _newChunk.AddComponent<MeshRenderer>();

                    _newChunk.AddComponent<MeshFilter>().mesh = new Mesh();
                    _newChunk.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
                    _meshRenderer.material = _mat;


                    //float quality = 0.1f;
                    //var meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
                    //meshSimplifier.Initialize(_newChunk.GetComponent<MeshFilter>().mesh);
                    //meshSimplifier.SimplifyMesh(quality);
                    //var destMesh = meshSimplifier.ToMesh();
                    //_newChunk.GetComponent<MeshFilter>().mesh = destMesh;



                    _newChunk.active = true;
                    //UnityEngine.Debug.Log("VERTICES " + _newChunk.GetComponent<MeshFilter>().mesh.vertices.Length);
                    //_newChunk.AddComponent<MeshCollider>();

                    meshFilters = new MeshFilter[_combineMax];
                    _indexOfInstance = 0;

                    //_stopWatch.Stop();
                    //int _milli = _stopWatch.Elapsed.Milliseconds;
                    //UnityEngine.Debug.Log(_milli);

                    _stop = false;
                }
                if (_stop)
                {
                    _indexOfInstance++;
                }*/
            }
        }
        else
        {
            CancelInvoke("_spawnChunk");
        }
    }


    void _spawnChunk()
    {
        if (_dispatcherToMainThread.Count > 0)
        {
            _stop = true;
            var _newChunker = _dispatcherToMainThread.Dequeue();

            if (_newChunker != null)
            {
                _testChunk = new GameObject();
                _testChunk.transform.position = _newChunker._chunkPosition;

                _meshFilter = _testChunk.AddComponent<MeshFilter>();
                _meshRenderer = _testChunk.AddComponent<MeshRenderer>();

                _meshFilter.mesh = new Mesh();
                _meshFilter.mesh.vertices = _newChunker._chunkVertices.ToArray();
                _meshFilter.mesh.triangles = _newChunker._chunkTriangles.ToArray();

                //_meshFilter.mesh = _chunkData._chunkMesh;
                _meshRenderer.material = _mat;
            }
        }
    }











    /*int currentChunkCoordX = 0;
    int currentChunkCoordY = 0;
    int currentChunkCoordZ = 0;
    Vector3 currentPosition;
    bool isMoving = false;

    IEnumerator CheckMoving()
    {
        currentChunkCoordX = Mathf.RoundToInt(_player.transform.position.x / (_smallChunkWidth * 0.5f));
        currentChunkCoordY = Mathf.RoundToInt(_player.transform.position.y / (_smallChunkHeight * 0.5f));
        currentChunkCoordZ = Mathf.RoundToInt(_player.transform.position.z / (_smallChunkDepth * 0.5f));

        //float currentChunkCoordX = viewer.transform.position.x;
        //float currentChunkCoordY = viewer.transform.position.y ;
        //float currentChunkCoordZ = viewer.transform.position.z ;

        currentPosition = new Vector3(currentChunkCoordX, currentChunkCoordY, currentChunkCoordZ);

        Vector3 startPos = currentPosition;
        yield return new WaitForSeconds(0.001f);
        Vector3 finalPos = currentPosition;

        if (startPos.x != finalPos.x || startPos.y != finalPos.y
            || startPos.z != finalPos.z)
        {
            isMoving = true;
        }

        else if (startPos.x == finalPos.x && startPos.y == finalPos.y
             && startPos.z == finalPos.z)
        {
            isMoving = false;
        }
    }*/






    public class checkingBytes
    {
        public Vector3 smallChunkPos;

        public int smallX;
        public int smallY;
        public int smallZ;
        public Vector3 worldScenePos;
        public bool resultsOne;
        public bool resultsTwo;
        public bool resultsThree;
        public chunkData[] worldChunk;
        public int[] map;

        public checkingBytes(Vector3 smallChunkPos, int smallX, int smallY, int smallZ, bool resultsOne, bool resultsTwo, bool resultsThree, chunkData[] worldChunk, int[] map)
        {
            this.smallChunkPos = smallChunkPos;
            this.resultsOne = resultsOne;
            this.resultsTwo = resultsTwo;
            this.resultsThree = resultsThree;

            this.smallX = smallX;
            this.smallY = smallY;
            this.smallZ = smallZ;

            this.worldChunk = worldChunk;
            this.map = map;

        }
    }













    /*public bool IsTransparent(int _x, int _y, int _z)
    {
    if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= _width) || (_y >= _height) || (_z >= _depth)) return true;
    return _arrayOfChunks[_x + _width * (_y + _height * _z)] == 0;
    }*/

    bool getChunkByte(int _x, int _y, int _z)
    {

        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < (_totalWidth) && _y < (_totalHeight) && _z < (_totalDepth))
        {
            //UnityEngine.Debug.Log("test");
            return true;// _arrayOfChunks[_x + _totalWidth * (_y + _totalHeight * _z)];

        }
        return false;
    }
    //_width * _mapWidth * _height * _mapHeight * _depth * _mapDepth


    /*int getChunkVertexByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < _vertexWidth && _y < _vertexHeight && _z < _vertexDepth)
        {
            return _chunkVertexArray[_x + _vertexWidth * (_y + _vertexHeight * _z)];
        }
        return 0;
    }*/


    private void OnApplicationQuit()
    {
        _killThread = true;
        //_backgroundWorker.CancelAsync();
        Destroy(this);
    }
    private void OnDestroy()
    {
        //_backgroundWorker.CancelAsync();
        _killThread = true;
        Destroy(this);
    }
    private void OnDisable()
    {
        //_backgroundWorker.CancelAsync();
        _killThread = true;
        Destroy(this);
    }
}