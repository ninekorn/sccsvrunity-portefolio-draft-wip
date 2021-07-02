using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SCCoreSystems
{
    [ExecuteInEditMode]
    //[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class sccstriequi : MonoBehaviour
    {
        int triangletype = 0;
        public int addCollider = 0;
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;



        public Transform medianPointTransform;
        /*public Transform vertex0;
        public Transform vertex1;
        public Transform vertex2;*/

        public float sizeOf = 0.05f;


        public float vert0X = 1;
        public float vert0Y = 1;
        public float vert1X = 1;
        public float vert1Y = 1;
        public float vert2X = 1;
        public float vert2Y = 1;

        //public Material shippartmaterial;



        //public Vector2 vert0 = new Vector3(-1, 0, 0);
        //public Vector2 vert1 = new Vector3(0, 1, 0);
        //public Vector2 vert2 = new Vector3(1, 0, 0);

        public float shippartrotation = 0;

        private Mesh mesh;
        private Vector3[] vertices;

        //public int width;
        //public int height;
        //int planeSize = 1;
        //int seed0 = 3420;

        private void Awake()
        {
            Generate();
        }

        private void Generate()
        {

            if ((MeshFilter)this.gameObject.GetComponent(typeof(MeshFilter)) == null)
            {
                meshFilter = (MeshFilter)this.gameObject.AddComponent(typeof(MeshFilter));
            }
            else
            {
                meshFilter = (MeshFilter)this.gameObject.GetComponent(typeof(MeshFilter));
            }


            if ((MeshRenderer)this.gameObject.GetComponent(typeof(MeshRenderer)) == null)
            {
                meshRenderer = (MeshRenderer)this.gameObject.AddComponent(typeof(MeshRenderer));
            }
            else
            {
                meshRenderer = (MeshRenderer)this.gameObject.GetComponent(typeof(MeshRenderer));
            }


            this.transform.name = "triequi";
            string planeAssetName = this.gameObject.name + ".asset";
            Mesh mesh = mesh = new Mesh();  //(Mesh)AssetDatabase.LoadAssetAtPath("Assets/Editor/" + planeAssetName, typeof(Mesh));


            mesh.name = this.gameObject.name;



            //GetComponent<MeshFilter>().mesh = mesh = new Mesh();
            //mesh.name = "Procedural Grid";

            vertices = new Vector3[3];
            Vector2[] uv = new Vector2[vertices.Length];

            float angle = 0.0f;
            float distance = 1.0f;

            Vector2 a = Vector2.zero;
            Vector2 b = Vector2.zero;
            Vector2 c = Vector2.zero;

            float x = 0;
            float y = 0;


            //EQUILATERAL FORMULA https://stackoverflow.com/questions/41294315/draw-an-equilateral-triangle-c-sharp
            //vertices[0] = new Vector3(-0.866f * sizeOf, -0.5f * sizeOf, 0);
            //vertices[1] = new Vector3(0.866f * sizeOf, -0.5f * sizeOf, 0);
            //vertices[2] = new Vector3(0, 1.0f * sizeOf, 0);


            //EQUILATERAL FORMULA https://stackoverflow.com/questions/11449856/draw-a-equilateral-triangle-given-the-center/11479662

            b.x = c.x * Mathf.Cos(sc_maths.DegreeToRadian(120)) - (c.y * Mathf.Sin(sc_maths.DegreeToRadian(120)));
            b.y = c.x * Mathf.Sin(sc_maths.DegreeToRadian(120)) + (c.y * Mathf.Cos(sc_maths.DegreeToRadian(120)));
            a.x = c.x * Mathf.Cos(sc_maths.DegreeToRadian(240)) - (c.y * Mathf.Sin(sc_maths.DegreeToRadian(240)));
            a.y = c.x * Mathf.Sin(sc_maths.DegreeToRadian(240)) + (c.y * Mathf.Cos(sc_maths.DegreeToRadian(240)));


            vertices[0] = a;
            vertices[1] = b;
            vertices[2] = c;



















            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 0);
            uv[2] = new Vector2(0, 0);

            mesh.vertices = vertices;
            mesh.uv = uv;


            int[] triangles = new int[]
            {
                0,1,2,2,1,0

            };
            mesh.triangles = triangles;
            mesh.RecalculateNormals();





            //var shippartmaterial = Resources.Load("fuselage", typeof(Material)) as Material;
            //this.GetComponent<MeshRenderer>().material = shippartmaterial;

            AssetDatabase.CreateAsset(mesh, "Assets/Editor/" + planeAssetName);
            AssetDatabase.SaveAssets();

            this.GetComponent<MeshFilter>().mesh = mesh;
            this.GetComponent<MeshFilter>().sharedMesh = mesh;
            this.GetComponent<MeshFilter>().mesh.RecalculateBounds();

            //meshFilter.sharedMesh = mesh;
            //mesh.RecalculateBounds();

            if (addCollider == 1)
                this.gameObject.AddComponent(typeof(BoxCollider));

            Selection.activeObject = this.gameObject;

        }

    }
}

