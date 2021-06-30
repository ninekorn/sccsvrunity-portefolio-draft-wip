using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace SCCoreSystems
{
    [ExecuteInEditMode]
    //[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class polygon4vert : MonoBehaviour
    {

        public float vertBottomLeftX = -1;
        public float vertBottomLeftY = -1;
        public float vertTopLeftX = -1;
        public float vertTopLeftY = 1;
        public float vertBottomRightX = 1;
        public float vertBottomRightY = -1;
        public float vertTopRightX = 1;
        public float vertTopRightY = 1;

        public string matname = "material name";



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
            MeshFilter meshFilter = (MeshFilter)this.gameObject.AddComponent(typeof(MeshFilter));
            this.gameObject.AddComponent(typeof(MeshRenderer));

            /*
            if (this.gameObject.GetComponent<MeshRenderer>() == null)
            {
                this.gameObject.AddComponent<MeshRenderer>();
            }
            if (this.gameObject.GetComponent<MeshFilter>() == null)
            {
                this.gameObject.AddComponent<MeshFilter>();

            }*/




            GetComponent<MeshFilter>().mesh = mesh = new Mesh();
            mesh.name = "Procedural Grid";

            vertices = new Vector3[4];
            Vector2[] uv = new Vector2[vertices.Length];

            //vertices[0] = new Vector3(-1,0, 0);
            //vertices[1] = new Vector3(0,1, 0);
            //vertices[2] = new Vector3(1,0, 0);

            vertices[0] = new Vector3(((vertBottomLeftX) * 0.5f), ((vertBottomRightY) * 0.5f));
            vertices[1] = new Vector3(((vertTopRightX) * 0.5f), ((vertTopRightY) * 0.5f));
            vertices[2] = new Vector3(((vertBottomRightX) * 0.5f), ((vertBottomRightY) * 0.5f));
            vertices[3] = new Vector3(((vertTopRightX) * 0.5f), ((vertTopRightY) * 0.5f));



            /*vertices[0] = new Vector3((1 + vertTopLeftX) * sizeX, (1 + vertTopRightY) * sizeY);//, (-1 + vertoffsetz) * _sizeZ);
            vertices[1] = new Vector3((1 + vertBottomLeftX) * sizeX, (-1 + vertBottomRightY) * sizeY);//, (-1 + vertoffsetz) * _sizeZ);
            vertices[2] = new Vector3((-1 + vertBottomLeftX) * sizeX, (-1 + vertBottomRightY) * sizeY);//, (-1 + vertoffsetz) * _sizeZ);
            vertices[3] = new Vector3((-1 + vertTopLeftX) * sizeX, (1 + vertTopRightY) * sizeY);//, (-1 + vertoffsetz) * _sizeZ);
            vertices[4] = new Vector3((1 + vertTopLeftX) * sizeX, (1 + vertTopRightY) * sizeY);//, (-1 + vertoffsetz) * _sizeZ);
            vertices[5] = new Vector3((-1 + vertBottomLeftX) * sizeX, (-1 + vertBottomRightY) * sizeY);//, (-1 + vertoffsetz) * _sizeZ);
            */



            /*
            vertices[0] = new Vector2((-1 * 0.5f) - (vertBottomLeftX * 0.5f), (-1 * 0.5f) - (vertBottomLeftY * 0.5f)); // BOTTOMLEFT index 0
            vertices[1] = new Vector2((-1 * 0.5f) - (vertTopLeftX * 0.5f), (1 * 0.5f) + (vertTopLeftY * 0.5f)); // TOPLEFT index 1
            vertices[2] = new Vector2((1 * 0.5f) + (vertBottomRightX  * 0.5f), (-1 * 0.5f) - (vertBottomRightY  * 0.5f)); // BOTTOMRIGHT index 2
            vertices[3] = new Vector2((1 * 0.5f) + (vertTopRightX * 0.5f), (1 * 0.5f) + (vertTopRightY * 0.5f)); // TOPRIGHT index 3
            */




            //vertices[0] = sc_maths.RotatePoint(vertices[0], Vector2.zero, shippartrotation);
            //vertices[1] = sc_maths.RotatePoint(vertices[1], Vector2.zero, shippartrotation);
            //vertices[2] = sc_maths.RotatePoint(vertices[2], Vector2.zero, shippartrotation);
            //vertices[3] = sc_maths.RotatePoint(vertices[3], Vector2.zero, shippartrotation);


            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 0);
            uv[2] = new Vector2(0, 0);
            uv[3] = new Vector2(0, 0);

            /*for (int i = 0, y = 0; y <= ySize; y++)
            {
                for (int x = 0; x <= xSize; x++, i++)
                {
                    vertices[i] = new Vector3(x- (xSize * 0.5f), y- (ySize*0.5f), 0);
                    uv[i] = new Vector2((float)x / xSize, (float)y / ySize);		
                }
            }*/
            mesh.vertices = vertices;
            mesh.uv = uv;


            int[] triangles = new int[]
            {
                0,1,2,3,2,1,
                1,2,3,2,1,0
            };
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            var shippartmaterial = Resources.Load(matname, typeof(Material)) as Material;
            this.GetComponent<MeshRenderer>().material = shippartmaterial;





            string planeAssetName = this.gameObject.name + ".asset";
            Mesh m = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Editor/" + planeAssetName, typeof(Mesh));

            if (m == null)
            {
                m = new Mesh();
                m.name = this.gameObject.name;
                AssetDatabase.CreateAsset(m, "Assets/Editor/" + planeAssetName);
                AssetDatabase.SaveAssets();
            }

            meshFilter.sharedMesh = m;
            m.RecalculateBounds();

            //if (addCollider)
            //    this.gameObject.AddComponent(typeof(BoxCollider));

            Selection.activeObject = this.gameObject;






























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



            DestroyImmediate(this);



        }

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
    }
}