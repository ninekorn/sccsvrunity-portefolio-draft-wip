//https://forum.unity.com/threads/projectile-trajectory-accounting-for-gravity-velocity-mass-distance.425560/
//James Leenz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SCCoreSystems
{
    public class sc_jamesleenz_projectile : MonoBehaviour
    {
        public float init_velo = 1500;
        public Transform Target;
        public Transform drone;
        public Transform turretpivot;
        public Transform bullseye;

        Rigidbody2D TargetRigid;
        int counter_frame = 0;
        public int gunDamage = 1;
        public float fireRate = 0.25f;
        public float weaponRange = 50f;
        public float hitForce = 100f;
        public GameObject bullet;
        public Transform gunEnd;
        private float nextFire;
        private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
        GameObject currentBullet;
        float velo = 0.0f;// Vector2.zero;
        Rigidbody2D droneRigid;

        public GameObject bullet_template;
        public GameObject bullet_shot;

        public static float Dot(float aX, float aY, float bX, float bY)
        {
            return (aX * bX) + (aY * bY);
        }


        void Start()
        {
            droneRigid = Target.GetComponent<Rigidbody2D>();

            //laserLine = GetComponent<LineRenderer>();
            //fpsCam = GetComponentInParent<Camera>();

            //bullet_template.tag = "bulletshot";
            //laserLine = GetComponent<LineRenderer>();
        }


        int healthregen = 0;
        int health = 1;




        public void prepareshot()
        {
            
            
            //velo =  projectileVelocity = 150;
            float distance = Vector3.Distance(turretpivot.position, Target.position);
            float trajectoryAngle;

            TargetRigid = Target.GetComponent<Rigidbody2D>();

            Vector3 TargetCenter = FirstOrderIntercept(turretpivot.position, droneRigid.velocity, init_velo, Target.position, TargetRigid.velocity);
            bullseye.position = TargetCenter;

            if (CalculateTrajectory(distance, init_velo, out trajectoryAngle))
            {
                float trajectoryHeight = Mathf.Tan(trajectoryAngle * Mathf.Deg2Rad) * distance;
                TargetCenter.y += trajectoryHeight;
            }

            var dirtargettoturret = TargetCenter - turretpivot.position;
            dirtargettoturret.Normalize();

            var forwarddirTurret = sc_maths._getDirection(Vector3.forward, turretpivot.rotation);
            forwarddirTurret.Normalize();

            var rightdirturret = sc_maths._getDirection(Vector3.right, turretpivot.rotation);
            rightdirturret.Normalize();

            //var rightdirturret = new Vector2(forwarddirTurret.y,-forwarddirTurret.x);

            var dotprod =sc_maths.Dot(dirtargettoturret.x, dirtargettoturret.y, rightdirturret.x, rightdirturret.y);

            //Debug.Log("dot: " + dotprod);
            //Debug.Log("dot: " + dotprod);
            //Debug.Log("!dot: " + dotprod);

            if (health > 0)
            {

            }

            //fire at TargetCenter

            if (counter_frame >= fireRate)
            {
                bullet_shot = Instantiate(bullet_template, gunEnd.transform.position, Quaternion.identity);

                bullet_shot.tag = "bulletshot";
                bullet_shot.GetComponent<sc_bullet>().force = init_velo;
                //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                bullet_shot.active = true;
                //currentShadowBullet.active = true;
                //StartCoroutine(ShotEffect());
                //nextFire = Time.time + fireRate;
                counter_frame = 0;
            }
        
            counter_frame++;

        }


        //StartCoroutine(ShotEffect());
        /*private IEnumerator ShotEffect()
        {
            gunAudio.Play();
            //laserLine.enabled = true;     
            yield return shotDuration;
            //laserLine.enabled = false;
        }*/




        //first-order intercept using absolute target position
        public static Vector3 FirstOrderIntercept
            (
                Vector3 shooterPosition,
                Vector3 shooterVelocity,
                float shotSpeed,
                Vector3 targetPosition,
                Vector3 targetVelocity
            )
        {
            Vector3 targetRelativePosition = targetPosition - shooterPosition;
            Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
            float t = FirstOrderInterceptTime
            (
                shotSpeed,
                targetRelativePosition,
                targetRelativeVelocity
            );
            return targetPosition + t * (targetRelativeVelocity);
        }
        //first-order intercept using relative target position
        public static float FirstOrderInterceptTime
        (
            float shotSpeed,
            Vector3 targetRelativePosition,
            Vector3 targetRelativeVelocity
        )
        {
            float velocitySquared = targetRelativeVelocity.sqrMagnitude;
            if (velocitySquared < 0.001f)
                return 0f;

            float a = velocitySquared - shotSpeed * shotSpeed;

            //handle similar velocities
            if (Mathf.Abs(a) < 0.001f)
            {
                float t = -targetRelativePosition.sqrMagnitude /
                (
                    2f * Vector3.Dot
                    (
                        targetRelativeVelocity,
                        targetRelativePosition
                    )
                );
                return Mathf.Max(t, 0f); //don't shoot back in time
            }

            float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
            float c = targetRelativePosition.sqrMagnitude;
            float determinant = b * b - 4f * a * c;

            if (determinant > 0f)
            { //determinant > 0; two intercept paths (most common)
                float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                        t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
                if (t1 > 0f)
                {
                    if (t2 > 0f)
                        return Mathf.Min(t1, t2); //both are positive
                    else
                        return t1; //only t1 is positive
                }
                else
                    return Mathf.Max(t2, 0f); //don't shoot back in time
            }
            else if (determinant < 0f) //determinant < 0; no intercept path
                return 0f;
            else //determinant = 0; one intercept path, pretty much never happens
                return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
        }

        public static bool CalculateTrajectory(float TargetDistance, float ProjectileVelocity, out float CalculatedAngle)
        {
            CalculatedAngle = 0.5f * (Mathf.Asin((-Physics.gravity.y * TargetDistance) / (ProjectileVelocity * ProjectileVelocity)) * Mathf.Rad2Deg);
            if (float.IsNaN(CalculatedAngle))
            {
                CalculatedAngle = 0;
                return false;
            }
            return true;
        }

    }
}
