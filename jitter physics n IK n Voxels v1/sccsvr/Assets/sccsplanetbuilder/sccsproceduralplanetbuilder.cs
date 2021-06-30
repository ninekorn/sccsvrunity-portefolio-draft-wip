﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class mainChunk
{
    public Vector3 worldPosition;
    public GameObject planetchunk;

    public mainChunk(Vector3 worldPos, GameObject planetchunk_)
    {
        worldPosition = worldPos;
        planetchunk = planetchunk_;
    }
}



public class sccsproceduralplanetbuilder : MonoBehaviour
{
    //byte[,,] blocks;
    static mainChunk[] blockers;

    //byte block;
    int realplanetwidth = 4;
    public Transform cube;
    Vector3[] myArray;

    //static int planetwidth = 16;
    //static int planetheight = 16;
    //static int planetdepth = 16;

    public int ChunkWidth_L = 16;
    public int ChunkWidth_R = 15;

    public int ChunkHeight_L = 16;
    public int ChunkHeight_R = 15;

    public int ChunkDepth_L = 16;
    public int ChunkDepth_R = 15;

    public static float noiseX;
    public static float noiseY;
    public static float noiseZ;

    //int planetwidth = 32;
    //int planetheight = 32;
    //int planetdepth = 32;
    public GameObject hingerelease;


    int _max = 0;

 
    void Fire()
    {
        GameObject obj = NewObjectPoolerScript.current.GetPooledObject();

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

        /*GetComponent<shadowBullet>().bullseyedirection = gunEnd.transform.forward;
        GetComponent<shadowBullet>().firing_ship = player.gameObject;
        GetComponent<shadowBullet>().gunEnd = gunEnd.gameObject;
        GetComponent<shadowBullet>().enabled = true;
        GetComponent<shadowBullet>().shadowObject = obj;// this.transform.FindChild("shadowbullet").gameObject;*/
    }


    float delayOrTime = 2.5f;
    float repeatrate = 2.5f;


    void Awake()
    {
        waitforseconds = new WaitForSeconds(0);
        //buildplanetchunk();

        StartCoroutine(buildplanetchunk());

        //InvokeRepeating("buildplanetchunk", delayOrTime, repeatrate);
    }


    // Update is called once per frame
    void ShootTheChunk() //FireShadowBullet
    {
        /*hingerelease = sccshingereleasepooler.current.GetPooledObject(); //this.transform.gameObject;// 

        if (hingerelease == null) return;

        hingerelease.transform.position = this.transform.position;// gunEnd.transform.position;// transform.position;
        hingerelease.transform.rotation = this.transform.rotation;//shadowProjectile.transform.rotation;// transform.rotation;
        hingerelease.transform.gameObject.SetActive(true);*/
    }



    WaitForSeconds waitforseconds;// = new WaitForSeconds();

    IEnumerator buildplanetchunk()
    {
        _max = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);

        int swtchXi = 0;
        int swtchYi = 0;
        int swtchZi = 0;


        int Xi = -ChunkWidth_L;
        int Yi = -ChunkHeight_L;
        int Zi = -ChunkDepth_L;

        for (int m = 0; m < 1; m++) // leaving it at 1 so that i ask myself the question wtf later. ive coded this already. im not doing it again. i need to find where i put it. peace of shit code. for brain grinders only
        {
        }
        blockers = new mainChunk[_max];

        //blockers = new mainChunk[(planetwidth * planetheight * planetdepth) + (planetwidth * planetheight * planetdepth)];
        //blockers = new mainChunk[(planetwidth + planetwidth) * (planetheight + planetheight) * (planetdepth + planetdepth)];

        Vector3 center = Vector3.zero;

