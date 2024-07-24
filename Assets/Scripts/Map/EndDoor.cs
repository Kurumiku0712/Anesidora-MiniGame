using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour
{
    public string chapterName;
    public bool isRight;
    private bool isEnter=false;
    public LoadManager loadManager;
    //public GameObject load;
    Collider2D collision;
    [SerializeField] private string newchapterPassword;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       isEnter = true;
    }
    private void Start()
    {
       // collision = Collider2D .FindObjectOfType ;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            EnterDoor();
        }
    }
    void EnterDoor()
    {
        if (isEnter)
        {
            if (isRight)
            {
                //if (collision.transform.tag == "Player")
               // {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                    //SceneManager.LoadScene(chapterName);
                    //load.SetActive(true);
                    loadManager.LoadNextLevel();
                        PlayerControl.instance.chapterPassword = newchapterPassword;
                    }

              //  }
            }
            else
            {
                //if (collision.transform.tag == "Player")
               // {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        // GameManager.instance.KillPlayer();
                        PlayerControl.instance.playerDP.realValue = 0;
                        PlayerControl.instance.BeAttack(PlayerControl.instance.playerHP.realValue);
                    }

               // }

            }
        }
        
    }
}
