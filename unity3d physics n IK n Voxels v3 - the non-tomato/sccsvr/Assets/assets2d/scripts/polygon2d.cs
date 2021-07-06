using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class polygon2d : MonoBehaviour
{

    public float xSizeL;
    public float xSizeR;
    public float ySizeL;
    public float ySizeR;

    //public Vector2 vert0 = new Vector3(-1, 0, 0);
    //public Vector2 vert1 = new Vector3(0, 1, 0);
    //public Vector2 vert2 = new Vector3(1, 0, 0);


    private Mesh mesh;
    public Vector3[] vertices;

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






















        /*GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";

		vertices = new Vector3[3];
		Vector2[] uv = new Vector2[vertices.Length];


        //vertices[0] = new Vector3(-1,0, 0);
        //vertices[1] = new Vector3(0,1, 0);
        //vertices[2] = new Vector3(1,0, 0);

        vertices[0] = new Vector3(-1 - (xSizeL * 0.5f), 0 - (ySize * 0.5f), 0);
        vertices[1] = new Vector3(1 - (xSizeL * 0.5f), 0 - (ySize * 0.5f), 0);
        vertices[2] = new Vector3(0 - (xSizeL * 0.5f), 1 - (ySize * 0.5f), 0);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 0);
        uv[2] = new Vector2(0, 0);

        /*for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x- (xSize * 0.5f), y- (ySize*0.5f), 0);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);		
			}
		}*/
        /*mesh.vertices = vertices;
		mesh.uv = uv;


        int[] triangles = new int[]
        {
            0,1,2
        };*/




        /*int[] triangles = new int[xSize * ySize * 6];
		for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
			for (int x = 0; x < xSize; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
				triangles[ti + 5] = vi + xSize + 2;
			}
		}*/







        /*
        mesh.triangles = triangles;
		mesh.RecalculateNormals();
        */






        //MeshSaverEditor.SaveMeshInPlace();
        //MeshSaverEditor.SaveMesh(mesh, "triangle2dmesh", true,true);


        //AssetDatabase.SaveAssets();











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