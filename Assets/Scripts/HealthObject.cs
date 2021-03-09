using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthObject : MonoBehaviour  //also contains redFlash animation
{
    protected float hp;

    protected Material matRed;
    protected Material matDefault=null;
    protected SpriteRenderer sr=null;

    public virtual void Start()
    {
        matRed = Resources.Load("RedFlash", typeof(Material)) as Material;
    }

    public virtual void receiveDmg(float dmg)
    {
        hp -= dmg;
        sr.material = matRed;
        if (hp <= 0) Destroy(gameObject);
        else
        {
            Invoke("ResetMaterial", .1f);
        }
    }

    public virtual void ResetMaterial()
    {
        sr.material = matDefault;
    }

}
