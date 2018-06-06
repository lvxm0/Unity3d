using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGUI : MonoBehaviour
{

    public float life = 0.0f;
    public float buffvalue = 1.0f;
    private Rect debuff;
    private Rect buff;
    private float res;//计算血条的值，来插值
    private Rect BloodBar;
    private Rect add;
    private Rect sub;

    void Start()
    {
        //确定位置（二维矩形）
        //血条横向  
        BloodBar = new Rect(20, 20, 200, 20);
        //加血  
        add = new Rect(20, 50, 40, 20);
        //减血  
        sub = new Rect(70, 50, 40, 20);
        //加buff
        buff = new Rect(120, 50, 40, 20);
        //加负面效果的buff
        debuff = new Rect(170, 50, 40, 20);
        res = life;
    }

    void OnGUI()
    {
        if (GUI.Button(add, "加血"))
        {
            res += 0.1f*buffvalue;
        }
        if (GUI.Button(sub, "减血"))
        {
            res -= 0.1f * buffvalue;
        }
        if (GUI.Button(buff, "增益"))
        {
            //加/减血速度变快
            buffvalue += 1f;
            //res += 0.1f * buffvalue;
        }
        if (GUI.Button(debuff, "削弱"))
        {
            //加/减血速度变快
            buffvalue -= 1f;
           // res -= 0.1f * buffvalue;
        }
        //增益削减倍数在0.1~3之间
        if(buffvalue <= 0f)
        {
            buffvalue = 0f;
        }
        if(buffvalue > 5.0f)
        {
            buffvalue = 5.0f;
        }
        //血条范围0~1
        if (res > 1.0f)
        {
            res = 1.0f;
        }
        if (res < 0.0f)
        {
            res = 0.0f;
        }
        
        //插值计算HP值

        life = Mathf.Lerp(life, res, 0.05f);

        // 显示血条public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue);
        GUI.HorizontalScrollbar(BloodBar, 0.0f, life, 0.0f, 1.0f);
    }
}
