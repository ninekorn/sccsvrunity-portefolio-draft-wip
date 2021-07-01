using UnityEngine;
using System.Collections;


public class playerInteraction : MonoBehaviour
{
    public Transform planetmanager;

    public byte activeBlockType = 0;
    public Transform retAdd, retDel;

    Mesh mesh;

    float planeSize = 0.25f;

    public Transform sphere;
    Vector3 yoMan;
    Vector3 yoMan1;

    int tileSize = 4;

    float diameter;
    float fraction;
    float radius;
    float whatever;

    int roundedX;
    int roundedY;
    int roundedZ;

    private Camera cam;

    void Start()
    {
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


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
                        var chunkX = (int)(Mathf.Floor(hit.point.x * planeSize) / planeSize);
                        var chunkY = (int)(Mathf.Floor(hit.point.y * planeSize) / planeSize);
                        var chunkZ = (int)(Mathf.Floor(hit.point.z * planeSize) / planeSize);

                        Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);
                        retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
                        //yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * -2);
                        //retDel.position = p;
                    }
                }
            }*/

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "collisionObject")
                {
                    var chunkX = (int)(Mathf.Floor(hit.transform.position.x * tileSize) / tileSize);
                    var chunkY = (int)(Mathf.Floor(hit.transform.position.y * tileSize) / tileSize);
                    var chunkZ = (int)(Mathf.Floor(hit.transform.position.z * tileSize) / tileSize);

                    //Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                    if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
                    {
                        mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z);

                        if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1) //FRONT FACE
                        {
                            Vector3 p = hit.point;

                            var x = (Mathf.Round(p.x * tileSize) / tileSize);
                            var y = (Mathf.Round(p.y * tileSize) / tileSize);
                            var z = (Mathf.Round(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                            retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                            //Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                            setAdjacentChunks(currentChunk,hit,indexX,indexY,indexZ);
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

                            //Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                            setAdjacentChunks(currentChunk, hit, indexX, indexY, indexZ);
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

                            //Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                            setAdjacentChunks(currentChunk, hit, indexX, indexY, indexZ);
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

                            //Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                            setAdjacentChunks(currentChunk, hit, indexX, indexY, indexZ);
                        }
                        else if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0) //BACK FACE
                        {
                            Vector3 p = hit.point;

                            var x = (Mathf.Round(p.x * tileSize) / tileSize);
                            var y = (Mathf.Round(p.y * tileSize) / tileSize);
                            var z = (Mathf.Round(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y + (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(hit.transform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(hit.transform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(hit.transform.position.z - retAddPos.z);

                            //Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                            setAdjacentChunks(currentChunk, hit, indexX, indexY, indexZ);
                        }
                        else if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0) //BACK FACE
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

                            //Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

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

                            setAdjacentChunks(currentChunk, hit, indexX, indexY, indexZ);
                        }










                    }
                }
            }
        }






















        /*||
       hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0 ||
       hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0 ||
       hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1 ||
       hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0 ||
       hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0*/
    }



    public void setAdjacentChunks(mainChunk currentChunk, RaycastHit hit, int indexX, int indexY, int indexZ)
    {
        int width = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().width;
        int height = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().height;
        int depth = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().depth;

        //Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

        if (indexX == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x - 4, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x - 4, (int)hit.transform.position.y, (int)hit.transform.position.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)width - 1, (int)indexY, (int)indexZ) == 1)
                {
                    //Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)width - 1, (int)indexY, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexX == width - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x + 4, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x + 4, (int)hit.transform.position.y, (int)hit.transform.position.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)0, (int)indexY, (int)indexZ) == 1)
                {
                    //Debug.Log("adjacent chunk right exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)0, (int)indexY, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexY == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y - 4, (int)hit.transform.position.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y - 4, (int)hit.transform.position.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)height - 1, (int)indexZ) == 1)
                {
                    //Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)height - 1, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexY == height - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y + 4, (int)hit.transform.position.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y + 4, (int)hit.transform.position.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)0, (int)indexZ) == 1)
                {
                    //Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)0, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexZ == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z - 4) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z - 4);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)indexY, (int)depth - 1) == 1)
                {
                    //Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)depth - 1, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }

        if (indexZ == depth - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z + 4) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z + 4);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().GetByte((int)indexX, (int)indexY, (int)0) == 1)
                {
                    //Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)0, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();
                }
            }
        }
    }
}
