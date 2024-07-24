using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class judgement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objPrefabInstantSource;//音乐预知物体 
    private GameObject musicInstant = null;//场景中是否有这个物体
    private GameObject musicInstant2;
    //public string menu_musicname;
    //   private GameObject musicInstant2 = null;//场景中是否有这个物体
    // Use this for initialization  
    void Awake()
    { 
        musicInstant = GameObject.FindGameObjectWithTag("Music");
        string name = SceneManager.GetActiveScene().name;

       
        if (musicInstant == null)
        {
           musicInstant = (GameObject)Instantiate(objPrefabInstantSource);         
        }
        if(name=="ch5"||name=="ch6"||name=="PureEnding"||name=="SadEnding"||name=="Start")
            Destroy(musicInstant);


    }
}
