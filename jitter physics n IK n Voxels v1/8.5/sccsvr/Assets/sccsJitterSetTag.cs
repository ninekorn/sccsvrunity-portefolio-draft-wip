using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jitter;
using Rigidbody = Jitter.Dynamics.RigidBody;

public class sccsJitterSetTag : MonoBehaviour {

    public int tagSelection = 0;
    //0 == JPhysics.BodyTag.jitterCollisionObject
    //1 == JPhysics.BodyTag.humanrig


    private void Start()
    {
        /*if (tagSelection == 0)
        {
            if (this.transform.GetComponent<JRigidBody>() != null)
            {
                this.transform.GetComponent<JRigidBody>().bodytag = JPhysics.BodyTag.jitterCollisionObject;

                object arrayOfTag = new object();
                arrayOfTag = JPhysics.BodyTag.jitterCollisionObject;
                this.transform.GetComponent<JRigidBody>().Body.Tag = arrayOfTag;
            }
            //this.transform.GetComponent<Rigidbody>().Tag = JPhysics.BodyTag.jitterCollisionObject;
        }
        else if (tagSelection == 1)
        {
            if (this.transform.GetComponent<JRigidBody>() != null)
            {
                this.transform.GetComponent<JRigidBody>().bodytag = JPhysics.BodyTag.humanrig;

                object arrayOfTag = new object();
                arrayOfTag = JPhysics.BodyTag.humanrig;
                this.transform.GetComponent<JRigidBody>().Body.Tag = arrayOfTag;

            }
            //this.transform.GetComponent<Rigidbody>().Tag = JPhysics.BodyTag.humanrig;
        }*/




    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
