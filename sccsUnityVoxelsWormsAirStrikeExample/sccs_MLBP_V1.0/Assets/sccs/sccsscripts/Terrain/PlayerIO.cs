using UnityEngine;
using System.Collections;


public class PlayerIO : MonoBehaviour
{
    
    public byte activeBlockType = 1;
    public Transform retAdd, retDel;
    //public Transform  retDel;
    //public Chunk1 chunk;

    Mesh mesh;
    public Terrain22 terrain22;

    float planeSize = 0.0625f;
    //float planeSize = 1f;




    float quarter = 1;

    public Transform sphere;
    public Vector3 yoMan;
    public Vector3 yoMan1;

    int multiplicator = 3;
    int multiplicatorReticle = 5;


    //int multiplicator = 1;
    //int multiplicatorReticle = 1;

    int tileSize = 16;
    int suppressorPos = 16;

    //int tileSize = 1;
    //int suppressorPos = 1;




    public Transform rayMod;

    public int viewSize1;

    public Vector3 BackupVector;


    void Start()
    {
        //terrain22 = GetComponent<Terrain22>();
        //mesh = GetComponent<MeshFilter>().mesh;

        retAdd.localScale = retAdd.localScale * planeSize;
        retDel.localScale = retDel.localScale * planeSize;

    }



