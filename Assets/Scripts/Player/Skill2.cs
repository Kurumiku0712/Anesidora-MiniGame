using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill2 : MonoBehaviour
{
    private float currentTime; //
    public Image coolingImage;
    private float coolingTimer;
    //public string alpha;
    public float cd;
    // Use this for initialization
    void Start()
    {
       // coolingImage = transform.GetComponent();
        coolingImage.raycastTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateImage();
        ClickSkill(cd);
    }

    public void ClickSkill(float timer)
    {
        if (Input.GetKeyDown(KeyCode.Alpha2)&&!PlayerControl.instance.isUnbeatable && !PlayerControl.instance.addHP&& !PlayerControl.instance.berserker && PlayerControl.instance.hasWeapon() != null)
        {
            coolingTimer = timer;
            currentTime = 0.0f;
            coolingImage.fillAmount = 1.0f;
        }
       

    }

    private void UpdateImage()
    {
        if (currentTime < coolingTimer)
        {
            currentTime += Time.deltaTime;
            coolingImage.fillAmount = 1 - currentTime / coolingTimer;
            if (coolingImage.fillAmount != 0)
            {
                coolingImage.raycastTarget = true;
            }
            else
            {
                coolingImage.raycastTarget = false;
            }
        }
    }


}
