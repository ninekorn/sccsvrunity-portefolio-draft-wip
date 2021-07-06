using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mainChunkGen1
{
    public Vector3 worldPosition;
    public GameObject planetchunk;
    public sccsplanetchunkGen1 somesccsplanetchunkGen1;
    //public byte[] arrayOfChunkMapOfBytes;
    public int index;

    public mainChunkGen1(Vector3 worldPos, GameObject planetchunk_, sccsplanetchunkGen1 somesccsplanetchunkGen1_, int index_)
    {
        index = index_;
        //arrayOfChunkMapOfBytes = arrayOfChunkMapOfBytes_;
        somesccsplanetchunkGen1 = somesccsplanetchunkGen1_;
        worldPosition = worldPos;
        planetchunk = planetchunk_;
    }
}