/*using UnityEngine;
using System.Collections;
using SPINACH.iSCentralDispatch;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

public class newTerrainGen : MonoBehaviour
{
    List<Vector3> chunks = new List<Vector3>();
    Dictionary<Vector3, GameObject> chunkerPos0 = new Dictionary<Vector3, GameObject>();
    Dictionary<Vector3, GameObject> chunkerPosTest = new Dictionary<Vector3, GameObject>();

    Dictionary<Vector3, GameObject> chunkLowDetail = new Dictionary<Vector3, GameObject>();
    Dictionary<Vector3, GameObject> chunkHighDetail = new Dictionary<Vector3, GameObject>();
    Dictionary<Vector3, GameObject> chunkTooFar = new Dictionary<Vector3, GameObject>();
    Dictionary<Vector3, GameObject> chunkActivated = new Dictionary<Vector3, GameObject>();

    Dictionary<Vector3, int> tempChunk0 = new Dictionary<Vector3, int>();
    List<Vector3> tempChunk00 = new List<Vector3>();

    public static newTerrainGen currentTerrain;

    public GameObject chunkFabLowDetail;
    public GameObject chunkFabHighDetail;


    Vector3 chunkPos0;
    Vector3 currentPos;
    Vector3 startPos;

    WaitForSeconds wait = new WaitForSeconds(0f);

    public int seed;
    public int levelOfDetail;
    int posDontExists = 1;
    int chunkPosCounter0 = 0;
    int threadID0 = 0;
    public int viewSize;
    public int viewSize1;
    public int viewSize0;

    float planeSize = 1;

    public float chunkSizeLOWDETAIL;
    public float chunkSizeHeight;
    public float chunkSizeHIGHDETAIL;

    private volatile bool cancelFlag = false;
    bool jobDone0 = false;

    GameObject currentValue;

    chunko chuk;

    void Start()
    {

        if (seed == 0)
        {
            seed = Random.Range(0, int.MaxValue);
        }
        currentTerrain = this;

        startPos = transform.position;
    }

    private void MyThread0()
    {
        while (cancelFlag == false)
        {
            for (float x = (currentPos.x) - viewSize0; x <= (currentPos.x) + viewSize0; x += chunkSizeLOWDETAIL)
            {
                for (float y = (currentPos.y) - viewSize1; y <= (currentPos.y) + viewSize1; y += chunkSizeHeight*planeSize)
                {
                    for (float z = (currentPos.z) - viewSize0; z <= (currentPos.z) + viewSize0; z += chunkSizeLOWDETAIL)
                    {
                        float chunkX0 = Mathf.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        float chunkY0 = Mathf.FloorToInt(y / chunkSizeHeight*planeSize) * chunkSizeHeight * planeSize;
                        float chunkZ0 = Mathf.FloorToInt(z / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                        chunkPos0 = new Vector3(chunkX0, chunkY0, chunkZ0);

                        if (chunkerPos0.ContainsKey(chunkPos0))
                        {
                            continue;
                        }
                        if (!chunkerPos0.ContainsKey(chunkPos0))
                        {
                            GameObject yo = (GameObject)iSCentralDispatch.Instantiate(chunkFabLowDetail, chunkPos0, Quaternion.identity);
                            chunkerPos0.Add(chunkPos0, yo);
                        }


                        //if (Vector3.Distance(currentPos, chunkPos0) > 5)
                        //{
                        //    if (chunkerPos0.ContainsKey(chunkPos0))
                        //    {
                        //        continue;
                        //    }
                        //    if (!chunkerPos0.ContainsKey(chunkPos0))
                        //    {
                        //        GameObject yo = (GameObject)iSCentralDispatch.Instantiate(chunkFabLowDetail, chunkPos0, Quaternion.identity);
                        //        chunkActivated.Add(chunkPos0, yo);
                        //        chunkerPosTest.Add(chunkPos0, yo);
                        //        chunkerPos0.Add(chunkPos0, yo);
                        //    }
                        //}
                    }
                }
            }

            //for (float x = (currentPos.x) - viewSize; x <= (currentPos.x) + viewSize; x += chunkSizeHIGHDETAIL)
            //{
            //    for (float z = (currentPos.z) - viewSize; z <= (currentPos.z) + viewSize; z += chunkSizeHIGHDETAIL)
            //    {
            //        float chunkX0 = Mathf.FloorToInt(x / chunkSizeHIGHDETAIL) * chunkSizeHIGHDETAIL;
            //        float chunkZ0 = Mathf.FloorToInt(z / chunkSizeHIGHDETAIL) * chunkSizeHIGHDETAIL;
            //        chunkPos0 = new Vector3(chunkX0, 0, chunkZ0);
            //
            //        int yoX = (int)Mathf.Round(currentPos.x);
            //        int yoY = (int)Mathf.Round(currentPos.y);
            //        int yoZ = (int)Mathf.Round(currentPos.z);
            //
            //Vector3 newPos = new Vector3(yoX, yoY, yoZ);
            //
            //       if (Vector3.Distance(currentPos, chunkPos0) <= 5)
            //        {
            //            if (tempChunk0.ContainsKey(chunkPos0))
            //            {
            //                iSCentralDispatch.DispatchMainThread(() =>
            //                {
            //                    StartCoroutine(LOD());
            //                });
            //                iSCentralDispatch.PauseThread(threadID0);
            //                continue;
            //            }
            //            if (!tempChunk0.ContainsKey(chunkPos0))
            //            {
            //                for (int u = 0; u < 1; u++)
            //                {
            //                    tempChunk0.Add(chunkPos0, posDontExists);
            //                    if (!chunkerPos0.ContainsKey(chunkPos0))
            //                    {
            //                        chunkerPos0.Add(chunkPos0, (GameObject)iSCentralDispatch.Instantiate(chunkFabHighDetail, chunkPos0, Quaternion.identity));
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            iSCentralDispatch.DispatchMainThread(() =>
            {
                StartCoroutine(Timer());
            });
            iSCentralDispatch.PauseThread(threadID0);
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        iSCentralDispatch.ResumeThread(threadID0);
    }



    IEnumerator LOD()
    {
        for (int i = 0; i < chunkerPosTest.Count; i++)
        {
            var currentChunk = chunkerPosTest.ElementAt(i);
            var currentKey = currentChunk.Key;
            var currentValue = currentChunk.Value;

            if (currentValue.GetComponent<chunko>().levelOfDetail == 1)
            {
                //currentValue.GetComponent<chunko>().enabled = false;
                currentValue.GetComponent<chunko>().levelOfDetail = 2;
                currentValue.GetComponent<chunko>().Regenerate();
            }
        }
        chunkerPosTest.Clear();
        yield return new WaitForSeconds(0.01f);
        //iSCentralDispatch.ResumeThread(threadID0);
    }


    void Update()
    {
        //for (int i = 0; i < chunkerPos0.Count; i++)
        //{
        //    var currentChunk = chunkerPos0.ElementAt(i);
        //    var currentKey = currentChunk.Key;
        //    var currentValue = currentChunk.Value;
        //
        ////    if (Vector3.Distance(transform.position, currentKey) > 10)
        //    {
        //        currentValue.GetComponent<MeshRenderer>().enabled = false;
        //    }
        //}




        currentPos = transform.position;

        int xMove = (int)(transform.position.x - startPos.x);
        int zMove = (int)(transform.position.z - startPos.z);
        int yMove = (int)(transform.position.y - startPos.y);

        if (jobDone0 == false)
        {
            threadID0 = iSCentralDispatch.DispatchNewThread("ChunkSpawnLOWERDetail0", MyThread0);
            //iSCentralDispatch.SetPriorityForThread(threadID2, iSCDThreadPriority.High);
            iSCentralDispatch.SetTargetFramerate(1);
            //counter222 = 0;
            jobDone0 = true;
        }

        if (Mathf.Abs(xMove) >= chunkSizeLOWDETAIL / 10 || Mathf.Abs(zMove) >= chunkSizeLOWDETAIL / 10 || Mathf.Abs(yMove) >= chunkSizeLOWDETAIL / 10)
        {
            startPos = transform.position;
        }
    }


    WaitForSeconds wait2 = new WaitForSeconds(0f);


    //IEnumerator CheckMoving()
    //{
    //    Vector3 startPos = transform.position;
    //    yield return new WaitForSeconds(0.01f);
    //    Vector3 finalPos = transform.position;
    //    if (startPos.x != finalPos.x || startPos.y != finalPos.y
    //        || startPos.z != finalPos.z)
    //    {
    //        isMoving = true;
    //    }
    //
    //    else if (startPos.x == finalPos.x && startPos.y == finalPos.y
    //         && startPos.z == finalPos.z)
    //    {
    //
    //        isMoving = false;
    //    }
    //}



     //IEnumerator Timing0()
     //{
     //    if (timer0 < 0)
     //    {
     //        timer0 = 0;
     //    }
     //
     //    if (timer0 == 0)
     //    {
     //
     //    }
     //    if (counter0 == 1)
     //    {
     //        if (timer0 != 0 && timer0 > 0)
     //        {
     //            timer0 += Time.deltaTime;
     //        }
     //    }
     //    yield return wait2;
     //}






    void OnDisable()
    {
        cancelFlag = true;
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID0);
    }


    void OnApplicationQuit()
    {
        cancelFlag = true;
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID0);
        //iSCentralDispatch.AbortThread(threadID);
    }

    void OnDestroy()
    {
        cancelFlag = true;
        if (iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread(threadID0);
        //iSCentralDispatch.AbortThread(threadID);
    }

}*/









