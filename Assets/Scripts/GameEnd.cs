using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    bool ESCDown;
    private void Update()
    {
        ESCDown = Input.GetKeyUp(KeyCode.Escape);
        if(ESCDown)
        {
            Application.Quit();
        }
    }
    
}
