using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

public class Shatter1 : MonoBehaviour
{

    public GameObject sphere;
    private List<Vector3> verts = new List<Vector3>();

    private Mesh mesh;
    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private Vector3 center;

    private Vector3[] OriginalVerts;

    private List<GameObject> FracturingObj = new List<GameObject>();

    Vector3[] vertTrigList;

    List<Vector3> vertys = new List<Vector3>();
    List<Vector3> tetraHedron = new List<Vector3>();
    List<Vector3> doubleVertys = new List<Vector3>();

    Vector3[] centering;
    int newTrianglesCount;
    Vector3[] normalz;

    Vector3 p0;
    Vector3 p1;
    Vector3 p2;
    Vector3 p3;

    private Mesh fracMesh;

    Vector3[] newVerts;
    Vector3[] doubleVerts;
    Vector3 singleVert;
    Vector3[] verto;
    Vector3 curVert;

    float radius;
    MeshFilter meshFilt;
    public int explosionForce;
    Vector2[] uvs;


    Vector3 randomPoint0;
    Vector3 randomPoint1;
    Vector3 randomPoint2;
    Vector3 randomPoint3;

    Vector3 randomPoint4;
    Vector3 randomPoint5;
    Vector3 randomPoint6;
    Vector3 randomPoint7;

    Vector3 randomPoint8;
    Vector3 randomPoint9;
    Vector3 randomPoint10;
    Vector3 randomPoint11;

    Vector3 randomPosition0;
    Vector3 randomPosition4;
    Vector3 randomPosition8;
    Vector3 randomPosition12;

    Vector3 randomPosition16;
    Vector3 randomPosition20;
    Vector3 randomPosition24;
    Vector3 randomPosition28;

    Vector3 randomPosition32;
    Vector3 randomPosition36;
    Vector3 randomPosition40;
    Vector3 randomPosition44;

    Vector3 randomPosition48;
    Vector3 randomPosition52;
    Vector3 randomPosition56;
    Vector3 randomPosition60;

    Vector3 randomPosition64;
    Vector3 randomPosition68;


    Vector3 minPosition0;
    Vector3 maxPosition0;

    Vector3 minPosition1;
    Vector3 maxPosition1;

    Vector3 minPosition2;
    Vector3 maxPosition2;

    Vector3 minPosition3;
    Vector3 maxPosition3;

    Vector3 minPosition4;
    Vector3 maxPosition4;

    Vector3 minPosition5;
    Vector3 maxPosition5;

    Vector3 minPosition6;
    Vector3 maxPosition6;

    Vector3 minPosition7;
    Vector3 maxPosition7;

    Vector3 minPosition8;
    Vector3 maxPosition8;

    Vector3 minPosition9;
    Vector3 maxPosition9;

    Vector3 minPosition10;
    Vector3 maxPosition10;

    Vector3 minPosition11;
    Vector3 maxPosition11;

    Vector3 minPosition12;
    Vector3 maxPosition12;

    Vector3 minPosition13;
    Vector3 maxPosition13;

    Vector3 minPosition14;
    Vector3 maxPosition14;

    Vector3 minPosition15;
    Vector3 maxPosition15;

    Vector3 minPosition16;
    Vector3 maxPosition16;

