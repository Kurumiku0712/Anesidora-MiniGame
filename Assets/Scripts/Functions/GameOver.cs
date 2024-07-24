using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    
    public GameObject gameOverGo;
    public Button restart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Over()
    {
        GameManager.instance.GameOver();
        //PlayerControl.instance.isDie = false;
   
    }
}
