using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.VR;

public class sccsIKSetTargetPosition : MonoBehaviour {


    public int someTypeOfIKSetup = 0;
    public GameObject target;
    public GameObject targetTwo;
    //public GameObject positionObject;
    //public GameObject centerEye;
    //public GameObject BaseOfCharacter;

    //public Transform footTarget;
    //public Transform upperleg;
    //public Transform lowerleg;
    //public Transform foot;
    //public Transform legstaticpivot;

    Vector3 originDirection = Vector3.zero;
    float originDirectionLength = 0;
    Vector3 initialPivotPosition = Vector3.zero;
    Vector3 lastFramePosition = Vector3.zero;

    int counterForSetPos = 0;
    public int counterForSetPosMax = 1;
    int counterForSetPosSwtc = 0;

    public float touchdowndistance = 0.25f;
    Vector3 lastPosition;


    void Start()
    {       
        initialPivotPosition = this.transform.position;
        //lastFramePosition = footTarget.position;
        originDirection = initialPivotPosition - target.transform.position;
        originDirectionLength = originDirection.magnitude;
        originDirection.Normalize();
    }



    private void Update()
    {
        if (someTypeOfIKSetup == 0)
        {
            transform.position = target.transform.position;
        }
        else if (someTypeOfIKSetup == 1)
        {
            var mag = (targetTwo.transform.position - transform.position).magnitude;

            transform.position = target.transform.position + (originDirection * mag);
        }

        /*var distToTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distToTarget < touchdowndistance)
        {
            transform.position = lastPosition;
        }*/


        /*if (counterForSetPos >= counterForSetPosMax)
        {
            
            counterForSetPos = 0;
        }
        counterForSetPos++;*/


        /*float distanceToTarget = Vector3.Distance(transform.position,target.transform.position);
        if (distanceToTarget <= touchdowndistance)
        {
            Debug.Log("touchdown penetration");
            //transform.position = lastPosition;
        }*/




        lastPosition = target.transform.position;
        //transform.position = parent.transform.position;
        // + (originDirection * originDirectionLength);
    }

    void SomeUpdate()
    {

        //transform.eulerAngles = parent.transform.rotation.eulerAngles;




        /*root.parent = transform;
        root.localScale = Vector3.one;

        //to readd if something bugs. steve chassé aka ninekorn notes.
        //root.localPosition = Vector3.zero;
        //to readd if something bugs. steve chassé aka ninekorn notes.

        root.localRotation = Quaternion.identity;
        */




        /*Vector3 prevPos = transform.position;
        Quaternion prevRot = transform.rotation;
        transform.localPosition = Vector3.zero;*/


        //transform.rotation = Quaternion.Euler(0.0f, parent.transform.rotation.eulerAngles.y, 0.0f);
        //transform.position = prevPos;
        //transform.rotation = prevRot;









        ///Vector3 parentPosition = parent.transform.position;
        //Vector3 currentPosition = transform.position;
        //currentPosition.y = parentPosition.y;


        //transform.position = currentPosition;
        //transform.localEulerAngles = parent.transform.eulerAngles;
        //transform.rotation = parent.transform.rotation;








        /*Vector3 offSet = BaseOfCharacter.transform.position - centerEye.transform.position;
        Vector3 currentPosition = transform.position;
        float yOffset = offSet.y;

        Vector3 basePositionOfHmd = centerEye.transform.position;
        basePositionOfHmd.y += yOffset;
        currentPosition.y += yOffset;*/


















        /*if (Vector3.Distance(basePositionOfHmd,BaseOfCharacter.transform.position) > 0.5f)
        {
            Vector3 direction = basePositionOfHmd - currentPosition;
            transform.Translate(direction);
            //Debug.Log("you are trying to move");
        }*/




        //Instantiate(positionObject, basePositionOfHmd, Quaternion.identity);





        //transform.rotation = parent.transform.rotation;
        //OVRPose poser = OVRManager.tracker.GetPose();
        //Vector3 poserPosition = poser.position;
        //Instantiate(positionObject, poserPosition + transform.position, Quaternion.identity);
        //Vector3 currentPositionOfCenterEye = centerEye.transform.position;
        //Vector3 transformPosition = transform.position;
        //Vector3 offset = currentPositionOfCenterEye - transformPosition;
        //Vector3 wantedPosition = centerEye.transform.position + offset;
        //wantedPosition.y -= transformPosition.y;
        //Instantiate(positionObject, currentPositionOfCenterEye, Quaternion.identity);
        //Vector3 pos = OVRManager.tracker.GetPose(3).position - InputTracking.GetLocalPosition(VRNode.CenterEye) + posOffset;
        //pos = OVRManager.tracker.GetPose(3).position - InputTracking.GetLocalPosition(VRNode.CenterEye);
        //transform.localPosition = pos;
        //Vector3 positionofEye = InputTracking.GetLocalPosition(VRNode.CenterEye);
        //Instantiate(positionObject, positionofEye, Quaternion.identity);


        /*UInt32 componentCount = CAPI.ovrAvatarComponent_Count(sdkAvatar);
        HashSet<string> componentsThisRun = new HashSet<string>();
        for (UInt32 i = 0; i < componentCount; i++)
        {
            IntPtr ptr = CAPI.ovrAvatarComponent_Get_Native(sdkAvatar, i);
            ovrAvatarComponent component = (ovrAvatarComponent)Marshal.PtrToStructure(ptr, typeof(ovrAvatarComponent));
            componentsThisRun.Add(component.name);
            //if (!trackedComponents.ContainsKey(component.name))
            //{
                GameObject componentObject = null;
                Type specificType = null;
                if ((Capabilities & ovrAvatarCapabilities.Base) != 0)
                {
                    ovrAvatarBaseComponent? baseComponent = CAPI.ovrAvatarPose_GetBaseComponent(sdkAvatar);
                    if (baseComponent.HasValue && ptr == baseComponent.Value.renderComponent)
                    {
                        specificType = typeof(OvrAvatarBase);
                        if (Base != null)
                        {
                            componentObject = Base.gameObject;
                        }
                    }
                }
            //}
        }*/
    }
}
