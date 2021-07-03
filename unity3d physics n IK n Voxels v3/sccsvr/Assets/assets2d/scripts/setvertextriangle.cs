using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class setvertextriangle : MonoBehaviour
{
    public Vector3 vertBottomLeft = new Vector3((-1 * 0.5f) - (0 * 0.5f), (-1 * 0.5f) - (0 * 0.5f),0);
    public Vector3 top = new Vector3((-1 * 0.5f) - (0 * 0.5f), (1 * 0.5f) - (0 * 0.5f), 0);
    public Vector3 vertBottomRight = new Vector3((1 * 0.5f) - (0 * 0.5f), (-1 * 0.5f) - (0 * 0.5f), 0);


    public Vector3[] vertices;

    public int startswtch = 0;
    public int getverts = 0;

    void Start()
    {

    }


    void Awake ()
    {
        vertices = this.GetComponent<MeshFilter>().mesh.vertices;

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
            vertices = this.GetComponent<MeshFilter>().mesh.vertices;

            vertices[0] = vertBottomLeft;
            vertices[1] = top;
            vertices[2] = vertBottomRight;

            this.GetComponent<MeshFilter>().mesh.vertices = vertices;

            //DestroyImmediate(this);
            startswtch = 0;
        }

	}
}
