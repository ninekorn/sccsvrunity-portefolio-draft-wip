using System.Collections.Generic;
using System.Threading;
using Jitter;
using Jitter.Collision;
using Jitter.Dynamics;
using Jitter.Dynamics.Constraints;
using Jitter.LinearMath;
using UnityEngine;
using Material = Jitter.Dynamics.Material;
using System.Collections;
using RigidBody = Jitter.Dynamics.RigidBody;
using SCCoreSystems;



public class JPhysics : MonoBehaviour
{
    public static JPhysics currentscript;

    public JPhysics()
    {
        currentscript = this;
    }

    public enum BodyTag
    {
        jitterCollisionObject,
        humanrig
        /*DrawMe,
        DontDrawMe,
        Terrain,
        pseudoCloth,
        PlayerHandLeft,
        PlayerHandRight,
        PlayerShoulderLeft,
        PlayerShoulderRight,
        PlayerTorso,
        PlayerPelvis,
        PlayerUpperArmLeft,
        PlayerLowerArmLeft,
        PlayerUpperArmRight,
        PlayerLowerArmRight,
        PlayerUpperLegLeft,
        PlayerLowerLegLeft,
        PlayerUpperLegRight,
        PlayerLowerLegRight,
        PlayerFootRight,
        PlayerFootLeft,
        PlayerHead,
        PlayerLeftElbowTarget,
        PlayerRightHandGrabTarget,
        PlayerLeftHandGrabTarget,

        PlayerRightElbowTarget,
        PlayerLeftElbowTargettwo,
        PlayerRightElbowTargettwo,
        PlayerLeftTargetKnee,
        PlayerRightTargetKnee,
        PlayerLeftTargettwoKnee,
        PlayerRightTargettwoKnee,
        sc_containment_grid,
        sc_grid,
        Screen,
        sc_jitter_cloth,
        //someothertest,
        //testChunkCloth,
        //cloth_cube,
        //screen_corners,
        //screen_pointer_touch,
        //screen_pointer_HMD,
        _terrain_tiles,
        _terrain,
        _floor,
        //_icosphere,
        //_sphere,
        _spectrum,
        _pixelspectrumscreen,
        //_physics_cube_group_b,
        _screen_assets,


        physicsInstancedCube,
        physicsInstancedCone,
        physicsInstancedCylinder,
        physicsInstancedCapsule,
        physicsInstancedSphere,

        sc_perko_voxel,
        physicsInstancedScreen,
        physicsInstancedScreenHeightmaps,
        sc_perko_voxel_planet_chunk*/
    }


    public float gravity = -1; //-9.81f
    public float gravitymul = 1.0f;

    public Transform thisplanet;
    Jitter.DataStructures.ReadOnlyHashset<Jitter.Dynamics.RigidBody> Rigidbodies;




    public static readonly Color Color = new Color(255, 210, 0);

    [SerializeField]
    private JMaterial defaultMaterial = null;

    internal static Material defaultPhysicsMaterial = new Material
    {
        KineticFriction = .3f,
        StaticFriction = .6f,
        Restitution = 0,
    };

    [SerializeField]
    private bool runInBackground = false;

    public static CollisionSystem collisionSystem;

    public static CollisionSystem CollisionSystem
    {
        get
        {
         
            if (collisionSystem == null)
            {
                collisionSystem = new CollisionSystemSAP();
            }
            return collisionSystem;
        }
    }

    //public static World TheWorld { get; set; }

    public World worldpub
    {
        get
        {
            if (world == null)
            {
                Debug.Log("null world. JPhysics.cs");
                world = World;
            }

            return world;
        }
    }


    //public World World => test;


    public static World world;

    public static World World
    {
        get
        {
            if (world == null)
            {
                world = new World(CollisionSystem);
                world.SetDampingFactors(.5f, .5f);
                world.Gravity = Physics.gravity.ToJVector();
            }
            return world;
        }
    }

    [SerializeField]
    private float sleepAngularVelocity = .05f;

    public float SleepAngularVelocity
    {
        get { return sleepAngularVelocity; }
        set
        {
            sleepAngularVelocity = value;
            UpdateWorld();
        }
    }

    [SerializeField]
    private float sleepVelocity = 0.05f;

    public float SleepVelocity
    {
        get { return sleepVelocity; }
        set
        {
            sleepVelocity = value;
            UpdateWorld();
        }
    }

    [SerializeField]
    private float angularDamping = .5f;
    public float AngularDamping
    {
        get { return angularDamping; }
        set
        {
            angularDamping = value;
            UpdateWorld();
        }
    }

