using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;



namespace SCCoreSystems
{


    public class shadowBulletTwo : MonoBehaviour
    {
        public Vector3 beforestrayvelo = Vector3.zero;
        public int letstraybulletgo = 0;
        public int straybullet = 0;

        public Vector3 bullseyedirection = Vector3.zero;
        public float projectileimpulse = 0.0f;
        public GameObject firing_ship;
        public GameObject hit_effect;

        Rigidbody myRigidbody;
        Vector3 previousPos;

        Vector3 movementThisStep;

        public LayerMask layerMask = -1;



        //public GameObject collPoint;
        Vector3 velocity;

        bool getVelocity = false;
        Vector3 worldPt;

        bool checkDistanceToCollision = false;
        float currentSpeed = 0;

 
        int pos = 0;

        private Collider myCollider;

        int getData = 0;

        GameObject shadowObject;

        RaycastHit rayHit;
        Rigidbody shadowRigid;

        public Vector3 LastKnownVelocity;
        public Vector3 LastKnownAngularVelocity;

        Vector3 offsetter1;

        Vector3 startPosition;
        Vector3 endPosition;

        bool getVelocityData = false;
        public collisionatorManager collisionatorManager;
        bool hasReachedTarget = false;
        bool applyingCollisionForceToObjectCollidedWith = false;

        bool updateStuff = false;


        RaycastHit currentHitPoint;

        public GameObject bulletTex;
        public GameObject shadowProjectile;
        public GameObject gunEnd;


        Vector3[] vertices;




        RaycastHit[] rayHitPoints;
        Vector3[] rayhits;
        Vector3[] rayhitsNorms;
        Vector3[] listOfVertices;
        float[] vertDistToCol;
        Transform[] objectsCollidingWith;
        int[] triangleIndexHit;



        RaycastHit[] rayHitPointsTwo;
        Vector3[] rayhitsTwo;
        Vector3[] rayhitsNormsTwo;
        Vector3[] listOfVerticesTwo;
        float[] vertDistToColTwo;
        Transform[] objectsCollidingWithTwo;
        int[] triangleIndexHitTwo;

        Transform parentObject;

        private void Start()
        {




            shadowObject = Instantiate(shadowProjectile, gunEnd.transform.position, Quaternion.identity); //transform.rotation
            shadowObject.SetActive(true);

            parentObject = shadowObject.transform.parent;

            shadowObject.transform.parent = null;
            shadowRigid = shadowObject.GetComponent<Rigidbody>();
            shadowRigid.isKinematic = false;

            myRigidbody = transform.GetComponent<Rigidbody>();
            previousPos = shadowObject.transform.position;
            vertices = transform.GetComponent<MeshFilter>().mesh.vertices;



            Vector3 realPos = shadowObject.transform.position;

            for (int i = 0; i < vertices.Length; i++)
            {
                /*worldPt = transform.TransformPoint(vertices[i]);
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
                }*/

                if (Physics.Raycast(realPos, shadowRigid.transform.forward, out rayHit, Mathf.Infinity, layerMask))
                {
                    rayHitPointsTwo[i] = rayHit;
                    rayhitsTwo[i] = rayHit.point;
                    rayhitsNormsTwo[i] = rayHit.normal;
                    listOfVerticesTwo[i] = vertices[i];
                    vertDistToColTwo[i] = Vector3.Distance(rayHit.point, realPos);
                    objectsCollidingWithTwo[i] = rayHit.transform;
                    triangleIndexHitTwo[i] = rayHit.triangleIndex;

                    //getVelocity = true;
                    //checkDistanceToCollision = true;
                    getData = 1;
                    hasReachedTarget = false;
                }
                else
                {
                    offsetter1 = shadowObject.transform.position * 0.99f;
                    getData = 1;
                    hasReachedTarget = false;
                }
            }









            //shadowRigid.AddForce(bullseyedirection * projectileimpulse, ForceMode.Impulse);


            rayHitPoints = new RaycastHit[vertices.Length];
            rayhits = new Vector3[vertices.Length];
            rayhitsNorms = new Vector3[vertices.Length];
            listOfVertices = new Vector3[vertices.Length];
            vertDistToCol = new float[vertices.Length];
            objectsCollidingWith = new Transform[vertices.Length];
            triangleIndexHit = new int[vertices.Length * 3];



            rayHitPointsTwo = new RaycastHit[vertices.Length];
            rayhitsTwo = new Vector3[vertices.Length];
            rayhitsNormsTwo = new Vector3[vertices.Length];
            listOfVerticesTwo = new Vector3[vertices.Length];
            vertDistToColTwo = new float[vertices.Length];
            objectsCollidingWithTwo = new Transform[vertices.Length];
            triangleIndexHitTwo = new int[vertices.Length * 3];





            myCollider = GetComponent<Collider>();
            startPosition = transform.position;
        }


