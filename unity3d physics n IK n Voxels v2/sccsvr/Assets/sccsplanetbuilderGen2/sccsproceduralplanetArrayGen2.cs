using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsproceduralplanetArrayGen2 : MonoBehaviour {

    public sccsproceduralplanetbuilderGen2 planetbuilder;
    public GameObject somesccschunk;
    public GameObject someplanebuilder;

    public int ChunkWidth_L = 4;
    public int ChunkWidth_R = 3;

    public int ChunkHeight_L = 4;
    public int ChunkHeight_R = 3;

    public int ChunkDepth_L = 4;
    public int ChunkDepth_R = 3;

    int counterCreateClassOfChunkGameObject = 0;

    int realplanetwidth = 12;

    int total = -1;



    void Start ()
    {



        total = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);

    }


    int startOnceSwtc = 0;


    void Update ()
    {
        if (startOnceSwtc == 0)
        {



            for (int i = 0; i < 1; i++)
            {
                if (t < total)
                {
                    CreateSomePlanets();
                    //InvokeRepeating("CreateSomePlanets", 1, 0.005f);
                }
                else
                {
                    //CancelInvoke();
                    Debug.Log("ended CreateSomePlanets");

                    t = 0;
                    xx = -ChunkWidth_L;
                    yy = -ChunkHeight_L;
                    zz = -ChunkDepth_L;
                    switchXX = 0;
                    switchYY = 0;

                    startOnceSwtc = 1;

                    break;
                }
            }
        }
    }





    int lastFrameCreateChunk = 0;
    int lastFrameCreateChunkFaces = 0;
    float posX = 0;
    float posY = 0;
    float posZ = 0;
    int xi = 0;
    int yi = 0;
    int zi = 0;
    int _index = 0;
    Vector3 planetchunkpos;
    int t = 0;
    int xx = 0;
    int yy = 0;
    int zz = 0;
    int switchXX = 0;
    int switchYY = 0;
    int someLoopBreaker = 0;


    void CreateSomePlanets()
    {
        //Debug.Log("test");
        //unroll
        //[loop]
        //in order to not use those in hlsl because i never completely understood how they worked then, i decided to learn how to build flat loops (per frame or not). i upgraded this today working a bit on it to a flat loop 
        //with negative and positive indexes so that it works as it was with my negative/positive planets chunks. by steve chassé aka ninekorn 2021-mai-08th.


        if (t < total)
        {
            posX = (xx);
            posY = (yy);
            posZ = (zz);

            planetchunkpos = new Vector3(posX, posY, posZ);

            xi = xx;
            yi = yy;
            zi = zz;

            if (xi < 0)
            {
                xi *= -1;
                xi = (ChunkWidth_R) + xi;
            }
            if (yi < 0)
            {
                yi *= -1;
                yi = (ChunkHeight_R) + yi;
            }
            if (zi < 0)
            {
                zi *= -1;
                zi = (ChunkDepth_R) + zi;
            }

            _index = xi + (ChunkWidth_L + ChunkWidth_R + 1) * (yi + (ChunkHeight_L + ChunkHeight_R + 1) * zi);

            if (_index < total)
            {
                //GameObject somePooledObject = Instantiate(someplanebuilder, planetchunkpos, Quaternion.identity);

                var somePooledObject = NewObjectPoolerScript.current.GetPooledObject();

                if (somePooledObject != null)
                {
                    somePooledObject.transform.position = planetchunkpos;
                    somePooledObject.SetActive(true);
                }
                else
                {
                    //Debug.Log("null somePooledObject");
                }
            }
            else
            {

            }

            zz += realplanetwidth;
            if (zz >= (ChunkDepth_R))
            {
                yy += realplanetwidth;
                zz = -ChunkDepth_L;
                switchYY = 1;
            }
            if (yy >= (ChunkHeight_R) && switchYY == 1)
            {
                xx += realplanetwidth;
                yy = -ChunkHeight_L;
                switchYY = 0;
                switchXX = 1;
            }
            if (xx >= (ChunkWidth_R) && switchXX == 1)
            {
                xx = -ChunkWidth_L;
                switchXX = 0;
            }
            t++;
        }

    }


    /*
    void createsomeplanets()
    {
        Instanti
    }*/








}
