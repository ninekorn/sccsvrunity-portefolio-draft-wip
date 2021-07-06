using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SplitMesh : MonoBehaviour
{

    public Material mat;
    Mesh mesh;


    Vector3[] newVerts;
    Vector3[] newNormals;
    Vector2[] newUvs;

    List<Vector3> vertys = new List<Vector3>();
    List<Vector3> norms = new List<Vector3>();
    Vector3[] normalz;
    int[] triang;

    void Start()
    {
        SplitMesh1();
    }

    void SplitMesh1()
    {
        MeshFilter MF = GetComponent<MeshFilter>();
        MeshRenderer MR = GetComponent<MeshRenderer>();
        Mesh M = MF.sharedMesh;

        Vector3[] verts = M.vertices;
        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {
            int[] indices = M.GetTriangles(submesh);
            for (int i = 0; i < indices.Length; i += 3)
            {
                newVerts = new Vector3[18];
                newUvs = new Vector2[3];

                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    vertys.Add(newVerts[n]);        
                }                       
            }
        }
        mesh = new Mesh();
        mesh.Clear();

        mesh.vertices = vertys.ToArray();

        normalz = new Vector3[mesh.vertices.Length];

        Vector2[] uvsss = new Vector2[mesh.vertices.Length];

         for (int u = 0; u < uvsss.Length; u++)
         {
             uvsss[u] = new Vector2(mesh.vertices[u].x, mesh.vertices[u].z);
         }

        triang = new int[]

        {
                             0,1,2,
                             3,4,5,
                             6,7,8,
                             9,10,11,
                             12,13,14,
                             15,16,17,
        };

        mesh.normals = normalz;
        mesh.uv = uvsss;

        mesh.triangles = triang;

        MF.sharedMesh = mesh;


        Material newMat = Resources.Load("Material/TerrainColor", typeof(Material)) as Material;
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = newMat;

        ;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}