using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class startGeneratingFaces : MonoBehaviour {

    List<GameObject> chunks;
    GameObject[] chunkz;

    //List<Vector3> createdTiles = new List<Vector3>();
    public Dictionary<Vector3, Vector3> createdTiles = new Dictionary<Vector3, Vector3>();
    List<Vector3> leftWalls = new List<Vector3>();


    public GameObject chunk;

    void Update ()
    {



        if (Input.GetKeyDown("g"))
        {
            chunks = new List<GameObject>();
            chunkz = GameObject.FindGameObjectsWithTag("chunks");

            createdTiles = LevelGenerator4.currentLevelGen.createdTiles;

            var enumerator0 = createdTiles.GetEnumerator();
            while (enumerator0.MoveNext())
            {
                var currentTuile = enumerator0.Current;
                Instantiate(chunk, currentTuile.Key, Quaternion.identity);
            }



            /*leftWalls = LevelGenerator999.currentLevelGen.leftWall;

            var enumerator1 = leftWalls.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                var currentTuile = enumerator1.Current;
                Instantiate(chunk, currentTuile, Quaternion.identity);
                chunk.name = "leftWall";
            }*/





        }






        if (Input.GetKeyDown("t"))
        {
            chunks = new List<GameObject>();
            chunkz = GameObject.FindGameObjectsWithTag("chunks");

            StartCoroutine(buildFaces());

            /*for (int i = 0; i < chunkz.Length; i++)
            {
                //StartCoroutine(buildFaces());
                GameObject singleChunk = chunkz[i];
                singleChunk.GetComponent<newFloorTiles>().Regenerate();
            }*/
        }







    }

    //WaitForSeconds waiting = new WaitForSeconds(0.5f);
    IEnumerator buildFaces()
    {
        for (int i = 0; i < chunkz.Length; i++)
        {
            GameObject singleChunk = chunkz[i];
            singleChunk.GetComponent<newFloorTiles>().Regenerate();
            yield return new WaitForSeconds(0f);
        }
        yield return new WaitForSeconds(0f);
    }

}
