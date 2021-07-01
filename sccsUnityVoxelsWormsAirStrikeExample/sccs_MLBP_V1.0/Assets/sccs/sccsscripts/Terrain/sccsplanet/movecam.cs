using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movecam : MonoBehaviour
{
    float yaw = 0;
    float pitch = 0;

    float speedH = 2.0f;
    float speedV = 2.0f;

    public Transform target;


    void Start () {
		
	}

    float posY = 0.0f;


    int swtchRotate = 0;

	// Update is called once per frame
	void Update ()
    {

        /*//https://www.youtube.com/watch?v=lYIRm4QEqro
        if (Input.GetMouseButton(1))
        {
            //Debug.Log("Pressed secondary button.");

            yaw += speedH * Input.GetAxis("Mouse X");
            pitch += speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }*/

        Vector3 tempPos = transform.position;
        tempPos.y = posY;
        transform.position = tempPos;


        transform.LookAt(target);

        if (Input.GetKey(KeyCode.A))
        {
            //transform.gameObject.GetComponent<Rigidbody>().free
            //transform.Translate(Vector3.left * Time.deltaTime);
            transform.RotateAround(target.position, transform.up, 20 * Time.deltaTime);
            if (swtchRotate == 0)
            {
                swtchRotate = 1;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.right * Time.deltaTime);
            transform.RotateAround(target.position, -transform.up, 20 * Time.deltaTime);
            if (swtchRotate == 0)
            {
                swtchRotate = 1;
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {

            //transform.Translate(Vector3.up * Time.deltaTime);
            transform.RotateAround(target.position, transform.right, 20 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(Vector3.down * Time.deltaTime);
            transform.RotateAround(target.position, -transform.right, 20 * Time.deltaTime);
        }




        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            swtchRotate = 0;
        }









        if (swtchRotate == 1)
        {
            posY = transform.position.y;
            swtchRotate = 2;
        }
        else if(swtchRotate == 0)
        {
            posY = transform.position.y;
        }
    }
}
