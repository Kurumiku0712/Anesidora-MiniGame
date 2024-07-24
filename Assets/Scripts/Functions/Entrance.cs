using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string entrancePassword;


    private void Start()
    {
        if (PlayerControl.instance.chapterPassword == entrancePassword)
        {
            PlayerControl.instance.transform.position = transform.position;//transform.position is our entrance positon
            Debug.Log("ENTER!");
        }
        else
        {
            Debug.LogWarning("WRONG PW");
        }

    }


}
