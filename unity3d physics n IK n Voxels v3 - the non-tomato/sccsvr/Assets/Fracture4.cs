using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEditor;

[ExecuteInEditMode]
public class Fracture4 : MonoBehaviour
{
    public GameObject sphere;
    private List<Vector3> verts = new List<Vector3>();
    
    private Mesh mesh;
    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private Vector3 center;

    private Vector3[] OriginalVerts;

    private List<GameObject> FracturingObj = new List<GameObject>();

    Vector3[] vertTrigList;

    List<Vector3> vertys = new List<Vector3>();
    List<Vector3> tetraHedron = new List<Vector3>();
    List<Vector3> doubleVertys = new List<Vector3>();

    Vector3[] centering;
    int newTrianglesCount;
    Vector3[] normalz;

    Vector3 p0;
    Vector3 p1;
    Vector3 p2;
    Vector3 p3;

    private Mesh fracMesh;

    Vector3[] newVerts;
    Vector3[] doubleVerts;
    Vector3 singleVert;
    Vector3[] verto;
    Vector3 curVert;

    float radius;
    MeshFilter meshFilt;
    public int explosionForce;
    Vector2[] uvs;

    public int regenerateNsavemeshtofile = 1;

    void Start()
    {
        gameObject.GetComponent<MeshRenderer>();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        vertices = gameObject.GetComponent<MeshFilter>().mesh.vertices;
        center = gameObject.GetComponent<MeshFilter>().mesh.bounds.center;
        OriginalVerts = mesh.vertices;

        for (int j = 0; j < OriginalVerts.Length; j++)
        {        
            verts.Add(OriginalVerts[j]);
            
        }

        int[] tris = mesh.triangles;

        for (int submesh = 0; submesh < mesh.subMeshCount; submesh++)
        {
            for (int i = 0; i < tris.Length; i += 3)
            {           
                newVerts = new Vector3[tris.Length];              
                for (int n = 0; n < 3; n++)
                {
                    int index = tris[i + n];
                    newVerts[n] = verts[index];
                    vertys.Add(newVerts[n]);
                    tetraHedron.Add(newVerts[n]);
                    doubleVertys.Add(newVerts[n]);
                }


            }
        }
        fracMesh = new Mesh();

        if (tetraHedron.Count == 12)
        {

            for (int i = 0; i < tetraHedron.Count; i++)
            {
                curVert = tetraHedron[i];
                verto = new Vector3[tetraHedron.Count];
                for (int j = i + 1; j < tetraHedron.Count; j++)
                {
                    if (Vector3.Distance(curVert, tetraHedron[j]) < 0.0001f)
                    {
                        tetraHedron[j] = curVert;                   
                    }
                }
            }
            tetraHedron = tetraHedron.Distinct().ToList();

            Vector3 A = tetraHedron[0];
            Vector3 B = tetraHedron[1];
            Vector3 C = tetraHedron[2];
            Vector3 D = tetraHedron[3];

            Vector3 yo = new Vector3((A.x + B.x + C.x + D.x) / 4, (A.y + B.y + C.y + D.y) / 4, (A.z + B.z + C.z + D.z) / 4);

            radius = Random.Range((float)0, (float)0.25f);

            Vector3 randomPoint = Random.insideUnitSphere * radius + yo;

            Vector3 centero = randomPoint;

            centero = randomPoint;

            for (int i = 0; i < vertys.Count; i++)
            {
                centering = new Vector3[vertys.Count];
            }

            for (int i = 0; i < vertys.Count; i++)
            {
                doubleVerts = new Vector3[vertys.Count * 2];
            }


            for (int i = 0; i < doubleVerts.Length; i++)
            {
                doubleVerts[i] = vertys[i];
                singleVert = doubleVerts[i];
                vertys.Add(singleVert);
                doubleVertys.Add(singleVert);
            }



            for (int j = 0; j < centering.Length; j++)
            {
                centering[j] = centero;
                vertys.Add(centering[j]);
            }

            newTrianglesCount = vertys.Count / 3;
            for (int i = 0; i < newTrianglesCount / 4; i++)
            {

                GameObject singleFrac = new GameObject();
                FracturingObj.Add(singleFrac);

                p0 = vertys[i * 3 + 0];
                p1 = vertys[i * 3 + 1];
                p2 = vertys[i * 3 + 2];
                p3 = vertys[i * 3 + doubleVertys.Count];


                vertTrigList = new Vector3[]
                 {
                    p0,p1,p2,p3,
                 };
              
                FracturingObj[i].transform.position = transform.position;
                //FracturingObj[i].transform.rotation = transform.rotation;
                FracturingObj[i].transform.localScale = transform.localScale *0.97f;





                if (FracturingObj[i].transform.gameObject.GetComponent<MeshRenderer>() == null)
                {
                    FracturingObj[i].transform.gameObject.AddComponent<MeshRenderer>();
                }

                if (FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>() == null)
                {
                    meshFilt = FracturingObj[i].transform.gameObject.AddComponent<MeshFilter>();
                }

                mesh.Clear();
                FracturingObj[i].transform.name = "Fragment" + " " + i;
                fracMesh.name = "Fragment" + " " + i;
                FracturingObj[i].transform.tag = "Fragment";
                FracturingObj[i].transform.parent = this.transform;

                //to readd
                //FracturingObj[i].transform.gameObject.AddComponent<activateCombine>().enabled = true;
                //to readd


                //FracturingObj[i].transform.gameObject.AddComponent<combine>().enabled = false;

                fracMesh.vertices = vertTrigList;
                
                normalz = new Vector3[vertTrigList.Length];
                

                int[] newTrigs = new int[]
                {           
                        0,1,2,
                        3,1,0,
                        3,2,1,
                        0,2,3,
                };


                Material newMat = Resources.Load("Material/FracColor3", typeof(Material)) as Material;
                FracturingObj[i].transform.gameObject.GetComponent<MeshRenderer>().material = newMat;
                           
                FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>().sharedMesh = fracMesh;

                Material[] m = new Material[]
                {
                    Resources.Load("Material/FracColor3") as Material,
                    Resources.Load("Material/FracColor4") as Material,
                    Resources.Load("Material/FracColor4") as Material,
                    Resources.Load("Material/FracColor4") as Material,
                };

                

                //fracMesh.Clear();

                fracMesh.subMeshCount = 4;

                int[] a1 = new int[] { 0, 1, 2 };
                fracMesh.SetTriangles(a1, 0);

                int[] a2 = new int[] { 3, 1, 0 };
                fracMesh.SetTriangles(a2, 1);

                int[] a3 = new int[] { 3, 2, 1 };
                fracMesh.SetTriangles(a3, 2);

                int[] a4 = new int[] { 0, 2, 3 };
                fracMesh.SetTriangles(a4, 3);


                meshFilt.mesh = fracMesh;
                FracturingObj[i].GetComponent<Renderer>().materials = m;
                fracMesh.normals = normalz;
                fracMesh.uv = uvs;

                FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                /*if (FracturingObj[i].transform.gameObject.GetComponent<MeshCollider>() == null)
                {
                    MeshCollider meshCol = FracturingObj[i].gameObject.AddComponent<MeshCollider>();
                    meshCol.convex = true;
                }*/


                //to readd
                //FracturingObj[i].transform.gameObject.AddComponent<sccsFragmentCollider>().enabled = true;
                //to readd





                /*
                if (FracturingObj[i].transform.gameObject.GetComponent<Rigidbody>() == null)
                {
                    FracturingObj[i].gameObject.AddComponent<Rigidbody>().AddExplosionForce(explosionForce, FracturingObj[i].transform.position, 100f, 0.1f);
                }*/







                /*if (gameObject.GetComponent<MeshCollider>() != null)
                {
                    Destroy(gameObject.GetComponent<MeshCollider>());
                }
                if (gameObject.GetComponent<BoxCollider>() != null)
                {
                    Destroy(gameObject.GetComponent<BoxCollider>());
                }
                if (gameObject.GetComponent<SphereCollider>() != null)
                {
                    Destroy(gameObject.GetComponent<SphereCollider>());
                }*/

                //AssetDatabase.CreateAsset(fracMesh, "Assets/Resources/Meshes" + FracturingObj[i].gameObject.name + ".asset");
                //AssetDatabase.SaveAssets();


                //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }



        }


        else
        {

            for (int i = 0; i < vertys.Count; i++)
            {
                centering = new Vector3[vertys.Count];

            }

            for (int i = 0; i < vertys.Count; i++)
            {
                doubleVerts = new Vector3[vertys.Count * 2];
            }


            for (int i = 0; i < doubleVerts.Length; i++)
            {
                doubleVerts[i] = vertys[i];
                singleVert = doubleVerts[i];              
                vertys.Add(singleVert);
                doubleVertys.Add(singleVert);
            }


            radius = Random.Range((float)0f, (float)0.25f);

            Vector3 randomPoint = Random.insideUnitSphere * radius + center;

            Vector3 centero = randomPoint;


            for (int j = 0; j < centering.Length; j++)
            {
                centering[j] = randomPoint;
                vertys.Add(centering[j]);
            }

            
            newTrianglesCount = vertys.Count / 3;
            //Debug.Log(vertys.Count);
            mesh.Clear();
            for (int i = 0; i < newTrianglesCount / 4; i++)
            {

                GameObject singleFrac = new GameObject();

                FracturingObj.Add(singleFrac);

                p0 = vertys[i * 3 + 0];
                p1 = vertys[i * 3 + 1];
                p2 = vertys[i * 3 + 2];
                p3 = vertys[i * 3 + doubleVertys.Count];

                vertTrigList = new Vector3[]
                 {
                    p0,p1,p2,p3,
                 };

                //Debug.Log(vertTrigList.Length);
                FracturingObj[i].transform.position = transform.position;
                //FracturingObj[i].transform.rotation = transform.rotation;
                FracturingObj[i].transform.localScale = transform.localScale * 0.97f;

                if (FracturingObj[i].transform.gameObject.GetComponent<MeshRenderer>() == null)
                {
                    FracturingObj[i].transform.gameObject.AddComponent<MeshRenderer>();
                }

                if (FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>() == null)
                {
                    meshFilt = FracturingObj[i].transform.gameObject.AddComponent<MeshFilter>();
                }

                fracMesh = FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>().sharedMesh;
                fracMesh = new Mesh();

                FracturingObj[i].transform.name = "Fragment" + " " + i;
                fracMesh.name = "Fragment" + " " + i;
                FracturingObj[i].transform.tag = "Fragment";

                FracturingObj[i].transform.gameObject.AddComponent<activateCombine>().enabled = true;
                FracturingObj[i].transform.parent = this.transform;
                //FracturingObj[i].transform.parent.GetComponent<reparator>().objToReact.Add(FracturingObj[i]);
                //FracturingObj[i].transform.parent.GetComponent<reparator>().counter = 1;

                var parentObject = FracturingObj[i].transform.parent;
                //FracturingObj[i].transform.parent = null;
                //Destroy(parentObject);
                parentObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                //FracturingObj[i].transform.gameObject.AddComponent<combine>().enabled = false;


                fracMesh.vertices = vertTrigList;
                //Debug.Log(fracMesh.vertices.Length);
                //Debug.Log(fracMesh.vertices.Length);

             




                uvs = new Vector2[fracMesh.vertices.Length];

                for (int j = 0; j < uvs.Length; j++)
                {
                    uvs[j] = new Vector2(fracMesh.vertices[j].x, fracMesh.vertices[j].z);
                }           

                Material[] m = new Material[]
                {
                    Resources.Load("Material/FracColor3") as Material,
                    Resources.Load("Material/FracColor4") as Material,
                    Resources.Load("Material/FracColor4") as Material,
                    Resources.Load("Material/FracColor4") as Material,
                };

                meshFilt.mesh = fracMesh;
                FracturingObj[i].GetComponent<Renderer>().materials = m;

                fracMesh.subMeshCount = 4;

                int[] a1 = new int[] { 0, 1, 2 };
                fracMesh.SetTriangles(a1, 0);

                int[] a2 = new int[] { 3, 1, 0 };
                fracMesh.SetTriangles(a2, 1);

                int[] a3 = new int[] { 3, 2, 1 };
                fracMesh.SetTriangles(a3, 2);

                int[] a4 = new int[] { 0, 2, 3 };
                fracMesh.SetTriangles(a4, 3);

                //normalz = new Vector3[vertTrigList.Length];
                normalz = FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices;
                fracMesh.normals = normalz;
                fracMesh.uv = uvs;

                ;
                fracMesh.RecalculateBounds();
                fracMesh.RecalculateNormals();

                /*if (FracturingObj[i].transform.gameObject.GetComponent<MeshCollider>() == null)
                {
                    MeshCollider meshCol = FracturingObj[i].gameObject.AddComponent<MeshCollider>();
                    meshCol.convex = true;
                }*/


                FracturingObj[i].transform.gameObject.AddComponent<sccsFragmentCollider>().enabled = true;

                FracturingObj[i].transform.gameObject.AddComponent<Recombiner>().enabled = true;
                /*if (FracturingObj[i].transform.gameObject.GetComponent<Rigidbody>() == null)
                {
                    FracturingObj[i].gameObject.AddComponent<Rigidbody>().AddExplosionForce(explosionForce, FracturingObj[i].transform.position, 10000f, 100000f);
                }
                if (FracturingObj[i].transform.gameObject.GetComponent<Rigidbody>() != null)
                {
                    FracturingObj[i].gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, FracturingObj[i].transform.position, 10000f, 100000f);
                }*/

                /*
                if (gameObject.GetComponent<MeshCollider>() != null)
                {
                    Destroy(gameObject.GetComponent<MeshCollider>(),0.1f);
                }
                if (gameObject.GetComponent<BoxCollider>() != null)
                {
                    Destroy(gameObject.GetComponent<BoxCollider>(),0.1f);
                }
                if (gameObject.GetComponent<SphereCollider>() != null)
                {
                    Destroy(gameObject.GetComponent<SphereCollider>(),0.1f);
                }*/



                /*if (regenerateNsavemeshtofile == 1)
                {

                    string planeAssetName = FracturingObj[i].transform.gameObject.name + ".asset";
                    AssetDatabase.CreateAsset(fracMesh, "Assets/Editor/" + planeAssetName);
                    AssetDatabase.SaveAssets();
                }*/




                //gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //transform.DetachChildren();





                //AssetDatabase.CreateAsset(fracMesh, "Assets/Resources/Meshes" + FracturingObj[i].gameObject.name);
                //AssetDatabase.SaveAssets();

            }
        }


    }
}