//EQUILATERAL FORMULA https://stackoverflow.com/questions/41294315/draw-an-equilateral-triangle-c-sharp
/*vertices[0].x = x;
vertices[0].y = y;

vertices[1].x = (float)(((x + distance) * sizeOf) * Mathf.Cos(angle));
vertices[1].y = (float)(((y + distance) * sizeOf) * Mathf.Sin(angle));

vertices[2].x = (float)(((x + distance) * sizeOf) * Mathf.Cos(angle + Mathf.PI / 3));
vertices[2].y = (float)(((y + distance) * sizeOf) * Mathf.Sin(angle + Mathf.PI / 3));
var coordinatesOfD = new Vector2((vertices[2].x + vertices[1].x) * 0.5f, (vertices[2].y + vertices[1].y) * 0.5f);

var fakemedianPointToOffsetTrigFromVector2Zero = (coordinatesOfD) * 0.5f;

var tempVert0 = vertices[0];
var tempVert1 = vertices[1];
var tempVert2 = vertices[2];
tempVert2 *= 0.5f;
Instantiate(medianPointTransform, tempVert2, Quaternion.identity);*/


/* //https://stackoverflow.com/questions/11449856/draw-a-equilateral-triangle-given-the-center/11479662
            A: (-0.866, -0.5)
            B: (0.866, -0.5)
            C: (0.0, 1.0)
            */
//vertices[0] = new Vector3(-0.866f, -0.5f, 0);
//vertices[1] = new Vector3(0.866f, -0.5f, 0);
//vertices[2] = new Vector3(0, 1.0f, 0);

//https://stackoverflow.com/questions/11449856/draw-a-equilateral-triangle-given-the-center/11479662
/*Vector2 mA = new Vector2();
Vector2 mB = new Vector2();
Vector2 mC = new Vector2();
float mCos120 = (float)Mathf.Cos(sc_maths.DegreeToRadian(120));
float mSin120 = (float)Mathf.Sin(sc_maths.DegreeToRadian(120));
float mCos240 = (float)Mathf.Cos(sc_maths.DegreeToRadian(240));
float mSin240 = (float)Mathf.Sin(sc_maths.DegreeToRadian(240));

float r = 1; // this is distance from the center to one of triangle's point.
//mA.set(0 + r, 0);
mA.x = (float)0 + r;
mA.y = 0;
mB.x = mA.x * mCos120 - mA.y * mSin120;
mB.y = mA.x * mSin120 + mA.y * mCos120;
mC.x = mA.x * mCos240 - mA.y * mSin240;
mC.y = mA.x * mSin240 + mA.y * mCos240;

vertices[0] = mA;
vertices[1] = mB;
vertices[2] = mC;*/






























//vertices[0] = new Vector3(-1,0, 0);
//vertices[1] = new Vector3(0,1, 0);
//vertices[2] = new Vector3(1,0, 0);



/*
vertices[0].x -= vertices[1].x * 0.5f;
vertices[0].y -= vertices[1].y * 0.5f;

vertices[1].x = vertices[0].x + (vertices[1].x * 0.5f);
vertices[1].y = vertices[0].y + (vertices[1].y * 0.5f);

vertices[2].x -= vertices[1].x * 0.5f;
vertices[2].y -= vertices[1].y * 0.5f;
*/


/*tempVert0.x -= fakemedianPointToOffsetTrigFromVector2Zero.x;
tempVert0.y -= fakemedianPointToOffsetTrigFromVector2Zero.y;
tempVert1.x -= fakemedianPointToOffsetTrigFromVector2Zero.x;
tempVert1.y -= fakemedianPointToOffsetTrigFromVector2Zero.y;
tempVert2.x -= fakemedianPointToOffsetTrigFromVector2Zero.x;
tempVert2.y -= fakemedianPointToOffsetTrigFromVector2Zero.y;*/




//var dirFromVertBottomLToTop = tempVert2 - tempVert0;
//dirFromVertBottomLToTop *= 0.5f;

