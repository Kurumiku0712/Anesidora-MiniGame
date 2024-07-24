using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clickStart()
    {
        SceneManager.LoadScene("ch1");
    }
    public void clickQuit()
    {
        Application.Quit();
    }
}
