using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerActive : MonoBehaviour
{
    public GameObject locker;
    public GameObject Hermes;
    private bool canOpen = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        canOpen = true;
        Debug.Log("碰撞发生");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canOpen = false;
        Debug.Log(canOpen);
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Locker();
        }

    }
    void Locker()
    {
        if (canOpen)
        {
            locker.SetActive(true);
            Hermes.SetActive(true);
        }
        
    }
}
