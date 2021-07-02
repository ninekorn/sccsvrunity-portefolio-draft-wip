using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]

public class sccspolygon : MonoBehaviour
{

    public string matname = "material name";

    public Vector3[] vertices;
    Vector2[] uv;


    public int startswtch = 0;
    public int getverts = 0;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    private Mesh mesh;


    void Start()
    {

    }


    void Awake ()
    {
        //vertices = this.GetComponent<MeshFilter>().mesh.vertices;



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




        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        uv = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            uv[i] = new Vector2(0, 0);
        }

        //this.GetComponent<MeshFilter>().mesh.vertices = vertices;
        mesh.vertices = vertices;
        mesh.uv = uv;
        int[] triangles = new int[]
          {
                    0,1,2,3,2,1,
                    1,2,3,2,1,0
          };

        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        var shippartmaterial = Resources.Load(matname, typeof(Material)) as Material;
        this.GetComponent<MeshRenderer>().material = shippartmaterial;

        string planeAssetName = this.gameObject.name + ".asset";
        //Mesh m = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Editor/" + planeAssetName, typeof(Mesh));

        Mesh m = new Mesh();
        m.name = this.gameObject.name;
        AssetDatabase.CreateAsset(m, "Assets/Editor/" + planeAssetName);
        AssetDatabase.SaveAssets();
        //meshFilter.sharedMesh = m;
        //m.RecalculateBounds();

        this.GetComponent<MeshFilter>().mesh = m;
        this.GetComponent<MeshFilter>().sharedMesh = m;
        this.GetComponent<MeshFilter>().mesh.RecalculateBounds();

        //if (addCollider)
        //    this.gameObject.AddComponent(typeof(BoxCollider));

        //Selection.activeObject = this.gameObject;

        //vertices[0].x = vertBottomLeft.x;
        //vertices[0].x = vertBottomLeft.x;
    }

    void Update ()
    {
        if (getverts == 1)
        {
            vertices = this.GetComponent<MeshFilter>().mesh.vertices;
            getverts = 0;
        }

        if (startswtch == 1)
        {
            if (vertices.Length > 0)
            {
                
            }

            /*vertices = this.GetComponent<MeshFilter>().mesh.vertices;

            vertices[0] = vertBottomLeft;
            vertices[1] = vertTopLeft;
            vertices[2] = vertBottomRight;
            vertices[3] = vertTopRight;*/
            //DestroyImmediate(this);
            startswtch = 0;
        }

	}
}



//public Vector3 vertBottomLeft = new Vector3((-1 * 0.5f) - (0 * 0.5f), (-1 * 0.5f) - (0 * 0.5f),0);
//public Vector3 vertTopLeft = new Vector3((-1 * 0.5f) - (0 * 0.5f), (1 * 0.5f) - (0 * 0.5f), 0);
//public Vector3 vertBottomRight = new Vector3((1 * 0.5f) - (0 * 0.5f), (-1 * 0.5f) - (0 * 0.5f), 0);
//public Vector3 vertTopRight = new Vector3((1 * 0.5f) - (0 * 0.5f), (1 * 0.5f) - (0 * 0.5f), 0);
//vertices = new Vector3[4];
//vertices[0] = vertBottomLeft;
//vertices[1] = vertTopLeft;
///vertices[2] = vertBottomRight;
//vertices[3] = vertTopRight;
//uv = new Vector2[4];
//uv[0] = new Vector2(0, 0);
//uv[1] = new Vector2(0, 0);
//uv[2] = new Vector2(0, 0);
//uv[3] = new Vector2(0, 0);