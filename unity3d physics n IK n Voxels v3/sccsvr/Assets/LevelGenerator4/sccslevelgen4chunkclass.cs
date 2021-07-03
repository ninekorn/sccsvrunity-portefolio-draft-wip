using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sccslevelgen4chunkclass
{
    public Vector3 worldPosition;
    public GameObject planetchunk;
    public sccslodchunk somesccslodchunk;
    //public byte[] arrayOfChunkMapOfBytes;
    public int index;

    public sccslevelgen4chunkclass(Vector3 worldPos, GameObject planetchunk_, sccslodchunk somesccslodchunk_, int index_)
    {
        index = index_;
        //arrayOfChunkMapOfBytes = arrayOfChunkMapOfBytes_;
        somesccslodchunk = somesccslodchunk_;
        worldPosition = worldPos;
        planetchunk = planetchunk_;
    }
}




/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mainChunkGen2
{
    public Vector3 worldPosition;
    public GameObject planetchunk;
    public sccsplanetchunkGen2 somesccsplanetchunk;
    //public byte[] arrayOfChunkMapOfBytes;
    public int index;

    public mainChunkGen2(Vector3 worldPos, GameObject planetchunk_, sccsplanetchunkGen2 sccsplanetchunks, int index_)
    {
        index = index_;
        //arrayOfChunkMapOfBytes = arrayOfChunkMapOfBytes_;
        somesccsplanetchunk = sccsplanetchunks;
        worldPosition = worldPos;
        planetchunk = planetchunk_;
    }
}*/