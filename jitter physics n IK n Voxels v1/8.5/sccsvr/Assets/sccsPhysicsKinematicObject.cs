using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsPhysicsKinematicObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    int OnTriggerEnterCounter = 0;
    int OnTriggerEnterCounterMax = 10;
    int OnTriggerEnterCounterSwtc = 0;

    int OnCollisionEnterCounter = 0;
    int OnCollisionEnterCounterMax = 10;
    int OnCollisionEnterCounterSwtc = 0;

    float kinematicObjectDeactivationDistance = 0.01f;



    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.transform.name);
        /*
        if (OnTriggerEnterCounter >= OnTriggerEnterCounterMax)
        {
            if (!this.transform.GetComponent<Rigidbody>().isKinematic)
            {
                if (collision.transform.GetComponent<Rigidbody>() != null)
                {
                    if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude < kinematicObjectDeactivationDistance)
                    {
                        if (!collision.transform.GetComponent<Rigidbody>().isKinematic)
                        {
                            collision.transform.GetComponent<Rigidbody>().isKinematic = true;
                        }

                        if (this.transform.GetComponent<Rigidbody>().velocity.magnitude < kinematicObjectDeactivationDistance)
                        {
                            this.transform.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }
                    else if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude >= kinematicObjectDeactivationDistance)
                    {
                        if (this.transform.GetComponent<Rigidbody>().isKinematic)
                        {
                            this.transform.GetComponent<Rigidbody>().isKinematic = false;
                            this.transform.GetComponent<Rigidbody>().AddForce(collision.transform.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
                        }
                    }
                }
                else
                {
                    if (this.transform.GetComponent<Rigidbody>().isKinematic)
                    {
                        this.transform.GetComponent<Rigidbody>().isKinematic = false;
                        this.transform.GetComponent<Rigidbody>().AddForce(collision.transform.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
                    }
                }
            }
            else if (this.transform.GetComponent<Rigidbody>().isKinematic)
            {
                if (collision.transform.GetComponent<Rigidbody>() != null)
                {
                    if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude < kinematicObjectDeactivationDistance)
                    {
                        if (!collision.transform.GetComponent<Rigidbody>().isKinematic)
                        {
                            collision.transform.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }
                    else if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude >= kinematicObjectDeactivationDistance)
                    {
                        if (this.transform.GetComponent<Rigidbody>().isKinematic)
                        {
                            this.transform.GetComponent<Rigidbody>().isKinematic = false;
                            this.transform.GetComponent<Rigidbody>().AddForce(collision.transform.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
                        }
                    }
                }
                else
                {
                    if (this.transform.GetComponent<Rigidbody>().isKinematic)
                    {
                        this.transform.GetComponent<Rigidbody>().isKinematic = false;

                        if (collision.transform.GetComponent<Rigidbody>() != null)
                        {
                            //Debug.Log(collision.transform.GetComponent<Rigidbody>().velocity.magnitude);
                            this.transform.GetComponent<Rigidbody>().AddForce(collision.transform.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
                        }
                        else
                        {
                            Debug.Log("velo" + collision.transform.GetComponent<sccsKinematicObjectMomentum>().veloFrameDiffMag);
                            this.transform.GetComponent<Rigidbody>().AddForce(collision.transform.GetComponent<sccsKinematicObjectMomentum>().Velocity*1000, ForceMode.Impulse);
                        }        
                    }
                }
            }

            OnTriggerEnterCounter = 0;
        }
        OnTriggerEnterCounter++;*/


    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        /*if (OnCollisionEnterCounter >= OnCollisionEnterCounterMax)
        {
            if (!this.transform.GetComponent<Rigidbody>().isKinematic)
            {
                if (collision.transform.GetComponent<Rigidbody>() != null)
                {
                    if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude < kinematicObjectDeactivationDistance)
                    {
                        if (!collision.transform.GetComponent<Rigidbody>().isKinematic)
                        {
                            collision.transform.GetComponent<Rigidbody>().isKinematic = true;
                        }

                        if (this.transform.GetComponent<Rigidbody>().velocity.magnitude < kinematicObjectDeactivationDistance)
                        {
                            this.transform.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }
                    else if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude >= kinematicObjectDeactivationDistance)
                    {
                        if (this.transform.GetComponent<Rigidbody>().isKinematic)
                        {
                            this.transform.GetComponent<Rigidbody>().isKinematic = false;
                            this.transform.GetComponent<Rigidbody>().AddForce(collision.transform.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
                        }
                    }
                }
                else
                {
                    if (this.transform.GetComponent<Rigidbody>().isKinematic)
                    {
                        this.transform.GetComponent<Rigidbody>().isKinematic = false;
                        this.transform.GetComponent<Rigidbody>().AddForce(collision.transform.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
                    }
                }
            }
            else if (this.transform.GetComponent<Rigidbody>().isKinematic)
            {
                if (collision.transform.GetComponent<Rigidbody>() != null)
                {
                    if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude < kinematicObjectDeactivationDistance)
                    {
                        if (!collision.transform.GetComponent<Rigidbody>().isKinematic)
                        {
                            collision.transform.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }
                    else if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude >= kinematicObjectDeactivationDistance)
                    {
                        if (this.transform.GetComponent<Rigidbody>().isKinematic)
                        {
                            this.transform.GetComponent<Rigidbody>().isKinematic = false;
                            this.transform.GetComponent<Rigidbody>().AddForce(collision.transform.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
                        }
                    }
                }
                else
                {
                    if (this.transform.GetComponent<Rigidbody>().isKinematic)
                    {
                        this.transform.GetComponent<Rigidbody>().isKinematic = false;

                        if (collision.transform.GetComponent<Rigidbody>() != null)
                        {
                            //Debug.Log(collision.transform.GetComponent<Rigidbody>().velocity.magnitude);
                            this.transform.GetComponent<Rigidbody>().AddForce(collision.transform.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
                        }
                        else
                        {
                            Debug.Log("velo" + collision.transform.GetComponent<sccsKinematicObjectMomentum>().veloFrameDiffMag);
                            this.transform.GetComponent<Rigidbody>().AddForce(collision.transform.GetComponent<sccsKinematicObjectMomentum>().Velocity * 1000, ForceMode.Impulse);
                        }
                    }
                }
            }

            OnCollisionEnterCounter = 0;
        }
        OnCollisionEnterCounter++;*/
    }
}
