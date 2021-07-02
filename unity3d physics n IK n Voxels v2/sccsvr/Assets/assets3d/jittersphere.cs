using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace SCCoreSystems
{


    [ExecuteInEditMode]
    public class jittersphere : MonoBehaviour //ScriptableWizard
    {
        float PIovertwo = 1.57079632679f;
        float TWOPI = 6.28318530718f;

        public bool addCollider = false;
        public bool createAtOrigin = true;
        public string optionalName;

        static Camera cam;
        static Camera lastUsedCam;



        public void Awake()
        {
            /*widthSegments = Mathf.Clamp(widthSegments, 1, 254);
            lengthSegments = Mathf.Clamp(lengthSegments, 1, 254);
            GameObject plane = new GameObject();

            if (!string.IsNullOrEmpty(optionalName))
                plane.name = optionalName;
            else
                plane.name = "Plane";

            if (!createAtOrigin && cam)
                plane.transform.position = cam.transform.position + cam.transform.forward * 5.0f;
            else
                plane.transform.position = Vector3.zero;

            Vector2 anchorOffset;
            string anchorId;
            switch (anchor)
            {
                case AnchorPoint.TopLeft:
                    anchorOffset = new Vector2(-width / 2.0f, length / 2.0f);
                    anchorId = "TL";
                    break;
                case AnchorPoint.TopHalf:
                    anchorOffset = new Vector2(0.0f, length / 2.0f);
                    anchorId = "TH";
                    break;
                case AnchorPoint.TopRight:
                    anchorOffset = new Vector2(width / 2.0f, length / 2.0f);
                    anchorId = "TR";
                    break;
                case AnchorPoint.RightHalf:
                    anchorOffset = new Vector2(width / 2.0f, 0.0f);
                    anchorId = "RH";
                    break;
                case AnchorPoint.BottomRight:
                    anchorOffset = new Vector2(width / 2.0f, -length / 2.0f);
                    anchorId = "BR";
                    break;
                case AnchorPoint.BottomHalf:
                    anchorOffset = new Vector2(0.0f, -length / 2.0f);
                    anchorId = "BH";
                    break;
                case AnchorPoint.BottomLeft:
                    anchorOffset = new Vector2(-width / 2.0f, -length / 2.0f);
                    anchorId = "BL";
                    break;
                case AnchorPoint.LeftHalf:
                    anchorOffset = new Vector2(-width / 2.0f, 0.0f);
                    anchorId = "LH";
                    break;
                case AnchorPoint.Center:
                default:
                    anchorOffset = Vector2.zero;
                    anchorId = "C";
                    break;
            }*/



            GameObject plane = new GameObject();


            MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
            plane.AddComponent(typeof(MeshRenderer));

            string planeAssetName = plane.name + ".asset"; //+ anchorId
            Mesh m = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Editor/" + planeAssetName, typeof(Mesh));

            if (m == null)
            {
                m = new Mesh();
                m.name = plane.name;


                int tessellation = 3;
                float diameter = 1.0f;
                float tileSize = 0.01f;


                int verticalSegments = tessellation;
                int horizontalSegments = tessellation * 2;
                diameter = tileSize * 0.5f;
                float radius = 0.0f;


                radius = diameter / 2;
                List<Vector3> list_verts = new List<Vector3>();
                List<Vector3> list_normals = new List<Vector3>();
                List<Vector2> list_uvs = new List<Vector2>();
                List<int> list_triangles = new List<int>();

                // Start with a single vertex at the bottom of the sphere.

                /*DVertex vertex = new DVertex()
                {
                    position = Vector3.down * radius,
                    texture = new Vector2(0, 1),
                    color = _color,
                    normal = Vector3.down,
                };*/
                list_verts.Add(Vector3.down * radius);
                list_normals.Add(Vector3.down);
                list_uvs.Add(new Vector2(0, 1));




                // Create rings of vertices at progressively higher latitudes.
                for (int i = 0; i < verticalSegments - 1; i++)
                {
                    float latitude = (float)(((i + 1) * Mathf.PI / verticalSegments) - PIovertwo);

                    float dy = (float)Mathf.Sin(latitude);
                    float dxz = (float)Mathf.Cos(latitude);

                    // Create a single ring of vertices at this latitude.
                    for (int j = 0; j < horizontalSegments; j++)
                    {
                        float longitude = j * TWOPI / horizontalSegments;

                        float dx = (float)Mathf.Cos(longitude) * dxz;
                        float dz = (float)Mathf.Sin(longitude) * dxz;

                        Vector3 normal = new Vector3(dx, dy, dz);

                        /*vertex = new DVertex()
                        {
                            position = normal * radius,
                            texture = new Vector2(0, 1),
                            color = _color,
                            normal = normal,
                        };*/
                        list_verts.Add(normal * radius);
                        list_normals.Add(normal);
                        list_uvs.Add(new Vector2(0, 1));
                    }
                }





                // Finish with a single vertex at the top of the sphere.
                /*vertex = new DVertex()
                {
                    position = Vector3.Up * radius,
                    texture = new Vector2(0, 1),
                    color = _color,
                    normal = Vector3.Up,
                };*/
                list_verts.Add(Vector3.up * radius);
                list_normals.Add(Vector3.up);
                list_uvs.Add(new Vector2(0, 1));


                // Create a fan connecting the bottom vertex to the bottom latitude ring.
                for (int i = 0; i < horizontalSegments; i++)
                {
                    list_triangles.Add(0);
                    list_triangles.Add(1 + (i + 1) % horizontalSegments);
                    list_triangles.Add(1 + i);
                }

                // Fill the sphere body with triangles joining each pair of latitude rings.
                for (int i = 0; i < verticalSegments - 2; i++)
                {
                    for (int j = 0; j < horizontalSegments; j++)
                    {
                        int nextI = i + 1;
                        int nextJ = (j + 1) % horizontalSegments;

                        list_triangles.Add(1 + i * horizontalSegments + j);
                        list_triangles.Add(1 + i * horizontalSegments + nextJ);
                        list_triangles.Add(1 + nextI * horizontalSegments + j);

                        list_triangles.Add(1 + i * horizontalSegments + nextJ);
                        list_triangles.Add(1 + nextI * horizontalSegments + nextJ);
                        list_triangles.Add(1 + nextI * horizontalSegments + j);
                    }
                }

                // Create a fan connecting the top vertex to the top latitude ring.
                for (int i = 0; i < horizontalSegments; i++)
                {
                    list_triangles.Add(list_verts.Count - 1);
                    list_triangles.Add(list_verts.Count - 2 - (i + 1) % horizontalSegments);
                    list_triangles.Add(list_verts.Count - 2 - i);
                }












                /*int hCount2 = widthSegments + 1;
                int vCount2 = lengthSegments + 1;
                int numTriangles = widthSegments * lengthSegments * 6;
                if (twoSided)
                {
                    numTriangles *= 2;
                }
                int numVertices = hCount2 * vCount2;

                Vector3[] vertices = new Vector3[numVertices];
                Vector2[] uvs = new Vector2[numVertices];
                int[] triangles = new int[numTriangles];
                Vector4[] tangents = new Vector4[numVertices];
                Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

                int index = 0;
                float uvFactorX = 1.0f / widthSegments;
                float uvFactorY = 1.0f / lengthSegments;
                float scaleX = width / widthSegments;
                float scaleY = length / lengthSegments;
                for (float y = 0.0f; y < vCount2; y++)
                {
                    for (float x = 0.0f; x < hCount2; x++)
                    {
                        if (orientation == Orientation.Horizontal)
                        {
                            vertices[index] = new Vector3(x * scaleX - width / 2f - anchorOffset.x, 0.0f, y * scaleY - length / 2f - anchorOffset.y);
                        }
                        else
                        {
                            vertices[index] = new Vector3(x * scaleX - width / 2f - anchorOffset.x, y * scaleY - length / 2f - anchorOffset.y, 0.0f);
                        }
                        tangents[index] = tangent;
                        uvs[index++] = new Vector2(x * uvFactorX, y * uvFactorY);
                    }
                }

                index = 0;
                for (int y = 0; y < lengthSegments; y++)
                {
                    for (int x = 0; x < widthSegments; x++)
                    {
                        triangles[index] = (y * hCount2) + x;
                        triangles[index + 1] = ((y + 1) * hCount2) + x;
                        triangles[index + 2] = (y * hCount2) + x + 1;

                        triangles[index + 3] = ((y + 1) * hCount2) + x;
                        triangles[index + 4] = ((y + 1) * hCount2) + x + 1;
                        triangles[index + 5] = (y * hCount2) + x + 1;
                        index += 6;
                    }
                    if (twoSided)
                    {
                        // Same tri vertices with order reversed, so normals point in the opposite direction
                        for (int x = 0; x < widthSegments; x++)
                        {
                            triangles[index] = (y * hCount2) + x;
                            triangles[index + 1] = (y * hCount2) + x + 1;
                            triangles[index + 2] = ((y + 1) * hCount2) + x;

                            triangles[index + 3] = ((y + 1) * hCount2) + x;
                            triangles[index + 4] = (y * hCount2) + x + 1;
                            triangles[index + 5] = ((y + 1) * hCount2) + x + 1;
                            index += 6;
                        }
                    }
                }*/

                //Vertices = list_verts.ToArray();
                //triangles = list_triangles.ToArray();


                m.vertices = list_verts.ToArray();
                m.uv = list_uvs.ToArray();
                m.triangles = list_triangles.ToArray();
                m.normals = list_normals.ToArray();
                m.RecalculateNormals();

                AssetDatabase.CreateAsset(m, "Assets/Editor/" + planeAssetName);
                AssetDatabase.SaveAssets();
            }

            meshFilter.sharedMesh = m;
            m.RecalculateBounds();

            if (addCollider)
                plane.AddComponent(typeof(BoxCollider));

            Selection.activeObject = plane;
        }


















        /*
        [MenuItem("GameObject/Create Other/Custom Plane...")]
        static void CreateWizard()
        {
            cam = Camera.current;
            // Hack because camera.current doesn't return editor camera if scene view doesn't have focus
            if (!cam)
                cam = lastUsedCam;
            else
                lastUsedCam = cam;
            ScriptableWizard.DisplayWizard("Create Plane", typeof(CreatePlane));
        }


        void OnWizardUpdate()
        {
            widthSegments = Mathf.Clamp(widthSegments, 1, 254);
            lengthSegments = Mathf.Clamp(lengthSegments, 1, 254);
        }


        void OnWizardCreate()
        {   
            GameObject plane = new GameObject();

            if (!string.IsNullOrEmpty(optionalName))
                plane.name = optionalName;
            else
                plane.name = "Plane";

            if (!createAtOrigin && cam)
                plane.transform.position = cam.transform.position + cam.transform.forward * 5.0f;
            else
                plane.transform.position = Vector3.zero;

            Vector2 anchorOffset;
            string anchorId;
            switch (anchor)
            {
                case AnchorPoint.TopLeft:
                    anchorOffset = new Vector2(-width / 2.0f, length / 2.0f);
                    anchorId = "TL";
                    break;
                case AnchorPoint.TopHalf:
                    anchorOffset = new Vector2(0.0f, length / 2.0f);
                    anchorId = "TH";
                    break;
                case AnchorPoint.TopRight:
                    anchorOffset = new Vector2(width / 2.0f, length / 2.0f);
                    anchorId = "TR";
                    break;
                case AnchorPoint.RightHalf:
                    anchorOffset = new Vector2(width / 2.0f, 0.0f);
                    anchorId = "RH";
                    break;
                case AnchorPoint.BottomRight:
                    anchorOffset = new Vector2(width / 2.0f, -length / 2.0f);
                    anchorId = "BR";
                    break;
                case AnchorPoint.BottomHalf:
                    anchorOffset = new Vector2(0.0f, -length / 2.0f);
                    anchorId = "BH";
                    break;
                case AnchorPoint.BottomLeft:
                    anchorOffset = new Vector2(-width / 2.0f, -length / 2.0f);
                    anchorId = "BL";
                    break;
                case AnchorPoint.LeftHalf:
                    anchorOffset = new Vector2(-width / 2.0f, 0.0f);
                    anchorId = "LH";
                    break;
                case AnchorPoint.Center:
                default:
                    anchorOffset = Vector2.zero;
                    anchorId = "C";
                    break;
            }

            MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
            plane.AddComponent(typeof(MeshRenderer));

            string planeAssetName = plane.name + widthSegments + "x" + lengthSegments + "W" + width + "L" + length + (orientation == Orientation.Horizontal ? "H" : "V") + anchorId + ".asset";
            Mesh m = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Editor/" + planeAssetName, typeof(Mesh));

            if (m == null)
            {
                m = new Mesh();
                m.name = plane.name;

                int hCount2 = widthSegments + 1;
                int vCount2 = lengthSegments + 1;
                int numTriangles = widthSegments * lengthSegments * 6;
                if (twoSided)
                {
                    numTriangles *= 2;
                }
                int numVertices = hCount2 * vCount2;

                Vector3[] vertices = new Vector3[numVertices];
                Vector2[] uvs = new Vector2[numVertices];
                int[] triangles = new int[numTriangles];
                Vector4[] tangents = new Vector4[numVertices];
                Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

                int index = 0;
                float uvFactorX = 1.0f / widthSegments;
                float uvFactorY = 1.0f / lengthSegments;
                float scaleX = width / widthSegments;
                float scaleY = length / lengthSegments;
                for (float y = 0.0f; y < vCount2; y++)
                {
                    for (float x = 0.0f; x < hCount2; x++)
                    {
                        if (orientation == Orientation.Horizontal)
                        {
                            vertices[index] = new Vector3(x * scaleX - width / 2f - anchorOffset.x, 0.0f, y * scaleY - length / 2f - anchorOffset.y);
                        }
                        else
                        {
                            vertices[index] = new Vector3(x * scaleX - width / 2f - anchorOffset.x, y * scaleY - length / 2f - anchorOffset.y, 0.0f);
                        }
                        tangents[index] = tangent;
                        uvs[index++] = new Vector2(x * uvFactorX, y * uvFactorY);
                    }
                }

                index = 0;
                for (int y = 0; y < lengthSegments; y++)
                {
                    for (int x = 0; x < widthSegments; x++)
                    {
                        triangles[index] = (y * hCount2) + x;
                        triangles[index + 1] = ((y + 1) * hCount2) + x;
                        triangles[index + 2] = (y * hCount2) + x + 1;

                        triangles[index + 3] = ((y + 1) * hCount2) + x;
                        triangles[index + 4] = ((y + 1) * hCount2) + x + 1;
                        triangles[index + 5] = (y * hCount2) + x + 1;
                        index += 6;
                    }
                    if (twoSided)
                    {
                        // Same tri vertices with order reversed, so normals point in the opposite direction
                        for (int x = 0; x < widthSegments; x++)
                        {
                            triangles[index] = (y * hCount2) + x;
                            triangles[index + 1] = (y * hCount2) + x + 1;
                            triangles[index + 2] = ((y + 1) * hCount2) + x;

                            triangles[index + 3] = ((y + 1) * hCount2) + x;
                            triangles[index + 4] = (y * hCount2) + x + 1;
                            triangles[index + 5] = ((y + 1) * hCount2) + x + 1;
                            index += 6;
                        }
                    }
                }

                m.vertices = vertices;
                m.uv = uvs;
                m.triangles = triangles;
                m.tangents = tangents;
                m.RecalculateNormals();

                AssetDatabase.CreateAsset(m, "Assets/Editor/" + planeAssetName);
                AssetDatabase.SaveAssets();
            }

            meshFilter.sharedMesh = m;
            m.RecalculateBounds();

            if (addCollider)
                plane.AddComponent(typeof(BoxCollider));

            Selection.activeObject = plane;
        }*/
    }
}