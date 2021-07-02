using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rigidbody = Jitter.Dynamics.RigidBody;
using Jitter;
using Jitter.LinearMath;


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
        
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            //obj.GetComponent<Rigidbody>().Tag = Jitter.Collision.bod;
            //
   

            if (obj.transform.GetComponent<JRigidBody>()!= null)
            {
                obj.transform.GetComponent<JRigidBody>().bodytag = JPhysics.BodyTag.jitterCollisionObject;
            }


            //var tag = obj.GetComponent<Rigidbody>().Tag;

            //(SCCoreSystems.sc_console.SC_console_directx.BodyTag)body.Tag

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
            obj.transform.GetComponent<JRigidBody>().bodytag = JPhysics.BodyTag.jitterCollisionObject;

            obj.SetActive(false);

            pooledObjects.Add(obj);     
            return obj;
        }
        return null;
    }
}
