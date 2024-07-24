using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill1 : MonoBehaviour
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
        ClickSkill(PlayerControl.instance.skillA_CD);
    }

    public void ClickSkill(float timer)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)&&!PlayerControl.instance.speedUp&& !PlayerControl.instance.addHP&& !PlayerControl.instance.berserker && PlayerControl.instance.hasWeapon()!=null )
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
