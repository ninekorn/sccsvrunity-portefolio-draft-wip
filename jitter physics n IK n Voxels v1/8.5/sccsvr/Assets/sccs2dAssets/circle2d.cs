using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class circle2d : MonoBehaviour
{

    public int vertexnumberonperimeter = 10;
    public float radius = 0.5f;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    void Awake()
    {
        createCircle();
    }

    void Update()
    {

    }

    void createCircle()
    {
        if ((MeshFilter)this.gameObject.GetComponent(typeof(MeshFilter)) == null)
        {
            meshFilter = (MeshFilter)this.gameObject.AddComponent(typeof(MeshFilter));
        }
        else
        {
            meshFilter = (MeshFilter)this.gameObject.GetComponent(typeof(MeshFilter));
        }


        if ((MeshRenderer)this.gameObject.GetComponent(typeof(MeshRenderer)) == null)
        {
            meshRenderer = (MeshRenderer)this.gameObject.AddComponent(typeof(MeshRenderer));
        }
        else
        {
            meshRenderer = (MeshRenderer)this.gameObject.GetComponent(typeof(MeshRenderer));
        }



        string planeAssetName = "unifycircle2d" + ".asset";
        Mesh m = m = new Mesh(); //(Mesh)AssetDatabase.LoadAssetAtPath("Assets/Editor/" + planeAssetName, typeof(Mesh));
                                 //m = new Mesh();
        m.name = this.gameObject.name;

        List<Vector3> vertexList = new List<Vector3> { };
        float x;
        float y;

        vertexList.Add(new Vector3(0, 0, 0f));
        for (int i = 0; i < vertexnumberonperimeter; i++)
        {
            x = radius * Mathf.Sin((2 * Mathf.PI * i) / vertexnumberonperimeter);
            y = radius * Mathf.Cos((2 * Mathf.PI * i) / vertexnumberonperimeter);
            vertexList.Add(new Vector3(x, y, 0f));
        }
        Vector3[] vertex = vertexList.ToArray();


        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (vertexnumberonperimeter - 1); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add((i) + 1);
            trianglesList.Add((i) + 2);
        }

        trianglesList.Add(0);
        trianglesList.Add(vertexList.Count - 1);
        trianglesList.Add(1);

        //int[] triangles = trianglesList.ToArray();


        int[] invertedtriangles = new int[trianglesList.Count];

        for (int i = 0; i < trianglesList.Count; i++)
        {
            invertedtriangles[trianglesList.Count - 1 - i] = trianglesList[i];
        }

        int[] totaltriangles = new int[trianglesList.Count * 2];

        int otheri = 0;
        for (int i = 0; i < totaltriangles.Length; i++)
        {
            if (i < trianglesList.Count)
            {
                totaltriangles[i] = trianglesList[i];
            }
            else
            {
                totaltriangles[i] = invertedtriangles[otheri];
                otheri++;
            }
        }

















        List<Vector3> normalsList = new List<Vector3> { };
        for (int i = 0; i < vertex.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();




        m.vertices = vertex;
        m.triangles = totaltriangles;
        m.normals = normals;

        //m.vertices = vertexList.ToArray();
        //m.uv = uv;
        //m.triangles = triangles;
        //m.tangents = tangents;
        //m.RecalculateNormals();

        AssetDatabase.CreateAsset(m, "Assets/Editor/" + planeAssetName);
        AssetDatabase.SaveAssets();

        //if (m == null)
        //{   
        //}


        this.GetComponent<MeshFilter>().mesh = m;
        this.GetComponent<MeshFilter>().sharedMesh = m;
        this.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        //meshFilter.sharedMesh = m;
        //m.RecalculateBounds();





























        /*
        GameObject unifycircle2d = new GameObject();

        MeshFilter meshFilter = (MeshFilter)unifycircle2d.AddComponent(typeof(MeshFilter));
        unifycircle2d.AddComponent(typeof(MeshRenderer));

        //plane.name + widthSegments + "x" + lengthSegments + "W" + width + "L" + length + (orientation == Orientation.Horizontal ? "H" : "V") + anchorId +

        string planeAssetName = "unifycircle2d" + ".asset";
        Mesh mesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Editor/" + planeAssetName, typeof(Mesh));







        List<Vector3> vertexList = new List<Vector3> { };
        float x;
        float y;

        vertexList.Add(new Vector3(0, 0, 0f));
        for (int i = 0; i < vertexnumberonperimeter; i++)
        {
            x = radius * Mathf.Sin((2 * Mathf.PI * i) / vertexnumberonperimeter);
            y = radius * Mathf.Cos((2 * Mathf.PI * i) / vertexnumberonperimeter);
            vertexList.Add(new Vector3(x, y, 0f));
        }
        Vector3[] vertex = vertexList.ToArray();



        //List<int> trianglesList = new List<int> { };
        //for (int i = 0; i < (n-2); i++)
        //{
        //    trianglesList.Add(0);
        //    trianglesList.Add((i) + 1);
        //    trianglesList.Add((i) + 2);
        //}
        //int[] triangles = trianglesList.ToArray();


        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (vertexnumberonperimeter - 1); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add((i) + 1);
            trianglesList.Add((i) + 2);
        }

        trianglesList.Add(0);
        trianglesList.Add(vertexList.Count - 1);
        trianglesList.Add(1);

        int[] triangles = trianglesList.ToArray();

        List<Vector3> normalsList = new List<Vector3> { };
        for (int i = 0; i < vertex.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();

        //GameObject newObject = new GameObject();
        //newObject.transform.position = new Vector3(0, 0, 0);

        //MeshRenderer meshRend = this.gameObject.GetComponent<MeshRenderer>();
        //MeshFilter meshFilt = this.gameObject.GetComponent<MeshFilter>();
        
        mesh = new Mesh();
        mesh.vertices = vertex;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.RecalculateNormals();

        /*m.vertices = vertices;
        m.uv = uvs;
        m.triangles = triangles;
        m.tangents = tangents;
        m.RecalculateNormals();

        AssetDatabase.CreateAsset(mesh, "Assets/Editor/" + planeAssetName);
        AssetDatabase.SaveAssets();


        meshFilter.sharedMesh = meshFilter.mesh;
        meshFilter.mesh.RecalculateBounds();


        //unifycircle2d.GetComponent<MeshFilter>().mesh = mesh;
        unifycircle2d.GetComponent<MeshFilter>().sharedMesh = mesh;

        //unifycircle2d.GetComponent<MeshFilter>() = meshFilter;




        Selection.activeObject = unifycircle2d;*/

    }
}
