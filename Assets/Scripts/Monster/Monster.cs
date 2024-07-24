using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MonsterState
{
    Idle,
    Tracking,
    Stroll,
    Attack,
    Die
}
public class Monster : MonoBehaviour, BeAttack
{
    public float hp;
    public int coin;
    public int magic;
    public GameObject smallui;
    protected string role = "Monster";
    public bool isStart =false;
    protected Transform targetPosition;
    protected Room room;
    protected MonsterState monsterState;
    private AudioSource music;
    private AudioClip death;
    public string deathMusic;
    private void Awake()
    {
        //给对象添加一个AudioSource组件
        music = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        music.playOnAwake = false;
        //加载音效文件，我把跳跃的音频文件命名为jump
        death  = Resources.Load<AudioClip>("Audio/" + deathMusic);
    }
    public virtual void BeAttack(float data)
    {
        hp -= data;
        if (hp <= 0)
        {
            monsterState = MonsterState.Die;
            smallui.SetActive(false);
            GetComponent<Animator>().SetBool("die", true);
            GetComponent<Collider2D>().enabled = false;
            room.MonsterDie(this);
            music.clip = death;
            music.Play();
            //Invoke("Die", 1.0f);
            for (int i = 0; i < coin; i++)
            {
                Instantiate(GameManager.instance.coinPre, transform.position, Quaternion.identity);
            }
            for (int i = 0; i < magic; i++)
            {
                Instantiate(GameManager.instance.mpPre, transform.position, Quaternion.identity);
            }
        }
        else
        {
            GetComponent<Animator>().Play("BeAttack");
            GameManager.instance.ShowAttack(data, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0)));
        }
    }
    void Die()
    {
        Destroy(gameObject);

    }
    public virtual void Initialization(Room room)
    {
        this.room = room;
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //boss死亡小怪死亡
    public void die()
    {
        monsterState = MonsterState.Die;
        GetComponent<Animator>().SetBool("die", true);
        GetComponent<Collider2D>().enabled = false;
    }

    public MonsterState getMonsterState()
    {
        return this.monsterState;
    }
}
