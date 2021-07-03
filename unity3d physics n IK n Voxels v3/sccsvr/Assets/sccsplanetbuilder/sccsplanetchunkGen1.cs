using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimplexNoise;
/*using CoherentNoise;
using CoherentNoise.Generation;
using CoherentNoise.Generation.Displacement;
using CoherentNoise.Generation.Fractal;
using CoherentNoise.Generation.Modification;
using CoherentNoise.Generation.Patterns;
using CoherentNoise.Texturing;*/

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
//[RequireComponent(typeof(MeshCollider))]

public class sccsplanetchunkGen1 : MonoBehaviour
{

    /*private float radiusplanetcorestart = 0.0f;
    private float radiusplanetcoreend = 5.0f;
    private float radiusplanetcavesstart = 5.0f;
    private float radiusplanetcavesend = 9.0f;
    private float radiusplanetcruststart = 9.0f;
    private float radiusplanetcrustend = 11.0f;
    private float radiusplanetmountainstart = 11.0f;
    private float radiusplanetmountainend = 20.0f;*/

    /*private float radiusplanetcorestart = 0.0f;
     private float radiusplanetcoreend = 2.5f;
     private float radiusplanetcavesstart = 2.5f;
     private float radiusplanetcavesend = 4.5f;
     private float radiusplanetcruststart = 4.5f;
     private float radiusplanetcrustend = 6.5f;
     private float radiusplanetmountainstart = 6.5f;
     private float radiusplanetmountainend = 10.0f;*/

    private float radiusplanetcorestart = 0.0f;
    private float radiusplanetcoreend = 0.5f;
    private float radiusplanetcavesstart = 0.5f;
    private float radiusplanetcavesend = 1.25f;
    private float radiusplanetcruststart = 1.25f;
    private float radiusplanetcrustend = 1.5f;
    private float radiusplanetmountainstart = 1.5f;
    private float radiusplanetmountainend = 2.25f;

    //public int width = 16;
    //public int height = 16;
    //public int depth = 16;

    public int width = 16;
    public int height = 16;
    public int depth = 16;





    public int ChunkWidth_L = 6;
    public int ChunkWidth_R = 5;

    public int ChunkHeight_L = 6;
    public int ChunkHeight_R = 5;

    public int ChunkDepth_L = 6;
    public int ChunkDepth_R = 5;




    //public byte[] map;
    public int[] map;
    public Mesh mesh;
    public List<Vector3> verts = new List<Vector3>();
    public List<int> tris = new List<int>();
    public List<Vector2> uv = new List<Vector2>();
    public MeshCollider meshCollider;
    public float planeSize = 0.25f;

    //public Transform sphere;
    float seed;
    int block;

    float nodeDiameter;
    float chunkRadius;
    float fraction;
    float chunkSize;

    int divider = 10;
    //public Transform cube;
    float noiseValue0;


    public float detailScale = 5;
    public float heightScale = 5;
    public int heightScale1 = 5;
    public int detailScale1 = 5;

    float whatever1 = 10;
    float whatever2 = 10;

    sccsproceduralplanetbuilderGen1 componentParent;
    Transform parentObject;
    Vector3 position;

    public void Start()
    {
        map = new int[width * height * depth];

        this.gameObject.tag = "collisionObject";
        this.gameObject.layer = 8; //"collisionObject"


        parentObject = this.transform.parent;
        componentParent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilderGen1>();

        nodeDiameter = planeSize;
        chunkRadius = planeSize / 2;
        fraction = (int)(1 / (planeSize));
        chunkSize = 1f;
        seed = 3420;
        radius = 5;
        center = Vector3.zero;


        mesh = new Mesh();
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
        this.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;



    }

    int radius = 5;
    Vector3 center;

    public sccsplanetchunkGen1()
    {



    }


