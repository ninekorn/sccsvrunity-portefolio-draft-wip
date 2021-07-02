using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCCoreSystems;

public class footTarget : MonoBehaviour
{

    public Transform upperleg;
    public Transform lowerleg;
    public Transform foot;
    public Transform foottarget;
    public Transform legstaticpivot;

    float upperleglength = 0;
    float lowerleglength = 0;
    float footlength = 0;
    float totallegLength = 0;

    public LayerMask layerMask;

    Vector3 IdleStandingTargetPositionVariableLength;
    Vector3 IdleStandingTargetPositionMax;
    Vector3 IdleStandingTargetPositionMin;
    // Use this for initialization
    void Start()
    {
        upperleglength = upperleg.localScale.z;
        lowerleglength = lowerleg.localScale.z;
        footlength = foot.localScale.z;
        totallegLength = upperleglength + lowerleglength + footlength;

        IdleStandingTargetPositionMax = transform.position + ((transform.forward * upperleglength) + (transform.forward * lowerleglength) + (transform.forward * footlength));
        IdleStandingTargetPositionMin = transform.position + ((transform.forward * (upperleglength * 0.5f)) + (transform.forward * (lowerleglength * 0.5f)) + (transform.forward * (footlength * 0.5f)));
    }


    // Update is called once per frame
    void Update()
    {
        //the IdleStandingTargetPositionMax position is locked in the forward direction of the leg static pivot. Leg is stretched
        //foottarget.transform.position

        //the IdleStandingTargetPositionMin position is locked in the forward direction of the leg static pivot but at half the length. Leg is bent as if the character has a crouching pose.

        var ray = new Ray(legstaticpivot.position, transform.forward);

        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 3, Color.green, 0.001f);

        if (Physics.Raycast(ray, out hit, totallegLength*1.25f, layerMask))
        {
            if (hit.transform.tag == "collisionObject")
            {
                Vector3 tempDir = legstaticpivot.position- foottarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                //IdleStandingTargetPositionVariableLength

                if (tempDir.magnitude > totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                {
                    foottarget.position = IdleStandingTargetPositionMax;
                    /*tempDir.Normalize();
                    var somePosOfSHLDR = new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    Vector3 tempVect = somePosOfSHLDR + (tempDir * ((lengthOfLowerArmRight * 1.0923f) + (lengthOfUpperArmRight * 1.0923f)));
                    MOVINGPOINTER.X = tempVect.X;
                    MOVINGPOINTER.Y = tempVect.Y;
                    MOVINGPOINTER.Z = tempVect.Z;*/
                }
                else if (tempDir.magnitude < (totallegLength * 0.5f))
                {
                    foottarget.position = IdleStandingTargetPositionMin;
                }
                else
                {
                    foottarget.position = hit.point;
                }
            }












            /*if (hit.transform.tag == "collisionObject")
            {

            }*/

            //sc_maths.ClampValue();
            //footTarget.transform.position = hit.point;
            /*if (hit.transform.tag == "collisionObject")
            {
                if (GetComponent<Fracture4>() != null)
                {

                }
                else
                {

                }
            }*/
        }

        
    }
}
