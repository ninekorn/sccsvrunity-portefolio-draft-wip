using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class reparator : MonoBehaviour
{
    public static reparator currentRep;

    public List<GameObject> objToReact = new List<GameObject>();
    public int counter = 0;

    public List<MeshFilter> filterator = new List<MeshFilter>();

    /// Usually rendering with triangle strips is faster.
    /// However when combining objects with very low triangle counts, it can be faster to use triangles.
    /// Best is to try out which value is faster in practice.
    public bool generateTriangleStrips = true;

    /// This option has a far longer preprocessing time at startup but leads to better runtime performance.
    MeshFilter filtero;
    MeshFilter filter;
    MeshFilter[] meshing;

    void Start()
    {
        currentRep = this;

    }

    void Update()
    {
        //Debug.Log(objToReact.Count);

        if (counter == 1)
        {
            //foreach (GameObject obj in objToReact)
            //{
            filtero = (MeshFilter)GetComponent(typeof(MeshFilter));
            filtero.mesh.Clear();
            filtero.mesh = new Mesh();

            filter = (MeshFilter)GetComponent(typeof(MeshFilter));
            filter.mesh.Clear();
            filter.mesh = new Mesh();

            for (int g = 0; g < objToReact.Count; g++)
            {
                if (objToReact[g].gameObject.active == false)
                {
                    if (objToReact[g].transform.parent != this.transform)
                    {
                        objToReact[g].transform.parent = this.transform;
                    }
                    
                    //objToReact[g].gameObject.SetActive(true);

                    //Debug.Log("WTF");

                    MeshFilter filtering = objToReact[g].gameObject.GetComponent<MeshFilter>();

                    if (!filterator.Contains(filtering))
                    {
                        filterator.Add(filtering);
                    }
                    



                    //Debug.Log(filterator.Count);
                    for (int t = 0; t < filterator.Count; t++)
                    {
                        meshing = filterator.ToArray();
                    }
                    Component[] filters = meshing;


                    //Component[] filters = filterator.ToArray();



                    //Component[] filters = GetComponentsInChildren<MeshFilter>(true);

                    //Debug.Log(filters.Length);


                    Matrix4x4 myTransform = transform.worldToLocalMatrix;
                    Hashtable materialToMesh = new Hashtable();

                    for (int i = 0; i < filters.Length; i++)
                    {
                        filter = (MeshFilter)filters[i];
                        Renderer curRenderer = filters[i].GetComponent<Renderer>();
                        MeshCombineUtility.MeshInstance instance = new MeshCombineUtility.MeshInstance();
                        instance.mesh = filter.sharedMesh;
                        if (curRenderer != null && curRenderer.enabled && instance.mesh != null)
                        {
                            instance.transform = myTransform * filter.transform.localToWorldMatrix;

                            Material[] materials = curRenderer.sharedMaterials;
                            for (int m = 0; m < materials.Length; m++)
                            {
                                instance.subMeshIndex = System.Math.Min(m, instance.mesh.subMeshCount - 1);

                                ArrayList objects = (ArrayList)materialToMesh[materials[m]];
                                if (objects != null)
                                {
                                    objects.Add(instance);
                                }
                                else
                                {
                                    objects = new ArrayList();
                                    objects.Add(instance);
                                    materialToMesh.Add(materials[m], objects);
                                }
                            }

                            /*var fragTransform = filters[i].GetComponent<Transform>();

                            if (!fragTransform.gameObject.activeSelf)
                            {
                                fragTransform.gameObject.SetActive(true);
                            }*/
                            //curRenderer.enabled = true;

                        }
                    }

                    foreach (DictionaryEntry de in materialToMesh)
                    {
                        ArrayList elements = (ArrayList)de.Value;
                        MeshCombineUtility.MeshInstance[] instances = (MeshCombineUtility.MeshInstance[])elements.ToArray(typeof(MeshCombineUtility.MeshInstance));
                        // We have a maximum of one material, so just attach the mesh to our own game object
                        if (materialToMesh.Count == 1)
                        {
                            // Make sure we have a mesh filter & renderer
                            if (GetComponent(typeof(MeshFilter)) == null)
                                gameObject.AddComponent(typeof(MeshFilter));
                            if (!GetComponent("MeshRenderer"))
                                gameObject.AddComponent<MeshRenderer>();

                            
                            filtero.mesh = MeshCombineUtility.Combine(instances, generateTriangleStrips);
                            gameObject.GetComponent<MeshRenderer>().material = Resources.Load("Material/FracColor0") as Material;//(Material)de.Key;


                            //transform.gameObject.SetActive(true);
                            //gameObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                        // We have multiple materials to take care of, build one mesh / gameobject for each material
                        // and parent it to this object


                        else
                        {

                            //GameObject go = new GameObject("Combined mesh");
                            //transform.parent = transform;
                            //transform.localScale = Vector3.one;
                            //transform.localRotation = Quaternion.identity;
                            //transform.localPosition = Vector3.zero;
                            //gameObject.AddComponent(typeof(MeshFilter));
                            //gameObject.AddComponent<MeshRenderer>();
                            //GetComponent<Renderer>().material = (Material)de.Key;
                            //MeshFilter filter = (MeshFilter)GetComponent(typeof(MeshFilter));
                            // filter.mesh = MeshCombineUtility.Combine(instances, generateTriangleStrips);

                            //for (int j = 0; j < objToReact.Count;j++)
                            //{
                            /*GameObject go = new GameObject("Combined mesh");
                            go.transform.parent = transform;
                            go.transform.localScale = Vector3.one;
                            go.transform.localRotation = Quaternion.identity;
                            go.transform.localPosition = Vector3.zero;
                            go.AddComponent(typeof(MeshFilter));
                            go.AddComponent<MeshRenderer>();
                            go.GetComponent<Renderer>().material = (Material)de.Key;
                            MeshFilter filter = (MeshFilter)go.GetComponent(typeof(MeshFilter));
                            filter.mesh = MeshCombineUtility.Combine(instances, generateTriangleStrips);*/
                            //}



                        }

                        //}
                        //objToReact[g].gameObject.SetActive(false);
                        //objToReact[g].SetActive(true);
                        //objToReact[g].SetActive(false);

                    }
                }

                /*if (!transform.gameObject.activeSelf)
                {
                    transform.gameObject.SetActive(true);
                }*/

                counter = 0;
            }
        }
    }

}