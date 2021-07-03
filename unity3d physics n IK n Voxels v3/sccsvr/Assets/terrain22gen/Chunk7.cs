using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimplexNoise;
using System.Threading;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
//[RequireComponent(typeof(MeshCollider))]

[ExecuteInEditMode]
public class Chunk7 : MonoBehaviour
{
    public int regenerateNsavemeshtofile = 0;

    //public static List<Chunk7> chunks = new List<Chunk7>();
    public static List<Vector3> chunks = new List<Vector3>();

    public static Chunk7 currentChunk;
    public int levelOfDetail;
    
    public int width = 10;
    public int height = 10;
    public int depth = 10;

    public byte[,,] map;
    protected Mesh mesh;
    protected List<Vector3> verts = new List<Vector3>();
    protected List<int> tris = new List<int>();
    protected List<Vector2> uv = new List<Vector2>();
    protected MeshCollider meshCollider;
    public float planeSize = 1f;

    public int inverseLOD;

    public int counter = 0;
    public float heightMultiplier;

    int meshSimplificationIncrement;
    int verticesPerLine;
    int seed;

    void Start()
    {


        //chunks.Add(transform.position);

        seed = 3420;// Terrain21.currentTerrain21.seed;

        /*if (counter == 0)
        {
            Debug.Log(seed);
        }*/

        currentChunk = this;

        //meshCollider = GetComponent<MeshCollider>();

        map = new byte[width, height, depth];

        //Vector3 grain0Offset = new Vector3(Random.value * 10000, Random.value * 10000, Random.value * 10000);
        //Vector3 grain1Offset = new Vector3(Random.value * 10000, Random.value * 10000, Random.value * 10000);
        //Vector3 grain2Offset = new Vector3(Random.value * 10000, Random.value * 10000, Random.value * 10000);

        for (int x = 0; x < width; x+= levelOfDetail)
        {
            for (int y = 0; y < height; y += levelOfDetail)
            {
                for (int z = 0; z < depth; z += levelOfDetail)
                {

                    Vector3 pos = new Vector3(x, y, z);
                    pos += transform.position;

                    float mountainValue = CalculateNoiseValue(pos, 0.009f);
                    mountainValue -= ((float)y / 14);

                    float noiseValue = CalculateNoiseValue(pos, 0.05f);
                    noiseValue -= ((float)y / 14);
                    noiseValue = Mathf.Max(noiseValue, CalculateNoiseValue(pos, 0.03f));
                    noiseValue -= ((float)y / 8);
                    noiseValue += mountainValue;

                    map[x, 0, z] = 1;


                    if (noiseValue > 0)
                        map[x, y, z] = 1;

                }
            }
        }
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        Regenerate();
        gameObject.GetComponent<Chunk7>().enabled = false;




        if (regenerateNsavemeshtofile == 1)
        {
            Regenerate();
            string planeAssetName = this.gameObject.name + ".asset";
            AssetDatabase.CreateAsset(mesh, "Assets/Editor/" + planeAssetName);
            AssetDatabase.SaveAssets();



        }




        /*this.GetComponent<MeshFilter>().mesh = mesh;
        this.GetComponent<MeshFilter>().sharedMesh = mesh;
        this.GetComponent<MeshFilter>().mesh.RecalculateBounds();

        //meshFilter.sharedMesh = mesh;
        //mesh.RecalculateBounds();

        if (addCollider == 1)
            this.gameObject.AddComponent(typeof(BoxCollider));

        Selection.activeObject = this.gameObject;*/
    }




    public virtual float CalculateNoiseValue(Vector3 pos , float scale)
    {
        float noiseX = Mathf.Abs((pos.x) * scale);
        float noiseY = Mathf.Abs((pos.y) * scale);
        float noiseZ = Mathf.Abs((pos.z) * scale);
        return Noise.Generate(noiseX, noiseY, noiseZ);
    }





    /*void Update()
    {
        if (counter == 1)
        {
            //Debug.Log("GOT UPDATED");
            mesh.Clear();
            meshCollider = GetComponent<MeshCollider>();

            map = new byte[(int)width, (int)width, (int)width];

            for (int x = 0; x < width; x += levelOfDetail)
            {
                //float noiseX = Mathf.Abs((float)(x + transform.position.x + seed) / 50);
                for (int z = 0; z < width; z += levelOfDetail)
                {
                    //float noiseZ = Mathf.Abs((float)(z + transform.position.z + seed) / 50);
                    for (int y = 0; y < height; y += levelOfDetail)
                    {
                        //float noiseY = Mathf.Abs((float)(y + transform.position.y + seed) / 50);

                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                        //noiseValue += (10f - (float)y) / 10;
                        //noiseValue /= (float)y / 5;

                        //if (noiseValue > 1f)
                        //{
                            map[x, y, z] = 1;
                        //}
                        //map[x, 0, z] = 1;
                        //map[x, y, z] = 1;

                    }
                }
            }
                    mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            Regenerate();
             //gameObject.GetComponent<Chunk2>().enabled = false;
            counter = 0;
        }
       
    }*/



