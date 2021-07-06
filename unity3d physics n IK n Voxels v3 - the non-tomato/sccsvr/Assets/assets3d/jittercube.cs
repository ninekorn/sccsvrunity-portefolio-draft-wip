using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace SCCoreSystems
{


    [ExecuteInEditMode]
    public class jittercube : MonoBehaviour //ScriptableWizard
    {

        public float _sizeX = 0.0f;
        public float _sizeY = 0.0f;
        public float _sizeZ = 0.0f;
        public float vertoffsetx = 0.0f;
        public float vertoffsety = 0.0f;
        public float vertoffsetz = 0.0f;




        public struct DVertex
        {
            public Vector3 position;
            public Vector2 texture;
            public Vector2 uv;
            public Vector3 normal;

        }

        public void Awake()
        {
            //GameObject plane = new GameObject();


            MeshFilter meshFilter = (MeshFilter)this.gameObject.AddComponent(typeof(MeshFilter));
            this.gameObject.AddComponent(typeof(MeshRenderer));

            string planeAssetName = "jittercube" + "x" + _sizeX + "y" + _sizeY + "z"+ _sizeZ + ".asset"; //+ anchorId
            Mesh m = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Editor/" + planeAssetName, typeof(Mesh));

            if (m == null)
            {
                m = new Mesh();
                m.name = this.gameObject.name;

                List<Vector3> list_verts = new List<Vector3>();
                List<Vector3> list_normals = new List<Vector3>();
                List<Vector2> list_uvs = new List<Vector2>();
                List<int> list_triangles = new List<int>();

                // Start with a single vertex at the bottom of the sphere.



                DVertex[] Vertices = new[]
                    {                                   
                    //TOP
                    new DVertex()
                    {
                        position = new Vector3((1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                        texture = new Vector2(0, 0),
                        //color = _//color,
                        normal = new Vector3(0, 1, 1),
                    },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY,  (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 1),

                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY,  (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3( (-1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ),
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 1),
                     },







                     //BOTTOM
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         normal = new Vector3(1, 0, 1),
                         //color = _//color,
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 0, 1),

                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 0, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 0, 1),

                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 0, 1),

                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 0, 1),

                     },

                    //FACE NEAR
                    new DVertex() //12
                    {
                        position = new Vector3((1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                        texture = new Vector2(1, 1),
                        //color = _//color,
                        normal = new Vector3(1, 0, 0),
                    },
                     new DVertex() //13
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(1, 0),
                         //color = _//color,
                         normal = new Vector3(1, 0, 0),
                     },
                     new DVertex() //14
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 0, 0),
                     },
                     new DVertex() //15
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 1),
                         //color = _//color,
                         normal = new Vector3(1, 0, 0),
                     },
                     new DVertex() //16
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(1, 1),
                         //color = _//color,
                         normal = new Vector3(1, 0, 0),
                     },
                     new DVertex() //17
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX,(-1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 0, 0),
                     },



                     //FACE FAR
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 0),
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 0),
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY,(1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 0),
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ),
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 0),
                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 0),
                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ),
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 1, 0),
                     },






                     //FACE LEFT
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ),
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 0, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 0, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ),
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 0, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 0, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY,(1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 0, 1),
                     },
                     new DVertex()
                     {
                         position = new Vector3((-1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(0, 0, 1),
                     },




                     //FACE RIGHT
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 1, 0),
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 1, 0),
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 1, 0),
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 1, 0),
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 1, 0),
                     },
                     new DVertex()
                     {
                         position = new Vector3((1+vertoffsetx)*_sizeX, (-1+vertoffsety)*_sizeY, (-1+vertoffsetz)*_sizeZ) ,
                         texture = new Vector2(0, 0),
                         //color = _//color,
                         normal = new Vector3(1, 1, 0),
                     },
                 };


                int[] triangles = new int[]
                {
                    5,4,3,2,1,0,
                    11,10,9,8,7,6,
                    17,16,15,14,13,12,
                    23,22,21,20,19,18,
                    29,28,27,26,25,24,
                    35,34,33,32,31,30,
                 };




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

                for (int i = 0; i < Vertices.Length; i++)
                {
                    list_verts[i] = Vertices[i].position;
                    list_uvs[i] = Vertices[i].texture;
                    list_normals[i] = Vertices[i].normal;
                }


                m.vertices = list_verts.ToArray();
                m.uv = list_uvs.ToArray();
                m.triangles = triangles;
                m.normals = list_normals.ToArray();
                m.RecalculateNormals();

                AssetDatabase.CreateAsset(m, "Assets/Editor/" + planeAssetName);
                AssetDatabase.SaveAssets();
            }

            meshFilter.sharedMesh = m;
            m.RecalculateBounds();

            //if (addCollider)
            //    plane.AddComponent(typeof(BoxCollider));

            Selection.activeObject = this.gameObject;
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