using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthObject : MonoBehaviour
{
    protected float hp;

    public abstract void receiveDmg(float dmg);

}
