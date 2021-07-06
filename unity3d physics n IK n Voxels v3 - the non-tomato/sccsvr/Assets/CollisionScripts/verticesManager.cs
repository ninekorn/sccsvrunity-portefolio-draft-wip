using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticesManager : MonoBehaviour
{
    public class arrayOfVerts
    {
        public Vector3[] verticesList;
        public int[] triangleList;
        public string nameOfObject;

        public arrayOfVerts(Vector3[] vertList,string name,int[] trigList)
        {
            this.verticesList = vertList;
            this.nameOfObject = name;
            this.triangleList = trigList;
        }
    }

    public GameObject[] objecter;
    arrayOfVerts vertArray;

    public static arrayOfVerts[] arrayOfObjectsData;

    void Start()
    {
        arrayOfObjectsData = new arrayOfVerts[objecter.Length];
        for (int i = 0; i < objecter.Length; i++)
        {
            Vector3[] vertices = objecter[i].GetComponent<MeshFilter>().mesh.vertices;
            int[] trigs = objecter[i].GetComponent<MeshFilter>().mesh.triangles;
            vertArray = new arrayOfVerts(vertices, objecter[i].transform.name,trigs);
            arrayOfObjectsData[i] = vertArray;
        }
    }
}
