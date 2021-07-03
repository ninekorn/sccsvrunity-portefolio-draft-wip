using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class SplitMeshIntoTriangles : MonoBehaviour
{
    public int swtch = 0;


    void Awake()
    {
        //SplitMesh();
    }


    public void Update()
    {


        if (swtch ==1 )
        {
            MeshFilter MF = GetComponent<MeshFilter>();
            MeshRenderer MR = GetComponent<MeshRenderer>();
            Mesh M = MF.sharedMesh;
            Vector3[] verts = M.vertices;
            Vector3[] normals = M.normals;
            Vector2[] uvs = M.uv;

            int trigindex = 0;

            for (int submesh = 0; submesh < M.subMeshCount; submesh++)
            {
                int[] indices = M.GetTriangles(submesh);

                for (int i = 0; i < indices.Length; i += 3)
                {
                    Vector3[] newVerts = new Vector3[3];
                    Vector3[] newNormals = new Vector3[3];
                    Vector2[] newUvs = new Vector2[3];
                    for (int n = 0; n < 3; n++)
                    {
                        int index = indices[i + n];
                        newVerts[n] = verts[index];
                        newUvs[n] = uvs[index];
                        newNormals[n] = normals[index];
                    }
                    Mesh mesh = new Mesh();
                    mesh.vertices = newVerts;
                    mesh.normals = newNormals;
                    mesh.uv = newUvs;

                    mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                    GameObject GO = new GameObject("Triangle " + (i / 3));
                    GO.transform.position = transform.position;
                    GO.transform.rotation = transform.rotation;
                    GO.transform.parent = this.transform;

                    GO.AddComponent<MeshRenderer>().sharedMaterial = MR.sharedMaterials[submesh];
                    //GO.AddComponent<MeshRenderer>().sharedMaterial = MR.sharedMaterials[submesh];

                    GO.AddComponent<MeshFilter>().sharedMesh = mesh;



                    //GO.transform.parent = null;

                    var rigid = GO.AddComponent<Rigidbody2D>();
                    var col = GO.AddComponent<PolygonCollider2D>();




                    if (trigindex == 0)
                    {
                        var points = new Vector2[]
                        {
                        new Vector2(newVerts[0].x,newVerts[0].y),
                        new Vector2(newVerts[1].x,newVerts[1].y),
                        new Vector2(newVerts[2].x,newVerts[2].y),
                        };


                        /*var points = new Vector2[]
                        {
                            new Vector2(-0.0125f,-0.0125f),
                            new Vector2(-0.0125f,0.0125f),
                            new Vector2(0.0125f,-0.0125f),
                        };*/
                        col.points = points;
                    }
                    else
                    {
                        var points = new Vector2[]
                         {
                            new Vector2(newVerts[0].x,newVerts[0].y),
                            new Vector2(newVerts[1].x,newVerts[1].y),
                            new Vector2(newVerts[2].x,newVerts[2].y),
                         };
                        /*var points = new Vector2[]
                        {
                            new Vector2(0,0.0125f),
                            new Vector2(0.025f,0.0125f),
                            new Vector2(0.025f,-0.0125f),
                        };*/

                        //col.points = points;
                        col.points = points;
                    }
                }
                trigindex++;
            }

            MR.enabled = false;
            //Destroy(gameObject);

            /*if (this.gameObject != null)
            {
                if (this.gameObject.tag == "bulletshot")
                {
                    Destroy(this.gameObject);
                }
            }*/


            swtch = 0;
        }

    }

}











//GO.AddComponent<BoxCollider>();
//GO.AddComponent<Rigidbody>().AddExplosionForce(100, transform.position, 30);

//Destroy(GO, 5 + Random.Range(0.0f, 5.0f));