    public int[] buildchunkmap(Vector3 position_)
    {
        position = position_;
        /*this.gameObject.tag = "collisionObject";

        this.gameObject.layer = 8; //"collisionObject"

        parentObject = this.transform.parent;

        componentParent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilder>();*/

        //noise = new PerlinNoise(Random.Range(1000000, 10000000));



        //meshCollider = GetComponent<MeshCollider>();
        //transform.localScale *= planeSize;

        //map = new byte[width*height*depth];

        map = new int[width * height * depth];


        float offsetDist = 0;

        Vector3 position1 = Vector3.zero;
        float distance1 = Vector3.Distance(position1, center);

        if (position.x < 0 || position.y < 0 || position.z < 0)
        {
            offsetDist = distance1;
        }






        for (int x = 0; x < width; x++)
        {
            float noiseX = Mathf.Abs(((float)(x * planeSize + position.x + seed) / detailScale) * heightScale);

            for (int y = 0; y < height; y++)
            {
                float noiseY = Mathf.Abs(((float)(y * planeSize + position.y + seed) / detailScale) * heightScale);

                for (int z = 0; z < depth; z++)
                {
                    float noiseZ = Mathf.Abs(((float)(z * planeSize + position.z + seed) / detailScale) * heightScale);

                    float posX = x * planeSize + position.x;
                    float posY = y * planeSize + position.y;
                    float posZ = z * planeSize + position.z;

                    Vector3 pos = new Vector3(posX, posY, posZ);

                    float distance = Vector3.Distance(pos, center);

                    int indexOf = x + width * (y + depth * z);

                    /*float temporaryY = 0.1f;
                    float temporaryZ = 0.1f;
                    float temporaryX = 0.1f;

                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                    float size0 = (1 / planeSize) * position.y;
                    temporaryY -= size0;


                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                    float size1 = (1 / planeSize) * position.x;
                    temporaryX -= size1;

                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);
                    float size2 = (1 / planeSize) * position.z;
                    temporaryZ -= size2;


                    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                    {
                        map[x, y, z] = 1;
                    }*/
                    //map[x, y, z] = 1;

                    /*float temporaryY = 1f;
                    float temporaryZ = 0.1f;
                    float temporaryX = 0.1f;


                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                    float size0 = (1 / planeSize) * position.y;
                    temporaryY -= size0;


                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                    float size1 = (1 / planeSize) * position.x;
                    temporaryX -= size1;

                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);

                    float size2 = (1 / planeSize) * position.z;
                    temporaryZ -= size2;*/


                    /*if ((int)Mathf.Round(temporaryY) >= y )
                    {
                        map[x, y, z] = 1;
                    }*/

                    /*if ((int)Mathf.Round(temporaryY) >= 0)
                    {
                        map[x, y, z] = 1;
                    }*/



                    //if (distance1 >= 0 && distance1 < 19 )
                    {
                        if (distance <= radiusplanetcoreend)
                        {
                            map[indexOf] = 1;
                        }

                        else if (distance > radiusplanetcoreend && distance <= radiusplanetcavesend)
                        {
                            float noiseValue0 = Noise.Generate(noiseX, noiseY, noiseZ);
                            if (noiseValue0 > 0.2f)
                            {
                                map[indexOf] = 1;
                            }
                        }

                        else if (distance >= radiusplanetcavesend && distance <= radiusplanetcrustend)
                        {
                            map[indexOf] = 1;
                        }

                        else if (distance > radiusplanetcrustend && distance < radiusplanetmountainend + offsetDist)
                        {


                            float temporaryY = 10;
                            float temporaryZ = 10;
                            float temporaryX = 10;

                            if (position.y < 0 && position.x < 0 && position.z < 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                                float size0 = (1 / planeSize) * position.y;
                                temporaryY -= size0;

                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                                float size1 = (1 / planeSize) * position.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);
                                float size2 = (1 / planeSize) * position.z;
                                temporaryZ -= size2;

                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[indexOf] = 1;
                                }
                            }

                            else if (position.y >= 0 && position.x >= 0 && position.z >= 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[indexOf] = 1;
                                }
                            }

                            else if (position.y >= 0 && position.x < 0 && position.z >= 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position.y;
                                temporaryY -= size0;

                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[indexOf] = 1;
                                }
                            }


                            else if (position.y >= 0 && position.x >= 0 && position.z < 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[indexOf] = 1;
                                }
                            }





                            else if (position.y >= 0 && position.x < 0 && position.z < 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position.y;
                                temporaryY -= size0;


                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[indexOf] = 1;
                                }
                            }



