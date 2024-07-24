using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject treasurePre;
    public Transform treasureTran;
    public AudioSource music;
    public AudioClip open;
    //public GameObject[] box;
    bool isOpen = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && !isOpen)
        {
            music.clip = open;
            music.Play();
            Instantiate(treasurePre, treasureTran);
            GetComponent<Animator>().Play("Open");
            isOpen = true;
            /*for (int i = 1; i < box.Length; i++)
            {
                box[i].SetActive(false);
            }*/
            //  box.SetActive(false);
        }
    }
}
