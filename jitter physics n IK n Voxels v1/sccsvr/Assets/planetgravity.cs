using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

using RigidBody = Jitter.Dynamics.RigidBody;

public class planetgravity : MonoBehaviour {

    public float gravity = -1; //-9.81f
    public float gravitymul = 1.0f;

    public Transform thisplanet;
    Jitter.DataStructures.ReadOnlyHashset<Jitter.Dynamics.RigidBody> Rigidbodies;
    Thread gravitythread;

    // Use this for initialization
    void Start ()
    {
       
    }



    // Update is called once per frame
    void Update ()
    {

    }

    void LateUpdate()
    {
        Rigidbodies = JPhysics.world.RigidBodies;
        IEnumerable someHashSet = JPhysics.world.RigidBodies;

        var someEnum = someHashSet.GetEnumerator();

        while (someEnum.MoveNext())
        {
            RigidBody rigid = (RigidBody)someEnum.Current;

            if (!rigid.IsStatic)
            {
                Vector3 directiontoplanetcenter = thisplanet.position - JitterExtensions.ToVector3(rigid.Position);

                var forceDir = (JitterExtensions.ToJVector(thisplanet.position) - rigid.Position) * -1;
                forceDir.Normalize();

                rigid.AddForce(forceDir * gravity * gravitymul);
            }
        }

        /*if (!rigid.GetComponent<Rigidbody>().isKinematic)
          {
              Vector3 directiontoplanetcenter = thisplanet.position - rigid.gameObject.transform.position;
              rigid.AddForce(-(thisplanet.position - rigid.gameObject.transform.position).normalized * gravity * gravitymul, ForceMode.Force);
          }
          else
          {
              //increase force on hinge but still make sure that it follows the laws of physics and instead of fucking up the whole thing, we will calculate the ship as a whole with its total rigidbodies later.

          }*/
        /*
        for (int Ri = 0; Ri < JPhysics.world.RigidBodies.Count; Ri++)
        {

            //JPhysics.world.RigidBodies[ri]


            /*if (!Rigidbodies[Ri].GetComponent<Rigidbody>().isKinematic)
            {
                Vector3 directiontoplanetcenter = thisplanet.position - Rigidbodies[Ri].gameObject.transform.position;
                Rigidbodies[Ri].AddForce(-(thisplanet.position - Rigidbodies[Ri].gameObject.transform.position).normalized * gravity * gravitymul, ForceMode.Force);
            }
            else
            {
                //increase force on hinge but still make sure that it follows the laws of physics and instead of fucking up the whole thing, we will calculate the ship as a whole with its total rigidbodies later.

            }
        }*/
        /*Rigidbodies = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

        for (int Ri = 0; Ri < Rigidbodies.Length; Ri++)
        {
            if (!Rigidbodies[Ri].GetComponent<Rigidbody>().isKinematic)
            {
                Vector3 directiontoplanetcenter = thisplanet.position - Rigidbodies[Ri].gameObject.transform.position;
                Rigidbodies[Ri].AddForce(-(thisplanet.position - Rigidbodies[Ri].gameObject.transform.position).normalized * gravity * gravitymul, ForceMode.Force);
            }
            else
            {
                //increase force on hinge but still make sure that it follows the laws of physics and instead of fucking up the whole thing, we will calculate the ship as a whole with its total rigidbodies later.

            }
        }*/

        //Jitter.sce


    }



    /*public void DoWork()
    {
        int threadcancel = 0;

        for (int i = 0; i < 1; i++)
        {
        threadloop:
            for (int Ri = 0; Ri < Rigidbodies.Length; Ri++)
            {
                Vector3 directiontoplanetcenter = thisplanet.position - Rigidbodies[Ri].gameObject.transform.position;

                Rigidbodies[Ri].AddForce((thisplanet.position - Rigidbodies[Ri].gameObject.transform.position).normalized * -9.81f, ForceMode.Force);


            }



            /*Rigidbody[] Rigidbodies = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];
            for (int x = 0; x < Rigidbodies.Length; x++)
            {
                //yield return new WaitForSeconds(0);
                for (int y = 0; y < Rigidbodies.Length; y++)
                {
                    if (x != y)
                    {
                        Rigidbodies[x].AddForce((Rigidbodies[y].gameObject.transform.position - Rigidbodies[x].transform.position) * (Rigidbodies[y].mass / Vector3.Distance(Rigidbodies[x].transform.position, Rigidbodies[y].transform.position)));
                    }
                }
            }
            Thread.Sleep(0);
            if (threadcancel == 0)
            {
                goto threadloop;
            }
            else
            {

            }
   
        }
    }*/

    // Update is called once per frame
    void Awake()
    {
        /*//StartCoroutine(Routine());
        gravitythread = new Thread((tester0000) =>
        {
            DoWork();
            
        }, 0); //100000 //999999999

        gravitythread.IsBackground = false;
        gravitythread.SetApartmentState(ApartmentState.STA);
        gravitythread.Start();*/
    }

}
