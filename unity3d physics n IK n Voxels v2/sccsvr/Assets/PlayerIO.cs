using UnityEngine;
using System.Collections;


//[RequireComponent (typeof(sccsproceduralplanetbuilderGen2))]
public class PlayerIO : MonoBehaviour
{

    public byte activeBlockType = 1;
    public Transform retAdd, retDel;
    //public Transform  retDel;
    //public Chunk1 chunk;

    Mesh mesh;
    public Transform sphere;
    Vector3 yoMan;
    Vector3 yoMan1;
    public sccsproceduralplanetbuilderGen2 terrain1;




    /*float planeSize= 0.125f; // 0.125f
    float quarter = 8f; //8f;
    int multiplicator = 3;
    int multiplicatorReticle = 5;
    int tileSize = 8;
    int suppressorPos = 8;*/



    float planeSize = 0.1f; // 0.125f
    float quarter = 10; //8f;
    int multiplicator = 1;
    int multiplicatorReticle = 3;
    int tileSize = 10;
    int suppressorPos = 10;






    void Start()
    {
        terrain1 = sccsproceduralplanetbuilderGen2.sccsproceduralplanetbuilderGen2staticscriptlock;// GetComponent<Terrain22>();
        //mesh = GetComponent<MeshFilter>().mesh;
    }



