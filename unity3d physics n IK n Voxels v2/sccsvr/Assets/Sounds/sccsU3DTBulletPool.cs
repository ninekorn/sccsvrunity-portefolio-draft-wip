using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsU3DTBulletPool : MonoBehaviour {

    public static new sccsU3DTBulletPool current;
    public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = true;
    List<GameObject> pooledObjects;

    public Transform gunEnd;

    void Awake()
    {
        current = this;

    }

	// Use this for initialization
	void Start () {
        gunEnd = this.transform;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
	
	// Update is called once per frame
	public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }


    // Update is called once per frame
    public GameObject RetakePooledObject(GameObject pooledobjecttoretake)
    {
        pooledobjecttoretake.SetActive(false);
        pooledObjects.Add(pooledobjecttoretake);

        return null;
    }
}
