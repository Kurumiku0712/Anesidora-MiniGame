using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowHorse : Weapon
{
    public LineRenderer line;
    public Transform pos;
    public float CD = 1;
    RaycastHit2D hit;
    LayerMask layerMask;
    public override void ShootButtonUp()
    {
        line.SetPosition(0, pos.position);
        line.SetPosition(1, pos.position);
    }
    public override void ShootButtonPressed()
    {
        hit = Physics2D.Raycast(pos.position, pos.right,100,layerMask);
        if (hit.transform != null)
        {
            Debug.Log(hit.transform.name);
            line.SetPosition(0, pos.position);
            line.SetPosition(1, hit.point);
            if (hit.transform.tag != role&&hit.transform.GetComponent<BeAttack>()!= null)
            {
                hit.transform.GetComponent<BeAttack>().BeAttack(attack);
            }
            GetComponent<Animator>().Play("Shoot");
        }
    }
    public override void UpdateLookAt(Vector3 target)
    {
        transform.right = (target - transform.position).normalized;
        if (transform.position.x > target.x)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else if (transform.position.x < target.x)
        {
            GetComponent<SpriteRenderer>().flipY = false;
        }
    }
    public override void Initialization(string role, int layer)
    {
        base.Initialization(role, layer);
        layerMask =~(1<<layer)&~LayerMask.GetMask("Room")& ~LayerMask.GetMask("Weapon");//可以被穿透的层
    }
}
