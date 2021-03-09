using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    protected float dmg;

    public virtual float getDamage()
    {
        return dmg;
    }

}
