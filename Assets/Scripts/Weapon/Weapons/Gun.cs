using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject bulletPrefab;
    public float bulletForce;
    public Transform pos;
    public float CD;
    public float cd1;
    public float cd2;
    public float timing;
    public string shoot;
    public string idle;
    private AudioSource music;
    private AudioClip fire;
    public string musicName;
    public float volum = 0.7f;
    private void Awake()
    {
        //给对象添加一个AudioSource组件
        music = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        music.playOnAwake = false;
        //加载音效文件，我把跳跃的音频文件命名为jump
        fire  = Resources.Load<AudioClip>("Audio/" + musicName);
    }
    public override void setCD()
    {
        CD = cd1;
    }

    public override void setCD2()
    {
        CD = cd2;
    }


    public override void ShootButtonDown()
    {
        if (Time.time - timing >= CD)
        {
            timing = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, pos.position, pos.rotation * Quaternion.AngleAxis(Random.Range(0, shake), Vector3.forward));
            bullet.GetComponent<Bullet>().Initialization(attack, role, bulletForce);
            music.clip = fire;
            music.volume = volum;
            //music.loop = true;
            music.Play();
            //射击动画效果
            GetComponent<Animator>().Play(shoot);
        }
    }
    public override void ShootButtonPressed()
    {
        if (Time.time - timing >= CD)
        {
            timing = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, pos.position, pos.rotation * Quaternion.AngleAxis(Random.Range(0, shake), Vector3.forward));
            bullet.GetComponent<Bullet>().Initialization(attack, role, bulletForce);
            music.clip = fire;
            music.volume = volum;
            //music.loop = true;
            music.Play();
            //射击动画效果
            GetComponent<Animator>().Play(shoot);
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
}
