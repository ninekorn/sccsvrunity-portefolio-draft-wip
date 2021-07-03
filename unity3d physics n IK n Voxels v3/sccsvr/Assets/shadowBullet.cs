using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace SCCoreSystems
{
    //i called it shadowbullet but you can name it what you want like forwardshadow, forwardGhost, forwardprojection of the rigidbody etc.. it's your choice really. ForwardVoidofDeath.
    public class shadowBullet : MonoBehaviour
    {
        float raylength = 0;
        public AudioSource projectileimpact;

        bulletcollision bulletColScript;

        public Vector3 beforestrayvelo = Vector3.zero;
        public int letstraybulletgo = 0;
        public int straybullet = 0;

        public Vector3 bullseyedirection = Vector3.zero;
        public float projectileimpulse = 10.5f;
        public GameObject firing_ship;
        public GameObject hit_effect;

        Rigidbody myRigidbody;
        Vector3 previousPos;

        Vector3 movementThisStep;

        public LayerMask layerMask = -1;

        Vector3[] vertices;

        //public GameObject collPoint;
        Vector3 velocity;

        bool getVelocity = false;
        Vector3 worldPt;

        bool checkDistanceToCollision = false;
        float currentSpeed = 0;

        Vector3[] rayhits;
        Vector3[] rayhitsNorms;
        Vector3[] listOfVertices;
        float[] vertDistToCol;
        Transform[] objectsCollidingWith;
        int[] triangleIndexHit;

        int pos = 0;

        private Collider myCollider;

        int getData = 0;

        //public GameObject shadowProjectile;

        RaycastHit rayHit;
        Rigidbody shadowRigid;

        public Vector3 LastKnownVelocity;
        public Vector3 LastKnownAngularVelocity;

        Vector3 offsetter1;

        Vector3 startPosition;
        Vector3 endPosition;

        bool getVelocityData = false;
        bool hasReachedTarget = false;

        bool updateStuff = false;

        RaycastHit[] rayHitPoints;
        RaycastHit currentHitPoint;

        public GameObject bulletTex;
        public GameObject shadowProjectile;
        public GameObject gunEnd;

		float directionToTargetDot = 0.0f;

		RaycastHit notNullHitPointTransform;


        public float FireTime = 0.49f;


        public sccsshadowbulletpoolerscript sccsshadowbulletpoolerscriptgetter;

        // Update is called once per frame
        /*void FireShadowBullet()
        {
            shadowProjectile = sccsshadowbulletpoolerscript.current.GetPooledObject(); //this.transform.gameObject;// 

            if (shadowProjectile == null) return;

            shadowProjectile.transform.position = gunEnd.transform.position;// transform.position;
            shadowProjectile.transform.rotation = shadowProjectile.transform.rotation;// transform.rotation;
            shadowProjectile.SetActive(true);
        }*/

        // Update is called once per frame
        /*void FireHitEffect()
        {
            shadowProjectile = sccsshadowbulletpoolerscript.current.GetPooledObject(); //this.transform.gameObject;// 

            if (shadowProjectile == null) return;

            shadowProjectile.transform.position = gunEnd.transform.position;// transform.position;
            shadowProjectile.transform.rotation = shadowProjectile.transform.rotation;// transform.rotation;
            shadowProjectile.SetActive(true);
        }*/

        private void Start()
        {

            //sccsshadowbulletpoolerscriptgetter = this.GetComponent<sccsshadowbulletpoolerscript>();
            //InvokeRepeating("FireShadowBullet", FireTime, FireTime);
            // FireShadowBullet();
            //InvokeRepeating("FireHitEffect", FireTime, FireTime);
            //shadowProjectile = Instantiate(shadowProjectile, gunEnd.transform.position, shadowProjectile.transform.rotation);
            //shadowProjectile.SetActive(true);

            shadowProjectile = this.transform.gameObject;
            shadowRigid = this.GetComponent<Rigidbody>();
            shadowRigid.isKinematic = false;
            shadowRigid.AddForce(shadowProjectile.transform.forward * projectileimpulse, ForceMode.Impulse);

            myRigidbody = transform.GetComponent<Rigidbody>();
            previousPos = shadowProjectile.transform.position;
            vertices = transform.GetComponent<MeshFilter>().mesh.vertices;

            rayHitPoints = new RaycastHit[vertices.Length];
            rayhits = new Vector3[vertices.Length];
            rayhitsNorms = new Vector3[vertices.Length];
            listOfVertices = new Vector3[vertices.Length];
            vertDistToCol = new float[vertices.Length];
            objectsCollidingWith = new Transform[vertices.Length];
            triangleIndexHit = new int[vertices.Length * 3];

            myCollider = GetComponent<Collider>();
            startPosition = transform.position;
        }


        int counter = 0;

        void Update()
        {
            startPosition = this.transform.position;
            movementThisStep = shadowProjectile.transform.position - previousPos;
            velocity = shadowProjectile.transform.GetComponent<Rigidbody>().velocity;

            float shadowSpeed = velocity.magnitude;
            float movementSqrMagnitude = movementThisStep.sqrMagnitude;

            Vector3 angularVelocityShadow = shadowProjectile.transform.GetComponent<Rigidbody>().angularVelocity;
            Vector3 realPos = shadowProjectile.transform.position;

            if (checkDistanceToCollision == false)
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    worldPt = transform.TransformPoint(vertices[i]);
                    if (Physics.Raycast(worldPt, velocity, out rayHit, Mathf.Infinity, layerMask))
                    {
                        rayHitPoints[i] = rayHit;
                        rayhits[i] = rayHit.point;
                        rayhitsNorms[i] = rayHit.normal;
                        listOfVertices[i] = vertices[i];
                        vertDistToCol[i] = Vector3.Distance(rayHit.point, worldPt);
                        objectsCollidingWith[i] = rayHit.transform;
                        triangleIndexHit[i] = rayHit.triangleIndex;
                        getVelocity = true;
                        checkDistanceToCollision = true;
                    }
                    else
                    {
                        offsetter1 = shadowProjectile.transform.position * 0.999f; //0.99f
                        getData = 1;
                        hasReachedTarget = false;
                    }
                }
            }

            if (getVelocity == true)
            {
                int[] idx = SortAndIndex(vertDistToCol);

                for (int i = rayhits.Length - 1; i >= 0; i--)
                {
                    if (vertDistToCol[idx[i]] > 0 && rayhits[idx[i]] != Vector3.zero)
                    {
                        pos = idx[i];
                    }
                }
                worldPt = transform.TransformPoint(listOfVertices[pos]);
                offsetter1 = rayhits[pos];
                currentHitPoint = rayHitPoints[pos];
                Vector3 objectCenter = transform.position;
                Vector3 offset00 = worldPt - objectCenter;
				//distanceToTarget = offset00.magnitude;
			
                offsetter1 -= offset00;
                getVelocity = false;
                hasReachedTarget = false;
                getData = 1;
            }



			if(currentHitPoint.transform != null)
			{
				notNullHitPointTransform = currentHitPoint;
			}


			if(notNullHitPointTransform.transform!= null)
			{
				var someDir = (notNullHitPointTransform.point + gunEnd.transform.right) - notNullHitPointTransform.point;
				var dirShadowBulletToTarget = shadowProjectile.transform.position - notNullHitPointTransform.point;
				dirShadowBulletToTarget.Normalize();
				directionToTargetDot = sc_maths.Dot(someDir.x,someDir.y,dirShadowBulletToTarget.x,dirShadowBulletToTarget.y);
			}







            if (letstraybulletgo == 0)
            {
				LastKnownVelocity = shadowProjectile.transform.GetComponent<Rigidbody>().velocity;

                if (getData == 1)
                {
                    if (hasReachedTarget == false)
                    {
                        if (Vector3.Distance(transform.position, offsetter1) < Vector3.Distance(transform.position, shadowProjectile.transform.position))
                        {
                            transform.position = Vector3.MoveTowards(transform.position, offsetter1, shadowSpeed * Time.deltaTime * 0.95f);
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(Vector3.Slerp(transform.position, shadowProjectile.transform.position, shadowSpeed * Time.deltaTime * 0.95f), offsetter1, shadowSpeed * Time.deltaTime * 0.95f);
                        }

                        transform.rotation = Quaternion.Lerp(transform.rotation, shadowProjectile.transform.rotation, angularVelocityShadow.magnitude * 10f * Time.deltaTime);
                        worldPt = transform.TransformPoint(listOfVertices[pos]);

                        if (Vector3.Distance(transform.position, offsetter1) < 0.1f)
                        {
                 
                        }

                        if(this.gameObject.GetComponent<bulletcollision>()!= null)
                        {
                            bulletColScript = this.gameObject.GetComponent<bulletcollision>();
                            bulletColScript.hastarget = 1;
                            bulletColScript.targetHitPoint = currentHitPoint;
                            //Debug.Log(currentHitPoint.transform.name);
                        }


                        if (transform.position == offsetter1)
                        {
                            if (this.gameObject.GetComponent<bulletcollision>() != null)
                            {
                                bulletColScript.enabled = true;
                                //bulletColScript.processChunkByteRemoval();
                            }

                            //LastKnownAngularVelocity = shadowProjectile.transform.GetComponent<Rigidbody>().angularVelocity;

                            myRigidbody.isKinematic = false;
                            //myRigidbody.velocity = Vector3.zero;
                            //myRigidbody.angularVelocity = LastKnownAngularVelocity;

                            if (currentHitPoint.transform != null)
                            {
                                if (currentHitPoint.transform.gameObject.tag == "collisionObject") // && currentHitPoint.transform != this.transform.parent)
                                {


                                    /*if (GetComponent<AudioSource>() != null)
                                    {
                                        GetComponent<AudioSource>().Play(0);
                                    }*/


                                    /* GameObject bulletDecal = Instantiate(bulletTex, currentHitPoint.point, Quaternion.identity);
                                     bulletDecal.SetActive(true);
                                     bulletDecal.transform.forward = -currentHitPoint.normal;
                                     GameObject hitObject = currentHitPoint.transform.gameObject;
                                     bulletDecal.transform.parent = currentHitPoint.transform;*/


                                    //DISCARDEDTOREADD
                                    //DISCARDEDTOREADD
                                    //DISCARDEDTOREADD
                                    //Instantiate(hit_effect, transform.position, Quaternion.identity);
                                    //Instantiate(projectileimpact, transform.position, Quaternion.identity);
                                    //DISCARDEDTOREADD
                                    //DISCARDEDTOREADD
                                    //DISCARDEDTOREADD

                                    //Debug.Log("reached target");
                                    if (transform.tag == "bulletshot")
                                    {
                                        /*if (gameObject.transform.GetComponent<BoxCollider>() != null)
                                        {
                                            Destroy(gameObject.transform.GetComponent<BoxCollider>());
                                        }*/
                                        /*if (gameObject.transform.GetComponent<MeshFilter>() != null)
                                        {
                                            Destroy(gameObject.transform.GetComponent<MeshFilter>());
                                        }
                                        if (gameObject.transform.GetComponent<MeshRenderer>() != null)
                                        {
                                            Destroy(gameObject.transform.GetComponent<MeshRenderer>());
                                        }*/




                                        //TOREACTIVATE
                                        /*gameObject.GetComponent<Fracture4>().enabled = true;
                                        for (int i = 0; i < gameObject.GetComponent<Fracture4>().FracturingObj.Count; i++)
                                        {
                                            gameObject.GetComponent<Fracture4>().FracturingObj[i].transform.parent = null;
                                        }*/
                                        //TOREACTIVATE




                                        //Destroy(this.gameObject);
                                    }



                                    /*if (shadowProjectile.transform.parent.tag == "bulletshot")
                                    {
                                        if (shadowProjectile.gameObject.transform.GetComponent<BoxCollider>() != null)
                                        {
                                            Destroy(shadowProjectile.gameObject.transform.GetComponent<BoxCollider>());
                                        }
                                        if (shadowProjectile.gameObject.transform.GetComponent<MeshFilter>() != null)
                                        {
                                            Destroy(shadowProjectile.gameObject.transform.GetComponent<MeshFilter>());
                                        }
                                        if (shadowProjectile.gameObject.transform.GetComponent<MeshRenderer>() != null)
                                        {
                                            Destroy(shadowProjectile.gameObject.transform.GetComponent<MeshRenderer>());
                                        }

                                        shadowProjectile.gameObject.GetComponent<Fracture4>().enabled = true;
                                        //Debug.Log("bulletshot");
                                        for (int i = 0; i < shadowProjectile.gameObject.GetComponent<Fracture4>().FracturingObj.Count; i++)
                                        {
                                            shadowProjectile.gameObject.GetComponent<Fracture4>().FracturingObj[i].transform.parent = null;
                                        }
                                        //Destroy(this.gameObject);

                                    }*/
                                }
                            }
                            else
                            {
                                //Debug.Log("collision is null. imperfect shot total: " + straybullet); //my stuff is not good enough yet.
                                //Destroy(this.transform.gameObject); //autodestruct otherwise a stray bullet becomes an asteroid lets try this but its going to fail when i go too fast but explode projectile if ultrauber close to the target.
                                letstraybulletgo = 1;
                                straybullet++;
                            }
                            /*if (currentHitPoint.transform.gameObject.tag == "enemy")
                            {
                                bulletDecal.GetComponent<Decal>().material = bulletDecal.GetComponent<Decal>().materialBlood;
                                bulletDecal.GetComponent<Decal>().sprite = bulletDecal.GetComponent<Decal>().spriteBlood;
                            }*/

                            //shadowProjectile.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                            shadowProjectile.transform.position = transform.position;
                            shadowRigid.isKinematic = true;
                            getData = 3;
                            hasReachedTarget = true;
                            checkDistanceToCollision = false;

                            //hasReachedTarget = true;
                            //checkDistanceToCollision = false;

                            /*LastKnownVelocity = shadowProjectile.transform.GetComponent<Rigidbody>().velocity;
                            LastKnownAngularVelocity = shadowProjectile.transform.GetComponent<Rigidbody>().angularVelocity;
                            collisionatorManager.collisionForceToApply(objectsCollidingWith[pos], rayhits[pos], LastKnownVelocity, movementThisStep, listOfVertices[pos], triangleIndexHit[pos], pos, transform);

                            applyingCollisionForceToObjectCollidedWith = true;
                            hasReachedTarget = true;
                            checkDistanceToCollision = false;
                            updateStuff = true;

                            if (getVelocityData == false)
                            {
                                if (LastKnownVelocity.magnitude > 10)
                                {
                                    getData = 2;
                                }
                                else
                                {
                                    myRigidbody.isKinematic = false;
                                    myRigidbody.velocity = LastKnownVelocity;
                                    myRigidbody.angularVelocity = LastKnownAngularVelocity;

                                    shadowProjectile.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                                    shadowProjectile.transform.position = transform.position;
                                    shadowRigid.isKinematic = true;
                                    getData = 3;
                                }
                                hasReachedTarget = true;
                                checkDistanceToCollision = false;
                                getData = 3;
                            }*/

                        }
                        else
                        {
							//Debug.Log(directionToTargetDot);
							if(directionToTargetDot < 0)
							{
								//Debug.Log("bullet0");
							}
							else
							{                
								this.transform.GetComponent<Rigidbody> ().velocity = Vector3.zero;						
								//Debug.Log("bullet1");
							}
								
							//shadowProjectile.transform.position = transform.position;
							//hasReachedTarget = false;
							//checkDistanceToCollision = false;
                        }
                    }
                }


                /*if (shadowProjectile.GetComponent<Rigidbody>().velocity.magnitude <= 1 && transform.GetComponent<Rigidbody>().velocity.magnitude <= 1)
                {
                    //Debug.Log("no Need for collision Manager");
                    shadowProjectile.transform.position = transform.position;
                    shadowProjectile.transform.parent = transform;
                }
                else
                {
                    if (shadowProjectile.transform.parent != null)
                    {
                        shadowProjectile.transform.parent = null;

                    }
                    if (shadowRigid.isKinematic == true)
                    {
                        shadowRigid.isKinematic = false;
                        shadowProjectile.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity;
                    }
                }*/
            }

            if (letstraybulletgo == 1)
            {
                this.transform.GetComponent<Rigidbody>().velocity = LastKnownVelocity;

                //Debug.Log("stray bullet");
				if(directionToTargetDot <= 0 || notNullHitPointTransform.transform== null)
				{
					//Debug.Log("bullet missed target");
					shadowProjectile.transform.position = transform.position;
					hasReachedTarget = false;
					checkDistanceToCollision = false;
				}

                letstraybulletgo = 2;
            }

            if (letstraybulletgo == 2)
            {
                shadowProjectile.transform.position = transform.position;

                if (Physics.Raycast(transform.position, this.transform.GetComponent<Rigidbody>().velocity.normalized, out rayHit, Mathf.Infinity, layerMask))
                {
                    if (rayHit.transform != null)
                    {
                        //Debug.Log(rayHit.transform.name);

                       /* if ()
                        {

                        }*/
                    }
                }
            }




            if (bulletlife > maxbulletlife)
            {
                if (this.gameObject.tag == "bulletshot")
                {
                    Destroy(this.gameObject);
                    bulletlife = 0;
                }
                else if (this.gameObject.tag == "bbbulletfragment")
                {
                    Destroy(this.gameObject);
                    bulletlife = 0;
                }
            }

            bulletlife++;
            
        }
        public int maxbulletlife = 999;
		public int bulletlife = 0;


        void OnCollisionEnter(Collision collision)
        {

            if (collision.transform.tag == "collisionObject") // && currentHitPoint.transform != this.transform.parent)
            {           
                //GetComponent<AudioSource>().enabled = true;
                Instantiate(hit_effect, transform.position, Quaternion.identity);
                Instantiate(projectileimpact, transform.position, Quaternion.identity);


                //Debug.Log("col");

                var bulletColScript = this.gameObject.GetComponent<bulletcollision>();
                bulletColScript.hastarget = 2;
                //bulletColScript.targetHitPoint = null;

                bulletColScript.collisionTransform = collision.transform;
                bulletColScript.collisionNormal = collision.contacts[0].normal;
                bulletColScript.collisionPoint = collision.contacts[0].point;

                bulletColScript.enabled = true;
                //bulletColScript.processChunkByteRemoval();



                /*GameObject bulletDecal = Instantiate(bulletTex, collision.contacts[0].point, Quaternion.identity);
                bulletDecal.SetActive(true);
                bulletDecal.transform.forward = -collision.contacts[0].normal;
                GameObject hitObject = collision.transform.gameObject;
                bulletDecal.transform.parent = collision.transform;
                
                Instantiate(hit_effect, transform.position, Quaternion.identity);*/

                //Debug.Log("reached target");
            }

            /*
            if (transform.tag == "bulletshot")
            {
                /*if (gameObject.transform.GetComponent<BoxCollider>() != null)
                {
                    Destroy(gameObject.transform.GetComponent<BoxCollider>());
                }
                if (gameObject.transform.GetComponent<MeshFilter>() != null)
                {
                    Destroy(gameObject.transform.GetComponent<MeshFilter>());
                }
                if (gameObject.transform.GetComponent<MeshRenderer>() != null)
                {
                    Destroy(gameObject.transform.GetComponent<MeshRenderer>());
                }

                if (gameObject.GetComponent<Fracture4>()!= null)
                {
                    gameObject.GetComponent<Fracture4>().enabled = true;
                }

                gameObject.SetActive(false);


                //Debug.Log("bulletshot");
                /*for (int i = 0; i < gameObject.GetComponent<Fracture4>().FracturingObj.Count; i++)
                {
                    gameObject.GetComponent<Fracture4>().FracturingObj[i].transform.parent = null;
                }
                //Destroy(this.gameObject);
            }*/











            //Destroy(this.gameObject);
            //Destroy(shadowProjectile.gameObject);
        }

        
        void OnTriggerEnter(Collider col)
        {
            //GetComponent<AudioSource>().enabled = true;
            /*if (GetComponent<AudioSource>() !=null)
            {
                GetComponent<AudioSource>().Play(0);
            }*/
            //Instantiate(hit_effect, transform.position, Quaternion.identity);

            /*if (this.gameObject.tag == "bulletshot")
            {
                GetComponent<AudioSource>().Play(0);
            }
            Destroy(this.gameObject);*/
            /*var bulletColScript = this.gameObject.GetComponent<bulletcollision>();
            bulletColScript.hastarget = 1;
            bulletColScript.targetHitPoint = currentHitPoint;
            bulletColScript.enabled = true;
            bulletColScript.processChunkByteRemoval();*/





















            /*if (this.gameObject.tag == "bulletshot")
            {
                //if (this.gameObject != null)
                {
                    //if (transform != null)
                    {
                        if (transform.parent != null)
                        {
                            if (transform.parent.tag == "bulletshot")
                            {
                                if (gameObject.transform.GetComponent<BoxCollider>() != null)
                                {
                                    Destroy(gameObject.transform.GetComponent<BoxCollider>());
                                }
                                if (gameObject.transform.GetComponent<MeshFilter>() != null)
                                {
                                    Destroy(gameObject.transform.GetComponent<MeshFilter>());
                                }
                                if (gameObject.transform.GetComponent<MeshRenderer>() != null)
                                {
                                    Destroy(gameObject.transform.GetComponent<MeshRenderer>());
                                }

                                gameObject.GetComponent<Fracture4>().enabled = true;
                                //Debug.Log("bulletshot");
                                for (int i = 0; i < shadowProjectile.gameObject.GetComponent<Fracture4>().FracturingObj.Count; i++)
                                {
                                    gameObject.GetComponent<Fracture4>().FracturingObj[i].transform.parent = null;
                                }
                                //Destroy(this.gameObject);

                            }
                        }
                        else
                        {
                            Debug.Log("parent == null");
                            Destroy(this.gameObject);
                        }
                    }
                    /*else
                    {
                        //Debug.Log("null0");
                        Destroy(this.gameObject);
                    }
                    Destroy(this.gameObject);





                    /*if (this.gameObject.transform.GetComponent<Rigidbody>() != null)
                    {
                        Destroy(this.gameObject.transform.GetComponent<Rigidbody>());
                    }*/









            /*var childL = this.gameObject.transform.Find("Triangle 0");
            if (childL != null)
            {
                if (childL.transform != null)
                {
                    if (childL.transform.parent != null)
                    {
                        childL.transform.parent = null;
                        childL.gameObject.SetActive(true);
                    }
                }
            }

            var childR = this.gameObject.transform.Find("Triangle 1");
            if (childR != null)
            {
                if (childR.transform != null)
                {
                    if (childR.transform.parent != null)
                    {
                        childR.transform.parent = null;
                        childR.gameObject.SetActive(true);
                    }
                }
            }

            /*parent = this.gameObject.transform.parent;
            this.gameObject.transform.parent = null;

        }
    }*/

        }


        /*void OnTriggerEnter(Collider col)
        {
            Instantiate(hit_effect, transform.position, Quaternion.identity);

            if (this.gameObject.tag == "bulletshot")
            {
                if (this.gameObject != null)
                {

                }
            }
        }*/






        int[] SortAndIndex<T>(T[] rg)
        {
            int i, c = rg.Length;
            var keys = new int[c];
            if (c > 1)
            {
                for (i = 0; i < c; i++)
                    keys[i] = i;

                System.Array.Sort(rg, keys /*, ... */);
            }
            return keys;
        }
    }

}



