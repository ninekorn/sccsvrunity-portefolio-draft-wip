using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Text;
using System.Net;
using System.Xml;
using UnityEngine;
using SPINACH.iSCentralDispatch;

namespace OculusProjektV0
{
    public class threadInfinite : MonoBehaviour
    {
        static int countdownTimer = 10;
        static bool switcher = false;
        public static int chunkWidth = 10;

        public static float planeSize = 0.1f;
        static bool endingTimer = false;

        float xPosition;
        float yPosition;
        float zPosition;

        float xPose;
        float yPose;
        float zPose;

        float areaPosX;
        float areaPosY;
        float areaPosZ;

        static smallChunk[,,] chunkSmall;
        public bigChunk[,,] chunkArear;

        bool starter = false;
        bool isMoving = false;

        Vector3 positionZero = new Vector3(0, 0, 0);
        public bool initializeArray = false;
        public byte[,,] mapping;

        private int width = 10;
        private int height = 1;
        private int depth = 10;

        Vector3 position;

        Queue<meshThreadInfo<meshData>> meshDataThreadInfoQueue = new Queue<meshThreadInfo<meshData>>();

        Queue<chunkPosThreadInfo<chunkPosition>> chunkPosDataThreadInfoQueue = new Queue<chunkPosThreadInfo<chunkPosition>>();




        public GameObject chunker;
        public GameObject plane;
        private Texture mat;
        int threadID0;
        int threadID1;
        int threadID2;
        int threadID3;

        bool startBuild = false;

        Queue<meshThreadInfo<meshData>> MyQueue = new Queue<meshThreadInfo<meshData>>();
        bool startThread00 = false;
        bool startThread01 = false;
        bool startThread02 = false;
        bool startThread03 = false;
        bool startThread09 = false;

        meshThreadInfo<meshData> threadInfo;

        static Queue q = Queue.Synchronized(new Queue());
        static bool running0 = true;
        static bool running1 = true;
        static bool running2 = true;
        static bool running3 = true;
        static bool running9 = true;


        bool startJob = false;

        public void Update()
        {
            try
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    //mainer();

                    startJob = true;
                }
                if (startJob == true)
                {
                    mining();
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Error: ");
                return;
            }
        }

        bool getMeshData = false;

        void mining()
        {
            if (startThread00 == false)
            {
                threadID0 = iSCentralDispatch.DispatchNewThread(() =>
                {
                    while (running0)
                    {
                        if (getMeshData == false)
                        {
                            requestMapData0(onMeshDataReceived0);
                            getMeshData = true;
                        }

                        if (meshDataThreadInfoQueue.Count > 0)
                        {
                            lock (meshDataThreadInfoQueue)
                            {
                                meshThreadInfo<meshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
                                threadInfo.callback(threadInfo.parameter);
                                ProcessQueue0(threadInfo.parameter);
                                running0 = false;
                            }
                        }
                        else
                        {
                            Thread.Sleep(1);
                        }
                        running0 = true;
                    }
                });
                startThread00 = true;
            }
        }

        void ProcessQueue0(meshData meshdata)
        {
            threaderJobber0(meshdata);
        }

        void onMeshDataReceived0(meshData meshdata)
        {

        }

        void threaderJobber0(meshData meshdata)
        {
            if (meshdata.lengthOfArray > 0)
            {
                GameObject currentChunk = (GameObject)iSCentralDispatch.Instantiate(chunker, new Vector3(meshdata.currentPos.x, meshdata.currentPos.y, meshdata.currentPos.z), Quaternion.identity);

                iSCentralDispatch.DispatchMainThread(() =>
                {
                    Mesh mesh = currentChunk.GetComponent<MeshFilter>().mesh;
                    string texture = "Assets/Resources/Textures/green";
                    mat = Resources.Load(texture, typeof(Texture)) as Texture;
                    currentChunk.GetComponent<MeshRenderer>().material.mainTexture = mat;

                    mesh.vertices = meshdata.positions.ToArray();
                    mesh.triangles = meshdata.triangleIndices.ToArray();
                    mesh.RecalculateNormals();
                    currentChunk.AddComponent<MeshCollider>();
                    currentChunk.GetComponent<MeshCollider>().sharedMesh = mesh;
                });
            }
            startThread00 = false;
            running0 = true;
        }


        void requestMapData0(Action<meshData> callback)
        {
            meshDataThread(callback);
        }


