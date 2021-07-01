using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCCoreSystems;

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

    int _max = 0;


    void Awake()
    {
        buildplanetchunk();
    }








    void buildplanetchunk()
    {
        _max = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);

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

                    blockers[_index] = new mainChunk(planetchunkpos, yo.gameObject);

                    blockers[_index].planetchunk.GetComponent<sccsplanetchunk>().buildchunkmap();
                }
            }
        }



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
                }
            }
        }






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
