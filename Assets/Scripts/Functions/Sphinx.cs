using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Sphinx : MonoBehaviour
{
    public string ChatName;//这个名字是FlowChart中，块的名字，需要把这个变量设定为flowchart中块的名字
    // Start is called before the first frame update
    public Flowchart flowChart;
    private bool canChat = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        canChat = true;
        Debug.Log("碰撞发生");
        Debug.Log(canChat);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canChat = false;
      
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Say();
        }
    }
    private void OnMouseDown()
    {
        Say();
    }
    void Say()
    {
        if (canChat)
        {
            if (flowChart.HasBlock(ChatName))
            {
                //执行对话
                flowChart.ExecuteBlock(ChatName);
            }
        }
    }
}