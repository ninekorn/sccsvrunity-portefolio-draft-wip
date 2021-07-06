using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oppositeForce : MonoBehaviour {

    private Rigidbody rigid;
    Vector3 previousPos;
    Vector3 movementThisFrame;
    public GameObject middleHip;
    float lastFrameVelocity;
    public float weight;

    public Transform thisplanet;

    public float gravity = -9.81f;
    public float gravitymul = 10.0f;

    void Start ()
    {
        rigid = GetComponent<Rigidbody>();
        rigid = this.gameObject.GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
        movementThisFrame = transform.position - previousPos;



        //float currentVelocity = rigid.velocity.magnitude;
        //float Gforce = (currentVelocity - lastFrameVelocity) / (Time.deltaTime * Physics.gravity.magnitude);

        //float Gforce = (transform.position.y/weight) / (Time.deltaTime * Physics.gravity.magnitude);

        //Debug.Log(Gforce);

        /*if (transform.position != previousPos)
        {
            Debug.Log("applying Force");
            Debug.DrawRay(transform.position,-movementThisFrame,Color.blue,0.1f);
            float mag = movementThisFrame.magnitude;
            rigid.AddForceAtPosition(-movementThisFrame * mag*100, transform.position);
            rigid.AddForce(-movementThisFrame * mag * 100, ForceMode.Impulse);
        }*/
        //lastFrameVelocity = currentVelocity;
        previousPos = transform.position;
	}



    void FixedUpdate()
    {
        var simGrav = (thisplanet.position - rigid.gameObject.transform.position).normalized * gravity * gravitymul;

        //var gdir = Physics.gravity.normalized;
        var d = Vector3.Dot(rigid.velocity, simGrav);
        d = Mathf.Max(0f, d); //if we don't clamp this we'd also remove upward velocities, if you want to do that as well... comment out this line
        d += simGrav.magnitude * Time.deltaTime;
        rigid.AddForce(-d * simGrav.normalized * rigid.mass, ForceMode.Impulse);

        var testVel = -d * simGrav.normalized * rigid.mass;
        rigid.angularVelocity = testVel;


    }


}
