using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulllet2 : Bullet
{
    public float lifeTimeMin ;
    public float lifeTimeMax;
    float lifeTime;
    float timing;

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, new Vector2(0, 0), 0.1f);
        if (Time.time-timing >=lifeTime)
        {
            Destroy(gameObject);
        }
    }
    public override void Initialization(float attack, string role, float bulletForce)
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * Random.Range(bulletForce*0.8f, bulletForce), ForceMode2D.Impulse);
        this.attack = attack;
        this.role = role;
        lifeTime = Random.Range(lifeTimeMin, lifeTimeMax);
        timing = Time.time;
    }
}
