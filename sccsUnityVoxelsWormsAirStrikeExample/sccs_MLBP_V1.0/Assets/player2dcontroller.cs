using System.Collections;
using System.Collections.Generic;
using UnityEngine;







public class player2dcontroller : MonoBehaviour 
{
	public Transform playerdrone;
	public Rigidbody rigidplayer;
	public float force = 10;
    public float forcethrusterRL = 10;
    public float forcethrusterangular = 10;

    public float smooth = 5.0f;
	float tiltAngle = 60f;

    public Transform thrusterR;
    public Transform thrusterL;

    public Vector3 lastframeVelocity = Vector3.zero;

    void Start ()
	{
        rigidplayer = playerdrone.gameObject.GetComponent<Rigidbody>();
    }

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.W))
		{
			//rigidbullet.useGravity = false;
			rigidplayer.AddForce(playerdrone.transform.up * force, ForceMode.Impulse);
            //rigidplayer.AddForceAtPosition(thrusterR.transform.up * force, thrusterR.transform.position);
            //rigidplayer.AddForceAtPosition(thrusterR.transform.up * force, thrusterL.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.S))
		{
			rigidplayer = playerdrone.gameObject.GetComponent<Rigidbody>();
			rigidplayer.isKinematic = false;
			//rigidbullet.useGravity = false;
			rigidplayer.AddForce(-playerdrone.transform.up * force, ForceMode.Impulse);
            //rigidplayer.AddForceAtPosition(-thrusterR.transform.up* force, thrusterR.transform.position);
            //rigidplayer.AddForceAtPosition(-thrusterR.transform.up * force, thrusterL.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.A))
		{
            //rigidbullet.useGravity = false;
            //rigidplayer.AddForce(-gunEnd.transform.right * force, ForceMode.Force);
            //float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
            //float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

            //Quaternion target = Quaternion.Euler(tiltAroundX,0,tiltAroundZ);
            //rigidplayer.rotation = Quaternion.Slerp(rigidplayer.rotation, rigidplayer.rotation, Time.deltaTime * smooth); //,target,Time.deltaTime*smooth
            //rigidplayer.MoveRotation(Time.deltaTime * smooth);
            rigidplayer.angularVelocity += playerdrone.transform.forward * forcethrusterRL * Time.deltaTime * smooth;// (, ForceMode.Force);

        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            //rigidbullet.useGravity = false;
            //rigidplayer.AddForce(-gunEnd.transform.right * force, ForceMode.Force);

            //float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
            //float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
            //rigidplayer.MoveRotation(-Time.deltaTime * smooth);
            //Quaternion target = Quaternion.Euler(tiltAroundX,0,tiltAroundZ);
            //rigidplayer.rotation = Quaternion.Slerp(rigidplayer.rotation,target,-Time.deltaTime*smooth);
            //rigidplayer.angularVelocity -= forcethrusterangular;
            rigidplayer.angularVelocity += -playerdrone.transform.forward * forcethrusterRL * Time.deltaTime * smooth;
            // (, ForceMode.Force);        
        }

        if (Input.GetKeyDown(KeyCode.Q))
		{
			rigidplayer.AddForce(-playerdrone.right * force, ForceMode.Impulse);
		}

		if(Input.GetKeyDown(KeyCode.E))
		{
			rigidplayer.AddForce(playerdrone.right * force, ForceMode.Impulse);
		}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidplayer.velocity = Vector3.zero;
            rigidplayer.angularVelocity = Vector3.zero;
            rigidplayer.angularDrag = 0;
        }









        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rigidplayer.AddForce(-playerdrone.right * force, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rigidplayer.AddForce(playerdrone.right * force, ForceMode.Impulse);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
			rigidplayer.AddForce(playerdrone.transform.up * force, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rigidplayer.AddForce(-playerdrone.transform.up * force, ForceMode.Impulse);
        }













    }
}








