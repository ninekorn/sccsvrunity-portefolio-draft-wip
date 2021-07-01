using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;



public class chunklevel4 : IEnumerable
{
    private Vector3[] _people;

    Vector3 position;
    Vector3 chunkyPos;


    public chunklevel4(Vector3[] pArray)//Vector3 pos, Vector3 chunkPos)
    {
        //position = pos;
        //chunkyPos = chunkPos;

        _people = new Vector3[pArray.Length];

        for (int i = 0; i < pArray.Length; i++)
        {
            _people[i] = pArray[i];
        }
    }

    // Implementation for the GetEnumerator method.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    public chunkEnum GetEnumerator()
    {
        return new chunkEnum(_people);
    }
}


public class chunkEnum : IEnumerator
{
    public Vector3[] _people;
    int position = -1;
    float timer = 10000;
    int counter = 0;
    int counter1 = 0;
    public int one = 50;
    public int two = 50;

    


    public chunkEnum(Vector3[] list)
    {
        _people = list;
    }

    public bool MoveNext()
    {
        TimeSpan timeout;       
        position++;
        return (position < _people.Length);
    }

    public void Reset()
    {
        position = -1;
    }

    object IEnumerator.Current
    {     
        get
        {
            return Current;
        }
    }

    public Vector3 Current
    {
        get
        {
            /*if (timer > 0 && counter == 0)
            {
                waitsomeTime();
            }*/
            try
            {
                return _people[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }

    }

    void waitsomeTime()
    {

        if (two > 0)
        {
            two -= 1;
        }
        if (two == 40)
        {
            Debug.Log("ANUS");
        }
        if (two == 0)
        {
            Debug.Log("Finished");
        }


        /*while (timer> 0)
        {
            //Debug.Log("yo");
            timer -= Time.deltaTime;
            Debug.Log(timer);
        }*/

    }

    /*public void MyDelay (float seconds)
    {
      
        do
        {
            timer -= Time.deltaTime;
        } while (timer > 0);
        if (timer == 0 || timer < 0)
        {
            Debug.Log(timer);
            return;
        }

    }*/


}




/*public class chunk : IEnumerable<newFloorTiles>
{
    private Dictionary<Vector3, Vector3> chunky = new Dictionary<Vector3, Vector3>();

    Vector3 pos;
    newFloorTiles currentChunk;
    Vector3 chunkPos;


    public chunk(Vector3 position, Vector3 currentChunkPos)
    {
        pos = position;
        chunkPos = currentChunkPos;
    }

    public IEnumerator<newFloorTiles> GetEnumerator()
    {
        return chunky.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)chunky.Values).GetEnumerator();
    }

}*/

public class LevelGenerator4 : MonoBehaviour
{
    //public static List<Vector3> chunks = new List<Vector3>();
    //public static Dictionary<Vector3, Vector3> chunkz = new Dictionary<Vector3, Vector3>();


    public static LevelGenerator4 currentLevelGen;

    public GameObject[] tiles;
    public GameObject wall;

    public int tileAmount;
    public float tileSize;

    //public List<Vector3> createdTiles;

    public Dictionary<Vector3, Vector3> createdTiles = new Dictionary<Vector3, Vector3>();

    public float chanceUp;
    public float chanceRight;
    public float chanceDown;

    public float SpawnerMoveWaitTime;
    public float BuildingWaitTime;


    float minY = 0;
    float maxY = 0;
    float minX = 0;
    float maxX = 0;

    [HideInInspector]
    public float xAmount;
    [HideInInspector]
    public float yAmount;


    public List<Vector3> adjacentWall = new List<Vector3>();

    public List<Vector3> createdWall = new List<Vector3>();

    //public Dictionary<Vector3, Vector3> adjacentWall = new Dictionary<Vector3, Vector3>();

    public Dictionary<Vector3, Vector3> tilesCreated = new Dictionary<Vector3, Vector3>();

    Dictionary<Vector3, Vector3> ExtremityFloor = new Dictionary<Vector3, Vector3>();




    [HideInInspector]
    public List<Vector3> leftFrontCornerInside = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> rightFrontCornerInside = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> leftBackCornerInside = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> rightBackCornerInside = new List<Vector3>();

    [HideInInspector]
    public List<Vector3> leftWall = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> rightWall = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> frontWall = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> backWall = new List<Vector3>();

    [HideInInspector]
    public List<Vector3> builtLeftWall = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> builtRightWall = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> builtFrontWall = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> builtBackWall = new List<Vector3>();


    [HideInInspector]
    public List<Vector3> builtLeftFrontInsideCorner= new List<Vector3>();
    [HideInInspector]
    public List<Vector3> builtRightFrontInsideCorner = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> builtLeftBackInsideCorner = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> builtRightBackInsideCorner = new List<Vector3>();

    [HideInInspector]
    public List<Vector3> builtLeftFrontOutsideCorner = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> builtRightFrontOutsideCorner = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> builtLeftBackOutsideCorner = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> builtRightBackOutsideCorner = new List<Vector3>();




    [HideInInspector]
    public List<Vector3> leftFrontCornerOutside = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> rightFrontCornerOutside = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> leftBackCornerOutside = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> rightBackCornerOutside = new List<Vector3>();


    [HideInInspector]
    public List<Vector3> threeWayWallLeft = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> threeWayWallRight = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> threeWayWallFront = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> threeWayWallBack = new List<Vector3>();

    public GameObject sphere;
    public GameObject sphere1;

    public float chunkWidth = 10;
    
    //public GameObject tileSpawner;


    List<Vector3> toRemove = new List<Vector3>();
    //public Dictionary<Vector3, Vector3> toRemove = new Dictionary<Vector3, Vector3>();

    List<Vector3> floorTilesList = new List<Vector3>();

    public GameObject leftWallz;
    public GameObject rightWallz;
    public GameObject frontWallz;
    public GameObject backWallz;

    public GameObject leftFrontInsideCornerWall;
    public GameObject RightFrontInsideCornerWall;
    public GameObject leftBackInsideCornerWall;
    public GameObject RightBackInsideCornerWall;

    public GameObject leftFrontOutsideCornerWall;
    public GameObject RightFrontOutsideCornerWall;
    public GameObject leftBackOutsideCornerWall;
    public GameObject RightBackOutsideCornerWall;

    public GameObject floorTiles;

    int counter = 0;
    Vector3 currentTile;

    /*bool isTileLeft;
    bool isTileRight;
    bool isWallFront;
    bool isWallBack;*/

    bool isSurrounded = false;

    int counter999 = 0;
    public float blockSize;


    chunk chunker;

    List<Vector3> currentChunkPos = new List<Vector3>();


    List<Vector3> builtFloorTiles = new List<Vector3>();


    void Awake()
    {
        
        //chunkWidth = chunkWidth * 0.25f;
        //tileSize = tileSize * 0.25f;
        currentLevelGen = this;
        StartCoroutine(GenerateLevel());
    }

    IEnumerator GenerateLevel()
    {
        for (int i = 0; i < tileAmount; i++)
        {
            float dir = UnityEngine.Random.Range(0f, 1f);
            int tile = UnityEngine.Random.Range(0, tiles.Length);
            CreateTile(tile);
            CallMoveGen(dir);
            yield return new WaitForSeconds(SpawnerMoveWaitTime);

            if (i == tileAmount - 1)
            {
                
                Finish();
            }
        }
        yield return 0;
    }


    void CallMoveGen(float ranDir)
    {
        if (ranDir < chanceUp && ranDir > 0)
        {
            MoveGen(0);
        }
        else if (ranDir < chanceRight && ranDir > chanceUp)
        {
            MoveGen(1);
        }
        else if (ranDir < chanceDown && ranDir > chanceRight)
        {
            MoveGen(2);
        }
        else
        {
            MoveGen(3);
        }
    }



    void CreateTile(int tileIndex)
    {
        if (!createdTiles.ContainsKey(transform.position))
        {           
            Instantiate(floorTiles, transform.position, Quaternion.identity);

            floorTiles.tag = "chunks";
            createdTiles.Add(transform.position, transform.position);
            tilesCreated.Add(transform.position, transform.position);           
        }
        else
        {
            tileAmount++;
        }
    }



    void MoveGen(int dir)
    {
        switch (dir)
        {
            /*case 0:
                transform.position = new Vector3(transform.position.x, 0, transform.position.z + (tileSize*blockSize));
                break;
            case 1:
                transform.position = new Vector3(transform.position.x + (tileSize ), 0, transform.position.z);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x, 0, transform.position.z - (tileSize ));
                break;
            case 3:
                transform.position = new Vector3(transform.position.x - (tileSize ), 0, transform.position.z);
                break;*/


            case 0:
                transform.position = new Vector3(transform.position.x, 0, transform.position.z + (tileSize ));
                break;
            case 1:
                transform.position = new Vector3(transform.position.x + (tileSize ), 0, transform.position.z);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x, 0, transform.position.z - (tileSize ));
                break;
            case 3:
                transform.position = new Vector3(transform.position.x - (tileSize ), 0, transform.position.z);
                break;

        }
    }

    void Finish()
    {
        /*var enumerator1 = createdTiles.GetEnumerator();

        while (enumerator1.MoveNext())
        {
            var currentTuile = enumerator1.Current;
            currentTile = currentTuile.Key;
            Instantiate(floorTiles, currentTile, Quaternion.identity);
        }*/

        var enumerator0 = createdTiles.GetEnumerator();

        while (enumerator0.MoveNext())
        {
            var currentTuile = enumerator0.Current;
            currentTile = currentTuile.Key;
            checkAllSides(currentTile);
        }
    }


    void checkAllSides(Vector3 currentTilePos)
    {


        /*for (float x = (currentPos.x / planeSize) - viewSize0; x <= (currentPos.x / planeSize) + viewSize0; x += chunkSizeLOWDETAIL)
        {
            for (float y = (currentPos.y / planeSize) - viewSize1; y <= (currentPos.y / planeSize) + viewSize1; y += chunkSizeLOWDETAIL)
            {
                for (float z = (currentPos.z / planeSize) - viewSize0; z <= (currentPos.z / planeSize) + viewSize0; z += chunkSizeLOWDETAIL)
                {
                    float chunkX0 = Mathf.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL * planeSize;*/


        for (var x = -tileSize / blockSize; x <= tileSize /blockSize; x+=chunkWidth)
        {
            for (var z = -tileSize / blockSize; z <= tileSize / blockSize; z += chunkWidth)
            {
                /*if (x == 0 && z == 0)
                {
                    continue;
                }*/

                float checkX = Mathf.FloorToInt(((currentTilePos.x + x) / chunkWidth)) * chunkWidth ;
                float checkY = Mathf.FloorToInt(((currentTilePos.z + z) / chunkWidth)) * chunkWidth ;


                //float checkX = ((currentTilePos.x + x));
                //float checkY = ((currentTilePos.z + z));


                if (checkX == currentTilePos.x && checkY == currentTilePos.z + (tileSize * 1 ) ||
                    checkX == currentTilePos.x && checkY == currentTilePos.z - (tileSize * 1) ||
                    checkX == currentTilePos.x + (tileSize * 1) && checkY == currentTilePos.z ||
                    checkX == currentTilePos.x - (tileSize * 1)  && checkY == currentTilePos.z||

                    checkX == currentTilePos.x + (tileSize * 1)  && checkY == currentTilePos.z + (tileSize * 1) ||
                    checkX == currentTilePos.x - (tileSize * 1)  && checkY == currentTilePos.z + (tileSize * 1) ||
                    checkX == currentTilePos.x + (tileSize * 1)  && checkY == currentTilePos.z - (tileSize * 1) ||
                    checkX == currentTilePos.x - (tileSize * 1)  && checkY == currentTilePos.z - (tileSize * 1))
                {


                    //Instantiate(sphere, new Vector3(checkX, 0, checkY), Quaternion.identity);
                    if (!adjacentWall.Contains(new Vector3(checkX, 0, checkY)) && !createdTiles.ContainsKey(new Vector3(checkX, 0, checkY)))
                    {
                       
                        adjacentWall.Add(new Vector3(checkX, 0, checkY));                
                    }
                }
            }
        }

        /*var enumerator2 = createdTiles.GetEnumerator();

        while (enumerator2.MoveNext())
        {
            var tls0 = enumerator2.Current;
            Instantiate(sphere, new Vector3(tls0.Key.x, tls0.Key.y, tls0.Key.z), Quaternion.identity);
        }

      var enumerator1 = adjacentWall.GetEnumerator();

       while (enumerator1.MoveNext())
       {
           var tls0 = enumerator1.Current;
           Instantiate(sphere1, new Vector3(tls0.Key.x, tls0.Key.y, tls0.Key.z), Quaternion.identity);
       }*/

        if (counter == 0)
        {
            counter = 1;
        }
    }

    void Update()
    {
        if (counter== 1)
        {

            /*var enumerator0 = adjacentWall.GetEnumerator();
            while (enumerator0.MoveNext())
            {
                var currentTuile = enumerator0.Current;
                //currentTile = currentTuile;
                StartCoroutine(buildWalls(currentTuile));
            }*/

            for (int i = 0; i < adjacentWall.Count;i++)
            {
                var currentTuile = adjacentWall[i];
                StartCoroutine(buildWalls(currentTuile));
            }

            toRemove = adjacentWall;

            for (int i = 0; i < backWall.Count; i++)
            {
                toRemove.Remove(backWall[i]);
            }
            for (int i = 0; i < frontWall.Count; i++)
            {
                toRemove.Remove(frontWall[i]);
            }
            for (int i = 0; i < rightWall.Count; i++)
            {
                toRemove.Remove(rightWall[i]);
            }
            for (int i = 0; i < leftWall.Count; i++)
            {
                toRemove.Remove(leftWall[i]);
            }
            for (int i = 0; i < builtLeftFrontInsideCorner.Count; i++)
            {
                toRemove.Remove(builtLeftFrontInsideCorner[i]);
            }
            for (int i = 0; i < builtRightFrontInsideCorner.Count; i++)
            {
                toRemove.Remove(builtRightFrontInsideCorner[i]);
            }
            for (int i = 0; i < builtLeftBackInsideCorner.Count; i++)
            {
                toRemove.Remove(builtLeftBackInsideCorner[i]);
            }
            for (int i = 0; i < builtRightBackInsideCorner.Count; i++)
            {
                toRemove.Remove(builtRightBackInsideCorner[i]);
            }

            for (int i = 0; i < builtLeftFrontOutsideCorner.Count; i++)
            {
                toRemove.Remove(builtLeftFrontOutsideCorner[i]);
            }
            for (int i = 0; i < builtRightFrontOutsideCorner.Count; i++)
            {
                toRemove.Remove(builtRightFrontOutsideCorner[i]);
            }

            for (int i = 0; i < builtLeftBackOutsideCorner.Count; i++)
            {
                toRemove.Remove(builtLeftBackOutsideCorner[i]);
            }


            for (int i = 0; i < builtRightBackOutsideCorner.Count; i++)
            {
                toRemove.Remove(builtRightBackOutsideCorner[i]);
            }

            if (counter999 == 0)
            {

                for (int i = 0; i < toRemove.Count; i++)
                {
                    var currentTile = toRemove[i];
                    StartCoroutine(buildFloorTiles());
                }
                counter999 = 1;
            }
            counter = 2;
        }
    }


    IEnumerator buildWalls(Vector3 currentTile)
    {

        /////////////////////////////////////LEFTWALL/////////////////////////////////////////
        bool leftTilly0 = findTiles(currentTile.x - chunkWidth *blockSize, currentTile.z);
        bool rightTilly0 = findTiles(currentTile.x + chunkWidth , currentTile.z);  

        bool frontWally0 = findWalls(currentTile.x , currentTile.z + chunkWidth );
        bool backWally0 = findWalls(currentTile.x , currentTile.z - chunkWidth );

        bool leftWally0 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightWally0 = findWalls(currentTile.x + chunkWidth , currentTile.z);

        if (leftTilly0 == false &&
           rightTilly0 == true &&
           frontWally0 == true &&
           backWally0 == true&&
           leftWally0 == false &&
           rightWally0 == false ||

           leftTilly0 == false &&
           rightTilly0 == false &&
           frontWally0 == true &&
           backWally0 == true &&
           leftWally0 == false &&
           rightWally0 == true)
        {
            if (!leftWall.Contains(currentTile))
            {
                leftWall.Add(currentTile);
                StartCoroutine(buildWallLeft());           
            }
        }
        /////////////////////////////////////RIGHTWALL/////////////////////////////////////////

        bool leftTilly1 = findTiles(currentTile.x - chunkWidth , currentTile.z);
        bool rightTilly1 = findTiles(currentTile.x + chunkWidth , currentTile.z);
        bool frontWally1 = findWalls(currentTile.x, currentTile.z + chunkWidth );
        bool backWally1 = findWalls(currentTile.x, currentTile.z - chunkWidth );

        bool leftWally1 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightWally1 = findWalls(currentTile.x + chunkWidth , currentTile.z);


        if (leftTilly1 == true &&
           rightTilly1 == false &&
           frontWally1 == true &&
           backWally1 == true&&
           leftWally0 == false&&
           rightWally0 == false ||

           leftTilly1 == false &&
           rightTilly1 == false &&
           frontWally1 == true &&
           backWally1 == true &&
           leftWally0 == true &&
           rightWally0 == false)
        {
            if (!rightWall.Contains(currentTile))
            {
                rightWall.Add(currentTile);
                StartCoroutine(buildWallRight());
            }
        }

        /////////////////////////////////////FRONTWALL/////////////////////////////////////////

        bool leftTilly2 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightTilly2 = findWalls(currentTile.x + chunkWidth , currentTile.z);
        bool frontTilly2 = findTiles(currentTile.x, currentTile.z + chunkWidth );
        bool backTilly2 = findTiles(currentTile.x, currentTile.z - chunkWidth );

        bool frontWally2 = findWalls(currentTile.x, currentTile.z + chunkWidth );
        bool backWally2 = findWalls(currentTile.x, currentTile.z - chunkWidth );


        if (leftTilly2 == true &&
           rightTilly2 == true &&
           frontTilly2 == true &&
           backTilly2 == false &&
           frontWally2 == false&&
           backWally2 == false ||

           leftTilly2 == true &&
           rightTilly2 == true &&
           frontTilly2 == false &&
           backTilly2 == false &&
           frontWally2 == true &&
           backWally2 == false)

        {
            if (!frontWall.Contains(currentTile))
            {
                frontWall.Add(currentTile);
                StartCoroutine(buildWallFront());
            }
        }

        /////////////////////////////////////BACKWALL/////////////////////////////////////////

        bool leftTilly3 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightTilly3 = findWalls(currentTile.x + chunkWidth , currentTile.z);
        bool frontTilly3 = findTiles(currentTile.x, currentTile.z + chunkWidth );
        bool backTilly3 = findTiles(currentTile.x, currentTile.z - chunkWidth );

        bool frontWally3 = findWalls(currentTile.x, currentTile.z + chunkWidth );
        bool backWally3 = findWalls(currentTile.x, currentTile.z - chunkWidth );

        if (leftTilly3 == true &&
           rightTilly3 == true &&
           frontTilly3 == false &&
           backTilly3 == true &&
           frontWally3 == false &&
           backWally3 == false ||

           leftTilly3 == true &&
           rightTilly3 == true &&
           frontTilly3 == false &&
           backTilly3 == false &&
           frontWally3 == false &&
           backWally3 == true)

        {
            if (!backWall.Contains(currentTile))
            {
                backWall.Add(currentTile);
                StartCoroutine(buildWallBack());
            }
        }

        /////////////////////////////////////LEFTFRONTINSIDECORNER/////////////////////////////////////////

        bool leftTilly4 = findTiles(currentTile.x - chunkWidth , currentTile.z);
        //bool rightTilly4 = findTiles(currentTile.x + chunkWidth, currentTile.z);

        bool frontTilly4 = findTiles(currentTile.x, currentTile.z + chunkWidth );
        bool frontWally4 = findWalls(currentTile.x, currentTile.z + chunkWidth );

        bool backWally4 = findWalls(currentTile.x, currentTile.z - chunkWidth );

        bool leftWally4 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightWally4 = findWalls(currentTile.x + chunkWidth , currentTile.z);

        if (leftTilly4== false &&
           frontTilly4 == false &&
           frontWally4 == false &&
           backWally4 == true &&
           leftWally4 == false &&
           rightWally4 == true)
        {
            if (!leftFrontCornerInside.Contains(currentTile))
            {
                leftFrontCornerInside.Add(currentTile);
                StartCoroutine(buildLeftFrontInsideCorner());
            }
        }


        /////////////////////////////////////RightFRONTINSIDECORNER/////////////////////////////////////////

        //bool leftTilly5 = findTiles(currentTile.x - chunkWidth, currentTile.z);
        bool rightTilly5 = findTiles(currentTile.x + chunkWidth , currentTile.z);

        bool frontTilly5 = findTiles(currentTile.x, currentTile.z + chunkWidth );
        bool frontWally5 = findWalls(currentTile.x, currentTile.z + chunkWidth );

        bool backWally5 = findWalls(currentTile.x, currentTile.z - chunkWidth );

        bool leftWally5 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightWally5 = findWalls(currentTile.x + chunkWidth , currentTile.z);

        if (rightTilly5 == false &&
           frontTilly5 == false &&
           frontWally5 == false &&
           backWally5 == true &&
           leftWally5 == true &&
           rightWally5 == false)
        {
            if (!rightFrontCornerInside.Contains(currentTile))
            {
                rightFrontCornerInside.Add(currentTile);
                StartCoroutine(buildRightFrontInsideCorner());
            }
        }
        /////////////////////////////////////LEFTBACKINSIDECORNER/////////////////////////////////////////

        bool leftTilly6 = findTiles(currentTile.x - chunkWidth , currentTile.z);
        //bool rightTilly6 = findTiles(currentTile.x + chunkWidth, currentTile.z);

        //bool frontTilly6 = findTiles(currentTile.x, currentTile.z + chunkWidth);
        bool frontWally6 = findWalls(currentTile.x, currentTile.z + chunkWidth );

        bool backWally6 = findWalls(currentTile.x, currentTile.z - chunkWidth );
        bool backTilly6 = findTiles(currentTile.x, currentTile.z - chunkWidth );

        bool leftWally6 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightWally6 = findWalls(currentTile.x + chunkWidth , currentTile.z);

        if (leftTilly6 == false &&
           //frontTilly6 == false &&
           frontWally6 == true &&
           backWally6 == false &&
           backTilly6 == false &&
           leftWally6 == false &&
           rightWally6 == true)
        {
            if (!leftBackCornerInside.Contains(currentTile))
            {
                leftBackCornerInside.Add(currentTile);
                StartCoroutine(buildLeftBackInsideCorner());
            }
        }

        /////////////////////////////////////RightBACKINSIDECORNER/////////////////////////////////////////

        //bool leftTilly7 = findTiles(currentTile.x - chunkWidth, currentTile.z);
        bool rightTilly7 = findTiles(currentTile.x + chunkWidth , currentTile.z);

        //bool frontTilly7 = findTiles(currentTile.x, currentTile.z + chunkWidth);
        bool frontWally7 = findWalls(currentTile.x, currentTile.z + chunkWidth );

        bool backWally7 = findWalls(currentTile.x, currentTile.z - chunkWidth );
        bool backTilly7 = findTiles(currentTile.x, currentTile.z - chunkWidth );

        bool leftWally7 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightWally7 = findWalls(currentTile.x + chunkWidth , currentTile.z);

        if (rightTilly7 == false &&
           //frontTilly7 == false &&
           frontWally7 == true &&
           backWally7 == false &&
           backTilly7 == false&&
           leftWally7 == true &&
           rightWally7 == false)
        {
            if (!rightBackCornerInside.Contains(currentTile))
            {
                rightBackCornerInside.Add(currentTile);
                StartCoroutine(buildRightBackInsideCorner());
            }
        }


        /////////////////////////////////////LEFTFRONTOUTSIDECORNER/////////////////////////////////////////

        //bool leftTilly8 = findTiles(currentTile.x - chunkWidth, currentTile.z);
        bool rightTilly8 = findTiles(currentTile.x + chunkWidth , currentTile.z);

        //bool frontTilly8 = findTiles(currentTile.x, currentTile.z + chunkWidth);
        bool frontWally8 = findWalls(currentTile.x, currentTile.z + chunkWidth );

        bool backWally8 = findWalls(currentTile.x, currentTile.z - chunkWidth );
        bool backTilly8 = findTiles(currentTile.x, currentTile.z - chunkWidth );

        bool leftWally8 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightWally8 = findWalls(currentTile.x + chunkWidth , currentTile.z);

        bool leftFrontTilly8 = findTiles(currentTile.x - chunkWidth , currentTile.z + chunkWidth );
        bool leftFrontWally8 = findWalls(currentTile.x - chunkWidth , currentTile.z + chunkWidth );

        bool rightBackTilly8 = findTiles(currentTile.x + chunkWidth , currentTile.z - chunkWidth );
        bool rightBackWally8 = findWalls(currentTile.x + chunkWidth , currentTile.z - chunkWidth );

        if (frontWally8 == true &&
           backTilly8 == true &&
           leftWally8 == true &&
           rightTilly8 == true &&
           leftFrontTilly8 == false &&
           leftFrontWally8 == false &&
           rightBackTilly8 == true||

           frontWally8 == true &&
           backTilly8 == true &&
           leftWally8 == true &&
           rightWally8 == true &&
           leftFrontTilly8 == false &&
           leftFrontWally8 == false &&
           rightBackTilly8 == true ||

           frontWally8 == true &&
           backWally8 == true &&
           leftWally8 == true &&
           rightTilly8 == true &&
           leftFrontTilly8 == false &&
           leftFrontWally8 == false &&
           rightBackTilly8 == true ||

           frontWally8 == true &&
           backWally8 == true &&
           leftWally8 == true &&
           rightWally8 == true &&
           leftFrontTilly8 == false &&
           leftFrontWally8 == false &&
           rightBackTilly8 == true ||

           frontWally8 == true &&
           backTilly8 == true &&
           leftWally8 == true &&
           rightTilly8 == true &&
           leftFrontTilly8 == false &&
           leftFrontWally8 == false &&
           rightBackWally8 == true||

            frontWally8 == true &&
            backTilly8 == true &&
            leftWally8 == true &&
            rightWally8 == true &&
            leftFrontTilly8 == false &&
            leftFrontWally8 == false &&
            rightBackWally8 == true ||

            frontWally8 == true &&
            backWally8 == true &&
            leftWally8 == true &&
            rightTilly8 == true &&
            leftFrontTilly8 == false &&
            leftFrontWally8 == false &&
            rightBackWally8 == true ||

            frontWally8 == true &&
            backWally8 == true &&
            leftWally8 == true &&
            rightWally8 == true &&
            leftFrontTilly8 == false &&
            leftFrontWally8 == false &&
            rightBackWally8 == true )
        {
            if (!leftFrontCornerOutside.Contains(currentTile))
            {
                leftFrontCornerOutside.Add(currentTile);
                StartCoroutine(buildLeftFrontOutsideCorner());
            }
        }
        /////////////////////////////////////RIGHTFRONTOUTSIDECORNER/////////////////////////////////////////

        bool leftWally9 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightTilly9 = findTiles(currentTile.x + chunkWidth , currentTile.z);

        //bool frontTilly9 = findTiles(currentTile.x, currentTile.z + chunkWidth);
        bool frontWally9 = findWalls(currentTile.x, currentTile.z + chunkWidth );

        bool backWally9 = findWalls(currentTile.x, currentTile.z - chunkWidth );
        bool backTilly9 = findTiles(currentTile.x, currentTile.z - chunkWidth );

        bool leftTilly9 = findTiles(currentTile.x - chunkWidth , currentTile.z);
        bool rightWally9 = findWalls(currentTile.x + chunkWidth , currentTile.z);

        bool leftBackTilly9 = findTiles(currentTile.x - chunkWidth , currentTile.z - chunkWidth );
        bool leftBackWally9 = findWalls(currentTile.x - chunkWidth , currentTile.z - chunkWidth );

        bool rightFrontWally9 = findWalls(currentTile.x + chunkWidth , currentTile.z + chunkWidth );
        bool rightFrontTilly9 = findTiles(currentTile.x + chunkWidth , currentTile.z + chunkWidth );



        if (frontWally9 == true &&
           backTilly9 == true &&
           leftTilly9 == true &&
           rightWally9 == true &&
           leftBackTilly9 == true &&
           rightFrontWally9 == false &&
           rightFrontTilly9 == false ||

           frontWally9 == true &&
           backTilly9 == true &&
           leftWally9 == true &&
           rightWally9 == true &&
           leftBackTilly9 == true &&
           rightFrontWally9 == false &&
           rightFrontTilly9 == false ||

           frontWally9 == true &&
           backWally9 == true &&
           leftTilly9 == true &&
           rightWally9 == true &&
           leftBackTilly9 == true &&
           rightFrontWally9 == false &&
           rightFrontTilly9 == false ||

           frontWally9 == true &&
           backWally9 == true &&
           leftWally9 == true &&
           rightWally9 == true &&
           leftBackTilly9 == true &&
           rightFrontWally9 == false &&
           rightFrontTilly9 == false ||


           frontWally9 == true &&
           backTilly9 == true &&
           leftTilly9 == true &&
           rightWally9 == true &&
           leftBackWally9 == true &&
           rightFrontWally9 == false &&
           rightFrontTilly9 == false ||


           frontWally9 == true &&
           backTilly9 == true &&
           leftWally9 == true &&
           rightWally9 == true &&
           leftBackWally9 == true &&
           rightFrontWally9 == false &&
           rightFrontTilly9 == false ||

           frontWally9 == true &&
           backWally9 == true &&
           leftTilly9 == true &&
           rightWally9 == true &&
           leftBackWally9 == true &&
           rightFrontWally9 == false &&
           rightFrontTilly9 == false||

           frontWally9 == true &&
           backWally9 == true &&
           leftWally9 == true &&
           rightWally9 == true &&
           leftBackWally9 == true &&
           rightFrontWally9 == false &&
           rightFrontTilly9 == false)
        {
            if (!rightFrontCornerOutside.Contains(currentTile))
            {
                rightFrontCornerOutside.Add(currentTile);
                StartCoroutine(buildRightFrontOutsideCorner());
            }
        }


        /////////////////////////////////////LEFTBACKOUTSIDECORNER/////////////////////////////////////////

        bool leftTilly10 = findTiles(currentTile.x - chunkWidth , currentTile.z);
        bool rightTilly10 = findTiles(currentTile.x + chunkWidth , currentTile.z);

        bool frontTilly10 = findTiles(currentTile.x, currentTile.z + chunkWidth );
        bool frontWally10 = findWalls(currentTile.x, currentTile.z + chunkWidth );

        bool backWally10 = findWalls(currentTile.x, currentTile.z - chunkWidth );

        bool leftWally10 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightWally10 = findWalls(currentTile.x + chunkWidth , currentTile.z);

        bool leftBackTilly10 = findTiles(currentTile.x - chunkWidth , currentTile.z - chunkWidth );
        bool leftBackWally10 = findWalls(currentTile.x - chunkWidth , currentTile.z - chunkWidth );

        bool rightFrontWally10 = findWalls(currentTile.x + chunkWidth , currentTile.z + chunkWidth );
        bool rightFrontTilly10 = findTiles(currentTile.x + chunkWidth , currentTile.z + chunkWidth );



        if (leftWally10 == true &&
           frontTilly10 == true &&
           backWally10 == true &&
           rightTilly10 == true &&
           leftBackTilly10 == false &&
           leftBackWally10 == false &&
           rightFrontTilly10 == true ||

           leftWally10 == true &&
           frontWally10 == true &&
           backWally10 == true &&
           rightTilly10 == true &&
           leftBackTilly10 == false &&
           leftBackWally10 == false &&
           rightFrontTilly10 == true ||

           leftWally10 == true &&
           frontTilly10 == true &&
           backWally10 == true &&
           rightWally10 == true &&
           leftBackTilly10 == false &&
           leftBackWally10 == false &&
           rightFrontTilly10 == true ||

           leftWally10 == true &&
           frontWally10 == true &&
           backWally10 == true &&
           rightWally10 == true &&
           leftBackTilly10 == false &&
           leftBackWally10 == false &&
           rightFrontTilly10 == true ||

           leftWally10 == true &&
           frontTilly10 == true &&
           backWally10 == true &&
           rightTilly10 == true &&
           leftBackTilly10 == false &&
           leftBackWally10 == false &&
           rightFrontWally10 == true ||

           leftWally10 == true &&
           frontWally10 == true &&
           backWally10 == true &&
           rightTilly10 == true &&
           leftBackTilly10 == false &&
           leftBackWally10 == false &&
           rightFrontWally10 == true ||

           leftWally10 == true &&
           frontTilly10 == true &&
           backWally10 == true &&
           rightWally10 == true &&
           leftBackTilly10 == false &&
           leftBackWally10 == false &&
           rightFrontWally10 == true ||

           leftWally10 == true &&
           frontWally10 == true &&
           backWally10 == true &&
           rightWally10 == true &&
           leftBackTilly10 == false &&
           leftBackWally10 == false &&
           rightFrontWally10 == true )


        {
            if (!leftBackCornerOutside.Contains(currentTile))
            {
                leftBackCornerOutside.Add(currentTile);
                StartCoroutine(buildLeftBackOutsideCorner());
            }
        }
        /////////////////////////////////////RightBACKOUTSIDECORNER/////////////////////////////////////////


        bool leftTilly11 = findTiles(currentTile.x - chunkWidth , currentTile.z);
        bool rightTilly11 = findTiles(currentTile.x + chunkWidth , currentTile.z);

        bool frontTilly11 = findTiles(currentTile.x, currentTile.z + chunkWidth );
        bool frontWally11 = findWalls(currentTile.x, currentTile.z + chunkWidth );

        bool backWally11 = findWalls(currentTile.x, currentTile.z - chunkWidth );

        bool leftWally11 = findWalls(currentTile.x - chunkWidth , currentTile.z);
        bool rightWally11 = findWalls(currentTile.x + chunkWidth , currentTile.z);

        bool leftFrontTilly11 = findTiles(currentTile.x - chunkWidth , currentTile.z + chunkWidth );
        bool leftFrontWally11 = findWalls(currentTile.x - chunkWidth , currentTile.z + chunkWidth );

        bool rightBackTilly11 = findTiles(currentTile.x + chunkWidth , currentTile.z - chunkWidth );
        bool rightBackWally11= findWalls(currentTile.x + chunkWidth , currentTile.z - chunkWidth );

        if (leftTilly11 == true &&
           frontTilly11 == true &&
           backWally11 == true &&
           rightWally11 == true &&
           rightBackWally11 == false &&
           rightBackTilly11 == false &&
           leftFrontTilly11 == true ||

           leftWally11 == true &&
           frontTilly11 == true &&
           backWally11 == true &&
           rightWally11 == true &&
           rightBackWally11 == false &&
           rightBackTilly11 == false &&
           leftFrontTilly11 == true ||

           leftTilly11 == true &&
           frontWally11 == true &&
           backWally11 == true &&
           rightWally11 == true &&
           rightBackWally11 == false &&
           rightBackTilly11 == false &&
           leftFrontTilly11 == true ||

           leftWally11 == true &&
           frontWally11 == true &&
           backWally11 == true &&
           rightWally11 == true &&
           rightBackWally11 == false &&
           rightBackTilly11 == false &&
           leftFrontTilly11 == true ||

           leftTilly11 == true &&
           frontTilly11 == true &&
           backWally11 == true &&
           rightWally11 == true &&
           rightBackWally11 == false &&
           rightBackTilly11 == false &&
           leftFrontWally11 == true ||

           leftWally11 == true &&
           frontTilly11 == true &&
           backWally11 == true &&
           rightWally11 == true &&
           rightBackWally11 == false &&
           rightBackTilly11 == false &&
           leftFrontWally11 == true ||

           leftTilly11 == true &&
           frontWally11 == true &&
           backWally11 == true &&
           rightWally11 == true &&
           rightBackWally11 == false &&
           rightBackTilly11 == false &&
           leftFrontWally11 == true ||

           leftWally11 == true &&
           frontWally11 == true &&
           backWally11 == true &&
           rightWally11 == true &&
           rightBackWally11 == false &&
           rightBackTilly11 == false &&
           leftFrontWally11 == true)

        {
            if (!rightBackCornerOutside.Contains(currentTile))
            {
                rightBackCornerOutside.Add(currentTile);
                StartCoroutine(buildRightBackOutsideCorner());
                // StopCoroutine("checkForWallLeft");
            }
        }

        yield return new WaitForSeconds(BuildingWaitTime);
       // Instantiate(sphere, currentTile, Quaternion.identity);
    }


    bool findWalls(float x, float z)
    {
        var enumerator0 = adjacentWall.GetEnumerator();
        //Vector3? tls0 = null;     
        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;
            var tuile = tls0;

            if ((x < tuile.x ) || (z < tuile.z ) || (x >= (tuile.x ) + chunkWidth ) || (z >= (tuile.z) + chunkWidth ))
            {
                continue;
            }
            return true;
        }
        return false;
    }




