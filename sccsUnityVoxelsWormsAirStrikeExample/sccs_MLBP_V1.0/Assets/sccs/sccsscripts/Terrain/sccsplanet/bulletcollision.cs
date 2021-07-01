using UnityEngine;
using System.Collections;


public class bulletcollision : MonoBehaviour
{
    public Vector3 collisionNormal = Vector3.zero;
    public Vector3 collisionPoint = Vector3.zero;
    public Transform collisionTransform;

    public Transform retAdd;

    public int hastarget = 0;
    public RaycastHit targetHitPoint;

    public Transform planetmanager;

    public byte activeBlockType = 0;

    Mesh mesh;

    float planeSize = 0.25f;

    Vector3 yoMan;
    Vector3 yoMan1;

    int tileSize = 4;

    float diameter;
    float fraction;
    float radius;
    float whatever;

    int FlooredX;
    int FlooredY;
    int FlooredZ;

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


    public void processChunkByteRemoval()
    {
        if (hastarget == 1)
        {
            if (targetHitPoint.transform!= null)
            {
                RaycastHit hit = targetHitPoint;

                if (hit.transform.tag == "collisionObject")
                {
                    var chunkX = (int)(Mathf.Round(hit.transform.position.x * tileSize) / tileSize);
                    var chunkY = (int)(Mathf.Round(hit.transform.position.y * tileSize) / tileSize);
                    var chunkZ = (int)(Mathf.Round(hit.transform.position.z * tileSize) / tileSize);

                    //Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                    if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
                    {

                        //Debug.Log("==count==");
                        mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z);

                        //Debug.Log("x: " + hit.normal.x + " y: " + hit.normal.y + " z: " + hit.normal.z);
                        if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1) //FRONT FACE
                        {
                            Vector3 p = hit.point;

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                            ////retAdd.position = retAddPos;
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

                            //Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                            setAdjacentChunks(currentChunk, hit, indexX, indexY, indexZ);
                        }
                        else if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1) //BACK FACE
                        {
                            Vector3 p = hit.point;

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
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

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
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

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
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

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y + (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
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

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
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
                    else
                    {

                    }
                }
            }
        }
        else if (hastarget == 0)
        {

            fraction = 1 / planeSize;
            radius = planeSize / 2;
            diameter = 0.1f;
            whatever = 1 / (diameter * 2);
            cam = Camera.main;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "collisionObject")
                {
                    var chunkX = (int)(Mathf.Round(hit.transform.position.x * tileSize) / tileSize);
                    var chunkY = (int)(Mathf.Round(hit.transform.position.y * tileSize) / tileSize);
                    var chunkZ = (int)(Mathf.Round(hit.transform.position.z * tileSize) / tileSize);

                    //Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                    if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
                    {
                        mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z);

                        if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1) //FRONT FACE
                        {
                            Vector3 p = hit.point;

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                            ////retAdd.position = retAddPos;
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

                            //Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                            setAdjacentChunks(currentChunk, hit, indexX, indexY, indexZ);
                        }
                        else if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1) //BACK FACE
                        {
                            Vector3 p = hit.point;

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
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

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
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

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
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

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y + (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
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

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
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
        else if (hastarget == 2)
        {
            //if (targetHitPoint.transform != null)
            {
                //Debug.Log("hastarget2");
                //RaycastHit hit = targetHitPoint;

                if (collisionTransform.transform.tag == "collisionObject")
                {

                    var chunkX = (int)(Mathf.Round(collisionTransform.position.x * tileSize) / tileSize);
                    var chunkY = (int)(Mathf.Round(collisionTransform.position.y * tileSize) / tileSize);
                    var chunkZ = (int)(Mathf.Round(collisionTransform.position.z * tileSize) / tileSize);

                    //Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                    collisionNormal.x = Mathf.Round(collisionNormal.x);
                    collisionNormal.y = Mathf.Round(collisionNormal.y);
                    collisionNormal.z = Mathf.Round(collisionNormal.z);




                    /*if (collisionNormal.x != 0 || collisionNormal.x != 1)
                    {
                        collisionNormal.x = 0;
                    }
                    if (collisionNormal.y != 0 || collisionNormal.y != 1)
                    {
                        collisionNormal.y = 0;
                    }

                    if (collisionNormal.z != 0 || collisionNormal.z != 1)
                    {
                        collisionNormal.z = 0;
                    }*/

                    //Debug.Log("x: " + collisionNormal.x + " y: " + collisionNormal.y + " z: " + collisionNormal.z);





                    if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)collisionTransform.position.x, (int)collisionTransform.position.y, (int)collisionTransform.position.z) != null)
                    {
                        mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)collisionTransform.position.x, (int)collisionTransform.position.y, (int)collisionTransform.position.z);

                        if (collisionNormal.x == 0 &&collisionNormal.y == 0 && collisionNormal.z == -1) //FRONT FACE
                        {
                            Vector3 p = collisionPoint;

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));



