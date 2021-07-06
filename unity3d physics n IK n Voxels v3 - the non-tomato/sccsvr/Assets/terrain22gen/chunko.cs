using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimplexNoise;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
//[RequireComponent(typeof(MeshCollider))]

public class chunko : MonoBehaviour
{
    int width = 10;
    int height = 10;
    int depth = 10;
    public byte[] map;
    //public byte[,,] map;
    protected Mesh mesh;
    protected List<Vector3> verts = new List<Vector3>();
    protected List<int> tris = new List<int>();
    protected List<Vector2> uv = new List<Vector2>();
    protected MeshCollider meshCollider;
    public static float planeSize = 0.1f;
    public Transform sphere;
    float seed;
    byte block;

    float nodeDiameter;
    float chunkRadius;
    float fraction;
    float chunkSize;

    int divider = 10;
    public Transform cube;

    public float detailScale;
    //public float heightScale;

    public float heightScale;
    public float scale = 6.5f;
    //PerlinNoise noise;

    public int heightScale1;
    public int detailScale1;

    public int earthSoilDetailScale;
    public int earthSoilheightScale;

    //Flat[x + width * (y + height * z)]

    public bool mapper = false;
    MeshRenderer meshRend;
    public int levelOfDetail;

    public chunko chunkry;

    void Awake()
    {
        //noise = new PerlinNoise(Random.Range(1000000, 10000000));
        //chunkry = this;
        nodeDiameter = planeSize;
        chunkRadius = planeSize / 2;
        fraction = (int)(1 / (planeSize));
        chunkSize = 1f;

        meshRend = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        
        //map = new byte[width*height*depth];
        seed = 3420;
        //seed = Random.Range(3000, 4000);

        int radius = 5;
        Vector3 center = Vector3.zero;


        //map = new byte[width, height, depth];

        map = new byte[width *height * depth];

        for (int x = 0; x < width; x+= levelOfDetail)
        {
            //float noiseX = Mathf.Abs(((float)(x * planeSize + transform.position.x + seed) / detailScale) * heightScale);

            for (int y = 0; y < height; y += levelOfDetail)
            {
                //float noiseY = Mathf.Abs(((float)(y * planeSize + transform.position.y + seed) / detailScale) * heightScale);

                for (int z = 0; z < depth; z += levelOfDetail)
                {
                    //float noiseZ = Mathf.Abs(((float)(z * planeSize + transform.position.z + seed) / detailScale) * heightScale);

                    float posX = x * planeSize + transform.position.x;
                    float posY = y * planeSize + transform.position.y;
                    float posZ = z * planeSize + transform.position.z;

                    Vector3 position = new Vector3(posX, posY, posZ);

                    float distance = Vector3.Distance(position, center);

                    Vector3 position1 = transform.position;

                    float distance1 = Vector3.Distance(position1, center);

                    //float noiseValue0 = Noise.Generate(noiseX, noiseY, noiseZ);

                    float temporaryY = 0.1f;
                    float temporaryZ = 0.1f;
                    float temporaryX = 0.1f;
                    //map[x, 0, z] = 1;


                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);
                    float size0 = (1 / planeSize) * transform.position.y;
                    temporaryY -= size0;

                    /*temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);
                    float size1 = (1 / planeSize) * transform.position.x;
                    temporaryX -= size1;*/

                    /*temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (y * planeSize + transform.position.y + seed) / earthSoilDetailScale) * earthSoilheightScale);
                    float size2 = (1 / planeSize) * transform.position.z;
                    temporaryZ -= size2;*/

                    /*if (temporaryY >= y)
                    {
                        map[x + width * (y + height * z)] = 1;
                    }*/

                    map[x + width * (y + height * z)] = 1;








                    /*float noiseValue0 = Noise.Generate(noiseX, noiseY, noiseZ);

                    if (noiseValue0 > 0.2f)
                    {
                        map[x, y, z] = 1;
                    }*/

                    /*if (distance1 >=0 && distance1 < 15)
                    {
                        if (distance < 2)
                        {
                            float noiseValue0 = Noise.Generate(noiseX, noiseY, noiseZ);
                            if (noiseValue0 > 0.2f)
                            {
                                map[x, y, z] = 1;
                            }
                        }

                        else if (distance >= 2 && distance < 4)
                        {
                            map[x, y, z] = 1;
                        }

                        else if (distance >=4 && distance < 5)
                        {
                            float temporaryY = 0.1f;
                            float temporaryZ = 0.1f;
                            float temporaryX = 0.1f;

                            if (transform.position.y < 0 && transform.position.x < 0 && transform.position.z < 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);
                                float size0 = (1 / planeSize) * transform.position.y;
                                temporaryY -= size0;

                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);
                                float size1 = (1 / planeSize) * transform.position.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (y * planeSize + transform.position.y + seed) / earthSoilDetailScale) * earthSoilheightScale);
                                float size2 = (1 / planeSize) * transform.position.z;
                                temporaryZ -= size2;

                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[x, y, z] = 1;
                                }
                            }

                            else if (transform.position.y >= 0 && transform.position.x >= 0 && transform.position.z >= 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size0 = (1 / planeSize) * transform.position.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size1 = (1 / planeSize) * transform.position.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (y * planeSize + transform.position.y + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size2 = (1 / planeSize) * transform.position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[x, y, z] = 1;
                                }
                            }

                            else if (transform.position.y >= 0 && transform.position.x < 0 && transform.position.z >= 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size0 = (1 / planeSize) * transform.position.y;
                                temporaryY -= size0;

                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size1 = (1 / planeSize) * transform.position.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (y * planeSize + transform.position.y + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size2 = (1 / planeSize) * transform.position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[x, y, z] = 1;
                                }
                            }


                            else if (transform.position.y >= 0 && transform.position.x >= 0 && transform.position.z < 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size0 = (1 / planeSize) * transform.position.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size1 = (1 / planeSize) * transform.position.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (y * planeSize + transform.position.y + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size2 = (1 / planeSize) * transform.position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[x, y, z] = 1;
                                }
                            }

                            else if (transform.position.y >= 0 && transform.position.x < 0 && transform.position.z < 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size0 = (1 / planeSize) * transform.position.y;
                                temporaryY -= size0;


                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size1 = (1 / planeSize) * transform.position.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (y * planeSize + transform.position.y + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size2 = (1 / planeSize) * transform.position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[x, y, z] = 1;
                                }
                            }



                            else if (transform.position.y < 0 && transform.position.x >= 0 && transform.position.z < 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size0 = (1 / planeSize) * transform.position.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size1 = (1 / planeSize) * transform.position.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (y * planeSize + transform.position.y + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size2 = (1 / planeSize) * transform.position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    map[x, y, z] = 1;
                                }
                            }





                            else if (transform.position.y < 0 && transform.position.x >= 0 && transform.position.z >= 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);
                                float size0 = (1 / planeSize) * transform.position.y;
                                temporaryY -= size0;

                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);
                                float size1 = (1 / planeSize) * transform.position.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (y * planeSize + transform.position.y + seed) / earthSoilDetailScale) * earthSoilheightScale);
                                float size2 = (1 / planeSize) * transform.position.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    map[x, y, z] = 1;
                                }
                            }





                            else if (transform.position.y < 0 && transform.position.x < 0 && transform.position.z >= 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size0 = (1 / planeSize) * transform.position.y;
                                temporaryY -= size0;


                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + transform.position.y + seed) / earthSoilDetailScale, (z * planeSize + transform.position.z + seed) / earthSoilDetailScale) * earthSoilheightScale);

                                float size1 = (1 / planeSize) * transform.position.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + transform.position.x + seed) / earthSoilDetailScale, (y * planeSize + transform.position.y + seed) / earthSoilDetailScale) * earthSoilheightScale);

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


                        }
                        else if (distance >= 5 && distance <= 8)
                        {
                            //map[x, y, z] = 1;


                            float temporaryY = 0.1f;
                            float temporaryZ = 0.1f;
                            float temporaryX = 0.1f;

                            if (transform.position.y < 0 && transform.position.x < 0 && transform.position.z < 0)
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

                            else if (transform.position.y >= 0 && transform.position.x >= 0 && transform.position.z >= 0)
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
                        }

                        else if (distance >= 8 &&  distance < 14f)
                        {
                            //float tempPosY = transform.position.y;


                            float temporaryY = 0.1f;
                            float temporaryZ = 0.1f;
                            float temporaryX = 0.1f;

                            if (transform.position.y < 0 && transform.position.x < 0 && transform.position.z < 0)
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

                            else if (transform.position.y >= 0 && transform.position.x >= 0 && transform.position.z >= 0)
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


                            ////(transform.position.y < 0 && transform.position.x < 0 && transform.position.z < 0)
                            ////transform.position.y >= 0 && transform.position.x >= 0 && transform.position.z >= 0)
                            ////transform.position.y >= 0 && transform.position.x < 0 && transform.position.z >= 0)
                            ////(transform.position.y >= 0 && transform.position.x >= 0 && transform.position.z < 0)
                            ////(transform.position.y >= 0 && transform.position.x < 0 && transform.position.z < 0)
                            ////(transform.position.y < 0 && transform.position.x >= 0 && transform.position.z < 0)
                            ////(transform.position.y < 0 && transform.position.x >= 0 && transform.position.z >= 0)
                            ////(transform.position.y < 0 && transform.position.x < 0 && transform.position.z >= 0)                
                        }
                    }*/
                }
            }
        }
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        Regenerate();
        GetComponent<chunko>().enabled = false;
    }

    public void Regenerate()
    {
        verts.Clear();
        tris.Clear();
        uv.Clear();

        mesh.triangles = tris.ToArray();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    block = map[x + width * (y + height * z)];

                    //block = map[x, y, z];

                    if (block == 0) continue;
                    {
                        DrawBrick(x, y, z);
                    }
                    //Instantiate(sphere, new Vector3(x*planeSize, y * planeSize, z * planeSize) +transform.position, Quaternion.identity);
                }
            }
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
        Color color = Color.grey;
        meshRend.material.color = color;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    public void DrawBrick(int x, int y, int z)
    {
        //Debug.Log(map[x,y,z]);

        Vector3 start = new Vector3(x * planeSize , y * planeSize , z * planeSize );
        Vector3 offset1, offset2;

        //TOPFACE
        if (IsTransparent(x, y + 1, z))
        {
            offset1 = Vector3.forward * planeSize * levelOfDetail;
            offset2 = Vector3.right * planeSize * levelOfDetail;
            DrawFace(start + Vector3.up * planeSize * levelOfDetail, offset1, offset2);
        }

        //LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            offset1 = Vector3.back * planeSize * levelOfDetail;
            offset2 = Vector3.down * planeSize * levelOfDetail;
            DrawFace(start + Vector3.up * planeSize * levelOfDetail + Vector3.forward * planeSize * levelOfDetail, offset1, offset2);
        }

        //RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            offset1 = Vector3.up * planeSize * levelOfDetail;
            offset2 = Vector3.forward * planeSize * levelOfDetail;
            DrawFace(start + Vector3.right * planeSize * levelOfDetail, offset1, offset2);
        }

        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            offset1 = Vector3.left * planeSize * levelOfDetail;
            offset2 = Vector3.up * planeSize * levelOfDetail;
            DrawFace(start + Vector3.right * planeSize * levelOfDetail, offset1, offset2);
        }
        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            offset1 = Vector3.right * planeSize * levelOfDetail;
            offset2 = Vector3.up * planeSize * levelOfDetail;
            DrawFace(start + Vector3.forward * planeSize * levelOfDetail, offset1, offset2);
        }


        //BOTTOMFACE
        if (IsTransparent(x, y - 1, z))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.forward * planeSize;
            DrawFace(start, offset1, offset2);
        }

        /*//TOPFACE
        if (y == width-1 && test.getChunk(transform.position.x , transform.posj0k1xition.y+1, transform.position.z) != null)
        {
            //Instantiate(sphere,new Vector3(x,y,z)*planeSize+transform.position,Quaternion.identity);
            mainChunk chuk = test.getChunk(transform.position.x , transform.position.y+1, transform.position.z);
            //Instantiate(sphere, chuk.chunker.transform.position + transform.position, Quaternion.identity);

            if (chuk.chunker.GetComponent<chunko>().GetByte(x, 0, z) == 0)
            {
                //TOPFACE
                if (IsTransparent(x, y + 1, z))
                {
                    offset1 = Vector3.forward * planeSize;
                    offset2 = Vector3.right * planeSize;
                    DrawFace(start + Vector3.up * planeSize, offset1, offset2);
                }
            }
        }
        else if (y != width - 1)
        {
            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = Vector3.forward * planeSize;
                offset2 = Vector3.right * planeSize;
                DrawFace(start + Vector3.up * planeSize, offset1, offset2);
            }
        }

        //LEFTFACE
        if (x == 0 && test.getChunk(transform.position.x - 1, transform.position.y, transform.position.z) != null)
        {
            //Instantiate(sphere,new Vector3(x,y,z)*planeSize+transform.position,Quaternion.identity);
            mainChunk chuk = test.getChunk(transform.position.x - 1, transform.position.y, transform.position.z);
            //Instantiate(sphere, chuk.chunker.transform.position, Quaternion.identity);

            if (chuk.chunker.GetComponent<chunko>().GetByte(width - 1, y, z) == 0)
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
        }

        //RIGHTFACE
        if (x == width - 1 && test.getChunk(transform.position.x + 1, transform.position.y, transform.position.z) != null)
        {
            mainChunk chuk = test.getChunk(transform.position.x + 1, transform.position.y, transform.position.z);

            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

            if (chuk.chunker.GetComponent<chunko>().GetByte(0, y, z) == 0)
            {
                //Instantiate(sphere, new Vector3(xx, yy, zz) + terrain.getChunkPos(transform.position.x, transform.position.y, transform.position.z).transform.position, Quaternion.identity);
                //RIGHTFACE
                if (IsTransparent(x + 1, y, z))
                {
                    offset1 = Vector3.up * planeSize;
                    offset2 = Vector3.forward * planeSize;
                    DrawFace(start + Vector3.right * planeSize, offset1, offset2);
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
                DrawFace(start + Vector3.right * planeSize, offset1, offset2);
            }
        }

        //FRONTFACE
        if (z == 0 && test.getChunk(transform.position.x, transform.position.y, transform.position.z - 1) != null)
        {
            mainChunk chuk = test.getChunk(transform.position.x, transform.position.y, transform.position.z - 1);

            if (chuk.chunker.GetComponent<chunko>().GetByte(x, y, width - 1) == 0)
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
                    DrawFace(start + Vector3.right * planeSize, offset1, offset2);
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
        if (z == width - 1 && test.getChunk(transform.position.x, transform.position.y, transform.position.z + 1) != null)
        {
            mainChunk chuk = test.getChunk(transform.position.x, transform.position.y, transform.position.z + 1);

            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

            if (chuk.chunker.GetComponent<chunko>().GetByte(x, y, 0) == 0)
            {
                //BACKFACE
                if (IsTransparent(x, y, z + 1))
                {
                    offset1 = Vector3.right * planeSize;
                    offset2 = Vector3.up * planeSize;
                    DrawFace(start + Vector3.forward * planeSize, offset1, offset2);
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
        if (y == 0 && test.getChunk(transform.position.x, transform.position.y - 1, transform.position.z) != null)
        {
            //Instantiate(sphere,new Vector3(x,y,z)*planeSize+transform.position,Quaternion.identity);
            mainChunk chuk = test.getChunk(transform.position.x, transform.position.y - 1, transform.position.z);
            //Instantiate(sphere, chuk.chunker.transform.position + transform.position, Quaternion.identity);

            if (chuk.chunker.GetComponent<chunko>().GetByte(x, width-1, z) == 0)
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
        else if (y != 0)
        {
            //BOTTOMFACE
            if (IsTransparent(x, y - 1, z))
            {
                offset1 = Vector3.right * planeSize;
                offset2 = Vector3.forward * planeSize;
                DrawFace(start, offset1, offset2);
            }         
        }   */
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
                chunky chuk = terrain.getChunk(transform.position.x + 1, transform.position.y, transform.position.z);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (x == 0)
        {
            if (terrain.getChunk(transform.position.x - 1, transform.position.y, transform.position.z) != null)
            {
                chunky chuk = terrain.getChunk(transform.position.x - 1, transform.position.y, transform.position.z);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (z == width - 1)
        {
            if (terrain.getChunk(transform.position.x, transform.position.y, transform.position.z + 1) != null)
            {
                chunky chuk = terrain.getChunk(transform.position.x, transform.position.y, transform.position.z + 1);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (z == 0)
        {
            if (terrain.getChunk(transform.position.x, transform.position.y, transform.position.z - 1) != null)
            {
                chunky chuk = terrain.getChunk(transform.position.x, transform.position.y, transform.position.z - 1);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }*/
    }




    public bool IsTransparent(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth)) return true;
        {
            //return map[x, y, z] == 0;
            return map[x + width * (y + height * z)] ==0 ;
            //return map[x + width * (y + depth * z)] == 0;
        }
    }


    public byte GetByte(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            return 0;
        }
        //return map[x, y, z];
        return map[x + width * (y + height * z)];

        //return map[x + width * (y + depth * z)];
    }







    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Regenerate();
            /*for (int x = 0; x < mesh.vertices.Length; x++)
            {
                Instantiate(sphere, mesh.vertices[x] + transform.position, Quaternion.identity);
            }
            Debug.Log(mesh.vertices.Length);*/
        }


        //Debug.Log(mesh.vertices.Length);
        /*if (mesh.vertices.Length > 65000)
        {
            map = new byte[(int)width, (int)width, (int)width];
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Regenerate();
        }*/
    }


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

        for (int x = xPose-width/2; x <  xPose+width/2 ; x++)
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

                    Debug.Log(xPos + " " + yPos + " " + zPos);
                    Instantiate(cube, new Vector3(xPos,yPos,zPos)+transform.position, Quaternion.identity);

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




/*//LEFTFACE
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
