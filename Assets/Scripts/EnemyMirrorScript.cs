using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMirrorScript : HealthObject
{
    private const float movingTimeValue = 4f;
    private const float standingTimeValue = 2f;

    public float movementSpeed = 0.8f;
    private float movingTime = movingTimeValue;
    private float standingTime = standingTimeValue;
    private bool facingLeft = true;

    private Material matRed;
    private Material matDefault;
    SpriteRenderer sr;

    public GameObject bulletPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        hp = 10f;
        sr = GetComponent<SpriteRenderer>();
        matRed = Resources.Load("RedFlash", typeof(Material)) as Material;
        matDefault = sr.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingTime > 0)
        {
            movingTime -= Time.deltaTime;
            transform.Translate(Vector2.left * Time.deltaTime * movementSpeed);
        }
        else if (standingTime > 0)
        {
            standingTime -= Time.deltaTime;
        }
        else
        {
            flip();
            movingTime = movingTimeValue;
            standingTime = standingTimeValue;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<DamagingObject>()!=null)
        {
            if (col.gameObject.CompareTag("Bullet"))
            {
                GameObject bullet = col.gameObject;
                if (col.otherCollider.GetType() == typeof(CapsuleCollider2D))
                {
                    Vector3 rot = bullet.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x, rot.y + 180, rot.z);
                    bullet.transform.rotation = Quaternion.Euler(rot);
                }
                else // if BoxCollider2D
                {
                    //novi script za svaki novi damage type?
                    receiveDmg(bullet.GetComponent<DamagingObject>().getDamage());
                    Destroy(bullet);
                }
            }
        }
    }

    void flip()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        transform.rotation = Quaternion.Euler(rot);
        facingLeft = !facingLeft;
    }

    public override void receiveDmg(float dmg)
    {
        hp -= dmg;
        sr.material = matRed;
        if (hp <= 0) Destroy(gameObject);
        else
        {
            Invoke("ResetMaterial", .1f);
        }
    }
    // if (facingLeft && col.transform.position.x < transform.position.x) Debug.Log("Reflect!");
    // else if (!facingLeft && col.transform.position.x > transform.position.x) Debug.Log("Reflect!");


    void ResetMaterial()
    {
        sr.material = matDefault;
    }
}
