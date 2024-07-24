using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeAttackText : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 1);
        transform.DOScale(new Vector3(1, 1, 0), 0.4f);
        transform.DOMoveY(transform.position.y + 20, 0.5f);
        Destroy(gameObject,0.6f);
    }
}
