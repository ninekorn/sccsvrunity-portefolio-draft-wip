// Convert any GameObject into a single triangle

//https://docs.unity3d.com/ScriptReference/Mesh.Clear.html
using UnityEngine;

public class meshClear : MonoBehaviour
{
    private bool once = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            convertMesh();
            /*if (Time.time > 2.0f)
            {
               
            }*/
        }
    }

    void convertMesh()
    {
        if (once)
            return;

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        // Clears all the data that the mesh currently has
        mesh.Clear();

        // create 3 vertices for the triangle
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };

        once = true;
    }
}