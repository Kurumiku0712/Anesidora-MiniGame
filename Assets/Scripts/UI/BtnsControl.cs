using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class BtnsControl : MonoBehaviour
{
    public string cypher;
    public GameObject Locker;
    public GameObject finalWeapon;
    public GameObject mechanism;
    private bool isRight=false;
    public GameObject ok;
    public GameObject cancel;
    public GameObject quit;
    public AudioSource music;
    public AudioClip success;
    public AudioClip error;
    public AudioClip click;
    //line render 相关
    [Tooltip("Line renderer used for the line drawing.")]
    public LineRenderer line;
    private int lineVertexIndex = 0;

    //密码锁的按钮组
    public Button[] btns;

    public Button btnOK;

    //密码保存
    public List<string> password = new List<string>();
    //public List<string> password;
    // Use this for initialization
    void Start()
    {
        
        //绑定按钮事件，这里随便试了一个
        for (int i = 0; i < btns.Length; i++)
        {
            UIEventListener btnListenser = btns[i].GetComponent<UIEventListener>();
            btnListenser.OnMouseEnter += whenMouseEnter;
            btnListenser.OnMouseDown += whenMouseDown;
            //btnListenser.OnMouseExit += whenMouseExit;
        }
        UIEventListener btnokListenser = btnOK.GetComponent<UIEventListener>();
        btnokListenser.OnClick += OnOKBtnClick;
    }

    
    #region 处理开始和结束
    bool IsStart;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            IsStart = true;
        }
        else
        {
            IsStart = false;
        }
    }
    #endregion

    void whenMouseEnter(GameObject go)
    {
        if (IsStart == false)
            return;

        Debug.Log("enter button  " + go.name);
        //go.GetComponent<Image>().color = go.GetComponent<Button>().colors.highlightedColor;
        music.clip = click;
        music.Play();
        int i = int.Parse(go.name);

        password.Add(go.name);

        DrawLines(i - 1);
    }

    #region 处理意外
    //处理鼠标按下时，刚好在按钮上的意外
    void whenMouseDown(GameObject go)
    {
        IsStart = true;
        whenMouseEnter(go);
    }
    #endregion

    // 画按钮之间的连线
    void DrawLines(int btnIndex)
    {
        if (line != null)
        {
            lineVertexIndex++;
            line.positionCount =lineVertexIndex;

            Vector3 cursorPos = btns[btnIndex].gameObject.transform.position;
            cursorPos.z = 0f;

            Vector3 cursorSpacePos = Camera.main.ScreenToWorldPoint(cursorPos);
            cursorSpacePos.z = 0f;
            line.SetPosition(lineVertexIndex - 1, cursorSpacePos);
        }
    }

    //输出设置的密码，可做后续处理，保存校对的啥的，可以继续延伸
    void OnOKBtnClick(GameObject go)
    {
        string pass = "";
        foreach (var item in password)
        {
            pass += item;
        }
        GameObject.Find("Canvas/lOCKER/Image/word").GetComponent<Text>().text = pass;
        if (pass==cypher)
        {
            //isRight = true;
            GameObject.Find("Canvas/lOCKER/Image/Text").GetComponent<Text>().text="密码正确";
            isRight = true;
            finalWeapon.SetActive(true);
            mechanism.SetActive(false);
            ok.SetActive(false);
            music.clip = success;
            music.Play();
            cancel.SetActive(false);
            quit.SetActive(true);

        }
        else
        {
            GameObject.Find("Canvas/lOCKER/Image/Text").GetComponent<Text>().text = "密码错误";
            music.clip = error;
            music.Play();
            password.Clear();

        }
        
        Debug.Log("您设置的密码是：" + pass);
    }
   
}