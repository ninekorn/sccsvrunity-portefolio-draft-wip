using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsgear : MonoBehaviour
{
    public string notes = "";
    public float explosionradius = 0.1f;
    public float explosionForce = 1.0f;
    //public Transform poinToAddImpulse;
    public float initforce = 10;
    public float continuousforce = 10;
    public int maxcounter = 1;

    public Transform vert0; //gearright vert0
    public Transform vert1; //gearright vert1
    public Transform vert2; //gearright vert2

    public Transform vert3; //gearright vert0
    public Transform vert4; //gearright vert1
    public Transform vert5; //gearright vert2

    public Rigidbody2D rigidgearL;
    public Rigidbody2D rigidgearR;

    public int vectortype = 0;


    void Start ()
    {
        rigidgearL = this.transform.GetComponent<Rigidbody2D>();
        rigidgearR = this.transform.GetComponent<Rigidbody2D>();
        //rigidgearL.angularVelocity += vert0.right * initforce;

        if (vectortype == 0)
        {

        }
        else if (vectortype == 1)
        {

        }
        else if (vectortype == 2)
        {

        }
    }

    int gearturncount = 0;

    int pushcounter = 0;


    void Update ()
    {
        if (pushcounter == 0)
        {
            //rigidgearL.AddExplosionForce(explosionForce, vert3.position, explosionradius, 0, ForceMode.Impulse);// vert3.forward * initforce, ForceMode.Force);
            //rigidgearL.AddForceAtPosition(vert3.forward * initforce, vert3.position, ForceMode.Impulse);
            //rigidgearL.AddForce(vert3.right * initforce, ForceMode2D.Impulse);
            rigidgearL.angularVelocity -= initforce;
            pushcounter = 1;
        }
        else if (pushcounter == 1)
        {
            //rigidgearL.AddExplosionForce(explosionForce, vert2.position, explosionradius, 0, ForceMode.Impulse);
            //rigidgearL.AddForceAtPosition(vert2.forward * initforce, vert2.position, ForceMode.Impulse);
            //rigidgearL.AddForce(vert1.right * initforce, ForceMode2D.Impulse);
            //rigidgearL.angularVelocity += vert2.right * initforce;
            rigidgearL.angularVelocity -= initforce;

            pushcounter = 2;
        }
        else if (pushcounter == 2)
        {
            //rigidgearL.AddExplosionForce(explosionForce, vert1.position, explosionradius, 0, ForceMode.Impulse);
            //rigidgearR.AddForceAtPosition(vert1.forward * initforce, vert1.position, ForceMode.Impulse);
            //rigidgearL.AddForce(vert5.right * initforce, ForceMode2D.Impulse);
            //rigidgearL.angularVelocity += vert1.right * initforce;
            rigidgearL.angularVelocity -= initforce;
            pushcounter = 0;
        }
        pushcounter++;












        /*//transform.Rotate(Vector3.up, Time.deltaTime, Space.World);
        if (pushcounter == 0)
        {
            //rigidgearL.AddExplosionForce(explosionForce, vert3.position, explosionradius, 0, ForceMode.Impulse);// vert3.forward * initforce, ForceMode.Force);
            //rigidgearL.AddForceAtPosition(vert3.forward * initforce, vert3.position, ForceMode.Impulse);
            rigidgearL.AddForce(vert3.forward * initforce, ForceMode2D.Force);
            //rigidgearL.angularVelocity += vert3.forward * initforce;
            pushcounter = 1;
        }
        else if(pushcounter == 1)
        {
            //rigidgearL.AddExplosionForce(explosionForce, vert2.position, explosionradius, 0, ForceMode.Impulse);
            //rigidgearL.AddForceAtPosition(vert2.forward * initforce, vert2.position, ForceMode.Impulse);
            rigidgearL.AddForce(vert2.forward * initforce, ForceMode2D.Impulse);
            //rigidgearL.angularVelocity += vert2.right * initforce;
            //rigidgearL.angularVelocity += vert2.forward * initforce;

            pushcounter = 2;
        }
        else if (pushcounter == 2)
        {
            //rigidgearL.AddExplosionForce(explosionForce, vert1.position, explosionradius, 0, ForceMode.Impulse);
            //rigidgearR.AddForceAtPosition(vert1.forward * initforce, vert1.position, ForceMode.Impulse);
            rigidgearL.AddForce(vert1.forward * initforce, ForceMode2D.Impulse);
            //rigidgearL.angularVelocity += vert1.right * initforce;
            //rigidgearL.angularVelocity += vert1.forward * initforce;
            pushcounter = 3;
        }
        else if (pushcounter == 3)
        {
            //rigidgearL.AddExplosionForce(explosionForce, vert0.position, explosionradius, 0, ForceMode.Impulse);
            //rigidgearL.AddForceAtPosition(vert0.forward * initforce, vert0.position, ForceMode.Impulse);
            rigidgearL.AddForce(vert0.forward * initforce, ForceMode2D.Impulse);
            //rigidgearL.angularVelocity += vert0.right * initforce;
            //rigidgearL.angularVelocity += vert0.forward * initforce;
            pushcounter = 4;
        }
        else if (pushcounter == 4)
        {
            //rigidgearL.AddExplosionForce(explosionForce, vert5.position, explosionradius, 0, ForceMode.Impulse);
            //rigidgearR.AddForceAtPosition(vert5.forward * initforce, vert5.position, ForceMode.Impulse);
            rigidgearL.AddForce(vert5.forward * initforce, ForceMode2D.Impulse);
            //rigidgearL.angularVelocity += vert5.right * initforce;
            //rigidgearL.angularVelocity += vert5.forward * initforce;
            pushcounter = 5;
        }
        else if (pushcounter == 5)
        {
            //rigidgearL.AddExplosionForce(explosionForce, vert4.position, explosionradius, 0, ForceMode.Impulse);
            //rigidgearL.AddForceAtPosition(vert4.forward * initforce, vert4.position, ForceMode.Impulse);
            rigidgearL.AddForce(vert4.forward * initforce, ForceMode2D.Impulse);
            //rigidgearL.angularVelocity += vert4.right * initforce;
            //rigidgearL.angularVelocity += vert4.forward * initforce;
            pushcounter = 0;
        }
        pushcounter++;*/




        /*
        if (gearturncount >= maxcounter)
        {
            //rigid.AddForce(poinToAddImpulse.up * force, ForceMode.Impulse);

            rigid.angularVelocity += poinToAddImpulse.right * force;

            gearturncount = 0;
        }
        gearturncount++;*/


    }


    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("colliding");
        /*rigid.angularVelocity += this.transform.right * continuousforce;

        if (gearturncount >= maxcounter)
        {
            //rigid.AddForce(poinToAddImpulse.up * force, ForceMode.Impulse);

            //rigid.angularVelocity += poinToAddImpulse.right * continuousforce;

            gearturncount = 0;
        }
        gearturncount++;*/
    }
}
