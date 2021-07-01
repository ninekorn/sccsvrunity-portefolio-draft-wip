using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

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

    public static RaycastShoot raycastShoot;
    public static GameObject hitObject;
    public GameObject bullet;
    public GameObject shadowBullet;

    void Start ()
    {
        raycastShoot = this;
        //laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }
	
	void Update ()
    {











        //if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) &&  Time.time>nextFire)
        if (Input.GetMouseButtonDown(0))
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
            }*/
        }
	}

    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        //laserLine.enabled = true;     
        yield return shotDuration;
        //laserLine.enabled = false;
    }


}
