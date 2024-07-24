using UnityEngine;
using System.Collections;
public class ShowInfo : MonoBehaviour
{
    //    public Transform cube;
    // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //检测结果
    // RaycastHit hitInfo;
    bool isShowTip;
    public string info;
    public int power;
    public float CD;
    //    // Use this for initialization
    void Start()
    {
        isShowTip = false;
    }
    void OnMouseEnter()
    {
        isShowTip = true;
        Debug.Log("yeah");//可以得到物体的名字

    }

    void OnMouseExit()
    {
        isShowTip = false;
    }
    /*private void Update()
    {
        if (isShowTip)
        {
            OnGUI();
            isShowTip = false;
        }
        
    }*/
    void OnGUI()
    {
        if (isShowTip)
        {
            GUIStyle fontStyle = new GUIStyle();
            fontStyle.normal.textColor = new Color(255, 255, 255);
            fontStyle.fontSize = 15;
            GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 500, 200), info + "\n威力：" + power + "\n冷却：" + CD,fontStyle);
            //GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 500, 200), "威力："+power);
        }


    }
}