using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class SpecificValue
{
    public float realValue;
    public float maxValue;
    public float specificValue
    {
        get { return realValue / maxValue; }
    }
}
public class PlayerControl : MonoBehaviour, BeAttack
{
   // public bool isDestroy;
    public GameObject smallui;
    public static PlayerControl instance;
    public SpecificValue playerHP;
    public SpecificValue playerDP;
    public SpecificValue playerMP;
    public int coin;
    public string chapterPassword;
    //move
    public float moveSpeed;
    public float dpCD1;
    public float dpCD2;
    public float dpSpeed;

    public GameObject playerPrefab;
    public bool isDie;
    private Rigidbody2D myRigidbody;
    private Weapon weapon;
    private Animator playerAnima;
    GameObject myPlayer;
    public GameObject myWeapon;
    Vector2 movement;
    Vector2 target;

    bool fireKeyDown;
    bool fireKeyUp;
    bool fireKeyPressed;
    bool weaponKeyDown;
    GameObject weaponInFloor;
    List<GameObject> nearWeapons = new List<GameObject>();
    float timing;
    float dpTiming1;
    float dpTiming2;
    bool dpRestore = false;
    private AudioSource music;
    private AudioClip move;
    private AudioClip death;
    public string musicName;
    public string deathMusic;
    //private bool isLoop=true;

    public Weapon hasWeapon()
    {
        return this.weapon;
    }

    private void Awake()
    {
        instance = this;
        //给对象添加一个AudioSource组件
        music = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        music.playOnAwake = false;
        //加载音效文件，我把跳跃的音频文件命名为jump
        move = Resources.Load<AudioClip>("Audio/" + musicName);
        death = Resources.Load<AudioClip>("Audio/" + deathMusic);
    }

