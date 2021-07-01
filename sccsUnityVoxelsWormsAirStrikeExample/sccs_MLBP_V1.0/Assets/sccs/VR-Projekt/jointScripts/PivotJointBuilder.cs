using UnityEngine;
using System.Collections;

public class PivotJointBuilder : MonoBehaviour
{

    public string pivotName = "pivot name";

    // RIGIDBODY SET UP
    public GameObject master;
    public GameObject slave;
    public float inertiaTensorValue = 50f;

    // INPUT
    public string keyboardInput = "LeftJoystickHorizontalButton";
    public string gamepadInput = "LeftJoystickHorizontal";
    public bool inverseInput = false;
    public float inputSpeed = 100f;

    // JOINT
    public AxisList whichAxis = AxisList.xAxis;
    public bool useLocalPositionAnchor = true;
    public Vector3 whichAnchor = Vector3.zero;

    public float thePositionSpring = 0;
    public float thePositionDamper = 100000000000000000000f;

    // LIMITS
    public bool isLimited = false;
    public float minLimitX = -45;
    public float maxLimitX = 45;
    public float limitY = 45;
    public float limitZ = 45;

    Transform master_T;
    Transform slave_T;

    void Start()
    {
        //setting inertia tensor so it won't be calculated from collider or default
        //high values seem to make joints less wobbly in direcions other then rotation direction
        Rigidbody masterRigidbody = master.GetComponent<Rigidbody>();
        masterRigidbody.inertiaTensor = new Vector3(inertiaTensorValue, inertiaTensorValue, inertiaTensorValue);
        masterRigidbody = master.AddComponent<Rigidbody>();

        Rigidbody slaveRigidbody = slave.GetComponent<Rigidbody>();
        slaveRigidbody.inertiaTensor = new Vector3(inertiaTensorValue, inertiaTensorValue, inertiaTensorValue);
        slaveRigidbody = slave.AddComponent<Rigidbody>();

        master_T = master.GetComponent<Transform>();
        slave_T = slave.GetComponent<Transform>();

        slave_T.parent = master_T;

        ConfigurableJoint _joint = master.AddComponent<ConfigurableJoint>();
        _joint.connectedBody = slave.GetComponent<Rigidbody>();

        // override the anchor with the localposition is specified
        if (useLocalPositionAnchor)
        {
            whichAnchor = slave_T.localPosition;
        }
        _joint.anchor = whichAnchor;

        _joint.xMotion = ConfigurableJointMotion.Locked;
        _joint.yMotion = ConfigurableJointMotion.Locked;
        _joint.zMotion = ConfigurableJointMotion.Locked;
        _joint.angularXMotion = ConfigurableJointMotion.Locked;
        _joint.angularYMotion = ConfigurableJointMotion.Locked;
        _joint.angularZMotion = ConfigurableJointMotion.Locked;

        var motion = ConfigurableJointMotion.Free;
        if (isLimited)
        {
            motion = ConfigurableJointMotion.Limited;
        }

        switch (whichAxis)
        {
            case AxisList.xAxis:
                _joint.angularXMotion = motion;

                //set drive for x axis
                JointDrive _jointXDrive = _joint.angularXDrive;
                _jointXDrive.positionSpring = thePositionSpring;
                _jointXDrive.positionDamper = thePositionDamper;
                _joint.angularXDrive = _jointXDrive;
                break;

            case AxisList.yAxis:
                whichAxis = AxisList.yAxis;
                _joint.angularYMotion = motion;

                //set drive for y and z axes
                JointDrive _jointYDrive = _joint.angularYZDrive;
                _jointYDrive.positionSpring = thePositionSpring;
                _jointYDrive.positionDamper = thePositionDamper;
                _joint.angularYZDrive = _jointYDrive;
                break;

            case AxisList.zAxis:
                whichAxis = AxisList.zAxis;
                _joint.angularZMotion = motion;

                //set drive for y and z axes
                JointDrive _jointZDrive = _joint.angularYZDrive;
                _jointZDrive.positionSpring = thePositionSpring;
                _jointZDrive.positionDamper = thePositionDamper;
                _joint.angularYZDrive = _jointZDrive;
                break;
        }

        if (isLimited) setLimitsAndSprings(ref _joint);

        PivotDrive pivotDrive = master.AddComponent<PivotDrive>();
        pivotDrive.KeyboardInputAxis = keyboardInput;
        pivotDrive.GamepadInputAxis = gamepadInput;
        pivotDrive.whichAxis = whichAxis;
        pivotDrive.speed = inputSpeed;
        pivotDrive.inverseInput = inverseInput;

        Destroy(this);
    }

    private void setLimitsAndSprings(ref ConfigurableJoint _joint)
    {
        //setting low angular x limit
        SoftJointLimit _lowAngularXLimit = _joint.lowAngularXLimit;
        _lowAngularXLimit.limit = minLimitX;
        _lowAngularXLimit.bounciness = Mathf.Infinity;
        _joint.lowAngularXLimit = _lowAngularXLimit;

        //setting high angular x limit
        SoftJointLimit _highAngularXLimit = _joint.highAngularXLimit;
        _highAngularXLimit.limit = maxLimitX;
        _highAngularXLimit.bounciness = Mathf.Infinity;
        _joint.highAngularXLimit = _highAngularXLimit;

        //setting angular y limit
        SoftJointLimit _angularYLimit = _joint.angularYLimit;
        _angularYLimit.limit = limitY;
        _angularYLimit.bounciness = Mathf.Infinity;
        _joint.angularYLimit = _angularYLimit;

        //setting angular z limit
        SoftJointLimit _angularZLimit = _joint.angularZLimit;
        _angularZLimit.limit = limitZ;
        _angularZLimit.bounciness = Mathf.Infinity;
        _joint.angularZLimit = _angularZLimit;

        //set angular x and yz springs
        SoftJointLimitSpring _angularLimitSpring = new SoftJointLimitSpring();
        _angularLimitSpring.spring = Mathf.Infinity;
        _angularLimitSpring.damper = Mathf.Infinity;
        _joint.angularXLimitSpring = _angularLimitSpring;
        _joint.angularYZLimitSpring = _angularLimitSpring;
    }

}

//effort of Jean-Fabre and samAsQ modified by Michał Kobus
//https://forum.unity3d.com/threads/excavator-simulation.66871