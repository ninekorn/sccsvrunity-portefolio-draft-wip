using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.VR;

public class copyRotation : MonoBehaviour {

    public GameObject parent;
   // public GameObject positionObject;
    //public GameObject centerEye;
    //public GameObject BaseOfCharacter;

    void Start()
    {

    }

    void Update()
    {
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

        transform.localEulerAngles = parent.transform.rotation.eulerAngles;


        //to retest
        /*
        Quaternion removingTranslation = transform.localRotation;

        Matrix4x4 someMatrix = new Matrix4x4();

        //someMatrix.rot
        Matrix4x4 getMatrix = Matrix4x4.Rotate(removingTranslation);
        getMatrix.m30 = 0;
        getMatrix.m31 = 0;
        getMatrix.m32 = 0;
        getMatrix.m33 = 1;
        Quaternion quatter = SCCoreSystems.sc_maths.QuaternionFromMatrix(getMatrix);
        transform.rotation = quatter;*/













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
