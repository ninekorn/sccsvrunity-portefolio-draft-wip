using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

public class Shatter : MonoBehaviour
{
    public GameObject sphere;

    private Mesh mesh;
    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private Vector3 center;

    private Vector3[] OriginalVerts;

    List<Vector3> vertys = new List<Vector3>();
    List<Vector3> tryoutVerts = new List<Vector3>();
    List<Vector3> doubleVertys = new List<Vector3>();
    List<Vector3> originalVerts = new List<Vector3>();
    List<List<Vector3>> vertyz;
    List<List<Vector3>> tempList;



    private List<Vector3> verts = new List<Vector3>();
    private List<GameObject> FracturingObj = new List<GameObject>();

    int t0;
    int t1;
    int t2;
    int t3;

    private Mesh fracMesh;

    Vector3[] newVerts;

    MeshFilter meshFilt;

    List<int> trize = new List<int>();

    List<Vector3> verti;

    int[] trisToSet;
    public int randomFrac;
    float radius;
    Vector3[] centering;

    List<Vector3> centerz = new List<Vector3>();
    Vector3[] doubleVerts;
    Vector3 singleVert;

    void Start()
    {
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
                    tryoutVerts.Add(newVerts[n]);
                    doubleVertys.Add(newVerts[n]);
                    originalVerts.Add(newVerts[n]);

                }
            }
        }




        /////////HERE IS THE NUMBER OF FRACTURED OBJECTS TO INSTANTIATE//////////
        for (int k = 0; k < randomFrac; k++)
        {
            fracMesh = new Mesh();
            GameObject singleFrac = new GameObject();
            FracturingObj.Add(singleFrac);

            FracturingObj[k].transform.position = transform.position;
            FracturingObj[k].transform.rotation = transform.rotation;
            FracturingObj[k].transform.localScale = transform.localScale;

            if (FracturingObj[k].transform.gameObject.GetComponent<MeshRenderer>() == null)
            {
                FracturingObj[k].transform.gameObject.AddComponent<MeshRenderer>();
            }

            if (FracturingObj[k].transform.gameObject.GetComponent<MeshFilter>() == null)
            {
                meshFilt = FracturingObj[k].transform.gameObject.AddComponent<MeshFilter>();
            }

            FracturingObj[k].transform.name = "Fragment" + " " + k;
            fracMesh.name = "Fragment" + " " + k;
            FracturingObj[k].transform.tag = "Fragment";

            mesh.Clear();

            fracMesh = FracturingObj[k].transform.gameObject.GetComponent<MeshFilter>().mesh;
        }





        

        vertyz = new List<List<Vector3>>();
        vertyz.Capacity = randomFrac;



        for (int i = 0; i < vertys.Count; i++)
        {
            doubleVerts = new Vector3[vertys.Count * 2];
        }



        for (int i = 0; i < doubleVerts.Length; i++)
        {
            doubleVerts[i] = vertys[i];
            vertys.Add(doubleVerts[i]);
            doubleVertys.Add(doubleVerts[i]);
            tryoutVerts.Add(doubleVerts[i]);
            //GameObject yoMan1 = (GameObject)Instantiate(sphere, doubleVerts[i] + transform.position, Quaternion.identity);
        }







        for (int i = 0; i < vertyz.Capacity; i++)
        {
            verti = new List<Vector3>();
            vertyz.Add(verti);
        }




        radius = Random.Range((float)0f, (float)0.25f);
        Vector3 randomPoint = Random.insideUnitSphere * radius + center;

        centering = new Vector3[originalVerts.Count];

        for (int j = 0; j < centering.Length; j++)
        {
            centering[j] = randomPoint;
            //tryoutVerts.Add(centering[j]);
            //vertys.Add(centering[j]);
        }




        //int numOfFractions = tryoutVerts.Count / randomFrac;
        //int numOfFractions = vertys.Count / randomFrac;
        //Debug.Log(tryoutVerts.Count);

        for (int i = 0; i < vertyz.Count; i++)
        {
            vertyz[i] = vertys.GetRange(i, vertys.Count / randomFrac);




            for (int j = 0; j < centering.Length / randomFrac; j++)
            {
                vertyz[i].Insert(vertys.Count/randomFrac, centering[j]);
            }

            FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices = vertyz[i].ToArray();


            for (int u = 0; u < vertyz[i].Count; u++)
            {
                GameObject yoMan1 = (GameObject)Instantiate(sphere, vertyz[i][u] + transform.position, Quaternion.identity);
            }

                




        }







        /*for (int i = 0; i < FracturingObj.Count; i++)
        {
            
            for (int j = 0; j < FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices.Length; j++)
            {
                GameObject yoMan1 = (GameObject)Instantiate(sphere, FracturingObj[0].GetComponent<MeshFilter>().mesh.vertices[j] + transform.position, Quaternion.identity);
                //GameObject yoMan1 = (GameObject)Instantiate(sphere, FracturingObj[0].GetComponent<MeshFilter>().mesh.vertices[j] + transform.position, Quaternion.identity);
            }

            
        }*/

        
















        /*GameObject yoMan1 = (GameObject)Instantiate(sphere, vertys[1] + transform.position, Quaternion.identity);
        GameObject yoMan2 = (GameObject)Instantiate(sphere, vertys[2] + transform.position, Quaternion.identity);
        GameObject yoMan3 = (GameObject)Instantiate(sphere, vertys[3] + transform.position, Quaternion.identity);
        GameObject yoMan4 = (GameObject)Instantiate(sphere, vertys[4] + transform.position, Quaternion.identity);
        GameObject yoMan5 = (GameObject)Instantiate(sphere, vertys[5] + transform.position, Quaternion.identity);
        GameObject yoMan6 = (GameObject)Instantiate(sphere, vertys[6] + transform.position, Quaternion.identity);*/
        /*GameObject yoMan7 = (GameObject)Instantiate(sphere, vertys[7] + transform.position, Quaternion.identity);
        GameObject yoMan8 = (GameObject)Instantiate(sphere, vertys[8] + transform.position, Quaternion.identity);
        GameObject yoMan9 = (GameObject)Instantiate(sphere, vertys[9] + transform.position, Quaternion.identity);
        GameObject yoMan10 = (GameObject)Instantiate(sphere, vertys[10] + transform.position, Quaternion.identity);
        GameObject yoMan11 = (GameObject)Instantiate(sphere, vertys[11] + transform.position, Quaternion.identity);
        GameObject yoMan12 = (GameObject)Instantiate(sphere, vertys[12] + transform.position, Quaternion.identity);*/



















        //Debug.Log(numOfFractions);

        /*for (int i = 0; i < vertyz.Count; i++)
        {
            vertyz[i] = vertys.GetRange(i * numOfFractions, vertys.Count / randomFrac);

            vertyz[i].Insert(0, centering[j]);

            //Debug.Log(vertyz[i].Count);
            FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices = vertyz[i].ToArray();
        }*/













        //

        /*for (int i = 0; i < vertyz.Count; i++)
        {
            for (int j = 0; j < centering.Length; j++)
            {

            }
            Debug.Log(vertyz[i].Count);
        }*/






        /*p0 = vertys[i * 3 + 0];
        p1 = vertys[i * 3 + 1];
        p2 = vertys[i * 3 + 2];
        p3 = vertys[i * 3 + doubleVertys.Count];*/



            /*for (int i = 0; i < vertys.Count / 3; i++)
            {
                int[] trisToSet = new int[vertys.Count];

                for (int u = 0; u < trisToSet.Length; u++)
                {
                    t0 = i * 3 + 0;
                    t1 = i * 3 + 1;
                    t2 = i * 3 + 2;
                    //t3 = i * 3 ;
                }

                trize.Add(t0);
                trize.Add(t1);
                trize.Add(t2);
                //trize.Add(t3);
            }*/



            /*for (int i = 0; i < FracturingObj.Count; i++)
            {
                for (int j = 0; j < FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices.Length / 3; j++)
                {
                    trisToSet = new int[FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices.Length / 3];

                    for (int k = 0; k < trisToSet.Length; k++)
                    {
                        t0 = j * 3 + 0;
                        t1 = j * 3 + 1;
                        t2 = j * 3 + 2;
                        t3 = j *3 +3;
                    }
                    trize.Add(t0);
                    trize.Add(t1);
                    trize.Add(t2);
                    trize.Add(t3);
                }
            }*/


            /*for (int i = 0; i < FracturingObj.Count; i++)
            {
                int[] trisToSet = new int[FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices.Length/3];

                for (int u = 0; u < trisToSet.Length; u++)
                {
                    t0 = i * 3 + 0;
                    t1 = i * 3 + 1;
                    t2 = i * 3 + 2;

                }

                trize.Add(t0);
                trize.Add(t1);
                trize.Add(t2);
            }*/




            //FracturingObj[0].GetComponent<MeshFilter>().mesh.triangles = trigze;

            for (int i = 0; i < FracturingObj.Count; i++)
        {

            //FracturingObj[i].GetComponent<MeshFilter>().mesh.triangles = trize.ToArray();
            //FracturingObj[i].GetComponent<MeshFilter>().mesh.triangles = trigze;


            Debug.Log(FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices.Length);

            for (int j = 0; j < FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices.Length; j++)
            {
                //GameObject yoMan0 = (GameObject)Instantiate(sphere, FracturingObj[0].GetComponent<MeshFilter>().mesh.vertices[j] + transform.position, Quaternion.identity);
                //GameObject yoMan0 = (GameObject)Instantiate(sphere, FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices[j] + transform.position, Quaternion.identity);
            }
            var o_399_13_636211226962133390 = FracturingObj[i].GetComponent<MeshFilter>().mesh;
            FracturingObj[i].GetComponent<MeshFilter>().mesh.RecalculateBounds();
            FracturingObj[i].GetComponent<MeshFilter>().mesh.RecalculateNormals();
        }


    }
}
