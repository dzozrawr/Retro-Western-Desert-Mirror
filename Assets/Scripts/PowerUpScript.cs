using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2D");
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<ShootingScript>().setThreeBulletPowUpOn(true);
            SoundManagerScript.PlaySound("threeBulletPowUpSound");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