/*var medianPointmaybe = vertices[1] + dirFromVertBottomLToTop;
medianPointTransform = Instantiate(medianPointTransform, medianPointmaybe, Quaternion.identity);
medianPointTransform.position = medianPointmaybe;


medianPointmaybe = vertices[0] - dirFromVertBottomLToTop;
medianPointTransform = Instantiate(medianPointTransform, medianPointmaybe, Quaternion.identity);
medianPointTransform.position = medianPointmaybe;

medianPointmaybe = vertices[2] - dirFromVertBottomLToTop;
medianPointTransform = Instantiate(medianPointTransform, medianPointmaybe, Quaternion.identity);
medianPointTransform.position = medianPointmaybe;*/

/*vertices[0].x -= medianPoint.x;
vertices[0].y -= medianPoint.y;
vertices[1].x -= medianPoint.x;
vertices[1].y -= medianPoint.y;
vertices[2].x -= medianPoint.x;
vertices[2].y -= medianPoint.y;*/






























/*vertices[0].x -= medianPoint.x;
vertices[0].y -= medianPoint.y;

vertices[1].x -= medianPoint.x;
vertices[1].y -= medianPoint.y;

vertices[2].x -= medianPoint.x;
vertices[2].y -= medianPoint.y;*/















/*for (int i = 0; i < vertices.Length; i++)
{
    Instantiate(medianPointTransform, vertices[i], Quaternion.identity);
}*/





/*vertices[0] = new Vector3((-1 * sizeOf) - (vert0X * sizeOf), (0 * sizeOf) - (vert0Y * sizeOf), 0);
vertices[1] = new Vector3((1 * sizeOf) - (vert1X * sizeOf), (0 * sizeOf) - (vert1Y * sizeOf), 0);
vertices[2] = new Vector3((0 * sizeOf) - (vert2X * sizeOf), (1 * sizeOf) - (vert2Y * sizeOf), 0);
*/

//vertices[0] = sc_maths.RotatePoint(vertices[0], Vector2.zero, shippartrotation);
//vertices[1] = sc_maths.RotatePoint(vertices[1], Vector2.zero, shippartrotation);
//vertices[2] = sc_maths.RotatePoint(vertices[2], Vector2.zero, shippartrotation);





/*for (int i = 0, y = 0; y <= ySize; y++)
{
    for (int x = 0; x <= xSize; x++, i++)
    {
        vertices[i] = new Vector3(x- (xSize * 0.5f), y- (ySize*0.5f), 0);
        uv[i] = new Vector2((float)x / xSize, (float)y / ySize);		
    }
}*/

/*mesh.vertices = new Vector3[vertices.Length];
for (int i = 0; i < vertices.Length; y++)
{
    mesh.vertices[i] = new Vector3(vertices[i].x, vertices[i].y,0);
}*/
/*int[] triangles = new int[xSize * ySize * 6];
for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
    for (int x = 0; x < xSize; x++, ti += 6, vi++) {
        triangles[ti] = vi;
        triangles[ti + 3] = triangles[ti + 2] = vi + 1;
        triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
        triangles[ti + 5] = vi + xSize + 2;
    }
}*/












//MeshSaverEditor.SaveMeshInPlace();
//MeshSaverEditor.SaveMesh(mesh, "triangle2dmesh", true,true);


// AssetDatabase.SaveAssets();











/*string prefabPath = "Assets/assets2d";
Object prefab = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));

if (!prefab)
{
    prefab = EditorUtility.CreateEmptyPrefab(prefabPath);
}*/
/*Mesh mesh = (Mesh)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(Mesh));
if (!mesh)
{
    mesh = new Mesh();
    mesh.name = name;
    AssetDatabase.AddObjectToAsset(mesh, prefabPath);
}
else
{
    mesh.Clear();
}
// generate your mesh in place
BlaBlaBla(mesh);
// assume that MeshFilter is already there. could check and AddComponent
template.GetComponent & lt; MeshFilter & gt; ().sharedMesh = mesh;
// make sure 
EditorUtility.ReplacePrefab(template, prefab, ReplacePrefabOptions.ReplaceNameBased);
// get rid of the temporary object (otherwise it stays over in scene)
Object.DestroyImmediate(template);*/






/* void OnDrawGizmos()
 {

     if (mesh.vertices == null)
     {
         return;
     }

     Gizmos.color = Color.black;
     for (int i = 0; i < mesh.vertices.Length; i++)
     {
         Gizmos.DrawSphere(new Vector3(mesh.vertices[i].x + transform.position.x, mesh.vertices[i].y + transform.position.y, mesh.vertices[i].z + transform.position.z), 0.01f);
     }


 }*/

