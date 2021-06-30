using UnityEngine;
using System.Collections;


public class playerInteraction : MonoBehaviour
{


    public Transform upperleg;
    public Transform lowerleg;
    public Transform foot;
    public Transform foottarget;
    public Transform legstaticpivot;

    float upperleglength = 0;
    float lowerleglength = 0;
    float footlength = 0;
    float totallegLength = 0;

    public LayerMask layerMask;

    Vector3 IdleStandingTargetPositionVariableLength;
    Vector3 IdleStandingTargetPositionMax;
    Vector3 IdleStandingTargetPositionMin;

    public int swtcForTypeOfInteract = 0;
    //0 == 

    public Transform footTarget;


    public Transform planetmanager;

    public byte activeBlockType = 0;
    public Transform retAdd, retDel;

    Mesh mesh;

    float planeSize = 0.25f;

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


    int multiplicator = 3;
    int multiplicatorReticle = 5;
    int tileSize = 4;
    int suppressorPos = 4;



    void Start()
    {
        if (swtcForTypeOfInteract == 2)
        {
            upperleglength = upperleg.localScale.z;
            lowerleglength = lowerleg.localScale.z;
            footlength = foot.localScale.z;
            totallegLength = upperleglength + lowerleglength + footlength;

            IdleStandingTargetPositionMax = transform.position + ((transform.forward * upperleglength) + (transform.forward * lowerleglength) + (transform.forward * footlength));
            IdleStandingTargetPositionMin = transform.position + ((transform.forward * (upperleglength)) + (transform.forward * (lowerleglength)) + (transform.forward * (footlength))*0.5f);

        }

        //retAdd.localScale = retAdd.localScale * planeSize;
        //retDel.localScale = retDel.localScale * planeSize;
        fraction = 1 / planeSize;
        radius = planeSize / 2;
        diameter = 0.1f;
        whatever = 1 / (diameter * 2);
        cam = Camera.main;
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

    int counterForByteChange = 0;
    int counterForByteChangeMax = 1;


    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("object hit:" + other.transform.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //var closestPointOnBounds = other.ClosestPointOnBounds(transform.position);


        ////Debug.Log("col:" + other.transform.name);
        var sccsfracturescript = other.transform.gameObject.GetComponent<Fracture4>();

        if (sccsfracturescript != null)
        {
            sccsfracturescript.enabled = true;
        }

        //Ray ray = new Ray(transform.position, transform.forward);// Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.forward * 25, Color.green, 0.001f);

        //var someTouch0 = Input.GetTouch(0);
        ////Debug.Log(""+ someTouch0);

        //bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        //bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        //if (buttonPressedLeft)
        {
            //Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
        }
        //if (buttonPressedRight)
        {
            //Debug.Log("buttonPressedRight:" + buttonPressedRight);
        }

        //if (counterForByteChange >= counterForByteChangeMax)
        {
            //if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
            {
                //if (Physics.Raycast(ray, out hit))
                {
                    if (collision.transform.tag == "collisionObject")
                    {
                        var chunkX = (int)(Mathf.Round(collision.transform.position.x * tileSize) / tileSize);
                        var chunkY = (int)(Mathf.Round(collision.transform.position.y * tileSize) / tileSize);
                        var chunkZ = (int)(Mathf.Round(collision.transform.position.z * tileSize) / tileSize);

                        ////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                        if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z) != null)
                        {
                            ////Debug.Log("==count==");
                            mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z);

                            ////Debug.Log("x: " + collision.normal.x + " y: " + collision.normal.y + " z: " + collision.normal.z);
                            if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == -1) //FRONT FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                                //retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);

                            }
                            else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 1) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);
                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);


                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == -1 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 0) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == 1 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 0) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == -1 && collision.contacts[0].normal.z == 0) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y, (z - (planeSize * 0.5f)));

                                retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 1 && collision.contacts[0].normal.z == 0) //TOP FACE
                            {
                                //Debug.Log("top face");
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), (y - (planeSize * 0.5f)), (z - (planeSize * 0.5f)));

                                retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

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

    void Update()
    {


        if (swtcForTypeOfInteract == 0)
        {
            ray = new Ray(transform.position, transform.forward);//ray = Camera.main.ScreenPointToRay(Input.mousePosition);



            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * 25, Color.green, 0.001f);

            //var someTouch0 = Input.GetTouch(0);
            ////Debug.Log(""+ someTouch0);

            /*bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

            if (buttonPressedLeft)
            {
                //Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
            }
            if (buttonPressedRight)
            {
                //Debug.Log("buttonPressedRight:" + buttonPressedRight);
            }*/

            //if (counterForByteChange >= counterForByteChangeMax)
            {
                //if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
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
                                var chunkX = (int)(Mathf.Round(hit.transform.position.x * tileSize) / tileSize);
                                var chunkY = (int)(Mathf.Round(hit.transform.position.y * tileSize) / tileSize);
                                var chunkZ = (int)(Mathf.Round(hit.transform.position.z * tileSize) / tileSize);

                                ////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                                if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
                                {
                                    ////Debug.Log("==count==");
                                    mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z);

                                    ////Debug.Log("x: " + hit.normal.x + " y: " + hit.normal.y + " z: " + hit.normal.z);
                                    if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1) //FRONT FACE
                                    {
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                                        //retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);

                                    }
                                    else if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1) //BACK FACE
                                    {
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);


                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y, (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0) //TOP FACE
                                    {
                                        //Debug.Log("top face");
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), (y - (planeSize * 0.5f)), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

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
                }
                counterForByteChange = 0;
            }
            counterForByteChange++;
        }
        else if (swtcForTypeOfInteract == 1)
        {
            ray = new Ray(transform.position, transform.forward);// Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * 25, Color.green, 0.001f);

            //var someTouch0 = Input.GetTouch(0);
            ////Debug.Log(""+ someTouch0);

            bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

            if (buttonPressedLeft)
            {
                //Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
            }
            if (buttonPressedRight)
            {
                //Debug.Log("buttonPressedRight:" + buttonPressedRight);
            }

            if (counterForByteChange >= counterForByteChangeMax)
            {
                if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
                {
                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        if (hit.transform.tag == "collisionObject")
                        {
                            if (GetComponent<Fracture4>() != null)
                            {

                            }
                            else
                            {
                                var chunkX = (int)(Mathf.Round(hit.transform.position.x * tileSize) / tileSize);
                                var chunkY = (int)(Mathf.Round(hit.transform.position.y * tileSize) / tileSize);
                                var chunkZ = (int)(Mathf.Round(hit.transform.position.z * tileSize) / tileSize);

                                ////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                                if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
                                {
                                    ////Debug.Log("==count==");
                                    mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z);

                                    ////Debug.Log("x: " + hit.normal.x + " y: " + hit.normal.y + " z: " + hit.normal.z);
                                    if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1) //FRONT FACE
                                    {
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                                        //retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);

                                    }
                                    else if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1) //BACK FACE
                                    {
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);


                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0) //BACK FACE
                                    {
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y, (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

                                        UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                        UnityTutorialPooledObject.SetActive(true);
                                    }
                                    else if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0) //TOP FACE
                                    {
                                        //Debug.Log("top face");
                                        Vector3 p = hit.point;

                                        var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                        var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                        var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                        Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), (y - (planeSize * 0.5f)), (z - (planeSize * 0.5f)));

                                        retAdd.position = retAddPos;
                                        var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                                        var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                                        var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                                        ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                                        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                                        currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                                        setAdjacentChunks(currentChunk, hit.transform.position, indexX, indexY, indexZ);

                                        var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                        var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                        UnityTutorialPooledObject.transform.position = retAddPos;

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
                }
                counterForByteChange = 0;
            }
            counterForByteChange++;
        }
        else if (swtcForTypeOfInteract == 2)
        {
            if (counterForIkFootPlacement <= counterForIkFootPlacementMax)
            {
                var ray = new Ray(transform.position, transform.forward);

                RaycastHit hit;
                Debug.DrawRay(transform.position, transform.forward * 3, Color.green, 0.001f);

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
                Debug.DrawRay(transform.position, transform.forward * 3, Color.green, 0.001f);

                if (Physics.Raycast(ray, out hit, totallegLength, layerMask))
                {
                    if (hit.transform.tag == "collisionObject")
                    {
                        Vector3 tempDir = legstaticpivot.position - foottarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                        //IdleStandingTargetPositionVariableLength


                        if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                        {
                            foottarget.position = IdleStandingTargetPositionMax;
                            tempDir.Normalize();
                            //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                            Vector3 tempVect = (legstaticpivot.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
                            //MOVINGPOINTER.X = tempVect.X;
                            //MOVINGPOINTER.Y = tempVect.Y;
                            //MOVINGPOINTER.Z = tempVect.Z;
                            foottarget.position = tempVect;// hit.point;
                        }
                        else
                        {
                            foottarget.position = hit.point + (tempDir * foot.localScale.y);
                        }



                        /*if (tempDir.magnitude < (totallegLength * 0.5f))
                        {
                            foottarget.position = IdleStandingTargetPositionMin;
                        }
                        else
                        {
                            foottarget.position = hit.point;
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




    /*||
   hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0 ||
   hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0 ||
   hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1 ||
   hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0 ||
   hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0*/
    public void setAdjacentChunks(mainChunk currentChunk, Vector3 pos, int indexX, int indexY, int indexZ)
    {
        int width = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().width;
        int height = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().height;
        int depth = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().depth;

        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

        if (indexX == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x - 4, (int)pos.y, (int)pos.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x - 4, (int)pos.y, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)width - 1, (int)indexY, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)width - 1, (int)indexY, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexX == width - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x + 4, (int)pos.y, (int)pos.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x + 4, (int)pos.y, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)0, (int)indexY, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk right exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)0, (int)indexY, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexY == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y - 4, (int)pos.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y - 4, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)height - 1, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)height - 1, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexY == height - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y + 4, (int)pos.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y + 4, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)0, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)0, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexZ == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y, (int)pos.z - 4) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y, (int)pos.z - 4);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)indexY, (int)depth - 1) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)depth - 1, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexZ == depth - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y, (int)pos.z + 4) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)pos.x, (int)pos.y, (int)pos.z + 4);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)indexY, (int)0) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)0, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }
    }
}


