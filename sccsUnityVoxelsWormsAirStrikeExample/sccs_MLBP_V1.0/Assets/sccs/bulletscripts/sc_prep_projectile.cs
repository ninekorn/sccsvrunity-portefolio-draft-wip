using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCCoreSystems
{
    public class sc_prep_projectile : MonoBehaviour
    {
        public int letstraybulletgo = 0;
        public int projectileshotcounter = 0;
        public float fireRate = 0.25f;
        public int gunDamage = 1;
        public float weaponRange = 50f;
        public float hitForce = 100f;

        //https://en.wikipedia.org/wiki/McDonnell_Douglas_CF-18_Hornet
        //public float projectileimpulse = 0.0f; //1050m/s
        //1 × 20 mm M61A1 Vulcan internal Gatling gun with 578 rounds, with a firing rate of 4,000 or 6,000 rounds per minute
        //3,450 feet per second (1,050 m/s) with PGU-28/B round
        //M61A1: 6,000 rounds per minute
        //M61A2: 6,600 rounds per minute


        public int bullettypeselected = 0;
        public int dir_select = 0;


        public GameObject weapon_prefab;
        public GameObject[] barrel_hardpoints;

        public float shot_speed;
        int barrel_index = 0;


        public Transform player;
        public Transform gunEnd;
        public GameObject bullet_template;
        public Transform target;
        public Transform turretpivot;


        GameObject bullet_shot;
        public float gravity = 0.0f;

        //public float turret_rotation_speed = 3f;
        //public float init_velo = 150;
        //public static float force = 150;



        private Camera fpsCam;
        private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
  
        //private LineRenderer laserLine;
        private float nextFire;
        //public GameObject[] bulletTex;

        static sc_prep_projectile sc_prep_projectile_;
        //public static GameObject hitObject;
        //public GameObject shadowBullet;

        public Rigidbody rigidbodytarget;

        float velo;

        void Start()
        {
            //bullet_template.tag = "bulletshot";
            sc_prep_projectile_ = this;
            //laserLine = GetComponent<LineRenderer>();

            fpsCam = GetComponentInParent<Camera>();


            if (target != null)
            {
                rigidbodytarget = target.GetComponent<Rigidbody>();
            }
        }

        int counterbarrelL = 999; //(does it still work?)
        int counterbarrelR = 999;

        float timetohit = 0.0f;
        Vector3 dirToBullseye = Vector3.zero;
        int counterfail = 0;

        public void prepareshot()
        {
            if (target != null)
            {
                rigidbodytarget = target.GetComponent<Rigidbody>();
            }
            else
            {

            }
            //pythagore theorem
            //c2=a2+b2

            //c2 =
            //Vector3 dirToTargetNow = target.position - gunEnd.position;


            if (rigidbodytarget != null && target != null)
            {
                //Debug.Log(rigidbodytarget.name);
                Vector2 targetDir = target.forward;
                Vector2 targetVelo = rigidbodytarget.velocity;

                /*
                ProjectileHelper.ComputeTimeToHitGround(gunEnd.position, rigidbodytarget.velocity, 0, 0, out timetohit);

                if (dir_select == 0)
                {
                    Vector3 posiattimeahead = ProjectileHelper.ComputePositionAtTimeAhead(rigidbodytarget.position, rigidbodytarget.velocity, gravity, timetohit); //0.1f
                    dirToBullseye = posiattimeahead - gunEnd.position;
                    dirToBullseye.Normalize();
                }
                else if (dir_select == 1)
                {
                    Vector3 dir1;
                    Vector3 dir2;
                    ProjectileHelper.ComputeDirectionToHitTargetWithSpeed(turretpivot.position, target.position, gravity, init_velo, out dir1, out dir2);
                    dir1.Normalize();
                    dir2.Normalize();

                    dirToBullseye = dir1;

                }
                else if (dir_select == 2)
                {
                    Vector3 dir1;
                    Vector3 dir2;
                    ProjectileHelper.ComputeDirectionToHitTargetWithSpeed(turretpivot.position, target.position, gravity, init_velo, out dir1, out dir2);
                    dir1.Normalize();
                    dir2.Normalize();

                    dirToBullseye = dir2;
                }*/



                /*if (Input.GetMouseButtonDown(0) && barrel_hardpoints != null)
                {
                    GameObject bullet = (GameObject)Instantiate(weapon_prefab, barrel_hardpoints[barrel_index].transform.position, transform.rotation);

                    Rigidbody2D rigidbody2D = bullet.gameObject.GetComponent<Rigidbody2D>();
                    rigidbody2D.AddForce(bullet.transform.up * shot_speed);
                    bullet.GetComponent<Projectile>().firing_ship = transform.parent.gameObject;
                    barrel_index++; //This will cycle sequentially through the barrels in the barrel_hardpoints array

                    if (barrel_index >= barrel_hardpoints.Length)
                    {
                        barrel_index = 0;
                    }
                }*/

                if (counterbarrelR >= fireRate)
                {
                    if (bullettypeselected == 0)
                    {
                        bullet_shot = Instantiate(bullet_template, bullet_template.transform.position, bullet_template.transform.rotation);

                        bullet_shot.transform.parent = null;
                        bullet_shot.tag = "bulletshot";

                        /*bullet_shot.GetComponent<shadowBulletTwo>().projectileimpulse = projectileimpulse;
                        bullet_shot.GetComponent<shadowBulletTwo>().bullseyedirection = gunEnd.up;
                        bullet_shot.GetComponent<shadowBulletTwo>().firing_ship = player.gameObject;
                        bullet_shot.GetComponent<shadowBulletTwo>().gunEnd = gunEnd.gameObject;
                        bullet_shot.GetComponent<shadowBulletTwo>().enabled = true;*/

                        //bullet_shot.GetComponent<shadowBullet>().projectileimpulse = projectileimpulse;
                        bullet_shot.GetComponent<shadowBullet>().bullseyedirection = gunEnd.transform.forward;
                        bullet_shot.GetComponent<shadowBullet>().firing_ship = player.gameObject;
                        bullet_shot.GetComponent<shadowBullet>().gunEnd = gunEnd.gameObject;
                        bullet_shot.GetComponent<shadowBullet>().enabled = true;

                        GetComponent<AudioSource>().Play(0);



                        //Debug.Log("projectileshotcounter: " + projectileshotcounter);
                        bullet_shot.SetActive(true);
                        //bullet_shot.GetComponent<Rigidbody>().AddForce(bullet_template.transform.forward * projectileimpulse, ForceMode.Impulse);

                        if (gunEnd == null)
                        {
                            Debug.Log("gunEnd null");
                        }

                        //bullet_shot.GetComponent<Rigidbody>().velocity += gunEnd.up;
                        //bullet_shot.GetComponent<sc_bullet>().force = init_velo;
                        //bullet_shot.GetComponent<sc_bullet>().bullseyedirection = gunEnd.up;
                        //bullet_shot.GetComponent<sc_bullet>().firing_ship = player.gameObject;

                        //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                  
                        //currentShadowBullet.active = true;
                        //StartCoroutine(ShotEffect());
                        nextFire = Time.time + fireRate;
                        counterbarrelR = 0;
                        projectileshotcounter++;
                    }
                    else if (bullettypeselected == 1)
                    {
                        bullet_shot = Instantiate(bullet_template, bullet_template.transform.position, Quaternion.identity);
                        bullet_shot.transform.parent = null;
                        bullet_shot.tag = "bulletshot";

                        //bullet_shot.GetComponent<sc_bullet>().force = projectileimpulse; // maybe later for the BAE Systems magnetic electric impulse or something.
                        bullet_shot.GetComponent<sc_bullet>().bullseyedirection = this.transform.forward;
                        bullet_shot.GetComponent<sc_bullet>().firing_ship = player.gameObject;
                        bullet_shot.SetActive(true);
                        //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);

                        //currentShadowBullet.active = true;
                        //StartCoroutine(ShotEffect());
                        nextFire = Time.time + fireRate;
                        counterbarrelR = 0;
                    }





                    /*
                    if (player.tag != "Player")
                    {
                        
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0) && barrel_hardpoints != null)
                        {
                            if (bullettypeselected == 0)
                            {
                                bullet_shot = Instantiate(bullet_template, bullet_template.transform.position, Quaternion.identity);
                                bullet_shot.tag = "bulletshot";

                                bullet_shot.GetComponent<Rigidbody>().AddForce(gunEnd.up * projectileforce, ForceMode.Impulse);

                                //bullet_shot.GetComponent<Rigidbody>().velocity += gunEnd.up;
                                //bullet_shot.GetComponent<sc_bullet>().force = init_velo;
                                //bullet_shot.GetComponent<sc_bullet>().bullseyedirection = gunEnd.up;
                                //bullet_shot.GetComponent<sc_bullet>().firing_ship = player.gameObject;

                                //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                                bullet_shot.active = true;
                                //currentShadowBullet.active = true;
                                //StartCoroutine(ShotEffect());
                                nextFire = Time.time + fireRate;
                                counterbarrelR = 0;
                            }
                            else if (bullettypeselected == 1)
                            {
                                //bullet_shot = Instantiate(bullet_template, gunEndR.transform.position, Quaternion.identity);

                                bullet_shot = Instantiate(bullet_template, bullet_template.transform.position, Quaternion.identity);
                                bullet_shot.tag = "bulletshot";
                                bullet_shot.GetComponent<sc_bullet>().force = init_velo;
                                bullet_shot.GetComponent<sc_bullet>().bullseyedirection = gunEnd.up;
                                bullet_shot.GetComponent<sc_bullet>().firing_ship = player.gameObject;

                                //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                                bullet_shot.active = true;
                                //currentShadowBullet.active = true;
                                //StartCoroutine(ShotEffect());
                                nextFire = Time.time + fireRate;
                                counterbarrelR = 0;
                            }
                        }
                    }*/

                    /*//bullet_shot = Instantiate(bullet_template, gunEndR.transform.position, Quaternion.identity);

                    bullet_shot = Instantiate(bullet_template, bullet_template.transform.position, Quaternion.identity);
                    bullet_shot.tag = "bulletshot";
                    bullet_shot.GetComponent<sc_bullet>().force = init_velo;
                    bullet_shot.GetComponent<sc_bullet>().bullseyedirection = gunEnd.up;
                    //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                    bullet_shot.active = true;
                    //currentShadowBullet.active = true;
                    //StartCoroutine(ShotEffect());
                    nextFire = Time.time + fireRate;
                    counterbarrelR = 0;*/
                }

                counterbarrelR++;
            }
            else
            {

                if (counterbarrelR >= fireRate)
                {
                    if (bullettypeselected == 0)
                    {
                        bullet_shot = Instantiate(bullet_template, bullet_template.transform.position, bullet_template.transform.rotation);

                        bullet_shot.transform.parent = null;
                        bullet_shot.tag = "bulletshot";

                        /*bullet_shot.GetComponent<shadowBulletTwo>().projectileimpulse = projectileimpulse;
                        bullet_shot.GetComponent<shadowBulletTwo>().bullseyedirection = gunEnd.up;
                        bullet_shot.GetComponent<shadowBulletTwo>().firing_ship = player.gameObject;
                        bullet_shot.GetComponent<shadowBulletTwo>().gunEnd = gunEnd.gameObject;
                        bullet_shot.GetComponent<shadowBulletTwo>().enabled = true;*/

                        //bullet_shot.GetComponent<shadowBullet>().projectileimpulse = projectileimpulse;
                        bullet_shot.GetComponent<shadowBullet>().bullseyedirection = gunEnd.transform.forward;
                        bullet_shot.GetComponent<shadowBullet>().firing_ship = player.gameObject;
                        bullet_shot.GetComponent<shadowBullet>().gunEnd = gunEnd.gameObject;
                        bullet_shot.GetComponent<shadowBullet>().enabled = true;

                        GetComponent<AudioSource>().Play(0);

                        //Debug.Log("projectileshotcounter: " + projectileshotcounter);
                        bullet_shot.SetActive(true);
                        //bullet_shot.GetComponent<Rigidbody>().AddForce(bullet_template.transform.forward * projectileimpulse, ForceMode.Impulse);

                        if (gunEnd == null)
                        {
                            Debug.Log("gunEnd null");
                        }

                        //bullet_shot.GetComponent<Rigidbody>().velocity += gunEnd.up;
                        //bullet_shot.GetComponent<sc_bullet>().force = init_velo;
                        //bullet_shot.GetComponent<sc_bullet>().bullseyedirection = gunEnd.up;
                        //bullet_shot.GetComponent<sc_bullet>().firing_ship = player.gameObject;

                        //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);

                        //currentShadowBullet.active = true;
                        //StartCoroutine(ShotEffect());
                        nextFire = Time.time + fireRate;
                        counterbarrelR = 0;
                        projectileshotcounter++;
                    }
                    else if (bullettypeselected == 1)
                    {
                        bullet_shot = Instantiate(bullet_template, bullet_template.transform.position, Quaternion.identity);
                        bullet_shot.transform.parent = null;
                        bullet_shot.tag = "bulletshot";

                        //bullet_shot.GetComponent<sc_bullet>().force = projectileimpulse; // maybe later for the BAE Systems magnetic electric impulse or something.
                        bullet_shot.GetComponent<sc_bullet>().bullseyedirection = this.transform.forward;
                        bullet_shot.GetComponent<sc_bullet>().firing_ship = player.gameObject;
                        bullet_shot.SetActive(true);
                        //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);

                        //currentShadowBullet.active = true;
                        //StartCoroutine(ShotEffect());
                        nextFire = Time.time + fireRate;
                        counterbarrelR = 0;
                    }





                    /*
                    if (player.tag != "Player")
                    {
                        
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0) && barrel_hardpoints != null)
                        {
                            if (bullettypeselected == 0)
                            {
                                bullet_shot = Instantiate(bullet_template, bullet_template.transform.position, Quaternion.identity);
                                bullet_shot.tag = "bulletshot";

                                bullet_shot.GetComponent<Rigidbody>().AddForce(gunEnd.up * projectileforce, ForceMode.Impulse);

                                //bullet_shot.GetComponent<Rigidbody>().velocity += gunEnd.up;
                                //bullet_shot.GetComponent<sc_bullet>().force = init_velo;
                                //bullet_shot.GetComponent<sc_bullet>().bullseyedirection = gunEnd.up;
                                //bullet_shot.GetComponent<sc_bullet>().firing_ship = player.gameObject;

                                //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                                bullet_shot.active = true;
                                //currentShadowBullet.active = true;
                                //StartCoroutine(ShotEffect());
                                nextFire = Time.time + fireRate;
                                counterbarrelR = 0;
                            }
                            else if (bullettypeselected == 1)
                            {
                                //bullet_shot = Instantiate(bullet_template, gunEndR.transform.position, Quaternion.identity);

                                bullet_shot = Instantiate(bullet_template, bullet_template.transform.position, Quaternion.identity);
                                bullet_shot.tag = "bulletshot";
                                bullet_shot.GetComponent<sc_bullet>().force = init_velo;
                                bullet_shot.GetComponent<sc_bullet>().bullseyedirection = gunEnd.up;
                                bullet_shot.GetComponent<sc_bullet>().firing_ship = player.gameObject;

                                //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                                bullet_shot.active = true;
                                //currentShadowBullet.active = true;
                                //StartCoroutine(ShotEffect());
                                nextFire = Time.time + fireRate;
                                counterbarrelR = 0;
                            }
                        }
                    }*/

                    /*//bullet_shot = Instantiate(bullet_template, gunEndR.transform.position, Quaternion.identity);

                    bullet_shot = Instantiate(bullet_template, bullet_template.transform.position, Quaternion.identity);
                    bullet_shot.tag = "bulletshot";
                    bullet_shot.GetComponent<sc_bullet>().force = init_velo;
                    bullet_shot.GetComponent<sc_bullet>().bullseyedirection = gunEnd.up;
                    //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                    bullet_shot.active = true;
                    //currentShadowBullet.active = true;
                    //StartCoroutine(ShotEffect());
                    nextFire = Time.time + fireRate;
                    counterbarrelR = 0;*/
                }

                counterbarrelR++;


                //Debug.Log("failcounter-" + counterfail);
                //counterfail++;


            }

















            /*
            bullet_shot = Instantiate(bullet_template, gunEndR.transform.position, Quaternion.identity);
            bullet_shot.GetComponent<Projectile>().firing_ship = turretpivot.parent.gameObject;
            bullet_shot.tag = "bulletshot";
            //bullet_shot.GetComponent<sc_bullet>().force = init_velo;
            //bullet_shot.GetComponent<sc_bullet>().bullseyedirection = gunEndR.up;
            //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
            //bullet_shot.active = true;
            //currentShadowBullet.active = true;
            //StartCoroutine(ShotEffect());
            nextFire = Time.time + fireRate;
            counterbarrelR = 0;*/

            /*if (bullet_shot != null)
            {
                velo = init_velo + (bullet_shot.GetComponent<Rigidbody2D>().velocity.magnitude * 1.15f);
            }*/


            /*var forwarddirTurret = sc_maths._getDirection(Vector3.forward, turretpivot.rotation);
            forwarddirTurret.Normalize();
            var rightdirturret = sc_maths._getDirection(Vector3.right, turretpivot.rotation);
            rightdirturret.Normalize();


            var dotprod = sc_maths.Dot(dirToBullseye.x, dirToBullseye.y, rightdirturret.x, rightdirturret.y);

            //Debug.Log(dotprod);

            if (dotprod > -0.00254321f && dotprod < 0 || dotprod < 0.00254321f && dotprod > 0)
            {
                if (counter_frame >= fireRate)
                {
                    bullet_shot = Instantiate(bullet_template, gunEnd.transform.position, Quaternion.identity);

                    bullet_shot.tag = "bulletshot";
                    bullet_shot.GetComponent<sc_bullet>().force = velo;
                    //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                    bullet_shot.active = true;
                    //currentShadowBullet.active = true;
                    //StartCoroutine(ShotEffect());
                    nextFire = Time.time + fireRate;
                    counter_frame = 0;
                }
            }
            else
            {

            }
            */




































            //Vector3 dirToTarget1;
            //Vector3 dirToTarget2;
            //ProjectileHelper.ComputeDirectionToHitTargetWithSpeed(gunEnd.position, target.position, -9.81f, 1500, out dirToTarget1, out dirToTarget2);
            //float timeToHit = 0.0f;
            //ProjectileHelper.ComputeTimeToHitGround(gunEnd.position, target.position,  0, -9.81f, out timeToHit);

            /*Vector3 veloattimeahead = ProjectileHelper.ComputeVelocityAtTimeAhead(rigidbodytarget.position, rigidbodytarget.velocity, -9.81f, 0.1f);
            Vector3 posiattimeahead = ProjectileHelper.ComputePositionAtTimeAhead(rigidbodytarget.position, rigidbodytarget.velocity, -9.81f, 0.1f);

            bool canreachtarget = ProjectileHelper.CanReachTargetWithSpeed(gunEnd.position, posiattimeahead, -9.81f, veloattimeahead.magnitude);

            if (canreachtarget)
            {
                
            }*/
            /*
            Vector3 dir1;
            Vector3 dir2;
            ProjectileHelper.ComputeDirectionToHitTargetWithSpeed(gunEnd.transform.position, rigidbodytarget.position, -9.81f, rigidbodytarget.velocity.magnitude, out dir1, out dir2);
            dir1.Normalize();
            dir2.Normalize();


            */













            /*var dirtargettoturret = posiattimeahead - gunEnd.position; //TargetCenter
            dirtargettoturret.Normalize();

            */










            /*
            if(counter_frame >= fireRate)
            {
                GameObject currentBullet = Instantiate(bullet, gunEnd.transform.position, Quaternion.identity);
                //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                currentBullet.active = true;
                //currentShadowBullet.active = true;
                StartCoroutine(ShotEffect());
                nextFire = Time.time + fireRate;

                counter_frame = 0;
            }

            counter_frame++;*/

            /*if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) &&  Time.time>nextFire)
            {
                GameObject currentBullet = Instantiate(bullet, gunEnd.transform.position, Quaternion.identity);
                //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
                currentBullet.active = true;
                //currentShadowBullet.active = true;
                StartCoroutine(ShotEffect());
                nextFire = Time.time + fireRate;

                /*Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(1.5f, 0.5f, 0));

                //Vector3 rayOrigin = cameraViewPoint.transform.position;

                RaycastHit hit;

                //laserLine.SetPosition(0, gunEnd.position);

                if (Physics.Raycast(rayOrigin,fpsCam.transform.forward,out hit, weaponRange))
                {
                    //laserLine.SetPosition(1, hit.point);

                    ShootableBox health = hit.collider.GetComponent<ShootableBox>();
                    if (health !=null)
                    {
                        health.Damage(gunDamage);
                    }

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce);
                        //GameObject bulletDecal = Instantiate(bulletTex[Random.Range(0, 0)], hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

                        GameObject bulletDecal = Instantiate(bulletTex[Random.Range(0, 0)], hit.point, Quaternion.identity );
                        bulletDecal.SetActive(true);
                        //Quaternion rot = Quaternion.Euler(40,0,0);
                        bulletDecal.transform.forward = -hit.normal;
                        hitObject = hit.transform.gameObject;
                        //bulletDecal.transform.rotation = rot;
                        bulletDecal.transform.parent = hit.transform;
                        if (hit.transform.gameObject.tag == "enemy")
                        {
                            bulletDecal.GetComponent<Decal>().material = bulletDecal.GetComponent<Decal>().materialBlood;
                            bulletDecal.GetComponent<Decal>().sprite = bulletDecal.GetComponent<Decal>().spriteBlood;
                        }
                        else
                        {
                            //bulletDecal.GetComponent<Decal>().sprite = 
                        }
                    }
                }
                else
                {
                    //laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
                }
            }*/
        }

        /*private IEnumerator ShotEffect()
        {
            gunAudio.Play();
            //laserLine.enabled = true;     
            yield return shotDuration;
            //laserLine.enabled = false;
        }*/

    }
}