    void Update()
    {
        //Ray ray = new Ray(rayMod.position + rayMod.forward , rayMod.forward);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0));

        


        //Debug.DrawRay(transform.position, Camera.main.(transform.position).direction, Color.red, 1000f);


        if (Physics.Raycast(ray, out hit, 100f))
        {

            //Debug.DrawRay(hit.point, Vector3.up * 10, Color.red,1000f);
            //Debug.Log("yo");





            Vector3 p = hit.point - hit.normal / 8;
            float offset = planeSize / 2;
            //float offset = 0;


            float offset2 = planeSize / 2;




            /////////////////////TOPFACE/////////////////////////
           if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0)
            {
               // Debug.Log("yo0");

                retDel.position = new Vector3((Mathf.Floor(p.x*tileSize )/tileSize ) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset * multiplicator, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);           
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset * multiplicator, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset+ offset + offset * multiplicator, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset  + offset * multiplicator, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
              
            }



                //////////////////////FRONT FACE///////////////////////////////
                if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1)
            {
                //Debug.Log("yo1");

                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset*3);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset*-2);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 5);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) -offset * 3);
            }

        
            /////////////////////RIGHT FACE////////////////////////////////
            if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0)
            {
                //Debug.Log("yo2");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset*multiplicator , (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * multiplicator, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset+ offset+ offset * multiplicator, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) +offset + offset + offset * multiplicator, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
            }

            //////////////////////LEFT FACE////////////////////////////////
            if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0)
            {
                //Debug.Log("yo3");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset*-multiplicator , (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * -multiplicator, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
            }

            ////////////////////BACK FACE////////////////////////////////
            if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1)
            {
                //Debug.Log("yo4");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * -multiplicator);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * multiplicator);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset +offset * multiplicator);
            }
            
            /*/////////////////////BOTTOM FACE/////////////////////////////////
            if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0)
            {
                //Debug.Log("yo5");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 1, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 1, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 1, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 1, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

            }*/


            ///////ADD BLOCK/////////////
            if (Input.GetMouseButtonDown(0))
            {
                float x = (yoMan1.x); //WORKING
                float y = (yoMan1.y);//WORKING
                float z = (yoMan1.z);//WORKING
                //Debug.Log(Terrain22.GetChunk(x, y, z ).transform.position);
                //Debug.Log(Terrain22.GetChunk(x, y, z).GetByte((int)x, (int)y, (int)z));
                




                //Terrain22.GetChunk(x, y, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, activeBlockType);
                /*Terrain22.GetChunk(x, y+planeSize, z).SetBrick(x / planeSize, (y) / planeSize  , z / planeSize, activeBlockType);
                Terrain22.GetChunk(x, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, activeBlockType);


                Terrain22.GetChunk(x - planeSize, y + planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x + planeSize, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);






                Terrain22.GetChunk(x + planeSize, y + planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x - planeSize, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);

                Terrain22.GetChunk(x + planeSize, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x - planeSize, y + planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);




                Terrain22.GetChunk(x, y + planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);


                Terrain22.GetChunk(x, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x, y + planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);



                Terrain22.GetChunk(x + planeSize, y + planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x - planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x + planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x - planeSize, y + planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);















                Terrain22.GetChunk(x - planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);
                Terrain22.GetChunk(x + planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);
                Terrain22.GetChunk(x, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);
                Terrain22.GetChunk(x, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);
                Terrain22.GetChunk(x + planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);
                Terrain22.GetChunk(x - planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);
                Terrain22.GetChunk(x - planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);
                Terrain22.GetChunk(x + planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);*/
            }


            ///////DELETE BLOCK///////////
            if (Input.GetMouseButtonDown(1))
            {


                float x = (yoMan.x); //WORKING
                float y = (yoMan.y);//WORKING
                float z = (yoMan.z);//WORKING

                //Debug.Log(Terrain22.GetChunkUnder(x, y-planeSize, z).transform.position);
                //Debug.Log(Terrain22.GetChunk(x, y , z).transform.position);
                //Debug.Log(Terrain22.GetChunkUnder(x, y - planeSize, z).GetByte((int)x, (int)y, (int)z));
                //Debug.Log(Terrain22.GetChunk(x, y-planeSize , z).transform.position);


                /*
                Chunk11 chunk3 = Terrain22.GetChunk(x, y-planeSize , z);

                //Debug.Log(Terrain22.GetChunk(x, y - planeSize, z).transform.position);
                chunk3.chunkBelow = chunk3;
                chunk3.yoMan = new Vector3(yoMan.x, yoMan.y , yoMan.z );

                /*if (Terrain22.GetChunk(x, y - planeSize, z) != null)
                {
                    Terrain22.GetChunk(x, y - planeSize, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }*/
                /*
                if (Terrain22.GetChunk(x, y , z) != null)
                {
                    Terrain22.GetChunk(x, y , z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }*/
 

                /*if (Terrain22.GetChunk(x, y - planeSize - planeSize, z) != null)
                {
                    Terrain22.GetChunk(x, y - planeSize - planeSize, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }*/


                /*if (Terrain22.GetChunkUnder(x, y - planeSize, z) != null)
                {
                    Terrain22.GetChunkUnder(x, y - planeSize, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }*/

                /*if (Terrain22.GetChunkUnder(x, y - planeSize - planeSize, z) != null)
                {
                    Terrain22.GetChunkUnder(x, y - planeSize - planeSize, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }*/














                /*if (Terrain22.GetChunkUnder(x, y, z) != null)
                {
                    Terrain22.GetChunkUnder(x, y, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }




                 if (Terrain22.GetChunkUnder(x + planeSize, y, z) != null)
                 {
                     Terrain22.GetChunkUnder(x + planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                 }


                 if (Terrain22.GetChunkUnder(x - planeSize, y, z) != null)
                 {
                     Terrain22.GetChunkUnder(x - planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                 }

                 if (Terrain22.GetChunkUnder(x, y, z + planeSize) != null)
                 {
                     Terrain22.GetChunkUnder(x, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                 }

                 if (Terrain22.GetChunkUnder(x, y, z - planeSize) != null)
                 {
                     Terrain22.GetChunkUnder(x, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                 }

                 if (Terrain22.GetChunkUnder(x + planeSize, y - planeSize, z - planeSize) != null)
                 {
                     Terrain22.GetChunkUnder(x + planeSize, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                 }

                 if (Terrain22.GetChunkUnder(x - planeSize, y - planeSize, z + planeSize) != null)
                 {
                     Terrain22.GetChunkUnder(x - planeSize, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                 }

                 if (Terrain22.GetChunkUnder(x - planeSize, y - planeSize, z - planeSize) != null)
                 {
                     Terrain22.GetChunkUnder(x - planeSize, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                 }










                 if (Terrain22.GetChunk( x, y - planeSize, z) != null)
                 {
                     Terrain22.GetChunk(x, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                 }



                 if (Terrain22.GetChunk(x + planeSize, y - planeSize, z - planeSize) != null)
                 {
                     Terrain22.GetChunk(x + planeSize, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                 }*/

                /*if (Terrain22.GetChunk(x, y - planeSize, z + planeSize) != null)
                {
                    Terrain22.GetChunk(x, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunk(x, y - planeSize, z - planeSize) != null)
                {
                    Terrain22.GetChunk(x, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunk(x - planeSize, y - planeSize, z) != null)
                {
                    Terrain22.GetChunk(x - planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunk(x + planeSize, y - planeSize, z) != null)
                {
                    Terrain22.GetChunk(x + planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunk(x - planeSize, y, z) != null)
                {
                    Terrain22.GetChunk(x - planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }




                if (Terrain22.GetChunk(x + planeSize, y, z) != null)
                {
                    Terrain22.GetChunk(x + planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunk(x, y, z + planeSize) != null)
                {
                    Terrain22.GetChunk(x, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }



                if (Terrain22.GetChunk(x, y, z - planeSize) != null)
                {
                    Terrain22.GetChunk(x, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunk(x + planeSize, y, z + planeSize) != null)
                {
                    Terrain22.GetChunk(x + planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunk(x - planeSize, y, z - planeSize) != null)
                {
                    Terrain22.GetChunk(x - planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunk(x - planeSize, y, z + planeSize) != null)
                {
                    Terrain22.GetChunk(x - planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunk(x + planeSize, y, z - planeSize) != null)
                {
                    Terrain22.GetChunk(x + planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }*/



                ///////////////////////////////////////////////////////////////////////////





                /*if (Terrain22.GetChunkUnder(x, y - planeSize, z) != null)
                {
                    Terrain22.GetChunkUnder(x, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x - planeSize, y - planeSize, z + planeSize) != null)
                {
                    Terrain22.GetChunkUnder(x - planeSize, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x + planeSize, y - planeSize, z - planeSize) != null)
                {
                    Terrain22.GetChunkUnder(x + planeSize, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x, y - planeSize, z + planeSize) != null)
                {
                    Terrain22.GetChunkUnder(x, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x, y - planeSize, z - planeSize) != null)
                {
                    Terrain22.GetChunkUnder(x, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunkUnder(x - planeSize, y - planeSize, z) != null)
                {
                    Terrain22.GetChunkUnder(x - planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x + planeSize, y - planeSize, z) != null)
                {
                    Terrain22.GetChunkUnder(x + planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x - planeSize, y, z) != null)
                {
                    Terrain22.GetChunkUnder(x - planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x + planeSize, y, z) != null)
                {
                    Terrain22.GetChunkUnder(x + planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x, y, z + planeSize) != null)
                {
                    Terrain22.GetChunkUnder(x, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunkUnder(x, y, z - planeSize) != null)
                {
                    Terrain22.GetChunkUnder(x, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x + planeSize, y, z + planeSize) != null)
                {
                    Terrain22.GetChunkUnder(x + planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x - planeSize, y, z - planeSize) != null)
                {
                    Terrain22.GetChunkUnder(x - planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkUnder(x - planeSize, y, z + planeSize) != null)
                {
                    Terrain22.GetChunkUnder(x - planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunkUnder(x + planeSize, y, z - planeSize) != null)
                {
                    Terrain22.GetChunkUnder(x + planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }*/


                //////////////////////////////////////////////////




                /*if (Terrain22.GetChunkWhatever(x, y - planeSize, z) != null)
                {
                    Terrain22.GetChunkWhatever(x, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x - planeSize, y - planeSize, z + planeSize) != null)
                {
                    Terrain22.GetChunkWhatever(x - planeSize, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x + planeSize, y - planeSize, z - planeSize) != null)
                {
                    Terrain22.GetChunkWhatever(x + planeSize, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x, y - planeSize, z + planeSize) != null)
                {
                    Terrain22.GetChunkWhatever(x, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x, y - planeSize, z - planeSize) != null)
                {
                    Terrain22.GetChunkWhatever(x, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunkWhatever(x - planeSize, y - planeSize, z) != null)
                {
                    Terrain22.GetChunkWhatever(x - planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x + planeSize, y - planeSize, z) != null)
                {
                    Terrain22.GetChunkWhatever(x + planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x - planeSize, y, z) != null)
                {
                    Terrain22.GetChunkWhatever(x - planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x + planeSize, y, z) != null)
                {
                    Terrain22.GetChunkWhatever(x + planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x, y, z + planeSize) != null)
                {
                    Terrain22.GetChunkWhatever(x, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunkWhatever(x, y, z - planeSize) != null)
                {
                    Terrain22.GetChunkWhatever(x, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x + planeSize, y, z + planeSize) != null)
                {
                    Terrain22.GetChunkWhatever(x + planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x - planeSize, y, z - planeSize) != null)
                {
                    Terrain22.GetChunkWhatever(x - planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever(x - planeSize, y, z + planeSize) != null)
                {
                    Terrain22.GetChunkWhatever(x - planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunkWhatever(x + planeSize, y, z - planeSize) != null)
                {
                    Terrain22.GetChunkWhatever(x + planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }*/



                /////////////////////////////////////////////////////////////

                /* if (Terrain22.GetChunkWhatever1(x, y - planeSize, z) != null)
                     {
                         Terrain22.GetChunkWhatever1(x, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x - planeSize, y - planeSize, z + planeSize) != null)
                     {
                         Terrain22.GetChunkWhatever1(x - planeSize, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x + planeSize, y - planeSize, z - planeSize) != null)
                     {
                         Terrain22.GetChunkWhatever1(x + planeSize, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x, y - planeSize, z + planeSize) != null)
                     {
                         Terrain22.GetChunkWhatever1(x, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x, y - planeSize, z - planeSize) != null)
                     {
                         Terrain22.GetChunkWhatever1(x, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                     }


                     if (Terrain22.GetChunkWhatever1(x - planeSize, y - planeSize, z) != null)
                     {
                         Terrain22.GetChunkWhatever1(x - planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x + planeSize, y - planeSize, z) != null)
                     {
                         Terrain22.GetChunkWhatever1(x + planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x - planeSize, y, z) != null)
                     {
                         Terrain22.GetChunkWhatever1(x - planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x + planeSize, y, z) != null)
                     {
                         Terrain22.GetChunkWhatever1(x + planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x, y, z + planeSize) != null)
                     {
                         Terrain22.GetChunkWhatever1(x, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                     }


                     if (Terrain22.GetChunkWhatever1(x, y, z - planeSize) != null)
                     {
                         Terrain22.GetChunkWhatever1(x, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x + planeSize, y, z + planeSize) != null)
                     {
                         Terrain22.GetChunkWhatever1(x + planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x - planeSize, y, z - planeSize) != null)
                     {
                         Terrain22.GetChunkWhatever1(x - planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                     }

                     if (Terrain22.GetChunkWhatever1(x - planeSize, y, z + planeSize) != null)
                     {
                         Terrain22.GetChunkWhatever1(x - planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                     }


                     if (Terrain22.GetChunkWhatever1(x + planeSize, y, z - planeSize) != null)
                     {
                         Terrain22.GetChunkWhatever1(x + planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                     }*/





                ///////////////////////////////////////






                /*if (Terrain22.GetChunkWhatever2(x, y - planeSize, z) != null)
                {
                    Terrain22.GetChunkWhatever2(x, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x - planeSize, y - planeSize, z + planeSize) != null)
                {
                    Terrain22.GetChunkWhatever2(x - planeSize, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x + planeSize, y - planeSize, z - planeSize) != null)
                {
                    Terrain22.GetChunkWhatever2(x + planeSize, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x, y - planeSize, z + planeSize) != null)
                {
                    Terrain22.GetChunkWhatever2(x, y - planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x, y - planeSize, z - planeSize) != null)
                {
                    Terrain22.GetChunkWhatever2(x, y - planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunkWhatever2(x - planeSize, y - planeSize, z) != null)
                {
                    Terrain22.GetChunkWhatever2(x - planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x + planeSize, y - planeSize, z) != null)
                {
                    Terrain22.GetChunkWhatever2(x + planeSize, y - planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x - planeSize, y, z) != null)
                {
                    Terrain22.GetChunkWhatever2(x - planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x + planeSize, y, z) != null)
                {
                    Terrain22.GetChunkWhatever2(x + planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x, y, z + planeSize) != null)
                {
                    Terrain22.GetChunkWhatever2(x, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunkWhatever2(x, y, z - planeSize) != null)
                {
                    Terrain22.GetChunkWhatever2(x, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x + planeSize, y, z + planeSize) != null)
                {
                    Terrain22.GetChunkWhatever2(x + planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x - planeSize, y, z - planeSize) != null)
                {
                    Terrain22.GetChunkWhatever2(x - planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }

                if (Terrain22.GetChunkWhatever2(x - planeSize, y, z + planeSize) != null)
                {
                    Terrain22.GetChunkWhatever2(x - planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }


                if (Terrain22.GetChunkWhatever2(x + planeSize, y, z - planeSize) != null)
                {
                    Terrain22.GetChunkWhatever2(x + planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                }*/







                //Terrain22.GetChunk(x, y + planeSize, z).SetBrick(x / planeSize, (y) / planeSize  , z / planeSize, 0);
                //Terrain22.GetChunk(x + planeSize, y + planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                //Terrain22.GetChunk(x - planeSize, y + planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                //Terrain22.GetChunk(x, y + planeSize, z - planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                //Terrain22.GetChunk(x, y + planeSize, z + planeSize).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);
                //Terrain22.GetChunk(x + planeSize, y + planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);      
                //Terrain22.GetChunk(x - planeSize, y + planeSize, z).SetBrick(x / planeSize, (y) / planeSize, z / planeSize, 0);









            }





















            /*if (Input.GetKeyDown("t"))
            {
                float x = (yoMan1.x); //WORKING
                float y = (yoMan1.y);//WORKING
                float z = (yoMan1.z);//WORKING
                //Debug.Log(Terrain22.GetChunk(x, y , z ).transform.position);

                Terrain22.GetChunk(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x - planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x + planeSize, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x + planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x - planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x - planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x + planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);

               
                Terrain22.GetChunk(x - planeSize, y, z).SetBrick(x / planeSize + planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x + planeSize, y, z).SetBrick(x / planeSize + planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x, y, z + planeSize).SetBrick(x / planeSize + planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x, y, z - planeSize).SetBrick(x / planeSize + planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x + planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x - planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x - planeSize, y, z + planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
                Terrain22.GetChunk(x + planeSize, y, z - planeSize).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
      

            }*/




















        }


        /*else
        {
            retAdd.position = new Vector3(0, -100, 0);
            retDel.position = new Vector3(0, -100, 0);
        }*/



    }



    /*protected void Control()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int x = Mathf.RoundToInt(retAdd.position.x);
            int y = Mathf.RoundToInt(retAdd.position.y);
            int z = Mathf.RoundToInt(retAdd.position.z);
            terrain22.GetChunk(x, y, z).SetBrick(x, y, z, activeBlockType);
        }

        if (Input.GetMouseButtonDown(1))
        {
            int x = Mathf.RoundToInt(retDel.position.x);
            int y = Mathf.RoundToInt(retDel.position.y);
            int z = Mathf.RoundToInt(retDel.position.z);
            terrain22.GetChunk(x, y, z).SetBrick(x, y, z, 0);
        }
    }*/
}




