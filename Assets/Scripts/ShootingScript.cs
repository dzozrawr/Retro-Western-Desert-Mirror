using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    protected float fireRate = 3f;
    protected float damage = 10f;

    public GameObject bulletPrefab = null;

    // public float bulletSpeed = 270f;

    protected float timeToFire = 0;

    protected Transform firePoint;

    private bool threeBulletPowUpOn = false;
    private float threeBulletAngle = 20;
    private const float threeBulletPowUpDurationVal= 10f;
    private float threeBulletPowUpDuration= threeBulletPowUpDurationVal;

    // Start is called before the first frame update
    public virtual void Start()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint.");
        }

        if (bulletPrefab == null)
        {
            Debug.LogError("No bulletPrefab.");
        }

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else if (Input.GetButtonDown("Fire1") && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }

        //power up management
        if (threeBulletPowUpOn)
        {
            threeBulletPowUpDuration -=Time.deltaTime;
            if (threeBulletPowUpDuration <= 0)
            {
                threeBulletPowUpOn = false;
                threeBulletPowUpDuration = threeBulletPowUpDurationVal;
            }
        }
    }

    public void setThreeBulletPowUpOn(bool on)
    {
        threeBulletPowUpOn = on;
    }

    public bool getThreeBulletPowUpOn()
    {
        return threeBulletPowUpOn;
    }

    public virtual void Shoot()
    {

        Quaternion bulletRotation;
        bulletRotation = firePoint.rotation;

        if (transform.localScale.x < 0)
        {
            Vector3 rot = bulletRotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            bulletRotation = Quaternion.Euler(rot);
        }

        if (!threeBulletPowUpOn)
        {

            Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        }
        else
        {
            Quaternion bulletRotation1= firePoint.rotation;
            Quaternion bulletRotation2 = firePoint.rotation;
            Vector3 rot1 = bulletRotation1.eulerAngles;
            Vector3 rot2 = bulletRotation2.eulerAngles;
            if (transform.localScale.x < 0)
            {               
                rot1 = new Vector3(rot1.x, rot1.y + 180, rot1.z+ threeBulletAngle);
                rot2 = new Vector3(rot2.x, rot2.y + 180, rot2.z - threeBulletAngle);
            }
            else
            {
                rot1 = new Vector3(rot1.x, rot1.y , rot1.z + threeBulletAngle);
                rot2 = new Vector3(rot2.x, rot2.y, rot2.z - threeBulletAngle);
            }
            bulletRotation1 = Quaternion.Euler(rot1);
            bulletRotation2 = Quaternion.Euler(rot2);
            Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            Instantiate(bulletPrefab, firePoint.position, bulletRotation1);
            Instantiate(bulletPrefab, firePoint.position, bulletRotation2);
        }


        SoundManagerScript.PlaySound("gunShot");
    }
}
