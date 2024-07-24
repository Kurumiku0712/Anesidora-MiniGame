using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideDoor : MonoBehaviour
{
    // public string chapterName;
    // public bool isRight;
    public GameObject hidewall;
    public GameObject hideroom;
    public GameObject door;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
         {
            hidewall.SetActive(false);
            hideroom.SetActive(true);
            Destroy(door);
           // this.GameObject.SetActive(false);

         }
        }

}

