using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public Text text;
    public int index;
    public void LoadNextLevel()
    {
        StartCoroutine(Loadlevel());
    }
    IEnumerator Loadlevel()
    {
        loadScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + index);
        operation.allowSceneActivation = false;
        while(!operation .isDone)
        {
            slider.value = operation.progress;
            text.text = operation.progress * 100 + "%";
            if(operation .progress >= 0.9f)
            {
                slider.value = 1;

                text.text = "按下任意键以继续";

                if(Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
