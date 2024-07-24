using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image healthbarPoint;
    public Image healthbarEffect;
    public MonsterBoss boss;


    private void Update()
    {


        healthbarPoint.fillAmount = boss.hp / boss.MaxHP;
        if (healthbarEffect.fillAmount > healthbarPoint.fillAmount)
        {
            healthbarEffect.fillAmount -= 0.05f;
        }
        else
        {
            healthbarEffect.fillAmount = healthbarPoint.fillAmount;
        }
    }

}

