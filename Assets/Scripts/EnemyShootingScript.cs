using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingScript : ShootingScript
{
    // Start is called before the first frame update

    GameObject player;
    public LayerMask notToHit;
    float shootingRange = 6;
    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        fireRate = 1.5f;
    }

    // Update is called once per frame
    public override void Update()
    {


        if (fireRate == 0)
        {
            Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            RaycastHit2D hit = Physics2D.Raycast(firePointPosition, playerPosition - firePointPosition, shootingRange, ~notToHit);
            //Shoot();
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                float angle = Vector2.Angle(playerPosition - firePointPosition, transform.right);
                if (firePointPosition.y > playerPosition.y) angle = -angle;
                Shoot(angle);

            }

        }
        else if (Time.time > timeToFire)
        {
            Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            RaycastHit2D hit = Physics2D.Raycast(firePointPosition, playerPosition - firePointPosition, shootingRange);

            // Shoot();
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                float angle = Vector2.Angle(playerPosition - firePointPosition, transform.right);
                if (firePointPosition.y > playerPosition.y) angle = -angle;
                Shoot(angle);
                timeToFire = Time.time + 1 / fireRate;
            }

        }
    }

    

    public void Shoot(float angle)
    {
        Quaternion bulletRotation;
        bulletRotation = firePoint.rotation;
        Vector3 rot = bulletRotation.eulerAngles;

        rot = new Vector3(rot.x, rot.y, angle);

        bulletRotation = Quaternion.Euler(rot);

        Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        
         SoundManagerScript.PlaySound("rifleShot");
    }
}
