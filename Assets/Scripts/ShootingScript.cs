using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public float fireRate = 0f;
    public float damage = 10f;
    public LayerMask whatToHit;

    public GameObject bulletPrefab=null;

    public float bulletSpeed=250f;

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
        else if (Input.GetButtonDown("Fire1")&& Time.time>timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet= Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);

        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed, 0f));
    }
}
