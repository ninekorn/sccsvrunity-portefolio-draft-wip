using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceDistribution : MonoBehaviour
{
    //i got no clue wtf im doing anyway

    public bool hasCollided = false;


    public GameObject forceDist;
    Rigidbody rigid;

    public Vector3 currentVelocityAtCollision;
    public Vector3 currentImpactOfCollision;
   


    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (hasCollided)
        {
            Vector3 forceAppliedToRigid = (currentVelocityAtCollision).normalized;
            Vector3 directionOfImpactRelativeToCenterOfMass = (currentImpactOfCollision - transform.position);

            Debug.DrawLine(transform.position, (forceAppliedToRigid - directionOfImpactRelativeToCenterOfMass), Color.red,1000f);
            //transform.position += forceAppliedToRigid- impactLocationToDivertForceTo;
            hasCollided = false;
        }
    }
}






/*for (float x = transform.position.x - 0.025f; x < transform.position.x + 0.025f; x+=0.01f)
{
    for (float y = transform.position.y - 0.025f; y < transform.position.y+0.025f; y+= 0.01f)
    {
        for (float z = transform.position.z - 0.025f; z < transform.position.z+0.025f; z+= 0.01f)
        {
            Instantiate(forceDist,new Vector3(x, y, z)+ transform.position , Quaternion.identity);
        }
    }
}*/
