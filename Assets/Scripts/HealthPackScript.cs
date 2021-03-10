using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : PickUpable
{
    float hpAmount = 20;
    public override void effect(Collider2D col)
    {
        col.gameObject.GetComponent<CharacterController>().receiveHp(hpAmount);
        SoundManagerScript.PlaySound("healthPackSound");
        Destroy(gameObject);
    }
}
