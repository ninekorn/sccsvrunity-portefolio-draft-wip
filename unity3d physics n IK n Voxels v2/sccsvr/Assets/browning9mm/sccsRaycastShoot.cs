using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class sccsRaycastShoot : MonoBehaviour
{
    public int counterbarrelFrameBeforeShootMax = 10;
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    public GameObject[] bulletTex;

    public static sccsRaycastShoot raycastShoot;
    public static GameObject hitObject;
    public GameObject bullet;
    public GameObject shadowBullet;

    Stopwatch raycastshootstopwatch = new Stopwatch();

    void Start()
    {
        raycastShoot = this;
        //laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();

        raycastshootstopwatch.Start();
    }
    int raycastshootstopwatchSwtc = 0;
    int counterbarrelR = 0;

    int counterbarrelRSwtc = 0;

    void Update()
    {
        if (raycastshootstopwatch.Elapsed.Milliseconds >= 500)
        {
            raycastshootstopwatchSwtc = 1;
            raycastshootstopwatch.Restart();
        }
        else
        {

            raycastshootstopwatchSwtc = 1;
        }

        if (raycastshootstopwatchSwtc == 1)
        {
            bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

            if (buttonPressedLeft)
            {
                if (counterbarrelR >= counterbarrelFrameBeforeShootMax)
                {
                    counterbarrelRSwtc = 1;
                }
                else
                {
                    //counterbarrelR = 0;
                    //counterbarrelRSwtc = 1;
                }





                if (counterbarrelRSwtc == 1)
                {
                    GameObject currentBullet = sccsU3DTBulletPool.current.GetPooledObject();// Instantiate(bullet, gunEnd.transform.position, Quaternion.identity);
                                                                                            //GameObject currentshadowBullet = sccsshadowbulletpoolerscript.current.GetPooledObject();

                    if (currentBullet != null) //&& currentshadowBullet!= null
                    {

                        //currentshadowBullet.transform.position = gunEnd.transform.position;
                        //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);

                        //currentBullet.GetComponent<sccsshadowBullet>().shadowObject = currentshadowBullet;
                        currentBullet.transform.position = gunEnd.transform.position;
                        currentBullet.GetComponent<sccsshadowBullet>().StartTheScript();

                        if (!currentBullet.GetComponent<sccsBulletInteraction>().enabled)
                        {
                            currentBullet.GetComponent<sccsBulletInteraction>().enabled = true;
                            currentBullet.GetComponent<sccsBulletInteraction>().projectileimpulse = currentBullet.GetComponent<sccsshadowBullet>().force;
                        }

                        currentBullet.GetComponent<BoxCollider>().enabled = false;

                        //currentshadowBullet.SetActive(true);
                        currentBullet.SetActive(true);
                        //currentShadowBullet.active = true;
                        //StartCoroutine(ShotEffect());
                        //nextFire = raycastshootstopwatch.Elapsed.Milliseconds + fireRate;


                        counterbarrelR = 0;
                        //projectileshotcounter++;

                        ////Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
                    }
                    counterbarrelRSwtc = 0;
                    raycastshootstopwatchSwtc = 0;
                }












                counterbarrelR++;
            }
            if (buttonPressedRight)
            {
                ////Debug.Log("buttonPressedRight:" + buttonPressedRight);
            }

        }






        /*

        //if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) &&  Time.time>nextFire)
        if (Input.GetMouseButtonDown(0))
        {
            GameObject currentBullet = Instantiate(bullet, gunEnd.transform.position, Quaternion.identity);
            //GameObject currentShadowBullet = Instantiate(shadowBullet, gunEnd.transform.position, Quaternion.identity);
            currentBullet.active = true;
            //currentShadowBullet.active = true;
            StartCoroutine(ShotEffect());
            nextFire = Time.time + fireRate;

           
        }*/
    }

    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        //laserLine.enabled = true;     
        yield return shotDuration;
        //laserLine.enabled = false;
    }


}







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
           }*/
