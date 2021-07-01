using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimplexNoise;
using System.Globalization;
/*using CoherentNoise;
using CoherentNoise.Generation;
using CoherentNoise.Generation.Displacement;
using CoherentNoise.Generation.Fractal;
using CoherentNoise.Generation.Modification;
using CoherentNoise.Generation.Patterns;
using CoherentNoise.Texturing;*/

//[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class chunkonew : MonoBehaviour
{
    public float planetmountainperlinmulX = 1.0f;
    public float planetmountainperlinmulY = 1.0f;
    public float planetmountainperlinmulZ = 1.0f;

    private float radiusplanetcorestart = 0.0f;
    private float radiusplanetcoreend = 5.0f;
    private float radiusplanetcavesstart = 5.0f;
    private float radiusplanetcavesend = 9.0f;
    private float radiusplanetcruststart = 9.0f;
    private float radiusplanetcrustend = 11.0f;
    private float radiusplanetmountainstart = 11.0f;
    private float radiusplanetmountainend = 20.0f;

    public int width = 8;
    public int height = 8;
    public int depth = 8;
    public float planeSize = 0.5f;

    //working setting:
    //width16heigth16depth16 and planesize 0.25f
    //width32heigth32depth32 and planesize 0.125f
    //width64heigth64depth64 and planesize 

    //!working setting:
    // 0.125f not working at 64x64x64 
    // 0.25f   

    public int switchPerlinMountainsNotWorking = 1;

    //public byte[] map;
    public byte[,,] map;
    protected Mesh mesh;
    protected List<Vector3> verts = new List<Vector3>();
    protected List<int> tris = new List<int>();
    protected List<Vector2> uv = new List<Vector2>();
    //protected MeshCollider meshCollider;

    public GameObject sphere;
    float seed = 3420;
    byte block;

    float nodeDiameter;
    float chunkRadius;
    float fraction;
    float chunkSize;

    int divider = 10;
    //public Transform cube;

    public float detailScale = 10;
    //public float heightScale;

    public float heightScale = 10;
    float noiseValue0 = 0;

    //PerlinNoise noise;

    public int heightScale1 = 10;
    public int detailScale1 = 10;
    
    string parentNameInfo = "";

    Vector3 chunkPosition = Vector3.zero;
    Vector3 chunkPositionMod = Vector3.zero;


    Transform parentObject;


    sccsproceduralplanetbuilder componentParent;

    public void buildchunkmap()
    {
        parentObject = this.transform.parent;


        componentParent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilder>();



        /*float inputsizecore = 5.0f;
        radiusplanetcoreend *= inputsizecore; // 5.0f
        radiusplanetcavesstart = radiusplanetcoreend; // 5.0f
        radiusplanetcavesend = radiusplanetcavesstart + radiusplanetcavesend;
        radiusplanetmaxcrust = radiusplanetcavesend + 1;*/

        /*parentNameInfo = transform.parent.name;

        int xIndex = parentNameInfo.IndexOf("x"); //0
        int yIndex = parentNameInfo.IndexOf("y"); //
        int zIndex = parentNameInfo.IndexOf("z"); //

        int lengthOfX = yIndex - xIndex;
        int lengthOfY = zIndex - yIndex;
        int lengthOfZ = parentNameInfo.Length - zIndex;

        //Debug.Log("x: " + xIndex + " y: " + yIndex + " z: " + zIndex);

        string chunkposStringX = parentNameInfo.Substring(0 + 1, lengthOfX - 1);
        string chunkposStringY = parentNameInfo.Substring(yIndex + 1, lengthOfY - 1);
        string chunkposStringZ = parentNameInfo.Substring(zIndex + 1, lengthOfZ - 1);

        //Debug.Log("y: " + chunkposStringY);
        //Debug.Log("x: " + chunkposStringX + " y: " + chunkposStringY + " z: " + chunkposStringZ);

        int chunkPosX = int.Parse(chunkposStringX, NumberStyles.AllowLeadingSign);
        int chunkPosY = int.Parse(chunkposStringY, NumberStyles.AllowLeadingSign);
        int chunkPosZ = int.Parse(chunkposStringZ, NumberStyles.AllowLeadingSign);

        chunkPosition.x = chunkPosX;
        chunkPosition.y = chunkPosY;
        chunkPosition.z = chunkPosZ;*/

        //Debug.Log("x: " + transform.localPosition.x + " y: " + transform.localPosition.y + " z: " + transform.localPosition.z);
        //Debug.Log("x: " + transform.position.x + " y: " + transform.position.y + " z: " + transform.position.z);

        //noise = new PerlinNoise(Random.Range(1000000, 10000000));

        nodeDiameter = planeSize;
        chunkRadius = planeSize / 2;
        fraction = (int)(1 / (planeSize));
        chunkSize = 1f;

        //meshCollider = GetComponent<MeshCollider>();
        //transform.localScale *= planeSize;

        //map = new byte[width*height*depth];
        seed = 3420;
        //seed = Random.Range(3000, 4000);

        //seed = 0;
        //checkBytePos();
        int radius = 5;
        Vector3 center = Vector3.zero;

        chunkPositionMod = transform.position;// - transform.parent.position; // + 

        //if (transform.position.y >= 3)
        //{
        map = new byte[width, height, depth];



        for (int x = 0; x < width; x++)
        {
            float noiseX = Mathf.Abs(((float)(x * planeSize + chunkPositionMod.x + seed) / detailScale) * heightScale);

            for (int y = 0; y < height; y++)
            {
                float noiseY = Mathf.Abs(((float)(y * planeSize + chunkPositionMod.y + seed) / detailScale) * heightScale);

                for (int z = 0; z < depth; z++)
                {
                    float noiseZ = Mathf.Abs(((float)(z * planeSize + chunkPositionMod.z + seed) / detailScale) * heightScale);

                    float posX = x * planeSize + chunkPositionMod.x; //transform.position.x;
                    float posY = y * planeSize + chunkPositionMod.y; //transform.position.y;
                    float posZ = z * planeSize + chunkPositionMod.z; //transform.position.z;

                    //Debug.Log(transform.position);

                    Vector3 position = new Vector3(posX, posY, posZ);

                    float distance = Vector3.Distance(position, Vector3.zero); //transform.parent.position

                    Vector3 position1 = chunkPositionMod;// transform.position;

                    float distance1 = Vector3.Distance(position1, Vector3.zero); //transform.parent.position



















                    /*float temporaryY = 0.1f;
                    float temporaryZ = 0.1f;
                    float temporaryX = 0.1f;

                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                    float size0 = (1 / planeSize) * transform.position.y;
                    temporaryY -= size0;

                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                    float size1 = (1 / planeSize) * transform.position.x;
                    temporaryX -= size1;

                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
                    float size2 = (1 / planeSize) * transform.position.z;
                    temporaryZ -= size2;

                    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                    {
                        map[x, y, z] = 1;
                    }*/
                    //map[x, y, z] = 1;

                    /*float temporaryY = 1f;
                    float temporaryZ = 0.1f;
                    float temporaryX = 0.1f;

                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                    float size0 = (1 / planeSize) * transform.position.y;
                    temporaryY -= size0;

                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                    float size1 = (1 / planeSize) * transform.position.x;
                    temporaryX -= size1;

                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                    float size2 = (1 / planeSize) * transform.position.z;
                    temporaryZ -= size2;*/

                    /*if ((int)Mathf.Round(temporaryY) >= y )
                    {
                        map[x, y, z] = 1;
                    }*/

                    /*if ((int)Mathf.Round(temporaryY) >= 0)
                    {
                        map[x, y, z] = 1;
                    }*/

                    if (distance1 >= radiusplanetcorestart && distance1 < radiusplanetmountainend) //
                    {
                        if (distance <= radiusplanetcoreend)
                        {
                            map[x, y, z] = 1;
                        }

                        if (distance > radiusplanetcoreend && distance < radiusplanetcavesend)
                        {
                            float noiseValue0 = Noise.Generate(noiseX, noiseY, noiseZ);
                            if (noiseValue0 > 0.2f)
                            {
                                map[x, y, z] = 1;
                            }
                        }

                        else if (distance >= radiusplanetcavesend && distance <= radiusplanetcrustend)
                        {
                            map[x, y, z] = 1;
                        }

                        if (switchPerlinMountainsNotWorking == 1)
                        {












                            float distOffset = 0;

                            if (transform.position.x != 0|| transform.position.y != 0 || transform.position.z != 0)
                            {
                                distOffset += distance1;
                            }




                            if (distance > radiusplanetcrustend && distance < (radiusplanetmountainend - 1)) //distOffset
                            {
                                float temporaryY = 10;
                                float temporaryZ = 10;
                                float temporaryX = 10;

                                if (transform.position.y >= 0 && transform.position.x >= 0 && transform.position.z >= 0)
                                {
                                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;


                                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;


                                    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else if (transform.position.y < 0 && transform.position.x < 0 && transform.position.z < 0)
                                {
                                    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;

                                    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;

                                    if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else if (transform.position.y >= 0 && transform.position.x < 0 && transform.position.z >= 0)
                                {
                                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;

                                    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;


                                    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) >= z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else if (transform.position.y >= 0 && transform.position.x >= 0 && transform.position.z < 0)
                                {
                                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;


                                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;


                                    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) < z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else if (transform.position.y >= 0 && transform.position.x < 0 && transform.position.z < 0)
                                {
                                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;


                                    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;


                                    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else if (transform.position.y < 0 && transform.position.x >= 0 && transform.position.z < 0)
                                {
                                    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;


                                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;


                                    if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) < z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else if (transform.position.y < 0 && transform.position.x >= 0 && transform.position.z >= 0)
                                {
                                    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;

                                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;


                                    if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else if (transform.position.y < 0 && transform.position.x < 0 && transform.position.z >= 0)
                                {
                                    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;


                                    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;

                                    if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) >= z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else
                                {
                                    map[x, y, z] = 0;

                                }












                                /*
                                float temporaryY = 10;
                                float temporaryZ = 10;
                                float temporaryX = 10;

                                //float mul = 1.0f;
                                if (transform.position.y >= 0 && transform.position.x >= 0 && transform.position.z >= 0)
                                {
                                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;

                                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;

                                    //Debug.Log("x#"+ x + "xT#" + temporaryX + "y#" + y + " yT" + temporaryY + "z#" + z + " zT" + temporaryZ);

                                    //temporaryX *= 0.1f;
                                    //temporaryY *= 0.1f;
                                    //temporaryZ *= 0.1f;

                                    if ((int)Mathf.Round(temporaryY * planetmountainperlinmulY) >= y && (int)Mathf.Round(temporaryX * planetmountainperlinmulX) >= x && (int)Mathf.Round(temporaryZ * planetmountainperlinmulZ) >= z) // 
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }

                                /*else if (transform.position.y >= 0 && transform.position.x < 0 && transform.position.z >= 0)
                                {
                                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;

                                    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;

                                    //Debug.Log(temporaryY);
                                    if ((int)Mathf.Round(temporaryY * planetmountainperlinmulY) >= y && (int)Mathf.Round(temporaryX * planetmountainperlinmulX) < x && (int)Mathf.Round(temporaryZ) >= z * planetmountainperlinmulZ)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }

                                else if (transform.position.y < 0 && transform.position.x >= 0 && transform.position.z >= 0)
                                {
                                    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;

                                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;

                                    //Debug.Log(temporaryY);
                                    if ((int)Mathf.Round(temporaryY * planetmountainperlinmulY) < y && (int)Mathf.Round(temporaryX * planetmountainperlinmulX) >= x && (int)Mathf.Round(temporaryZ * planetmountainperlinmulZ) >= z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }

                                else if (transform.position.y < 0 && transform.position.x < 0 && transform.position.z < 0)
                                {
                                    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;

                                    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;

                                    //Debug.Log(temporaryY);


                                    if ((int)Mathf.Round(temporaryY * planetmountainperlinmulY) < y && (int)Mathf.Round(temporaryX * planetmountainperlinmulX) < x && (int)Mathf.Round(temporaryZ * planetmountainperlinmulZ) < z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }


                                else if (transform.position.y >= 0 && transform.position.x >= 0 && transform.position.z < 0)
                                {
                                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;


                                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;

                                    //Debug.Log(temporaryY);
                                    if ((int)Mathf.Round(temporaryY * planetmountainperlinmulY) >= y && (int)Mathf.Round(temporaryX * planetmountainperlinmulX) >= x && (int)Mathf.Round(temporaryZ * planetmountainperlinmulZ) < z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }


                                else if (transform.position.y >= 0 && transform.position.x < 0 && transform.position.z < 0)
                                {
                                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;


                                    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;

                                    //Debug.Log(temporaryY);
                                    if ((int)Mathf.Round(temporaryY * planetmountainperlinmulY) >= y && (int)Mathf.Round(temporaryX * planetmountainperlinmulX) < x && (int)Mathf.Round(temporaryZ * planetmountainperlinmulZ) < z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }



                                else if (transform.position.y < 0 && transform.position.x >= 0 && transform.position.z < 0)
                                {
                                    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;


                                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;

                                    //Debug.Log(temporaryY);
                                    if ((int)Mathf.Round(temporaryY * planetmountainperlinmulY) < y && (int)Mathf.Round(temporaryX * planetmountainperlinmulX) >= x && (int)Mathf.Round(temporaryZ * planetmountainperlinmulZ) < z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }


                                else if (transform.position.y < 0 && transform.position.x < 0 && transform.position.z >= 0)
                                {
                                    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;


                                    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;

                                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;


                                    //Debug.Log(temporaryY);
                                    if ((int)Mathf.Round(temporaryY * planetmountainperlinmulY) < y && (int)Mathf.Round(temporaryX * planetmountainperlinmulX) < x && (int)Mathf.Round(temporaryZ * planetmountainperlinmulZ) >= z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else
                                {
                                    map[x, y, z] = 0;

                                }*/


                                ////(transform.localPosition.y < 0 && transform.localPosition.x < 0 && transform.localPosition.z < 0)
                                ////transform.localPosition.y >= 0 && transform.localPosition.x >= 0 && transform.localPosition.z >= 0)
                                ////transform.localPosition.y >= 0 && transform.localPosition.x < 0 && transform.localPosition.z >= 0)
                                ////(transform.localPosition.y >= 0 && transform.localPosition.x >= 0 && transform.localPosition.z < 0)
                                ////(transform.localPosition.y >= 0 && transform.localPosition.x < 0 && transform.localPosition.z < 0)
                                ////(transform.localPosition.y < 0 && transform.localPosition.x >= 0 && transform.localPosition.z < 0)
                                ////(transform.localPosition.y < 0 && transform.localPosition.x >= 0 && transform.localPosition.z >= 0)
                                ////(transform.localPosition.y < 0 && transform.localPosition.x < 0 && transform.localPosition.z >= 0)


                                /*if (transform.position.y < 0)
                                {
                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;
                                    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    if ((int)Mathf.Round(temporaryY) <= y)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else
                                {
                                    float size0 = (1 / planeSize) * transform.position.y;
                                    temporaryY -= size0;
                                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    if ((int)Mathf.Round(temporaryY) >= y )
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }

                                if (transform.position.x < 0)
                                {
                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;
                                    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    if ((int)Mathf.Round(temporaryX) <= x)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else
                                {
                                    float size1 = (1 / planeSize) * transform.position.x;
                                    temporaryX -= size1;
                                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                                    if ((int)Mathf.Round(temporaryX) <= x)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }



                                if (transform.position.z < 0)
                                {
                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;
                                    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
                                    if ((int)Mathf.Round(temporaryZ) <= z)
                                    {
                                        map[x, y, z] = 1;
                                    }
                                }
                                else
                                {
                                    float size2 = (1 / planeSize) * transform.position.z;
                                    temporaryZ -= size2;
                                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
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
        }
        //Regenerate();
    }

    public void Regenerate()
    {
        //verts.Clear();
        //tris.Clear();
        //uv.Clear();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    //block = map[x + width * (y + depth * z)];
                    if (map != null)
                    {
                        block = map[x, y, z];

                        if (block == 0) continue;
                        {
                            DrawBrick(x, y, z);
                        }
                        //Instantiate(sphere, new Vector3(x*planeSize, y * planeSize, z * planeSize) +transform.position, Quaternion.identity);
                    }
                }
            }
        }

        mesh = new Mesh();

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();

        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = mesh;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        //GetComponent<chunku>().enabled = false;

        GetComponent<MeshFilter>().mesh = mesh;




        GetComponent<chunkonew>().enabled = false;
    }

    //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;
    //Instantiate(sphere, new Vector3(xx, yy, zz) + terrain.getChunkPos(chunkdata.transform.position.x, chunkdata.transform.position.y, chunkdata.transform.position.z).transform.position, Quaternion.identity);

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

        /*//LEFTFACE
		if (x == 0 && componentParent.getChunk(Mathf.RoundToInt(transform.position.x - 1), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z)) != null)
        {
            //Instantiate(sphere,new Vector3(x,y,z)*planeSize+transform.position,Quaternion.identity);
			mainChunk chunkdata = componentParent.getChunk(Mathf.RoundToInt(transform.position.x - 1), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));

			if (chunkdata.chunker.GetComponent<chunkonew>().GetByte(width - 1, y, z) == 0)
            {
                //LEFTFACE
                if (IsTransparent(x - 1, y, z))
                {
                    offset1 = Vector3.back * planeSize;
                    offset2 = Vector3.down * planeSize;
                    DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2);
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
                DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2);
            }
        }*/




        //RIGHTFACE
        if (x != width - 1)
        {
            //RIGHTFACE
            if (IsTransparent(x + 1, y, z))
            {
                offset1 = Vector3.up * planeSize;
                offset2 = Vector3.forward * planeSize;
                DrawFace(start + Vector3.right * planeSize, offset1, offset2);
            }
        }
        else if (x == width - 1)
        {
            if (componentParent.getChunk((int)(transform.position.x + 4), (int)(transform.position.y), (int)(transform.position.z)) != null)
            {
                mainChunk chunkdata = componentParent.getChunk((int)(transform.position.x + 4), (int)(transform.position.y), (int)(transform.position.z));

                float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                if (chunkdata != null)
                {
                    var comp = chunkdata.planetchunk.GetComponent<chunkonew>();

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
        }

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
            if (componentParent.getChunk((int)(transform.position.x - 4), (int)(transform.position.y), (int)(transform.position.z)) != null)
            {
                mainChunk chunkdata = componentParent.getChunk((int)(transform.position.x - 4), (int)(transform.position.y), (int)(transform.position.z));

                float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                if (chunkdata != null)
                {
                    var comp = chunkdata.planetchunk.GetComponent<chunkonew>();

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
        if (z == 0 && componentParent.getChunk((int)(transform.position.x), (int)(transform.position.y), (int)(transform.position.z - 4)) != null)
        {
            mainChunk chunkdata = componentParent.getChunk((int)(transform.position.x), (int)(transform.position.y), (int)(transform.position.z - 4));

            if (chunkdata != null)
            {
                var comp = chunkdata.planetchunk.GetComponent<chunkonew>();

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
        if (z == width - 1 && componentParent.getChunk((int)(transform.position.x), (int)(transform.position.y), (int)(transform.position.z + 4)) != null)
        {
            mainChunk chunkdata = componentParent.getChunk((int)(transform.position.x), (int)(transform.position.y), (int)(transform.position.z + 4));

            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

            if (chunkdata != null)
            {
                var comp = chunkdata.planetchunk.GetComponent<chunkonew>();

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
        if (y == height - 1 && componentParent.getChunk((int)(transform.position.x), (int)(transform.position.y + 4), (int)(transform.position.z)) != null)
        {
            mainChunk chunkdata = componentParent.getChunk((int)(transform.position.x), (int)(transform.position.y + 4), (int)(transform.position.z));

            if (chunkdata != null)
            {
                var comp = chunkdata.planetchunk.GetComponent<chunkonew>();

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
        if (y == 0 && componentParent.getChunk((int)(transform.position.x), (int)(transform.position.y - 4), (int)(transform.position.z)) != null)
        {
            mainChunk chunkdata = componentParent.getChunk((int)(transform.position.x), (int)(transform.position.y - 4), (int)(transform.position.z));

            if (chunkdata != null)
            {
                var comp = chunkdata.planetchunk.GetComponent<chunkonew>();

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
        }
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
            if (terrain.getChunk(transform.position.x + 1, transform.position.y, transform.position.z) != null)
            {
                chunky chunkdata = terrain.getChunk(transform.position.x + 1, transform.position.y, transform.position.z);
                chunkdata.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (x == 0)
        {
            if (terrain.getChunk(transform.position.x - 1, transform.position.y, transform.position.z) != null)
            {
                chunky chunkdata = terrain.getChunk(transform.position.x - 1, transform.position.y, transform.position.z);
                chunkdata.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (z == width - 1)
        {
            if (terrain.getChunk(transform.position.x, transform.position.y, transform.position.z + 1) != null)
            {
                chunky chunkdata = terrain.getChunk(transform.position.x, transform.position.y, transform.position.z + 1);
                chunkdata.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (z == 0)
        {
            if (terrain.getChunk(transform.position.x, transform.position.y, transform.position.z - 1) != null)
            {
                chunky chunkdata = terrain.getChunk(transform.position.x, transform.position.y, transform.position.z - 1);
                chunkdata.chunker.GetComponent<chunk>().Regenerate();
            }
        }*/
    }

    public bool IsTransparent(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth)) return true;
        {
            return map[x, y, z] == 0;
            //return map[x + width * (y + depth * z)] == 0;
        }
    }

    public byte GetByte(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth)) return 0;
        {
            return map[x, y, z];
        }
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
        float xPosition = (transform.position.x);
        float yPosition = (transform.position.y);
        float zPosition = (transform.position.z);

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
                    //Instantiate(cube, new Vector3(xPos,yPos,zPos)+transform.position, Quaternion.identity);

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
            Gizmos.DrawSphere(new Vector3(mesh.vertices[i].x + transform.position.x, mesh.vertices[i].y + transform.position.y, mesh.vertices[i].z + transform.position.z), 0.01f);
        }


    }*/
}






/*if (x == 0)
        {
            Instantiate(sphere, new Vector3(x * planeSize + transform.position.x, y * planeSize + transform.position.y, z * planeSize + transform.position.z), Quaternion.identity);
        }
        else if (y == 0)
        {
            Instantiate(sphere, new Vector3(x * planeSize + transform.position.x, y * planeSize + transform.position.y, z * planeSize + transform.position.z), Quaternion.identity);
        }
        else if(z == 0)
        {
            Instantiate(sphere, new Vector3(x * planeSize + transform.position.x, y * planeSize + transform.position.y, z * planeSize + transform.position.z), Quaternion.identity);
        }


        else if (x == width-1)
        {
            Instantiate(sphere, new Vector3(x * planeSize + transform.position.x, y * planeSize + transform.position.y, z * planeSize + transform.position.z), Quaternion.identity);
        }
        else if (y == width - 1)
        {
            Instantiate(sphere, new Vector3(x * planeSize + transform.position.x, y * planeSize + transform.position.y, z * planeSize + transform.position.z), Quaternion.identity);
        }
        else if (z == width - 1)
        {
            Instantiate(sphere, new Vector3(x * planeSize + transform.position.x, y * planeSize + transform.position.y, z * planeSize + transform.position.z), Quaternion.identity);
        }*/







/*public void SetBrick(int x, int y, int z, byte block)
{
    //int x = Mathf.RoundToInt();

    //x -= (int)Mathf.RoundToInt(transform.position.x);
    //y -= (int)Mathf.RoundToInt(transform.position.y);
    //z -= (int)Mathf.RoundToInt(transform.position.z);

    //x -= (int)Mathf.RoundToInt(transform.position.x);
    //y -= (int)Mathf.RoundToInt(transform.position.y);
    //z -= (int)Mathf.RoundToInt(transform.position.z);

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

    chunk chunkdata = terrain.getChunkPos(transform.position.x + 1, transform.position.y, transform.position.z);

    if (chunkdata.GetByte(0, y, z) == 1)
    {
        Debug.Log("yo1");

        if (chunkdata.map[width - 1, y, z] != block)
        {
            Debug.Log("yo2");

            chunkdata.map[0, y, z] = block;
            chunkdata.Regenerate();
        }
    }           
}*/







/*//RIGHTFACE
   if (x == width - 1 && terrain.getChunkPos(transform.position.x + 1, transform.position.y, transform.position.z) != null)
   {
       chunk chunkdata = terrain.getChunkPos(transform.position.x + 1, transform.position.y, transform.position.z);

       float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
       float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
       float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

       if (chunkdata.GetByte(0, y, z) == 0)
       {
           //Instantiate(sphere, new Vector3(xx, yy, zz) + terrain.getChunkPos(transform.position.x, transform.position.y, transform.position.z).transform.position, Quaternion.identity);
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
if (x == 0 && terrain.getChunkPos(transform.position.x - 1, transform.position.y, transform.position.z) != null)
{
    chunk chunkdata = terrain.getChunkPos(transform.position.x - 1, transform.position.y, transform.position.z);

    //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;
    if (chunkdata.GetByte(width-1, y, z) == 0)
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
if (z == 0 && terrain.getChunkPos(transform.position.x , transform.position.y, transform.position.z-1) != null)
{
    chunk chunkdata = terrain.getChunkPos(transform.position.x , transform.position.y, transform.position.z-1);

    float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

    if (chunkdata.GetByte(x, y, width-1) == 0)
    {
    //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;
        //Instantiate(sphere, new Vector3(xx, yy, zz) + terrain.getChunkPos(transform.position.x, transform.position.y, transform.position.z).transform.position, Quaternion.identity);
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
if (z == width-1 && terrain.getChunkPos(transform.position.x, transform.position.y, transform.position.z + 1) != null)
{
    chunk chunkdata = terrain.getChunkPos(transform.position.x, transform.position.y, transform.position.z + 1);

    float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

    if (chunkdata.GetByte(x, y, 0) == 0)
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






/*if (test.getChunk(transform.position.x, transform.position.y + 1, transform.position.z) != null)
{
float noiseXX = Mathf.Abs(((float)(x * planeSize + transform.position.x + seed) / detailScale) * heightScale);
float noiseYY = Mathf.Abs(((float)(y * planeSize + transform.position.y + 1 + seed) / detailScale) * heightScale);
float noiseZZ = Mathf.Abs(((float)(z * planeSize + transform.position.z + seed) / detailScale) * heightScale);
float heightingo = Noise.Generate(noiseXX, noiseYY, noiseZZ);
heightingo += (10f - (float)y) / 10;
heightingo /= (float)y / 5;
mainChunk chunkdata = test.getChunk(transform.position.x, transform.position.y + 1, transform.position.z);
if (heightingo >= 0.2f)
{
chunkdata.chunker.GetComponent<chunku>().map[x, y, z] = 1;
}
}*/





/*temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
                           temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
                           temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);


                           float size0 = (1 / planeSize) * transform.position.y;
                           temporaryY -= size0;

                           /*float size1 = (1 / planeSize) * transform.position.x;
                           temporaryX -= size1;

                           float size2 = (1 / planeSize) * transform.position.z;
                           temporaryZ -= size2;

                           if ((int)Mathf.Round(temporaryY) < y )
                           {
                               map[x, y, z] = 1;
                           }*/












/*if (transform.position.y < 0)
{
    float size0 = (1 / planeSize) * transform.position.y;
    temporaryY -= size0;
    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryY) <= y)
    {
        map[x, y, z] = 1;
    }
}
else
{
    float size0 = (1 / planeSize) * transform.position.y;
    temporaryY -= size0;
    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryY) >= y )
    {
        map[x, y, z] = 1;
    }
}

if (transform.position.x < 0)
{
    float size1 = (1 / planeSize) * transform.position.x;
    temporaryX -= size1;
    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryX) <= x)
    {
        map[x, y, z] = 1;
    }
}
else
{
    float size1 = (1 / planeSize) * transform.position.x;
    temporaryX -= size1;
    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryX) <= x)
    {
        map[x, y, z] = 1;
    }
}



if (transform.position.z < 0)
{
    float size2 = (1 / planeSize) * transform.position.z;
    temporaryZ -= size2;
    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryZ) <= z)
    {
        map[x, y, z] = 1;
    }
}
else
{
    float size2 = (1 / planeSize) * transform.position.z;
    temporaryZ -= size2;
    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryZ) >= z)
    {
        map[x, y, z] = 1;
    }
}*/



/*if (transform.position.y < 0 || transform.position.x < 0 || transform.position.z < 0)
{
    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

    float size0 = (1 / planeSize) * transform.position.y;
    temporaryY -= size0;

    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

    float size1 = (1 / planeSize) * transform.position.x;
    temporaryX -= size1;
    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

    float size2 = (1 / planeSize) * transform.position.z;
    temporaryZ -= size2;
    if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
    {
        map[x, y, z] = 1;
    }                            
}*/

/*if (transform.position.y >= 0 || transform.position.x >= 0 || transform.position.z >= 0)
{
    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

    float size0 = (1 / planeSize) * transform.position.y;
    temporaryY -= size0;


    temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / detailScale1, (z * planeSize + transform.position.z + seed) / detailScale1) * heightScale1);

    float size1 = (1 / planeSize) * transform.position.x;
    temporaryX -= size1;

    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / detailScale1, (y * planeSize + transform.position.y + seed) / detailScale1) * heightScale1);

    float size2 = (1 / planeSize) * transform.position.z;
    temporaryZ -= size2;


    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
    {
        map[x, y, z] = 1;
    }
}*/


















