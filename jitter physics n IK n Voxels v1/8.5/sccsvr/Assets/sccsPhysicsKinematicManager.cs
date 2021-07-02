using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsPhysicsKinematicManager : MonoBehaviour {


    public int initFrameCounter = 0;
    public int initFrameCounterMax = 50;
    public int initFrameCounterSwtc = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {

        //making sure that the scene is activated and physics of objects that are falling, are actually falling before manually putting them kinematic. but i will add a swtc later so that this scripts gets activated differently.
        if (initFrameCounter >= initFrameCounterMax)
        {
            Rigidbody[] rigidbodies = Rigidbody.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

            for (int rb = 0; rb < rigidbodies.Length; rb++)
            {
                if (rigidbodies[rb].velocity.magnitude < 0.1f)
                {
                    if (!rigidbodies[rb].isKinematic)
                    {
                        rigidbodies[rb].isKinematic = true;
                    }          
                }
            }



            if (gameObject.GetComponent<MeshCollider>() != null)
            {
                //gameObject.GetComponent<MeshCollider>().enabled = false;
                //Destroy(gameObject.GetComponent<MeshCollider>(),0.1f);
                gameObject.GetComponent<MeshCollider>().isTrigger = true;
            }
            if (gameObject.GetComponent<BoxCollider>() != null)
            {
                //Destroy(gameObject.GetComponent<BoxCollider>(),0.1f);
                //gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
            if (gameObject.GetComponent<SphereCollider>() != null)
            {
                //Destroy(gameObject.GetComponent<SphereCollider>(),0.1f);
                //gameObject.GetComponent<SphereCollider>().enabled = false;
                gameObject.GetComponent<SphereCollider>().isTrigger = true;
            }






            initFrameCounter = 0;
        }
        initFrameCounter++;
    }
}
