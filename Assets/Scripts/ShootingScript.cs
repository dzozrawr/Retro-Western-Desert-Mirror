using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public float fireRate = 3f;
    public float damage = 10f;

    public GameObject bulletPrefab = null;

   // public float bulletSpeed = 270f;

    float timeToFire = 0;

    Transform firePoint;

    // Start is called before the first frame update
    void Start()
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
    void Update()
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
    }

    void Shoot()
    {

        Quaternion bulletRotation;
        bulletRotation = firePoint.rotation;

        if (transform.localScale.x < 0)
        {
            Vector3 rot = bulletRotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            bulletRotation = Quaternion.Euler(rot);
        }

        Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        SoundManagerScript.PlaySound("gunShot");
    }
}
