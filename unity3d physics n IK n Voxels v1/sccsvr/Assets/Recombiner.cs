using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Recombiner : MonoBehaviour {



    public GameObject sphere;




    private Mesh mesh;
    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private Vector3 center;

    private Vector3[] OriginalVerts;

    List<Vector3> vertys = new List<Vector3>();
    List<Vector3> tryoutVerts = new List<Vector3>();
    private List<Vector3> verts = new List<Vector3>();

    int newTrianglesCount;
    private Mesh fracMesh;
    int t0;
    int t1;
    int t2;

    

    Vector3[] newVerts;


    MeshFilter meshFilt;

    List<int> trize = new List<int>();






    void Start ()
    {
        //Debug.Log("yo");

        gameObject.GetComponent<MeshRenderer>();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        vertices = gameObject.GetComponent<MeshFilter>().mesh.vertices;
        center = gameObject.GetComponent<MeshFilter>().mesh.bounds.center;
        OriginalVerts = mesh.vertices;

        for (int j = 0; j < OriginalVerts.Length; j++)
        {
            verts.Add(OriginalVerts[j]);

        }

        int[] tris = mesh.triangles;

        /*for (int i = 0; i < reassemble.tris.Count; i ++)
        {
            int[] tris = reassemble.tris[i];
        }*/

        


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

                }


            }
        }
        fracMesh = new Mesh();

        newTrianglesCount = vertys.Count / 3;

        mesh.Clear();

        fracMesh = new Mesh();
        fracMesh = transform.gameObject.GetComponent<MeshFilter>().sharedMesh;


        //Debug.Log(vertys.Count);
        fracMesh.vertices = vertys.ToArray();

        for (int i = 0; i < vertys.Count / 3; i++)
        {
            int[] trisToSet = new int[vertys.Count];

            for (int u = 0; u < trisToSet.Length; u++)
            {
                t0 = i * 3 + 0;
                t1 = i * 3 + 1;
                t2 = i * 3 + 2;
            }

            trize.Add(t0);
            trize.Add(t1);
            trize.Add(t2);
        }

        fracMesh.triangles = trize.ToArray();

        if (transform.gameObject.GetComponent<MeshRenderer>() == null)
        {
            transform.gameObject.AddComponent<MeshRenderer>();
        }

        if (transform.gameObject.GetComponent<MeshFilter>() == null)
        {
            meshFilt = transform.gameObject.AddComponent<MeshFilter>();
        }

        ;
        fracMesh.RecalculateBounds();
        fracMesh.RecalculateNormals();

        Destroy(this);
    }
	
}
