using UnityEngine;

using UnityEngine.Video;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour
{

    //定义参数获取VideoPlayer组件和RawImage组件

    public VideoPlayer videoPlayer;

    public RawImage rawImage;
    public GameObject END;

    // Use this for initialization

    void Start()
    {

        //获取场景中对应的组件


    }



    // Update is called once per frame

    void Update()
    {

        //如果videoPlayer没有对应的视频texture，则返回

        if (videoPlayer.texture == null)
        {

            return;

        }

        //把VideoPlayerd的视频渲染到UGUI的RawImage

        rawImage.texture = videoPlayer.texture;
        if (!videoPlayer.isPlaying)
        {
            END.SetActive(true);
        }
      



    }

}