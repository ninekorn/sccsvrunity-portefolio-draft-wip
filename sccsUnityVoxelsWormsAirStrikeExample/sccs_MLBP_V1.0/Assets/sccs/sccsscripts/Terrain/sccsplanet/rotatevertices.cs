using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCCoreSystems;

[ExecuteInEditMode]
public class rotatevertices : MonoBehaviour {

    Transform pivotOfObject;
    Vector3[] vertices;
    int[] triangles;

    void Start ()
    {
        Vector3 center = new Vector3(transform.position.x, transform.position.y, transform.position.z);//any V3 you want as the pivot point.
        Quaternion newRotation = new Quaternion();
        newRotation.eulerAngles = new Vector3(90, 0, 0);//the degrees the vertices are to be rotated, for example (0,90,0) 


        vertices = this.gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;
        triangles = this.gameObject.GetComponent<MeshFilter>().sharedMesh.triangles;
        /*for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = sc_maths.RotatePoint(vertices[i]);
        }*/
       
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = newRotation * (vertices[i]);
            //vertices[i] = newRotation * (vertices[i] - center) + center;
        }
        buildMesh();
    }


    public void buildMesh()
    {
        this.gameObject.GetComponent<MeshFilter>().mesh.Clear();
        this.gameObject.GetComponent<MeshFilter>().sharedMesh.vertices = vertices;
        this.gameObject.GetComponent<MeshFilter>().sharedMesh.triangles = triangles;
        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = mesh;
        this.gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        this.gameObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();

        /*if (this.gameObject.GetComponent<MeshCollider>() == null)
        {
            this.gameObject.AddComponent<MeshCollider>();
        }
        else
        {
            Destroy(this.gameObject.GetComponent<MeshCollider>());
            this.gameObject.AddComponent<MeshCollider>();
        }*/
    }
    // Update is called once per frame
    void Update () {
		
	}
}