                            else if (position.y < 0 && position.x >= 0 && position.z < 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[indexOf] = 1;
                                }
                            }





                            else if (position.y < 0 && position.x >= 0 && position.z >= 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                                float size0 = (1 / planeSize) * position.y;
                                temporaryY -= size0;

                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                                float size1 = (1 / planeSize) * position.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);
                                float size2 = (1 / planeSize) * position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[indexOf] = 1;
                                }
                            }





                            else if (position.y < 0 && position.x < 0 && position.z >= 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * position.y;
                                temporaryY -= size0;


                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * position.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * position.z;
                                temporaryZ -= size2;

                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[indexOf] = 1;
                                }
                            }
                            else
                            {
                                map[indexOf] = 0;

                            }


                            ////(position.y < 0 && position.x < 0 && position.z < 0)
                            ////position.y >= 0 && position.x >= 0 && position.z >= 0)
                            ////position.y >= 0 && position.x < 0 && position.z >= 0)
                            ////(position.y >= 0 && position.x >= 0 && position.z < 0)
                            ////(position.y >= 0 && position.x < 0 && position.z < 0)
                            ////(position.y < 0 && position.x >= 0 && position.z < 0)
                            ////(position.y < 0 && position.x >= 0 && position.z >= 0)
                            ////(position.y < 0 && position.x < 0 && position.z >= 0)














                            /*if (position.y < 0)
                            {
                                float size0 = (1 / planeSize) * position.y;
                                temporaryY -= size0;
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryY) <= y)
                                {
                                    map[x, y, z] = 1;
                                }
                            }
                            else
                            {
                                float size0 = (1 / planeSize) * position.y;
                                temporaryY -= size0;
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryY) >= y )
                                {
                                    map[x, y, z] = 1;
                                }
                            }

                            if (position.x < 0)
                            {
                                float size1 = (1 / planeSize) * position.x;
                                temporaryX -= size1;
                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryX) <= x)
                                {
                                    map[x, y, z] = 1;
                                }
                            }
                            else
                            {
                                float size1 = (1 / planeSize) * position.x;
                                temporaryX -= size1;
                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryX) <= x)
                                {
                                    map[x, y, z] = 1;
                                }
                            }



                            if (position.z < 0)
                            {
                                float size2 = (1 / planeSize) * position.z;
                                temporaryZ -= size2;
                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryZ) <= z)
                                {
                                    map[x, y, z] = 1;
                                }
                            }
                            else
                            {
                                float size2 = (1 / planeSize) * position.z;
                                temporaryZ -= size2;
                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[x, y, z] = 1;
                                }
                            }*/

                        }
                    }
                }
            }
        }
        return map;
        //GetComponent<sccsplanetchunk>().enabled = false;
    }





    public void buildMesh()
    {

        this.gameObject.GetComponent<MeshFilter>().mesh.Clear();
        this.gameObject.GetComponent<MeshFilter>().mesh.vertices = verts.ToArray();
        this.gameObject.GetComponent<MeshFilter>().mesh.triangles = tris.ToArray();
        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = mesh;
        this.gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        this.gameObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();

        //to readd
        //to readd
        //to readd
        /* if (this.gameObject.GetComponent<MeshCollider>() == null)
         {
             this.gameObject.AddComponent<MeshCollider>();
         }
         else
         {
             Destroy(this.gameObject.GetComponent<MeshCollider>());
             this.gameObject.AddComponent<MeshCollider>();

         }*/
        //to readd
        //to readd
        //to readd

    }




    public void Regenerate()
    {
        verts.Clear();
        tris.Clear();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    //block = map[x + width * (y + depth * z)];
                    int indexOf = x + width * (y + depth * z);
                    block = map[indexOf];

                    if (block == 0) continue;
                    {
                        DrawBrick(x, y, z);
                    }
                    //Instantiate(sphere, new Vector3(x*planeSize, y * planeSize, z * planeSize) +position, Quaternion.identity);
                }
            }
        }
    }

    //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;
    //Instantiate(sphere, new Vector3(xx, yy, zz) + terrain.getChunkPos(chuk.position.x, chuk.position.y, chuk.position.z).position, Quaternion.identity);

    public void DrawBrick(int x, int y, int z)
    {
        //Debug.Log(map[x,y,z]);

        Vector3 start = new Vector3(x * planeSize, y * planeSize, z * planeSize);
        Vector3 offset1, offset2;



        /*//TOPFACE
        if (IsTransparent(x, y + 1, z))
        {
            offset1 = Vector3.forward * planeSize;
            offset2 = Vector3.right * planeSize;
            DrawFace(start + Vector3.up * planeSize, offset1, offset2);
        }

        //LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            offset1 = Vector3.back * planeSize;
            offset2 = Vector3.down * planeSize;
            DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2);
        }

        //RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            offset1 = Vector3.up * planeSize;
            offset2 = Vector3.forward * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
        }
        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            offset1 = Vector3.left * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
        }
        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.forward * planeSize, offset1, offset2);
        }
        //BOTTOMFACE
        if (IsTransparent(x, y - 1, z))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.forward * planeSize;
            DrawFace(start, offset1, offset2);
        }*/








        //TOPFACE
        if (y == height - 1)
        {
            if (componentParent.getChunk((int)(position.x), (int)Mathf.Round(position.y + 4), (int)(position.z)) != null)
            {
                mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x), (int)(position.y + 4), (int)(position.z));

                if (chunkdata != null)
                {
                    var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunkGen1>();

                    /*if (comp != null)
                    {
                        if (comp.IsTransparent(x, 0, z))
                        {
                            offset1 = Vector3.forward * planeSize;
                            offset2 = Vector3.right * planeSize;
                            DrawFace(start + Vector3.up * planeSize, offset1, offset2);
                        }
                    }*/
                    /*else
                    {
                        //TOPFACE
                        if (IsTransparent(x, height - 1, z))
                        {
                            offset1 = Vector3.forward * planeSize;
                            offset2 = Vector3.right * planeSize;
                            DrawFace(start + Vector3.up * planeSize, offset1, offset2);
                        }
                    }*/
                }
                /*else
                {
                    //TOPFACE
                    if (IsTransparent(x, height - 1, z))
                    {
                        offset1 = Vector3.forward * planeSize;
                        offset2 = Vector3.right * planeSize;
                        DrawFace(start + Vector3.up * planeSize, offset1, offset2);
                    }
                }*/
            }
            /*else
            {
                //TOPFACE
                if (IsTransparent(x, height - 1, z))
                {
                    offset1 = Vector3.forward * planeSize;
                    offset2 = Vector3.right * planeSize;
                    DrawFace(start + Vector3.up * planeSize, offset1, offset2);
                }
            }*/
        }
        else if (y == 0)
        {

            if (componentParent.getChunk((int)(position.x), (int)(position.y - 4), (int)(position.z)) != null)
            {
                mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x), (int)(position.y - 4), (int)(position.z));

                if (chunkdata != null)
                {
                    var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunkGen1>();

                    if (comp != null)
                    {
                        if (comp.IsTransparent(x, height - 1, z))
                        {
                            offset1 = Vector3.forward * planeSize;
                            offset2 = Vector3.right * planeSize;
                            DrawFace(start + Vector3.up * planeSize, offset1, offset2);
                        }
                    }
                    else
                    {
                        /*//TOPFACE
                        if (IsTransparent(x, 0, z))
                        {
                            offset1 = Vector3.forward * planeSize;
                            offset2 = Vector3.right * planeSize;
                            DrawFace(start + Vector3.up * planeSize, offset1, offset2);
                        }*/
                    }
                }
                else
                {
                    /*//TOPFACE
                    if (IsTransparent(x, 0, z))
                    {
                        offset1 = Vector3.forward * planeSize;
                        offset2 = Vector3.right * planeSize;
                        DrawFace(start + Vector3.up * planeSize, offset1, offset2);
                    }*/
                }
            }
            else
            {
                /*//TOPFACE
                if (IsTransparent(x, 0, z))
                {
                    offset1 = Vector3.forward * planeSize;
                    offset2 = Vector3.right * planeSize;
                    DrawFace(start + Vector3.up * planeSize, offset1, offset2);
                }*/
            }
        }
        else
        {
            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = Vector3.forward * planeSize;
                offset2 = Vector3.right * planeSize;
                DrawFace(start + Vector3.up * planeSize, offset1, offset2);
            }
        }









        /*//RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            offset1 = Vector3.up * planeSize;
            offset2 = Vector3.forward * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
        }*/



        /*
        //RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            offset1 = Vector3.up * planeSize;
            offset2 = Vector3.forward * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
        }
        */
        /*
        //RIGHTFACE
        if (x == width - 1) // && x != 0
        {
            if (componentParent.getChunk((int)(position.x + 4), (int)(position.y), (int)(position.z)) != null)
            {
                mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x + 4), (int)(position.y), (int)(position.z));

                //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                if (chunkdata != null)
                {
                    var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunk>();

                    if (comp != null)
                    {
                        if (comp.IsTransparent(0, y, z))
                        {
                            offset1 = Vector3.up * planeSize;
                            offset2 = Vector3.forward * planeSize;
                            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                        }
                    }
                    else
                    {
                        if (IsTransparent(width - 1, y, z))
                        {
                            offset1 = Vector3.up * planeSize;
                            offset2 = Vector3.forward * planeSize;
                            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                        }
                    }
                }
                else
                {
                    if (IsTransparent(width - 1, y, z))
                    {
                        offset1 = Vector3.up * planeSize;
                        offset2 = Vector3.forward * planeSize;
                        DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                    }
                }
            }
            else
            {
                if (IsTransparent(width - 1, y, z))
                {
                    offset1 = Vector3.up * planeSize;
                    offset2 = Vector3.forward * planeSize;
                    DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                }
            }
        }
        else if(x == 0)
        {
            if (componentParent.getChunk((int)(position.x - 4), (int)(position.y), (int)(position.z)) != null)
            {
                mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x - 4), (int)(position.y), (int)(position.z));

                //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                if (chunkdata != null)
                {
                    var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunk>();

                    if (comp != null)
                    {
                        if (comp.IsTransparent(width - 1, y, z))
                        {
                            offset1 = Vector3.up * planeSize;
                            offset2 = Vector3.forward * planeSize;
                            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                        }
                    }
                    else
                    {
                        if (IsTransparent(0, y, z))
                        {
                            offset1 = Vector3.up * planeSize;
                            offset2 = Vector3.forward * planeSize;
                            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                        }
                    }
                }
                else
                {
                    if (IsTransparent(0, y, z))
                    {
                        offset1 = Vector3.up * planeSize;
                        offset2 = Vector3.forward * planeSize;
                        DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                    }
                }
            }
            else
            {
                if (IsTransparent(0, y, z))
                {
                    offset1 = Vector3.up * planeSize;
                    offset2 = Vector3.forward * planeSize;
                    DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                }
            }
          
        }
        else
        {
            if (IsTransparent(x+1, y, z))
            {
                offset1 = Vector3.up * planeSize;
                offset2 = Vector3.forward * planeSize;
                DrawFace(start + Vector3.right * planeSize, offset1, offset2);
            }
        }*/









































        /*
        //TOPFACE
        if (IsTransparent(x, y + 1, z))
        {
            offset1 = Vector3.forward * planeSize;
            offset2 = Vector3.right * planeSize;
            DrawFace(start + Vector3.up * planeSize, offset1, offset2);
        }


        
        if (x != 0)
        {
            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                offset1 = Vector3.back * planeSize;
                offset2 = Vector3.down * planeSize;
                DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2);
            }
        }

        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            offset1 = Vector3.left * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
        }
        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.forward * planeSize, offset1, offset2);
        }
        //BOTTOMFACE
        if (IsTransparent(x, y - 1, z))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.forward * planeSize;
            DrawFace(start, offset1, offset2);
        }*/





        /*else if (x == width - 1)
        {
            if (componentParent.getChunk((int)(position.x + 4), (int)(position.y), (int)(position.z)) != null)
            {
                mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x + 4), (int)(position.y), (int)(position.z));

                //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                if (chunkdata != null)
                {
                    var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunk>();

                    if (comp != null)
                    {
                        if (comp.IsTransparent(0, y, z))
                        {
                            offset1 = Vector3.up * planeSize;
                            offset2 = Vector3.forward * planeSize;
                            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                        }
                    }
                }
            }
        }*/
        /*else if (x == 0)
        {
            if (componentParent.getChunk((int)(position.x - 4), (int)(position.y), (int)(position.z)) != null)
            {
                mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x - 4), (int)(position.y), (int)(position.z));

                //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                if (chunkdata != null)
                {
                    var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunk>();

                    if (comp != null)
                    {
                        if (comp.IsTransparent(width - 1, y, z))
                        {
                            offset1 = Vector3.up * planeSize;
                            offset2 = Vector3.forward * planeSize;
                            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                        }
                    }
                }
            }
        }*/



        /*
        //LEFTFACE
        if (x != 0)
        {
            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                offset1 = Vector3.back * planeSize;
                offset2 = Vector3.down * planeSize;
                DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2);
            }
        }
        else if (x == 0)
        {
            if (componentParent.getChunk((int)(position.x - 4), (int)(position.y), (int)(position.z)) != null)
            {
                mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x - 4), (int)(position.y), (int)(position.z));

                float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                if (chunkdata != null)
                {
                    var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunk>();

                    if (comp != null)
                    {
                        if (comp.IsTransparent(width - 1, y, z))
                        {
                            offset1 = Vector3.back * planeSize;
                            offset2 = Vector3.down * planeSize;
                            DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2);
                        }
                    }
                }
            }
        }










        //FRONTFACE
        if (z == 0 && componentParent.getChunk((int)(position.x), (int)(position.y), (int)(position.z - 4)) != null)
        {
            mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x), (int)(position.y), (int)(position.z - 4));

            if (chunkdata != null)
            {
                var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunk>();

                if (comp != null)
                {
                    if (comp.IsTransparent(x, y, depth - 1))
                    {
                        offset1 = Vector3.left * planeSize;
                        offset2 = Vector3.up * planeSize;
                        DrawFace(start + Vector3.right * planeSize, offset1, offset2);
                    }
                }
            }
        }

        else if (z != 0)
        {
            //FRONTFACE
            if (IsTransparent(x, y, z - 1))
            {
                offset1 = Vector3.left * planeSize;
                offset2 = Vector3.up * planeSize;
                DrawFace(start + Vector3.right * planeSize, offset1, offset2);
            }
        }

        //BACKFACE
        if (z == width - 1 && componentParent.getChunk((int)(position.x), (int)(position.y), (int)(position.z + 4)) != null)
        {
            mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x), (int)(position.y), (int)(position.z + 4));

            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

            if (chunkdata != null)
            {
                var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunk>();

                if (comp != null)
                {
                    if (comp.IsTransparent(x, y, 0))
                    {
                        offset1 = Vector3.right * planeSize;
                        offset2 = Vector3.up * planeSize;
                        DrawFace(start + Vector3.forward * planeSize, offset1, offset2);
                    }
                }
            }
        }

        else if (z != width - 1)
        {
            //BACKFACE
            if (IsTransparent(x, y, z + 1))
            {
                offset1 = Vector3.right * planeSize;
                offset2 = Vector3.up * planeSize;
                DrawFace(start + Vector3.forward * planeSize, offset1, offset2);
            }
        }






        //TOPFACE
        if (y == height - 1 && componentParent.getChunk((int)(position.x), (int)(position.y + 4), (int)(position.z)) != null)
        {
            mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x), (int)(position.y + 4), (int)(position.z));

            if (chunkdata != null)
            {
                var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunk>();

                if (comp != null)
                {
                    if (comp.IsTransparent(x, 0, z))
                    {
                        offset1 = Vector3.forward * planeSize;
                        offset2 = Vector3.right * planeSize;
                        DrawFace(start + Vector3.up * planeSize, offset1, offset2);
                    }
                }
            }
        }

        else if (y != height - 1)
        {
            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = Vector3.forward * planeSize;
                offset2 = Vector3.right * planeSize;
                DrawFace(start + Vector3.up * planeSize, offset1, offset2);
            }
        }

        //BOTTOMFACE
        if (y == 0 && componentParent.getChunk((int)(position.x), (int)(position.y - 4), (int)(position.z)) != null)
        {
            mainChunkGen1 chunkdata = componentParent.getChunk((int)(position.x), (int)(position.y - 4), (int)(position.z));

            if (chunkdata != null)
            {
                var comp = chunkdata.planetchunk.GetComponent<sccsplanetchunk>();

                if (comp != null)
                {
                    if (comp.IsTransparent(x, height - 1, z))
                    {
                        offset1 = Vector3.right * planeSize;
                        offset2 = Vector3.forward * planeSize;
                        DrawFace(start, offset1, offset2);
                    }
                }
            }
        }
        else if (y != 0)
        {
            //BOTTOMFACE
            if (IsTransparent(x, y - 1, z))
            {
                offset1 = Vector3.right * planeSize;
                offset2 = Vector3.forward * planeSize;
                DrawFace(start, offset1, offset2);
            }
        }*/
    }

    public void DrawFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        int index = verts.Count;

        verts.Add(start);
        verts.Add(start + offset1);
        verts.Add(start + offset2);
        verts.Add(start + offset1 + offset2);

        tris.Add(index + 0);
        tris.Add(index + 1);
        tris.Add(index + 2);
        tris.Add(index + 3);
        tris.Add(index + 2);
        tris.Add(index + 1);
    }

    public void SetByte(int x, int y, int z, byte block)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            //Debug.Log("out of range");
            return;
        }
        int indexOf = x + width * (y + depth * z);

        map[indexOf] = block;

        /*if (this.gameObject.GetComponent<MeshCollider>() != null)
        {
            Destroy(this.gameObject.GetComponent<MeshCollider>());
        }*/
        //Destroy(this.gameObject.GetComponent<MeshFilter>());

        //verts.Clear();
        //tris.Clear();

        //Regenerate();

        //return map[x + width * (y + depth * z)];
    }





    public void SetBrick(int x, int y, int z, byte block)
    {
        //Debug.Log(x + " " + y + " " + z);

        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= width) || (z >= width))
        {
            return;
        }
        //Debug.Log(x + " " + y + " " + z);

        /*if (x > 0 && x < width)
        {
            if (map[x, y, z] != block)
            {
                map[x, y, z] = block;          
                Regenerate();
            }
        }*/

        /*if (map[x, y, z] != block)
        {
            map[x, y, z] = block;
            Regenerate();
        }

        if (x == width - 1)
        {
            if (terrain.getChunk(position.x + 1, position.y, position.z) != null)
            {
                chunky chuk = terrain.getChunk(position.x + 1, position.y, position.z);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (x == 0)
        {
            if (terrain.getChunk(position.x - 1, position.y, position.z) != null)
            {
                chunky chuk = terrain.getChunk(position.x - 1, position.y, position.z);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (z == width - 1)
        {
            if (terrain.getChunk(position.x, position.y, position.z + 1) != null)
            {
                chunky chuk = terrain.getChunk(position.x, position.y, position.z + 1);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (z == 0)
        {
            if (terrain.getChunk(position.x, position.y, position.z - 1) != null)
            {
                chunky chuk = terrain.getChunk(position.x, position.y, position.z - 1);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }*/
    }




    public bool IsTransparent(int x, int y, int z)
    {
        int indexOf = x + width * (y + depth * z);

        if (indexOf < 16 * 16 * 16)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth))// return true;
            {

                return true;
            }
            else
            {
                if (map != null)
                {

                    return map[indexOf] == 0;
                }
                else
                {
                    return true;
                }

            }
        }
        return true;

    }


    public int GetByte(int x, int y, int z)
    {
        int indexOf = x + width * (y + depth * z);

        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            return 0;
        }
        return map[indexOf];
        //return map[x + width * (y + depth * z)];
    }







    /*void Update()
    {
        //Debug.Log(mesh.vertices.Length);
        /*if (mesh.vertices.Length > 65000)
        {
            map = new byte[(int)width, (int)width, (int)width];
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Regenerate();
        }
    }*/


    /* void checkBytePos()
     {
         /*for (int x = 0; x < width; x++)
         {
             for (int y = 0; y < height; y++)
             {
                 for (int z = 0; z < depth; z++)
                 {
                     Instantiate(cube, new Vector3(x, y, z) * planeSize, Quaternion.identity);

                 }
             }
         }
     }*/



    void checkBytePos()
    {
        float xPosition = (position.x);
        float yPosition = (position.y);
        float zPosition = (position.z);

        int xPose;
        int yPose;
        int zPose;

        if (xPosition < 0)
        {
            xPose = (int)Mathf.Ceil(xPosition);
        }

        else
        {
            xPose = (int)Mathf.Floor(xPosition);
        }

        if (yPosition < 0)
        {
            yPose = (int)Mathf.Ceil(yPosition);
        }

        else
        {
            yPose = (int)Mathf.Floor(yPosition);
        }

        if (zPosition < 0)
        {
            zPose = (int)Mathf.Ceil(zPosition);
        }

        else
        {
            zPose = (int)Mathf.Floor(zPosition);
        }




        /*if (xPosition < 0)
        {
            xPose = (int)Mathf.Round(xPosition);
            if (xPose % 2 != 0)
            {
                xPose -= currentWidth;
            }
        }

        else
        {
            xPose = (int)Mathf.Floor(xPosition);
            if (xPose % 2 != 0)
            {
                xPose += currentWidth;
            }
        }

        if (yPosition < 0)
        {
            yPose = (int)Mathf.Round(yPosition);
            if (yPose % 2 != 0)
            {
                yPose -= currentWidth;
            }
        }

        else
        {
            yPose = (int)Mathf.Floor(yPosition);
            if (yPose % 2 != 0)
            {
                yPose += currentWidth;
            }
        }

        if (zPosition < 0)
        {
            zPose = (int)Mathf.Round(zPosition);
            if (zPose % 2 != 0)
            {
                zPose -= currentWidth;
            }
        }

        else
        {
            zPose = (int)Mathf.Floor(zPosition);
            if (zPose % 2 != 0)
            {
                zPose += currentWidth;
            }
        }*/

        for (int x = xPose - width / 2; x < xPose + width / 2; x++)
        {
            for (int y = yPose - width / 2; y < yPose + width / 2; y++)
            {
                for (int z = zPose - width / 2; z < zPose + width / 2; z++)
                {
                    int xPos = (x);
                    int yPos = (y);
                    int zPos = (z);

                    //Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);

                    if (x < 0)
                    {
                        int yo = (int)Mathf.Ceil(-x / divider);
                        int yo1 = x + (yo * divider);
                        xPos = divider;
                        xPos += -yo;
                    }
                    else
                    {
                        int yo = (int)Mathf.Floor(x / divider);
                        xPos = x - (yo * divider);
                    }
                    if (y < 0)
                    {
                        int yo = (int)Mathf.Ceil(-y / divider);
                        int yo1 = y + (yo * divider);
                        yPos = divider;
                        yPos += -yo1;
                    }
                    else
                    {
                        int yo = (int)Mathf.Floor(y / divider);
                        yPos = y - (yo * divider);
                    }
                    if (z < 0)
                    {
                        int yo = (int)Mathf.Ceil(-z / divider);
                        int yo1 = z + (yo * divider);
                        zPos = divider;
                        zPos += -yo1;
                    }
                    else
                    {
                        int yo = (int)Mathf.Floor(z / divider);
                        zPos = z - (yo * divider);
                    }

                    //Debug.Log(xPos + " " + yPos + " " + zPos);
                    //Instantiate(cube, new Vector3(xPos,yPos,zPos)+position, Quaternion.identity);

                }
            }
        }
    }















    //Flat[x + WIDTH * (y + DEPTH * z)]










    /*void OnDrawGizmos()
    {

        if (mesh.vertices == null)
        {
            return;
        }

        Gizmos.color = Color.black;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(new Vector3(mesh.vertices[i].x + position.x, mesh.vertices[i].y + position.y, mesh.vertices[i].z + position.z), 0.01f);
        }


    }*/
}






