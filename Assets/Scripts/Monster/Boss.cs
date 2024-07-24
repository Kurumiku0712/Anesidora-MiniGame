using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BossState
{
    Idle,
    Stroll,
    Second_Idle,
    Attack,
    Die,
    Pure,
    Unbeatable
      

}
public class Boss : MonoBehaviour, BeAttack
{
    public float hp;
    public GameObject smallui;
    public float currentHP;
    public int coin;
    public int magic;
    public GameObject trap;
    protected string role = "Monster";
    public bool isStart = false;
    protected Transform targetPosition;
    protected Room room;
    private BoxCollider2D boxCollider2D;
    public AudioSource sound;
    public AudioClip change;
    public float volum = 0.7f;
    private bool playsound = true;//先声明

    protected BossState bossState;
    public bool isUnbeatable = false;


    public BossState getBossState()
    {
        return this.bossState;
    }




    public virtual void BeAttack(float data)
    {
        if(bossState == BossState.Unbeatable)
        {
            hp -= 0;
        }

        else
        {
            if (hp > currentHP / 2)
            {
                bossState = BossState.Idle;
                GetComponent<Animator>().SetBool("isFirst", true);
            }
            if (hp <= currentHP / 2)
            {
                bossState = BossState.Second_Idle;
                GetComponent<Animator>().SetBool("isHenshin", true);
                GetComponent<Animator>().SetBool("isFirst", false);
                GetComponent<Animator>().SetBool("isSecond", true);
                boxCollider2D = this.GetComponent<BoxCollider2D>();
                boxCollider2D.size = new Vector2(0.65f, 0.5f);
                boxCollider2D.offset = new Vector2(0f, 0f);
                trap.SetActive(true);
                if (playsound)
                {
                    sound.clip = change;
                    sound.volume = volum;
                    sound.Play();
                    playsound = false;
                }
            }
            if (hp <= 0)
            {
                smallui.SetActive(false);
                bossState = BossState.Die;
                GetComponent<Animator>().SetBool("isDie", true);
                GetComponent<Collider2D>().enabled = false;
                room.BossDie(this);
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

                if (!isUnbeatable)
                {
                    GetComponent<Animator>().Play("BeAttack");
                    GameManager.instance.ShowAttack(data, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0)));
                }
            }

            hp -= data;


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
}