    [SerializeField]
    private float linearDamping = .5f;
    public float LinearDamping
    {
        get { return linearDamping; }
        set
        {
            linearDamping = value;
            UpdateWorld();
        }
    }


    public int initFrameCounter = 0;
    public int initFrameCounterMax = 50;
    public int initFrameCounterSwtc = 0;

    public void UpdateWorld()
    {
        //Debug.Log("is updating world");
        world.SetDampingFactors(angularDamping, linearDamping);
        world.SetInactivityThreshold(sleepAngularVelocity, sleepVelocity, .5f);
        if (defaultMaterial != null)
        {
            defaultPhysicsMaterial = defaultMaterial.ToMaterial();
        }





        /*if (initFrameCounter >= initFrameCounterMax)
        {
            Rigidbody[] rigidbodies = Rigidbody.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

            for (int rb = 0; rb<rigidbodies.Length; rb++)
            {
                if (rigidbodies[rb].velocity.magnitude< 0.1f)
                {
                    if (!rigidbodies[rb].isKinematic)
                    {
                        rigidbodies[rb].isKinematic = true;
                    }          
                }
            }

            if (gameObject.GetComponent<MeshCollider>() != null)
            {
                //gameObject.GetComponent<MeshCollider>().enabled = false;
                //Destroy(gameObject.GetComponent<MeshCollider>(),0.1f);
                gameObject.GetComponent<MeshCollider>().isTrigger = true;
            }
            if (gameObject.GetComponent<BoxCollider>() != null)
            {
                //Destroy(gameObject.GetComponent<BoxCollider>(),0.1f);
                //gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
            if (gameObject.GetComponent<SphereCollider>() != null)
            {
                //Destroy(gameObject.GetComponent<SphereCollider>(),0.1f);
                //gameObject.GetComponent<SphereCollider>().enabled = false;
                gameObject.GetComponent<SphereCollider>().isTrigger = true;
            }
            initFrameCounter = 0;
        }
        initFrameCounter++;*/




        if (initFrameCounter >= initFrameCounterMax)
        {
            //TO READD
            //TO READD
            //TO READD
            /*var Rigidbodies = JPhysics.world.RigidBodies;
            IEnumerable someHashSet = JPhysics.world.RigidBodies;

            var someEnum = someHashSet.GetEnumerator();

            while (someEnum.MoveNext())
            {
                RigidBody rigid = (RigidBody)someEnum.Current;

                if (!rigid.IsStatic)
                {
                    if (rigid.AngularVelocity.Length() < 0.001f || rigid.LinearVelocity.Length() < 0.001f)
                    {
                        /*if (rigid.AngularVelocity.Length() < 0.001f)
                        {

                        }
            if (rigid.LinearVelocity.Length() < 0.001f)
                        {
                            rigid.IsStatic = true;
                        }
                    }
                }
            }*/

            initFrameCounter = 0;
        }
        initFrameCounter++;

        //TO READD
        //TO READD
        //TO READD



        //Vector3 directiontoplanetcenter = thisplanet.position - JitterExtensions.ToVector3(rigid.Position);
        //var forceDir = (JitterExtensions.ToJVector(thisplanet.position) - rigid.Position) * -1;
        //forceDir.Normalize();
        //rigid.AddForce(forceDir * gravity * gravitymul);

        /*Rigidbody[] rigidbodies = Rigidbody.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

        for (int rb = 0; rb < rigidbodies.Length; rb++)
        {
            if (rigidbodies[rb].velocity.magnitude < 0.1f)
            {
                if (!rigidbodies[rb].isKinematic)
                {
                    rigidbodies[rb].isKinematic = true;
                }
            }
        }*/



        /*if (gameObject.GetComponent<MeshCollider>() != null)
        {
            //gameObject.GetComponent<MeshCollider>().enabled = false;
            //Destroy(gameObject.GetComponent<MeshCollider>(),0.1f);
            gameObject.GetComponent<MeshCollider>().isTrigger = true;
        }
        if (gameObject.GetComponent<BoxCollider>() != null)
        {
            //Destroy(gameObject.GetComponent<BoxCollider>(),0.1f);
            //gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
        if (gameObject.GetComponent<SphereCollider>() != null)
        {
            //Destroy(gameObject.GetComponent<SphereCollider>(),0.1f);
            //gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
        }*/

    }

    private void Awake()
    {
        World.Clear();
        UpdateWorld();

        if (runInBackground)
        {
            InitializeThreads();
        }
    }

    private void FixedUpdate()
    {
        if (runInBackground == false)
        {
            World.Step(Time.fixedDeltaTime, false);
        }
    }

