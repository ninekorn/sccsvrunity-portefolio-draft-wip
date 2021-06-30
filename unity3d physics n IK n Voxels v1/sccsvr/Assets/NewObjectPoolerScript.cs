using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectPoolerScript : MonoBehaviour {

    public static new NewObjectPoolerScript current;
    public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = true;
    List<GameObject> pooledObjects;

    void Awake()
    {
        current = this;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

	// Use this for initialization
	void Start () {
       
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
}