        for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x += 4)
        {
            for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y += 4)
            {
                for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z += 4)
                {
                    float posX = (x);
                    float posY = (y);
                    float posZ = (z);

                    Vector3 planetchunkpos = new Vector3(posX, posY, posZ);

                    var xx = x;
                    var yy = y;
                    var zz = z;

                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = (ChunkWidth_R) + xx;
                    }
                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = (ChunkHeight_R) + yy;
                    }
                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = (ChunkDepth_R) + zz;
                    }

                    int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

                    Transform yo = Instantiate(cube, planetchunkpos, Quaternion.identity);

                    yo.transform.parent = transform;

                    if (yo.GetComponent<sccsplanetchunk>() != null)
                    {
                        //Debug.Log("!null");
                        yo.GetComponent<sccsplanetchunk>().buildchunkmap();
                        blockers[_index] = new mainChunk(planetchunkpos, yo.gameObject);
                    }
                    else
                    {
                        //Debug.Log("null");
                    }
                    blockers[_index].planetchunk.GetComponent<sccsplanetchunk>().buildchunkmap();
                    yield return waitforseconds;
                }
            }
        }

        // yield return waitforseconds;

        for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x += 4)
        {
            for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y += 4)
            {
                for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z += 4)
                {
                    var xx = x;
                    var yy = y;
                    var zz = z;

                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = (ChunkWidth_R) + xx;
                    }
                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = (ChunkHeight_R) + yy;
                    }
                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = (ChunkDepth_R) + zz;
                    }

                    int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);


                    blockers[_index].planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    blockers[_index].planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                    //yield return new WaitForSeconds(0);
                    yield return waitforseconds;
                }
            }
        }


        try
        {
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

    }



    public mainChunk getChunk(int x, int y, int z)
    {
        if ((x < -ChunkWidth_L) || (y < -ChunkHeight_L) || (z < -ChunkDepth_L) || (x >= ChunkWidth_R + 1) || (y >= (ChunkHeight_R + 1)) || (z >= (ChunkDepth_R + 1)))
        {
            return null;
        }

        if (x < 0)
        {
            x *= -1;
            x = (ChunkWidth_R) + x;
        }
        if (y < 0)
        {
            y *= -1;
            y = (ChunkHeight_R) + y;
        }
        if (z < 0)
        {
            z *= -1;
            z = (ChunkDepth_R) + z;
        }

        int _index = x + (ChunkWidth_L + ChunkWidth_R + 1) * (y + (ChunkHeight_L + ChunkHeight_R + 1) * z);

        return blockers[_index];

        //return map[_index] == 0;
        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
		{
			return null;
		}

		return blockers[x, y, z];*/




        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
        {
            return null;
        }
        if (blockers[x, y, z] == null)
        {
            return null;
        }
        return blockers[x, y, z];*/
    }

    public void drawBrick(int x, int y, int z)
    {
        Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
    }

    /*public byte GetByte(mainChunk chuk,int x, int y, int z)
    {
		chuk.chunker.get

        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            return 0;
        }
        return blocks[x, y, z];
    }*/
}



/*var xx = Xi;
var yy = Yi;
var zz = Zi;

if (xx < 0)
{
    xx *= -1;
    xx = (ChunkWidth_R) + xx;
}
if (yy < 0)
{
    yy *= -1;
    yy = (ChunkHeight_R) + yy;
}
if (zz < 0)
{
    zz *= -1;
    zz = (ChunkDepth_R) + zz;
}

int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);


if (swtchZi == 0)
{
    if (Zi <= ChunkDepth_R)
    {
        Zi+=4;
    }

    if (Zi > ChunkDepth_R) // prob an else if here instead
    {
        Zi = 0;
        swtchZi = 1;
        swtchYi = 1;
    }
}
if (swtchYi == 1)
{
    if (Yi <= ChunkHeight_R)
    {
        Yi += 4;
        swtchYi = 0;
        swtchZi = 0;
    }

    if (Yi > ChunkHeight_R) // prob an else if here instead
    {
        Yi = 0;
        swtchYi = 0;
        swtchXi = 1;
    }
}*/
/*if (swtchXi == 1)
{
    if (Xi <= ChunkWidth_R)
    {
        Xi += 4;
        swtchXi = 0;
        swtchYi = 0;
        swtchZi = 0;       
    }

    if (Xi > ChunkWidth_R) // prob an else if here instead
    {
        swtchYi = 0;
        swtchXi = 1;
    }
}*/






/*if (swtchXi == 0)
{
    if(Xi<= ChunkWidth_R)
    {
        Xi+=4;
    }

    if (Xi > ChunkWidth_R)
    {
        Xi = 0;
        swtchXi = 1;
        swtchYi = 1;
    }
}
if (swtchYi == 1)
{
    if (Yi <= ChunkHeight_R)
    {
        Yi+=4;
    }

    if (Yi > ChunkHeight_R)
    {
        Yi = 0;
        swtchYi = 2;
        swtchZi = 1;
    }
}

if (swtchZi == 1)
{
    if (Zi <= ChunkDepth_R)
    {
        Zi+=4;
    }

    if (Zi > ChunkDepth_R)
    {
        Zi = 0;
        swtchZi = 0;
        swtchXi = 0;
    }
}*/

//Debug.Log("max: " + _max + " x: " + Xi + " y: " + Yi + " z: " + Zi);





























//yield return waitforseconds;


/*
for (int x = -planetwidth; x < planetwidth; x += 4)
{
    for (int y = -planetheight; y < planetheight; y += 4)
    {
        for (int z = -planetdepth; z < planetdepth; z += 4)
        {
            Vector3 position = new Vector3(x, y, z);
            Transform yo = Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);

            yo.transform.parent = transform;

            if (x < 0)
            {
                x *= -1;
                x = (planetwidth - 1) + x;
            }
            if (y < 0)
            {
                y *= -1;
                y = (planetheight - 1) + y;
            }
            if (z < 0)
            {
                z *= -1;
                z = (planetdepth - 1) + z;
            }

            int _index = x + (planetwidth + planetwidth) * (y + (planetheight + planetheight) * z);



            blockers[_index] = new mainChunk(new Vector3(x, y, z), yo.gameObject);
        }
    }
}*/
