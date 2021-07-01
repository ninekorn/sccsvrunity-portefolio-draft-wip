using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trianglesetpivot : MonoBehaviour
{

    Vector3[] vertices;

    void Start ()
    {
        vertices = GetComponent<MeshFilter>().mesh.vertices;



	}
	
	void Update ()
    {
		
	}
}
