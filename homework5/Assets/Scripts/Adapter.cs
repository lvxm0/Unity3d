using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//将一个接口转换成客户希望的另一个接口，
//适配器模式使接口不兼容的那些类可以一起工作，其别名为包装器
public class Adapter : MonoBehaviour, IActionManager
{
    public CCActionManager Cmanager;
    public PhysManager Pmanager;
    void Start()
    {
        //挂载在上面
        Cmanager = gameObject.AddComponent<CCActionManager>() as CCActionManager;
        Pmanager = gameObject.AddComponent<PhysManager>() as PhysManager;
    }
    public void playDisk(bool mode, GameObject disk)
    {
        if(mode == true)
        {
            //物理模式
            Pmanager.fly(disk);
        }
        else
        {
            //运动学
            Cmanager.fly(disk);
        }
    }
}

