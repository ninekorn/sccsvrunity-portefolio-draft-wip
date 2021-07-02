using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mainChunkFinal
{
    public Vector3 worldPosition;
    public GameObject planetchunk;
    public sccslodchunkfinal somesccsplanetchunkFinal;
    //public int[] arrayOfChunkMapOfBytes;
    public int index;
    public List<int> sometriangles;
    public List<Vector3> somevertex;
    public sccsproceduralplanetbuilderGen2 planetbuilder;

    public mainChunkFinal(Vector3 worldPos, GameObject planetchunk_, sccslodchunkfinal somesccsplanetchunkFinal_, int index_, sccsproceduralplanetbuilderGen2 planetbuilder_)
    {
        planetbuilder = planetbuilder_;
        index = index_;
        //arrayOfChunkMapOfBytes = arrayOfChunkMapOfBytes_;
        somesccsplanetchunkFinal = somesccsplanetchunkFinal_;
        worldPosition = worldPos;
        planetchunk = planetchunk_;
    }
}