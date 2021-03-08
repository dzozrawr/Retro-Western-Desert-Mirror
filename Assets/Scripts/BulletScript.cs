using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float lifeTimeInSec = 2f;
    float timeOfDeath;
    public float bulletSpeed = 7f;
    public float dmg = 5f;
   // public LayerMask notToHit;
    // Start is called before the first frame update
    void Start()
    {
        timeOfDeath = Time.time + lifeTimeInSec;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime* bulletSpeed);
        if(Time.time>= timeOfDeath)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMirrorScript>()!=null)
        {
            collision.gameObject.GetComponent<EnemyMirrorScript>().receiveDmg(dmg);
        }
        Destroy(gameObject);
    }


}
