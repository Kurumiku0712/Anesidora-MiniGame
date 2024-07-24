using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necklace : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;
    public GameObject necklace;
    private bool isTrig;
    private void OnTriggerEnter2D(Collider2D other)
    {
        isTrig = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Change();
        }
    }
    void Change()
    {
        if (isTrig)
        {
        Door1.SetActive(false);
        Door2.SetActive(true);
        necklace.SetActive(false);
       Destroy( PlayerControl.instance.myWeapon);
        }
        
    }

}