/*if (x == 0)
        {
            Instantiate(sphere, new Vector3(x * planeSize + position.x, y * planeSize + position.y, z * planeSize + position.z), Quaternion.identity);
        }
        else if (y == 0)
        {
            Instantiate(sphere, new Vector3(x * planeSize + position.x, y * planeSize + position.y, z * planeSize + position.z), Quaternion.identity);
        }
        else if(z == 0)
        {
            Instantiate(sphere, new Vector3(x * planeSize + position.x, y * planeSize + position.y, z * planeSize + position.z), Quaternion.identity);
        }


        else if (x == width-1)
        {
            Instantiate(sphere, new Vector3(x * planeSize + position.x, y * planeSize + position.y, z * planeSize + position.z), Quaternion.identity);
        }
        else if (y == width - 1)
        {
            Instantiate(sphere, new Vector3(x * planeSize + position.x, y * planeSize + position.y, z * planeSize + position.z), Quaternion.identity);
        }
        else if (z == width - 1)
        {
            Instantiate(sphere, new Vector3(x * planeSize + position.x, y * planeSize + position.y, z * planeSize + position.z), Quaternion.identity);
        }*/







/*public void SetBrick(int x, int y, int z, byte block)
{
    //int x = Mathf.RoundToInt();

    //x -= (int)Mathf.RoundToInt(position.x);
    //y -= (int)Mathf.RoundToInt(position.y);
    //z -= (int)Mathf.RoundToInt(position.z);

    //x -= (int)Mathf.RoundToInt(position.x);
    //y -= (int)Mathf.RoundToInt(position.y);
    //z -= (int)Mathf.RoundToInt(position.z);

    // int x = 


    //Debug.Log(xx);
    //Debug.Log(yy);
    //Debug.Log(zz);

    Debug.Log("yo");

    if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= width) || (z >= width))
    {
        return;
    }
    if (map[x, y, z] != block)
    {
        map[x, y, z] = block;
        Regenerate();
    }
}*/









