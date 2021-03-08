using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMirrorScript : MonoBehaviour
{
    private const float movingTimeValue = 4f;
    private const float standingTimeValue = 2f;

    public float hp = 10f;
    public float movementSpeed=0.1f;
    private float movingTime = movingTimeValue;
    private float standingTime = standingTimeValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingTime > 0)
        {
            movingTime -= Time.deltaTime;
            transform.Translate(Vector2.left * Time.deltaTime * movementSpeed);
        } else if(standingTime > 0)
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

    void flip()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        transform.rotation = Quaternion.Euler(rot);
    }

    public void receiveDmg(float dmg)
    {
        hp -= dmg;
        if (hp <= 0) Destroy(gameObject);
    }
}
