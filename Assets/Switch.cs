using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class Switch : MonoBehaviour
{
    private bool isOn;
    //private bool on;
    public GameObject globalLight;
   // public GameObject LS;
    public string FlowName;
    public string ChatName;//这个名字是FlowChart中，块的名字，需要把这个变量设定为flowchart中块的名字
    // Start is called before the first frame update
    private bool canChat = false;
    public AudioSource music;
    public AudioClip open;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOn = true;
        canChat = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isOn = false;
        canChat = false;
    }
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TurnOn();
            Say();
          //  LS.GetComponent<Collider2D>().enabled = false;
        }
       
    }
    void TurnOn()
    {
        if (isOn)
        {
            music.clip = open;
            music.Play();
            globalLight.SetActive(true);
            GetComponent<Animator>().Play("lightingSwitchOn");
            
        }
        
    }
    void Say()
    {
        if (canChat)
        {
            //对话
            Flowchart flowChart = GameObject.Find(FlowName).GetComponent<Flowchart>();
            if (flowChart.HasBlock(ChatName))
            {
                //执行对话
                flowChart.ExecuteBlock(ChatName);
            }
        }
    }
    /* void TurnOff()
     {
         if (isOn)
         {
             globalLight.SetActive(false);
             GetComponent<Animator>().Play("lightingSwitch");
             on = true;
         }
     }*/
}