        void meshDataThread(Action<meshData> callback)
        {
            bigChunk bigChunker = new bigChunk();
            bigChunker.smallChunkerList = new smallChunk[10, 10, 10];
            bigChunker.worldAreaPosition = Vector3.zero;
            Vector3 areaPos = bigChunker.worldAreaPosition;

            for (int x = 0; x < 200; x += 20)
            {
                for (int y = 0; y < 100; y += 10)
                {
                    for (int z = 0; z < 200; z += 20)
                    {
                        int xx = (x) / 20;
                        int yy = (y) / 10;
                        int zz = (z) / 20;

                        Vector3 realPos3 = new Vector3(x * planeSize, y * planeSize, z * planeSize);

                        if (bigChunker.smallChunkerList[xx, yy, zz] == null)
                        {
                            bigChunker.smallChunkerList[xx, yy, zz] = new smallChunk();
                            bigChunker.smallChunkerList[xx, yy, zz].worldPosition = realPos3;

                            chunk chunki = new chunk();
                            meshData meshDator = chunki.startBuildingArray(realPos3);

                            lock (meshDataThreadInfoQueue)
                            {
                                meshDataThreadInfoQueue.Enqueue(new meshThreadInfo<meshData>(callback, meshDator));
                            }
                        }
                    }
                }
            }
        }





























        /*void getPositions(Action<chunkPosition> callback)
        {
            chunkPosBig bigChunker = new chunkPosBig();
            bigChunker.smallChunkerList = new chunkPosSmall[10, 10, 10];
            bigChunker.worldAreaPosition = Vector3.zero;
            Vector3 areaPos = bigChunker.worldAreaPosition;

            for (int x = 0; x < 200; x += 20)
            {
                for (int y = 0; y < 100; y += 10)
                {
                    for (int z = 0; z < 200; z += 20)
                    {
                        int xx = (x) / 20;
                        //int yPoser = (int)Math.Round((y) * chunkWidth);
                        //int yy = (int)(Mathf.Round(y*10));
                        int yy = (y)/10;
                        int zz = (z) / 20;

                        Vector3 realPos3 = new Vector3(x * planeSize, y * planeSize, z * planeSize);
                        // Vector3 realPos3 = colissdecriss3 + new Vector3(areaPos.x, areaPos.y, areaPos.z);

                        if (bigChunker.smallChunkerList[xx, yy, zz] == null)
                        {
                            //iSCentralDispatch.Instantiate(plane,realPos3,Quaternion.identity);
                            bigChunker.smallChunkerList[xx, yy, zz] = new chunkPosSmall(realPos3,xx,yy,zz);
                            //bigChunker.smallChunkerList[xx, yy, zz].worldPosition = realPos3;
                            chunkPosition newChunkPos = chunkerPos(realPos3,bigChunker);
                            //Debug.Log(realPos3);

                            //lock (chunkPosDataThreadInfoQueue)
                            //{//Console.WriteLine("viarge");
                                chunkPosDataThreadInfoQueue.Enqueue(new chunkPosThreadInfo<chunkPosition>(callback, newChunkPos));
                            //}
                        }
                    }
                }
            }
        }

        chunkPosition chunkerPos(Vector3 realPos3,chunkPosBig chunkBig)
        {
            var yo = new chunkPosition(realPos3, chunkBig);
            return yo;
        }*/











        public struct meshData
        {
            //public readonly smallChunk smallChunk;
            //public readonly bigChunk bigChunk;
            public readonly Vector3 currentPos;
            public readonly int lengthOfArray;
            public Vector3[] positions;
            public int[] triangleIndices;

            public meshData(Vector3 currentPos,/* bigChunk bigChunk, smallChunk smallChunk,*/ int lengthOfArray, Vector3[] positions, int[] triangleIndices)
            {
                //this.smallChunk = smallChunk;
                //this.bigChunk = bigChunk;
                this.currentPos = currentPos;
                this.lengthOfArray = lengthOfArray;
                this.positions = positions;
                this.triangleIndices = triangleIndices;
            }
        }

        struct meshThreadInfo<T>
        {
            public readonly Action<T> callback;
            public readonly T parameter;

            public meshThreadInfo(Action<T> callback, T parameter)
            {
                this.callback = callback;
                this.parameter = parameter;
            }
        }






        public struct chunkPosition
        {
            public Vector3 currentChunkPos;
            public chunkPosBig chunkBig;

            public chunkPosition(Vector3 currentChunkPos, chunkPosBig chunkBig)
            {
                this.currentChunkPos = currentChunkPos;
                this.chunkBig = chunkBig;
            }
        }

