using UnityEngine;
using System.Collections;

public class ScreenBounds : MonoBehaviour
{
    public static ScreenBounds currentScreenBounds;
    public float leftConstraint = 0.0f;
    public float rightConstraint = 0.0f;
    public float bottomConstraint = 0.0f;
    public float topConstraint = 0.0f;

    public GameObject ship;
    //public GameObject ship2;
    //public GameObject ship3;

    public GameObject shipSwapper;

    public float speed;
    public float rotspeed;

    Renderer renderers;
    bool isWrappingX = false;
    bool isWrappingY = false;
    public GameObject cube;

    bool isMoving = false;
    Vector3 currentDirection;
    public Vector3 realPosition;
    public Vector3 fakePosition;

    float realPosX = 0;
    float realPosY = 0;
    float realPosZ = 0;
    float xx = 0;
    float yy = 0;
    float zz = 0;
    public Vector3 amIMoving;

    float constraints = 288;
    float shipconstraints = 144;
    float smallconstraints = 144;
    float mirrorPos = 18;
    public GameObject realShip;
    Vector3 realShipPosition;
    bool startingSwap = false;
    bool stopAction = false;
    bool swapper = false;

    IEnumerator CheckMoving()
    {
        Vector3 startPos = ship.transform.position;
        yield return new WaitForSeconds(0.01f);
        Vector3 finalPos = ship.transform.position;
        if (startPos.x != finalPos.x || startPos.y != finalPos.y
            || startPos.z != finalPos.z)
        {
            isMoving = true;
        }

        else if (startPos.x == finalPos.x && startPos.y == finalPos.y
             && startPos.z == finalPos.z)
        {
            isMoving = false;
        }
        currentDirection = finalPos -startPos;
    }

    void Awake()
    {
        StartCoroutine(CheckMoving());

        realPosition = ship.transform.position;
        fakePosition = ship.transform.position;
        currentScreenBounds = this;

        leftConstraint = -smallconstraints;
        rightConstraint = smallconstraints;
        bottomConstraint = -smallconstraints;
        topConstraint = smallconstraints;

        //CreateGhostShips();
        //PositionGhostShips();
    }

    void Update()
    {
        //PositionGhostShips();
        amIMoving = ship.transform.position;
        realPosX = 0;
        realPosY = 0;
        realPosZ = 0;

        StartCoroutine(CheckMoving());

        Debug.DrawRay(ship.transform.position,currentDirection*10,Color.green,0.1f);

        if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(Vector3.forward * speed);
            ship.transform.Translate(Vector3.forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(-Vector3.forward * speed);
            ship.transform.Translate(-Vector3.forward * speed);

        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.right * speed);
            ship.transform.Translate(Vector3.right * speed);

        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(-Vector3.right * speed);
            ship.transform.Translate(-Vector3.right * speed);
        }

        /*if (Input.GetAxis("Mouse X") < 0)
        {
            transform.Rotate(0, (Input.GetAxis("Mouse X") * Time.deltaTime * rotspeed), 0);
            //Debug.Log("Mouse moved left");
        }
        else if (Input.GetAxis("Mouse X") > 0)
        {
            transform.Rotate(0, (Input.GetAxis("Mouse X") * Time.deltaTime * rotspeed), 0);
            //Debug.Log("Mouse moved right");
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            transform.Rotate((Input.GetAxis("Mouse Y") * Time.deltaTime * rotspeed), 0, 0);
            //Debug.Log("Mouse moved left");
        }
        else if (Input.GetAxis("Mouse Y") > 0)
        {
            transform.Rotate((Input.GetAxis("Mouse Y") * Time.deltaTime * rotspeed), 0, 0);
            //Debug.Log("Mouse moved right");
        }*/

        Vector3 shipPos = ship.transform.position;   
        float shipX = shipPos.x;
        float shipY = shipPos.y;
        float shipZ = shipPos.z;

        if (shipX <= leftConstraint)
        {
            Plane pointChecker = new Plane(Vector3.right,new Vector3(shipconstraints, 0,0));
            Ray ray = new Ray(ship.transform.position, -currentDirection);
            float distance;
            pointChecker.Raycast(ray, out distance);
            Vector3 pointer = ray.GetPoint(distance);
            //Instantiate(cube,pointer,Quaternion.identity);
            realPosX = ship.transform.position.x;
            shipX = pointer.x;
        }
        if (shipX > rightConstraint)
        {
            Plane pointChecker = new Plane(Vector3.right, new Vector3(-shipconstraints, 0, 0));
            Ray ray = new Ray(ship.transform.position, -currentDirection);
            float distance;
            pointChecker.Raycast(ray, out distance);
            Vector3 pointer = ray.GetPoint(distance);
            realPosX = ship.transform.position.x;
            shipX = pointer.x;
        }