        int counter = 0;

        void Update()
        {
            startPosition = transform.position;
            movementThisStep = shadowObject.transform.position - previousPos;
            velocity = shadowObject.transform.GetComponent<Rigidbody>().velocity;

            float shadowSpeed = velocity.magnitude;
            float movementSqrMagnitude = movementThisStep.sqrMagnitude;

            Vector3 angularVelocityShadow = shadowObject.transform.GetComponent<Rigidbody>().angularVelocity;
            Vector3 realPos = shadowObject.transform.position;

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
                        offsetter1 = shadowObject.transform.position * 0.99f;
                        getData = 1;
                        hasReachedTarget = false;
                    }
                }
            }

            for (var i = objectsCollidingWith.Length - 1; i >= 0; i--)
            {
                if (objectsCollidingWith[i] != null)
                {
                    Debug.Log(objectsCollidingWith[i].name);
                }
            }


            /*for (int i = 0; i < vertices.Length; i++)
            {
                if (Physics.Raycast(realPos, shadowRigid.transform.forward, out rayHit, Mathf.Infinity, layerMask))
                {
                    rayHitPointsTwo[i] = rayHit;
                    rayhitsTwo[i] = rayHit.point;
                    rayhitsNormsTwo[i] = rayHit.normal;
                    listOfVerticesTwo[i] = vertices[i];
                    vertDistToColTwo[i] = Vector3.Distance(rayHit.point, realPos);
                    objectsCollidingWithTwo[i] = rayHit.transform;
                    triangleIndexHitTwo[i] = rayHit.triangleIndex;

                    //getVelocity = true;
                    //checkDistanceToCollision = true;
                    getData = 1;
                    hasReachedTarget = false;
                }
                else
                {
                    offsetter1 = shadowObject.transform.position * 0.99f;
                    getData = 1;
                    hasReachedTarget = false;
                }
            }

            for (var i = objectsCollidingWithTwo.Length - 1; i >= 0; i--)
            {
                if (objectsCollidingWithTwo[i] != null)
                {
                    Debug.Log(objectsCollidingWithTwo[i].name);
                }

            }*/





            if (getVelocity == true)
            {
                /*int i = 0;
                for (i = rayHitPointsTwo.Length - 1; i >= 0; i--)
                {
                    if (rayHitPointsTwo[i].transform != null)
                    {
                        currentHitPoint = rayHitPointsTwo[i];
                    }
                }

                worldPt = transform.TransformPoint(listOfVerticesTwo[i]);
                offsetter1 = rayhitsTwo[i];*/


















                /*int[] idx = SortAndIndex(vertDistToCol);

                for (int i = rayhits.Length - 1; i >= 0; i--)
                {
                    if (vertDistToCol[idx[i]] > 0 && rayhits[idx[i]] != Vector3.zero)
                    {
                        pos = idx[i];
                    }
                }

                worldPt = transform.TransformPoint(listOfVertices[pos]);
                offsetter1 = rayhits[pos];

                if (rayHitPoints[pos].transform!= null)
                {
                    currentHitPoint = rayHitPoints[pos];
                }*/
               /*Vector3 objectCenter = transform.position;
                Vector3 offset00 = worldPt - objectCenter;
                offsetter1 -= offset00;
                getVelocity = false;
                hasReachedTarget = false;
                getData = 1;*/
            }







            /*if (currentHitPoint.transform == null)
            {
                Debug.Log("null");
            }*/





            /*
            if (letstraybulletgo == 0)
            {
                if (getData == 1)
                {
                    //if (applyingCollisionForceToObjectCollidedWith)
                    //{
                    //    shadowObject.transform.GetComponent<Rigidbody>().velocity *= 0.85f;
                    //    shadowObject.transform.GetComponent<Rigidbody>().angularVelocity *= 0.75f;
                    //    velocity = shadowObject.transform.GetComponent<Rigidbody>().velocity;
                    //    shadowSpeed = velocity.magnitude;
                    //    applyingCollisionForceToObjectCollidedWith = false;
                    //    hasReachedTarget = false;
                    //}

                    if (hasReachedTarget == false)
                    {
                        if (Vector3.Distance(transform.position, offsetter1) < Vector3.Distance(transform.position, shadowObject.transform.position))
                        {
                            transform.position = Vector3.MoveTowards(transform.position, offsetter1, shadowSpeed * Time.deltaTime * 0.95f);
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(Vector3.Slerp(transform.position, shadowObject.transform.position, shadowSpeed * Time.deltaTime * 0.95f), offsetter1, shadowSpeed * Time.deltaTime * 0.95f);
                        }

                        transform.rotation = Quaternion.Lerp(transform.rotation, shadowObject.transform.rotation, angularVelocityShadow.magnitude * 10f * Time.deltaTime);
                        worldPt = transform.TransformPoint(listOfVertices[pos]);

                        if (transform.position == offsetter1)
                        {
                            Debug.Log("reachedtarget");
                            //LastKnownAngularVelocity = shadowObject.transform.GetComponent<Rigidbody>().angularVelocity;

                            //collisionatorManager.collisionForceToApply(objectsCollidingWith[pos], rayhits[pos], LastKnownVelocity, movementThisStep, listOfVertices[pos], triangleIndexHit[pos], pos, transform, currentHitPoint);

                            myRigidbody.isKinematic = false;
                            //myRigidbody.velocity = Vector3.zero;
                            //myRigidbody.angularVelocity = LastKnownAngularVelocity;

                            if (currentHitPoint.transform != null)
                            {
                                if (currentHitPoint.transform.gameObject.tag == "collisionObject") // && currentHitPoint.transform != this.transform.parent)
                                {
                                    GameObject bulletDecal = Instantiate(bulletTex, currentHitPoint.point, Quaternion.identity);
                                    bulletDecal.SetActive(true);
                                    bulletDecal.transform.forward = -currentHitPoint.normal;
                                    GameObject hitObject = currentHitPoint.transform.gameObject;
                                    bulletDecal.transform.parent = currentHitPoint.transform;

                                    Instantiate(hit_effect, transform.position, Quaternion.identity);
                                    //Debug.Log("reached target");
                                    if (transform.tag == "bulletshot")
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
                                        for (int i = 0; i < gameObject.GetComponent<Fracture4>().FracturingObj.Count; i++)
                                        {
                                            gameObject.GetComponent<Fracture4>().FracturingObj[i].transform.parent = null;
                                        }
                                        //Destroy(this.gameObject);

        }



        //if (shadowObject.transform.parent.tag == "bulletshot")
        //{
        //    if (shadowObject.gameObject.transform.GetComponent<BoxCollider>() != null)
        //    {
        //        Destroy(shadowObject.gameObject.transform.GetComponent<BoxCollider>());
        //    }
        //    if (shadowObject.gameObject.transform.GetComponent<MeshFilter>() != null)
        //    {
        //        Destroy(shadowObject.gameObject.transform.GetComponent<MeshFilter>());
        //    }
        //    if (shadowObject.gameObject.transform.GetComponent<MeshRenderer>() != null)
        //    {
        //        Destroy(shadowObject.gameObject.transform.GetComponent<MeshRenderer>());
        //    }
        //
        //    shadowObject.gameObject.GetComponent<Fracture4>().enabled = true;
        //    //Debug.Log("bulletshot");
        //    for (int i = 0; i < shadowObject.gameObject.GetComponent<Fracture4>().FracturingObj.Count; i++)
        //    {
        //        shadowObject.gameObject.GetComponent<Fracture4>().FracturingObj[i].transform.parent = null;
        //    }
        //    //Destroy(this.gameObject);
        //
        //}
    }
                            }
                            else
                            {
                                //Debug.Log("collision is null. imperfect shot total: " + straybullet); //my stuff is not good enough yet.
                                //Destroy(this.transform.gameObject); //autodestruct otherwise a stray bullet becomes an asteroid lets try this but its going to fail when i go too fast but explode projectile if ultrauber close to the target.
                                //letstraybulletgo = 1;
                                straybullet++;
                            }
                            //if (currentHitPoint.transform.gameObject.tag == "enemy")
                            //{
                            //    bulletDecal.GetComponent<Decal>().material = bulletDecal.GetComponent<Decal>().materialBlood;
                            //    bulletDecal.GetComponent<Decal>().sprite = bulletDecal.GetComponent<Decal>().spriteBlood;
                            //}

                            //shadowObject.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                            shadowObject.transform.position = transform.position;
                            shadowRigid.isKinematic = true;
                            getData = 3;
                            hasReachedTarget = true;
                            checkDistanceToCollision = false;

                            //hasReachedTarget = true;
                            //checkDistanceToCollision = false;

                            //LastKnownVelocity = shadowObject.transform.GetComponent<Rigidbody>().velocity;
                            //LastKnownAngularVelocity = shadowObject.transform.GetComponent<Rigidbody>().angularVelocity;
                            //collisionatorManager.collisionForceToApply(objectsCollidingWith[pos], rayhits[pos], LastKnownVelocity, movementThisStep, listOfVertices[pos], triangleIndexHit[pos], pos, transform);

                            //applyingCollisionForceToObjectCollidedWith = true;
                            //hasReachedTarget = true;
                            //checkDistanceToCollision = false;
                            //updateStuff = true;

                            //if (getVelocityData == false)
                            //{
                            //    if (LastKnownVelocity.magnitude > 10)
                            //    {
                            //        getData = 2;
                            //    }
                            //    else
                            //    {
                            //        myRigidbody.isKinematic = false;
                            //        myRigidbody.velocity = LastKnownVelocity;
                            //        myRigidbody.angularVelocity = LastKnownAngularVelocity;
                            //
                            //        shadowObject.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                            //        shadowObject.transform.position = transform.position;
                            //        shadowRigid.isKinematic = true;
                            //        getData = 3;
                            //    }
                            //    hasReachedTarget = true;
                            //    checkDistanceToCollision = false;
                            //    getData = 3;
                            //}

                        }
                        else
                        {
                            shadowObject.transform.position = transform.position;
                            hasReachedTarget = true;
                            checkDistanceToCollision = false;
                        }
                    }
                }


                /*if (shadowObject.GetComponent<Rigidbody>().velocity.magnitude <= 1 && transform.GetComponent<Rigidbody>().velocity.magnitude <= 1)
                {
                    //Debug.Log("no Need for collision Manager");
                    shadowObject.transform.position = transform.position;
                    shadowObject.transform.parent = transform;
                }
                else
                {
                    if (shadowObject.transform.parent != null)
                    {
                        shadowObject.transform.parent = null;
                    }
                    if (shadowRigid.isKinematic == true)
                    {
                        shadowRigid.isKinematic = false;
                        shadowObject.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity;
                    }
                }
            }*/

            /*if (letstraybulletgo == 1)
            {
                this.transform.GetComponent<Rigidbody>().velocity = LastKnownVelocity;
                letstraybulletgo = 2;
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
            }*/


            LastKnownVelocity = shadowObject.transform.GetComponent<Rigidbody>().velocity;
            bulletlife++;
        }
        public int maxbulletlife = 59;
        private int bulletlife = 59;






        void OnTriggerEnter(Collider col)
        {
            Instantiate(hit_effect, transform.position, Quaternion.identity);



            if (this.gameObject.tag == "bulletshot")
            {

            }






















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
                                for (int i = 0; i < shadowObject.gameObject.GetComponent<Fracture4>().FracturingObj.Count; i++)
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





        //stackoverflow
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



