using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

//[CustomEditor(typeof(Decal))]
public class DecalEditor : MonoBehaviour {	

	private List<Material> materials;

	private Matrix4x4 oldMatrix;
	private Vector3 oldScale;
	private static bool showAffectedObject = false;
	private GameObject[] affectedObjects;
    public Decal decal;

    public LayerMask layerMask;

    private void Start()
    {
        Decal[] decals = (Decal[])GameObject.FindObjectsOfType(typeof(Decal));
        materials = new List<Material>();
        foreach (Decal decal in decals)
        {
            if (decal.material != null && !materials.Contains(decal.material))
            {
                materials.Add(decal.material);
            }
        }
        paintDecal(decal);


        paintDecal1(decal);
        

    }


    void paintDecal1(Decal target)
    {
        Decal decal = (Decal)target;

        /*Ray ray = new Ray(transform.position-new Vector3(0,0,transform.position.z*0.1f), transform.forward);

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 50))
        {
            Debug.Log("yo");
            decal.transform.position = hit.point;
            decal.transform.forward = -hit.normal;
            if (Vector3.Distance(transform.position,hit.point)>=0.1f)
            {
                Destroy(this);sssssw
            }
        }*/


        Vector3 scale = decal.transform.localScale;
        if (decal.sprite != null)
        {
            float ratio = (float)decal.sprite.rect.width / decal.sprite.rect.height;
            if (oldScale.x != scale.x)
            {
                scale.y = scale.x / ratio;
            }
            else
            if (oldScale.y != scale.y)
            {
                scale.x = scale.y * ratio;
            }
            else
            if (scale.x != scale.y * ratio)
            {
                scale.x = scale.y * ratio;
            }
            decal.transform.localScale = scale;
        }

        bool hasChanged = oldMatrix != decal.transform.localToWorldMatrix;
        oldMatrix = decal.transform.localToWorldMatrix;
        oldScale = decal.transform.localScale;


        if (hasChanged)
        {
            BuildDecal(decal);
        }
    }

	

    void paintDecal(Decal target)
    {
        Decal decal = (Decal)target;

        if (decal.material != null && !materials.Contains(decal.material))
        {
            materials.Add(decal.material);
        }
        if (decal.material != null && decal.material.mainTexture != null)
        {
            if (decal.sprite && decal.sprite.texture != decal.material.mainTexture) decal.sprite = null;
        }
        decal.maxAngle = Mathf.Clamp(decal.maxAngle, 1, 180);
        decal.pushDistance = Mathf.Clamp(decal.pushDistance, 0.01f, 1);
        BuildDecal(decal);
    }

	private void BuildDecal(Decal decal) {
		MeshFilter filter = decal.GetComponent<MeshFilter>();
		if(filter == null) filter = decal.gameObject.AddComponent<MeshFilter>();
		if(decal.GetComponent<Renderer>() == null) decal.gameObject.AddComponent<MeshRenderer>();
		decal.GetComponent<Renderer>().material = decal.material;

		if(decal.material == null || decal.sprite == null) {
			filter.mesh = null;
			return;
		}

        //Debug.Log(decal.affectedLayers);
		affectedObjects = GetAffectedObjects(decal.GetBounds(), decal.affectedLayers, decal);

		foreach(GameObject go in affectedObjects) {
			DecalBuilder.BuildDecalForObject( decal, go );
		}

		DecalBuilder.Push( decal.pushDistance );

		Mesh mesh = DecalBuilder.CreateMesh();
		if(mesh != null) {
			mesh.name = "DecalMesh";
			filter.mesh = mesh;
		}
	}

    private static GameObject[] GetAffectedObjects(Bounds bounds, LayerMask affectedLayers, Decal decal)
    {
        //MeshRenderer[] renderers = (MeshRenderer[])GameObject.FindObjectsOfType<MeshRenderer>();
        List<GameObject> objects = new List<GameObject>();

        //Debug.Log(decal.transform.parent.gameObject.layer);

        if (decal != null)
        {
            if (decal.transform != null)
            {
                if (decal.transform.parent != null)
                {
                    if (decal.transform.parent.gameObject != null)
                    {
                        if (decal.transform.parent.gameObject.layer == 11)
                        {
                            //Debug.Log("test0");
                            if (bounds.Intersects(decal.transform.parent.gameObject.GetComponent<MeshRenderer>().bounds))
                            {
                                //Debug.Log("test1");
                                objects.Add(decal.transform.parent.gameObject);
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("!decal.transform.parent.gameObject");
                    }
                }
                else
                {
                    Debug.Log("!decal.transform.parent");
                }
            }
            else
            {
                Debug.Log("!decal.transform");
            }
        }
        else
        {
            Debug.Log("!decal");
        }


        /*foreach (Renderer r in renderers)
        {
            if (r.gameObject.layer == affectedLayers)
            {
                Debug.Log(r.gameObject.name);
            }
        }*/

           /* foreach (Renderer r in renderers)
        {
            if (!r.enabled) continue;
        
            if (r.gameObject.layer == affectedLayers)
            {
                Debug.Log(r.gameObject.name);
                if (r.GetComponent<Decal>() != null) continue;

                if (bounds.Intersects(r.bounds))
                {
                    objects.Add(r.gameObject);
                }
            }          
        }*/
        return objects.ToArray();
    }



}