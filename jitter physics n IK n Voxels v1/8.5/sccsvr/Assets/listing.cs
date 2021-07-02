using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class listing : MonoBehaviour
{

    public static listing currentList;

    public static List<GameObject> fragments = new List<GameObject>();

    List<Vector3> tempVerts = new List<Vector3>();

    public Transform player;
    GameObject frag;
    int counter = 0;
    Vector3 yo;

    void Start()
    {
        currentList = this;

    }


    void Update()
    {
        //Debug.Log(fragments.Count);

        for (int i = 0; i < fragments.Count; i++)
        {
            frag = fragments[i];

            if (Vector3.Distance(player.transform.position, frag.transform.position) <= 5f)
            {
                Debug.DrawRay(frag.transform.position, Vector3.up * 10, Color.green, 1000f);
              

                for (int j = 0; j < frag.GetComponent<MeshFilter>().mesh.vertices.Length; j++)
                {

                        //var thisMatrix = transform.localToWorldMatrix;

                        //Matrix4x4 myTransform = transform.localToWorldMatrix;

                    
                        //Vector3 vertex = frag.GetComponent<MeshFilter>().mesh.vertices[j];
                        //Vector3 yo = thisMatrix.MultiplyPoint3x4(vertex);
                    


                    if (frag.transform.parent.GetComponent<reassemble>().vertys.Contains(frag.GetComponent<MeshFilter>().mesh.vertices[j]))
                    {
                        Debug.Log("anus");
                        frag.transform.parent.GetComponent<reassemble>().vertys.Remove(frag.GetComponent<MeshFilter>().mesh.vertices[j]);
                    } 
                    
                    
                    
                    
                    
                                  
                }



                for (int j = 0; j < frag.GetComponent<MeshFilter>().mesh.triangles.Length; j++)
                {
                    if (frag.transform.parent.GetComponent<reassemble>().tris.Contains(frag.GetComponent<MeshFilter>().mesh.triangles[j]))
                    {
                        frag.transform.parent.GetComponent<reassemble>().tris.Remove(frag.GetComponent<MeshFilter>().mesh.triangles[j]);
                    }
                }

                for (int j = 0; j < frag.GetComponent<MeshFilter>().mesh.normals.Length; j++)
                {
                    if (frag.transform.parent.GetComponent<reassemble>().norms.Contains(frag.GetComponent<MeshFilter>().mesh.normals[j]))
                    {
                        frag.transform.parent.GetComponent<reassemble>().norms.Remove(frag.GetComponent<MeshFilter>().mesh.normals[j]);
                    }
                }


                if (frag.transform.parent.GetComponent<reassemble>().objToReact.Contains(frag.transform.gameObject))
                {
                    frag.transform.parent.GetComponent<reassemble>().objToReact.Remove(frag.transform.gameObject);
                }


                if (counter == 0)
                {
                    frag.transform.parent.GetComponent<reassemble>().counter = 2;
                    counter = 1;
                }

                frag.transform.gameObject.SetActive(true);
                

            }




            /*else if (Vector3.Distance(player.transform.position, frag.transform.position) > 5f)
            {

                for (int j = 0; j < frag.GetComponent<MeshFilter>().mesh.vertices.Length; j++)
                {
                    if (!frag.transform.parent.GetComponent<reassemble>().vertys.Contains(frag.GetComponent<MeshFilter>().mesh.vertices[j]))
                    {
                        frag.transform.parent.GetComponent<reassemble>().vertys.Add(frag.GetComponent<MeshFilter>().mesh.vertices[j]);
                    }
                }



                for (int j = 0; j < frag.GetComponent<MeshFilter>().mesh.triangles.Length; j++)
                {
                    if (!frag.transform.parent.GetComponent<reassemble>().tris.Contains(frag.GetComponent<MeshFilter>().mesh.triangles[j]))
                    {
                        frag.transform.parent.GetComponent<reassemble>().tris.Add(frag.GetComponent<MeshFilter>().mesh.triangles[j]);
                    }
                }

                for (int j = 0; j < frag.GetComponent<MeshFilter>().mesh.normals.Length; j++)
                {
                    if (!frag.transform.parent.GetComponent<reassemble>().norms.Contains(frag.GetComponent<MeshFilter>().mesh.normals[j]))
                    {
                        frag.transform.parent.GetComponent<reassemble>().norms.Add(frag.GetComponent<MeshFilter>().mesh.normals[j]);
                    }
                }


                if (!frag.transform.parent.GetComponent<reassemble>().objToReact.Contains(frag.transform.gameObject))
                {
                    frag.transform.parent.GetComponent<reassemble>().objToReact.Add(frag.transform.gameObject);
                }


                if (counter == 1)
                {
                    frag.transform.parent.GetComponent<reassemble>().counter = 1;
                    counter = 0;
                }

                frag.transform.gameObject.SetActive(false);
            }*/
            
        }
    }
}
