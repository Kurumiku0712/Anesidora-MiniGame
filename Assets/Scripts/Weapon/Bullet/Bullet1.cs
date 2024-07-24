using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : Bullet
{
    public override void Initialization(float attack, string role, float bulletForce)
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
        this.attack = attack;
        this.role = role;
    }
}
