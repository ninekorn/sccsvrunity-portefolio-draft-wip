using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class reassemble : MonoBehaviour
{

    public static reassemble currentReassemble;


    private Rigidbody rb;
    Mesh mesh;
    MeshCollider meshCollider;

    public List<Vector3> vertys = new List<Vector3>();
    public List<Vector3> norms = new List<Vector3>();
    public List<int> tris = new List<int>();
    public List<Vector3> verty = new List<Vector3>();
    public List<GameObject> objToReact = new List<GameObject>();

    public List<Vector3> vertany = new List<Vector3>();
    public List<Vector3> vertanus = new List<Vector3>();

    int tri;
    Vector3 norm;
    Vector3 vert;

    Vector3[] verts;
    int[] triangles;
    Vector3[] normals;
    Vector2[] uvs;

    public int childObjCounter = 0;
    public int counter = 0;
    int vertAmount;
    Vector3[] newVerts;
    Vector3[] OriginalVerts;

    int newTrianglesCount;
    private Mesh fracMesh;
    int t0;
    int t1;
    int t2;
    MeshFilter meshFilt;

    List<int> trize = new List<int>();
    Matrix4x4 thisMatrix;
    void Start()
    {
        //currentReassemble = this;

    }

    void Update()
    {
        //Debug.Log(vertys.Count);

        //Debug.Log(objToReact.Count);

        if (counter == 1)
        {




            //transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            mesh = GetComponent<MeshFilter>().mesh;

            mesh.Clear();
            mesh = new Mesh();





            mesh.subMeshCount = objToReact.Count;

            // var thisMatrix = transform.worldToLocalMatrix;


            /*Matrix4x4 myTransform = transform.worldToLocalMatrix;
            
            for (int i = 0;i <objToReact.Count; i++)
            {

                thisMatrix = objToReact[i].transform.worldToLocalMatrix;

            }

            for (int j = 0; j < vertys.Count; j++)
            {
                Vector3 vertex = vertys[j];
                Vector3 yo = myTransform*thisMatrix.MultiplyPoint3x4(vertex);

                verty.Add(yo);
                mesh.vertices = verty.ToArray();
            }*/
            var thisMatrix = transform.worldToLocalMatrix;

            for (int j = 0; j < vertys.Count; j++)
            {
                Vector3 vertex = vertys[j];
                Vector3 yo = thisMatrix.MultiplyPoint3x4(vertex);

                verty.Add(yo);
                mesh.vertices = verty.ToArray();
            }








            //mesh.vertices = vertys.ToArray();





            for (int u = 0; u < objToReact.Count; u++)
            {
                GameObject yo = objToReact[u];
                yo.SetActive(false);
            }



            if (mesh.subMeshCount == 1)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;

                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                                0,1,2,
                                3,1,0,
                                3,2,1,
                                0,2,3,
                    };
                    //Debug.Log("OSTI0");
                    mesh.SetTriangles(a0, 0);
                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();

            }


            if (mesh.subMeshCount == 2)
            {

                //Debug.Log("OSTI1");
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {

                    int[] a0 = new int[]
                    {
                                0,1,2,
                                3,1,0,
                                3,2,1,
                                0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                                4,5,6,
                                7,5,4,
                                7,6,5,
                                4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);
                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }

            if (mesh.subMeshCount == 3)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                // mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }
            if (mesh.subMeshCount == 4)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }



            if (mesh.subMeshCount == 5)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }










            if (mesh.subMeshCount == 6)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }







            if (mesh.subMeshCount == 7)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);
                }





                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }






            if (mesh.subMeshCount == 8)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);

                    int[] a7 = new int[]
                    {
                            28,29,30,
                            31,29,28,
                            31,30,29,
                            28,30,31,

                    };
                    mesh.SetTriangles(a7, 7);
                }





                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                // mesh.RecalculateNormals();
            }





            if (mesh.subMeshCount == 9)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);

                    int[] a7 = new int[]
                    {
                            28,29,30,
                            31,29,28,
                            31,30,29,
                            28,30,31,

                    };
                    mesh.SetTriangles(a7, 7);

                    int[] a8 = new int[]
                    {
                            32,33,34,
                            35,33,32,
                            35,34,33,
                            32,34,35,

                    };
                    mesh.SetTriangles(a8, 8);
                }





                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }



            if (mesh.subMeshCount == 10)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);

                    int[] a7 = new int[]
                    {
                            28,29,30,
                            31,29,28,
                            31,30,29,
                            28,30,31,

                    };
                    mesh.SetTriangles(a7, 7);

                    int[] a8 = new int[]
                    {
                            32,33,34,
                            35,33,32,
                            35,34,33,
                            32,34,35,

                    };
                    mesh.SetTriangles(a8, 8);

                    int[] a9 = new int[]
                    {
                            36,37,38,
                            39,37,36,
                            39,38,37,
                            36,38,39,

                    };
                    mesh.SetTriangles(a9, 9);
                }





                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }




            if (mesh.subMeshCount == 11)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);

                    int[] a7 = new int[]
                    {
                            28,29,30,
                            31,29,28,
                            31,30,29,
                            28,30,31,

                    };
                    mesh.SetTriangles(a7, 7);

                    int[] a8 = new int[]
                    {
                            32,33,34,
                            35,33,32,
                            35,34,33,
                            32,34,35,

                    };
                    mesh.SetTriangles(a8, 8);

                    int[] a9 = new int[]
                    {
                            36,37,38,
                            39,37,36,
                            39,38,37,
                            36,38,39,

                    };
                    mesh.SetTriangles(a9, 9);

                    int[] a10 = new int[]
                    {
                            40,41,42,
                            43,41,40,
                            43,42,41,
                            40,42,43,

                    };
                    mesh.SetTriangles(a10, 10);
                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }

            if (mesh.subMeshCount == 12)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;

                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);

                    int[] a7 = new int[]
                    {
                            28,29,30,
                            31,29,28,
                            31,30,29,
                            28,30,31,

                    };
                    mesh.SetTriangles(a7, 7);

                    int[] a8 = new int[]
                    {
                            32,33,34,
                            35,33,32,
                            35,34,33,
                            32,34,35,

                    };
                    mesh.SetTriangles(a8, 8);

                    int[] a9 = new int[]
                    {
                            36,37,38,
                            39,37,36,
                            39,38,37,
                            36,38,39,

                    };
                    mesh.SetTriangles(a9, 9);

                    int[] a10 = new int[]
                    {
                            40,41,42,
                            43,41,40,
                            43,42,41,
                            40,42,43,

                    };
                    mesh.SetTriangles(a10, 10);

                    int[] a11 = new int[]
                    {
                            44,45,46,
                            47,45,44,
                            47,46,45,
                            44,46,47,

                    };
                    mesh.SetTriangles(a11, 11);
                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;

                // mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }


           for (int i = 0; i < mesh.vertices.Length; i++)
           {
               Debug.DrawRay(mesh.vertices[i] + transform.position, Vector3.up * 5f, Color.red, 1f);
           }


            verty.Clear();
            counter = 0;
           
        }
        /*for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Debug.DrawRay(mesh.vertices[i] + transform.position, Vector3.up * 5f, Color.red, 0.1f);
        }*/
        //Debug.Log(vertys.Count);





















        if (counter == 2)
        {




            //transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            mesh = GetComponent<MeshFilter>().mesh;

            mesh.Clear();
            mesh = new Mesh();





            mesh.subMeshCount = objToReact.Count;

            // var thisMatrix = transform.worldToLocalMatrix;
            var thisMatrix = transform.worldToLocalMatrix;

            //Matrix4x4 myTransform = transform.localToWorldMatrix;

            for (int j = 0; j < vertys.Count; j++)
            {
                Vector3 vertex = vertys[j];
                Vector3 yo = thisMatrix.MultiplyPoint3x4(vertex);

                verty.Add(yo);
                //mesh.vertices = verty.ToArray();
            }







            mesh.vertices = vertys.ToArray();





            for (int u = 0; u < objToReact.Count; u++)
            {
                GameObject yo = objToReact[u];
                yo.SetActive(false);
            }



            if (mesh.subMeshCount == 1)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;

                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                                0,1,2,
                                3,1,0,
                                3,2,1,
                                0,2,3,
                    };
                    //Debug.Log("OSTI0");
                    mesh.SetTriangles(a0, 0);
                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();

            }


            if (mesh.subMeshCount == 2)
            {

                //Debug.Log("OSTI1");
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {

                    int[] a0 = new int[]
                    {
                                0,1,2,
                                3,1,0,
                                3,2,1,
                                0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                                4,5,6,
                                7,5,4,
                                7,6,5,
                                4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);
                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }

            if (mesh.subMeshCount == 3)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                // mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }
            if (mesh.subMeshCount == 4)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }



            if (mesh.subMeshCount == 5)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }










            if (mesh.subMeshCount == 6)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }







            if (mesh.subMeshCount == 7)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);
                }





                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }






            if (mesh.subMeshCount == 8)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);

                    int[] a7 = new int[]
                    {
                            28,29,30,
                            31,29,28,
                            31,30,29,
                            28,30,31,

                    };
                    mesh.SetTriangles(a7, 7);
                }





                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                // mesh.RecalculateNormals();
            }





            if (mesh.subMeshCount == 9)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);

                    int[] a7 = new int[]
                    {
                            28,29,30,
                            31,29,28,
                            31,30,29,
                            28,30,31,

                    };
                    mesh.SetTriangles(a7, 7);

                    int[] a8 = new int[]
                    {
                            32,33,34,
                            35,33,32,
                            35,34,33,
                            32,34,35,

                    };
                    mesh.SetTriangles(a8, 8);
                }





                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }



            if (mesh.subMeshCount == 10)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);

                    int[] a7 = new int[]
                    {
                            28,29,30,
                            31,29,28,
                            31,30,29,
                            28,30,31,

                    };
                    mesh.SetTriangles(a7, 7);

                    int[] a8 = new int[]
                    {
                            32,33,34,
                            35,33,32,
                            35,34,33,
                            32,34,35,

                    };
                    mesh.SetTriangles(a8, 8);

                    int[] a9 = new int[]
                    {
                            36,37,38,
                            39,37,36,
                            39,38,37,
                            36,38,39,

                    };
                    mesh.SetTriangles(a9, 9);
                }





                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }




            if (mesh.subMeshCount == 11)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);

                    int[] a7 = new int[]
                    {
                            28,29,30,
                            31,29,28,
                            31,30,29,
                            28,30,31,

                    };
                    mesh.SetTriangles(a7, 7);

                    int[] a8 = new int[]
                    {
                            32,33,34,
                            35,33,32,
                            35,34,33,
                            32,34,35,

                    };
                    mesh.SetTriangles(a8, 8);

                    int[] a9 = new int[]
                    {
                            36,37,38,
                            39,37,36,
                            39,38,37,
                            36,38,39,

                    };
                    mesh.SetTriangles(a9, 9);

                    int[] a10 = new int[]
                    {
                            40,41,42,
                            43,41,40,
                            43,42,41,
                            40,42,43,

                    };
                    mesh.SetTriangles(a10, 10);
                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;
                //mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }

            if (mesh.subMeshCount == 12)
            {
                transform.gameObject.GetComponent<MeshFilter>().mesh = mesh;

                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    int[] a0 = new int[]
                    {
                            0,1,2,
                            3,1,0,
                            3,2,1,
                            0,2,3,

                    };
                    mesh.SetTriangles(a0, 0);

                    int[] a1 = new int[]
                    {
                            4,5,6,
                            7,5,4,
                            7,6,5,
                            4,6,7,

                    };
                    mesh.SetTriangles(a1, 1);

                    int[] a2 = new int[]
                    {
                            8,9,10,
                            11,9,8,
                            11,10,9,
                            8,10,11,

                    };
                    mesh.SetTriangles(a2, 2);

                    int[] a3 = new int[]
                    {
                            12,13,14,
                            15,13,12,
                            15,14,13,
                            12,14,15,

                    };
                    mesh.SetTriangles(a3, 3);

                    int[] a4 = new int[]
                    {
                            16,17,18,
                            19,17,16,
                            19,18,17,
                            16,18,19,

                    };
                    mesh.SetTriangles(a4, 4);

                    int[] a5 = new int[]
                    {
                            20,21,22,
                            23,21,20,
                            23,22,21,
                            20,22,23,

                    };
                    mesh.SetTriangles(a5, 5);

                    int[] a6 = new int[]
                    {
                            24,25,26,
                            27,25,24,
                            27,26,25,
                            24,26,27,

                    };
                    mesh.SetTriangles(a6, 6);

                    int[] a7 = new int[]
                    {
                            28,29,30,
                            31,29,28,
                            31,30,29,
                            28,30,31,

                    };
                    mesh.SetTriangles(a7, 7);

                    int[] a8 = new int[]
                    {
                            32,33,34,
                            35,33,32,
                            35,34,33,
                            32,34,35,

                    };
                    mesh.SetTriangles(a8, 8);

                    int[] a9 = new int[]
                    {
                            36,37,38,
                            39,37,36,
                            39,38,37,
                            36,38,39,

                    };
                    mesh.SetTriangles(a9, 9);

                    int[] a10 = new int[]
                    {
                            40,41,42,
                            43,41,40,
                            43,42,41,
                            40,42,43,

                    };
                    mesh.SetTriangles(a10, 10);

                    int[] a11 = new int[]
                    {
                            44,45,46,
                            47,45,44,
                            47,46,45,
                            44,46,47,

                    };
                    mesh.SetTriangles(a11, 11);
                }
                //mesh.normals = norms.ToArray();
                transform.gameObject.AddComponent<Recombiner>().enabled = true;

                // mesh.RecalculateBounds();
                //mesh.RecalculateNormals();
            }


            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                Debug.DrawRay(mesh.vertices[i] + transform.position, Vector3.up * 5f, Color.red, 1f);
            }


            verty.Clear();
            counter = 0;

        }
    }



}
