using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.Entities;


public class createObjectArray : MonoBehaviour
{
    public cubicFrac[] someCubicFracArray;// = new Entity[];
    //public Entity[] someEntityArray;// = new Entity[];
    public int[] someEntityArrayIndex;// = new Entity[];

    //public GameObject objectToSpawn;

    static int wr = 2;
    static int wl = 1;

    static int hb = 2;
    static int ht = 1;

    static int df = 2;
    static int db = 1;

    Vector3 initialPosition = new Vector3(0,2,0);

    NewObjectPoolerScript newObjectPoolerScript;

    int total = 1;// (wr + wl + 1) * (hb + ht + 1) * (df + db + 1);// SC_Globals.tinyChunkWidth * SC_Globals.tinyChunkHeight * SC_Globals.tinyChunkDepth;

    int xx = 0;
    int yy = 0;
    int zz = 0;

    int switchXX = 0;
    int switchYY = 0;

    int t = 0;
    public float sizeOfCube = 0.1f;

    //public GameObject terrain;

    //private Entity retrievedPooledObjectEntity;
    //private BlobAssetStore blobAssetStore;
  


    // Start is called before the first frame update
    void Start()
    {
        //unity official object pool tutorial. but named differently.
         newObjectPoolerScript = GetComponent<NewObjectPoolerScript>();

        initialPosition += transform.position;

        /*for (int x = wr;x < wl; x++)
        {
            for (int y = hb; y < ht; y++)
            {
                for (int z = df; z < db; z++)
                {
                    Instantiate(objectToSpawn, new Vector3(x,y,z),Quaternion.identity);
                }
            }
        }*/


        total = (wr + wl + 1) * (hb + ht + 1) * (df + db + 1);
        someCubicFracArray = new cubicFrac[total];
        //someEntityArray = new Entity[total];
        someEntityArrayIndex = new int[total];


        xx = 0;
        yy = 0;
        zz = 0;
        switchXX = 0;
        switchYY = 0;
        t = 0;

        StartCoroutine("SimpleCoroutine");
    }

