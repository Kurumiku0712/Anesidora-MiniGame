using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWand : Weapon
{
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;

    public float bulletForce;
    public Transform pos;
    public float CD = 1;
    public float angle;
    public int magnification;
    float timingCD = 0;
    public Boss boss;
    public float attack2;
    public float attack3=2000;
    public override void ShootButtonDown()
    {
        if (Time.time - timingCD >= CD && target != null)
        {
            timingCD = Time.time;
            Vector3 vec = pos.rotation.eulerAngles + new Vector3(0, 0, angle * 2);

            if (boss.hp <= boss.currentHP / 2 && boss.hp >= boss.currentHP / 3)
            {
                for (int i = 0; i < 36; i++)
                {
                    GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.Euler(vec - new Vector3(0, 0, angle * i)));
                    bullet.GetComponent<Bullet>().Initialization(attack, role, bulletForce);
                }
            }
            else if (boss.hp <= boss.currentHP / 3)
            {
                for (int i = 0; i < 36; i++)
                {
                    GameObject bullet = Instantiate(bulletPrefab2, pos.position, Quaternion.Euler(vec - new Vector3(0, 0, angle * i)));
                    bullet.GetComponent<Bullet>().Initialization(attack, role, bulletForce);
                }
            }

            else
            {
                for (int i = 0; i < 5; i++)
                {
                    GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.Euler(vec - new Vector3(0, 0, angle * i)));
                    bullet.GetComponent<Bullet>().Initialization(attack, role, bulletForce);
                }
            }


            //射击动画效果
            GetComponent<Animator>().Play("Shoot");
        }

        if (boss.hp <= boss.currentHP / 2)
        {
            
            attack = attack2;
        }
        if (boss.hp <= boss.currentHP /3)
        {

            attack = attack3;
        }
    }
    public override void UpdateLookAt(Vector3 target)
    {
        pos.transform.right = (target - pos.transform.position).normalized;
        Debug.LogWarning(target);
        if (transform.position.x > target.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x < target.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