        if (shipY <= leftConstraint)
        {
            Plane pointChecker = new Plane(Vector3.up, new Vector3(0, shipconstraints, 0));
            Ray ray = new Ray(ship.transform.position, -currentDirection);
            float distance;
            pointChecker.Raycast(ray, out distance);
            Vector3 pointer = ray.GetPoint(distance);
            realPosY = ship.transform.position.y;
            shipY = pointer.y;
        }
        if (shipY > rightConstraint)
        {
            Plane pointChecker = new Plane(Vector3.up, new Vector3(0, -shipconstraints, 0));
            Ray ray = new Ray(ship.transform.position, -currentDirection);
            float distance;
            pointChecker.Raycast(ray, out distance);
            Vector3 pointer = ray.GetPoint(distance);
            realPosY = ship.transform.position.y;
            shipY = pointer.y;
        }

        if (shipZ <= leftConstraint)
        {
            Plane pointChecker = new Plane(Vector3.forward, new Vector3(0, 0, shipconstraints));
            Ray ray = new Ray(ship.transform.position, -currentDirection);
            float distance;
            pointChecker.Raycast(ray, out distance);
            Vector3 pointer = ray.GetPoint(distance);
            realPosZ = ship.transform.position.z;
            shipZ = pointer.z;
        }
        if (shipZ > rightConstraint)
        {
            Plane pointChecker = new Plane(Vector3.forward, new Vector3(0, 0, -shipconstraints));
            Ray ray = new Ray(ship.transform.position, -currentDirection);
            float distance;
            pointChecker.Raycast(ray, out distance);
            Vector3 pointer = ray.GetPoint(distance);
            realPosZ = ship.transform.position.z;
            shipZ = pointer.z;





        }







