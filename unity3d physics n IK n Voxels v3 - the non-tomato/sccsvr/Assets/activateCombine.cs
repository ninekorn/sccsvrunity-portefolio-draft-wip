using UnityEngine;
using System.Collections;

public class activateCombine : MonoBehaviour {

    Transform parent;

    int counter = 0;
    float timer1 = 0;


	void Start ()
    {
        parent = this.transform.parent;

        timer1 = 2;
        //this.transform.parent = null;

    }
	

	void Update ()
    {
        timer();
        if (counter == 1)
        {
            if (this.transform.gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {


                parent.GetComponent<reparator>().objToReact.Add(this.transform.gameObject);
                
                parent.GetComponent<reparator>().initializereparator = 1;

                lister.fragments.Add(this.transform.gameObject);


                //parent.GetComponent<reparator>().objToReact.Add(FracturingObj[i]);
                
                this.transform.gameObject.SetActive(false);
                Destroy(this);
                /*if (transform.gameObject.GetComponent<combine>() == null)
                {
                    transform.gameObject.AddComponent<combine>().enabled = true;
                }
                else
                {
                    transform.gameObject.GetComponent<combine>().enabled = true;
                }*/

                counter = 0;
            }
        }   
    }

    void timer()
    {

        if (timer1 <= 0)
        {
            timer1 = 0;
        }

        if (timer1 == 0)
        {

            counter = 1;

        }
        if (counter != 1)

        {
            if (timer1 != 0)
            {
                timer1 -= Time.deltaTime;
            }
        }

    }






}
