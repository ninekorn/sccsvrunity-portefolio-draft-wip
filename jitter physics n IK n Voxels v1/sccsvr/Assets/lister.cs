using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lister : MonoBehaviour
{

    //public static listing currentlister;

    public static List<GameObject> fragments = new List<GameObject>();

    List<Vector3> tempVerts = new List<Vector3>();

    public Transform player;
    GameObject frag;
    int counter = 0;
    Vector3 yo;


    bool recentActivation = false;

    void Start()
    {


    }


    void Update()
    {
        //Debug.Log(fragments.Count);

        for (int i = 0; i < fragments.Count; i++)
        {
            frag = fragments[i];

            if (Vector3.Distance(player.transform.position, frag.transform.position) <= 5f)
            {

                //Debug.DrawRay(frag.transform.position, Vector3.up * 10, Color.green, 1000f);
                if (frag.transform.parent.GetComponent<reparator>().objToReact.Contains(frag.transform.gameObject))
                {
                    frag.transform.parent.GetComponent<reparator>().objToReact.Remove(frag.transform.gameObject);
                }
                
                if (frag.transform.parent.GetComponent<reparator>().filterator.Contains(frag.transform.gameObject.GetComponent<MeshFilter>()))
                {
                    frag.transform.parent.GetComponent<reparator>().filterator.Remove(frag.transform.gameObject.GetComponent<MeshFilter>());
                }

               
                frag.transform.gameObject.SetActive(true);
                frag.transform.parent.GetComponent<reparator>().counter = 1;
                recentActivation = true;
            }
            else
            {
                if (!frag.transform.parent.GetComponent<reparator>().objToReact.Contains(frag.transform.gameObject))
                {
                    frag.transform.parent.GetComponent<reparator>().objToReact.Add(frag.transform.gameObject);
                }

                if (!frag.transform.parent.GetComponent<reparator>().filterator.Contains(frag.transform.gameObject.GetComponent<MeshFilter>()))
                {
                    frag.transform.parent.GetComponent<reparator>().filterator.Add(frag.transform.gameObject.GetComponent<MeshFilter>());
                }


                frag.transform.parent.GetComponent<reparator>().counter = 1;
                frag.transform.gameObject.SetActive(false);
                recentActivation = false;
            }



            /*if (recentActivation == true && Vector3.Distance(player.transform.position, frag.transform.position) <= 5f)
            {
                frag.transform.parent.GetComponent<reparator>().counter = 0;
            }
            */



            /*if (Vector3.Distance(player.transform.position, frag.transform.position) > 5f && recentActivation == true)
            {
                Debug.DrawRay(frag.transform.position, Vector3.up * 20, Color.red, 1000f);

                if (!frag.transform.parent.GetComponent<reparator>().objToReact.Contains(frag.transform.gameObject))
                {
                    frag.transform.parent.GetComponent<reparator>().objToReact.Add(frag.transform.gameObject);
                }

                if (!frag.transform.parent.GetComponent<reparator>().filterator.Contains(frag.transform.gameObject.GetComponent<MeshFilter>()))
                {
                    frag.transform.parent.GetComponent<reparator>().filterator.Add(frag.transform.gameObject.GetComponent<MeshFilter>());
                }

                            
                frag.transform.parent.GetComponent<reparator>().counter = 1;
                frag.transform.gameObject.SetActive(false);
                recentActivation = false;
            }*/
            
            /*else if (recentActivation == false && Vector3.Distance(player.transform.position, frag.transform.position) > 5f)
            {
                frag.transform.parent.GetComponent<reparator>().counter = 0;
            }*/







        }
    }
}
