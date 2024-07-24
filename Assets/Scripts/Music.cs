using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{

    // Start is called before the first frame update
    //void Awake()
    //{
    //    //string name = SceneManager.GetActiveScene().name;
    //    //if (name == "ch1" && this.gameObject != null)
    //    //{
    //    //   // Debug.Log(name);
    //    //    Destroy(this.gameObject);
    //    //}
    //}
    void Start()
    {
        if (this.gameObject != null)
            DontDestroyOnLoad(this.gameObject);
        //    music.time = 0f;
        //     music.Play();
    }

}
    


