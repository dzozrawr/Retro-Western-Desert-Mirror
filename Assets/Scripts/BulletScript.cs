using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : DamagingObject
{
    protected float lifeTimeInSec = 2f;
    protected float timeOfDeath;
    public float bulletSpeed = 7f;
    
   // public LayerMask notToHit;
    // Start is called before the first frame update
    void Start()
    {
        dmg = 5f;
        timeOfDeath = Time.time + lifeTimeInSec;

    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime* bulletSpeed);
        if(Time.time>= timeOfDeath)
        {
            Destroy(gameObject);
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }//else if?        
    }



   // public void refreshLife()
   // {

  //  }
}