    public void Regenerate()
    {
        verts.Clear();
        tris.Clear();
        uv.Clear();
        mesh.triangles = tris.ToArray();

        for (int x = 0; x < width; x ++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z ++)
                {
                    byte block = map[x, y, z];
                    if (block == 0) continue;
                    {
                        DrawBrick(x, y, z);
               
                    }
                }
            }
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = mesh;
        //gameObject.GetComponent<MeshCollider>().enabled = false;
       
    }


    /*public void DrawBrick(int x, int y, int z)
    {
        Vector3 start = new Vector3((x), (y) ,(z)) ;
        Vector3 offset1, offset2;

        //TOPFACE
        if (IsTransparent(x, y+1* levelOfDetail, z))
        {
            offset1 = new Vector3(-1,0,0);
            offset2 = new Vector3(0,0,1);
            DrawFace(start  + (new Vector3(1, 0, 0) * levelOfDetail), (offset1* levelOfDetail ), (offset2*levelOfDetail) );
        }

       //BOTTOM FACE
        if (IsTransparent(x, y - 1 * levelOfDetail, z))
        {
            offset1 = (new Vector3(1, 0, 0));
            offset2 = ( new Vector3(0, 0, 1));
            DrawFace(start + Vector3.down , offset1 * levelOfDetail, offset2 * levelOfDetail);
        }

        //RIGHTFACE
        if (IsTransparent(x - 1, y, z))
        {
            offset1 = new Vector3(0, -1, 0) * heightMultiplier * levelOfDetail;
            offset2 = new Vector3(0, 0, 1) * levelOfDetail;
            DrawFace(start, offset1, offset2 );
        }

        //LEFTFACE
        if (IsTransparent(x + 1 , y, z))
        {
            offset1 = new Vector3(0, 1, 0) * heightMultiplier;
            offset2 = (new Vector3(0, 0, 1));
            DrawFace(start + Vector3.right * levelOfDetail + Vector3.down * heightMultiplier, offset1 , offset2 * levelOfDetail);
        }

        //BACKFACE
        /*if (IsTransparent(x, y, z + 1 ))
        {
            offset1 = new Vector3(-1, 0, 0) * levelOfDetail;
            offset2 = new Vector3(0, -1, 0) * heightMultiplier;
            DrawFace(start + Vector3.right * levelOfDetail + Vector3.forward * levelOfDetail, offset1, offset2);
        }


        //FRONTFACE
        if (IsTransparent(x, y, z - 1 ))
        {
            offset1 = new Vector3(1, 0, 0) * levelOfDetail;
            offset2 = new Vector3(0, -1, 0) * heightMultiplier;
            DrawFace(start, offset1, offset2);
        }


    }*/



    public void DrawBrick(int x, int y, int z)
    {
        /*Vector3 start = new Vector3(x, y , (z + levelOfDetail));
        Vector3 offset1, offset2;

        //BOTTOMFACE
        if (IsTransparent(x, y - 1 * levelOfDetail, z))
        {
            offset1 = Vector3.left * levelOfDetail;
            offset2 = Vector3.back * levelOfDetail;
            DrawFace(start  + Vector3.right * levelOfDetail, offset1, offset2);
        }
        //TOPFACE
        if (IsTransparent(x, y + 1 * levelOfDetail, z))
        {
            offset1 = Vector3.right * levelOfDetail;
            offset2 = Vector3.back * levelOfDetail;
            DrawFace(start + Vector3.up * levelOfDetail, offset1, offset2);
        }

        //LEFTFACE
        if (IsTransparent(x - 1 * levelOfDetail, y, z))
        {
            offset1 = Vector3.up * levelOfDetail;
            offset2 = Vector3.back * levelOfDetail;
            DrawFace(start, offset1, offset2);
        }

        //RIGHTFACE
        if (IsTransparent(x + 1 * levelOfDetail, y, z))
        {
            offset1 = Vector3.down * levelOfDetail;
            offset2 = Vector3.back * levelOfDetail;
            DrawFace(start + Vector3.right * levelOfDetail + Vector3.up * levelOfDetail, offset1, offset2);
        }



        if (IsTransparent(x, y, z - 1 * levelOfDetail))
        {
            offset1 = Vector3.left * levelOfDetail;
            offset2 = Vector3.up * levelOfDetail;
            DrawFace(start + Vector3.right * levelOfDetail + Vector3.back * levelOfDetail, offset1, offset2);
        }


        if (IsTransparent(x, y, z + 1 * levelOfDetail))
        {
            offset1 = Vector3.right * levelOfDetail;
            offset2 = Vector3.up * levelOfDetail;
            DrawFace(start, offset1, offset2);
        }*/


        Vector3 start = new Vector3(x * planeSize, y * planeSize, z * planeSize);
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
            offset1 = Vector3.right * planeSize * levelOfDetail;
            offset2 = Vector3.forward * planeSize * levelOfDetail;
            DrawFace(start, offset1, offset2);
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



    public bool IsTransparent(float x, float y, float z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth)) return true;
        {
            return map[(int)x, (int)y, (int)z] == 0;
        }
    }
























    public void SetBrick(float x, float y, float z, byte block)
    {
        x -= ((transform.position.x) / planeSize);
        y -= ((transform.position.y) / planeSize);
        z -= ((transform.position.z) / planeSize);

        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth))
        {
            return;
        }


        map[(int)x, (int)y, (int)z] = block;
        Regenerate();

        if (map[(int)x, (int)y, (int)z] != block)
        {
            map[(int)x, (int)y, (int)z] = block;
            Regenerate();
        }
    }



    public bool FindChunk(Vector3 pos)
    {

        for (int a = 0; a < chunks.Count; a++)
        {
            Vector3 cpos = chunks[a];

            if ((pos.x < cpos.x) || (pos.z < cpos.z) || (pos.x >= cpos.x + width) || (pos.z >= cpos.z + depth)) continue;
            return true;
        }
        return false;
    }








    /*void OnDrawGizmos()
    {
        if (mesh.vertices == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(mesh.vertices[i] + transform.position, 0.1f);
        }
    }*/











}