/*if (x == width - 1)
{
    Debug.Log("yo0");

    chunk chuk = terrain.getChunkPos(position.x + 1, position.y, position.z);

    if (chuk.GetByte(0, y, z) == 1)
    {
        Debug.Log("yo1");

        if (chuk.map[width - 1, y, z] != block)
        {
            Debug.Log("yo2");

            chuk.map[0, y, z] = block;
            chuk.Regenerate();
        }
    }           
}*/







/*//RIGHTFACE
   if (x == width - 1 && terrain.getChunkPos(position.x + 1, position.y, position.z) != null)
   {
       chunk chuk = terrain.getChunkPos(position.x + 1, position.y, position.z);

       float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
       float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
       float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

       if (chuk.GetByte(0, y, z) == 0)
       {
           //Instantiate(sphere, new Vector3(xx, yy, zz) + terrain.getChunkPos(position.x, position.y, position.z).position, Quaternion.identity);
           //RIGHTFACE
           if (IsTransparent(x + 1, y, z))
           {
               offset1 = Vector3.up * planeSize;
               offset2 = Vector3.forward * planeSize;
               DrawFace(start + Vector3.right * planeSize, offset1, offset2, block);
           }             
       }         
   }
   else if (x != width - 1)
   {
       //RIGHTFACE
       if (IsTransparent(x + 1, y, z))
       {
           offset1 = Vector3.up * planeSize;
           offset2 = Vector3.forward * planeSize;
           DrawFace(start + Vector3.right * planeSize, offset1, offset2, block);
       }
   }*/

