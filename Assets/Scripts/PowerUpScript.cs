using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : PickUpable
{
    public override void effect(Collider2D col)
    {
        col.gameObject.GetComponent<ShootingScript>().setThreeBulletPowUpOn(true);
        SoundManagerScript.PlaySound("threeBulletPowUpSound");
        Destroy(gameObject);
    }


}