        setRealPosition(realPosX, realPosY, realPosZ);
        shipPos.x = shipX;
        shipPos.y = shipY;
        shipPos.z = shipZ;
        ship.transform.position = shipPos;
        realPosition = transform.position;
    }

    void setRealPosition(float x, float y,float z)
    {
        xx += Mathf.Round(x * 2);
        yy += Mathf.Round(y * 2);
        zz += Mathf.Round(z * 2);
        fakePosition = new Vector3(xx, yy, zz);
    }


    public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
    {

        Vector3 lineVec3 = linePoint2 - linePoint1;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parrallel
        if (Mathf.Abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
            intersection = linePoint1 + (lineVec1 * s);
            return true;
        }
        else
        {
            intersection = Vector3.zero;
            return false;
        }
    }

    Transform[] ghosts = new Transform[3];
    void CreateGhostShips()
    {
        for (int i = 0; i < 3; i++)
        {
            ghosts[i] = Instantiate(cube.transform, Vector3.zero, Quaternion.identity) as Transform;
           
            //DestroyImmediate(ghosts[i].GetComponent<Renderer>());
        }
        //ship2 = ghosts[0].transform.gameObject;
        //ship3 = ghosts[1].transform.gameObject;
        //PositionGhostShips();
    }

    /*void SwapShips()
    {
        foreach (var ghost in ghosts)
        {
            if (ghost.position.x < constraints && ghost.position.x > -constraints &&
                ghost.position.y < constraints && ghost.position.y > -constraints &&
                ghost.position.z < constraints && ghost.position.z > -constraints)
            {
                transform.position = ghost.position;
                break;
            }
        }
        PositionGhostShips();
    }*/

    bool canSwapShip = false;
    bool canSwapShip3 = false;

    void PositionGhostShips()
    {
        Vector3 ghostPosition;

        if (canSwapShip == false)
        {
            ghostPosition.x = ship.transform.position.x + 288;
            ghostPosition.y = ship.transform.position.y;
            ghostPosition.z = -144;
            ghosts[0].position = ghostPosition;
        }
        if (canSwapShip3 == false)
        {
            ghostPosition.x =- 432;
            ghostPosition.y = ship.transform.position.y;
            ghostPosition.z = ship.transform.position.z;
            ghosts[1].position = ghostPosition;
        }
        //ghostPosition.x = transform.position.x;
        //ghostPosition.y = transform.position.y;
        //ghostPosition.z = transform.position.z + constraints;
        //ghosts[1].position = ghostPosition;







        /*ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z;

        ghosts[0].position = ghostPosition;

        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z;
        ghosts[1].position = ghostPosition;

        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z - constraints;
        ghosts[2].position = ghostPosition;

        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z + constraints;
        ghosts[3].position = ghostPosition;

        ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z - constraints;
        ghosts[4].position = ghostPosition;

        ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z + constraints;
        ghosts[5].position = ghostPosition;*/















        // Let's start with the far right.
        /*ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y;
        ghosts[0].position = ghostPosition;

        // Bottom-right
        ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y - constraints;
        ghosts[1].position = ghostPosition;

        // Bottom
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y - constraints;
        ghosts[2].position = ghostPosition;

        // Bottom-left
        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y - constraints;
        ghosts[3].position = ghostPosition;

        // Left
        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y;
        ghosts[4].position = ghostPosition;

        // Top-left
        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y + constraints;
        ghosts[5].position = ghostPosition;

        // Top
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y + constraints;
        ghosts[6].position = ghostPosition;

        // Top-right
        ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y + constraints;
        ghosts[7].position = ghostPosition;*/







        /*ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z+ constraints;
        ghosts[8].position = ghostPosition;

        // Bottom-right
        ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y - constraints;
        ghostPosition.z = transform.position.z + constraints;

        ghosts[9].position = ghostPosition;

        // Bottom
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y - constraints;
        ghostPosition.z = transform.position.z + constraints;

        ghosts[10].position = ghostPosition;

        // Bottom-left
        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y - constraints;
        ghostPosition.z = transform.position.z + constraints;

        ghosts[11].position = ghostPosition;

        // Left
        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z + constraints;

        ghosts[12].position = ghostPosition;

        // Top-left
        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y + constraints;
        ghostPosition.z = transform.position.z + constraints;

        ghosts[13].position = ghostPosition;

        // Top
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y + constraints;
        ghostPosition.z = transform.position.z + constraints;
        ghosts[14].position = ghostPosition;

        // Top-right
        ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y + constraints;
        ghostPosition.z = transform.position.z + constraints;
        ghosts[15].position = ghostPosition;

        // Top-Middle
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z + constraints;
        ghosts[16].position = ghostPosition;







        ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z - constraints;
        ghosts[17].position = ghostPosition;

        // Bottom-right
        ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y - constraints;
        ghostPosition.z = transform.position.z - constraints;

        ghosts[18].position = ghostPosition;

        // Bottom
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y - constraints;
        ghostPosition.z = transform.position.z - constraints;

        ghosts[19].position = ghostPosition;

        // Bottom-left
        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y - constraints;
        ghostPosition.z = transform.position.z - constraints;

        ghosts[20].position = ghostPosition;

        // Left
        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z - constraints;

        ghosts[21].position = ghostPosition;

        // Top-left
        ghostPosition.x = transform.position.x - constraints;
        ghostPosition.y = transform.position.y + constraints;
        ghostPosition.z = transform.position.z - constraints;

        ghosts[22].position = ghostPosition;

        // Top
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y + constraints;
        ghostPosition.z = transform.position.z - constraints;
        ghosts[23].position = ghostPosition;

        // Top-right
        ghostPosition.x = transform.position.x + constraints;
        ghostPosition.y = transform.position.y + constraints;
        ghostPosition.z = transform.position.z - constraints;
        ghosts[24].position = ghostPosition;

        // Top-Middle
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y;
        ghostPosition.z = transform.position.z - constraints;
        ghosts[25].position = ghostPosition;*/





        // All ghost ships should have the same rotation as the main ship
        /*for (int i = 0; i < 8; i++)
        {
            ghosts[i].rotation = transform.rotation;
        }*/
    }

















    /*float position;
           Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);

           if (shipPlane.Raycast(inputRay, out position))
           {
               Vector3 hitPoint = inputRay.GetPoint(position);
               float distanceFromShip = Vector3.Distance(inPoint, hitPoint);
           }*/


    /*if (shipX < leftConstraint - buffer)
     { // ship is past world-space view / off screen
         shipX = rightConstraint + buffer;  // move ship to opposite side
     }

     if (shipX > rightConstraint + buffer)
     {
         shipX = leftConstraint - buffer;
     }*/




















    bool CheckRenderers()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            return true;
        }

        // Otherwise, the object is invisible
        return false;
    }


    void ScreenWrap()
    {
        var isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;

            isWrappingX = true;
        }

        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;

            isWrappingY = true;
        }

        transform.position = newPosition;
    }




    void OffscreenCheck()
    {

        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 maxWorldWithPlayerSize = new Vector3(-10 + transform.localScale.x / 2, 0, 0);
        Vector3 minWorldWithPlayerSize = new Vector3(10 - transform.localScale.x / 2, 0, 0);
        Vector3 maxScreenWithPlayerSize = Camera.main.WorldToScreenPoint(maxWorldWithPlayerSize);
        Vector3 minScreenWithPlayerSize = Camera.main.WorldToScreenPoint(minWorldWithPlayerSize);

        if (screenPos.x < minScreenWithPlayerSize.x)
        {
            this.transform.position = new Vector3(
                maxWorldWithPlayerSize.x,
                this.transform.position.y,
                this.transform.position.z);
        }

        if (screenPos.x > maxScreenWithPlayerSize.x)
        {
            this.transform.position = new Vector3(
                minWorldWithPlayerSize.x,
                this.transform.position.y,
                this.transform.position.z);
        }

    }

}
