using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster0102 :Monster
{
    public Transform pos;
    public float attackRange;
    public GameObject bulletPre;
    public float trackingRange;
    public int bulletCount;
    public float bulletForce;
    public float CD = 5;
    public int attack;
    float timing;
    MyAstarAI myAI;
    float trackingtiming;
    float strolltiming;
    float attacktiming;
    bool seeTarget;
    LayerMask layerMask;
    RaycastHit2D hit;
    void Start()
    {
        myAI = GetComponent<MyAstarAI>();
        timing = -CD;
        monsterState = MonsterState.Idle;
    }
    void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Tracking:
                Tracking();
                break;
            case MonsterState.Die:
                Die();
                break;
            case MonsterState.Stroll:
                Stroll();
                break;
            default:
                break;
        }
        
    }

    private void Die()
    {
        
    }

    private void Tracking()
    {
        foreach (GameObject item in GameManager.instance.troops)//这里是以不止一个目标为前提写的
        {
            if (Vector2.Distance(item.transform.position, transform.position) < attackRange && Time.time - timing > CD)
            {
                timing = Time.time;
                GetComponent<Animator>().SetTrigger("run");
                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject go = Instantiate(bulletPre, pos.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
                    go.GetComponent<Bullet>().Initialization(attack, role, bulletForce);
                    go.transform.SetParent(GameManager.instance.weaponRecycle);
                }
            }
        }
    }
    void Stroll()
    {
        RaycastDetection();
        if (seeTarget)
        {
            monsterState = MonsterState.Tracking;
        }
       
        myAI.NextTarget();
    }
    private void Idle()
    {
        if (isStart)
        {
            monsterState = MonsterState.Tracking;
        }
    }
    public void RaycastDetection()
    {
        hit = Physics2D.Raycast(transform.position + Vector3.up, (targetPosition.position - (transform.position + Vector3.up)).normalized, trackingRange, layerMask);
        //Debug.DrawLine(transform.position+Vector3.up, transform.position+(targetPosition.position - transform.position).normalized*10, Color.red);

        if (hit.transform != null && hit.transform == targetPosition)
        {
            seeTarget = true;
            Debug.DrawLine(transform.position + Vector3.up, hit.transform.position, Color.red);
        }
        else
        {
            seeTarget = false;
        }
    }
}
