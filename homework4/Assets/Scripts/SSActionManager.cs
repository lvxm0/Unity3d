using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework5
{
    public class SSActionManager :MonoBehaviour
    {
        private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
        private List<SSAction> waitingAdd = new List<SSAction>();
        private List<int> waitingDelete = new List<int>();

        protected void Update()
        {
            //将等待增加的动作列表的动作添加到动作列表中
            foreach (SSAction ac in waitingAdd) actions[ac.GetInstanceID()] = ac;
            waitingAdd.Clear();
            //向待删除列表添加待删除的动作序号
            foreach (KeyValuePair<int, SSAction> kv in actions)
            {
                SSAction ac = kv.Value;
                if (ac.destroy)
                {
                    waitingDelete.Add(ac.GetInstanceID()); 
                }
                else if (ac.enable)
                {
                    ac.Update();
                }
            }
            //删除待删除的动作
            foreach(int key in waitingDelete)
            {
                SSAction ac = actions[key];
                actions.Remove(key);
                DestroyObject(ac);
            }
            waitingDelete.Clear();

        }

        public void RunAction(GameObject gameobject,SSAction action,ISSActionCallback manager)
        {
            action.gameobject = gameobject;
            action.transform = gameobject.transform;
            action.callback = manager;
            waitingAdd.Add(action);
            action.Start();
        }
        protected void Start() { }
    }
}