/*//LEFTFACE
if (x == 0 && terrain.getChunkPos(position.x - 1, position.y, position.z) != null)
{
    chunk chuk = terrain.getChunkPos(position.x - 1, position.y, position.z);

    //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;
    if (chuk.GetByte(width-1, y, z) == 0)
    {
        //LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            offset1 = Vector3.back * planeSize;
            offset2 = Vector3.down * planeSize;
            DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, block);
        }
    }

}
else if (x != 0)
{
    //LEFTFACE
    if (IsTransparent(x - 1, y, z))
    {
        offset1 = Vector3.back * planeSize;
        offset2 = Vector3.down * planeSize;
        DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, block);
    }
}*/


/*//FRONTFACE
if (z == 0 && terrain.getChunkPos(position.x , position.y, position.z-1) != null)
{
    chunk chuk = terrain.getChunkPos(position.x , position.y, position.z-1);

    float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

    if (chuk.GetByte(x, y, width-1) == 0)
    {
    //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;
        //Instantiate(sphere, new Vector3(xx, yy, zz) + terrain.getChunkPos(position.x, position.y, position.z).position, Quaternion.identity);
        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            offset1 = Vector3.left * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2, block);
        }
    }
}
else if (z != 0)
{
    //FRONTFACE
    if (IsTransparent(x, y, z - 1))
    {
        offset1 = Vector3.left * planeSize;
        offset2 = Vector3.up * planeSize;
        DrawFace(start + Vector3.right * planeSize, offset1, offset2, block);
    }
}


//BACKFACE
if (z == width-1 && terrain.getChunkPos(position.x, position.y, position.z + 1) != null)
{
    chunk chuk = terrain.getChunkPos(position.x, position.y, position.z + 1);

    float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

    if (chuk.GetByte(x, y, 0) == 0)
    {
        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.forward * planeSize, offset1, offset2, block);
        }
    }
}
else if (z != width - 1)
{
    //BACKFACE
    if (IsTransparent(x, y, z + 1))
    {
        offset1 = Vector3.right * planeSize;
        offset2 = Vector3.up * planeSize;
        DrawFace(start + Vector3.forward * planeSize, offset1, offset2, block);
    }
}*/