    void Update()
    {
        Ray ray = new Ray(transform.position + transform.forward / 2, transform.forward);
        RaycastHit hit;






        if (Physics.Raycast(ray, out hit, 100f))
        {
            //Debug.DrawRay(hit.point, Vector3.up * 15, Color.green, 0.1f);

            Vector3 p = hit.point - hit.normal / 4;
            float offset = planeSize / 2;
            //float offset = 0;


            float offset2 = planeSize / 2;

            if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0)
            {
                Debug.Log("top side hit");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset * multiplicator, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                //retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Floor(p.y * suppressorPos) / suppressorPos) + offset * multiplicator, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset * multiplicatorReticle, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Floor(p.y * suppressorPos) / suppressorPos) + offset * 5, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);
            }

            if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1)
            {
                Debug.Log("back side hit");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 5);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 5);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
            }

            if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0)
            {
                Debug.Log("right side hit");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
            }


            if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0)
            {
                Debug.Log("left side hit");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
            }

            if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1)
            {
                Debug.Log("front side hit");

                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 3);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 6);

            }


            if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0)
            {
                Debug.Log("bottom side hit");
                //Debug.Log("yo1");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 3, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

            }



            /*if (Input.GetMouseButtonDown(0))
            {
                float x = ((yoMan1.x * 1) / 1) / 1; //WORKING
                float y = ((yoMan1.y * 1) / 1) / 1;//WORKING
                float z = ((yoMan1.z * 1) / 1) / 1;//WORKING
                terrain1.getChunk(x, y, z).SetBrick(x/planeSize, y/ planeSize, z/ planeSize, activeBlockType);

            }


            if (Input.GetMouseButtonDown(1))
            {
                //Debug.Log(hit.normal);
                //Debug.DrawRay(hit.point, Vector3.up * 10, Color.red, 0.1f);

                float x = ((yoMan.x * 1) / 1) / 1; //WORKING
                float y = ((yoMan.y * 1) / 1) / 1;//WORKING
                float z = ((yoMan.z * 1) / 1) / 1;//WORKING

                terrain1.GetChunk(x , y , z ).SetBrick(x /planeSize, y / planeSize, z / planeSize, 0);

            }*/


            //Control();
        }



        /*else
        {
            retAdd.position = new Vector3(0, -100, 0);
            retDel.position = new Vector3(0, -100, 0);
        }*/
        


        /*if (mesh.vertices.Length > 65000)
        {
            map = new byte[width, width, width];

        }*/
 

    }



    protected void Control()
    {
        /*if (Input.GetMouseButtonDown(0))
        {         
            int x = Mathf.RoundToInt(retAdd.position.x);
            int y = Mathf.RoundToInt(retAdd.position.y);
            int z = Mathf.RoundToInt(retAdd.position.z);
            //chunk.SetBrick(x, y, z, activeBlockType);
            terrain1.GetChunk(x, y, z).SetBrick(x, y, z, activeBlockType);

        }*/

        /*if (Input.GetMouseButtonDown(1))
        {
            int x = Mathf.RoundToInt(retDel.position.x);
            int y = Mathf.RoundToInt(retDel.position.y);
            int z = Mathf.RoundToInt(retDel.position.z);
            //chunk.SetBrick(x, y, z, 0);
            terrain1.GetChunk(x, y, z).SetBrick(x, y, z, 0);
            //GetComponent<cubicFrac>().enabled = true;


        }*/

        /*float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel > 0)
        {
            activeBlockType++;
            if (activeBlockType>3) 
            {
                activeBlockType = 1;
            }
        }
        else if (wheel < 0)
        {
            activeBlockType++;
            if (activeBlockType<1)
            {
                activeBlockType = 3;
            }
        }*/


    }



}
/*
 * using UnityEngine;
using System.Collections;


[RequireComponent (typeof(Terrain))]
public class PlayerIO : MonoBehaviour
{

    public byte activeBlockType = 1;
    //public Transform retAdd, retDel;
      public Transform  retDel;
    //public Chunk1 chunk;

    Mesh mesh;
    public Terrain1 terrain1;

    float planeSize= 0.25f;
    float quarter = 8f;
    int chunkSize = 10;

    float quarter2 = 2f;

    public Transform sphere;

    void Start()
    {
        terrain1 = GetComponent<Terrain1>();
        //mesh = GetComponent<MeshFilter>().mesh;
    }



    void Update()
    {
        Ray ray = new Ray(transform.position + transform.forward / 2, transform.forward);
        RaycastHit hit;






        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.DrawRay(hit.point, Vector3.up * 1, Color.red, 0.1f);

            Vector3 p = new Vector3 (hit.point.x *(1), hit.point.y *(1), hit.point.z *( 1));


            Vector3 hitPoint;


            hitPoint.x = (Mathf.Round(hit.point.x * quarter *1) / quarter) ;
            hitPoint.y = (Mathf.Round(hit.point.y * quarter* 1) / quarter) ;
            hitPoint.z = (Mathf.Round(hit.point.z * quarter* 1) / quarter);





            Debug.DrawRay(hitPoint, Vector3.up * 10, Color.green, 0.1f);





            //retDel.position = new Vector3(((p.x)-hit.normal.x/10), ((p.y) - hit.normal.y/ 10), ((p.z) - hit.normal.z/ 10));
            //retDel.position = new Vector3(((Mathf.Round(p.x * quarter) / quarter)  - hit.normal.y / 10), ((Mathf.Round(p.y * quarter) / quarter)  - hit.normal.y / 10) , ((Mathf.Round(p.z * quarter) / quarter) - hit.normal.y / 10) );
            //retDel.position = new Vector3((Mathf.Round(hit.point.x * quarter) / quarter ) - hit.normal.y , (Mathf.Round(hit.point.y * quarter) / quarter ) - hit.normal.y , (Mathf.Round(hit.point.z * quarter) / quarter ) - hit.normal.y);
            //retDel.position = new Vector3((hitPoint.x * quarter) / quarter,(hitPoint.y * quarter) / quarter,(hitPoint.z * quarter) / quarter);

            retDel.position = new Vector3((hitPoint.x -hit.normal.x / 8), (hitPoint.y - hit.normal.y / 8) , (hitPoint.z - hit.normal.z / 8));







            //p = hit.point+hit.normal / 100;
            //retAdd.position = new Vector3((p.x), (p.y), (p.z));



            /*if (Input.GetMouseButtonDown(0))
            {
                float x = (retAdd.position.x);
                float y = (retAdd.position.y);
                float z = (retAdd.position.z);


                //chunk.SetBrick(x, y, z, activeBlockType);
                terrain1.GetChunk(x, y, z).SetBrick(x/planeSize, y/ planeSize, z/ planeSize, activeBlockType);

            }


            if (Input.GetMouseButtonDown(1))
            {




                Debug.DrawRay(hitPoint, Vector3.up* 10, Color.green, 0.1f);
                GameObject yo = (GameObject)Instantiate(sphere, retDel.position, Quaternion.identity);
//float x = ((retDel.position.x) * planeSize) ;
//float y = ((retDel.position.y) * planeSize) ;
//float z = ((retDel.position.z) * planeSize);



//float x = (Mathf.Round(hit.point.x* 1) / 1 * 1);recheck
//float y = (Mathf.Round(hit.point.y* 1) / 1 * 1);recheck
//float z = (Mathf.Round(hit.point.z* 1) / 1 * 1);recheck

//float x = (Mathf.Round(hitPoint.x));recheck
//float y = (Mathf.Round(hitPoint.y));recheck
//float z = (Mathf.Round(hitPoint.z));recheck

//float x = (Mathf.Round(hit.point.x) / planeSize * .75f) * planeSize; NOTHING
//float y = (Mathf.Round(hit.point.y) / planeSize * .75f) * planeSize; NOTHING
//float z = (Mathf.Round(hit.point.z) / planeSize * .75f) * planeSize; NOTHING

//float x = ((hit.point.x / chunkSize) * chunkSize * planeSize) ; recheck
//float y = ((hit.point.y / chunkSize) * chunkSize * planeSize) ;recheck
//float z = ((hit.point.z / chunkSize) * chunkSize * planeSize) ;recheck

float x = ((hit.point.x / 1) * 1 * 1); //WORKING SOSO
float y = ((hit.point.y / 1) * 1 * 1);//WORKING SOSO
float z = ((hit.point.z / 1) * 1 * 1);//WORKING SOSO



//float x = (Mathf.Round(hit.point.x  - hit.normal.x / 2 ));
//float y = (Mathf.Round(hit.point.y  - hit.normal.y / 2) );
//float z = (Mathf.Round(hit.point.z  - hit.normal.z / 2) );

//float x = (Mathf.Round(retDel.position.x * 1) * .5f * .25f);
//float y = (Mathf.Round(retDel.position.y * 1) * .5f * .25f);
//float z = (Mathf.Round(retDel.position.z * 1) * .5f * .25f);

//float x = (Mathf.Round(hit.point.x* 2) / planeSize * planeSize) ; //Working but half
//float y = (Mathf.Round(hit.point.y* 2) / planeSize * planeSize) ;//Working but half
//float z = (Mathf.Round(hit.point.z* 2) / planeSize * planeSize) ;//Working but half


//float x = (Mathf.Round(hit.point.x * 1) / 1 * planeSize);
//float y = (Mathf.Round(hit.point.y * 1) / 1 * planeSize);
//float z = (Mathf.Round(hit.point.z * 1) / 1 * planeSize);











//float x = (Mathf.Round(retDel.position.x / 1) * 1); //A RESULT HERE
//float y = (Mathf.Round(retDel.position.y / 1) * 1); //A RESULT HERE
//float z = (Mathf.Round(retDel.position.z / 1) * 1); //A RESULT HERE

//float x = ((retDel.position.x * chunkSize) / chunkSize * planeSize);
//float y = ((retDel.position.y * chunkSize) / chunkSize * planeSize);
//float z = ((retDel.position.z * chunkSize) / chunkSize * planeSize);

//float x = ((retDel.position.x * 1) / 1 * 1);
//float y = ((retDel.position.y * 1) / 1 * 1);
//float z = ((retDel.position.z * 1) / 1 * 1);

//float x = (Mathf.Round(retDel.position.x / (5 * 1)) * 1* 1); //A RESULT HERE
//float y = (Mathf.Round(retDel.position.y / (5 * 1)) * 1 * 1); //A RESULT HERE
//float z = (Mathf.Round(retDel.position.z / (5 * 1)) * 1* 1); //A RESULT HERE



float x1 = (Mathf.Round(hit.point.x * quarter2) / quarter2);
float y1 = (Mathf.Round(hit.point.y * quarter2) / quarter2);
float z1 = (Mathf.Round(hit.point.z * quarter2) / quarter2);















//float x1 = (Mathf.Round(hit.point.x / planeSize) * planeSize * planeSize);
//float y1 = (Mathf.Round(hit.point.y / planeSize) * planeSize * planeSize) ;
//float z1 = (Mathf.Round(hit.point.z / planeSize) * planeSize * planeSize);











//chunk.SetBrick(x, y, z, 0);
terrain1.GetChunk(x1,y1,z1).SetBrick(x/(1 * 1), y / (1 * 1), z / (1 * 1), 0);
                //GetComponent<cubicFrac>().enabled = true;


            }


            //Control();
        }



        /*else
        {
            retAdd.position = new Vector3(0, -100, 0);
            retDel.position = new Vector3(0, -100, 0);
        }*/
        


        /*if (mesh.vertices.Length > 65000)
        {
            map = new byte[width, width, width];

        }
 

    }



    protected void Control()
{
    /*if (Input.GetMouseButtonDown(0))
    {         
        int x = Mathf.RoundToInt(retAdd.position.x);
        int y = Mathf.RoundToInt(retAdd.position.y);
        int z = Mathf.RoundToInt(retAdd.position.z);
        //chunk.SetBrick(x, y, z, activeBlockType);
        terrain1.GetChunk(x, y, z).SetBrick(x, y, z, activeBlockType);

    }*/

    /*if (Input.GetMouseButtonDown(1))
    {
        int x = Mathf.RoundToInt(retDel.position.x);
        int y = Mathf.RoundToInt(retDel.position.y);
        int z = Mathf.RoundToInt(retDel.position.z);
        //chunk.SetBrick(x, y, z, 0);
        terrain1.GetChunk(x, y, z).SetBrick(x, y, z, 0);
        //GetComponent<cubicFrac>().enabled = true;


    }*/

    /*float wheel = Input.GetAxis("Mouse ScrollWheel");
    if (wheel > 0)
    {
        activeBlockType++;
        if (activeBlockType>3) 
        {
            activeBlockType = 1;
        }
    }
    else if (wheel < 0)
    {
        activeBlockType++;
        if (activeBlockType<1)
        {
            activeBlockType = 3;
        }
    }


}



}*/