    Vector3 minPosition17;
    Vector3 maxPosition17;





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
                    tetraHedron.Add(newVerts[n]);
                    doubleVertys.Add(newVerts[n]);
                }


            }
        }

        fracMesh = new Mesh();




        for (int i = 0; i < vertys.Count; i++)
        {
            centering = new Vector3[vertys.Count];
        }



        for (int i = 0; i < vertys.Count; i++)
        {
            doubleVerts = new Vector3[vertys.Count * 2];
        }




        for (int i = 0; i < doubleVerts.Length; i++)
        {
            doubleVerts[i] = vertys[i];
            singleVert = doubleVerts[i];
            vertys.Add(singleVert);
            doubleVertys.Add(singleVert);
        }

        radius = Random.Range((float)0f, (float)0.25f);

        randomPoint0 = Random.insideUnitSphere * radius + center;






        /////////////FRONT FACE EDGES //////////////
        /*minPosition0 = verts[0];
        maxPosition0 = verts[1];

        minPosition1 = verts[1];
        maxPosition1 = verts[3];

        minPosition2 = verts[3];
        maxPosition2 = verts[2];

        minPosition3 = verts[2];
        maxPosition3 = verts[0];

        //////////////TOP FACE EDGES//////////////

        minPosition4 = verts[3];
        maxPosition4 = verts[5];

        minPosition5 = verts[2];
        maxPosition5 = verts[4];

        minPosition6 = verts[4];
        maxPosition6 = verts[5];

        //////////////BACK FACE EDGES//////////////

        minPosition7 = verts[4];
        maxPosition7 = verts[6];

        minPosition8 = verts[5];
        maxPosition8 = verts[7];

        minPosition9 = verts[6];
        maxPosition9 = verts[7];

        //////////////BOTTOM FACE EDGES//////////////

        minPosition10 = verts[0];
        maxPosition10 = verts[6];

        minPosition11 = verts[1];
        maxPosition11 = verts[7];

        //////////////FRONT FACES//////////////
        minPosition12 = verts[0];
        maxPosition12 = verts[3];

        //////////////TOP FACES//////////////
        minPosition13 = verts[2];
        maxPosition13 = verts[5];

        //////////////BACK FACES //////////////
        minPosition14 = verts[4];
        maxPosition14 = verts[7];

        //////////////LEFT FACES //////////////
        minPosition15 = verts[2];
        maxPosition15 = verts[6];

        //////////////RIGHT FACES //////////////
        minPosition16 = verts[3];
        maxPosition16 = verts[7];

        //////////////BOTTOM FACES //////////////
        minPosition17 = verts[1];
        maxPosition17 = verts[6];*/

        /*randomPosition0 = new Vector3(Random.Range(minPosition0.x, maxPosition0.x), Random.Range(minPosition0.y, maxPosition0.y), Random.Range(minPosition0.z, maxPosition0.z));
        randomPosition4 = new Vector3(Random.Range(minPosition1.x, maxPosition1.x), Random.Range(minPosition1.y, maxPosition1.y), Random.Range(minPosition1.z, maxPosition1.z));
        randomPosition8 = new Vector3(Random.Range(minPosition2.x, maxPosition2.x), Random.Range(minPosition2.y, maxPosition2.y), Random.Range(minPosition2.z, maxPosition2.z));
        randomPosition12 = new Vector3(Random.Range(minPosition3.x, maxPosition3.x), Random.Range(minPosition3.y, maxPosition3.y), Random.Range(minPosition3.z, maxPosition3.z));
        randomPosition16 = new Vector3(Random.Range(minPosition4.x, maxPosition4.x), Random.Range(minPosition4.y, maxPosition4.y), Random.Range(minPosition4.z, maxPosition4.z));
        randomPosition20 = new Vector3(Random.Range(minPosition5.x, maxPosition5.x), Random.Range(minPosition5.y, maxPosition5.y), Random.Range(minPosition5.z, maxPosition5.z));
        randomPosition24 = new Vector3(Random.Range(minPosition6.x, maxPosition6.x), Random.Range(minPosition6.y, maxPosition6.y), Random.Range(minPosition6.z, maxPosition6.z));
        randomPosition28 = new Vector3(Random.Range(minPosition7.x, maxPosition7.x), Random.Range(minPosition7.y, maxPosition7.y), Random.Range(minPosition7.z, maxPosition7.z));
        randomPosition32 = new Vector3(Random.Range(minPosition8.x, maxPosition8.x), Random.Range(minPosition8.y, maxPosition8.y), Random.Range(minPosition8.z, maxPosition8.z));
        randomPosition36 = new Vector3(Random.Range(minPosition9.x, maxPosition9.x), Random.Range(minPosition9.y, maxPosition9.y), Random.Range(minPosition9.z, maxPosition9.z));
        randomPosition40 = new Vector3(Random.Range(minPosition10.x, maxPosition10.x), Random.Range(minPosition10.y, maxPosition10.y), Random.Range(minPosition10.z, maxPosition10.z));
        randomPosition44 = new Vector3(Random.Range(minPosition11.x, maxPosition11.x), Random.Range(minPosition11.y, maxPosition11.y), Random.Range(minPosition11.z, maxPosition11.z));
        randomPosition48 = new Vector3(Random.Range(minPosition12.x, maxPosition12.x), Random.Range(minPosition12.y, maxPosition12.y), Random.Range(minPosition12.z, maxPosition12.z));
        randomPosition52 = new Vector3(Random.Range(minPosition13.x, maxPosition13.x), Random.Range(minPosition13.y, maxPosition13.y), Random.Range(minPosition13.z, maxPosition13.z));
        randomPosition56 = new Vector3(Random.Range(minPosition14.x, maxPosition14.x), Random.Range(minPosition14.y, maxPosition14.y), Random.Range(minPosition14.z, maxPosition14.z));
        randomPosition60 = new Vector3(Random.Range(minPosition15.x, maxPosition15.x), Random.Range(minPosition15.y, maxPosition15.y), Random.Range(minPosition15.z, maxPosition15.z));
        randomPosition64 = new Vector3(Random.Range(minPosition16.x, maxPosition16.x), Random.Range(minPosition16.y, maxPosition16.y), Random.Range(minPosition16.z, maxPosition16.z));
        randomPosition68 = new Vector3(Random.Range(minPosition17.x, maxPosition17.x), Random.Range(minPosition17.y, maxPosition17.y), Random.Range(minPosition17.z, maxPosition17.z));


        GameObject yoMan0 = (GameObject)Instantiate(sphere, randomPosition0 + transform.position, Quaternion.identity);
        GameObject yoMan4 = (GameObject)Instantiate(sphere, randomPosition4 + transform.position, Quaternion.identity);
        GameObject yoMan8 = (GameObject)Instantiate(sphere, randomPosition8 + transform.position, Quaternion.identity);
        GameObject yoMan12 = (GameObject)Instantiate(sphere, randomPosition12 + transform.position, Quaternion.identity);
        GameObject yoMan16 = (GameObject)Instantiate(sphere, randomPosition16 + transform.position, Quaternion.identity);
        GameObject yoMan20 = (GameObject)Instantiate(sphere, randomPosition20 + transform.position, Quaternion.identity);
        GameObject yoMan24 = (GameObject)Instantiate(sphere, randomPosition24 + transform.position, Quaternion.identity);
        GameObject yoMan28 = (GameObject)Instantiate(sphere, randomPosition28 + transform.position, Quaternion.identity);
        GameObject yoMan32 = (GameObject)Instantiate(sphere, randomPosition32 + transform.position, Quaternion.identity);
        GameObject yoMan36 = (GameObject)Instantiate(sphere, randomPosition36 + transform.position, Quaternion.identity);
        GameObject yoMan40 = (GameObject)Instantiate(sphere, randomPosition40 + transform.position, Quaternion.identity);
        GameObject yoMan44 = (GameObject)Instantiate(sphere, randomPosition44 + transform.position, Quaternion.identity);
        GameObject yoMan48 = (GameObject)Instantiate(sphere, randomPosition48 + transform.position, Quaternion.identity);
        GameObject yoMan52 = (GameObject)Instantiate(sphere, randomPosition52 + transform.position, Quaternion.identity);
        GameObject yoMan56 = (GameObject)Instantiate(sphere, randomPosition56 + transform.position, Quaternion.identity);
        GameObject yoMan60 = (GameObject)Instantiate(sphere, randomPosition60 + transform.position, Quaternion.identity);
        GameObject yoMan64 = (GameObject)Instantiate(sphere, randomPosition64 + transform.position, Quaternion.identity);
        GameObject yoMan68 = (GameObject)Instantiate(sphere, randomPosition68 + transform.position, Quaternion.identity);*/














        for (int j = 0; j < centering.Length; j++)
        {
            centering[j] = randomPoint0;
            vertys.Add(centering[j]);
        }

        newTrianglesCount = vertys.Count / 3;




        mesh.Clear();
        for (int i = 0; i < newTrianglesCount / 4; i++)
        {

            GameObject singleFrac = new GameObject();

            FracturingObj.Add(singleFrac);

            p0 = vertys[i * 3 + 0];
            p1 = vertys[i * 3 + 1];
            p2 = vertys[i * 3 + 2];
            p3 = vertys[i * 3 + doubleVertys.Count];

            vertTrigList = new Vector3[]
             {
                    p0,p1,p2,p3,
             };
            //Debug.Log(vertTrigList.Length);



            ///////////////////////////////////////////////////////////
            FracturingObj[i].transform.position = transform.position;
            FracturingObj[i].transform.rotation = transform.rotation;
            FracturingObj[i].transform.localScale = transform.localScale;

            if (FracturingObj[i].transform.gameObject.GetComponent<MeshRenderer>() == null)
            {
                FracturingObj[i].transform.gameObject.AddComponent<MeshRenderer>();
            }

            if (FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>() == null)
            {
                meshFilt = FracturingObj[i].transform.gameObject.AddComponent<MeshFilter>();
            }

            fracMesh = FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>().sharedMesh;
            fracMesh = new Mesh();

            FracturingObj[i].transform.name = "Fragment" + " " + i;
            fracMesh.name = "Fragment" + " " + i;
            FracturingObj[i].transform.tag = "Fragment";
            //////////////////////////////////////////////////////////////

            fracMesh.vertices = vertTrigList;
            normalz = new Vector3[vertTrigList.Length];

            /*uvs = new Vector2[fracMesh.vertices.Length];

            for (int j = 0; j < uvs.Length; j++)
            {
                uvs[j] = new Vector2(fracMesh.vertices[j].x, fracMesh.vertices[j].z);
            }*/































            Material[] m = new Material[]
            {
                    Resources.Load("Material/TerrainColor") as Material,
                    Resources.Load("Material/TerrainColor1") as Material,
                    Resources.Load("Material/TerrainColor2") as Material,
                    Resources.Load("Material/TerrainColor3") as Material,
            };


            meshFilt.mesh = fracMesh;
            FracturingObj[i].GetComponent<Renderer>().materials = m;

            fracMesh.subMeshCount = 4;

            int[] a1 = new int[] { 0, 1, 2 };
            fracMesh.SetTriangles(a1, 0);

            int[] a2 = new int[] { 3, 1, 0 };
            fracMesh.SetTriangles(a2, 1);

            int[] a3 = new int[] { 3, 2, 1 };
            fracMesh.SetTriangles(a3, 2);

            int[] a4 = new int[] { 0, 2, 3 };
            fracMesh.SetTriangles(a4, 3);

            fracMesh.normals = normalz;
            fracMesh.uv = uvs;

            ;
            fracMesh.RecalculateBounds();
            fracMesh.RecalculateNormals();

















            /*if (FracturingObj[i].transform.gameObject.GetComponent<MeshCollider>() == null)
            {
                MeshCollider meshCol = FracturingObj[i].gameObject.AddComponent<MeshCollider>();
                meshCol.convex = true;
            }

            if (FracturingObj[i].transform.gameObject.GetComponent<Rigidbody>() == null)
            {
                FracturingObj[i].gameObject.AddComponent<Rigidbody>().AddExplosionForce(explosionForce, FracturingObj[i].transform.position, 10000f, 100000f);
            }

            if (gameObject.GetComponent<MeshCollider>() != null)
            {
                Destroy(gameObject.GetComponent<MeshCollider>(), 0.1f);
            }
            if (gameObject.GetComponent<BoxCollider>() != null)
            {
                Destroy(gameObject.GetComponent<BoxCollider>(), 0.1f);
            }
            if (gameObject.GetComponent<SphereCollider>() != null)
            {
                Destroy(gameObject.GetComponent<SphereCollider>(), 0.1f);
            }*/
        }
    }
}
