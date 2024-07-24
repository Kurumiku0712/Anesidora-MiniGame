using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

interface BeAttack
{
    void BeAttack(float data);
}
interface GetTipsInfo
{
    string GetTipsInfo();
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Image HPImage;
    public Text HPText;
    public Image DPImage;
    public Text DPText;
    public Image MPImage;
    public Text MPText;
    public GameObject gameOverGO;
    public GameObject gamePauseGO;
    public Button restart;
    public Button continued;
    public Text coinText;

    public GameObject beAttackText;
    public LittleMap littleMap;
    public Transform weaponRecycle;
    public GameObject tips;
    public GameObject tipsImage;
    public List<GameObject> troops;

    public GameObject coinPre;
    public GameObject mpPre;

    PlayerControl player;
    Room room;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       // coolingImage1.raycastTarget = false;
        troops = new List<GameObject>();
        troops.Add(PlayerControl.instance.gameObject);
        //找到所有房间进行初始化
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Room");
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].GetComponent<Room>().Initialization();
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        UpdateCoinNumber();
    }
    void Update()
    {
        if (room != null)
        {
            room.UpdateRoomInfo();
        }

        UpdatePlayerInfo();
        UpdateImage1();
        UpdateImage2();
        UpdateImage3();
        UpdateImage4();
    }
    public void UpdateCoinNumber()
    {
        coinText.text = player.coin.ToString();
    }
    public void UpdatePlayerInfo()
    {
        HPImage.fillAmount = player.playerHP.realValue / player.playerHP.maxValue;
        HPText.text = player.playerHP.realValue + "/" + player.playerHP.maxValue;
        DPImage.fillAmount = player.playerDP.realValue / player.playerDP.maxValue;
        DPText.text = player.playerDP.realValue + "/" + player.playerDP.maxValue;
        MPImage.fillAmount = player.playerMP.realValue / player.playerMP.maxValue;
        MPText.text = player.playerMP.realValue + "/" + player.playerMP.maxValue;
    }
    public void ClearPlayerInfo()
    {
        player.playerHP.realValue = player.playerHP.maxValue;
        player.playerDP.realValue = player.playerDP.maxValue;
        player.playerMP.realValue = player.playerMP.maxValue;
    }
    public void KillPlayer()
    {
        player.playerHP.realValue = 0;
        player.playerDP.realValue = 0;
        player.isDie = true;
    }
    public void GameOver()
    {
        gameOverGO.SetActive(true);
        restart.onClick.AddListener(() => { SceneManager.LoadScene("ch1"); });
    }
    public void Gamepause()
    {
        Time.timeScale = 0;
        gamePauseGO.SetActive(true);
        continued.onClick.AddListener(() => { Time.timeScale = 1; gamePauseGO.SetActive(false); });
    }
    public void ContinueGame()
    {
        continued.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
        });
        }
    public void UpdatePlayerRoom(Room room)
    {
        this.room = room;
        //更新地图显示
        littleMap.UpdatePlayerIndex(room.roomNum);
        if (!room.isExplored)
        {
            littleMap.UpdateCanExploreInMap(room);
            littleMap.UpdateRoomsExploredInMap(room.roomNum);
        }
    }
    public void UpdateTipsInfo(string s)
    {
        tips.SetActive(true);
        tipsImage.SetActive(true);
        tips.GetComponent<Text>().text = s;
    }
    public void CloseTips()
    {
        tips.SetActive(false);
        tipsImage.SetActive(false);
    }
    public void ShowAttack(float data, Vector3 pos)
    {
        GameObject textgo = Instantiate(beAttackText, pos, Quaternion.identity);
        textgo.transform.SetParent(GameObject.Find("Canvas").transform);
        textgo.GetComponent<Text>().text = "-" + data;
    }
    public void Reborn()
    {
        Destroy(PlayerControl.instance.gameObject);
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void GoOn()
    {
        Time.timeScale = 1;
    }
    public void Exit()
    {
        Application.Quit();
    }
    //skill
    //private float currentTime1; //
    public Image coolingImage1;
  //  private float coolingTimer1;

    private void UpdateImage1()
    {
        if (player.isUnbeatable& !player.addHP && !player.berserker && !player.speedUp && player.hasWeapon() != null)//
        {
            // coolingTimer4 = player.skillD_CD;//
            //currentTime4 =Time.time;
            coolingImage1.fillAmount = 1.0f;
        }
        if (Time.time - player.skillA_Timing <= player.skillA_CD)
        {
            //currentTime4 += Time.deltaTime;
            coolingImage1.fillAmount = 1 - (Time.time - player.skillA_Timing) / player.skillA_CD;
        }
    }
 //   private float currentTime2; //
    public Image coolingImage2;//
   // private float coolingTimer2;//

    private void UpdateImage2()//
    {
        if (player.speedUp && !player.addHP && !player.berserker && !player.isUnbeatable && player.hasWeapon() != null)//
        {
            // coolingTimer4 = player.skillD_CD;//
            //currentTime4 =Time.time;
            coolingImage2.fillAmount = 1.0f;
        }
        if (Time.time - player.skillB_Timing <= player.skillB_CD)
        {
            //currentTime4 += Time.deltaTime;
            coolingImage2.fillAmount = 1 - (Time.time - player.skillB_Timing) / player.skillB_CD;
        }
    }
  //  private float currentTime3; //
    public Image coolingImage3;//
   // private float coolingTimer3;//

    private void UpdateImage3()//
    {
        if( player.addHP && !player.speedUp && !player.berserker && !player.isUnbeatable && player.hasWeapon() != null)//
        {
            // coolingTimer4 = player.skillD_CD;//
            //currentTime4 =Time.time;
            coolingImage3.fillAmount = 1.0f;
        }
        if (Time.time - player.skillC_Timing <= player.skillC_CD)
        {
            //currentTime4 += Time.deltaTime;
            coolingImage3.fillAmount = 1 - (Time.time - player.skillC_Timing) / player.skillC_CD;
        }
    }

    public Image coolingImage4;//
  //  private float coolingTimer4;//
    private void UpdateImage4()//
    {
        if (player.berserker&&!player.speedUp && !player.addHP && !player.isUnbeatable && player.hasWeapon() != null)//
        {
           // coolingTimer4 = player.skillD_CD;//
            //currentTime4 =Time.time;
            coolingImage4.fillAmount = 1.0f;
        }
        if (Time.time-player.skillD_Timing <= player.skillD_CD)
        {
            //currentTime4 += Time.deltaTime;
            coolingImage4.fillAmount = 1 - (Time.time - player.skillD_Timing) / player.skillD_CD;
        }
    }
}
