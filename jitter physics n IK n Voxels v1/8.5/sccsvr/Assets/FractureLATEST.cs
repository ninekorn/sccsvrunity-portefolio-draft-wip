using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FractureLATEST : MonoBehaviour
{

    private Mesh mesh;
    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private Vector3 center;

    private Vector3 randomPoint;
    private Vector3[] randomPoints;

    private Vector3 point0;

    private List<Vector3> verts = new List<Vector3>();


    private Vector3[] OriginalVerts;

    public GameObject sphere;

    public Material texture;

    private List<GameObject> FracturingObj = new List<GameObject>();

    private Mesh fracMesh;

    public int explosionForce;
    int radius;

    void Start()
    {
        gameObject.GetComponent<MeshRenderer>();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        vertices = gameObject.GetComponent<MeshFilter>().mesh.vertices;
        center = gameObject.GetComponent<MeshFilter>().mesh.bounds.center;
        OriginalVerts = mesh.vertices;

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector3 vertis = mesh.vertices[i];
            verts.Add(vertis);


        }


        //////////////FRONT FACE EDGES //24-25-26-27
        Vector3 minPosition0 = mesh.vertices[0];
        Vector3 maxPosition0 = mesh.vertices[1];
        var randomPosition0 = new Vector3(Random.Range(minPosition0.x, maxPosition0.x), Random.Range(minPosition0.y, maxPosition0.y), Random.Range(minPosition0.z, maxPosition0.z));

        Vector3 minPosition1 = mesh.vertices[1];
        Vector3 maxPosition1 = mesh.vertices[3];
        var randomPosition1 = new Vector3(Random.Range(minPosition1.x, maxPosition1.x), Random.Range(minPosition1.y, maxPosition1.y), Random.Range(minPosition1.z, maxPosition1.z));

        Vector3 minPosition2 = mesh.vertices[3];
        Vector3 maxPosition2 = mesh.vertices[2];
        var randomPosition2 = new Vector3(Random.Range(minPosition2.x, maxPosition2.x), Random.Range(minPosition2.y, maxPosition2.y), Random.Range(minPosition2.z, maxPosition2.z));

        Vector3 minPosition3 = mesh.vertices[2];
        Vector3 maxPosition3 = mesh.vertices[0];
        var randomPosition3 = new Vector3(Random.Range(minPosition3.x, maxPosition3.x), Random.Range(minPosition3.y, maxPosition3.y), Random.Range(minPosition3.z, maxPosition3.z));


        //////////////TOP FACE EDGES//28-29-30

        Vector3 minPosition4 = mesh.vertices[3];
        Vector3 maxPosition4 = mesh.vertices[5];
        var randomPosition4 = new Vector3(Random.Range(minPosition4.x, maxPosition4.x), Random.Range(minPosition4.y, maxPosition4.y), Random.Range(minPosition4.z, maxPosition4.z));

        Vector3 minPosition5 = mesh.vertices[2];
        Vector3 maxPosition5 = mesh.vertices[4];
        var randomPosition5 = new Vector3(Random.Range(minPosition5.x, maxPosition5.x), Random.Range(minPosition5.y, maxPosition5.y), Random.Range(minPosition5.z, maxPosition5.z));

        Vector3 minPosition6 = mesh.vertices[4];
        Vector3 maxPosition6 = mesh.vertices[5];
        var randomPosition6 = new Vector3(Random.Range(minPosition6.x, maxPosition6.x), Random.Range(minPosition6.y, maxPosition6.y), Random.Range(minPosition6.z, maxPosition6.z));

        //////////////BACK FACE EDGES//31-32-33

        Vector3 minPosition7 = mesh.vertices[4];
        Vector3 maxPosition7 = mesh.vertices[6];
        var randomPosition7 = new Vector3(Random.Range(minPosition7.x, maxPosition7.x), Random.Range(minPosition7.y, maxPosition7.y), Random.Range(minPosition7.z, maxPosition7.z));

        Vector3 minPosition8 = mesh.vertices[5];
        Vector3 maxPosition8 = mesh.vertices[7];
        var randomPosition8 = new Vector3(Random.Range(minPosition8.x, maxPosition8.x), Random.Range(minPosition8.y, maxPosition8.y), Random.Range(minPosition8.z, maxPosition8.z));

        Vector3 minPosition9 = mesh.vertices[6];
        Vector3 maxPosition9 = mesh.vertices[7];
        var randomPosition9 = new Vector3(Random.Range(minPosition9.x, maxPosition9.x), Random.Range(minPosition9.y, maxPosition9.y), Random.Range(minPosition9.z, maxPosition9.z));

        //////////////BOTTOM FACE EDGES//34-35

        Vector3 minPosition10 = mesh.vertices[0];
        Vector3 maxPosition10 = mesh.vertices[6];
        var randomPosition10 = new Vector3(Random.Range(minPosition10.x, maxPosition10.x), Random.Range(minPosition10.y, maxPosition10.y), Random.Range(minPosition10.z, maxPosition10.z));

        Vector3 minPosition11 = mesh.vertices[1];
        Vector3 maxPosition11 = mesh.vertices[7];
        var randomPosition11 = new Vector3(Random.Range(minPosition11.x, maxPosition11.x), Random.Range(minPosition11.y, maxPosition11.y), Random.Range(minPosition11.z, maxPosition11.z));


        //////////////FRONT FACES//36//
        Vector3 minPosition12 = mesh.vertices[0];
        Vector3 maxPosition12 = mesh.vertices[3];
        var randomPosition12 = new Vector3(Random.Range(minPosition12.x, maxPosition12.x), Random.Range(minPosition12.y, maxPosition12.y), Random.Range(minPosition12.z, maxPosition12.z));


        //////////////top FACES//37
        Vector3 minPosition13 = mesh.vertices[2];
        Vector3 maxPosition13 = mesh.vertices[5];
        var randomPosition13 = new Vector3(Random.Range(minPosition13.x, maxPosition13.x), Random.Range(minPosition13.y, maxPosition13.y), Random.Range(minPosition13.z, maxPosition13.z));

        //////////////BACK FACES //38
        Vector3 minPosition14 = mesh.vertices[4];
        Vector3 maxPosition14 = mesh.vertices[7];
        var randomPosition14 = new Vector3(Random.Range(minPosition14.x, maxPosition14.x), Random.Range(minPosition14.y, maxPosition14.y), Random.Range(minPosition14.z, maxPosition14.z));

        //////////////LEFT FACES //39
        Vector3 minPosition15 = mesh.vertices[2];
        Vector3 maxPosition15 = mesh.vertices[6];
        var randomPosition15 = new Vector3(Random.Range(minPosition15.x, maxPosition15.x), Random.Range(minPosition15.y, maxPosition15.y), Random.Range(minPosition15.z, maxPosition15.z));


        //////////////RIGHT FACES //40
        Vector3 minPosition16 = mesh.vertices[3];
        Vector3 maxPosition16 = mesh.vertices[7];
        var randomPosition16 = new Vector3(Random.Range(minPosition16.x, maxPosition16.x), Random.Range(minPosition16.y, maxPosition16.y), Random.Range(minPosition16.z, maxPosition16.z));

        //////////////BOTTOM FACES //41
        Vector3 minPosition17 = mesh.vertices[1];
        Vector3 maxPosition17 = mesh.vertices[6];
        var randomPosition17 = new Vector3(Random.Range(minPosition17.x, maxPosition17.x), Random.Range(minPosition17.y, maxPosition17.y), Random.Range(minPosition17.z, maxPosition17.z));



        verts.Add(randomPosition0);
        verts.Add(randomPosition1);
        verts.Add(randomPosition2);
        verts.Add(randomPosition3);

        verts.Add(randomPosition4);
        verts.Add(randomPosition5);
        verts.Add(randomPosition6);
        verts.Add(randomPosition7);

        verts.Add(randomPosition8);
        verts.Add(randomPosition9);
        verts.Add(randomPosition10);
        verts.Add(randomPosition11);

        verts.Add(randomPosition12);
        verts.Add(randomPosition13);
        verts.Add(randomPosition14);

        verts.Add(randomPosition15);
        verts.Add(randomPosition16);
        verts.Add(randomPosition17);

        radius = (int)Random.Range((float)0f, (float)0.5f);

        randomPoint = Random.insideUnitSphere * radius + center;
        randomPoints = new Vector3[]
        {
                point0 = randomPoint,

        };
        verts.Add(point0);




        mesh.triangles = null;
        mesh.vertices = null;
        mesh.Clear();
        DestroyImmediate(gameObject.GetComponent<BoxCollider>());
        mesh.vertices = verts.ToArray();


        for (int i = 0; i < OriginalVerts.Length; i++)
        {
            GameObject singleFrac = new GameObject();


            FracturingObj.Add(singleFrac);

            for (int j = 0; j < FracturingObj.Count; j++)
            {

                FracturingObj[j].transform.position = transform.position;
                FracturingObj[j].transform.rotation = transform.rotation;
                FracturingObj[j].transform.localScale = transform.localScale;


            }

        }

        for (int i = 0; i < FracturingObj.Count; i++)
        {
            if (FracturingObj[i].transform.gameObject.GetComponent<MeshRenderer>() == null)
            {
                FracturingObj[i].transform.gameObject.AddComponent<MeshRenderer>();
            }

            if (FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>() == null)
            {
                MeshFilter meshFilt = FracturingObj[i].transform.gameObject.AddComponent<MeshFilter>();
            }

            fracMesh = FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>().mesh;
            fracMesh = new Mesh();
            fracMesh.vertices = null;
            fracMesh.triangles = null;
            FracturingObj[i].transform.name = "Fragment" + " " + i;
            fracMesh.name = "Fragment" + " " + i;
            FracturingObj[i].transform.tag = "Fragment";



            FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>().sharedMesh = fracMesh;
            FracturingObj[i].transform.gameObject.GetComponent<Renderer>().material = texture;      
              
        }


        ////////FRONT FACE FRACTURES////////

        FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
             verts[0],verts[24],verts[27],verts[36],verts[42],
        };

        FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
             0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
             verts[1],verts[24],verts[25],verts[36],verts[42],
        };

        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0,  3,2,0,  4,1,0, 0,2,4,  3,1,4,  2,3,4,
        };

        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[2],verts[26],verts[27],verts[36],verts[42],
        };

        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[3], verts[25], verts[26], verts[36], verts[42],
        };

        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };



        ////////FRONT FACE FRACTURES////////


        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[2], verts[26], verts[29], verts[37],verts[42],
        };

        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[3], verts[26], verts[28], verts[37], verts[42],
        };

        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[4], verts[29], verts[30], verts[37], verts[42],
        };

        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[5], verts[28], verts[30], verts[37], verts[42],
        };

        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        ////////FRONT FACE FRACTURES////////


        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[4], verts[30], verts[31], verts[38], verts[42],
        };

        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[5], verts[30], verts[32], verts[38], verts[42],
        };

        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
          verts[6], verts[31], verts[33], verts[38], verts[42],
        };

        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[7], verts[32], verts[33], verts[38], verts[42],
        };

        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };
        ////////FRONT FACE FRACTURES////////




        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[1], verts[25], verts[35], verts[40], verts[42],
        };

        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        { 
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
         verts[7], verts[32], verts[35], verts[40], verts[42],
        };

        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[3], verts[25], verts[28], verts[40], verts[42],
        };

        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[5], verts[28], verts[32], verts[40], verts[42],
        };

        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };
        ////////FRONT FACE FRACTURES////////







        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[6], verts[31], verts[34], verts[39], verts[42],
        };

        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
         verts[0], verts[27], verts[34], verts[39], verts[42],
        };

        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[4], verts[29], verts[31], verts[39], verts[42],
        };

        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[2], verts[27], verts[29], verts[39], verts[42],
        };

        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };



        ////////FRONT FACE FRACTURES////////




        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[6], verts[33], verts[34], verts[41], verts[42],
        };

        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
           0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[7], verts[33], verts[35], verts[41], verts[42],
        };

        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[0], verts[24], verts[34], verts[41], verts[42],
        };

        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        { 
            verts[1], verts[24], verts[35], verts[41], verts[42],
        };

        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };



        for (int  i = 0; i <FracturingObj.Count;i++)
        {
            if (FracturingObj[i].transform.gameObject.GetComponent<MeshCollider>() == null)
            {

                MeshCollider meshCol = FracturingObj[i].gameObject.AddComponent<MeshCollider>();
                meshCol.convex = true;


                /*BoxCollider meshCol = FracturingObj[i].gameObject.AddComponent<BoxCollider>();
                Vector3 fracCenter = FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>().mesh.bounds.center;

                meshCol.size = new Vector3(0.1f, 0.1f, 0.1f);
                meshCol.center = new Vector3(fracCenter.x, fracCenter.y, fracCenter.z);*/

            }

            if (FracturingObj[i].transform.gameObject.GetComponent<Rigidbody>() == null)
            {
                FracturingObj[i].gameObject.AddComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, 100f, 10f);
            }
            FracturingObj[i].gameObject.AddComponent<SplitMesh>().enabled = true;
            //FracturingObj[i].transform.parent = this.transform;
            Destroy(this.transform.gameObject);
        }
    }
}
