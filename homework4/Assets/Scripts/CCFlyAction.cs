using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework5
{
    public class CCFlyAction : SSAction
    {
        //平抛运动
        float g;                // 重力加速度
        float horspeed;         // 水平速度
        Vector3 direction;      // 飞行方向
        float time;             // 飞行时间

        //初始化
        public override void Start()
        {
            enable = true;
            g = 9.8f;
            time = 0;
            horspeed = gameobject.GetComponent<DiskData>().speed;
            direction = gameobject.GetComponent<DiskData>().direction;
        }

        //每帧更新
        public override void Update()
        {
            if (gameobject.activeSelf)
            {
                /** 
                 * 计算飞碟的累计飞行时间 
                 */
                time += Time.deltaTime;

                /** 
                 * 飞碟在竖直方向的运动 : g*t*t,将这帧抽象成匀速运动，v=gt
                 */

                transform.Translate(Vector3.down * g * time * Time.deltaTime);

                /** 
                 * 飞碟在水平方向的运动 : x=vt
                 */

                transform.Translate(direction * horspeed * Time.deltaTime);

                /** 
                 * 当飞碟的y坐标比-4小时，飞碟落地 
                 */

                if (this.transform.position.y < -4)
                {
                    this.destroy = true;
                    this.enable = false;
                    this.callback.SSActionEvent(this);
                }
            }

        }

        //实例化一个动作给动作管理器
        public static CCFlyAction GetSSAction()
        {
            CCFlyAction action = ScriptableObject.CreateInstance<CCFlyAction>();
            return action;
        }

    }
}