  private void Start()
    {
        //isLoop = true;
        myPlayer = Instantiate(playerPrefab, transform);
        playerAnima = myPlayer.GetComponent<Animator>();
        myRigidbody = transform.GetComponent<Rigidbody2D>();
        isDie = false;
        if(playerHP.maxValue > 0)
        {
           DontDestroyOnLoad(gameObject);
        }
        else
        {        
            DestroyImmediate(gameObject);
        }
    }
    void Update()
    {
        skillA();
        skillB();
        skillC();           
        skillD();
        
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    GameManager.instance.ShowAttack(1, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0)));    
        //}
        fireKeyDown = Input.GetMouseButtonDown(1);
        fireKeyUp = Input.GetMouseButtonUp(1);
        weaponKeyDown= Input.GetKeyUp(KeyCode.E);
        if (fireKeyDown)
        {
            timing = 0;
        }
        if (Input.GetMouseButton(1))
        {
            timing += Time.deltaTime;
        }
        if (fireKeyUp)
        {
            timing = 0;
        }
        if (timing >= 0.2)
        {
            fireKeyPressed = true;
        }
        else
        {
            fireKeyPressed = false;
        }
        if (!isDie)
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            SetLookAt();
            if (movement == new Vector2(0, 0))
            {
                playerAnima.SetBool("run", false);
            }
            else
            {
                playerAnima.SetBool("run", true);
            }
            Ray2DForCircle();
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                music.clip = move;
                music.loop = true;
                music.Play();

            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                music.clip = move;
                music.loop = false;
                music.Stop();
            }
            if (weaponInFloor != null && weaponKeyDown)
            {
                GetWeapon();
            }
            else
            {
                if (fireKeyDown)
                {
                    if (weapon != null && canShoot == true)
                    {
                        weapon.ShootButtonDown();
                    }
                    else
                    {
                        //Debug.Log("没有武器只能手刀撒");
                    }
                }
                else if (fireKeyPressed)
                {
                    if (weapon != null && canShoot == true)
                    {
                        weapon.ShootButtonPressed();
                    }
                }
                if (fireKeyUp)
                {
                    if (weapon != null)
                    {
                        weapon.ShootButtonUp();
                    }
                }
            }
            if (Time.time - dpTiming1 > dpCD1 && dpRestore && Time.time - dpTiming2 > dpCD2)//脱离战场&&防御值需要恢复&&恢复CD完成
            {
                dpTiming2 = Time.time;
                playerDP.realValue+=dpSpeed;
                if (playerDP.realValue >= playerDP.maxValue)
                {
                    playerDP.realValue = playerDP.maxValue;
                    dpRestore = false;
                }
            }

        }
    }
    private void FixedUpdate()
    {
        if (!isDie)
        {
            myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

    }

    void GetWeapon()
    {
        if (weaponInFloor != null)//地上有枪就换枪
        {
            if (weapon != null)
            {
                myWeapon.transform.SetParent(GameManager.instance.weaponRecycle);
                weapon.PickDown();
            }
            myWeapon = weaponInFloor;
            weapon = myWeapon.GetComponent<Weapon>();
            weapon.Pickup();
            myWeapon.transform.SetParent(transform);
            myWeapon.transform.localPosition = new Vector3(0, 0, 0);
            myWeapon.transform.localRotation = Quaternion.identity;
            //注册
            weapon.Initialization(gameObject.tag, gameObject.layer);
        }
    }
    void SetLookAt()
    {
        if (myRigidbody.position.x > target.x)
        {
            myPlayer.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (myRigidbody.position.x < target.x)
        {
            myPlayer.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (weapon != null)
        {
            weapon.UpdateLookAt(target);
        }
    }

    /// <summary>
    /// 圆形检测
    /// </summary>
    public void Ray2DForCircle()
    {
        weaponInFloor = null;
        GameManager.instance.CloseTips();
        //int layer = 1 << LayerMask.NameToLayer("RayLayer");
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, 0.5f, 0), 0.9f);

        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag("Weapon"))
                {
                    weaponInFloor = cols[i].gameObject;
                }
                if (cols[i].GetComponent<GetTipsInfo>() != null)
                {
                    GameManager.instance.UpdateTipsInfo(cols[i].GetComponent<GetTipsInfo>().GetTipsInfo());
                }
            }
        }
    }

    public void BeAttack(float data)
    {
        dpRestore = true;
        dpTiming1 = Time.time;

        if (isUnbeatable)
        {
            playerHP.realValue -= 0;
            playerDP.realValue -= 0;
        }

        else
        {
            if (playerDP.realValue == 0)
            {
                playerHP.realValue -= data;
            }
            else if (playerDP.realValue < data)
            {
                playerHP.realValue -= data - playerMP.realValue;
                playerDP.realValue = 0;
            }
            else
            {
                playerDP.realValue -= data;
            }
            if (playerHP.realValue <= 0)
            {
                smallui.SetActive(false);
                playerAnima.SetBool("die", true);
                isDie = true;
                music.clip = death;
                music.loop = false;
                music.Play();
                GameManager.instance.GameOver();
                GetComponent<Collider2D>().enabled = false;
                GameManager.instance.ClearPlayerInfo();
                //Destroy(myPlayer);
                Destroy(myWeapon);
            }
            else
            {
                if (!isUnbeatable && !speedUp && !addHP && !berserker)
                {
                    playerAnima.Play("BeAttack");
                    GetComponent<CinemachineImpulseSource>().GenerateImpulse();
                    GameManager.instance.ShowAttack(data, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0)));
                }
            }
        }

       
    }
    /*void Des()
    {
        if(isDestroy)
        {
            myWeapon.SetActive(false);
        }
    }
    */




    // 4个技能

    public bool isUnbeatable = false;
    public float unbeatableTime;
    public float skillA_Timing = -60000;
    public float skillA_CD = 60000;

    public bool canShoot = true;
  

    // 技能1:按1后进入无敌状态持续10s有60000s的CD
    // 技能释放期间无法进行攻击
    public void skillA()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time - skillA_Timing >= skillA_CD && !speedUp && !addHP && !berserker&&weapon!=null)
        {
            skillA_Timing = Time.time;
            isUnbeatable = true;
            unbeatableTime = 10;

        }

        if (isUnbeatable)
        {
            playerAnima.SetBool("isUnbeatable", true);

            canShoot = false;
            unbeatableTime -= Time.deltaTime;

            if (unbeatableTime <= 0)
            {
                isUnbeatable = false;
                canShoot = true;
                playerAnima.SetBool("isUnbeatable", false);

            }
        }
       
       

    }

    public bool speedUp = false;
    float speedUpTime;
    public float skillB_Timing = -30;
    public float skillB_CD = 30;
   

    // 技能2:按2后移动速度加倍持续10s有30s的CD
    public void skillB()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time - skillB_Timing >= skillB_CD && !isUnbeatable && !addHP && !berserker&&weapon != null)
        {
            skillB_Timing = Time.time;
            speedUp = true;
            speedUpTime = 10;
           

        }

        if (speedUp)
        {
            playerAnima.SetBool("isSpeedUp", true);
            moveSpeed = 12f;

            speedUpTime -= Time.deltaTime;

            if (speedUpTime <= 0)
            {
                speedUp = false;
                moveSpeed = 6.5f;
                playerAnima.SetBool("isSpeedUp", false);
            }
        }
        

    }

    public bool addHP = false;
    float addHPTime;
    public float skillC_Timing = -60;
    public float skillC_CD = 60;
   
    // 技能3:按3后治疗恢复1500点血量
    public void skillC()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3)&&Time.time - skillC_Timing >= skillC_CD && !speedUp && !isUnbeatable && !berserker && weapon != null)
        {
            skillC_Timing = Time.time;
            addHP = true; 
            addHPTime = 0.1f;
        }

        if (addHP)
        {
            playerAnima.Play("Heal");


            if (playerHP.realValue <= playerHP.maxValue - 1500)
            {
                playerHP.realValue += 1500;

            }
            else
            {
                playerHP.realValue = playerHP.maxValue;
            }
            addHP = false;
            //addHPTime -= Time.deltaTime;


        }
        /*if (!addHP)
        {
            //addHP = false;
            playerAnima.SetBool("addHP", false);

        }*/
       
    }

    public bool berserker = false;
    float berserkerTime;
    public float skillD_Timing = -30;
    public float skillD_CD = 30;
    
    // 技能4:按4后攻速增加持续3s有30s的CD
    public void skillD()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && Time.time - skillD_Timing >= skillD_CD && !speedUp && !addHP && !isUnbeatable && weapon != null)
        {
            skillD_Timing = Time.time;
            berserker = true;
            berserkerTime = 3;
        }

        if (berserker)
        {
            playerAnima.SetBool("isBerserker", true);
           
            weapon.setCD2();

            berserkerTime -= Time.deltaTime;

            if (berserkerTime <= 0)
            {
                berserker = false;
                weapon.setCD();
                playerAnima.SetBool("isBerserker", false);

            }
        }
       
    }
    
}