/*//BOTTOMFACE
if (IsTransparent(x, y - 1, z))
{
    offset1 = Vector3.right * planeSize;
    offset2 = Vector3.forward * planeSize;
    DrawFace(start, offset1, offset2, block);
}*/






//Generate basic terrain sine
/*  int[] terrainContour;
                    int Widther = 8;
                    int Heighter = 8;
                    terrainContour = new int[Widther * Heighter];

                    //Make Random Numbers
                    //double rand1 = randomizer.NextDouble() + 1;
                    //double rand2 = randomizer.NextDouble() + 2;
                    // double rand3 = randomizer.NextDouble() + 3;
                    //double rand1 = Random.Range(1, 10);
                    //double rand2 = Random.Range(1, 10);
                    //double rand3 = Random.Range(1, 10);


                    double rand1 = Mathf.Round(Noise.Generate(noiseX, noiseY, noiseZ));
                    double rand2 = Mathf.Round(Noise.Generate(noiseY, noiseZ, noiseX));
                    double rand3 = Mathf.Round(Noise.Generate(noiseZ, noiseX, noiseY));
  //Variables, Play with these for unique results!
                    //float peakheight = 20;
                    //float flatness = 50;
                    //int offset = 30;

                    float peakheight = 1;
                    float flatness = 25;
                    int offset = 15;
 * 
 * for (int xxx = 0; xxx < Widther; xxx++)
{
    double height = peakheight / rand1 * Mathf.Sin((float)(xxx / flatness * rand1 + rand1));
    height += peakheight / rand2 * Mathf.Sin((float)(xxx / flatness * rand2 + rand2));
    height += peakheight / rand3 * Mathf.Sin((float)(xxx / flatness * rand3 + rand3));

    height += offset;

    terrainContour[x] = (int)height;
}

if (y < terrainContour[x])
    map[x, y, z] = 1;
else
    map[x, y, z] = 0;*/