/*private void MyThread0()
{
    while (cancelFlag == false)
    {
        do
        {
            for (float x = (currentPos.x) - viewSize0; x <= (currentPos.x) + viewSize0; x += chunkSizeLOWDETAIL)
            {
                for (float z = (currentPos.z) - viewSize0; z <= (currentPos.z) + viewSize0; z += chunkSizeLOWDETAIL)
                {
                    float chunkX0 = Mathf.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                    float chunkZ0 = Mathf.FloorToInt(z / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL;
                    chunkPos0 = new Vector3(chunkX0, 0, chunkZ0);

                    if (tempChunk0.ContainsKey(chunkPos0))
                    {
                        continue;
                    }
                    if (!tempChunk0.ContainsKey(chunkPos0))
                    {

                        for (int u = 0; u < 1; u++)
                        {

                            chunkPosCounter0 += 1;
                            tempChunk0.Add(chunkPos0, posDontExists);
                            //tempChunk00.Add(chunkPos0);

                            //iSCentralDispatch.Instantiate(sphere1, chunkPos0, Quaternion.identity);
                            chunkerPos0.Add((GameObject)iSCentralDispatch.Instantiate(chunkFabLowDetail, chunkPos0, Quaternion.identity), chunkPos0);
                            chunkPosCounter0 -= 1;

                        }
                        Thread.Sleep(1);
                    }

                }
            }
        } while (jobDone0 == true);
    }
}*/



