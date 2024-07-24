using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public GameObject treasurePre;
    public Transform treasureTran;
    //public GameObject bag;
    bool isOpen = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && !isOpen)
        {
            Instantiate(treasurePre, treasureTran);
            GetComponent<Animator>().Play("Backpack");
            isOpen = true;
           // transform.GetComponent<Collider>().enabled = false;
        }
    }
}
