using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SCCoreSystems
{
    public class sc_bullet : MonoBehaviour
    {

        private AudioSource gunAudio;
        //public Transform bullet;
        public Rigidbody rigidbullet;
        public Transform gunEnd;

        [HideInInspector]
        public float force = 0;

        public Vector3 bullseyedirection;

        public int maxbulletlife = 100;
        public int bulletlife = 0;
        public GameObject hit_effect;
        public GameObject firing_ship;
        public Transform parent;


        void Awake()
        {
            if (this.gameObject.tag == "bbbulletfragment")
            {
                if (this.gameObject.GetComponent<SplitMeshIntoTriangles>() != null)
                {
                    this.gameObject.GetComponent<SplitMeshIntoTriangles>().enabled = false;
                }

                //maxbulletlife = 59;
            }
            else if (this.gameObject.tag == "bulletshot")
            {
                if (this.gameObject.GetComponent<SplitMeshIntoTriangles>() != null)
                {
                    this.gameObject.GetComponent<SplitMeshIntoTriangles>().enabled = false;
                }
                //rigidbullet.isKinematic = false;
                //rigidbullet.useGravity = false;
                //rigidbullet.AddForce(bullseyedirection * force, ForceMode.Impulse);
                //gunAudio = GetComponent<AudioSource>();
                //gunAudio.Play();
            }
            rigidbullet = this.transform.gameObject.GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (bulletlife > maxbulletlife)
            {
                if (this.gameObject.tag == "bulletshot")
                {
                    Destroy(this.gameObject);
                    bulletlife = 0;
                }
                else if(this.gameObject.tag == "bbbulletfragment")
                {
                    Destroy(this.gameObject);
                    bulletlife = 0;
                }
            }
            bulletlife++;
        }

        void OnTriggerEnter(Collider col)
        {
            //Instantiate(hit_effect, transform.position, Quaternion.identity);

            if (this.gameObject.tag == "bulletshot")
            {
                if (this.gameObject != null)
                {
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
                    }*/

                    /*parent = this.gameObject.transform.parent;
                    this.gameObject.transform.parent = null;*/
                    Destroy(this.gameObject);
                }
            }
            /*else if (this.gameObject.tag == "bbbulletfragment")
            {
                if (this.gameObject != null)
                {
                    Destroy(this.gameObject);
                }
            }*/

            /*if (this.gameObject.GetComponent<SplitMeshIntoTriangles>() != null)
            {
                this.gameObject.GetComponent<SplitMeshIntoTriangles>().enabled = true;
                //this.gameObject.GetComponent<SplitMeshIntoTriangles>().SplitMesh();
            }*/

            //SplitMeshIntoTriangles splitmesh = new SplitMeshIntoTriangles();
            //Destroy(gameObject);
            //Don't want to collide with the ship that's shooting this thing, nor another projectile.
            /*//if (col.gameObject.tag == "c-19-mesh") //Projectile //col.gameObject != firing_ship && 
            {
                //Instantiate(hit_effect, transform.position, Quaternion.identity);
                //Destroy(gameObject);
            }*/
        }

        /*
        //If your GameObject starts to collide with another GameObject with a Collider
        void OnCollisionEnter(Collision collision)
        {
            //Output the Collider's GameObject's name
            Debug.Log("test0"); // collision.collider.name

            if (collision.collider.name == "c-19")
            {
                Debug.Log("test");
            }


        }

        //If your GameObject keeps colliding with another GameObject with a Collider, do something
        void OnCollisionStay(Collision collision)
        {
            Debug.Log("test1"); //
            //Check to see if the Collider's name is "Chest"
            if (collision.collider.name == "Chest")
            {
                //Output the message
                Debug.Log("Chest is here!");
            }
        }*/

        /*void OnCollisionEnter(Collision col)
        {

            if (col.gameObject.tag == "climb")
            {
                Destroy(col.gameObject);
            }
        }*/
    }
}