    /*
    private void OnDrawGizmos()
    {
        JBBox box;

        foreach (var island in World.Islands)
        {
            box = JBBox.SmallBox;

            foreach (RigidBody body in island.Bodies)
            {
                box = JBBox.CreateMerged(box, body.BoundingBox);
            }

            var color = Gizmos.color;
            Gizmos.color = island.IsActive() ? Color.green : Color.yellow;
            JRuntimeDrawer.Instance.DrawAabb(box.Min, box.Max);
            Gizmos.color = color;
        }
    }*/

    public static IEnumerable<JCollision> DetectCollisions(RigidBody body)
    {
        var position1 = body.Position;
        var orientation1 = body.Orientation;

        foreach (RigidBody body2 in World.RigidBodies)
        {
            if (body == body2)
            {
                continue;
            }
            var position2 = body2.Position;
            var orientation2 = body2.Orientation;

            JVector point;
            JVector normal;
            float penetration;
            var collisionDetected = XenoCollide.Detect(body.Shape, body2.Shape, ref orientation1, ref orientation2, ref position1, ref position2, out point, out normal, out penetration);
            if (collisionDetected)
            {
                yield return new JCollision(body, body2, point.ToVector3(), normal.ToVector3(), penetration);
            }
        }
    }

    public static JCollision DetectCollision(RigidBody body1, RigidBody body2)
    {
        System.Diagnostics.Debug.Assert(body1 != body2, "body1 == body2");

        var position1 = body1.Position;
        var position2 = body2.Position;
        var orientation1 = body1.Orientation;
        var orientation2 = body2.Orientation;

        JVector point;
        JVector normal;
        float penetration;
        var collisionDetected = XenoCollide.Detect(body1.Shape, body2.Shape, ref orientation1, ref orientation2, ref position1, ref position2, out point, out normal, out penetration);
        if (collisionDetected == false)
        {
            return null;
        }

        return new JCollision(body1, body2, point.ToVector3(), normal.ToVector3(), penetration);
    }

    public static JRaycastHit Raycast(Ray ray, float maxDistance = 10f, RaycastCallback callback = null)
    {
        RigidBody hitBody;
        JVector hitNormal;
        float hitFraction;

        var origin = ray.origin.ToJVector();
        var direction = ray.direction.ToJVector();

        if (collisionSystem.Raycast(origin, direction, callback, out hitBody, out hitNormal, out hitFraction))
        {
            if (hitFraction <= maxDistance)
            {
                return new JRaycastHit(hitBody, hitNormal.ToVector3(), ray.origin, ray.direction, hitFraction);
            }
        }
        else
        {
            direction *= maxDistance;
            if (collisionSystem.Raycast(origin, direction, callback, out hitBody, out hitNormal, out hitFraction))
            {
                return new JRaycastHit(hitBody, hitNormal.ToVector3(), ray.origin, direction.ToVector3(), hitFraction);
            }
        }
        return null;
    }

    public static void AddBody(RigidBody body)
    {
        lock (sync)
            World.AddBody(body);
    }

    public static void RemoveBody(RigidBody body)
    {
        lock (sync)
            World.RemoveBody(body);
    }

    public static void AddConstraint(Constraint constraint)
    {
        lock (sync)
        {
            World.AddConstraint(constraint);
        }
    }

    public static void RemoveConstraint(Constraint constraint)
    {
        lock (sync)
        {
            World.RemoveConstraint(constraint);
        }
    }

    public static int RigidBodyCount
    {
        get { return World.RigidBodies.Count; }
    }

    private Thread controlThread;
    private Thread stepThread;
    private volatile bool cancel;
    private float deltaTime;
    private AutoResetEvent reset;
    private static readonly object sync = new object();

    private void OnDestroy()
    {
        cancel = true;
        if (reset != null)
        {
            reset.Set();
        }
    }

    private void InitializeThreads()
    {
        controlThread = new Thread(ControlThreadProc);
        controlThread.IsBackground = true;
        deltaTime = Time.fixedDeltaTime;

        stepThread = new Thread(StepThreadProc);
        stepThread.IsBackground = true;

        reset = new AutoResetEvent(false);
        cancel = false;
        controlThread.Start();
        stepThread.Start();
    }

    private void ControlThreadProc()
    {
        while (cancel == false)
        {
            Thread.Sleep((int)(deltaTime * 1000));
            reset.Set();
        }
        Debug.Log("control thread stopped");
    }

    private void StepThreadProc()
    {
        while (cancel == false)
        {
            reset.WaitOne();
            lock (sync)
            {
                World.Step(deltaTime, false);
            }
        }
        Debug.Log("step thread stopped");
    }
}