    bool findTiles(float x, float z)
    {
        var enumerator0 = createdTiles.GetEnumerator();
        //Vector3? tls0 = null;     
        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;
            var tuile = tls0.Key;

            if ((x < tuile.x ) || (z < tuile.z ) || (x >= (tuile.x ) + chunkWidth ) || (z >= (tuile.z ) + chunkWidth *blockSize))
            {
                continue;
            }
            return true;
        }
        return false;
    }



    /*bool findWalls(float x, float z)
    {
        var enumerator0 = adjacentWall.GetEnumerator();
        //Vector3? tls0 = null;     
        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;
            var tuile = tls0.Key;

            if ((x < tuile.x + blockSize) || (z < tuile.z + blockSize) || (x >= (tuile.x + blockSize) + chunkWidth ) || (z >= (tuile.z + blockSize) + chunkWidth ))
            {
                continue;
            }
            return true;
        }
        return false;
    }




    bool findTiles(float x, float z)
    {
        var enumerator0 = createdTiles.GetEnumerator();
        //Vector3? tls0 = null;     
        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;
            var tuile = tls0.Key;

            if ((x < tuile.x + blockSize) || (z < tuile.z + blockSize) || (x >= (tuile.x + blockSize) + chunkWidth ) || (z >= (tuile.z + blockSize) + chunkWidth ))
            {
                Debug.Log("anus");
                continue;
            }
            return true;
        }
        return false;
    }*/





    IEnumerator buildWallLeft()
    {
        for (int i = 0; i < leftWall.Count;i++)
        {
            if (!builtLeftWall.Contains(leftWall[i]))
            {
                Instantiate(leftWallz, leftWall[i], Quaternion.identity);
                builtLeftWall.Add(leftWall[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);


    }


    IEnumerator buildWallRight()
    {
        for (int i = 0; i < rightWall.Count; i++)
        {
            if (!builtRightWall.Contains(rightWall[i]))
            {
                Instantiate(rightWallz, rightWall[i], Quaternion.identity);
                builtRightWall.Add(rightWall[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);       
    }

    

    IEnumerator buildWallFront()
    {
        for (int i = 0; i < frontWall.Count; i++)
        {
            if (!builtFrontWall.Contains(frontWall[i]))
            {
                Instantiate(frontWallz, frontWall[i], Quaternion.identity);
                builtFrontWall.Add(frontWall[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }



    IEnumerator buildWallBack()
    {
        for (int i = 0; i < backWall.Count; i++)
        {
            if (!builtBackWall.Contains(backWall[i]))
            {
                Instantiate(backWallz, backWall[i], Quaternion.identity);
                builtBackWall.Add(backWall[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }


    IEnumerator buildLeftFrontInsideCorner()
    {
        for (int i = 0; i < leftFrontCornerInside.Count; i++)
        {
            if (!builtLeftFrontInsideCorner.Contains(leftFrontCornerInside[i]))
            {
                Instantiate(leftFrontInsideCornerWall, leftFrontCornerInside[i], Quaternion.identity);
                builtLeftFrontInsideCorner.Add(leftFrontCornerInside[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }


    IEnumerator buildRightFrontInsideCorner()
    {
        for (int i = 0; i < rightFrontCornerInside.Count; i++)
        {
            if (!builtRightFrontInsideCorner.Contains(rightFrontCornerInside[i]))
            {
                Instantiate(RightFrontInsideCornerWall, rightFrontCornerInside[i], Quaternion.identity);
                builtRightFrontInsideCorner.Add(rightFrontCornerInside[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }


    IEnumerator buildLeftBackInsideCorner()
    {
        for (int i = 0; i < leftBackCornerInside.Count; i++)
        {
            if (!builtLeftBackInsideCorner.Contains(leftBackCornerInside[i]))
            {
                Instantiate(leftBackInsideCornerWall, leftBackCornerInside[i], Quaternion.identity);
                builtLeftBackInsideCorner.Add(leftBackCornerInside[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }





    IEnumerator buildRightBackInsideCorner()
    {
        for (int i = 0; i < rightBackCornerInside.Count; i++)
        {
            if (!builtRightBackInsideCorner.Contains(rightBackCornerInside[i]))
            {
                Instantiate(RightBackInsideCornerWall, rightBackCornerInside[i], Quaternion.identity);
                builtRightBackInsideCorner.Add(rightBackCornerInside[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }




    IEnumerator buildLeftFrontOutsideCorner()
    {
        for (int i = 0; i < leftFrontCornerOutside.Count; i++)
        {
            if (!builtLeftFrontOutsideCorner.Contains(leftFrontCornerOutside[i]))
            {
                Instantiate(leftFrontOutsideCornerWall, leftFrontCornerOutside[i], Quaternion.identity);
                builtLeftFrontOutsideCorner.Add(leftFrontCornerOutside[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }




    IEnumerator buildRightFrontOutsideCorner()
    {
        for (int i = 0; i < rightFrontCornerOutside.Count; i++)
        {
            if (!builtRightFrontOutsideCorner.Contains(rightFrontCornerOutside[i]))
            {
                Instantiate(RightFrontOutsideCornerWall, rightFrontCornerOutside[i], Quaternion.identity);
                builtRightFrontOutsideCorner.Add(rightFrontCornerOutside[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }




    IEnumerator buildLeftBackOutsideCorner()
    {
        for (int i = 0; i < leftBackCornerOutside.Count; i++)
        {
            if (!builtLeftBackOutsideCorner.Contains(leftBackCornerOutside[i]))
            {
                Instantiate(leftBackOutsideCornerWall, leftBackCornerOutside[i], Quaternion.identity);
                builtLeftBackOutsideCorner.Add(leftBackCornerOutside[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }




    IEnumerator buildRightBackOutsideCorner()
    {
        for (int i = 0; i < rightBackCornerOutside.Count; i++)
        {
            if (!builtRightBackOutsideCorner.Contains(rightBackCornerOutside[i]))
            {
                Instantiate(RightBackOutsideCornerWall, rightBackCornerOutside[i], Quaternion.identity);
                builtRightBackOutsideCorner.Add(rightBackCornerOutside[i]);
            }
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }














    IEnumerator buildFloorTiles()
    {
        yield return new WaitForSeconds(BuildingWaitTime);

        for (int i = 0; i < toRemove.Count; i++)
        {
            if (!builtFloorTiles.Contains(toRemove[i]))
            {
                Instantiate(floorTiles, toRemove[i], Quaternion.identity);
                builtFloorTiles.Add(toRemove[i]);
                yield return new WaitForSeconds(BuildingWaitTime);
            }
            yield return new WaitForSeconds(BuildingWaitTime);
        }
        yield return new WaitForSeconds(BuildingWaitTime);
    }











    WaitForSeconds slowdown = new WaitForSeconds(2f);

    IEnumerator waitFunction()
    {
        yield return slowdown;
        //StartCoroutine("checkForWallLeft");
    }



    /*public static bool FindChunk(Vector3 pos)
    {
        for (int a = 0; a < chunks.Count; a++)
        {
            Vector3 cpos = chunks[a].transform.position;

            if ((pos.x < cpos.x) || (pos.z < cpos.z) || (pos.x >= cpos.x + width - 2) || (pos.z >= cpos.z + width - 2)) continue;
            //return chunks[a];
            return true;

        }
        return false;
    }*/













    public static newFloorTiles GetChunk(float x, float y, float z)
    {
        var enumerator0 = newFloorTiles.chunkz.GetEnumerator();

        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;

            if ((x < tls0.Value.x) || y < tls0.Value.y || (z < tls0.Value.z) || (x >= (tls0.Value.x) + 10) || (y >= (tls0.Value.y) + 10) || (z >= tls0.Value.z + 10))
            {
                continue;
            }
            return tls0.Key;
        }
        return null;
    }

















    /*bool findTiles(float x, float z)
    {
        var enumerator0 = tilesCreated.GetEnumerator();
        //Vector3? tls0 = null;     
        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;
            var tuile = tls0.Key;

            if ((x < tuile.x) || (z < tuile.z) || (x >= (tuile.x) + chunkWidth) || (z >= tuile.z + chunkWidth))
            {
                continue;
            }
            return true;
        }
        return false;
    }*/


    /*bool findWalls(float x, float z)
    {
        var enumerator0 = adjacentWall.GetEnumerator();
        //Vector3? tls0 = null;     
        while (enumerator0.MoveNext())
        {
            var tls0 = enumerator0.Current;
            var tuile = tls0.Key;

            if ((x < tuile.x) || (z < tuile.z) || (x >= (tuile.x) + chunkWidth) || (z >= tuile.z + chunkWidth))
            {
                continue;
            }
            return true;
        }
        return false;
    }*/

}





/* StartCoroutine(DelayedCallback((float x, float z) =>
               {

               }));     
   public IEnumerator DelayedCallback(System.Action<float,float> callBack)
   {

       int counter999 = 1;
       yield return new WaitForSeconds(1f);
       yield return counter999;
       Debug.Log("yo");

   }*/







/*IEnumerator TryToSleep()
{
    float x = currentTile.x;
    float z = currentTile.z;

    var request = CountSheep();
    yield return StartCoroutine(request);
    int? result = request.Current as int?;
    Debug.Log(result);

}*/

/*IEnumerator CountSheep()
{
    int count = 0;
    while (count <99)
    {
        Debug.Log(count);
        yield return new WaitForSeconds(0.05f);
        count++;
    }      
    yield return count;
}*/
