using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounders : MonoBehaviour {

    private Renderer rend;
    bool canStopVelocity = false;
    Vector3[] vertices;

    void Start()
    {
        vertices = transform.GetComponent<MeshFilter>().mesh.vertices;
        //rend = transform.GetComponent<Renderer>();
    }

    private void Update()
    {

        /*Bounds bounds = transform.GetComponent<Collider>().bounds;
        MeshFilter[] meshes = GetComponentsInChildren<MeshFilter>();

        for (int i = 0; i < meshes.Length; i++)
        {
            Mesh ms = meshes[i].mesh;
            int vc = ms.vertexCount;

            for (int j = 0; j < vc; j++)
            {
                if (i == 0 && j == 0)
                {
                    bounds = new Bounds(transform.TransformPoint(vertices[j]), Vector3.zero);
                }
                else
                {
                    bounds.Encapsulate(transform.TransformPoint(vertices[j]));
                }
            }
        }*/















        /*if (transform.name == "cube1")
        {
            if (canStopVelocity == false)
            {
                transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0.1f),ForceMode.Force);

            }
        }*/
    }


    private void OnTriggerEnter(Collider collider)
    {
        /*if (transform.name == "cube1")
        {
          
            if (collider.transform.GetComponent<Rigidbody>() != null)
            {
                //Rigidbody rigid0 = collider.transform.GetComponent<Rigidbody>();
                Rigidbody rigid1 = transform.GetComponent<Rigidbody>();

                Vector3 velocity = rigid1.velocity;
                Debug.Log("addingVelocity");
                //collider.transform.GetComponent<Rigidbody>().velocity = velocity;
                collider.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0.1f), ForceMode.Force);


                canStopVelocity = true;
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
          
        }*/
    }



    /*void OnCollisionStay(Collision collision)
    {
        /*Bounds bounds = transform.GetComponent<Renderer>().bounds;

        if (collision.transform.GetComponent<Renderer>().bounds.Intersects(bounds))
        {
            Debug.Log("intersects bounds");
        }

        Debug.Log("collision");
    }*/




    /*void OnDrawGizmosSelected()
    {
        Collider collider = transform.GetComponent<Collider>();

        Vector3 center = collider.bounds.center;

        float radius = collider.bounds.extents.magnitude;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, collider.bounds.size);


        /*Vector3 center = rend.bounds.center;
        float radius = rend.bounds.extents.magnitude;
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(center, rend.bounds.size);

        //Gizmos.DrawWireSphere(center, radius);
    }*/
}