/*for (int xxxx = 0; xxxx < Widther; xxxx++)
{
    for (int yyyy = 0; yyyy < Heighter; yyyy++)
    {

        ///tiles[x, y] = Blank Tile
    }
}*/











/*float noiseValue0 = Noise.Generate(noiseX, noiseY, noiseZ);

if (noiseValue0 > 0.2f)
{
if (Mathf.Round(noiseValue0) + y == y && Mathf.Round(noiseValue0) + x == x && Mathf.Round(noiseValue0) + z == z)
{
    map[x, y, z] = 1;
}
}*/






/*if (test.getChunk(position.x, position.y + 1, position.z) != null)
{
float noiseXX = Mathf.Abs(((float)(x * planeSize + position.x + seed) / detailScale) * heightScale);
float noiseYY = Mathf.Abs(((float)(y * planeSize + position.y + 1 + seed) / detailScale) * heightScale);
float noiseZZ = Mathf.Abs(((float)(z * planeSize + position.z + seed) / detailScale) * heightScale);
float heightingo = Noise.Generate(noiseXX, noiseYY, noiseZZ);
heightingo += (10f - (float)y) / 10;
heightingo /= (float)y / 5;
mainChunkGen1 chuk = test.getChunk(position.x, position.y + 1, position.z);
if (heightingo >= 0.2f)
{
chuk.chunker.GetComponent<chunku>().map[x, y, z] = 1;
}
}*/





/*temporaryX *= (Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
                           temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);
                           temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);


                           float size0 = (1 / planeSize) * position.y;
                           temporaryY -= size0;

                           /*float size1 = (1 / planeSize) * position.x;
                           temporaryX -= size1;

                           float size2 = (1 / planeSize) * position.z;
                           temporaryZ -= size2;

                           if ((int)Mathf.Round(temporaryY) < y )
                           {
                               map[x, y, z] = 1;
                           }*/












/*if (position.y < 0)
{
    float size0 = (1 / planeSize) * position.y;
    temporaryY -= size0;
    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryY) <= y)
    {
        map[x, y, z] = 1;
    }
}
else
{
    float size0 = (1 / planeSize) * position.y;
    temporaryY -= size0;
    temporaryY *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryY) >= y )
    {
        map[x, y, z] = 1;
    }
}

if (position.x < 0)
{
    float size1 = (1 / planeSize) * position.x;
    temporaryX -= size1;
    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryX) <= x)
    {
        map[x, y, z] = 1;
    }
}
else
{
    float size1 = (1 / planeSize) * position.x;
    temporaryX -= size1;
    temporaryX *= (Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryX) <= x)
    {
        map[x, y, z] = 1;
    }
}



if (position.z < 0)
{
    float size2 = (1 / planeSize) * position.z;
    temporaryZ -= size2;
    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryZ) <= z)
    {
        map[x, y, z] = 1;
    }
}
else
{
    float size2 = (1 / planeSize) * position.z;
    temporaryZ -= size2;
    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryZ) >= z)
    {
        map[x, y, z] = 1;
    }
}*/



/*if (position.y < 0 || position.x < 0 || position.z < 0)
{
    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

    float size0 = (1 / planeSize) * position.y;
    temporaryY -= size0;

    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

    float size1 = (1 / planeSize) * position.x;
    temporaryX -= size1;
    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);

    float size2 = (1 / planeSize) * position.z;
    temporaryZ -= size2;
    if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
    {
        map[x, y, z] = 1;
    }                            
}*/

/*if (position.y >= 0 || position.x >= 0 || position.z >= 0)
{
    temporaryY *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

    float size0 = (1 / planeSize) * position.y;
    temporaryY -= size0;


    temporaryX *= (Mathf.PerlinNoise((y * planeSize + position.y + seed) / detailScale1, (z * planeSize + position.z + seed) / detailScale1) * heightScale1);

    float size1 = (1 / planeSize) * position.x;
    temporaryX -= size1;

    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + position.x + seed) / detailScale1, (y * planeSize + position.y + seed) / detailScale1) * heightScale1);

    float size2 = (1 / planeSize) * position.z;
    temporaryZ -= size2;


    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
    {
        map[x, y, z] = 1;
    }
}*/


