//by robertbu
/*//https://answers.unity.com/questions/540888/converting-mouse-position-to-world-stationary-came.html 
if (Input.GetMouseButtonDown(0))
{
    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit))
    {
        //hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y,(int)hit.transform.position.z) != null)
        {
            var chunkX = (int)(Mathf.Round(hit.point.x * planeSize) / planeSize);
            var chunkY = (int)(Mathf.Round(hit.point.y * planeSize) / planeSize);
            var chunkZ = (int)(Mathf.Round(hit.point.z * planeSize) / planeSize);

            //Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);
            retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
            //yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * -2);
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
    ////Debug.Log("yo0");

    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset * multiplicator, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    //retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Round(p.y * suppressorPos) / suppressorPos) + offset * multiplicator, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset * multiplicatorReticle, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Round(p.y * suppressorPos) / suppressorPos) + offset * 5, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);
}

if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1)
{
    ////Debug.Log("yo1");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 5);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
}

if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0)
{
    ////Debug.Log("yo2");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
}


if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0)
{
    ////Debug.Log("yo2");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
}

if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1)
{
    ////Debug.Log("yo1");

    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 3);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 6);

}


if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0)
{
    ////Debug.Log("yo1");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 3, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

}



var chunkX = (int)(Mathf.Round(hit.transform.position.x * tileSize) / tileSize);
var chunkY = (int)(Mathf.Round(hit.transform.position.y * tileSize) / tileSize);
var chunkZ = (int)(Mathf.Round(hit.transform.position.z * tileSize) / tileSize);

////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
{

    ////Debug.Log("==count==");
    mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z);

    if (Input.GetMouseButtonDown(0))
    {
        int x = (int)(((yoMan1.x * 1) / 1) / 1); //WORKING
        int y = (int)(((yoMan1.y * 1) / 1) / 1);//WORKING
        int z = (int)(((yoMan1.z * 1) / 1) / 1);//WORKING
                                           //terrain1.GetChunk(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);

        var planetchunk = hit.transform;
        planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)x, (int)y, (int)z, activeBlockType);

        planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
        planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

        setAdjacentChunks(currentChunk, hit, x, y, z);
    }


    if (Input.GetMouseButtonDown(1))
    {
        ////Debug.Log(hit.normal);
        //Debug.DrawRay(hit.point, Vector3.up * 10, Color.red, 0.1f);

        int x = (int)(((yoMan.x * 1) / 1) / 1); //WORKING
        int y = (int)(((yoMan.y * 1) / 1) / 1);//WORKING
        int z = (int)(((yoMan.z * 1) / 1) / 1);//WORKING

        //terrain1.GetChunk(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
        var planetchunk = hit.transform;
        planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)x, (int)y, (int)z, activeBlockType);

        planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
        planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

        setAdjacentChunks(currentChunk, hit, x, y, z);
    }

}*/






















