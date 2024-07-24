using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class EnLighten3 : MonoBehaviour
{
    private Animator animator;
    private bool canLighten;
    public GameObject endDoor;
    public GameObject goodDoor;
    // public bool isLight1,isLight2,isLight3;
    // public GameObject torch1,torch2,torch3;
    public GameObject torch1;
    public GameObject torch3;
    public GameObject torch2;
    public AudioSource music;
    public AudioClip fire;
    public AudioClip disapear;

    private bool canChat=false;
    // public bool isLight1,isLight2,isLight3;
    // public GameObject torch1,torch2,torch3;
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        canLighten = true;
        Debug.Log("碰撞发生");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canLighten = false;
    }
    private void Update()
    {
       
        //light3 = animator.GetBool("isLight3");
        if (Input.GetKeyDown(KeyCode.F))
        {
            Light();
        }
   
    }
    void Light()
    {
        if (canLighten)
        {
            if (torch1.GetComponent<Animator>().GetBool("isLight1") && torch2.GetComponent<Animator>().GetBool("isLight2"))
            {
                canChat = true;
                torch3.GetComponent<Animator>().SetBool("isLight3", true);
                endDoor.SetActive(true);
                goodDoor.SetActive(true);
                music.clip = fire;
                music.Play();


            }
            else if(!torch1.GetComponent<Animator>().GetBool("isLight1") || !torch2.GetComponent<Animator>().GetBool("isLight2"))
            {
                canChat=false;
                torch3.GetComponent<Animator>().SetBool("isLight3", true);
                torch1.GetComponent<Animator>().SetBool("isLight1", false); torch2.GetComponent<Animator>().SetBool("isLight2",false);
                music.clip = disapear;
                music.Play();
            }
            if(torch1.GetComponent<Animator>().GetBool("isLight1") && torch2.GetComponent<Animator>().GetBool("isLight2")&& torch3.GetComponent<Animator>().GetBool("isLight3"))
            {
                Destroy(torch1.GetComponent<BoxCollider2D >());
                Destroy(torch2.GetComponent<BoxCollider2D>());
                Destroy(torch2.GetComponent<BoxCollider2D>());
                Say();
            }
        
        }
       
    }
    void Say()
    {
        if (canChat)
        {
            //对话
            Flowchart flowChart = GameObject.Find("doorOpened").GetComponent<Flowchart>();
            if (flowChart.HasBlock("DoorOpen"))
            {
                //执行对话
                flowChart.ExecuteBlock("DoorOpen");
            }
        }
    }

}