        struct chunkPosThreadInfo<T>
        {
            public readonly Action<T> callback;
            public readonly T parameter;

            public chunkPosThreadInfo(Action<T> callback, T parameter)
            {
                this.callback = callback;
                this.parameter = parameter;
            }
        }




















        public bigChunk getBigChunk(Vector3 currentPos,/*, int x, int y, int z,*/
bigChunk[,,] chuk)
        {
            int x = (int)currentPos.x;
            int y = (int)currentPos.y;
            int z = (int)currentPos.z;
            Console.WriteLine("yo1");

            //bigChunk[,,] chunker = null;

            if ((x < 0) || (y < 0) || (z < 0) || (x >= chunkWidth) || (y >= chunkWidth) || (z >= chunkWidth))
            {
                //Console.WriteLine("outside range");
                return null;
            }
            //chuk = chunker[x, y, z];
            return chuk[x, y, z];
        }


        public smallChunk getSmallChunk(int x, int y, int z, smallChunk[,,] chuk)
        {
            //int x = (int)currentPos.X;
            //int y = (int)(Math.Round(currentPos.Y));
            //int z = (int)currentPos.Z;

            if ((x < 0) || (y < 0) || (z < 0) || (x >= 10) || (y >= 1) || (z >= 10))
            {
                //Debug.Log("outside range");
                return null;
            }

            return chuk[x, y, z];
        }


        public void OnDisable()
        {

        }

        public void OnApplicationQuit()
        {

        }

        public void OnDestroy()
        {

        }
    }
}







/*static Queue q = Queue.Synchronized(new Queue());
static bool running = true;

static void mining()
{
    Thread producer1 = new Thread(() =>
    {
        while (running)
        {
            q.Enqueue(Guid.NewGuid());
            Thread.Sleep(1);
        }
    });

    Thread producer2 = new Thread(() =>
    {
        while (running)
        {
            q.Enqueue(Guid.NewGuid());
            Thread.Sleep(1);
        }
    });

    Thread consumer = new Thread(() =>
    {
        while (running)
        {
            if (q.Count > 0)
            {
                Guid g = (Guid)q.Dequeue();
                Debug.Log(g.ToString() + " ");
            }
            else
            {
                Debug.Log(" . ");
            }
            Thread.Sleep(1);
        }
    });

    consumer.IsBackground = true;
    consumer.Start();

    producer1.Start();
    producer2.Start();

    //running = false;
}*/








