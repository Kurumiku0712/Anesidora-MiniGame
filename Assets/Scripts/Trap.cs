using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    public float attack;
    private bool canAttack;
    public float CD;
    //Collider2D other;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        canAttack = true;
        if (canAttack)
        {
            StartCoroutine(Attack(CD,other,PlayerControl.instance.playerHP.realValue+ PlayerControl.instance.playerDP.realValue));
        }
      /*  if (!canAttack)
        {
            StopCoroutine(Attack(CD, other, 10000000));
        }*/
        //\\StopCoroutine(Attack(1, other, 5));
    }
     private void OnTriggerExit2D(Collider2D other)
    {
        canAttack = false;
    }
   
    /// <summary>
    /// 持续伤害效果
    /// </summary>
    /// <param name="timeOff">时间间隔</param>
    /// <param name="count">伤害次数</param>
    /// <returns></returns>
    /*IEnumerator TakeDamage(float timeOff, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(timeOff);
            
                PlayerControl.instance.BeAttack(attack);

        }
    }*/
    IEnumerator Attack(float time,Collider2D other,float count)
    {
        
        for(float i = 0; i <= count; i++)
        {
            if (canAttack)
            {
                yield return new WaitForSeconds(time);
                other.GetComponent<BeAttack>().BeAttack(attack);
            }
            

        }
        
    }
   
}
