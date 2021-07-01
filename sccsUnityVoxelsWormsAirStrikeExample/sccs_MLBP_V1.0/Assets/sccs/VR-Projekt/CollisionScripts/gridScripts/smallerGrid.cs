using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class smallerGrid : MonoBehaviour {

	public int xSize, ySize;

	private Mesh mesh;
	private Vector3[] vertices;

    float planeSize = 0.1f;
    //public int width;
    //public int height;
    //int planeSize = 1;
    //int seed0 = 3420;

    private void Awake () {
		Generate();
	}

	private void Generate () {

		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";

		vertices = new Vector3[(xSize + 1) * (ySize + 1)];
		Vector2[] uv = new Vector2[vertices.Length];

		for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {

                //float noiseY = Mathf.Abs((float)(y * planeSize + transform.position.y + seed0) / 100);
                //float noiseX = Mathf.Abs((float)(x * planeSize + transform.position.x + seed0) / 100);


                //float noiseValue = Noise.Generate(noiseX, noiseY);

                /*noiseValue += (10 - (float)y) / 10;
                noiseValue /= (float)y / 5;*/

                vertices[i] = new Vector3(x* planeSize, 1, y* planeSize);
                uv[i] = new Vector2((float)x* planeSize / xSize, (float)y* planeSize / ySize);		
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;


		int[] triangles = new int[xSize * ySize * 6];
		for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
			for (int x = 0; x < xSize; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
				triangles[ti + 5] = vi + xSize + 2;
			}
		}
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
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