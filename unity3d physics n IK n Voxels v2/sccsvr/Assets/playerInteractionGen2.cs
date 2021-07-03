using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class playerInteractionGen2 : MonoBehaviour
{
    int addfracturedcubeonimpact = 0;


    int someswtc = 0;
    Ray someray;
    Vector3 lastFrameRayPos = Vector3.zero;
    Vector3 lastFrameRayDirForward = Vector3.zero;
    Vector3 lastFrameRayInitDirUp = Vector3.zero;
    int lastFrameRayPosSwtc = 0;
    Vector3 currentRayPosition = Vector3.zero;

    int currentFrameRayPosSwtc = 0;

    sccsInstancesunitypool.instancedata instancedata;
    public GameObject somePointer0;
    GameObject somePointer1;
    GameObject somePointer2;
    RaycastHit hit;

    public Transform pickaxetiptransform;

    public float raycounterLoopMax = 20;

    int InitcounterForIkFootPlacement = 0;
    public int InitcounterForIkFootPlacementMax = 10;
    int InitcounterForIkFootPlacementSwtc = 0;
    float raylength = 0;
    float raycounterSwtc = 0;

    int counterForByteChangeMax = 1;

    public Transform upperleg;
    public Transform lowerleg;
    public Transform foot;
    //public Transform footTarget;
    public Transform legstaticpivot;

    float upperleglength = 0;
    float lowerleglength = 0;
    float footlength = 0;
    float totallegLength = 0;

    public LayerMask layerMask;

    Vector3 IdleStandingTargetPositionVariableLength;
    Vector3 IdleStandingTargetPositionMax;
    Vector3 IdleStandingTargetPositionMin;
    RaycastHit spherecasthit;
    public int swtcForTypeOfInteract = 0;
    //0 == 
    Vector3 thepos;
    public Transform footTarget;


    public Transform planetmanager;

    public byte activeBlockType = 0;
    public Transform retAdd, retDel;

    Mesh mesh;

    public Transform sphere;
    Vector3 yoMan;
    Vector3 yoMan1;

    float diameter;
    float fraction;
    float radius;
    float whatever;

    int roundedX;
    int roundedY;
    int roundedZ;

    private Camera cam;

    /*float planeSize = 0.25f;
    int multiplicator = 1;
    int multiplicatorReticle = 3;
    int realplanetwidth = 4;
    int suppressorPos = 4;*/

    float planeSize = 0.1f;
    int multiplicator = 1;
    int multiplicatorReticle = 3;
    public int realplanetwidth = 2;
    int suppressorPos = 1;
    public int chunkWidth = 20;
    int counterForByteChange = 0;

    Stopwatch stopwatch = new Stopwatch();

    public Material hitmaterial;


    void Start()
    {


        somePointer1 = Instantiate(somePointer0, this.transform.position, Quaternion.identity);
        somePointer2 = Instantiate(somePointer0, this.transform.position, Quaternion.identity);

        if (swtcForTypeOfInteract == 2)
        {
            upperleglength = upperleg.localScale.z;
            lowerleglength = lowerleg.localScale.z;
            footlength = foot.localScale.z;
            totallegLength = upperleglength + lowerleglength + footlength;

            IdleStandingTargetPositionMax = transform.position + ((transform.forward * upperleglength) + (transform.forward * lowerleglength) + (transform.forward * footlength));
            IdleStandingTargetPositionMin = transform.position + ((transform.forward * (upperleglength)) + (transform.forward * (lowerleglength)) + (transform.forward * (footlength)) * 0.5f);

        }

        //retAdd.localScale = retAdd.localScale * planeSize;
        //retDel.localScale = retDel.localScale * planeSize;
        fraction = 1 / planeSize;
        radius = planeSize / 2;
        diameter = 0.1f;
        whatever = 1 / (diameter * 2);
        cam = Camera.main;
        stopwatch.Start();
        tippickaxestopwatch.Start();


        lastFrameRayInitDirUp = pickaxetiptransform.transform.up;
        lastFrameRayDirForward = pickaxetiptransform.transform.forward;
        lastFrameRayPos = pickaxetiptransform.position;
        currentRayPosition = footTarget.transform.position;
    }

    int ontriggerstaycounter = 0;
    int ontriggerstaycounterMax = 50;
    int ontriggerstaycounterSwtc = 0;

    int ontriggerentercounter = 0;
    int ontriggerentercounterMax = 50;
    int ontriggerentercounterSwtc = 0;

    int ontriggerexitcounter = 0;
    int ontriggerexitcounterMax = 50;
    int ontriggerexitcounterSwtc = 0;


    private void OnTriggerStayNot(Collider other)
    {

        //Debug.Log("object hit OnTriggerEnter:" + other.transform.name);

        if (ontriggerstaycounter >= ontriggerstaycounterMax)
        {


            if (GetComponent<Fracture4>() != null)
            {

            }
            else
            {
                var closestPointOnBounds = other.ClosestPointOnBounds(transform.position);

                //retAdd.transform.position = closestPointOnBounds;


                float posx = (closestPointOnBounds.x);
                float posy = (closestPointOnBounds.y);
                float posz = (closestPointOnBounds.z);


                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                //retAdd.position = chunkbytepos;

                int indexX = 0;
                int indexY = 0;
                int indexZ = 0;


                if (posx < 0)
                {
                    posx *= -1;
                }

                if (posx >= 1.0f)
                {
                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                }
                else
                {
                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                }



                if (posy < 0)
                {
                    posy *= -1;
                }

                if (posy >= 1.0f)
                {
                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                }
                else
                {
                    //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                    indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                }


                if (posz < 0)
                {
                    posz *= -1;
                }


                //Debug.Log("posz:" + posz);
                if (posz >= 1.0f)
                {

                    indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                }
                else
                {
                    indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                }

                if (indexX < 0)
                {
                    indexX *= -1;
                }


                if (indexY < 0)
                {
                    indexY *= -1;
                }

                if (indexZ < 0)
                {
                    indexZ *= -1;
                    indexZ = chunkWidth - indexZ;
                }


                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                mainChunkFinal currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)other.transform.position.x, (int)other.transform.position.y, (int)other.transform.position.z);

                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

            }
            ontriggerstaycounter = 0;
        }
        ontriggerstaycounter++;
    }

    private void OnTriggerExitNot(Collider other)
    {

        //Debug.Log("object hit OnTriggerEnter:" + other.transform.name);

        if (ontriggerexitcounter >= ontriggerexitcounterMax)
        {


            if (GetComponent<Fracture4>() != null)
            {

            }
            else
            {
                var closestPointOnBounds = other.ClosestPointOnBounds(transform.position);

                retAdd.transform.position = closestPointOnBounds;


                float posx = (closestPointOnBounds.x);
                float posy = (closestPointOnBounds.y);
                float posz = (closestPointOnBounds.z);


                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                retAdd.position = chunkbytepos;

                int indexX = 0;
                int indexY = 0;
                int indexZ = 0;


                if (posx < 0)
                {
                    posx *= -1;
                }

                if (posx >= 1.0f)
                {
                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                }
                else
                {
                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                }



                if (posy < 0)
                {
                    posy *= -1;
                }

                if (posy >= 1.0f)
                {
                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                }
                else
                {
                    //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                    indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                }


                if (posz < 0)
                {
                    posz *= -1;
                }


                //Debug.Log("posz:" + posz);
                if (posz >= 1.0f)
                {

                    indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                }
                else
                {
                    indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                }

                if (indexX < 0)
                {
                    indexX *= -1;
                }


                if (indexY < 0)
                {
                    indexY *= -1;
                }

                if (indexZ < 0)
                {
                    indexZ *= -1;
                    indexZ = chunkWidth - indexZ;
                }


                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                mainChunkFinal currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)other.transform.position.x, (int)other.transform.position.y, (int)other.transform.position.z);

                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

            }
            ontriggerexitcounter = 0;
        }
        ontriggerexitcounter++;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("object hit OnTriggerEnter:" + other.transform.name);

        if (ontriggerentercounter >= ontriggerentercounterMax)
        {


            if (GetComponent<Fracture4>() != null)
            {

            }
            else
            {
                var closestPointOnBounds = other.ClosestPointOnBounds(transform.position);

                //retAdd.transform.position = closestPointOnBounds;


                float posx = (closestPointOnBounds.x);
                float posy = (closestPointOnBounds.y);
                float posz = (closestPointOnBounds.z);


                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                //retAdd.position = chunkbytepos;

                int indexX = 0;
                int indexY = 0;
                int indexZ = 0;


                if (posx < 0)
                {
                    posx *= -1;
                }

                if (posx >= 1.0f)
                {
                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                }
                else
                {
                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                }



                if (posy < 0)
                {
                    posy *= -1;
                }

                if (posy >= 1.0f)
                {
                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                }
                else
                {
                    //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                    indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                }


                if (posz < 0)
                {
                    posz *= -1;
                }


                //Debug.Log("posz:" + posz);
                if (posz >= 1.0f)
                {

                    indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                }
                else
                {
                    indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                }

                if (indexX < 0)
                {
                    indexX *= -1;
                }


                if (indexY < 0)
                {
                    indexY *= -1;
                }

                if (indexZ < 0)
                {
                    indexZ *= -1;
                    indexZ = chunkWidth - indexZ;
                }


                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                mainChunkFinal currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)other.transform.position.x, (int)other.transform.position.y, (int)other.transform.position.z);

                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

            }
            ontriggerentercounter = 0;
        }
        ontriggerentercounter++;
    }





    void OnGUI()
    {
        /*Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();*/
    }


    private void OnCollisionEnterNot(Collision collision)
    {
        Debug.Log("object hit OnCollisionEnter:" + collision.transform.name);

        if (GetComponent<Fracture4>() != null)
        {

        }
        else
        {
            var closestPointOnBounds = collision.contacts[0].point;// (transform.position);

            //retAdd.transform.position = closestPointOnBounds;


            float posx = (closestPointOnBounds.x);
            float posy = (closestPointOnBounds.y);
            float posz = (closestPointOnBounds.z);


            Vector3 chunkbytepos = new Vector3(posx, posy, posz);

            //retAdd.position = chunkbytepos;

            int indexX = 0;
            int indexY = 0;
            int indexZ = 0;


            if (posx < 0)
            {
                posx *= -1;
            }

            if (posx >= 1.0f)
            {
                indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
            }
            else
            {
                indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
            }



            if (posy < 0)
            {
                posy *= -1;
            }

            if (posy >= 1.0f)
            {
                indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
            }
            else
            {
                //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
            }


            if (posz < 0)
            {
                posz *= -1;
            }


            //Debug.Log("posz:" + posz);
            if (posz >= 1.0f)
            {

                indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
            }
            else
            {
                indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
            }

            if (indexX < 0)
            {
                indexX *= -1;
            }


            if (indexY < 0)
            {
                indexY *= -1;
            }

            if (indexZ < 0)
            {
                indexZ *= -1;
                indexZ = chunkWidth - indexZ;
            }


            ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
            ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

            mainChunkFinal currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z);

            currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

            currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
            currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

        }
    }




    /*
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("object hit:" + other.transform.name);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //var closestPointOnBounds = other.ClosestPointOnBounds(transform.position);


        //////Debug.Log("col:" + other.transform.name);
        var sccsfracturescript = other.transform.gameObject.GetComponent<Fracture4>();

        if (sccsfracturescript != null)
        {
            sccsfracturescript.enabled = true;
        }

        //Ray ray = new Ray(transform.position, transform.forward);// Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.forward * 25, Color.green, 0.001f);

        //var someTouch0 = Input.GetTouch(0);
        //////Debug.Log(""+ someTouch0);

        //bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        //bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        //if (buttonPressedLeft)
        {
            ////Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
        }
        //if (buttonPressedRight)
        {
            ////Debug.Log("buttonPressedRight:" + buttonPressedRight);
        }

        //if (counterForByteChange >= counterForByteChangeMax)
        {
            //if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
            {
                //if (Physics.Raycast(ray, out hit))
                {
                    if (collision.transform.tag == "collisionObject")
                    {
                        var chunkX = (int)(Mathf.Round(collision.transform.position.x * realplanetwidth) / realplanetwidth);
                        var chunkY = (int)(Mathf.Round(collision.transform.position.y * realplanetwidth) / realplanetwidth);
                        var chunkZ = (int)(Mathf.Round(collision.transform.position.z * realplanetwidth) / realplanetwidth);

                        //////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                        if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z) != null)
                        {
                            //////Debug.Log("==count==");
                            mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z);

                            //////Debug.Log("x: " + collision.normal.x + " y: " + collision.normal.y + " z: " + collision.normal.z);
                            if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == -1) //FRONT FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * realplanetwidth) / realplanetwidth);
                                var y = (Mathf.Round(p.y * realplanetwidth) / realplanetwidth);
                                var z = (Mathf.Round(p.z * realplanetwidth) / realplanetwidth);

                                Vector3 chunkbytepos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                                //retAdd.position = chunkbytepos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - chunkbytepos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - chunkbytepos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - chunkbytepos.z);

                                //////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                //////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = chunkbytepos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);

                            }
                            else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 1) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * realplanetwidth) / realplanetwidth);
                                var y = (Mathf.Round(p.y * realplanetwidth) / realplanetwidth);
                                var z = (Mathf.Round(p.z * realplanetwidth) / realplanetwidth);

                                Vector3 chunkbytepos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                retAdd.position = chunkbytepos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - chunkbytepos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - chunkbytepos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - chunkbytepos.z);

                                //////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);
                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);


                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = chunkbytepos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == -1 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 0) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * realplanetwidth) / realplanetwidth);
                                var y = (Mathf.Round(p.y * realplanetwidth) / realplanetwidth);
                                var z = (Mathf.Round(p.z * realplanetwidth) / realplanetwidth);

                                Vector3 chunkbytepos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                retAdd.position = chunkbytepos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - chunkbytepos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - chunkbytepos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - chunkbytepos.z);

                                //////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = chunkbytepos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == 1 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 0) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * realplanetwidth) / realplanetwidth);
                                var y = (Mathf.Round(p.y * realplanetwidth) / realplanetwidth);
                                var z = (Mathf.Round(p.z * realplanetwidth) / realplanetwidth);

                                Vector3 chunkbytepos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                retAdd.position = chunkbytepos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - chunkbytepos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - chunkbytepos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - chunkbytepos.z);

                                //////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = chunkbytepos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == -1 && collision.contacts[0].normal.z == 0) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * realplanetwidth) / realplanetwidth);
                                var y = (Mathf.Round(p.y * realplanetwidth) / realplanetwidth);
                                var z = (Mathf.Round(p.z * realplanetwidth) / realplanetwidth);

                                Vector3 chunkbytepos = new Vector3(x - (planeSize * 0.5f), y, (z - (planeSize * 0.5f)));

                                retAdd.position = chunkbytepos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - chunkbytepos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - chunkbytepos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - chunkbytepos.z);

                                //////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = chunkbytepos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 1 && collision.contacts[0].normal.z == 0) //TOP FACE
                            {
                                ////Debug.Log("top face");
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * realplanetwidth) / realplanetwidth);
                                var y = (Mathf.Round(p.y * realplanetwidth) / realplanetwidth);
                                var z = (Mathf.Round(p.z * realplanetwidth) / realplanetwidth);

                                Vector3 chunkbytepos = new Vector3(x - (planeSize * 0.5f), (y - (planeSize * 0.5f)), (z - (planeSize * 0.5f)));

                                retAdd.position = chunkbytepos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - chunkbytepos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - chunkbytepos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - chunkbytepos.z);

                                //////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                //////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = chunkbytepos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
            counterForByteChange = 0;
        }
        counterForByteChange++;
    }*/



    Ray ray;
    Vector3 positionThisObject;
    Vector3 directionForwardOfThisObject;

    Stopwatch tippickaxestopwatch = new Stopwatch();

    void Update()
    {
        /*
        bool thumbstickleft = OVRInput.Get(OVRInput.Touch.SecondaryThumbstick, OVRInput.Controller.LTouch);
        bool thumbstickright = OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight, OVRInput.Controller.LTouch);
        bool thumbstickdown = OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown, OVRInput.Controller.LTouch);
        bool thumbstickup = OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp, OVRInput.Controller.LTouch);*/

        if (InitcounterForIkFootPlacementSwtc == 0)
        {
            if (InitcounterForIkFootPlacement >= InitcounterForIkFootPlacementMax)
            {
                Debug.Log("***INIT COUNTER REACHED. can start ray***");
                InitcounterForIkFootPlacementSwtc = 1;
                InitcounterForIkFootPlacement = 0;
            }
            InitcounterForIkFootPlacement++;
        }



        if (counterForByteChange == 1)
        {
            if (stopwatch.ElapsedTicks >= counterForByteChangeMax)
            {
                stopwatch.Restart();
                counterForByteChange = 0;
            }
        }

        if (swtcForTypeOfInteract == 0)
        {
            //ray = new Ray(transform.position, transform.forward);//ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Debug.DrawRay(transform.position, transform.forward * 0.05f, Color.red, 0.001f);

            //var someTouch0 = Input.GetTouch(0);
            //////Debug.Log(""+ someTouch0);

            /*bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

            if (buttonPressedLeft)
            {
                ////Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
            }
            if (buttonPressedRight)
            {
                ////Debug.Log("buttonPressedRight:" + buttonPressedRight);
            }*/

            if (counterForByteChange == 0)// counterForByteChangeMax)
            {
                if (InitcounterForIkFootPlacementSwtc == 1)
                {
                    //Debug.Log("test0");
                    if (raycounterSwtc == 0 || raycounterSwtc == 1)
                    {
                        //Debug.Log("test1");
                        if (raylength < raycounterLoopMax)
                        {
                            //Debug.Log("test2");
                            if (tippickaxestopwatch.Elapsed.Ticks >= 1)
                            {
                                //Debug.Log("test3");
                                for (int r = 0; r < 1; r++)
                                {
                                    //Debug.Log("test4");
                                    var ray = new Ray(pickaxetiptransform.transform.position + (pickaxetiptransform.forward * (raylength * 0.0075f)), pickaxetiptransform.forward * 0.0075f);
                                    Debug.DrawRay(pickaxetiptransform.transform.position + (pickaxetiptransform.forward * (raylength * 0.0075f)), pickaxetiptransform.forward * 0.0075f, Color.white, 0.001f);

                                    var somespherecasthitbool = Physics.SphereCast(pickaxetiptransform.transform.position, 0.05f, pickaxetiptransform.forward, out spherecasthit, 0.05f, layerMask);
                                    var someraycasthitbool = Physics.Raycast(ray, out hit, (raylength * 0.05f), layerMask);

                                    //spherecasthit.triangleIndex                      

                                    if (somespherecasthitbool)
                                    {

                                        MeshCollider meshCollider = spherecasthit.collider as MeshCollider;
                                        if (meshCollider != null || meshCollider.sharedMesh != null)
                                        {
                                            //Debug.Log("drawing mesh hit face triangles");
                                            Mesh somemesh = meshCollider.sharedMesh;
                                            Vector3[] vertices = somemesh.vertices;
                                            int[] triangles = somemesh.triangles;

                                            Vector3 p0 = vertices[triangles[spherecasthit.triangleIndex * 3 + 0]];
                                            Vector3 p1 = vertices[triangles[spherecasthit.triangleIndex * 3 + 1]];
                                            Vector3 p2 = vertices[triangles[spherecasthit.triangleIndex * 3 + 2]];

                                            Transform hitTransform = spherecasthit.collider.transform;
                                            p0 = hitTransform.TransformPoint(p0);
                                            p1 = hitTransform.TransformPoint(p1);
                                            p2 = hitTransform.TransformPoint(p2);

                                            //Debug.DrawRay(p0, (p1 - p0).normalized, Color.red, 1000);
                                            //Debug.DrawRay(p1, (p2 - p1).normalized, Color.red, 1000);
                                            //Debug.DrawRay(p2, (p0 - p2).normalized, Color.red, 1000);

                                            /*Debug.DrawLine(p0, (p1), Color.red, 5);
                                            Debug.DrawLine(p1, (p2), Color.red, 5);
                                            Debug.DrawLine(p2, (p0), Color.red, 5);*/

                                            somePointer0.transform.position = p0;
                                            somePointer1.transform.position = p1;
                                            somePointer2.transform.position = p2;

                                            var perp = Vector3.Cross((p1 - p0).normalized, (p2 - p1).normalized);
                                            perp.Normalize();
                                            //Debug.DrawLine(hitTransform.transform.position, (hitTransform.transform.position + perp), Color.magenta, 5);
                                            var barycentriccoord = spherecasthit.barycentricCoordinate;



                                            thepos = spherecasthit.point;// barycentriccoord;



                                            //Debug.Log("test6");
                                            var chunkX = (int)(Mathf.Round(thepos.x * realplanetwidth) / realplanetwidth);
                                            var chunkY = (int)(Mathf.Round(thepos.y * realplanetwidth) / realplanetwidth);
                                            var chunkZ = (int)(Mathf.Round(thepos.z * realplanetwidth) / realplanetwidth);

                                            //////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                                            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)thepos.x, (int)thepos.y, (int)thepos.z) != null)
                                            {
                                                Debug.Log("==count==");

                                                mainChunkFinal currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)thepos.x, (int)thepos.y, (int)thepos.z);

                                                //Debug.Log("x: " + perp.x + " y: " + perp.y + " z: " + perp.z);
                                                if (perp.x == 0 && perp.y == 0 && perp.z == -1) //BACK FACE
                                                {
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                    }



                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                        indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                    }


                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }


                                                    //Debug.Log("posz:" + posz);
                                                    if (posz >= 1.0f)
                                                    {

                                                        indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                                                    }

                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }


                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                        indexZ = chunkWidth - indexZ;
                                                    }


                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                                                    if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                    {

                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

                                                        raylength = 0;
                                                        //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                        if (addfracturedcubeonimpact == 1)
                                                        {

                                                            instancedata = new sccsInstancesunitypool.instancedata();
                                                            instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                            instancedata.currentInstanceGameObject = null;
                                                            instancedata.instanceindex = -1;
                                                            instancedata.enabled = -1;
                                                            instancedata.swap = -1;
                                                            instancedata.instanceenabledcounter = -1;
                                                            instancedata.instanceenabledcounterSwtc = -1;
                                                            instancedata.instanceenabledcounterMax = -1;

                                                            sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);

                                                            /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                            UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                            UnityTutorialPooledObject.SetActive(true);*/
                                                        }

                                                    }


                                                }
                                                else if (perp.x == 0 && perp.y == 0 && perp.z == 1) //FRONT FACE
                                                {
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                    }



                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                    }


                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    ////Debug.Log("posz:" + posz);
                                                    if (posz >= 1.0f)
                                                    {
                                                        indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                                                        ////Debug.Log("0indexZ:" + indexZ);
                                                        if (indexZ < 0)
                                                        {
                                                            indexZ *= -1;
                                                            if (indexZ >= 5)
                                                            {
                                                                ////Debug.Log("2indexZ:" + indexZ);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexZ:" + indexZ);
                                                            }
                                                            indexZ = (chunkWidth - 1) - indexZ;
                                                            ////Debug.Log("4indexZ:" + indexZ);

                                                        }
                                                        else
                                                        {
                                                            if (indexZ == 0)
                                                            {
                                                                indexZ = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexZ:" + indexZ);
                                                                if (indexZ >= 5)
                                                                {
                                                                    indexZ -= 1;
                                                                    ////Debug.Log("2indexZ:" + indexZ);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexZ:" + indexZ);
                                                                    indexZ -= 1;
                                                                }
                                                            }
                                                        }

                                                        ////Debug.Log("11indexZ:" + indexZ);
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                                                        ////Debug.Log("0indexZ:" + indexZ);
                                                        if (indexZ < 0)
                                                        {

                                                            indexZ *= -1;
                                                            if (indexZ >= 5)
                                                            {
                                                                ////Debug.Log("2indexZ:" + indexZ);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexZ:" + indexZ);
                                                            }
                                                            indexZ = (chunkWidth - 1) - indexZ;
                                                            ////Debug.Log("4indexZ:" + indexZ);

                                                        }
                                                        else
                                                        {
                                                            if (indexZ == 0)
                                                            {
                                                                indexZ = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexZ:" + indexZ);
                                                                if (indexZ >= 5)
                                                                {
                                                                    indexZ -= 1;
                                                                    ////Debug.Log("2indexZ:" + indexZ);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexZ:" + indexZ);
                                                                    indexZ -= 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }

                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                    }

                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                                                    if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                    {
                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                                                        raylength = 0;


                                                        //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                        if (addfracturedcubeonimpact == 1)
                                                        {
                                                            instancedata = new sccsInstancesunitypool.instancedata();
                                                            instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                            instancedata.currentInstanceGameObject = null;
                                                            instancedata.instanceindex = -1;
                                                            instancedata.enabled = -1;
                                                            instancedata.swap = -1;
                                                            instancedata.instanceenabledcounter = -1;
                                                            instancedata.instanceenabledcounterSwtc = -1;
                                                            instancedata.instanceenabledcounterMax = -1;

                                                            sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                            /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                            UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                            UnityTutorialPooledObject.SetActive(true);*/
                                                        }
                                                    }
                                                }
                                                else if (perp.x == -1 && perp.y == 0 && perp.z == 0) //LEFT FACE
                                                {
                                                    /*Vector3 p = spherecasthit.point;// spherecasthit.point; // this.transform.TransformPoint(thepointinquestion);

                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    if (posx > 1.0f)
                                                    {
                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));

                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                    }



                                                    /*if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    if (posy > 1.0f)
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexY = Mathf.FloorToInt((Mathf.Round(posy * chunkWidth))); //indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }




                                                    /*if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    if (posz > 1.0f)
                                                    {
                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                    }



                                                    //indexZ = (chunkWidth - 1) - indexZ;


                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                        //indexX = (chunkWidth - 1)- indexX;
                                                    }


                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                        //indexY = (chunkWidth - 1) - indexY;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                        //indexZ = (chunkWidth - 1) - indexZ;
                                                    }*/
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    ////Debug.Log("posz:" + posz);
                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.RoundToInt(((((posx - Mathf.Round(posx))) * chunkWidth)));


                                                        //Debug.Log("0indexX:" + indexX);
                                                        if (indexX < 0)
                                                        {

                                                            if (indexX >= -4)
                                                            {
                                                                //indexX += 1;
                                                                indexX *= -1;
                                                                indexX = (chunkWidth - 1) - indexX;
                                                                indexX += 1;
                                                                ////Debug.Log("2indexX:" + indexX);
                                                            }
                                                            else
                                                            {
                                                                //indexX += 1;
                                                                //indexX -= 1;
                                                                ////Debug.Log("3indexX:" + indexX);
                                                            }
                                                            //indexX = (chunkWidth - 1) - indexX;
                                                            ////Debug.Log("4indexX:" + indexX);

                                                        }
                                                        else
                                                        {
                                                            if (indexX == 0)
                                                            {
                                                                //indexX = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                /*////Debug.Log("1indexX:" + indexX);
                                                                if (indexX >= 5)
                                                                {
                                                                    indexX -= 1;
                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                    indexX -= 1;
                                                                }*/
                                                            }
                                                        }

                                                        ////Debug.Log("11indexX:" + indexX);
                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                        //Debug.Log("0indexX:" + indexX);
                                                        if (indexX < 0)
                                                        {

                                                            indexX *= -1;
                                                            if (indexX >= 5)
                                                            {
                                                                ////Debug.Log("2indexX:" + indexX);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexX:" + indexX);
                                                            }
                                                            indexX = (chunkWidth - 1) - indexX;
                                                            ////Debug.Log("4indexX:" + indexX);

                                                        }
                                                        else
                                                        {
                                                            if (indexX == 0)
                                                            {
                                                                //indexX = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexX:" + indexX);
                                                                /*if (indexX >= 5)
                                                                {
                                                                    indexX -= 1;
                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                    indexX -= 1;
                                                                }*/
                                                            }
                                                        }
                                                    }


                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    if (posz >= 1.0f)
                                                    {
                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                    }



                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                        indexY = Mathf.FloorToInt((Mathf.Round(posy * chunkWidth)));
                                                    }



                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }

                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                    }


                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                                                    if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                    {
                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                                                        raylength = 0;
                                                        //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                        if (addfracturedcubeonimpact == 1)
                                                        {
                                                            instancedata = new sccsInstancesunitypool.instancedata();
                                                            instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                            instancedata.currentInstanceGameObject = null;
                                                            instancedata.instanceindex = -1;
                                                            instancedata.enabled = -1;
                                                            instancedata.swap = -1;
                                                            instancedata.instanceenabledcounter = -1;
                                                            instancedata.instanceenabledcounterSwtc = -1;
                                                            instancedata.instanceenabledcounterMax = -1;

                                                            sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                            /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                            UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                            UnityTutorialPooledObject.SetActive(true);*/
                                                        }
                                                    }
                                                }
                                                else if (perp.x == 1 && perp.y == 0 && perp.z == 0) //RIGHT FACE
                                                {
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    ////Debug.Log("posz:" + posz);
                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.RoundToInt(((((posx - Mathf.Round(posx))) * chunkWidth)));
                                                        ////Debug.Log("0indexX:" + indexX);
                                                        if (indexX < 0)
                                                        {
                                                            indexX *= -1;
                                                            if (indexX >= 5)
                                                            {
                                                                ////Debug.Log("2indexX:" + indexX);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexX:" + indexX);
                                                            }
                                                            indexX = (chunkWidth - 1) - indexX;
                                                            ////Debug.Log("4indexX:" + indexX);

                                                        }
                                                        else
                                                        {
                                                            if (indexX == 0)
                                                            {
                                                                indexX = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexX:" + indexX);
                                                                if (indexX >= 5)
                                                                {
                                                                    indexX -= 1;
                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                    indexX -= 1;
                                                                }
                                                            }
                                                        }

                                                        ////Debug.Log("11indexX:" + indexX);
                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                        ////Debug.Log("0indexX:" + indexX);
                                                        if (indexX < 0)
                                                        {

                                                            indexX *= -1;
                                                            if (indexX >= 5)
                                                            {
                                                                ////Debug.Log("2indexX:" + indexX);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexX:" + indexX);
                                                            }
                                                            indexX = (chunkWidth - 1) - indexX;
                                                            ////Debug.Log("4indexX:" + indexX);

                                                        }
                                                        else
                                                        {
                                                            if (indexX == 0)
                                                            {
                                                                indexX = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexX:" + indexX);
                                                                if (indexX >= 5)
                                                                {
                                                                    indexX -= 1;
                                                                    ////Debug.Log("2indexX:" + indexX);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexX:" + indexX);
                                                                    indexX -= 1;
                                                                }
                                                            }
                                                        }
                                                    }









                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    if (posz >= 1.0f)
                                                    {
                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                    }



                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                    }



                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }

                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                    }

                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                                                    if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                    {
                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                                                        raylength = 0;
                                                        //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                        if (addfracturedcubeonimpact == 1)
                                                        {
                                                            instancedata = new sccsInstancesunitypool.instancedata();
                                                            instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                            instancedata.currentInstanceGameObject = null;
                                                            instancedata.instanceindex = -1;
                                                            instancedata.enabled = -1;
                                                            instancedata.swap = -1;
                                                            instancedata.instanceenabledcounter = -1;
                                                            instancedata.instanceenabledcounterSwtc = -1;
                                                            instancedata.instanceenabledcounterMax = -1;

                                                            sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                            /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                            UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                            UnityTutorialPooledObject.SetActive(true);*/
                                                        }
                                                    }
                                                }
                                                else if (perp.x == 0 && perp.y == -1 && perp.z == 0) //BOTTOM FACE
                                                {
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    //Debug.Log("posz:" + posz);
                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.RoundToInt(((((posy - Mathf.Round(posy))) * chunkWidth)));
                                                        //Debug.Log("0indexY:" + indexY);
                                                        if (indexY < 0)
                                                        {
                                                            //indexY *= -1;
                                                            if (indexY > -5)
                                                            {
                                                                indexY *= -1;
                                                                indexY = (chunkWidth - 1) - indexY;
                                                                indexY += 1;
                                                                ////Debug.Log("2indexY:" + indexY);
                                                            }
                                                            else
                                                            {
                                                                indexY *= -1;
                                                                ////Debug.Log("3indexY:" + indexY);
                                                            }
                                                            //indexY = (chunkWidth - 1) - indexY;
                                                            ////Debug.Log("4indexY:" + indexY);

                                                        }
                                                        else
                                                        {
                                                            if (indexY == 0)
                                                            {
                                                                //indexY = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                //Debug.Log("1indexY:" + indexY);
                                                                if (indexY >= 5)
                                                                {
                                                                    //indexY -= 1;
                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                    //indexY -= 1;
                                                                }
                                                            }
                                                        }

                                                        ////Debug.Log("11indexY:" + indexY);
                                                    }
                                                    else
                                                    {
                                                        indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                        //Debug.Log("0indexY:" + indexY);
                                                        if (indexY < 0)
                                                        {

                                                            indexY *= -1;
                                                            if (indexY >= 5)
                                                            {
                                                                ////Debug.Log("2indexY:" + indexY);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexY:" + indexY);
                                                            }
                                                            indexY = (chunkWidth - 1) - indexY;
                                                            ////Debug.Log("4indexY:" + indexY);

                                                        }
                                                        else
                                                        {
                                                            if (indexY == 0)
                                                            {
                                                                //indexY = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                //indexY = chunkWidth - 1;
                                                                //Debug.Log("1indexY:" + indexY);
                                                                if (indexY >= 5)
                                                                {
                                                                    //indexY -= 1;
                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                    //indexY -= 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    if (posz >= 1.0f)
                                                    {
                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));

                                                    }

                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));

                                                    }
                                                    else
                                                    {
                                                        //indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                    }

                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }

                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                    }






                                                    ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                    //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                                                    if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                    {
                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

                                                        //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                        raylength = 0;
                                                        if (addfracturedcubeonimpact == 1)
                                                        {
                                                            instancedata = new sccsInstancesunitypool.instancedata();
                                                            instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                            instancedata.currentInstanceGameObject = null;
                                                            instancedata.instanceindex = -1;
                                                            instancedata.enabled = -1;
                                                            instancedata.swap = -1;
                                                            instancedata.instanceenabledcounter = -1;
                                                            instancedata.instanceenabledcounterSwtc = -1;
                                                            instancedata.instanceenabledcounterMax = -1;

                                                            sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                            /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                            UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                            UnityTutorialPooledObject.SetActive(true);*/
                                                        }
                                                    }
                                                }
                                                else if (perp.x == 0 && perp.y == 1 && perp.z == 0) //TOP FACE
                                                {
                                                    Vector3 p = spherecasthit.point;


                                                    float posx = (p.x);
                                                    float posy = (p.y);
                                                    float posz = (p.z);


                                                    Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                    retAdd.position = chunkbytepos;

                                                    int indexX = 0;
                                                    int indexY = 0;
                                                    int indexZ = 0;


                                                    if (posy < 0)
                                                    {
                                                        posy *= -1;
                                                    }

                                                    ////Debug.Log("posz:" + posz);
                                                    if (posy >= 1.0f)
                                                    {
                                                        indexY = Mathf.RoundToInt(((((posy - Mathf.Round(posy))) * chunkWidth)));
                                                        ////Debug.Log("0indexY:" + indexY);
                                                        if (indexY < 0)
                                                        {
                                                            indexY *= -1;
                                                            if (indexY >= 5)
                                                            {
                                                                ////Debug.Log("2indexY:" + indexY);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexY:" + indexY);
                                                            }
                                                            indexY = (chunkWidth - 1) - indexY;
                                                            ////Debug.Log("4indexY:" + indexY);

                                                        }
                                                        else
                                                        {
                                                            if (indexY == 0)
                                                            {
                                                                indexY = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexY:" + indexY);
                                                                if (indexY >= 5)
                                                                {
                                                                    indexY -= 1;
                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                    indexY -= 1;
                                                                }
                                                            }
                                                        }

                                                        ////Debug.Log("11indexY:" + indexY);
                                                    }
                                                    else
                                                    {
                                                        indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                        ////Debug.Log("0indexY:" + indexY);
                                                        if (indexY < 0)
                                                        {

                                                            indexY *= -1;
                                                            if (indexY >= 5)
                                                            {
                                                                ////Debug.Log("2indexY:" + indexY);
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("3indexY:" + indexY);
                                                            }
                                                            indexY = (chunkWidth - 1) - indexY;
                                                            ////Debug.Log("4indexY:" + indexY);

                                                        }
                                                        else
                                                        {
                                                            if (indexY == 0)
                                                            {
                                                                indexY = chunkWidth - 1;
                                                            }
                                                            else
                                                            {
                                                                ////Debug.Log("1indexY:" + indexY);
                                                                if (indexY >= 5)
                                                                {
                                                                    indexY -= 1;
                                                                    ////Debug.Log("2indexY:" + indexY);
                                                                }
                                                                else
                                                                {
                                                                    ////Debug.Log("3indexY:" + indexY);
                                                                    indexY -= 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (posz < 0)
                                                    {
                                                        posz *= -1;
                                                    }

                                                    if (posz >= 1.0f)
                                                    {
                                                        indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                    }

                                                    if (posx < 0)
                                                    {
                                                        posx *= -1;
                                                    }

                                                    if (posx >= 1.0f)
                                                    {
                                                        indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                    }
                                                    else
                                                    {
                                                        indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                    }

                                                    if (indexX < 0)
                                                    {
                                                        indexX *= -1;
                                                    }

                                                    if (indexY < 0)
                                                    {
                                                        indexY *= -1;
                                                    }

                                                    if (indexZ < 0)
                                                    {
                                                        indexZ *= -1;
                                                    }




                                                    if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                    {
                                                        ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                        ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                        currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                                                        //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                        raylength = 0;
                                                        if (addfracturedcubeonimpact == 1)
                                                        {
                                                            instancedata = new sccsInstancesunitypool.instancedata();
                                                            instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                            instancedata.currentInstanceGameObject = null;
                                                            instancedata.instanceindex = -1;
                                                            instancedata.enabled = -1;
                                                            instancedata.swap = -1;
                                                            instancedata.instanceenabledcounter = -1;
                                                            instancedata.instanceenabledcounterSwtc = -1;
                                                            instancedata.instanceenabledcounterMax = -1;

                                                            sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                            /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                            var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                            UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                            UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                            UnityTutorialPooledObject.SetActive(true);*/
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {

                                            }

                                        }




                                    }


                                    /*if (somespherecasthitbool)
                                    {
                                        thepos = this.transform.position;

                                      
                                    }*/

                                    if (someraycasthitbool && !somespherecasthitbool || someraycasthitbool && somespherecasthitbool)//if (Physics.Raycast(ray, out hit, (raylength * 0.01f), layerMask) && !somespherecasthitbool)
                                    {
                                        if (this.transform.name == "tip")//if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
                                        {
                                            if (hit.rigidbody != null)
                                            {
                                                Debug.Log("someraycasthitbool && !somespherecasthitbool");

                                                if (hit.transform.tag == "collisionObject")
                                                {

                                                    if (hit.rigidbody != null)
                                                    {

                                                        thepos = hit.point;
                                                    }
                                                    else
                                                    {
                                                        thepos = this.transform.position;
                                                    }

                                                    if (GetComponent<Fracture4>() != null)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        //Debug.Log("test6");
                                                        var chunkX = (int)(Mathf.Round(thepos.x * realplanetwidth) / realplanetwidth);
                                                        var chunkY = (int)(Mathf.Round(thepos.y * realplanetwidth) / realplanetwidth);
                                                        var chunkZ = (int)(Mathf.Round(thepos.z * realplanetwidth) / realplanetwidth);

                                                        //////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                                                        if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)thepos.x, (int)thepos.y, (int)thepos.z) != null)
                                                        {
                                                            //////Debug.Log("==count==");
                                                            mainChunkFinal currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)thepos.x, (int)thepos.y, (int)thepos.z);

                                                            //////Debug.Log("x: " + hit.normal.x + " y: " + hit.normal.y + " z: " + hit.normal.z);
                                                            if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1) //BACK FACE
                                                            {
                                                                Vector3 p = hit.point;


                                                                float posx = (p.x);
                                                                float posy = (p.y);
                                                                float posz = (p.z);


                                                                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                retAdd.position = chunkbytepos;

                                                                int indexX = 0;
                                                                int indexY = 0;
                                                                int indexZ = 0;


                                                                if (posx < 0)
                                                                {
                                                                    posx *= -1;
                                                                }

                                                                if (posx >= 1.0f)
                                                                {
                                                                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                                }



                                                                if (posy < 0)
                                                                {
                                                                    posy *= -1;
                                                                }

                                                                if (posy >= 1.0f)
                                                                {
                                                                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                                    indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                                }


                                                                if (posz < 0)
                                                                {
                                                                    posz *= -1;
                                                                }


                                                                //Debug.Log("posz:" + posz);
                                                                if (posz >= 1.0f)
                                                                {

                                                                    indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                                                                }

                                                                if (indexX < 0)
                                                                {
                                                                    indexX *= -1;
                                                                }


                                                                if (indexY < 0)
                                                                {
                                                                    indexY *= -1;
                                                                }

                                                                if (indexZ < 0)
                                                                {
                                                                    indexZ *= -1;
                                                                    indexZ = chunkWidth - indexZ;
                                                                }


                                                                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));
                                                                if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                                {

                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

                                                                    raylength = 0;
                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        instancedata = new sccsInstancesunitypool.instancedata();
                                                                        instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                                        instancedata.currentInstanceGameObject = null;
                                                                        instancedata.instanceindex = -1;
                                                                        instancedata.enabled = -1;
                                                                        instancedata.swap = -1;
                                                                        instancedata.instanceenabledcounter = -1;
                                                                        instancedata.instanceenabledcounterSwtc = -1;
                                                                        instancedata.instanceenabledcounterMax = -1;

                                                                        sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                                        /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);*/
                                                                    }

                                                                }

                                                            }
                                                            else if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1) //FRONT FACE
                                                            {
                                                                Vector3 p = hit.point;


                                                                float posx = (p.x);
                                                                float posy = (p.y);
                                                                float posz = (p.z);


                                                                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                retAdd.position = chunkbytepos;

                                                                int indexX = 0;
                                                                int indexY = 0;
                                                                int indexZ = 0;


                                                                if (posx < 0)
                                                                {
                                                                    posx *= -1;
                                                                }

                                                                if (posx >= 1.0f)
                                                                {
                                                                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                                }



                                                                if (posy < 0)
                                                                {
                                                                    posy *= -1;
                                                                }

                                                                if (posy >= 1.0f)
                                                                {
                                                                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                                }


                                                                if (posz < 0)
                                                                {
                                                                    posz *= -1;
                                                                }

                                                                ////Debug.Log("posz:" + posz);
                                                                if (posz >= 1.0f)
                                                                {
                                                                    indexZ = Mathf.RoundToInt(((((posz - Mathf.Round(posz))) * chunkWidth)));
                                                                    ////Debug.Log("0indexZ:" + indexZ);
                                                                    if (indexZ < 0)
                                                                    {
                                                                        indexZ *= -1;
                                                                        if (indexZ >= 5)
                                                                        {
                                                                            ////Debug.Log("2indexZ:" + indexZ);
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("3indexZ:" + indexZ);
                                                                        }
                                                                        indexZ = (chunkWidth - 1) - indexZ;
                                                                        ////Debug.Log("4indexZ:" + indexZ);

                                                                    }
                                                                    else
                                                                    {
                                                                        if (indexZ == 0)
                                                                        {
                                                                            indexZ = chunkWidth - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("1indexZ:" + indexZ);
                                                                            if (indexZ >= 5)
                                                                            {
                                                                                indexZ -= 1;
                                                                                ////Debug.Log("2indexZ:" + indexZ);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexZ:" + indexZ);
                                                                                indexZ -= 1;
                                                                            }
                                                                        }
                                                                    }

                                                                    ////Debug.Log("11indexZ:" + indexZ);
                                                                }
                                                                else
                                                                {
                                                                    indexZ = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));
                                                                    ////Debug.Log("0indexZ:" + indexZ);
                                                                    if (indexZ < 0)
                                                                    {

                                                                        indexZ *= -1;
                                                                        if (indexZ >= 5)
                                                                        {
                                                                            ////Debug.Log("2indexZ:" + indexZ);
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("3indexZ:" + indexZ);
                                                                        }
                                                                        indexZ = (chunkWidth - 1) - indexZ;
                                                                        ////Debug.Log("4indexZ:" + indexZ);

                                                                    }
                                                                    else
                                                                    {
                                                                        if (indexZ == 0)
                                                                        {
                                                                            indexZ = chunkWidth - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("1indexZ:" + indexZ);
                                                                            if (indexZ >= 5)
                                                                            {
                                                                                indexZ -= 1;
                                                                                ////Debug.Log("2indexZ:" + indexZ);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexZ:" + indexZ);
                                                                                indexZ -= 1;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                if (indexX < 0)
                                                                {
                                                                    indexX *= -1;
                                                                }

                                                                if (indexY < 0)
                                                                {
                                                                    indexY *= -1;
                                                                }

                                                                if (indexZ < 0)
                                                                {
                                                                    indexZ *= -1;
                                                                }

                                                                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));
                                                                if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                                {

                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                                                                    raylength = 0;
                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        instancedata = new sccsInstancesunitypool.instancedata();
                                                                        instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                                        instancedata.currentInstanceGameObject = null;
                                                                        instancedata.instanceindex = -1;
                                                                        instancedata.enabled = -1;
                                                                        instancedata.swap = -1;
                                                                        instancedata.instanceenabledcounter = -1;
                                                                        instancedata.instanceenabledcounterSwtc = -1;
                                                                        instancedata.instanceenabledcounterMax = -1;

                                                                        sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                                        /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);*/
                                                                    }
                                                                }
                                                            }
                                                            else if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0) //LEFT FACE
                                                            {
                                                                /*Vector3 p = hit.point;// hit.point; // this.transform.TransformPoint(thepointinquestion);

                                                                float posx = (p.x);
                                                                float posy = (p.y);
                                                                float posz = (p.z);


                                                                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                retAdd.position = chunkbytepos;

                                                                int indexX = 0;
                                                                int indexY = 0;
                                                                int indexZ = 0;


                                                                if (posx < 0)
                                                                {
                                                                    posx *= -1;
                                                                }

                                                                if (posx > 1.0f)
                                                                {
                                                                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));

                                                                }
                                                                else
                                                                {
                                                                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                                }



                                                                /*if (posy < 0)
                                                                {
                                                                    posy *= -1;
                                                                }

                                                                if (posy > 1.0f)
                                                                {
                                                                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexY = Mathf.FloorToInt((Mathf.Round(posy * chunkWidth))); //indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                }




                                                                /*if (posz < 0)
                                                                {
                                                                    posz *= -1;
                                                                }

                                                                if (posz > 1.0f)
                                                                {
                                                                    indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                                }



                                                                //indexZ = (chunkWidth - 1) - indexZ;


                                                                if (indexX < 0)
                                                                {
                                                                    indexX *= -1;
                                                                    //indexX = (chunkWidth - 1)- indexX;
                                                                }


                                                                if (indexY < 0)
                                                                {
                                                                    indexY *= -1;
                                                                    //indexY = (chunkWidth - 1) - indexY;
                                                                }

                                                                if (indexZ < 0)
                                                                {
                                                                    indexZ *= -1;
                                                                    //indexZ = (chunkWidth - 1) - indexZ;
                                                                }*/
                                                                Vector3 p = hit.point;


                                                                float posx = (p.x);
                                                                float posy = (p.y);
                                                                float posz = (p.z);


                                                                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                retAdd.position = chunkbytepos;

                                                                int indexX = 0;
                                                                int indexY = 0;
                                                                int indexZ = 0;


                                                                if (posx < 0)
                                                                {
                                                                    posx *= -1;
                                                                }

                                                                ////Debug.Log("posz:" + posz);
                                                                if (posx >= 1.0f)
                                                                {
                                                                    indexX = Mathf.RoundToInt(((((posx - Mathf.Round(posx))) * chunkWidth)));


                                                                    //Debug.Log("0indexX:" + indexX);
                                                                    if (indexX < 0)
                                                                    {

                                                                        if (indexX >= -4)
                                                                        {
                                                                            //indexX += 1;
                                                                            indexX *= -1;
                                                                            indexX = (chunkWidth - 1) - indexX;
                                                                            indexX += 1;
                                                                            ////Debug.Log("2indexX:" + indexX);
                                                                        }
                                                                        else
                                                                        {
                                                                            //indexX += 1;
                                                                            //indexX -= 1;
                                                                            ////Debug.Log("3indexX:" + indexX);
                                                                        }
                                                                        //indexX = (chunkWidth - 1) - indexX;
                                                                        ////Debug.Log("4indexX:" + indexX);

                                                                    }
                                                                    else
                                                                    {
                                                                        if (indexX == 0)
                                                                        {
                                                                            //indexX = chunkWidth - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            /*////Debug.Log("1indexX:" + indexX);
                                                                            if (indexX >= 5)
                                                                            {
                                                                                indexX -= 1;
                                                                                ////Debug.Log("2indexX:" + indexX);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexX:" + indexX);
                                                                                indexX -= 1;
                                                                            }*/
                                                                        }
                                                                    }

                                                                    ////Debug.Log("11indexX:" + indexX);
                                                                }
                                                                else
                                                                {
                                                                    indexX = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                                    //Debug.Log("0indexX:" + indexX);
                                                                    if (indexX < 0)
                                                                    {

                                                                        indexX *= -1;
                                                                        if (indexX >= 5)
                                                                        {
                                                                            ////Debug.Log("2indexX:" + indexX);
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("3indexX:" + indexX);
                                                                        }
                                                                        indexX = (chunkWidth - 1) - indexX;
                                                                        ////Debug.Log("4indexX:" + indexX);

                                                                    }
                                                                    else
                                                                    {
                                                                        if (indexX == 0)
                                                                        {
                                                                            //indexX = chunkWidth - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("1indexX:" + indexX);
                                                                            /*if (indexX >= 5)
                                                                            {
                                                                                indexX -= 1;
                                                                                ////Debug.Log("2indexX:" + indexX);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexX:" + indexX);
                                                                                indexX -= 1;
                                                                            }*/
                                                                        }
                                                                    }
                                                                }


                                                                if (posz < 0)
                                                                {
                                                                    posz *= -1;
                                                                }

                                                                if (posz >= 1.0f)
                                                                {
                                                                    indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                                }



                                                                if (posy < 0)
                                                                {
                                                                    posy *= -1;
                                                                }

                                                                if (posy >= 1.0f)
                                                                {
                                                                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    //indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                                    indexY = Mathf.FloorToInt((Mathf.Round(posy * chunkWidth)));
                                                                }



                                                                if (indexX < 0)
                                                                {
                                                                    indexX *= -1;
                                                                }

                                                                if (indexY < 0)
                                                                {
                                                                    indexY *= -1;
                                                                }

                                                                if (indexZ < 0)
                                                                {
                                                                    indexZ *= -1;
                                                                }


                                                                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                                                                if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                                {
                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                                                                    raylength = 0;
                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        instancedata = new sccsInstancesunitypool.instancedata();
                                                                        instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                                        instancedata.currentInstanceGameObject = null;
                                                                        instancedata.instanceindex = -1;
                                                                        instancedata.enabled = -1;
                                                                        instancedata.swap = -1;
                                                                        instancedata.instanceenabledcounter = -1;
                                                                        instancedata.instanceenabledcounterSwtc = -1;
                                                                        instancedata.instanceenabledcounterMax = -1;

                                                                        sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                                        /* var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                         var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                         UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                         UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                         UnityTutorialPooledObject.SetActive(true);*/
                                                                    }
                                                                }
                                                            }
                                                            else if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0) //RIGHT FACE
                                                            {
                                                                Vector3 p = hit.point;


                                                                float posx = (p.x);
                                                                float posy = (p.y);
                                                                float posz = (p.z);


                                                                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                retAdd.position = chunkbytepos;

                                                                int indexX = 0;
                                                                int indexY = 0;
                                                                int indexZ = 0;


                                                                if (posx < 0)
                                                                {
                                                                    posx *= -1;
                                                                }

                                                                ////Debug.Log("posz:" + posz);
                                                                if (posx >= 1.0f)
                                                                {
                                                                    indexX = Mathf.RoundToInt(((((posx - Mathf.Round(posx))) * chunkWidth)));
                                                                    ////Debug.Log("0indexX:" + indexX);
                                                                    if (indexX < 0)
                                                                    {
                                                                        indexX *= -1;
                                                                        if (indexX >= 5)
                                                                        {
                                                                            ////Debug.Log("2indexX:" + indexX);
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("3indexX:" + indexX);
                                                                        }
                                                                        indexX = (chunkWidth - 1) - indexX;
                                                                        ////Debug.Log("4indexX:" + indexX);

                                                                    }
                                                                    else
                                                                    {
                                                                        if (indexX == 0)
                                                                        {
                                                                            indexX = chunkWidth - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("1indexX:" + indexX);
                                                                            if (indexX >= 5)
                                                                            {
                                                                                indexX -= 1;
                                                                                ////Debug.Log("2indexX:" + indexX);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexX:" + indexX);
                                                                                indexX -= 1;
                                                                            }
                                                                        }
                                                                    }

                                                                    ////Debug.Log("11indexX:" + indexX);
                                                                }
                                                                else
                                                                {
                                                                    indexX = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                                    ////Debug.Log("0indexX:" + indexX);
                                                                    if (indexX < 0)
                                                                    {

                                                                        indexX *= -1;
                                                                        if (indexX >= 5)
                                                                        {
                                                                            ////Debug.Log("2indexX:" + indexX);
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("3indexX:" + indexX);
                                                                        }
                                                                        indexX = (chunkWidth - 1) - indexX;
                                                                        ////Debug.Log("4indexX:" + indexX);

                                                                    }
                                                                    else
                                                                    {
                                                                        if (indexX == 0)
                                                                        {
                                                                            indexX = chunkWidth - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("1indexX:" + indexX);
                                                                            if (indexX >= 5)
                                                                            {
                                                                                indexX -= 1;
                                                                                ////Debug.Log("2indexX:" + indexX);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexX:" + indexX);
                                                                                indexX -= 1;
                                                                            }
                                                                        }
                                                                    }
                                                                }









                                                                if (posz < 0)
                                                                {
                                                                    posz *= -1;
                                                                }

                                                                if (posz >= 1.0f)
                                                                {
                                                                    indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                                }



                                                                if (posy < 0)
                                                                {
                                                                    posy *= -1;
                                                                }

                                                                if (posy >= 1.0f)
                                                                {
                                                                    indexY = Mathf.FloorToInt((((posy - Mathf.Floor(posy)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexY = Mathf.FloorToInt((((posy - Mathf.Round(posy)) * chunkWidth)));
                                                                }



                                                                if (indexX < 0)
                                                                {
                                                                    indexX *= -1;
                                                                }

                                                                if (indexY < 0)
                                                                {
                                                                    indexY *= -1;
                                                                }

                                                                if (indexZ < 0)
                                                                {
                                                                    indexZ *= -1;
                                                                }

                                                                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));
                                                                if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                                {

                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                                                                    raylength = 0;
                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        instancedata = new sccsInstancesunitypool.instancedata();
                                                                        instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                                        instancedata.currentInstanceGameObject = null;
                                                                        instancedata.instanceindex = -1;
                                                                        instancedata.enabled = -1;
                                                                        instancedata.swap = -1;
                                                                        instancedata.instanceenabledcounter = -1;
                                                                        instancedata.instanceenabledcounterSwtc = -1;
                                                                        instancedata.instanceenabledcounterMax = -1;

                                                                        sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                                        /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);*/
                                                                    }
                                                                }
                                                            }
                                                            else if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0) //BOTTOM FACE
                                                            {
                                                                Vector3 p = hit.point;


                                                                float posx = (p.x);
                                                                float posy = (p.y);
                                                                float posz = (p.z);


                                                                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                retAdd.position = chunkbytepos;

                                                                int indexX = 0;
                                                                int indexY = 0;
                                                                int indexZ = 0;


                                                                if (posy < 0)
                                                                {
                                                                    posy *= -1;
                                                                }

                                                                //Debug.Log("posz:" + posz);
                                                                if (posy >= 1.0f)
                                                                {
                                                                    indexY = Mathf.RoundToInt(((((posy - Mathf.Round(posy))) * chunkWidth)));
                                                                    //Debug.Log("0indexY:" + indexY);
                                                                    if (indexY < 0)
                                                                    {
                                                                        //indexY *= -1;
                                                                        if (indexY > -5)
                                                                        {
                                                                            indexY *= -1;
                                                                            indexY = (chunkWidth - 1) - indexY;
                                                                            indexY += 1;
                                                                            ////Debug.Log("2indexY:" + indexY);
                                                                        }
                                                                        else
                                                                        {
                                                                            indexY *= -1;
                                                                            ////Debug.Log("3indexY:" + indexY);
                                                                        }
                                                                        //indexY = (chunkWidth - 1) - indexY;
                                                                        ////Debug.Log("4indexY:" + indexY);

                                                                    }
                                                                    else
                                                                    {
                                                                        if (indexY == 0)
                                                                        {
                                                                            //indexY = chunkWidth - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            //Debug.Log("1indexY:" + indexY);
                                                                            if (indexY >= 5)
                                                                            {
                                                                                //indexY -= 1;
                                                                                ////Debug.Log("2indexY:" + indexY);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexY:" + indexY);
                                                                                //indexY -= 1;
                                                                            }
                                                                        }
                                                                    }

                                                                    ////Debug.Log("11indexY:" + indexY);
                                                                }
                                                                else
                                                                {
                                                                    indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                                    //Debug.Log("0indexY:" + indexY);
                                                                    if (indexY < 0)
                                                                    {

                                                                        indexY *= -1;
                                                                        if (indexY >= 5)
                                                                        {
                                                                            ////Debug.Log("2indexY:" + indexY);
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("3indexY:" + indexY);
                                                                        }
                                                                        indexY = (chunkWidth - 1) - indexY;
                                                                        ////Debug.Log("4indexY:" + indexY);

                                                                    }
                                                                    else
                                                                    {
                                                                        if (indexY == 0)
                                                                        {
                                                                            //indexY = chunkWidth - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            //indexY = chunkWidth - 1;
                                                                            //Debug.Log("1indexY:" + indexY);
                                                                            if (indexY >= 5)
                                                                            {
                                                                                //indexY -= 1;
                                                                                ////Debug.Log("2indexY:" + indexY);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexY:" + indexY);
                                                                                //indexY -= 1;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                if (posz < 0)
                                                                {
                                                                    posz *= -1;
                                                                }

                                                                if (posz >= 1.0f)
                                                                {
                                                                    indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));

                                                                }

                                                                if (posx < 0)
                                                                {
                                                                    posx *= -1;
                                                                }

                                                                if (posx >= 1.0f)
                                                                {
                                                                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));

                                                                }
                                                                else
                                                                {
                                                                    //indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                                }

                                                                if (indexX < 0)
                                                                {
                                                                    indexX *= -1;
                                                                }

                                                                if (indexY < 0)
                                                                {
                                                                    indexY *= -1;
                                                                }

                                                                if (indexZ < 0)
                                                                {
                                                                    indexZ *= -1;
                                                                }






                                                                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                //Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));

                                                                if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                                {
                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().sccsSetMap();
                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                                    //currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().CreateChunkFaces();


                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().chunkbuildingswtc = 1;

                                                                    //currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().swtchz = 0;
                                                                    //currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().swtchzBytes = 0;


                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                                    raylength = 0;
                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        instancedata = new sccsInstancesunitypool.instancedata();
                                                                        instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                                        instancedata.currentInstanceGameObject = null;
                                                                        instancedata.instanceindex = -1;
                                                                        instancedata.enabled = -1;
                                                                        instancedata.swap = -1;
                                                                        instancedata.instanceenabledcounter = -1;
                                                                        instancedata.instanceenabledcounterSwtc = -1;
                                                                        instancedata.instanceenabledcounterMax = -1;

                                                                        sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                                        /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);*/
                                                                    }
                                                                }
                                                            }
                                                            else if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0) //TOP FACE
                                                            {
                                                                Vector3 p = hit.point;


                                                                float posx = (p.x);
                                                                float posy = (p.y);
                                                                float posz = (p.z);


                                                                Vector3 chunkbytepos = new Vector3(posx, posy, posz);

                                                                retAdd.position = chunkbytepos;

                                                                int indexX = 0;
                                                                int indexY = 0;
                                                                int indexZ = 0;


                                                                if (posy < 0)
                                                                {
                                                                    posy *= -1;
                                                                }

                                                                ////Debug.Log("posz:" + posz);
                                                                if (posy >= 1.0f)
                                                                {
                                                                    indexY = Mathf.RoundToInt(((((posy - Mathf.Round(posy))) * chunkWidth)));
                                                                    ////Debug.Log("0indexY:" + indexY);
                                                                    if (indexY < 0)
                                                                    {
                                                                        indexY *= -1;
                                                                        if (indexY >= 5)
                                                                        {
                                                                            ////Debug.Log("2indexY:" + indexY);
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("3indexY:" + indexY);
                                                                        }
                                                                        indexY = (chunkWidth - 1) - indexY;
                                                                        ////Debug.Log("4indexY:" + indexY);

                                                                    }
                                                                    else
                                                                    {
                                                                        if (indexY == 0)
                                                                        {
                                                                            indexY = chunkWidth - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("1indexY:" + indexY);
                                                                            if (indexY >= 5)
                                                                            {
                                                                                indexY -= 1;
                                                                                ////Debug.Log("2indexY:" + indexY);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexY:" + indexY);
                                                                                indexY -= 1;
                                                                            }
                                                                        }
                                                                    }

                                                                    ////Debug.Log("11indexY:" + indexY);
                                                                }
                                                                else
                                                                {
                                                                    indexY = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                                    ////Debug.Log("0indexY:" + indexY);
                                                                    if (indexY < 0)
                                                                    {

                                                                        indexY *= -1;
                                                                        if (indexY >= 5)
                                                                        {
                                                                            ////Debug.Log("2indexY:" + indexY);
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("3indexY:" + indexY);
                                                                        }
                                                                        indexY = (chunkWidth - 1) - indexY;
                                                                        ////Debug.Log("4indexY:" + indexY);

                                                                    }
                                                                    else
                                                                    {
                                                                        if (indexY == 0)
                                                                        {
                                                                            indexY = chunkWidth - 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            ////Debug.Log("1indexY:" + indexY);
                                                                            if (indexY >= 5)
                                                                            {
                                                                                indexY -= 1;
                                                                                ////Debug.Log("2indexY:" + indexY);
                                                                            }
                                                                            else
                                                                            {
                                                                                ////Debug.Log("3indexY:" + indexY);
                                                                                indexY -= 1;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                if (posz < 0)
                                                                {
                                                                    posz *= -1;
                                                                }

                                                                if (posz >= 1.0f)
                                                                {
                                                                    indexZ = Mathf.FloorToInt((((posz - Mathf.Floor(posz)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexZ = Mathf.FloorToInt((Mathf.Round(posz * chunkWidth)));
                                                                }

                                                                if (posx < 0)
                                                                {
                                                                    posx *= -1;
                                                                }

                                                                if (posx >= 1.0f)
                                                                {
                                                                    indexX = Mathf.FloorToInt((((posx - Mathf.Floor(posx)) * chunkWidth)));
                                                                }
                                                                else
                                                                {
                                                                    indexX = Mathf.FloorToInt((Mathf.Round(posx * chunkWidth)));
                                                                }

                                                                if (indexX < 0)
                                                                {
                                                                    indexX *= -1;
                                                                }

                                                                if (indexY < 0)
                                                                {
                                                                    indexY *= -1;
                                                                }

                                                                if (indexZ < 0)
                                                                {
                                                                    indexZ *= -1;
                                                                }





                                                                ////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                                ////Debug.Log("indexX: " + (indexX) + " indexY: " + (indexY) + " indexZ: " + (indexZ));
                                                                if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)indexZ) == 1)
                                                                {
                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType, chunkbytepos);

                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                                                                    //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);
                                                                    raylength = 0;
                                                                    if (addfracturedcubeonimpact == 1)
                                                                    {
                                                                        instancedata = new sccsInstancesunitypool.instancedata();
                                                                        instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                                        instancedata.currentInstanceGameObject = null;
                                                                        instancedata.instanceindex = -1;
                                                                        instancedata.enabled = -1;
                                                                        instancedata.swap = -1;
                                                                        instancedata.instanceenabledcounter = -1;
                                                                        instancedata.instanceenabledcounterSwtc = -1;
                                                                        instancedata.instanceenabledcounterMax = -1;

                                                                        sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);
                                                                        /*var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                                        UnityTutorialPooledObject.transform.position = chunkbytepos;

                                                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                                        UnityTutorialPooledObject.SetActive(true);*/
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    raylength++;
                                }
                                tippickaxestopwatch.Restart();
                            }
                        }
                        else
                        {
                            raylength = 0;


                            tippickaxestopwatch.Restart();
                        }

                    }

                }
            }



            /*else
            {
                if (tippickaxestopwatch.Elapsed.Ticks >= 50)
                {
                    for (int r = 0; r < 1; r++)
                    {
                        var ray = new Ray(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f);
                        Debug.DrawRay(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f, Color.white, 0.001f);
                    }
                    tippickaxestopwatch.Restart();
                    raylength = 0;
                }
            }*/

            /*
            if (raylength <= raycounterLoopMax)
            {
                for (int r = 0; r < 1; r++)
                {
                    var ray = new Ray(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f);
                    Debug.DrawRay(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f, Color.white, 0.001f);
                }

            }
            else
            {
                for (int r = 0; r < 1; r++)
                {
                    var ray = new Ray(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f);
                    Debug.DrawRay(this.transform.position + (transform.forward * (raylength * 0.01f)), transform.forward * 0.005f, Color.white, 0.001f);
                }
                raylength = 0;
            }
            raylength++;*/



            /*if (this.transform.name == "tip")//if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, 0.1f))
                {
                    if (hit.transform.tag == "collisionObject")
                    {
                        if (GetComponent<Fracture4>() != null)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }*/
            //counterForByteChange = 0;
            //counterForByteChange++;
        }
        else if (swtcForTypeOfInteract == 1)
        {
            raycounterLoopMax = 750; //750
            //SCCSRAYCASTLASERCHUNKDIGGER


            //ray = new Ray(transform.position, transform.forward);// Camera.main.ScreenPointToRay(Input.mousePosition);


            //Debug.DrawRay(transform.position, transform.forward * 25, Color.black, 0.001f);

            //var someTouch0 = Input.GetTouch(0);
            //////Debug.Log(""+ someTouch0);

            bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

            if (buttonPressedLeft)
            {
                ////Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
            }
            if (buttonPressedRight)
            {
                ////Debug.Log("buttonPressedRight:" + buttonPressedRight);
            }



            if (buttonPressedLeft || buttonPressedRight)
            {

            }
            else
            {
                //gottarget = 0;
                raylength = 0;
                someswtc = 0;

                lastFrameRayPosSwtc = 0;
                currentFrameRayPosSwtc = 0;

                tippickaxestopwatch.Restart();
            }

            if (counterForByteChange == 0)// counterForByteChangeMax)
            {
                if (InitcounterForIkFootPlacementSwtc == 1)
                {
                    if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
                    {
                        if (raycounterSwtc == 0 || raycounterSwtc == 1)
                        {
                            //if (tippickaxestopwatch.Elapsed.Ticks >= 1)
                            {
                                for (int r = 0; r < 1; r++)
                                {
                                loopagain:

                                    if (currentFrameRayPosSwtc == 0)
                                    {
                                        currentRayPosition = foot.transform.position;
                                        //lastFrameRayDirForward = pickaxetiptransform.transform.forward;
                                        currentFrameRayPosSwtc = 1;
                                    }

                                    //Debug.Log("test1");
                                    if (raylength < raycounterLoopMax)
                                    {
                                        //Debug.Log("test3");

                                        Vector3 rayFrameInitPos = foot.transform.position;
                                        Vector3 rayInityDir = footTarget.forward * someRayLength;

                                        //Debug.Log("test4");
                                        //someray = new Ray(footTarget.transform.position + ((footTarget.forward * (raylength * someRayLength))), footTarget.forward * someRayLength0025f);

                                        //Debug.DrawRay(foot.transform.position + (foot.forward * (raylength * 0.025f)), foot.forward * 0.0075f, Color.red, someRayLength); 

                                        //var somespherecasthitbool = Physics.SphereCast(footTarget.transform.position, someRayLength, footTarget.forward, out spherecasthit, someRayLength, layerMask);
                                        //var someraycasthitbool = Physics.Raycast(someray, out hit, (raylength * someRayLength), layerMask);

                                        Vector3 rayFrameInitPos0 = currentRayPosition + (footTarget.forward * (raylength * someRayLength));
                                        //Vector3 rayInityDir0 = foot.forward * 0.075f;

                                        //Debug.Log("test4");
                                        //var ray = new Ray(foot.transform.position + (foot.forward * (raylength * 0.25f)), foot.forward * 0.075f);
                                        //Debug.DrawRay(foot.transform.position + (foot.forward * (raylength * 0.25f)), foot.forward * 0.075f, Color.red, someRayLength);
                                        //var somespherecasthitbool = Physics.SphereCast(foot.transform.position, 0.05f, foot.forward, out spherecasthit, 0.05f, layerMask);
                                        //var someraycasthitbool = Physics.Raycast(ray, out hit, (raylength * 0.5f), layerMask);

                                        //Vector3 rayFrameInitPos0 = ray.origin;

                                        //int posnot0roundedx = Mathf.RoundToInt(rayFrameInitPos0.x);
                                        //int posnot0roundedy = Mathf.RoundToInt(rayFrameInitPos0.y);
                                        //int posnot0roundedz = Mathf.RoundToInt(rayFrameInitPos0.z);

                                        int posnot0roundedx = 0;
                                        int posnot0roundedy = 0;
                                        int posnot0roundedz = 0;

                                        /*if (rayFrameInitPos0.x < 0)
                                        {
                                            posnot0roundedx = Mathf.FloorToInt(rayFrameInitPos0.x * 20) * 20;
                                        }
                                        else
                                        {
                                            posnot0roundedx = Mathf.FloorToInt(rayFrameInitPos0.x * 20) * 20;
                                        }

                                        if (rayFrameInitPos0.y < 0)
                                        {
                                            posnot0roundedy = Mathf.FloorToInt(rayFrameInitPos0.y * 20) * 20;
                                        }
                                        else
                                        {
                                            posnot0roundedy = Mathf.FloorToInt(rayFrameInitPos0.y * 20) * 20;
                                            //posnot0roundedy -= 1;
                                        }

                                        if (rayFrameInitPos0.z < 0)
                                        {
                                            posnot0roundedz = Mathf.FloorToInt(rayFrameInitPos0.z * 20) * 20;
                                            //posnot0roundedz += 1;
                                        }
                                        else
                                        {
                                            posnot0roundedz = Mathf.RoundToInt(rayFrameInitPos0.z * 20) * 20;
                                        }*/

                                        //Debug.Log("" + (Mathf.Ceil(-3.5f)));



                                        //Vector3 neighboorPos = Vector3.zero;


                                        //neighboorPos.x = Mathf.FloorToInt(Mathf.Floor(neighboorPos.x) / realplanetwidth);           
                                        //neighboorPos.y = Mathf.FloorToInt(Mathf.Floor(neighboorPos.y) / realplanetwidth);
                                        //neighboorPos.z = Mathf.FloorToInt(Mathf.Floor(neighboorPos.z) / realplanetwidth);



                                        //float someremainsx = rayFrameInitPos0.x - Mathf.Floor(rayFrameInitPos0.x); // 23.5432f - 23.0f
                                        int neighboorindexx = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.x) / realplanetwidth); //   23.0f/2 = 11

                                        int neighboorindexy = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.y) / realplanetwidth); //   23.0f/2 = 11

                                        int neighboorindexz = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.z) / realplanetwidth); //   23.0f/2 = 11



                                        /*if (rayFrameInitPos0.x < 0)
                                        {


                                            var remains = rayFrameInitPos0.x - Mathf.Floor(rayFrameInitPos0.x);
                                            var someDiv = Mathf.Floor(rayFrameInitPos0.x) / realplanetwidth;
                                            //var somediv = remains / realplanetwidth;
                                            neighboorPos.x = someDiv;
                                        }
                                        else
                                        {
                                            neighboorPos.x = Mathf.FloorToInt(rayFrameInitPos0.x * 20) * 20;
                                        }

                                        if (rayFrameInitPos0.y < 0)
                                        {
                                            neighboorPos.y = Mathf.FloorToInt(rayFrameInitPos0.y * 20) * 20;
                                        }
                                        else
                                        {
                                            neighboorPos.y = Mathf.FloorToInt(rayFrameInitPos0.y * 20) * 20;
                                            //posnot0roundedy -= 1;
                                        }

                                        if (rayFrameInitPos0.z < 0)
                                        {
                                            neighboorPos.z = Mathf.FloorToInt(rayFrameInitPos0.z * 20) * 20;
                                            //posnot0roundedz += 1;
                                        }
                                        else
                                        {
                                            neighboorPos.z = Mathf.RoundToInt(rayFrameInitPos0.z * 20) * 20;
                                        }*/















                                        /*
                                        posnot0roundedy = Mathf.RoundToInt(rayFrameInitPos0.y);
                                        posnot0roundedz = Mathf.RoundToInt(rayFrameInitPos0.z);
                                        if (posnot0roundedx < 0)
                                        {
                                            posnot0roundedx -= 1;
                                        }

                                        if (posnot0roundedy < 0)
                                        {
                                            posnot0roundedy += 1;
                                        }
                                        else //if (posnot0roundedy < 0)
                                        {
                                            //posnot0roundedy += 1;
                                        }

                                        if (posnot0roundedz < 0)
                                        {
                                            posnot0roundedz += 1;
                                        }*/

                                        //sphere.transform.position = new Vector3(posnot0roundedx, posnot0roundedy, posnot0roundedz);

                                        //Quaternion someQuat = Quaternion.LookRotation(lastFrameRayDirForward, lastFrameRayInitDirUp);

                                        //Debug.DrawRay(lastFrameRayPos, lastFrameRayDirForward * someRayLength, Color.red, 5);

                                        //Vector3 neighboorPos = new Vector3(posnot0roundedx, posnot0roundedy, posnot0roundedz);

                                        if (lastFrameRayPosSwtc == 0)
                                        {
                                            lastFrameRayInitDirUp = footTarget.transform.up;
                                            lastFrameRayDirForward = pickaxetiptransform.transform.forward;
                                            lastFrameRayPosSwtc = 1;
                                        }

                                        sphere.transform.position = new Vector3(rayFrameInitPos0.x, rayFrameInitPos0.y, rayFrameInitPos0.z);
                                        sphere.transform.rotation = Quaternion.LookRotation(lastFrameRayDirForward, lastFrameRayInitDirUp);

                                        int gottarget = 0;

                                        if (gottarget == 0)
                                        {
                                            //Debug.Log("neighboorPos:" + neighboorPos);
                                            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk(neighboorindexx, neighboorindexy, neighboorindexz) != null)
                                            {
                                                //Debug.Log("==count==");
                                                mainChunkFinal currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk(neighboorindexx, neighboorindexy, neighboorindexz);// posnotroundedx, posnotroundedy, posnotroundedz);
                                                                                                                                                                                                        //
                                                currentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                                                //sphere.transform.rotation = Quaternion.FromToRotation(lastFrameRayDirForward,lastFrameRayInitDirUp);// foot.forward);

                                                //posnot0roundedx = Mathf.FloorToInt(rayFrameInitPos0.x);
                                                //posnot0roundedy = Mathf.FloorToInt(rayFrameInitPos0.y);
                                                //posnot0roundedz = Mathf.FloorToInt(rayFrameInitPos0.z);




                                                /*float somex = 0;
                                                float somey = 0;
                                                float somez = 0;

                                                if (rayFrameInitPos0.x < 0)
                                                {
                                                    somex = Mathf.Floor(rayFrameInitPos0.x);
                                                }
                                                else
                                                {
                                                    somex = Mathf.Floor(rayFrameInitPos0.x);
                                                }

                                                if (rayFrameInitPos0.y < 0)
                                                {
                                                    somey = Mathf.Floor(rayFrameInitPos0.y);
                                                }
                                                else
                                                {
                                                    somey = Mathf.Floor(rayFrameInitPos0.y);
                                                }


                                                if (rayFrameInitPos0.z < 0)
                                                {
                                                    somez = Mathf.Floor(rayFrameInitPos0.z);
                                                }
                                                else
                                                {
                                                    somez = Mathf.Floor(rayFrameInitPos0.z);
                                                }*/



                                                //indexx = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                //indexy = Mathf.RoundToInt((Mathf.Round(posy * chunkWidth)));
                                                //indexz = Mathf.RoundToInt((Mathf.Round(posz * chunkWidth)));

                                                //Debug.Log("posx:" + posx);
                                                /*
                                                


                                                /*
                                                if (posx < 0)
                                                {
                                                    //indexx = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                    indexx = Mathf.RoundToInt(((((posx - Mathf.Floor(posx))) * chunkWidth)));

                                                    //Debug.Log("01indexx:" + indexx);
                                                    if (indexx == 0)
                                                    {
                                                        indexx = 0;
                                                    }
                                                    else
                                                    {
                                                        //Debug.Log("01indexx:" + indexx);

                                                        if (indexx < 0)
                                                        {
                                                            //indexx *= -1;

                                                            if (indexx >= -4 && indexx < 0)
                                                            {
                                                                //Debug.Log("2indexx:" + indexx);
                                                                indexx *= -1;
                                                                indexx = (chunkWidth) - indexx;
                                                            }
                                                            else
                                                            {

                                                            }
                                                        }
                                                        else
                                                        {

                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //indexx = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                    indexx = Mathf.RoundToInt(((((posx - Mathf.Floor(posx))) * chunkWidth)));
                                                    //Debug.Log("00indexx:" + indexx);
                                                    if (indexx >= 0 && indexx < 5)
                                                    {
                                                        //Debug.Log("0");
                                                        //indexx = ((chunkWidth / 2) - indexx);
                                                    }
                                                    else if (indexx >= 5)
                                                    {
                                                        //Debug.Log("1");
                                                    }
                                                    else if (indexx < 0 && indexx >= -5)
                                                    {
                                                        //Debug.Log("2");
                                                    }
                                                    else if (indexx < -5)
                                                    {
                                                        //Debug.Log("3");
                                                    }
                                                }






                                                if (posy < 0)
                                                {
                                                    //indexy = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                    indexy = Mathf.RoundToInt(((((posy - Mathf.Floor(posy))) * chunkWidth)));

                                                    //Debug.Log("01indexy:" + indexy);
                                                    if (indexy == 0)
                                                    {
                                                        indexy = 0;
                                                    }
                                                    else
                                                    {
                                                        //Debug.Log("01indexy:" + indexy);

                                                        if (indexy < 0)
                                                        {
                                                            //indexy *= -1;

                                                            if (indexy >= -4 && indexy < 0)
                                                            {
                                                                //Debug.Log("2indexy:" + indexy);
                                                                indexy *= -1;
                                                                indexy = (chunkWidth) - indexy;
                                                            }
                                                            else
                                                            {

                                                            }
                                                        }
                                                        else
                                                        {

                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //indexy = Mathf.RoundToInt((Mathf.Round(posx * chunkWidth)));
                                                    indexy = Mathf.RoundToInt(((((posy - Mathf.Floor(posy))) * chunkWidth)));
                                                    //Debug.Log("00indexy:" + indexy);
                                                    if (indexy >= 0 && indexy < 5)
                                                    {
                                                        //Debug.Log("0");
                                                        //indexy = ((chunkWidth / 2) - indexy);
                                                    }
                                                    else if (indexy >= 5)
                                                    {
                                                        //Debug.Log("1");
                                                    }
                                                    else if (indexy < 0 && indexy >= -5)
                                                    {
                                                        //Debug.Log("2");
                                                    }
                                                    else if (indexy < -5)
                                                    {
                                                        //Debug.Log("3");
                                                    }
                                                }




                                                //Debug.Log("posz:" + posz);
                                                if (posz < 0)
                                                {
                                                    indexz = Mathf.RoundToInt(((((posz - Mathf.Floor(posz))) * chunkWidth)));

                                                    //Debug.Log("01indexz:" + indexz);
                                                    if (indexz == 0)
                                                    {
                                                        indexz = 0;
                                                    }
                                                    else
                                                    {
                                                        //Debug.Log("01indexz:" + indexz);

                                                        if (indexz < 0)
                                                        {
                                                            //indexz *= -1;

                                                            if (indexz >= -4 && indexz < 0)
                                                            {
                                                                //Debug.Log("2indexz:" + indexz);
                                                                indexz *= -1;
                                                                indexz = (chunkWidth) - indexz;
                                                            }
                                                            else
                                                            {
                                                                //Debug.Log("3indexz:" + indexz);


                                                                if (indexz == 0)
                                                                {
                                                                    indexz = 0;
                                                                }
                                                                /*else if (indexz == (chunkWidth - 1))
                                                                {
                                                                    indexz = (chunkWidth - 1) - indexz;
                                                                }
                                                                else
                                                                {
                                                                    indexz *= -1;
                                                                    indexz = (chunkWidth - 1) - indexz;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {

                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //indexz = Mathf.FloorToInt((Mathf.Floor(posz * chunkWidth)));
                                                    indexz = Mathf.RoundToInt(((((posz - Mathf.Floor(posz))) * chunkWidth)));

                                                    //Debug.Log("00indexz:" + indexz);
                                                    if (indexz >= 0 && indexz < 5)
                                                    {
                                                        //Debug.Log("0");
                                                        //indexz = ((chunkWidth / 2) - indexz);
                                                    }
                                                    else if (indexz >= 5)
                                                    {
                                                        //Debug.Log("1");
                                                    }
                                                    else if (indexz < 0 && indexz >= -5)
                                                    {
                                                        //Debug.Log("2");
                                                    }
                                                    else if (indexz < -5)
                                                    {
                                                        //Debug.Log("3");
                                                    }
                                                }*/


                                                /*float somex = Mathf.Floor(rayFrameInitPos0.x * 10) / 10;
                                                float somey = Mathf.Floor(rayFrameInitPos0.y * 10) / 10;
                                                float somez = Mathf.Floor(rayFrameInitPos0.z * 10) / 10;

                                                Vector3 p = new Vector3(somex, somey, somez);

                                                float posx = (p.x);
                                                float posy = (p.y);
                                                float posz = (p.z);

                                                Vector3 chunkbytepos = new Vector3(posx, posy, posz);*/

                                                //retAdd.position = chunkbytepos;



                                                //float someremainsx = rayFrameInitPos0.x - Mathf.Floor(rayFrameInitPos0.x); // 23.5432f - 23.0f
                                                int somechunkdivx = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.x) / realplanetwidth); //   23.0f/2 = 11
                                                int somechunkworldindexx = somechunkdivx * realplanetwidth; //11 * 2 = 22
                                                int indexx = Mathf.FloorToInt(Mathf.FloorToInt((rayFrameInitPos0.x - somechunkworldindexx) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15


                                                int somechunkdivy = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.y) / realplanetwidth); //   23.0f/2 = 11
                                                int somechunkworldindexy = somechunkdivy * realplanetwidth; //11 * 2 = 22
                                                int indexy = Mathf.FloorToInt(Mathf.FloorToInt((rayFrameInitPos0.y - somechunkworldindexy) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15


                                                int somechunkdivz = Mathf.FloorToInt(Mathf.Floor(rayFrameInitPos0.z) / realplanetwidth); //   23.0f/2 = 11
                                                int somechunkworldindexz = somechunkdivz * realplanetwidth; //11 * 2 = 22
                                                int indexz = Mathf.FloorToInt(Mathf.FloorToInt((rayFrameInitPos0.z - somechunkworldindexz) * 10)); // (23.5432-22)*10 = 1.5432*10 = 15.432 floored = 15



                                                Vector3 chunkbytepos = new Vector3(indexx, indexy, indexz);

                                                /*float tempx = indexx;
                                                float tempy = indexy;
                                                float tempz = indexz;

                                                if (tempx < 0)
                                                {
                                                    tempx *= -1;

                                                    indexx = Mathf.RoundToInt(((((tempx - Mathf.Floor(tempx))) * chunkWidth)));

                                                    if (indexx == 0)
                                                    {
                                                        indexx = 0;
                                                    }
                                                    else
                                                    {
                                                        //Debug.Log("01indexx:" + indexx);

                                                        if (indexx < 0)
                                                        {
                                                            Debug.Log("test0");
                                                            //indexx *= -1;

                                                            if (indexx >= (Mathf.RoundToInt(chunkWidth * 0.5f) - 1) * -1 && indexx < 0)
                                                            {
                                                                //Debug.Log("2indexx:" + indexx);
                                                                indexx *= -1;
                                                                indexx = (chunkWidth) - indexx;
                                                            }
                                                            else
                                                            {

                                                            }
                                                        }
                                                        else
                                                        {

                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    indexx = Mathf.RoundToInt(((((tempx - Mathf.Floor(tempx))) * chunkWidth)));
                                                }

                                                if (tempy < 0)
                                                {
                                                    tempy *= -1;
                                                    indexy = Mathf.RoundToInt(((((tempy - Mathf.Floor(tempy))) * chunkWidth)));

                                                    Debug.Log("y0:" + indexy);
                                                    if (indexy < 0)
                                                    {
                                                        Debug.Log("test1");
                                                        //indexx *= -1;

                                                        if (indexy >= (Mathf.RoundToInt(chunkWidth * 0.5f) - 1) * -1 && indexy < 0)
                                                        {
                                                            Debug.Log("y1:" + indexy);
                                                            //Debug.Log("2indexx:" + indexx);
                                                            indexy *= -1;
                                                            indexy = (chunkWidth) - indexy;
                                                            Debug.Log("y2:" + indexy);
                                                        }
                                                        else
                                                        {

                                                        }
                                                    }
                                                    else
                                                    {
                                                        indexy = (chunkWidth) - indexy;
                                                    }
                                                }
                                                else
                                                {
                                                    indexy = Mathf.RoundToInt(((((tempy - Mathf.Floor(tempy))) * chunkWidth)));
                                                }

                                                if (tempz < 0)
                                                {
                                                    tempz *= -1;
                                                    indexz = Mathf.RoundToInt(((((tempz - Mathf.Floor(tempz))) * chunkWidth)));


                                                    if (indexz < 0)
                                                    {
                                                        //indexx *= -1;

                                                        if (indexz >= (Mathf.RoundToInt(chunkWidth * 0.5f) - 1) * -1 && indexz < 0)
                                                        {
                                                            //Debug.Log("2indexx:" + indexx);
                                                            indexz *= -1;
                                                            indexz = (chunkWidth) - indexz;
                                                        }
                                                        else
                                                        {

                                                        }
                                                    }
                                                    else
                                                    {

                                                    }
                                                }
                                                else
                                                {
                                                    indexz = Mathf.RoundToInt(((((tempz - Mathf.Floor(tempz))) * chunkWidth)));
                                                }*/















                                                //Vector3 chunkbytepos = new Vector3(indexx, indexy, indexz);

                                                if (indexx < 0)
                                                {
                                                    indexx *= -1;
                                                }

                                                if (indexy < 0)
                                                {
                                                    indexy *= -1;
                                                }

                                                if (indexz < 0)
                                                {
                                                    indexz *= -1;
                                                }

                                                retAdd.position = rayFrameInitPos0;

                                                //////Debug.Log("x: " + (x) + " y: " + (y) + " z: " + (z));
                                                //Debug.Log("indexx: " + (indexx) + " indexy: " + (indexy) + " indexz: " + (indexz));
                                                int tempindexx = indexx;
                                                int tempindexy = indexy;
                                                int tempindexz = indexz;


                                                if (tempindexx < 0 || tempindexy < 0 || tempindexz < 0)
                                                {

                                                }

                                                if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexx, (int)indexy, (int)indexz) == 1)
                                                {
                                                    //Debug.Log("byte == 1");
                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexx, (int)indexy, (int)indexz, activeBlockType, chunkbytepos);

                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().sccsSetMap();
                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                                                    //currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().CreateChunkFaces();
                                                    currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().chunkbuildingswtc = 1;





                                                    if (currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().vertexlist.Count > 0)
                                                    {
                                                        //currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

                                                        //setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexy, indexz);
                                                        if (addfracturedcubeonimpact == 1)
                                                        {
                                                            instancedata = new sccsInstancesunitypool.instancedata();
                                                            chunkbytepos.x += planeSize * 0.5f;
                                                            chunkbytepos.y += planeSize * 0.5f;
                                                            chunkbytepos.z += planeSize * 0.5f;
                                                            instancedata.InitInstancePositionToLinkTo = chunkbytepos;
                                                            instancedata.currentInstanceGameObject = null;
                                                            instancedata.instanceindex = 0;
                                                            instancedata.enabled = -1;
                                                            instancedata.swap = -1;
                                                            instancedata.instanceenabledcounter = 0;
                                                            instancedata.instanceenabledcounterSwtc = -1;
                                                            instancedata.instanceenabledcounterMax = 1;
                                                            sccsInstancesunitypool.current.addObjectInstanceToDrawingList(instancedata);

                                                            //float somexx = Mathf.Floor(currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().chunkPos.x);
                                                            //float someyy = Mathf.Floor(currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().chunkPos.y);
                                                            //float somezz = Mathf.Floor(currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().chunkPos.z);
                                                            //new Vector3(somexx, someyy, somezz) 


                                                            //var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                                            //var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                                            //UnityTutorialPooledObject.transform.position = chunkbytepos;// + new Vector3((indexX * 0.1f), (indexY * 0.1f), (indexZ * 0.1f)); ;// retAdd.position; new Vector3((int)indexX, (int)indexY, (int)indexZ);


                                                            //chunkbytepos.x += planeSize * 0.5f;
                                                            //chunkbytepos.y += planeSize * 0.5f;
                                                            //chunkbytepos.z += planeSize * 0.5f;

                                                            //UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                                            //UnityTutorialPooledObject.SetActive(true);
                                                        }
                                                        gottarget = 1;
                                                        someswtc++;
                                                    }
                                                    else
                                                    {

                                                        //currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                                        //lastFrameRayPos = currentRayPosition;
                                                        gottarget = 0;
                                                        someswtc++;
                                                    }

                                                }
                                                else
                                                {
                                                    int maxraylengthinverysmallunitswhenrayhitschunk = 750;
                                                    if (someswtc < maxraylengthinverysmallunitswhenrayhitschunk)
                                                    {
                                                        /*if (someswtc == 0)
                                                        {
                                                            for (int rever = 0; rever < 25; rever++)
                                                            {
                                                                currentRayPosition += (-footTarget.forward * (raylength * someRayLength));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                                        }*/
                                                        //currentRayPosition += (footTarget.forward * (raylength * someRayLength));

                                                        //Debug.Log("byte == 0");
                                                        //currentFrameRayPosSwtc = 0;

                                                        //lastFrameRayPosSwtc = 1;
                                                        //Debug.Log("someswtc:" + someswtc);
                                                        //tippickaxestopwatch.Restart();

                                                        //lastFrameRayPos = currentRayPosition;
                                                        gottarget = 0;
                                                        someswtc++;
                                                        tippickaxestopwatch.Restart();

                                                        //currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                                        //goto loopagain;

                                                    }
                                                    else
                                                    {
                                                        //lastFrameRayPos = currentRayPosition;
                                                        gottarget = 0;
                                                        raylength = 0;
                                                        someswtc = 0;
                                                        tippickaxestopwatch.Restart();

                                                        //currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                                        lastFrameRayPosSwtc = 0;
                                                        currentFrameRayPosSwtc = 0;
                                                    }






                                                    //Debug.Log("someswtc:" + someswtc);





                                                    /*for (int x = -1;x <= 1;x++)
                                                    {
                                                        for (int y = -1; y <= 1; y++)
                                                        {
                                                            for (int z = -1; z <= 1; z++)
                                                            {

                                                            }
                                                        }
                                                    }*/
                                                }

                                                //someray.origin;
                                                //lastFrameRayDirForward = someray.direction;

                                                //currentRayPosition = footTarget.transform.position;
                                                /*for (int x = -1; x <= 1; x++)
                                                {
                                                    for (int y = -1; y <= 1; y++)
                                                    {
                                                        for (int z = -1; z <= 1; z++)
                                                        {

                                                            //currentRayPosition = footTarget.transform.position;
                                                            //lastFrameRayPos = currentRayPosition;
                                                        }
                                                    }
                                                }*/
                                            }
                                            else
                                            {
                                                //chunk is null
                                                //Debug.Log("someswtc:" + someswtc);

                                                //someswtc = 0;
                                                raylength++;
                                                tippickaxestopwatch.Restart();

                                            }

                                            currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                            lastFrameRayPos = currentRayPosition;
                                        }
                                        else
                                        {
                                            gottarget = 0;
                                            raylength = 0;
                                            someswtc = 0;
                                            tippickaxestopwatch.Restart();

                                            //currentRayPosition += (footTarget.forward * (raylength * someRayLength));
                                            lastFrameRayPosSwtc = 0;
                                            currentFrameRayPosSwtc = 0;
                                        }
                                        /*if (gottarget == 0)
                                        {
                                            lastFrameRayPos = currentRayPosition;// someray.origin;
                                            //lastFrameRayDirForward = someray.direction;
                                        }*/

                                    }
                                    else
                                    {
                                        someswtc = 0;
                                        raylength = 0;
                                        lastFrameRayPosSwtc = 0;
                                        currentFrameRayPosSwtc = 0;
                                        tippickaxestopwatch.Restart();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else if (swtcForTypeOfInteract == 2)
        {
            if (counterForIkFootPlacement <= counterForIkFootPlacementMax)
            {
                var ray = new Ray(transform.position, transform.forward);

                RaycastHit hit;
                Debug.DrawRay(transform.position, transform.forward * 1, Color.green, someRayLength);

                /*if (Physics.Raycast(ray, out hit, 0.25f))
                {

                    //footTarget.transform.position = hit.point;

                    /*if (hit.transform.tag == "collisionObject")
                    {
                        if (GetComponent<Fracture4>() != null)
                        {

                        }
                        else
                        {

                        }
                    }
                }*/

                ray = new Ray(legstaticpivot.position, transform.forward);

                //RaycastHit hittwo;
                Debug.DrawRay(transform.position, transform.forward * 1, Color.green, someRayLength);

                if (Physics.Raycast(ray, out hit, totallegLength, layerMask))
                {
                    if (hit.transform.tag == "collisionObject")
                    {
                        Vector3 tempDir = legstaticpivot.position - footTarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                        //IdleStandingTargetPositionVariableLength

                        if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                        {
                            footTarget.position = IdleStandingTargetPositionMax;
                            tempDir.Normalize();
                            //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                            Vector3 tempVect = (legstaticpivot.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
                            //MOVINGPOINTER.X = tempVect.X;
                            //MOVINGPOINTER.Y = tempVect.Y;
                            //MOVINGPOINTER.Z = tempVect.Z;
                            footTarget.position = tempVect;// hit.point;
                        }
                        else
                        {
                            footTarget.position = hit.point + (tempDir * foot.localScale.y);
                        }



                        /*if (tempDir.magnitude < (totallegLength * 0.5f))
                        {
                            footTarget.position = IdleStandingTargetPositionMin;
                        }
                        else
                        {
                            footTarget.position = hit.point;
                        }*/

                    }
                }
                counterForIkFootPlacement = 0;
            }
            counterForIkFootPlacement++;
        }
    }

    int counterForIkFootPlacement = 0;
    int counterForIkFootPlacementMax = 100;
    int counterForIkFootPlacementSwtc = 0;

    float someRayLength = 0.0001f;


    /*||
   hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0 ||
   hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0 ||
   hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1 ||
   hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0 ||
   hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0*/
    public void setAdjacentChunks(mainChunkFinal currentChunk, Vector3 pos, int indexX, int indexY, int indexZ)
    {
        int width = currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().width;
        int height = currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().height;
        int depth = currentChunk.planetchunk.GetComponent<sccslodchunkfinal>().depth;

        //////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

        if (indexX == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x - 4, (int)pos.y, (int)pos.z) != null)
            {
                mainChunkFinal adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x - 4, (int)pos.y, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)width - 1, (int)indexY, (int)indexZ) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)width - 1, (int)indexY, (int)indexZ, 1, pos);

                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                }
            }
        }

        if (indexX == width - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x + 4, (int)pos.y, (int)pos.z) != null)
            {
                mainChunkFinal adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x + 4, (int)pos.y, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)0, (int)indexY, (int)indexZ) == 1)
                {
                    //////Debug.Log("adjacent chunk right exists");
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)0, (int)indexY, (int)indexZ, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                }
            }
        }

        if (indexY == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x, (int)pos.y - 4, (int)pos.z) != null)
            {
                mainChunkFinal adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x, (int)pos.y - 4, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)height - 1, (int)indexZ) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)height - 1, (int)indexZ, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                }
            }
        }

        if (indexY == height - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x, (int)pos.y + 4, (int)pos.z) != null)
            {
                mainChunkFinal adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x, (int)pos.y + 4, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)0, (int)indexZ) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)0, (int)indexZ, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                }
            }
        }

        if (indexZ == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x, (int)pos.y, (int)pos.z - 4) != null)
            {
                mainChunkFinal adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x, (int)pos.y, (int)pos.z - 4);

                if (adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)depth - 1) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)depth - 1, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                }
            }
        }

        if (indexZ == depth - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x, (int)pos.y, (int)pos.z + 4) != null)
            {
                mainChunkFinal adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)pos.x, (int)pos.y, (int)pos.z + 4);

                if (adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().GetByte((int)indexX, (int)indexY, (int)0) == 1)
                {
                    //////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)indexX, (int)indexY, (int)0, 1, pos);
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();
                }
            }
        }
    }
}