    IEnumerator SimpleCoroutine()
    {
        for (; t < total;)
        {
            /*int currentByte = map[xx + SC_Globals.tinyChunkWidth * (yy + SC_Globals.tinyChunkHeight * zz)];

            int index = xx + (SC_Globals.tinyChunkWidth * (yy + (SC_Globals.tinyChunkHeight * zz)));

            if (index >= 0 && index <= 7)
            {
                
            }*/
            //Instantiate(objectToSpawn, new Vector3(initialPosition.x + xx, initialPosition.y + yy, initialPosition.z + zz), Quaternion.identity);
            var retrievedPooledObject = newObjectPoolerScript.GetPooledObject();     
            retrievedPooledObject.transform.position = new Vector3(initialPosition.x + (xx* sizeOfCube), initialPosition.y + (yy* sizeOfCube), initialPosition.z + (zz* sizeOfCube));
            retrievedPooledObject.SetActive(true);

            someCubicFracArray[t] = retrievedPooledObject.GetComponent<cubicFrac>();

            /*blobAssetStore = new BlobAssetStore();
            GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
            retrievedPooledObjectEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(retrievedPooledObject, settings);

            someCubicFracArray[t] = retrievedPooledObject.GetComponent<cubicFrac>();
            someEntityArray[t] = retrievedPooledObjectEntity;
            someEntityArrayIndex[t] = retrievedPooledObjectEntity.Index;

            Destroy(retrievedPooledObject);*/

            zz++;
            if (zz >= (df + db + 1))
            {
                yy++;
                zz = 0;
                switchYY = 1;
            }
            if (yy >= (hb + ht + 1) && switchYY == 1)
            {
                xx++;
                yy = 0;
                switchYY = 0;
                switchXX = 1;
            }
            if (xx >= (wr + wl + 1) && switchXX == 1)
            {
                xx = 0;
                switchXX = 0;
                break;
            }
        }
        t++;
        yield return new WaitForSeconds(1);
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}



/*int total = SC_Globals.tinyChunkWidth * SC_Globals.tinyChunkHeight * SC_Globals.tinyChunkDepth;

int xx = 0;
int yy = 0;
int zz = 0;

int switchXX = 0;
int switchYY = 0;

for (int t = 0; t<total; t++)
{
    int currentByte = map[xx + SC_Globals.tinyChunkWidth * (yy + SC_Globals.tinyChunkHeight * zz)];

int index = xx + (SC_Globals.tinyChunkWidth * (yy + (SC_Globals.tinyChunkHeight * zz)));

    if (index >= 0 && index <= 7)
    {
        if (currentByte == 0)
        {
            DarrayOfDeVectorMapTempX = (DarrayOfDeVectorMapTempX* 10);
        }
        else
        {
            DarrayOfDeVectorMapTempX = (DarrayOfDeVectorMapTempX* 10) + 1;
        }
    }
    else if (index >= 8 && index <= 15)
    {
        if (currentByte == 0)
        {
            DarrayOfDeVectorMapTempTwoX = (DarrayOfDeVectorMapTempTwoX* 10);
        }
        else
        {
            DarrayOfDeVectorMapTempTwoX = (DarrayOfDeVectorMapTempTwoX* 10) + 1;
        }
    }
    else if (index >= 16 && index <= 23)
    {
        if (currentByte == 0)
        {
            DarrayOfDeVectorMapTempY = (DarrayOfDeVectorMapTempY* 10);
        }
        else
        {
            DarrayOfDeVectorMapTempY = (DarrayOfDeVectorMapTempY* 10) + 1;
        }
    }
    else if (index >= 24 && index <= 31)
    {
        if (currentByte == 0)
        {
            DarrayOfDeVectorMapTempTwoY = (DarrayOfDeVectorMapTempTwoY* 10);
        }
        else
        {
            DarrayOfDeVectorMapTempTwoY = (DarrayOfDeVectorMapTempTwoY* 10) + 1;
        }
    }

    else if (index >= 32 && index <= 39)
    {
        if (currentByte == 0)
        {
            DarrayOfDeVectorMapTempZ = (DarrayOfDeVectorMapTempZ* 10);
        }
        else
        {
            DarrayOfDeVectorMapTempZ = (DarrayOfDeVectorMapTempZ* 10) + 1;
        }
    }
    else if (index >= 40 && index <= 47)
    {

        if (currentByte == 0)
        {
            DarrayOfDeVectorMapTempTwoZ = (DarrayOfDeVectorMapTempTwoZ* 10);
        }
        else
        {
            DarrayOfDeVectorMapTempTwoZ = (DarrayOfDeVectorMapTempTwoZ* 10) + 1;
        }

    }
    else if (index >= 48 && index <= 55)
    {

        if (currentByte == 0)
        {
            DarrayOfDeVectorMapTempW = (DarrayOfDeVectorMapTempW* 10);
        }
        else
        {
            DarrayOfDeVectorMapTempW = (DarrayOfDeVectorMapTempW* 10) + 1;
        }

    }
    else if (index >= 56 && index <= 63)
    {
        if (currentByte == 0)
        {
            DarrayOfDeVectorMapTempTwoW = (DarrayOfDeVectorMapTempTwoW* 10);
        }
        else
        {
            DarrayOfDeVectorMapTempTwoW = (DarrayOfDeVectorMapTempTwoW* 10) + 1;
        }
    }

    zz++;
    if (zz >= SC_Globals.tinyChunkDepth)
    {
        yy++;
        zz = 0;
        switchYY = 1;
    }
    if (yy >= SC_Globals.tinyChunkHeight && switchYY == 1)
    {
        xx++;
        yy = 0;
        switchYY = 0;
        switchXX = 1;
    }
    if (xx >= SC_Globals.tinyChunkWidth && switchXX == 1)
    {
        xx = 0;
        switchXX = 0;
        break;
    }
}*/