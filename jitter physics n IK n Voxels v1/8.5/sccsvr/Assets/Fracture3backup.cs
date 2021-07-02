using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Fracture3backup : MonoBehaviour
{
    private Thread t1;

    private Mesh mesh;
    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private Vector3 center;

    private Vector3 randomPoint;
    private Vector3[] randomPoints;

    private Vector3 point0;
    private Vector3 point1;
    private Vector3 point2;
    private Vector3 point3;
    private Vector3 point4;
    private Vector3 point5;
    private Vector3 point6;
    private Vector3 point7;
    private Vector3 point8;
    private Vector3 point9;
    private Vector3 point10;
    private Vector3 point11;
    private Vector3 point12;
    private Vector3 point13;
    private Vector3 point14;
    private Vector3 point15;
    private Vector3 point16;
    private Vector3 point17;
    private Vector3 point18;
    private Vector3 point19;
    private Vector3 point20;
    private Vector3 point21;
    private Vector3 point22;
    private Vector3 point23;

    private List<Vector3> verts = new List<Vector3>();


    private Vector3[] OriginalVerts;

    public GameObject sphere;

    public Material texture;

    private List<GameObject> FracturingObj = new List<GameObject>();

    private Mesh fracMesh;

    public int explosionForce;
    int radius;




    private void Func1()
    {
    
    }








    void Start()
    {
        t1 = new Thread(Func1) { Name = "Thread 1" };
        if (!t1.IsAlive)
        {
            t1.Start();
        }


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


        //////////////FRONT FACE EDGES //////////////
        Vector3 minPosition0 = mesh.vertices[0];
        Vector3 maxPosition0 = mesh.vertices[1];
        Vector3 randomPosition0 = new Vector3(Random.Range(minPosition0.x, maxPosition0.x), Random.Range(minPosition0.y, maxPosition0.y), Random.Range(minPosition0.z, maxPosition0.z));
        Vector3 randomPosition1 = randomPosition0;
        Vector3 randomPosition2 = randomPosition0;
        Vector3 randomPosition3 = randomPosition0;

        Vector3 minPosition1 = mesh.vertices[1];
        Vector3 maxPosition1 = mesh.vertices[3];
        Vector3 randomPosition4 = new Vector3(Random.Range(minPosition1.x, maxPosition1.x), Random.Range(minPosition1.y, maxPosition1.y), Random.Range(minPosition1.z, maxPosition1.z));
        Vector3 randomPosition5 = randomPosition4;
        Vector3 randomPosition6 = randomPosition4;
        Vector3 randomPosition7 = randomPosition4;

        Vector3 minPosition2 = mesh.vertices[3];
        Vector3 maxPosition2 = mesh.vertices[2];
        Vector3 randomPosition8 = new Vector3(Random.Range(minPosition2.x, maxPosition2.x), Random.Range(minPosition2.y, maxPosition2.y), Random.Range(minPosition2.z, maxPosition2.z));
        Vector3 randomPosition9 = randomPosition8;
        Vector3 randomPosition10 = randomPosition8;
        Vector3 randomPosition11 = randomPosition8;

        Vector3 minPosition3 = mesh.vertices[2];
        Vector3 maxPosition3 = mesh.vertices[0];
        Vector3 randomPosition12 = new Vector3(Random.Range(minPosition3.x, maxPosition3.x), Random.Range(minPosition3.y, maxPosition3.y), Random.Range(minPosition3.z, maxPosition3.z));
        Vector3 randomPosition13 = randomPosition12;
        Vector3 randomPosition14 = randomPosition12;
        Vector3 randomPosition15 = randomPosition12;



        //////////////TOP FACE EDGES//////////////

        Vector3 minPosition4 = mesh.vertices[3];
        Vector3 maxPosition4 = mesh.vertices[5];
        Vector3 randomPosition16 = new Vector3(Random.Range(minPosition4.x, maxPosition4.x), Random.Range(minPosition4.y, maxPosition4.y), Random.Range(minPosition4.z, maxPosition4.z));
        Vector3 randomPosition17 = randomPosition16;
        Vector3 randomPosition18 = randomPosition16;
        Vector3 randomPosition19 = randomPosition16;

        Vector3 minPosition5 = mesh.vertices[2];
        Vector3 maxPosition5 = mesh.vertices[4];
        Vector3 randomPosition20 = new Vector3(Random.Range(minPosition5.x, maxPosition5.x), Random.Range(minPosition5.y, maxPosition5.y), Random.Range(minPosition5.z, maxPosition5.z));
        Vector3 randomPosition21 = randomPosition20;
        Vector3 randomPosition22 = randomPosition20;
        Vector3 randomPosition23 = randomPosition20;

        Vector3 minPosition6 = mesh.vertices[4];
        Vector3 maxPosition6 = mesh.vertices[5];
        Vector3 randomPosition24 = new Vector3(Random.Range(minPosition6.x, maxPosition6.x), Random.Range(minPosition6.y, maxPosition6.y), Random.Range(minPosition6.z, maxPosition6.z));
        Vector3 randomPosition25 = randomPosition24;
        Vector3 randomPosition26 = randomPosition24;
        Vector3 randomPosition27 = randomPosition24;




        //////////////BACK FACE EDGES//////////////

        Vector3 minPosition7 = mesh.vertices[4];
        Vector3 maxPosition7 = mesh.vertices[6];
        Vector3 randomPosition28 = new Vector3(Random.Range(minPosition7.x, maxPosition7.x), Random.Range(minPosition7.y, maxPosition7.y), Random.Range(minPosition7.z, maxPosition7.z));
        Vector3 randomPosition29 = randomPosition28;
        Vector3 randomPosition30 = randomPosition28;
        Vector3 randomPosition31 = randomPosition28;

        Vector3 minPosition8 = mesh.vertices[5];
        Vector3 maxPosition8 = mesh.vertices[7];
        Vector3 randomPosition32 = new Vector3(Random.Range(minPosition8.x, maxPosition8.x), Random.Range(minPosition8.y, maxPosition8.y), Random.Range(minPosition8.z, maxPosition8.z));
        Vector3 randomPosition33 = randomPosition32;
        Vector3 randomPosition34 = randomPosition32;
        Vector3 randomPosition35 = randomPosition32;

        Vector3 minPosition9 = mesh.vertices[6];
        Vector3 maxPosition9 = mesh.vertices[7];
        Vector3 randomPosition36 = new Vector3(Random.Range(minPosition9.x, maxPosition9.x), Random.Range(minPosition9.y, maxPosition9.y), Random.Range(minPosition9.z, maxPosition9.z));
        Vector3 randomPosition37 = randomPosition36;
        Vector3 randomPosition38 = randomPosition36;
        Vector3 randomPosition39 = randomPosition36;



        //////////////BOTTOM FACE EDGES//////////////

        Vector3 minPosition10 = mesh.vertices[0];
        Vector3 maxPosition10 = mesh.vertices[6];
        Vector3 randomPosition40 = new Vector3(Random.Range(minPosition10.x, maxPosition10.x), Random.Range(minPosition10.y, maxPosition10.y), Random.Range(minPosition10.z, maxPosition10.z));
        Vector3 randomPosition41 = randomPosition40;
        Vector3 randomPosition42 = randomPosition40;
        Vector3 randomPosition43 = randomPosition40;

        Vector3 minPosition11 = mesh.vertices[1];
        Vector3 maxPosition11 = mesh.vertices[7];
        Vector3 randomPosition44 = new Vector3(Random.Range(minPosition11.x, maxPosition11.x), Random.Range(minPosition11.y, maxPosition11.y), Random.Range(minPosition11.z, maxPosition11.z));
        Vector3 randomPosition45 = randomPosition44;
        Vector3 randomPosition46 = randomPosition44;
        Vector3 randomPosition47 = randomPosition44;


        //////////////FRONT FACES//////////////
        Vector3 minPosition12 = mesh.vertices[0];
        Vector3 maxPosition12 = mesh.vertices[3];
        Vector3 randomPosition48 = new Vector3(Random.Range(minPosition12.x, maxPosition12.x), Random.Range(minPosition12.y, maxPosition12.y), Random.Range(minPosition12.z, maxPosition12.z));
        Vector3 randomPosition49 = randomPosition48;
        Vector3 randomPosition50 = randomPosition48;
        Vector3 randomPosition51 = randomPosition48;

        //////////////TOP FACES//////////////
        Vector3 minPosition13 = mesh.vertices[2];
        Vector3 maxPosition13 = mesh.vertices[5];
        Vector3 randomPosition52 = new Vector3(Random.Range(minPosition13.x, maxPosition13.x), Random.Range(minPosition13.y, maxPosition13.y), Random.Range(minPosition13.z, maxPosition13.z));
        Vector3 randomPosition53 = randomPosition52;
        Vector3 randomPosition54 = randomPosition52;
        Vector3 randomPosition55 = randomPosition52;


        //////////////BACK FACES //////////////
        Vector3 minPosition14 = mesh.vertices[4];
        Vector3 maxPosition14 = mesh.vertices[7];
        Vector3 randomPosition56 = new Vector3(Random.Range(minPosition14.x, maxPosition14.x), Random.Range(minPosition14.y, maxPosition14.y), Random.Range(minPosition14.z, maxPosition14.z));
        Vector3 randomPosition57 = randomPosition56;
        Vector3 randomPosition58 = randomPosition56;
        Vector3 randomPosition59 = randomPosition56;


        //////////////LEFT FACES //////////////
        Vector3 minPosition15 = mesh.vertices[2];
        Vector3 maxPosition15 = mesh.vertices[6];
        Vector3 randomPosition60 = new Vector3(Random.Range(minPosition15.x, maxPosition15.x), Random.Range(minPosition15.y, maxPosition15.y), Random.Range(minPosition15.z, maxPosition15.z));
        Vector3 randomPosition61 = randomPosition60;
        Vector3 randomPosition62 = randomPosition60;
        Vector3 randomPosition63 = randomPosition60;


        //////////////RIGHT FACES //////////////
        Vector3 minPosition16 = mesh.vertices[3];
        Vector3 maxPosition16 = mesh.vertices[7];
        Vector3 randomPosition64 = new Vector3(Random.Range(minPosition16.x, maxPosition16.x), Random.Range(minPosition16.y, maxPosition16.y), Random.Range(minPosition16.z, maxPosition16.z));
        Vector3 randomPosition65 = randomPosition64;
        Vector3 randomPosition66 = randomPosition64;
        Vector3 randomPosition67 = randomPosition64;


        //////////////BOTTOM FACES //////////////
        Vector3 minPosition17 = mesh.vertices[1];
        Vector3 maxPosition17 = mesh.vertices[6];
        Vector3 randomPosition68 = new Vector3(Random.Range(minPosition17.x, maxPosition17.x), Random.Range(minPosition17.y, maxPosition17.y), Random.Range(minPosition17.z, maxPosition17.z));
        Vector3 randomPosition69 = randomPosition68;
        Vector3 randomPosition70 = randomPosition68;
        Vector3 randomPosition71 = randomPosition68;

        //////////////FRONT FACE BOTTOM EDGES //24-25-26-27
        verts.Add(randomPosition0);
        verts.Add(randomPosition1);
        verts.Add(randomPosition2);
        verts.Add(randomPosition3);
        //////////////FRONT FACE RIGHT EDGES //28-29-30-31
        verts.Add(randomPosition4);
        verts.Add(randomPosition5);
        verts.Add(randomPosition6);
        verts.Add(randomPosition7);
        //////////////FRONT FACE TOP EDGES //32-33-34-35
        verts.Add(randomPosition8);
        verts.Add(randomPosition9);
        verts.Add(randomPosition10);
        verts.Add(randomPosition11);
        //////////////FRONT FACE LEFT EDGES //36-37-38-39
        verts.Add(randomPosition12);
        verts.Add(randomPosition13);
        verts.Add(randomPosition14);
        verts.Add(randomPosition15);

        //////////////TOP FACE RIGHT EDGES //40-41-42-43
        verts.Add(randomPosition16);
        verts.Add(randomPosition17);
        verts.Add(randomPosition18);
        verts.Add(randomPosition19);
        
        //////////////TOP FACE LEFT EDGES //44-45-46-47
        verts.Add(randomPosition20);
        verts.Add(randomPosition21);
        verts.Add(randomPosition22);
        verts.Add(randomPosition23);

        //////////////TOP FACE FORWARD EDGES //48-49-50-51
        verts.Add(randomPosition24);
        verts.Add(randomPosition25);
        verts.Add(randomPosition26);
        verts.Add(randomPosition27);

        //////////////BACK FACE LEFT EDGES//52-53-54-55
        verts.Add(randomPosition28);
        verts.Add(randomPosition29);
        verts.Add(randomPosition30);
        verts.Add(randomPosition31);
        //////////////BACK FACE RIGHT EDGES//56-57-58-59
        verts.Add(randomPosition32);
        verts.Add(randomPosition33);
        verts.Add(randomPosition34);
        verts.Add(randomPosition35);
        //////////////BACK FACE FORWARD EDGES//60-61-62-63
        verts.Add(randomPosition36);
        verts.Add(randomPosition37);
        verts.Add(randomPosition38);
        verts.Add(randomPosition39);


        //////////////BOTTOM LEFT FACE EDGES//64-65-66-67
        verts.Add(randomPosition40);
        verts.Add(randomPosition41);
        verts.Add(randomPosition42);
        verts.Add(randomPosition43);
        //////////////BOTTOM RIGHT FACE EDGES//68-69-70-71
        verts.Add(randomPosition44);
        verts.Add(randomPosition45);
        verts.Add(randomPosition46);
        verts.Add(randomPosition47);


        //////////////FRONT FACE MIDDLE//72-73-74-75
        verts.Add(randomPosition48);
        verts.Add(randomPosition49);
        verts.Add(randomPosition50);
        verts.Add(randomPosition51);
        //////////////TOP MIDDLE//76-77-78-79
        verts.Add(randomPosition52);
        verts.Add(randomPosition53);
        verts.Add(randomPosition54);
        verts.Add(randomPosition55);
        //////////////BACK MIDDLE//80-81-82-83
        verts.Add(randomPosition56);
        verts.Add(randomPosition57);
        verts.Add(randomPosition58);
        verts.Add(randomPosition59);

        //////////////LEFT MIDDLE//84-85-86-87
        verts.Add(randomPosition60);
        verts.Add(randomPosition61);
        verts.Add(randomPosition62);
        verts.Add(randomPosition63);

        //////////////RIGHT MIDDLE//88-89-90-91
        verts.Add(randomPosition64);
        verts.Add(randomPosition65);
        verts.Add(randomPosition66);
        verts.Add(randomPosition67);

        //////////////BOTTOM MIDDLE//92-93-94-95
        verts.Add(randomPosition68);
        verts.Add(randomPosition69);
        verts.Add(randomPosition70);
        verts.Add(randomPosition71);



        radius = (int)Random.Range((float)0f, (float)0.5f);

        randomPoint = Random.insideUnitSphere * radius + center;
        randomPoints = new Vector3[]
        {
            point0 = randomPoint,
        };

        point1 = point0;
        point2 = point0;
        point3 = point0;
        point4 = point0;
        point5 = point0;
        point6 = point0;
        point7 = point0;
        point8 = point0;
        point9 = point0;
        point10 = point0;
        point11 = point0;
        point12 = point0;
        point13 = point0;
        point14 = point0;
        point15 = point0;
        point16 = point0;
        point17 = point0;
        point18 = point0;
        point19 = point0;
        point20 = point0;
        point21 = point0;
        point22 = point0;
        point23 = point0;


        //////Center 96//////
        verts.Add(point0);
        //////Center 97//////
        verts.Add(point1);
        //////Center 98//////
        verts.Add(point2);
        //////Center 99//////
        verts.Add(point3);
        //////Center 100//////
        verts.Add(point4);
        //////Center 101//////
        verts.Add(point5);
        //////Center 102//////
        verts.Add(point6);
        //////Center 103//////
        verts.Add(point7);
        //////Center 104//////
        verts.Add(point8);
        //////Center 105//////
        verts.Add(point9);
        //////Center 106//////
        verts.Add(point10);
        //////Center 107//////
        verts.Add(point11);
        //////Center 108//////
        verts.Add(point12);
        //////Center 109//////
        verts.Add(point13);
        //////Center 110//////
        verts.Add(point14);
        //////Center 111//////
        verts.Add(point15);
        //////Center 112//////
        verts.Add(point16);
        //////Center 113//////
        verts.Add(point17);
        //////Center 114//////
        verts.Add(point18);
        //////Center 115//////
        verts.Add(point19);
        //////Center 116//////
        verts.Add(point20);
        //////Center 117//////
        verts.Add(point21);
        //////Center 118//////
        verts.Add(point22);
        //////Center 119//////
        verts.Add(point23);

        //mesh.triangles = null;
        //mesh.vertices = null;


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
             verts[0],verts[24],verts[36],verts[72],verts[96],
        };

        FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
             0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
             verts[1],verts[25],verts[28],verts[73],verts[97],

        };

        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0,  3,2,0,  4,1,0, 0,2,4,  3,1,4,  2,3,4,
        };

        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[2],verts[37],verts[32],verts[74],verts[98],
        };

        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            //1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
             0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[3], verts[33], verts[29], verts[75], verts[99],
        };

        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            //1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };
        



        ////////TOP FACE FRACTURES////////


        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[8], verts[34], verts[44], verts[76],verts[100],
        };

        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[9], verts[35], verts[40], verts[77], verts[101],
        };

        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[10], verts[45], verts[48], verts[78], verts[102],
        };

        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[11], verts[49], verts[41], verts[79], verts[103],
        };

        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            //1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        ////////BACK FACE FRACTURES////////


        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[4], verts[52], verts[50], verts[80], verts[104],
        };

        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            //0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[5], verts[51], verts[56], verts[81], verts[105],
        };

        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
          verts[6], verts[53], verts[60], verts[82], verts[106],
        };

        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[7], verts[61], verts[57], verts[83], verts[107],
        };

        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            //1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
             0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };



        ////////RIGHT FACE FRACTURES////////


        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[16], verts[30], verts[68], verts[88], verts[108],
        };

        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        { 
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
         verts[19], verts[58], verts[69], verts[89], verts[109],
        };

        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[17], verts[31], verts[42], verts[90], verts[110],
        };

        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[18], verts[59], verts[43], verts[91], verts[111],
        };

        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            //0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };




        ////////LEFT FACE FRACTURES////////

        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[20], verts[64], verts[54], verts[84], verts[112],
        };

        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            //1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
         verts[23], verts[65], verts[38], verts[85], verts[113],
        };

        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            //0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[21], verts[55], verts[46], verts[86], verts[114],
        };

        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            //1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[22], verts[39], verts[47], verts[87], verts[115],
        };

        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };



        ////////FRONT FACE FRACTURES////////




        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[12], verts[62], verts[66], verts[92], verts[116],
        };

        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
           0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };

        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[15], verts[63], verts[70], verts[93], verts[117],
        };

        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        {
            verts[13], verts[26], verts[67], verts[94], verts[118],
        };

        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };

        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = new Vector3[]
        { 
            verts[14], verts[26], verts[71], verts[95], verts[119],
        };

        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };



        for (int i = 0; i < FracturingObj.Count; i++)
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
            //FracturingObj[i].gameObject.AddComponent<SplitMesh>().enabled = true;
            //FracturingObj[i].transform.parent = this.transform;
            Destroy(this.transform.gameObject);
        }
    }


    void OnDrawGizmos()
    {

        /*if (mesh.vertices == null)
        {
            return;
        }*/

       /* Gizmos.color = Color.black;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(new Vector3(mesh.vertices[i].x + transform.position.x, mesh.vertices[i].y + transform.position.y, mesh.vertices[i].z + transform.position.z), 0.01f);
        }*/
        /*if (mesh.vertices == null)
       {
           return;
       }*/

        Gizmos.color = Color.black;
        for (int i = 0; i < verts.Count; i++)
        {
            Gizmos.DrawSphere(new Vector3(verts[11].x + transform.position.x, verts[11].y + transform.position.y, verts[11].z + transform.position.z), 1f);
        }


    }






}