/*
int gottarget = 0;
for (int x = -1; x <= 1; x++)
{
    for (int y = -1; y <= 1; y++)
    {
        for (int z = -1; z <= 1; z++)
        {

            Vector3 neighboorPos = new Vector3(x + posnot0roundedx, y + posnot0roundedy, z + posnot0roundedz);

            if (gottarget == 0)
            {
                if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)neighboorPos.x, (int)neighboorPos.y, (int)(neighboorPos.z)) != null)
                {
                    //Debug.Log("==count==");
                    mainChunkFinal currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)neighboorPos.x, (int)neighboorPos.y, (int)(neighboorPos.z));// posnotroundedx, posnotroundedy, posnotroundedz);

                    currentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    lastFrameRayPos = someray.origin;
                    gottarget = 1;
                }
            }
        }
    }
}*/


/*int posnotroundedx = Mathf.FloorToInt(rayFrameInitPos.x);
int posnotroundedy = Mathf.FloorToInt(rayFrameInitPos.y);
int posnotroundedz = Mathf.FloorToInt(rayFrameInitPos.z);

for (int x = -1; x <= 1; x++)
{
    for (int y = -1; y <= 1; y++)
    {
        for (int z = -1; z <= 1; z++)
        {

            Vector3 neighboorPos = new Vector3(x + posnotroundedx, y + posnotroundedy, z + posnotroundedz);

            if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)neighboorPos.x, (int)neighboorPos.y, (int)(neighboorPos.z)) != null)
            {
                //Debug.Log("==count==");
                mainChunkFinal currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)neighboorPos.x, (int)neighboorPos.y, (int)(neighboorPos.z));// posnotroundedx, posnotroundedy, posnotroundedz);

                currentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
            }
        }
    }
}*/