                            //retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collisionTransform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collisionTransform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collisionTransform.position.z - retAddPos.z);

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
                            //Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));


                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().Regenerate();
                            currentChunk.planetchunk.GetComponent<sccsplanetchunk>().buildMesh();

                            setAdjacentChunksWithTransform(currentChunk, collisionTransform, indexX, indexY, indexZ);
                        }
                        else if (collisionNormal.x == 0 &&collisionNormal.y == 0 &&collisionNormal.z == 1) //BACK FACE
                        {
                            Vector3 p = collisionPoint;

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collisionTransform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collisionTransform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collisionTransform.position.z - retAddPos.z);

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

                            setAdjacentChunksWithTransform(currentChunk, collisionTransform, indexX, indexY, indexZ);
                        }
                        else if (collisionNormal.x == -1 &&collisionNormal.y == 0 &&collisionNormal.z == 0) //BACK FACE
                        {
                            Vector3 p = collisionPoint;

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collisionTransform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collisionTransform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collisionTransform.position.z - retAddPos.z);

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

                            setAdjacentChunksWithTransform(currentChunk, collisionTransform, indexX, indexY, indexZ);
                        }
                        else if (collisionNormal.x == 1 &&collisionNormal.y == 0 &&collisionNormal.z == 0) //BACK FACE
                        {
                            Vector3 p = collisionPoint;

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collisionTransform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collisionTransform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collisionTransform.position.z - retAddPos.z);

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

                            setAdjacentChunksWithTransform(currentChunk, collisionTransform, indexX, indexY, indexZ);
                        }
                        else if (collisionNormal.x == 0 &&collisionNormal.y == -1 &&collisionNormal.z == 0) //BACK FACE
                        {
                            Vector3 p = collisionPoint;

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y + (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collisionTransform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collisionTransform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collisionTransform.position.z - retAddPos.z);

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

                            setAdjacentChunksWithTransform(currentChunk, collisionTransform, indexX, indexY, indexZ);
                        }
                        else if (collisionNormal.x == 0 &&collisionNormal.y == 1 &&collisionNormal.z == 0) //BACK FACE
                        {
                            Vector3 p = collisionPoint;

                            var x = (Mathf.Floor(p.x * tileSize) / tileSize);
                            var y = (Mathf.Floor(p.y * tileSize) / tileSize);
                            var z = (Mathf.Floor(p.z * tileSize) / tileSize);

                            Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                            //retAdd.position = retAddPos;
                            var remainsx = Mathf.Abs(collisionTransform.position.x - retAddPos.x);
                            var remainsy = Mathf.Abs(collisionTransform.position.y - retAddPos.y);
                            var remainsz = Mathf.Abs(collisionTransform.position.z - retAddPos.z);

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

                            setAdjacentChunksWithTransform(currentChunk, collisionTransform, indexX, indexY, indexZ);
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





    public void setAdjacentChunksWithTransform(mainChunk currentChunk, Transform hitTransform, int indexX, int indexY, int indexZ)
    {
        int width = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().width;
        int height = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().height;
        int depth = currentChunk.planetchunk.GetComponent<sccsplanetchunk>().depth;

        //Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

        if (indexX == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x - 4, (int)hitTransform.position.y, (int)hitTransform.position.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x - 4, (int)hitTransform.position.y, (int)hitTransform.position.z);

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
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x + 4, (int)hitTransform.position.y, (int)hitTransform.position.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x + 4, (int)hitTransform.position.y, (int)hitTransform.position.z);

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
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x, (int)hitTransform.position.y - 4, (int)hitTransform.position.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x, (int)hitTransform.position.y - 4, (int)hitTransform.position.z);

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
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x, (int)hitTransform.position.y + 4, (int)hitTransform.position.z) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x, (int)hitTransform.position.y + 4, (int)hitTransform.position.z);

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
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x, (int)hitTransform.position.y, (int)hitTransform.position.z - 4) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x, (int)hitTransform.position.y, (int)hitTransform.position.z - 4);

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
            if (planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x, (int)hitTransform.position.y, (int)hitTransform.position.z + 4) != null)
            {
                mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilder>().getChunk((int)hitTransform.position.x, (int)hitTransform.position.y, (int)hitTransform.position.z + 4);

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
