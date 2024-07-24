using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DoorTips : MonoBehaviour
{
    public string ChatName;//这个名字是FlowChart中，块的名字，需要把这个变量设定为flowchart中块的名字
    // Start is called before the first frame update
    public Flowchart flowChart;
    private bool canChat = false;
    public AudioSource music;
    public AudioClip trigger;
    private bool once = true;
    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        canChat = true;
        Debug.Log("碰撞发生");
        if(once)
        {
            music.clip = trigger;
            music.Play();
            once = false;
        }
    
        Debug.Log(canChat);
        
    }
    private void OnCollisionExit2D(UnityEngine.Collision2D other)
    {
        canChat = false;

    }
    private void Update()
    {
        if(canChat)
        Say();
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
            canChat = false;
        }

    }
}