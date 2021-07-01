using UnityEngine;
using System.Collections;

public enum AxisList { xAxis, yAxis, zAxis }

public class PivotDrive : MonoBehaviour
{
    public string KeyboardInputAxis = "LeftJoystickHorizontal";
    public string GamepadInputAxis = "LeftJoystickHorizontalButton";
    public bool inverseInput = false;
    public float speed = 1f;
    public AxisList whichAxis = AxisList.xAxis;

    private ConfigurableJoint _joint;

    private FixedJoint _fixedJoint;

    void Awake()
    {
        //initialize joint
        _joint = GetComponent<ConfigurableJoint>();
        
        if (whichAxis == AxisList.xAxis)
        {
            var _drive = _joint.angularXDrive;
            _joint.angularXDrive = _drive;
        }
        else
        {
            var _drive = _joint.angularYZDrive;
            _joint.angularYZDrive = _drive;
        }

        if (_joint == null)
        {
            Debug.LogError("Missing ConfigurableJoint", this);
        }
    }


    void FixedUpdate()
    {
        float _inputValue = 0;

        //_inputValue += Input.GetAxis(KeyboardInputAxis) + Input.GetAxis(GamepadInputAxis);

        _inputValue += Input.GetAxis("Horizontal");


        if (inverseInput) _inputValue = -_inputValue;

        // get the velocity
        float _velocity = _inputValue * speed * Time.deltaTime;

        if (_velocity != 0f)
        {
            //if we are moving and the fixed joint exists detroy it
            if (_fixedJoint != null)
            {
                Destroy(_fixedJoint);
            }

            // angular is the same on all axis for convenience
            _joint.targetAngularVelocity = new Vector3(_velocity, _velocity, _velocity); 
        }
        else
        {
            //hinge is stopped, create fixed joint 
            if (_fixedJoint == null)
            {
                //create fixed joint and join it to the same object as hinge
                _fixedJoint = this.gameObject.AddComponent<FixedJoint>();
                _fixedJoint.connectedBody = _joint.connectedBody.GetComponent<Rigidbody>();

                //set joint velocity to nothing
                _joint.targetAngularVelocity = new Vector3(0f, 0f, 0f);
            }
        }
    }
}

//effort of Jean-Fabre and samAsQ modified by Michał Kobus
//https://forum.unity3d.com/threads/excavator-simulation.66871