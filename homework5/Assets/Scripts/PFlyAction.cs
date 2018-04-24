using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PFlyAction : SSAction
{
    private Vector3 hirv = Vector3.zero;//水平初速度方向
    private PFlyAction() { }
    public override void Start()
    {
        //使用重力,水平速度来一波
        gameobject.GetComponent<Rigidbody>().useGravity = true;
        gameobject.GetComponent<Rigidbody>().velocity = hirv;
    }
    public static PFlyAction GetSSAction(GameObject disk)
    {
        Vector3 direction = disk.GetComponent<DiskData>().direction;
        float speed = disk.GetComponent<DiskData>().speed;
        float angle1 = 20f;//Random.Range(15f, 30f);
        PFlyAction action = CreateInstance<PFlyAction>();
        if (direction.x == -1)
        {
            Debug.Log(speed +" "+angle1);
            action.hirv = Quaternion.Euler(new Vector3(0, 0, -angle1)) * Vector3.left * speed;
        }
        else
        {
            action.hirv = Quaternion.Euler(new Vector3(0, 0, angle1)) * Vector3.right * speed;
        }
        return action;
    }
    
    public override void Update() { }
    public override void FixedUpdate()
    {
        //位置过低，销毁，完成动作
        Debug.Log("fixedupdate");
        if (this.transform.position.y < -15)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
}

