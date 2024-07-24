using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    //将要显示的背包拖入
    public GameObject Menu;
    //背包状态
    private bool MenuState;


    void Update()
    {
        //调用判断方法
        OpenMenu();


    }

    private void OpenMenu()
    {
        
        //默认状态赋值为false
        MenuState = Menu.activeSelf;
        //如果按下按键i
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //将状态设置为false的方面状态true
            MenuState =! MenuState;
            //将背包的状态设置为状态值
            Menu.SetActive(MenuState);
            Time.timeScale = 0;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //将状态设置为false的方面状态true
            MenuState = false;
            //将背包的状态设置为状态值
            Menu.SetActive(MenuState);
            Time.timeScale = 1;

        }


    }
}