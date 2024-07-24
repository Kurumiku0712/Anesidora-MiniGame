using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    public AudioSource music;
    public AudioClip open;
    // Start is called before the first frame update
    void TurnOnFL()
    {
        if(!flashlight.activeSelf)
        {
            music.clip = open;
            music.Play();
            flashlight.SetActive(true);
        }
        else if (flashlight.activeSelf)
         {
            flashlight.SetActive(false);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            TurnOnFL();
        }
    }
}
