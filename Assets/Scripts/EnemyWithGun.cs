using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithGun : HealthObject
{

    bool facingRight = true;
    GameObject player;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        hp = 25f;
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<DamagingObject>() != null)
        {
            if (col.gameObject.CompareTag("Bullet"))
            {
                GameObject bullet = col.gameObject;

                receiveDmg(bullet.GetComponent<DamagingObject>().getDamage());
                Destroy(bullet);
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.x < transform.position.x) && (facingRight))
        {
            flip();
        }
        else if ((player.transform.position.x > transform.position.x) && (!facingRight))
        {
            flip();
        }
    }

    void flip()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        transform.rotation = Quaternion.Euler(rot);
        facingRight = !facingRight;
    }
}
