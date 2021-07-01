using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsgearlogic : MonoBehaviour
{
    public Rigidbody rigid;
    //public Transform poinToAddImpulse;
    public float initforce = 10;
    public float continuousforce = 10;
    public int maxcounter = 1;

    /*public Transform vert0; //gearright vert0
    public Transform vert1; //gearright vert1
    public Transform vert2; //gearright vert2

    public Transform vert3; //gearright vert0
    public Transform vert4; //gearright vert1
    public Transform vert5; //gearright vert2*/


    void Start ()
    {
        rigid = this.transform.GetComponent<Rigidbody>();

        //rigid.angularVelocity += vert2.right * initforce;
    }

    int gearturncount = 0;

    int pushcounter = 0;


    void Update ()
    {
        if (pushcounter == 0)
        {

        }


        /*
        if (gearturncount >= maxcounter)
        {
            //rigid.AddForce(poinToAddImpulse.up * force, ForceMode.Impulse);

            rigid.angularVelocity += poinToAddImpulse.right * force;

            gearturncount = 0;
        }
        gearturncount++;*/

        pushcounter++;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("geartotheleft");
        if (col.transform.tag == "geartotheleft")
        {
            Debug.Log("geartotheleft");
        }



        //rigid.angularVelocity += this.transform.right * continuousforce;

        if (gearturncount >= maxcounter)
        {
            //rigid.AddForce(poinToAddImpulse.up * force, ForceMode.Impulse);

            //rigid.angularVelocity += poinToAddImpulse.right * continuousforce;

            gearturncount = 0;
        }
        gearturncount++;
    }
}
