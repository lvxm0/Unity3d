using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCFlyAction : SSAction
{
    public float g = -5;//重力加速度

    private Vector3 hirv = Vector3.zero;//水平初速度方向
  private Vector3 gv = Vector3.zero;//重力速度方向
    private float time;//飞行时间
    private Vector3 angle = Vector3.zero;//飞行角度(参考别人的处理)

    private CCFlyAction() { }

    public static CCFlyAction GetSSAction(GameObject disk, float angle, float power)
    {
        Vector3 direction = disk.GetComponent<DiskData>().direction;
        float speed = disk.GetComponent<DiskData>().speed;
        CCFlyAction action = CreateInstance<CCFlyAction>();
        if (direction.x == -1)
        {
            action.hirv = Quaternion.Euler(new Vector3(0, 0, -angle)) * Vector3.left * power;
        }
        else
        {
            action.hirv = Quaternion.Euler(new Vector3(0, 0, angle)) * Vector3.right * power;
        }
        return action;
    }
    public override void Start() { }
    public override void Update()
    {
        time += Time.fixedDeltaTime;
        gv.y = g * time;
        //竖直速度 v = at
        //位移
        // transform.Translate(Vector3.down * g * time * Time.fixedDeltaTime);
        /// transform.Translate(hirv * Time.fixedDeltaTime);

        // transform.Translate();
        //transform.position += (start_vector + gv) * Time.fixedDeltaTime;
        transform.position += (hirv + gv) * Time.fixedDeltaTime;
        
        angle.z = Mathf.Atan((hirv.y + gv.y) / hirv.x) * Mathf.Rad2Deg;
         transform.eulerAngles = angle;

        //位置过低，销毁，完成动作
        if (this.transform.position.y < -15)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }

}

