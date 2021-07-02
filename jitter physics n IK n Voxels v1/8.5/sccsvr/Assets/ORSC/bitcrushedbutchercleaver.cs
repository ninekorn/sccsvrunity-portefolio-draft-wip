using UnityEngine;
using System.Collections;

public class bitcrushedbutchercleaver : MonoBehaviour
{
    public GameObject thislimb;
    public GameObject thislimbstarget;
    public GameObject pivotofthislimb;

    public float speed = 10;
    public float rotate_speed = 10;

    public float max_distance = 1f;

    void Start()
    {
        //sword = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //sword.transform.localScale = new Vector3(0.1f, 0.5f, 2);

        //GameObject sharpside = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //sharpside.transform.localScale = new Vector3(0.1f, 0.1f, 2);
        //sharpside.transform.position = new Vector3(0, -0.25f, 0);
        //sharpside.GetComponent<MeshRenderer>().material.color = Color.red;
        //sharpside.transform.parent = sword.transform;

        //target = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //target.transform.localScale = new Vector3(0.2f, 1, 0.2f);
        //sharpside.GetComponent<MeshRenderer>().material.color = Color.yellow;

        //hand = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //hand.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //hand.transform.position = new Vector3(0, 0, -1f);
        //sharpside.GetComponent<MeshRenderer>().material.color = Color.green;

        //sword.transform.parent = hand.transform;
    }

    void Update()
    {

        //Vector3 mov = new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * speed, Input.GetAxis("Mouse Y") * Time.deltaTime * speed, 0);

        //hand.transform.position += mov;

        Vector3 clamped = pivotofthislimb.transform.position;
        clamped.x = Mathf.Clamp(clamped.x, thislimbstarget.transform.position.x - max_distance, thislimbstarget.transform.position.x + max_distance);
        clamped.y = Mathf.Clamp(clamped.y, thislimbstarget.transform.position.y - max_distance, thislimbstarget.transform.position.y + max_distance);

        pivotofthislimb.transform.position = clamped;

        Quaternion rot = new Quaternion();
        rot.SetLookRotation(thislimb.transform.forward, -(thislimbstarget.transform.position - pivotofthislimb.transform.position).normalized);
        thislimb.transform.rotation = Quaternion.Lerp(thislimb.transform.rotation, rot, rotate_speed * Time.deltaTime);

    }
}