/*void meshDataThread0(Action<meshData> callback)
{
    try
    {
        bigChunk bigChunker = new bigChunk();
        bigChunker.smallChunkerList = new smallChunk[10, 10, 10];
        bigChunker.worldAreaPosition = Vector3.zero;
        Vector3 areaPos = bigChunker.worldAreaPosition;

        for (int x = 0; x < 500; x += 50)
        {
            for (int y = 0; y < 100; y += 10)
            {
                for (int z = 0; z < 500; z += 50)
                {
                    int xx = x / 50;
                    //int yPoser = (int)Math.Round((y) * chunkWidth);
                    int yy = y / 10;
                    int zz = z / 50;

                    Vector3 colissdecriss3 = new Vector3(x * planeSize, y * planeSize, z * planeSize);
                    Vector3 realPos3 = colissdecriss3 + new Vector3(areaPos.x, areaPos.y, areaPos.z);

                    if (bigChunker.smallChunkerList[xx, yy, zz] == null)
                    {
                        chunk chunki = new chunk();
                        bigChunker.smallChunkerList[xx, yy, zz] = new smallChunk();
                        bigChunker.smallChunkerList[xx, yy, zz].worldPosition = realPos3;
                        meshData meshDator = chunki.startBuildingArray(realPos3, xx, yy, zz, bigChunker, bigChunker.smallChunkerList[xx, yy, zz]);

                        lock (meshDataThreadInfoQueue)
                        {//Console.WriteLine("viarge");
                            meshDataThreadInfoQueue.Enqueue(new meshThreadInfo<meshData>(callback, meshDator));
                        }
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Debug.Log("Error: ");
        return;
    }
}

void meshDataThread1(Action<meshData> callback)
{
    try
    {
        bigChunk bigChunker = new bigChunk();
        bigChunker.smallChunkerList = new smallChunk[10, 10, 10];
        bigChunker.worldAreaPosition = Vector3.zero;
        Vector3 areaPos = bigChunker.worldAreaPosition;

        for (int x = 0; x < 500; x += 50)
        {
            for (int y = 0; y < 100; y += 10)
            {
                for (int z = 500; z < 1000; z += 50)
                {
                    int xx = x / 50;
                    //int yPoser = (int)Math.Round((y) * chunkWidth);
                    int yy = y / 10;
                    int zz = (z-500) / 50;

                    Vector3 colissdecriss3 = new Vector3(x * planeSize, y * planeSize, z * planeSize);
                    Vector3 realPos3 = colissdecriss3 + new Vector3(areaPos.x, areaPos.y, areaPos.z);

                    if (bigChunker.smallChunkerList[xx, yy, zz] == null)
                    {
                        chunk chunki = new chunk();
                        bigChunker.smallChunkerList[xx, yy, zz] = new smallChunk();
                        bigChunker.smallChunkerList[xx, yy, zz].worldPosition = realPos3;
                        meshData meshDator = chunki.startBuildingArray(realPos3, xx, yy, zz, bigChunker, bigChunker.smallChunkerList[xx, yy, zz]);

                        lock (meshDataThreadInfoQueue)
                        {//Console.WriteLine("viarge");
                            meshDataThreadInfoQueue.Enqueue(new meshThreadInfo<meshData>(callback, meshDator));
                        }
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Debug.Log("Error: ");
        return;
    }
}

void meshDataThread2(Action<meshData> callback)
{
    try
    {
        bigChunk bigChunker = new bigChunk();
        bigChunker.smallChunkerList = new smallChunk[10, 10, 10];
        bigChunker.worldAreaPosition = Vector3.zero;
        Vector3 areaPos = bigChunker.worldAreaPosition;

        for (int x = 500; x < 1000; x += 50)
        {
            for (int y = 0; y < 100; y += 10)
            {
                for (int z = 0; z < 500; z += 50)
                {
                    int xx = (x-500) / 50;
                    //int yPoser = (int)Math.Round((y) * chunkWidth);
                    int yy = y / 10;
                    int zz = z / 50;

                    Vector3 colissdecriss3 = new Vector3(x * planeSize, y * planeSize, z * planeSize);
                    Vector3 realPos3 = colissdecriss3 + new Vector3(areaPos.x, areaPos.y, areaPos.z);

                    if (bigChunker.smallChunkerList[xx, yy, zz] == null)
                    {
                        chunk chunki = new chunk();
                        bigChunker.smallChunkerList[xx, yy, zz] = new smallChunk();
                        bigChunker.smallChunkerList[xx, yy, zz].worldPosition = realPos3;
                        meshData meshDator = chunki.startBuildingArray(realPos3, xx, yy, zz, bigChunker, bigChunker.smallChunkerList[xx, yy, zz]);

                        lock (meshDataThreadInfoQueue)
                        {//Console.WriteLine("viarge");
                            meshDataThreadInfoQueue.Enqueue(new meshThreadInfo<meshData>(callback, meshDator));
                        }
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Debug.Log("Error: ");
        return;
    }
}


void meshDataThread3(Action<meshData> callback)
{
    try
    {
        bigChunk bigChunker = new bigChunk();
        bigChunker.smallChunkerList = new smallChunk[10, 10, 10];
        bigChunker.worldAreaPosition = Vector3.zero;
        Vector3 areaPos = bigChunker.worldAreaPosition;

        for (int x = 500; x < 1000; x += 50)
        {
            for (int y = 0; y < 100; y += 10)
            {
                for (int z = 500; z < 1000; z += 50)
                {
                    int xx = (x-500) / 50;
                    //int yPoser = (int)Math.Round((y) * chunkWidth);
                    int yy = y / 10;
                    int zz = (z-500) / 50;

                    Vector3 colissdecriss3 = new Vector3(x * planeSize, y * planeSize, z * planeSize);
                    Vector3 realPos3 = colissdecriss3 + new Vector3(areaPos.x, areaPos.y, areaPos.z);

                    if (bigChunker.smallChunkerList[xx, yy, zz] == null)
                    {
                        chunk chunki = new chunk();
                        bigChunker.smallChunkerList[xx, yy, zz] = new smallChunk();
                        bigChunker.smallChunkerList[xx, yy, zz].worldPosition = realPos3;
                        meshData meshDator = chunki.startBuildingArray(realPos3, xx, yy, zz, bigChunker, bigChunker.smallChunkerList[xx, yy, zz]);

                        lock (meshDataThreadInfoQueue)
                        {//Console.WriteLine("viarge");
                            meshDataThreadInfoQueue.Enqueue(new meshThreadInfo<meshData>(callback, meshDator));
                        }
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Debug.Log("Error: ");
        return;
    }
}*/










