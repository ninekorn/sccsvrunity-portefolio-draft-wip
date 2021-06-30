using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class cubicFrac : MonoBehaviour
{
    private Thread t1;
    private Thread t2;

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

    public Material texture;

    private List<GameObject> FracturingObj = new List<GameObject>();

    private Mesh fracMesh;

    public int explosionForce;
    int radius;

    Vector3 minPosition0;
    Vector3 maxPosition0;
    Vector3 randomPosition0;

    Vector3 minPosition1;
    Vector3 maxPosition1;
    Vector3 randomPosition4;

    Vector3 minPosition2;
    Vector3 maxPosition2;
    Vector3 randomPosition8;

    Vector3 minPosition3;
    Vector3 maxPosition3;
    Vector3 randomPosition12;

    Vector3 minPosition4;
    Vector3 maxPosition4;
    Vector3 randomPosition16;

    Vector3 minPosition5;
    Vector3 maxPosition5;
    Vector3 randomPosition20;

    Vector3 minPosition6;
    Vector3 maxPosition6;
    Vector3 randomPosition24;

    Vector3 minPosition7;
    Vector3 maxPosition7;
    Vector3 randomPosition28;

    Vector3 minPosition8;
    Vector3 maxPosition8;
    Vector3 randomPosition32;

    Vector3 minPosition9;
    Vector3 maxPosition9;
    Vector3 randomPosition36;

    Vector3 minPosition10;
    Vector3 maxPosition10;
    Vector3 randomPosition40;

    Vector3 minPosition11;
    Vector3 maxPosition11;
    Vector3 randomPosition44;

    Vector3 minPosition12;
    Vector3 maxPosition12;
    Vector3 randomPosition48;

    Vector3 minPosition13;
    Vector3 maxPosition13;
    Vector3 randomPosition52;

    Vector3 minPosition14;
    Vector3 maxPosition14;
    Vector3 randomPosition56;

    Vector3 minPosition15;
    Vector3 maxPosition15;
    Vector3 randomPosition60;

    Vector3 minPosition16;
    Vector3 maxPosition16;
    Vector3 randomPosition64;

    Vector3 minPosition17;
    Vector3 maxPosition17;
    Vector3 randomPosition68;

    Vector3[] FracObjVerts0;
    int[] FracObjTris0;
    int[] FracObjTris100;
    int[] FracObjTris101;
    int[] FracObjTris102;
    int[] FracObjTris103;
    int[] FracObjTris104;

    Vector3[] FracObjVerts1;
    int[] FracObjTris1;
    int[] FracObjTris200;
    int[] FracObjTris201;
    int[] FracObjTris202;
    int[] FracObjTris203;
    int[] FracObjTris204;

    Vector3[] FracObjVerts2;
    int[] FracObjTris2;
    int[] FracObjTris300;
    int[] FracObjTris301;
    int[] FracObjTris302;
    int[] FracObjTris303;
    int[] FracObjTris304;

    Vector3[] FracObjVerts3;
    int[] FracObjTris3;
    int[] FracObjTris400;
    int[] FracObjTris401;
    int[] FracObjTris402;
    int[] FracObjTris403;
    int[] FracObjTris404;


    Vector3[] FracObjVerts4;
    int[] FracObjTris4;
    int[] FracObjTris500;
    int[] FracObjTris501;
    int[] FracObjTris502;
    int[] FracObjTris503;
    int[] FracObjTris504;

    Vector3[] FracObjVerts5;
    int[] FracObjTris5;
    int[] FracObjTris600;
    int[] FracObjTris601;
    int[] FracObjTris602;
    int[] FracObjTris603;
    int[] FracObjTris604;

    Vector3[] FracObjVerts6;
    int[] FracObjTris6;
    int[] FracObjTris700;
    int[] FracObjTris701;
    int[] FracObjTris702;
    int[] FracObjTris703;
    int[] FracObjTris704;

    Vector3[] FracObjVerts7;
    int[] FracObjTris7;
    int[] FracObjTris800;
    int[] FracObjTris801;
    int[] FracObjTris802;
    int[] FracObjTris803;
    int[] FracObjTris804;


    Vector3[] FracObjVerts8;
    int[] FracObjTris8;
    int[] FracObjTris900;
    int[] FracObjTris901;
    int[] FracObjTris902;
    int[] FracObjTris903;
    int[] FracObjTris904;


    Vector3[] FracObjVerts9;
    int[] FracObjTris9;
    int[] FracObjTris1000;
    int[] FracObjTris1001;
    int[] FracObjTris1002;
    int[] FracObjTris1003;
    int[] FracObjTris1004;

    Vector3[] FracObjVerts10;
    int[] FracObjTris10;
    int[] FracObjTris2000;
    int[] FracObjTris2001;
    int[] FracObjTris2002;
    int[] FracObjTris2003;
    int[] FracObjTris2004;

    Vector3[] FracObjVerts11;
    int[] FracObjTris11;
    int[] FracObjTris3000;
    int[] FracObjTris3001;
    int[] FracObjTris3002;
    int[] FracObjTris3003;
    int[] FracObjTris3004;

    Vector3[] FracObjVerts12;
    int[] FracObjTris12;
    int[] FracObjTris4000;
    int[] FracObjTris4001;
    int[] FracObjTris4002;
    int[] FracObjTris4003;
    int[] FracObjTris4004;

    Vector3[] FracObjVerts13;
    int[] FracObjTris13;
    int[] FracObjTris5000;
    int[] FracObjTris5001;
    int[] FracObjTris5002;
    int[] FracObjTris5003;
    int[] FracObjTris5004;

    Vector3[] FracObjVerts14;
    int[] FracObjTris14;
    int[] FracObjTris6000;
    int[] FracObjTris6001;
    int[] FracObjTris6002;
    int[] FracObjTris6003;
    int[] FracObjTris6004;

    Vector3[] FracObjVerts15;
    int[] FracObjTris15;
    int[] FracObjTris7000;
    int[] FracObjTris7001;
    int[] FracObjTris7002;
    int[] FracObjTris7003;
    int[] FracObjTris7004;

    Vector3[] FracObjVerts16;
    int[] FracObjTris16;
    int[] FracObjTris8000;
    int[] FracObjTris8001;
    int[] FracObjTris8002;
    int[] FracObjTris8003;
    int[] FracObjTris8004;

    Vector3[] FracObjVerts17;
    int[] FracObjTris17;
    int[] FracObjTris9000;
    int[] FracObjTris9001;
    int[] FracObjTris9002;
    int[] FracObjTris9003;
    int[] FracObjTris9004;

    Vector3[] FracObjVerts18;
    int[] FracObjTris18;
    int[] FracObjTris10000;
    int[] FracObjTris10001;
    int[] FracObjTris10002;
    int[] FracObjTris10003;
    int[] FracObjTris10004;


    Vector3[] FracObjVerts19;
    int[] FracObjTris19;
    int[] FracObjTris11000;
    int[] FracObjTris11001;
    int[] FracObjTris11002;
    int[] FracObjTris11003;
    int[] FracObjTris11004;


    Vector3[] FracObjVerts20;
    int[] FracObjTris20;
    int[] FracObjTris12000;
    int[] FracObjTris12001;
    int[] FracObjTris12002;
    int[] FracObjTris12003;
    int[] FracObjTris12004;

    Vector3[] FracObjVerts21;
    int[] FracObjTris21;
    int[] FracObjTris13000;
    int[] FracObjTris13001;
    int[] FracObjTris13002;
    int[] FracObjTris13003;
    int[] FracObjTris13004;

    Vector3[] FracObjVerts22;
    int[] FracObjTris22;
    int[] FracObjTris14000;
    int[] FracObjTris14001;
    int[] FracObjTris14002;
    int[] FracObjTris14003;
    int[] FracObjTris14004;

    Vector3[] FracObjVerts23;
    int[] FracObjTris23;
    int[] FracObjTris15000;
    int[] FracObjTris15001;
    int[] FracObjTris15002;
    int[] FracObjTris15003;
    int[] FracObjTris15004;


    public Material mat;
    Mesh mesher;


    Vector3[] newVerts;
    Vector3[] newNormals;
    Vector2[] newUvs;

    List<Vector3> vertys = new List<Vector3>();
    List<Vector3> norms = new List<Vector3>();
    Vector3[] normalz;
    int[] triang;







    private void Func1()
    {
        /////////////FRONT FACE EDGES //////////////
        Vector3 randomPosition1 = randomPosition0;
        Vector3 randomPosition2 = randomPosition0;
        Vector3 randomPosition3 = randomPosition0;

        Vector3 randomPosition5 = randomPosition4;
        Vector3 randomPosition6 = randomPosition4;
        Vector3 randomPosition7 = randomPosition4;

        Vector3 randomPosition9 = randomPosition8;
        Vector3 randomPosition10 = randomPosition8;
        Vector3 randomPosition11 = randomPosition8;

        Vector3 randomPosition13 = randomPosition12;
        Vector3 randomPosition14 = randomPosition12;
        Vector3 randomPosition15 = randomPosition12;

        //////////////TOP FACE EDGES//////////////

        Vector3 randomPosition17 = randomPosition16;
        Vector3 randomPosition18 = randomPosition16;
        Vector3 randomPosition19 = randomPosition16;

        Vector3 randomPosition21 = randomPosition20;
        Vector3 randomPosition22 = randomPosition20;
        Vector3 randomPosition23 = randomPosition20;

        Vector3 randomPosition25 = randomPosition24;
        Vector3 randomPosition26 = randomPosition24;
        Vector3 randomPosition27 = randomPosition24;

        //////////////BACK FACE EDGES//////////////
        Vector3 randomPosition29 = randomPosition28;
        Vector3 randomPosition30 = randomPosition28;
        Vector3 randomPosition31 = randomPosition28;

        Vector3 randomPosition33 = randomPosition32;
        Vector3 randomPosition34 = randomPosition32;
        Vector3 randomPosition35 = randomPosition32;

        Vector3 randomPosition37 = randomPosition36;
        Vector3 randomPosition38 = randomPosition36;
        Vector3 randomPosition39 = randomPosition36;

        //////////////BOTTOM FACE EDGES//////////////

        Vector3 randomPosition41 = randomPosition40;
        Vector3 randomPosition42 = randomPosition40;
        Vector3 randomPosition43 = randomPosition40;

        Vector3 randomPosition45 = randomPosition44;
        Vector3 randomPosition46 = randomPosition44;
        Vector3 randomPosition47 = randomPosition44;

        //////////////FRONT FACES//////////////
        Vector3 randomPosition49 = randomPosition48;
        Vector3 randomPosition50 = randomPosition48;
        Vector3 randomPosition51 = randomPosition48;

        //////////////TOP FACES//////////////
        Vector3 randomPosition53 = randomPosition52;
        Vector3 randomPosition54 = randomPosition52;
        Vector3 randomPosition55 = randomPosition52;


        //////////////BACK FACES //////////////
        Vector3 randomPosition57 = randomPosition56;
        Vector3 randomPosition58 = randomPosition56;
        Vector3 randomPosition59 = randomPosition56;


        //////////////LEFT FACES //////////////
        Vector3 randomPosition61 = randomPosition60;
        Vector3 randomPosition62 = randomPosition60;
        Vector3 randomPosition63 = randomPosition60;


        //////////////RIGHT FACES //////////////
        Vector3 randomPosition65 = randomPosition64;
        Vector3 randomPosition66 = randomPosition64;
        Vector3 randomPosition67 = randomPosition64;


        //////////////BOTTOM FACES //////////////
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
    }


    private void Func2()
    {


        ////////FRONT FACE FRACTURES////////

        FracObjVerts0 = new Vector3[]
        {
             verts[0],verts[24],verts[36],verts[72],verts[96],
        };


        /* FracObjTris0 = new int[]
        {
              0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/


        FracObjTris0 = new int[]
        {
            0,3,1,
        };
        FracObjTris100 = new int[]
        {
            0,2,3,
        };
        FracObjTris101 = new int[]
        {
            0,1,4,
        };
        FracObjTris102 = new int[]
        {
            4,2,0,
        };
        FracObjTris103 = new int[]
        {
            4,1,3,
        };
        FracObjTris104 = new int[]
        {
            4,3,2,
        };




        FracObjVerts1 = new Vector3[]
        {
             verts[1],verts[25],verts[28],verts[73],verts[97],

        };

        /*FracObjTris1 = new int[]
        {
            1,3,0,  3,2,0,  4,1,0, 0,2,4,  3,1,4,  2,3,4,
        };*/

        FracObjTris1 = new int[]
        {
            1,3,0,
        };
        FracObjTris200 = new int[]
        {
            3,2,0,
        };
        FracObjTris201 = new int[]
        {
            4,1,0,
        };
        FracObjTris202 = new int[]
        {
            0,2,4,
        };
        FracObjTris203 = new int[]
        {
            3,1,4,
        };
        FracObjTris204 = new int[]
        {
            2,3,4,
        };




        FracObjVerts2 = new Vector3[]
        {
            verts[2],verts[37],verts[32],verts[74],verts[98],
        };

        /*FracObjTris2 = new int[]
        {        
             0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris2 = new int[]
        {
             0,3,1,
        };
        FracObjTris300 = new int[]
        {
             0,2,3,
        };
        FracObjTris301 = new int[]
        {
             0,1,4,
        };
        FracObjTris302 = new int[]
        {
             4,2,0,
        };
        FracObjTris303 = new int[]
        {
             4,1,3,
        };
        FracObjTris304 = new int[]
        {
             4,3,2,
        };



        FracObjVerts3 = new Vector3[]
        {
            verts[3], verts[33], verts[29], verts[75], verts[99],
        };

        /*FracObjTris3 = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris3 = new int[]
       {
             0,3,1,
       };
        FracObjTris400 = new int[]
        {
             0,2,3,
        };
        FracObjTris401 = new int[]
        {
             0,1,4,
        };
        FracObjTris402 = new int[]
        {
             4,2,0,
        };
        FracObjTris403 = new int[]
        {
             4,1,3,
        };
        FracObjTris404 = new int[]
        {
             4,3,2,
        };



        ////////TOP FACE FRACTURES////////


        FracObjVerts4 = new Vector3[]
        {
            verts[8], verts[34], verts[44], verts[76],verts[100],
        };

        /*FracObjTris4 = new int[]
       {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/

        FracObjTris4 = new int[]
       {
             0,3,1,
       };
        FracObjTris500 = new int[]
        {
             0,2,3,
        };
        FracObjTris501 = new int[]
        {
             0,1,4,
        };
        FracObjTris502 = new int[]
        {
             4,2,0,
        };
        FracObjTris503 = new int[]
        {
             4,1,3,
        };
        FracObjTris504 = new int[]
        {
             4,3,2,
        };




        FracObjVerts5 = new Vector3[]
        {
            verts[9], verts[35], verts[40], verts[77], verts[101],
        };

        /*FracObjTris5 = new int[]
       {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };*/
        FracObjTris5 = new int[]
        {
            1,3,0,
        };
        FracObjTris600 = new int[]
        {
            3,2,0,
        };
        FracObjTris601 = new int[]
        {
            4,1,0,
        };
        FracObjTris602 = new int[]
        {
            0,2,4,
        };
        FracObjTris603 = new int[]
        {
            3,1,4,
        };
        FracObjTris604 = new int[]
        {
            2,3,4,
        };




        FracObjVerts6 = new Vector3[]
         {
            verts[10], verts[45], verts[48], verts[78], verts[102],
        };

        /*FracObjTris6 = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/

        FracObjTris6 = new int[]
       {
             0,3,1,
       };
        FracObjTris700 = new int[]
        {
             0,2,3,
        };
        FracObjTris701 = new int[]
        {
             0,1,4,
        };
        FracObjTris702 = new int[]
        {
             4,2,0,
        };
        FracObjTris703 = new int[]
        {
             4,1,3,
        };
        FracObjTris704 = new int[]
        {
             4,3,2,
        };



        FracObjVerts7 = new Vector3[]
        {
            verts[11], verts[49], verts[41], verts[79], verts[103],
        };

        /*FracObjTris7 = new int[]
       {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris7 = new int[]
       {
             0,3,1,
       };
        FracObjTris800 = new int[]
        {
             0,2,3,
        };
        FracObjTris801 = new int[]
        {
             0,1,4,
        };
        FracObjTris802 = new int[]
        {
             4,2,0,
        };
        FracObjTris803 = new int[]
        {
             4,1,3,
        };
        FracObjTris804 = new int[]
        {
             4,3,2,
        };

        ////////BACK FACE FRACTURES////////


        FracObjVerts8 = new Vector3[]
        {
            verts[4], verts[52], verts[50], verts[80], verts[104],
        };

        /*FracObjTris8 = new int[]
       {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };*/

        FracObjTris8 = new int[]
        {
            1,3,0,
        };
        FracObjTris900 = new int[]
        {
            3,2,0,
        };
        FracObjTris901 = new int[]
        {
            4,1,0,
        };
        FracObjTris902 = new int[]
        {
            0,2,4,
        };
        FracObjTris903 = new int[]
        {
            3,1,4,
        };
        FracObjTris904 = new int[]
        {
            2,3,4,
        };



        FracObjVerts9 = new Vector3[]
        {
            verts[5], verts[51], verts[56], verts[81], verts[105],
        };

        /*FracObjTris9 = new int[]
       {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };*/
        FracObjTris9 = new int[]
        {
            1,3,0,
        };
        FracObjTris1000 = new int[]
        {
            3,2,0,
        };
        FracObjTris1001 = new int[]
        {
            4,1,0,
        };
        FracObjTris1002 = new int[]
        {
            0,2,4,
        };
        FracObjTris1003 = new int[]
        {
            3,1,4,
        };
        FracObjTris1004 = new int[]
        {
            2,3,4,
        };


        FracObjVerts10 = new Vector3[]
        {
          verts[6], verts[53], verts[60], verts[82], verts[106],
        };

        /*FracObjTris10 = new int[]
       {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris10 = new int[]
       {
             0,3,1,
       };
        FracObjTris2000 = new int[]
        {
             0,2,3,
        };
        FracObjTris2001 = new int[]
        {
             0,1,4,
        };
        FracObjTris2002 = new int[]
        {
             4,2,0,
        };
        FracObjTris2003 = new int[]
        {
             4,1,3,
        };
        FracObjTris2004 = new int[]
        {
             4,3,2,
        };



        FracObjVerts11 = new Vector3[]
        {
            verts[7], verts[61], verts[57], verts[83], verts[107],
        };

        /*FracObjTris11 = new int[]
       {
             0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris11 = new int[]
      {
             0,3,1,
      };
        FracObjTris3000 = new int[]
        {
             0,2,3,
        };
        FracObjTris3001 = new int[]
        {
             0,1,4,
        };
        FracObjTris3002 = new int[]
        {
             4,2,0,
        };
        FracObjTris3003 = new int[]
        {
             4,1,3,
        };
        FracObjTris3004 = new int[]
        {
             4,3,2,
        };


        ////////RIGHT FACE FRACTURES////////


        FracObjVerts12 = new Vector3[]
        {
            verts[16], verts[30], verts[68], verts[88], verts[108],
        };

        /*FracObjTris12 = new int[]
        { 
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,

        };*/

        FracObjTris12 = new int[]
        {
            1,3,0,
        };
        FracObjTris4000 = new int[]
        {
            3,2,0,
        };
        FracObjTris4001 = new int[]
        {
            4,1,0,
        };
        FracObjTris4002 = new int[]
        {
            0,2,4,
        };
        FracObjTris4003 = new int[]
        {
            3,1,4,
        };
        FracObjTris4004 = new int[]
        {
            2,3,4,
        };


        FracObjVerts13 = new Vector3[]
        {
         verts[19], verts[58], verts[69], verts[89], verts[109],
        };

        /*FracObjTris13 = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris13 = new int[]
      {
             0,3,1,
      };
        FracObjTris5000 = new int[]
        {
             0,2,3,
        };
        FracObjTris5001 = new int[]
        {
             0,1,4,
        };
        FracObjTris5002 = new int[]
        {
             4,2,0,
        };
        FracObjTris5003 = new int[]
        {
             4,1,3,
        };
        FracObjTris5004 = new int[]
        {
             4,3,2,
        };

        FracObjVerts14 = new Vector3[]
        {
            verts[17], verts[31], verts[42], verts[90], verts[110],
        };

        /*FracObjTris14 = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris14 = new int[]
      {
             0,3,1,
      };
        FracObjTris6000 = new int[]
        {
             0,2,3,
        };
        FracObjTris6001 = new int[]
        {
             0,1,4,
        };
        FracObjTris6002 = new int[]
        {
             4,2,0,
        };
        FracObjTris6003 = new int[]
        {
             4,1,3,
        };
        FracObjTris6004 = new int[]
        {
             4,3,2,
        };


        FracObjVerts15 = new Vector3[]
        {
            verts[18], verts[59], verts[43], verts[91], verts[111],
        };

        /*FracObjTris15 = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };*/

        FracObjTris15 = new int[]
        {
            1,3,0,
        };
        FracObjTris7000 = new int[]
        {
            3,2,0,
        };
        FracObjTris7001 = new int[]
        {
            4,1,0,
        };
        FracObjTris7002 = new int[]
        {
            0,2,4,
        };
        FracObjTris7003 = new int[]
        {
            3,1,4,
        };
        FracObjTris7004 = new int[]
        {
            2,3,4,
        };


        ////////LEFT FACE FRACTURES////////

        FracObjVerts16 = new Vector3[]
        {
            verts[20], verts[64], verts[54], verts[84], verts[112],
        };

        /*FracObjTris16 = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris16 = new int[]
      {
             0,3,1,
      };
        FracObjTris8000 = new int[]
        {
             0,2,3,
        };
        FracObjTris8001 = new int[]
        {
             0,1,4,
        };
        FracObjTris8002 = new int[]
        {
             4,2,0,
        };
        FracObjTris8003 = new int[]
        {
             4,1,3,
        };
        FracObjTris8004 = new int[]
        {
             4,3,2,
        };


        FracObjVerts17 = new Vector3[]
        {
         verts[23], verts[65], verts[38], verts[85], verts[113],
        };

        /*FracObjTris17 = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };*/
        FracObjTris17 = new int[]
        {
            1,3,0,
        };
        FracObjTris9000 = new int[]
        {
            3,2,0,
        };
        FracObjTris9001 = new int[]
        {
            4,1,0,
        };
        FracObjTris9002 = new int[]
        {
            0,2,4,
        };
        FracObjTris9003 = new int[]
        {
            3,1,4,
        };
        FracObjTris9004 = new int[]
        {
            2,3,4,
        };



        FracObjVerts18 = new Vector3[]
         {
            verts[21], verts[55], verts[46], verts[86], verts[114],
        };

        /*FracObjTris18 = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris18 = new int[]
      {
             0,3,1,
      };
        FracObjTris10000 = new int[]
        {
             0,2,3,
        };
        FracObjTris10001 = new int[]
        {
             0,1,4,
        };
        FracObjTris10002 = new int[]
        {
             4,2,0,
        };
        FracObjTris10003 = new int[]
        {
             4,1,3,
        };
        FracObjTris10004 = new int[]
        {
             4,3,2,
        };

        FracObjVerts19 = new Vector3[]
        {
            verts[22], verts[39], verts[47], verts[87], verts[115],
        };

        /*FracObjTris19 = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };*/
        FracObjTris19 = new int[]
        {
            1,3,0,
        };
        FracObjTris11000 = new int[]
        {
            3,2,0,
        };
        FracObjTris11001 = new int[]
        {
            4,1,0,
        };
        FracObjTris11002 = new int[]
        {
            0,2,4,
        };
        FracObjTris11003 = new int[]
        {
            3,1,4,
        };
        FracObjTris11004 = new int[]
        {
            2,3,4,
        };


        ////////FRONT FACE FRACTURES////////




        FracObjVerts20 = new Vector3[]
        {
            verts[12], verts[62], verts[66], verts[92], verts[116],
        };

        /*FracObjTris20 = new int[]
        {
           0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris20 = new int[]
      {
             0,3,1,
      };
        FracObjTris12000 = new int[]
        {
             0,2,3,
        };
        FracObjTris12001 = new int[]
        {
             0,1,4,
        };
        FracObjTris12002 = new int[]
        {
             4,2,0,
        };
        FracObjTris12003 = new int[]
        {
             4,1,3,
        };
        FracObjTris12004 = new int[]
        {
             4,3,2,
        };







        FracObjVerts21 = new Vector3[]
        {
            verts[15], verts[63], verts[70], verts[93], verts[117],
        };

        /*FracObjTris21 = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };*/
        FracObjTris21 = new int[]
       {
            1,3,0,
       };
        FracObjTris13000 = new int[]
        {
            3,2,0,
        };
        FracObjTris13001 = new int[]
        {
            4,1,0,
        };
        FracObjTris13002 = new int[]
        {
            0,2,4,
        };
        FracObjTris13003 = new int[]
        {
            3,1,4,
        };
        FracObjTris13004 = new int[]
        {
            2,3,4,
        };



        FracObjVerts22 = new Vector3[]
        {
            verts[13], verts[26], verts[67], verts[94], verts[118],
        };

        /*FracObjTris22 = new int[]
        {
            1,3,0, 3,2,0, 4,1,0, 0,2,4, 3,1,4, 2,3,4,
        };*/
        FracObjTris22 = new int[]
       {
            1,3,0,
       };
        FracObjTris14000 = new int[]
        {
            3,2,0,
        };
        FracObjTris14001 = new int[]
        {
            4,1,0,
        };
        FracObjTris14002 = new int[]
        {
            0,2,4,
        };
        FracObjTris14003 = new int[]
        {
            3,1,4,
        };
        FracObjTris14004 = new int[]
        {
            2,3,4,
        };



        FracObjVerts23 = new Vector3[]
        {
            verts[14], verts[26], verts[71], verts[95], verts[119],
        };

        /*FracObjTris23 = new int[]
        {
            0,3,1, 0,2,3, 0,1,4, 4,2,0, 4,1,3, 4,3,2,
        };*/
        FracObjTris23 = new int[]
      {
             0,3,1,
      };
        FracObjTris15000 = new int[]
        {
             0,2,3,
        };
        FracObjTris15001 = new int[]
        {
             0,1,4,
        };
        FracObjTris15002 = new int[]
        {
             4,2,0,
        };
        FracObjTris15003 = new int[]
        {
             4,1,3,
        };
        FracObjTris15004 = new int[]
        {
             4,3,2,
        };





    }





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
        StartCoroutine("buildFracs");
    }


    IEnumerator buildFracs()
    {

        /////////////FRONT FACE EDGES //////////////
        minPosition0 = verts[0];
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
        maxPosition17 = verts[6];

        //StartCoroutine("getRandomPoints");

        randomPosition0 = new Vector3(Random.Range(minPosition0.x, maxPosition0.x), Random.Range(minPosition0.y, maxPosition0.y), Random.Range(minPosition0.z, maxPosition0.z));
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

        radius = (int)Random.Range((float)0f, (float)0.5f);

        randomPoint = Random.insideUnitSphere * radius + center;

        randomPoints = new Vector3[]
        {
            point0 = randomPoint,
        };

        t1 = new Thread(Func1) { Name = "Thread 1" };
        if (!t1.IsAlive)
        {
            t1.Start();
        }

        DestroyImmediate(gameObject.GetComponent<BoxCollider>());








        for (int i = 0; i < OriginalVerts.Length; i++)
        {
            GameObject singleFrac = new GameObject();

            FracturingObj.Add(singleFrac);

            for (int j = 0; j < FracturingObj.Count; j++)
            {

                FracturingObj[j].transform.position = transform.position;
                FracturingObj[j].transform.rotation = transform.rotation;
                FracturingObj[j].transform.localScale = transform.localScale * 0.90f;
            }
        }
        Material[] m = new Material[]
        {
             Resources.Load("Material/FracColor0") as Material,
             Resources.Load("Material/FracColor0") as Material,
             Resources.Load("Material/FracColor1") as Material,
             Resources.Load("Material/FracColor1") as Material,
             Resources.Load("Material/FracColor1") as Material,
             Resources.Load("Material/FracColor1") as Material,
        };

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
            //fracMesh.vertices = null;
            //fracMesh.triangles = null;
            FracturingObj[i].transform.name = "Fragment" + " " + i;
            fracMesh.name = "Fragment" + " " + i;
            FracturingObj[i].transform.tag = "Fragment";

            if (FracturingObj[i].transform.gameObject.GetComponent<Rigidbody>() == null)
            {
                FracturingObj[i].gameObject.AddComponent<Rigidbody>().mass = 0.01f;//.AddExplosionForce(explosionForce, transform.position, 100f, 1f);
            }









            FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>().sharedMesh = fracMesh;
            //FracturingObj[i].transform.gameObject.GetComponent<Renderer>().material = texture;
            FracturingObj[i].GetComponent<Renderer>().materials = m;
            FracturingObj[i].transform.gameObject.GetComponent<MeshFilter>().mesh.subMeshCount = 6;
        }




        t2 = new Thread(Func2) { Name = "Thread 2" };

        if (!t2.IsAlive)
        {
            t2.Start();
        }

        yield return new WaitForEndOfFrame();
        yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(0.01f);



        FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts0;
        //FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris0;
        FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris0, 0);
        FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris100, 1);
        FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris101, 2);
        FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris102, 3);
        FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris103, 4);
        FracturingObj[0].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris104, 5);









        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts1;
        //FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris1;
        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris1, 0);
        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris200, 1);
        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris201, 2);
        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris202, 3);
        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris203, 4);
        FracturingObj[1].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris204, 5);



        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts2;
        //FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris2;
        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris2, 0);
        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris300, 1);
        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris301, 2);
        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris302, 3);
        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris303, 4);
        FracturingObj[2].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris304, 5);


        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts3;
        //FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris3;
        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris3, 0);
        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris400, 1);
        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris401, 2);
        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris402, 3);
        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris403, 4);
        FracturingObj[3].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris404, 5);


        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts4;
        //FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris4;
        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris4, 0);
        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris500, 1);
        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris501, 2);
        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris502, 3);
        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris503, 4);
        FracturingObj[4].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris504, 5);

        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts5;
        //FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris5;
        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris5, 0);
        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris600, 1);
        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris601, 2);
        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris602, 3);
        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris603, 4);
        FracturingObj[5].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris604, 5);


        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts6;
        //FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris6;
        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris6, 0);
        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris700, 1);
        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris701, 2);
        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris702, 3);
        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris703, 4);
        FracturingObj[6].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris704, 5);

        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts7;
        //FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris7;
        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris7, 0);
        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris800, 1);
        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris801, 2);
        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris802, 3);
        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris803, 4);
        FracturingObj[7].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris804, 5);

        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts8;
        //FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris8;
        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris8, 0);
        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris900, 1);
        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris901, 2);
        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris902, 3);
        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris903, 4);
        FracturingObj[8].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris904, 5);


        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts9;
        //FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris9;
        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris9, 0);
        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris1000, 1);
        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris1001, 2);
        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris1002, 3);
        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris1003, 4);
        FracturingObj[9].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris1004, 5);


        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts10;
        //FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris10;
        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris10, 0);
        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris2000, 1);
        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris2001, 2);
        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris2002, 3);
        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris2003, 4);
        FracturingObj[10].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris2004, 5);

        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts11;
        //FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris11;
        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris11, 0);
        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris3000, 1);
        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris3001, 2);
        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris3002, 3);
        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris3003, 4);
        FracturingObj[11].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris3004, 5);

        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts12;
        //FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris12;
        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris12, 0);
        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris4000, 1);
        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris4001, 2);
        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris4002, 3);
        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris4003, 4);
        FracturingObj[12].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris4004, 5);


        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts13;
        //FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris13;
        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris13, 0);
        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris5000, 1);
        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris5001, 2);
        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris5002, 3);
        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris5003, 4);
        FracturingObj[13].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris5004, 5);


        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts14;
        //FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris14;
        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris14, 0);
        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris6000, 1);
        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris6001, 2);
        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris6002, 3);
        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris6003, 4);
        FracturingObj[14].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris6004, 5);


        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts15;
        //FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris15;
        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris15, 0);
        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris7000, 1);
        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris7001, 2);
        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris7002, 3);
        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris7003, 4);
        FracturingObj[15].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris7004, 5);


        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts16;
        //FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris16;
        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris16, 0);
        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris8000, 1);
        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris8001, 2);
        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris8002, 3);
        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris8003, 4);
        FracturingObj[16].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris8004, 5);


        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts17;
        //FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris17;
        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris17, 0);
        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris9000, 1);
        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris9001, 2);
        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris9002, 3);
        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris9003, 4);
        FracturingObj[17].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris9004, 5);


        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts18;
        //FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris18;
        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris18, 0);
        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris10000, 1);
        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris10001, 2);
        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris10002, 3);
        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris10003, 4);
        FracturingObj[18].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris10004, 5);

        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts19;
        //FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris19;
        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris19, 0);
        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris11000, 1);
        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris11001, 2);
        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris11002, 3);
        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris11003, 4);
        FracturingObj[19].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris11004, 5);


        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts20;
        //FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris20;
        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris20, 0);
        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris12000, 1);
        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris12001, 2);
        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris12002, 3);
        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris12003, 4);
        FracturingObj[20].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris12004, 5);



        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts21;
        //FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris21;
        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris21, 0);
        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris13000, 1);
        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris13001, 2);
        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris13002, 3);
        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris13003, 4);
        FracturingObj[21].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris13004, 5);


        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts22;
        //FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris22;
        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris22, 0);
        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris14000, 1);
        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris14001, 2);
        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris14002, 3);
        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris14003, 4);
        FracturingObj[22].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris14004, 5);

        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.vertices = FracObjVerts23;
        //FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.triangles = FracObjTris23;
        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris23, 0);
        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris15000, 1);
        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris15001, 2);
        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris15002, 3);
        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris15003, 4);
        FracturingObj[23].transform.gameObject.GetComponent<MeshFilter>().mesh.SetTriangles(FracObjTris15004, 5);












        for (int i = 0; i < FracturingObj.Count; i++)
        {
            //to readd if issues
            //FracturingObj[i].transform.gameObject.AddComponent<activateCombine>().enabled = true;
            //to readd if issues

            FracturingObj[i].GetComponent<MeshFilter>().mesh.RecalculateNormals();
            FracturingObj[i].GetComponent<MeshFilter>().mesh.RecalculateBounds();

            for (int j = 0; j < FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices.Length; j++)
            {
                Vector3[] normalz = FracturingObj[i].GetComponent<MeshFilter>().mesh.vertices;
                FracturingObj[i].GetComponent<MeshFilter>().mesh.normals = normalz;
                FracturingObj[i].GetComponent<MeshFilter>().mesh.RecalculateNormals();
            }


            if (FracturingObj[i].transform.gameObject.GetComponent<MeshCollider>() == null)
            {

                MeshCollider meshCol = FracturingObj[i].gameObject.AddComponent<MeshCollider>();
                meshCol.convex = true;

            }

            /*if (FracturingObj[i].transform.gameObject.GetComponent<Rigidbody>() == null)
            {
                FracturingObj[i].gameObject.AddComponent<Rigidbody>().mass = 0.01f;//.AddExplosionForce(explosionForce, transform.position, 100f, 1f);
            }*/

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
            }
            FracturingObj[i].transform.parent = this.transform;

            //to readd if issues
            //FracturingObj[i].transform.gameObject.AddComponent<activateCombine>().enabled = true;
            //to readd if issues


            //GetComponent<reparator>().objToReact.Add(FracturingObj[i]);

            //Destroy(this.transform.gameObject,0.1f);
            mesh.Clear();


        }
        StopCoroutine("buildFracs");
        //




    }















    /*void OnApplicationQuit()
    {
        t1.Abort();
        t2.Abort();
    }

    void OnDestroy()
    {
        t1.Abort();
        t2.Abort();
    }*/


}
