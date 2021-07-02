using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mainChunkGen2
{
    public Vector3 worldPosition;
    public GameObject planetchunk;
    public sccslodchunk1 somesccsplanetchunkGen2;
    //public byte[] arrayOfChunkMapOfBytes;
    public int index;

    public mainChunkGen2(Vector3 worldPos, GameObject planetchunk_, sccslodchunk1 somesccsplanetchunkGen2_, int index_)
    {
        index = index_;
        //arrayOfChunkMapOfBytes = arrayOfChunkMapOfBytes_;
        somesccsplanetchunkGen2 = somesccsplanetchunkGen2_;
        worldPosition = worldPos;
        planetchunk = planetchunk_;
    }
}