/*using UnityEngine;
using System.Collections;


public class PlayerIO : MonoBehaviour
{

    public byte activeBlockType = 1;
    public Transform retAdd, retDel;
    //public Transform  retDel;
    //public Chunk1 chunk;

    Mesh mesh;
    public Terrain22 terrain22;

    float planeSize = 0.125f;
    float quarter = 8f;

    public Transform sphere;
    Vector3 yoMan;
    Vector3 yoMan1;
    int multiplicator = 3;
    int multiplicatorReticle = 5;


    int tileSize = 8;
    int suppressorPos = 8;




    void Start()
    {
        terrain22 = GetComponent<Terrain22>();
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
                //Debug.Log("yo0");

                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset * multiplicator, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                //retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Floor(p.y * suppressorPos) / suppressorPos) + offset * multiplicator, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset * multiplicatorReticle, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Floor(p.y * suppressorPos) / suppressorPos) + offset * 5, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);
            }

            if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1)
            {
                //Debug.Log("yo1");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 5);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
            }

            if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0)
            {
                //Debug.Log("yo2");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
            }


            if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0)
            {
                //Debug.Log("yo2");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
            }

            if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1)
            {
                //Debug.Log("yo1");

                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 3);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 6);

            }


            if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0)
            {
                //Debug.Log("yo1");
                retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 3, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

            }



            if (Input.GetMouseButtonDown(0))
            {
                float x = ((yoMan1.x * 1) / 1) / 1; //WORKING
                float y = ((yoMan1.y * 1) / 1) / 1;//WORKING
                float z = ((yoMan1.z * 1) / 1) / 1;//WORKING
                //terrain1.GetChunk(x, y, z).SetBrick(x/planeSize, y/ planeSize, z/ planeSize, activeBlockType);

            }


            if (Input.GetMouseButtonDown(1))
            {
                //Debug.Log(hit.normal);
                //Debug.DrawRay(hit.point, Vector3.up * 10, Color.red, 0.1f);

                float x = ((yoMan.x * 1) / 1) / 1; //WORKING
                float y = ((yoMan.y * 1) / 1) / 1;//WORKING
                float z = ((yoMan.z * 1) / 1) / 1;//WORKING

                //terrain1.GetChunk(x , y , z ).SetBrick(x /planeSize, y / planeSize, z / planeSize, 0);



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

        }*/

