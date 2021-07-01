using UnityEngine;
using System.Collections;

public class swordtest : MonoBehaviour
{
    public GameObject sword;
    public GameObject target;
    public GameObject hand;
    public float speed = 10;
    public float rotate_speed = 10;
    public float max_distance = 1f;

    void Start()
    {
        sword = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sword.transform.localScale = new Vector3(0.1f, 0.5f, 2f);
        GameObject sharpside = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sharpside.transform.localScale = new Vector3(0.1f, 0.1f, 2f);
        sharpside.transform.position = new Vector3(0, -0.25f, 0);
        sharpside.GetComponent<Renderer>().material.color = Color.red;
        sharpside.transform.parent = sword.transform;
        target = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        target.transform.localScale = new Vector3(0.2f, 1, 0.2f);
        target.GetComponent<Renderer>().material.color = Color.yellow;
        hand = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        hand.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        hand.transform.position = new Vector3(0, 0, -1f);
        hand.GetComponent<Renderer>().material.color = Color.green;
        sword.transform.parent = hand.transform;
    }
    void Update()
    {
        Vector3 mov = new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * speed, Input.GetAxis("Mouse Y") * Time.deltaTime * speed, 0);
        hand.transform.position += mov;
        Vector3 clamped = hand.transform.position;
        clamped.x = Mathf.Clamp(clamped.x, target.transform.position.x - max_distance, target.transform.position.x + max_distance);
        clamped.y = Mathf.Clamp(clamped.y, target.transform.position.y - max_distance, target.transform.position.y + max_distance);
        hand.transform.position = clamped;
        Quaternion rot = new Quaternion();
        rot.SetLookRotation(sword.transform.forward, -(target.transform.position - hand.transform.position).normalized);
        sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, rot, rotate_speed * Time.deltaTime);
    }
}






