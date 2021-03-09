using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBulletScript : BulletScript
{
    // Start is called before the first frame update
    void Start()
    {
        dmg = 10f;
        bulletSpeed = 5f;
        timeOfDeath = Time.time + lifeTimeInSec;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
