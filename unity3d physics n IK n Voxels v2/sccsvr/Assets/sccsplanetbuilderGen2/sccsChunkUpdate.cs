using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsChunkUpdate : MonoBehaviour
{
    public sccsChunk thechunk;
    
    public int drawswtc = 0;
    int resetswtc = 0;

    int t = 0;
    int total = 0;
    int posx = 0;
    int posy = 0;
    int posz = 0;
    int xx = 0;
    int yy = 0;
    int zz = 0;
    int xi = 0;
    int yi = 0;
    int zi = 0;
    int swtchx = 0;
    int swtchy = 0;
    public int swtchz = 0;
    public int width = 10;
    public int height = 10;
    public int depth = 10;
    public int index = 0;
    int counterCreateChunkObjectFaces = 0;

    public void Start()
    {
        total = width * height * depth;
        
    }

    public void Update()
    {

        if (drawswtc == 1)
        {
            if (thechunk.planetchunk != null)
            {

                if (resetswtc == 0)
                {
                    thechunk.sccsSetMap();
                    thechunk.Regenerate();
                    resetswtc = 1;
                }


                for (int i = 0; i < 100; i++)
                {
                    if (swtchz == 0)
                    {
                        InvokeRepeating("CreateChunkFaces", 0, 0.001f);
                    }
                    else
                    {
                        CancelInvoke();
                        buildMesh();
                        t = 0;

                        posx = 0;
                        posy = 0;
                        posz = 0;

                        xx = 0;
                        yy = 0;
                        zz = 0;

                        xi = 0;
                        yi = 0;
                        zi = 0;

                        swtchx = 0;
                        swtchy = 0;
                        swtchz = 0;

                        counterCreateChunkObjectFaces = 0;

                        drawswtc = 0;
                        resetswtc = 0;
                    }
                }
            }
        }
    }

    public void buildMesh()
    {
        thechunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh.Clear();
        thechunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh.vertices = thechunk.vertexlist.ToArray();
        thechunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh.triangles = thechunk.triangles.ToArray();
        thechunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        thechunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();
    }


    public void CreateChunkFaces()
    {
        if (swtchz == 0)
        {
            if (t < total)
            {
                posx = (xx);
                posy = (yy);
                posz = (zz);

                xi = xx;
                yi = yy;
                zi = zz;

                if (xi < 0)
                {
                    xi *= -1;
                    xi = (width) + xi;
                }
                if (yi < 0)
                {
                    yi *= -1;
                    yi = (height) + yi;
                }
                if (zi < 0)
                {
                    zi *= -1;
                    zi = (depth) + zi;
                }

                //zi = (depth - 1) - zi;

                index = xi + (width) * (yi + (height) * zi);

                if (index < total)
                {
                    thechunk.CreateChunkFaces();
                }
                else
                {
                    //t = total;
                }

                zz++;
                if (zz >= (depth))
                {
                    xx++;
                    zz = 0;
                    swtchx = 1;
                }
                if (xx >= (width) && swtchx == 1)
                {
                    //faceStart = 0;
                    yy++;
                    xx = 0;
                    swtchx = 0;
                    swtchy = 1;
                }
                if (yy >= (height) && swtchy == 1)
                {
                    //yy = -ChunkHeight_L;
                    swtchy = 0;
                    swtchx = 0;
                    swtchz = 1;
                }
                t++;
                counterCreateChunkObjectFaces++;
            }

            //Debug.Log("total:" + total + "/t:" + t);
        }
    }
}
