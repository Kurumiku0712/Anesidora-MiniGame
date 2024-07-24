using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GamePause : MonoBehaviour
{
    public GameObject gamePauseGo;
    public Button restart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pause()
    {
        GameManager.instance.Gamepause();
    }
}
