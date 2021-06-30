using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class sccslodchunkfinal : MonoBehaviour
{
   public sccsChunk thechunk;
    int counterCreateChunkObjectFaces = 0;

    int t = 0;
    int total = 0;
    int posx = 0;
    int posy = 0;
    int posz = 0;
    int xx = 0;
    int yy = 0;
    int zz = 0;
    int xi = 0;
    int yi = 0;
    int zi = 0;

    int swtchx = 0;
    int swtchy = 0;
    int swtchz = 0;

    public Vector3 chunkPos;

    //[HideInInspector]
    //public List<Vector3> vertexlist = new List<Vector3>();

    [HideInInspector]
    public int[] map;


    public int width = 20;
    public int height = 20;
    public int depth = 20;

    public Vector3 realIndexedPosition = Vector3.zero;

    public float planeSize = 0.1f;

    float nodeDiameter;
    float chunkRadius;
    float fraction;

    public float detailScale = 5;
    public float heightScale = 5;
    public int heightScale1 = 5;
    public int detailScale1 = 5;

    sccsproceduralplanetbuilderGen2 componentParent;
    Transform parentObject;

    NewObjectPoolerScript UnityTutorialGameObjectPool; //this.transform.GetComponent<NewObjectPoolerScript>();

    public int chunkbuildingswtc = 0;
    int _maxHeight = 0;

    public Vector3[] somevertexlist;
    public int[] sometrianglelist;

    private void Start()
    {
        //chunkPos = this.transform.position;

        //this.gameObject.tag = "collisionObject";
        //this.gameObject.layer = 8; //"collisionObject"
        //UnityTutorialGameObjectPool = this.transform.GetComponent<NewObjectPoolerScript>();

        //parentObject = this.transform.parent;
        //componentParent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilderGen2>().currentplanetbuilder;
    }


    public void sccslodchunkfinalcustomstart()
    {
        //chunkPos = this.transform.position;

        total = width * height * depth;

        t = 0;
        total = 0;
        posx = 0;
        posy = 0;
        posz = 0;
        xx = 0;
        yy = 0;
        zz = 0;
        xi = 0;
        yi = 0;
        zi = 0;

        swtchx = 0;
        swtchy = 0;
        swtchz = 0;
    }


    
    int radius = 5;

    Vector3 center;
    
    float speeding = 0.01f;
    int resetswtch = 0;

    private void Update()
    {

        if (chunkbuildingswtc == 1)
        {

            if (resetswtch == 0)
            {
                thechunk.sccsSetMap();
                thechunk.Regenerate();
                swtchz = 0;
                resetswtch = 1;
            }



            for (int i = 0; i < 100; i++) //total
            {
                //Debug.Log("faces generation");
                if (counterCreateChunkObjectFaces <= total)
                {
                    InvokeRepeating("CreateChunkFaces", 0, speeding);


                    //StartCoroutine("CreateChunkFacesCoroutine");
                    //StartCoroutine("CreateChunkFaces");
                    //buildTopRight(xi, yi, zi, planetchunkpos);
                }
                else
                {
                    CancelInvoke();
                    buildMesh();
                    resetswtch = 0;
                    //Debug.Log("ended faces generation");
                    t = 0;
                    total = 0;
                    posx = 0;
                    posy = 0;
                    posz = 0;
                    xx = 0;
                    yy = 0;
                    zz = 0;
                    xi = 0;
                    yi = 0;
                    zi = 0;

                    swtchx = 0;
                    swtchy = 0;
                    swtchz = 0;
                    chunkbuildingswtc = -1;
                    counterCreateChunkObjectFaces = 0;
                    break;
                }
            }
        }
    }

    public void buildMesh()
    {
        thechunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh.Clear();
        thechunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh.vertices = somevertexlist;
        thechunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh.triangles = sometrianglelist;
        thechunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        thechunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();
    }

    public float _iterateSpeed = 0.5f;

    int _maxWidth = 0;

    int _maxDepth = 0;

    int index = 0;


    public void CreateChunkFaces()
    {
        if (swtchz == 0)
        {
            if (t < total)
            {
                posx = (xx);
                posy = (yy);
                posz = (zz);

                xi = xx;
                yi = yy;
                zi = zz;

                if (xi < 0)
                {
                    xi *= -1;
                    xi = (width) + xi;
                }
                if (yi < 0)
                {
                    yi *= -1;
                    yi = (height) + yi;
                }
                if (zi < 0)
                {
                    zi *= -1;
                    zi = (depth) + zi;
                }

                //zi = (depth - 1) - zi;

                index = xi + (width) * (yi + (height) * zi);

                if (index < total)
                {
                    thechunk.CreateChunkFaces();
                }
                else
                {
                    //t = total;
                }

                zz++;
                if (zz >= (depth))
                {
                    xx++;
                    zz = 0;
                    swtchx = 1;
                }
                if (xx >= (width) && swtchx == 1)
                {
                    //faceStart = 0;
                    yy++;
                    xx = 0;
                    swtchx = 0;
                    swtchy = 1;
                }
                if (yy >= (height) && swtchy == 1)
                {
                    //yy = -ChunkHeight_L;
                    swtchy = 0;
                    swtchx = 0;
                    swtchz = 1;
                }
                t++;
                counterCreateChunkObjectFaces++;
            }

            //Debug.Log("total:" + total + "/t:" + t);
        }
    }
}




/*int _currentMaxIndex = _lastCorner3;
//UnityEngine.Debug.Log(1 ^ _currentMaxIndex);

int y = 0;
int total = 0;
int _threshold = _currentMaxIndex;

for (int x = 0; x<_currentMaxIndex; x++)
{
if (y > (_threshold - 1))
{
y = 0; //reset
total += x;
}
y += 1;
}
UnityEngine.Debug.Log(y);
UnityEngine.Debug.Log(_currentMaxIndex + "__currentMaxIndex ");
*/


/*var _numberOfVerticesThatAreSharedVertices = _faceCount * 2; // because 2 is the minimum for new faces. whatever = 18 + 4 + 4 so 
float _vertsPerFace = 4;
int x = 0;
int _numberOfFacesThatUseFourNewVertices = 0;
for (; x < _currentMaxIndex; x++)
{
_numberOfFacesThatUseFourNewVertices++;
if (_numberOfFacesThatUseFourNewVertices * _vertsPerFace + _numberOfVerticesThatAreSharedVertices - _currentMaxIndex == 0)
{
break;
}
else
{
_numberOfVerticesThatAreSharedVertices -= 2;
}
}
UnityEngine.Debug.Log(_numberOfFacesThatUseFourNewVertices);
UnityEngine.Debug.Log(_numberOfVerticesThatAreSharedVertices);
*/

