using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mainChunk
{
    public Vector3 worldPosition;
    public GameObject planetchunk;
    public sccsplanetchunk somesccsplanetchunk;
    //public byte[] arrayOfChunkMapOfBytes;
    public int index;

    public mainChunk(Vector3 worldPos, GameObject planetchunk_, sccsplanetchunk sccsplanetchunks, int index_)
    {
        index = index_;
        //arrayOfChunkMapOfBytes = arrayOfChunkMapOfBytes_;
        somesccsplanetchunk = sccsplanetchunks;
        worldPosition = worldPos;
        planetchunk = planetchunk_;
    }
}