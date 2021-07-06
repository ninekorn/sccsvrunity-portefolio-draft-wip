using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsFragmentCollider : MonoBehaviour {

    MeshCollider meshCol;

    Rigidbody rigid;

    // Use this for initialization
    void Awake ()
    {
        meshCol = this.gameObject.AddComponent<MeshCollider>();
        meshCol.convex = true;
        meshCol.enabled = false;

        rigid = this.gameObject.AddComponent<Rigidbody>();

    }
    /*
    void Start()
    {
        meshCol = this.gameObject.AddComponent<MeshCollider>();
        meshCol.convex = true;
        meshCol.enabled = true;

    }*/



    int counterForInitActivateCollider = 0;
    int counterForInitActivateColliderMax = 2;
    int counterForInitActivateColliderSwtc = 0;
    public float explosionForce = 100;

    // Update is called once per frame
    void Update ()
    {
        if (counterForInitActivateColliderSwtc == 0)
        {
            if (counterForInitActivateCollider >= counterForInitActivateColliderMax)
            {
                if (!meshCol.enabled)
                {
                    rigid.AddExplosionForce(explosionForce, this.transform.position, 10, 10);
                    meshCol.enabled = true;
                }
                counterForInitActivateColliderSwtc = 1;
                counterForInitActivateCollider = 0;
            }
           
            counterForInitActivateCollider++;
        }
       
	}
}