/*if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)posnotroundedx, (int)posnotroundedy, (int)(posnotroundedz)) != null)
{
    //Debug.Log("==count==");
    mainChunkFinal currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)posnotroundedx, (int)posnotroundedy, (int)(posnotroundedz));// posnotroundedx, posnotroundedy, posnotroundedz);

    currentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
}*/


//spherecasthit.triangleIndex                      
//if (Physics.Raycast(ray, out hit, 10000, layerMask))


//by robertbu
/*//https://answers.unity.com/questions/540888/converting-mouse-position-to-world-stationary-came.html 
if (Input.GetMouseButtonDown(0))
{
    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit))
    {
        //hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y,(int)hit.transform.position.z) != null)
        {
            var chunkX = (int)(Mathf.Round(hit.point.x * planeSize) / planeSize);
            var chunkY = (int)(Mathf.Round(hit.point.y * planeSize) / planeSize);
            var chunkZ = (int)(Mathf.Round(hit.point.z * planeSize) / planeSize);

            ////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);
            retDel.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset * 3);
            //yoMan = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset * -2);
            //retDel.position = p;
        }
    }
}*/



/*Vector3 p = hit.point - hit.normal / 4;
float offset = planeSize / 2;
//float offset = 0;


float offset2 = planeSize / 2;

if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0)
{
    //////Debug.Log("yo0");

    retDel.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset * multiplicator, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset);
    //retDel.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Round(p.y * suppressorPos) / suppressorPos) + offset * multiplicator, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset * multiplicatorReticle, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Round(p.y * suppressorPos) / suppressorPos) + offset * 5, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);
}

if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1)
{
    //////Debug.Log("yo1");
    retDel.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset * 3);
    yoMan = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset * 5);
    yoMan1 = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset * 3);
}

if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0)
{
    //////Debug.Log("yo2");
    retDel.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset * 3, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset * 3, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset * 5, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset * 5, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset);
}


if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0)
{
    //////Debug.Log("yo2");
    retDel.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) - offset * 3, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) - offset * 3, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) - offset * 5, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) - offset * 5, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset);
}

if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1)
{
    //////Debug.Log("yo1");

    retDel.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset * 3);
    yoMan = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset * 5);

    retAdd.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset * 5);
    yoMan1 = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) + offset, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset * 6);

}


if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0)
{
    //////Debug.Log("yo1");
    retDel.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) - offset * 5, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) - offset * 3, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) - offset * 5, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * realplanetwidth) / realplanetwidth) + offset, (Mathf.Round(p.y * realplanetwidth) / realplanetwidth) - offset * 5, (Mathf.Ceil(p.z * realplanetwidth) / realplanetwidth) + offset);

}



var chunkX = (int)(Mathf.Round(hit.transform.position.x * realplanetwidth) / realplanetwidth);
var chunkY = (int)(Mathf.Round(hit.transform.position.y * realplanetwidth) / realplanetwidth);
var chunkZ = (int)(Mathf.Round(hit.transform.position.z * realplanetwidth) / realplanetwidth);

//////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

if (planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
{

    //////Debug.Log("==count==");
    mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderGen2>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z);

    if (Input.GetMouseButtonDown(0))
    {
        int x = (int)(((yoMan1.x * 1) / 1) / 1); //WORKING
        int y = (int)(((yoMan1.y * 1) / 1) / 1);//WORKING
        int z = (int)(((yoMan1.z * 1) / 1) / 1);//WORKING
                                           //terrain1.GetChunk(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);

        var planetchunk = hit.transform;
        planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)x, (int)y, (int)z, activeBlockType);

        planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
        planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

        setAdjacentChunks(currentChunk, hit, x, y, z);
    }


    if (Input.GetMouseButtonDown(1))
    {
        //////Debug.Log(hit.normal);
        //Debug.DrawRay(hit.point, Vector3.up * 10, Color.red, 0.1f);

        int x = (int)(((yoMan.x * 1) / 1) / 1); //WORKING
        int y = (int)(((yoMan.y * 1) / 1) / 1);//WORKING
        int z = (int)(((yoMan.z * 1) / 1) / 1);//WORKING

        //terrain1.GetChunk(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
        var planetchunk = hit.transform;
        planetchunk.GetComponent<sccslodchunkfinal>().SetByte((int)x, (int)y, (int)z, activeBlockType);

        planetchunk.GetComponent<sccslodchunkfinal>().Regenerate();
        planetchunk.GetComponent<sccslodchunkfinal>().buildMesh();

        setAdjacentChunks(currentChunk, hit, x, y, z);
    }

}*/













