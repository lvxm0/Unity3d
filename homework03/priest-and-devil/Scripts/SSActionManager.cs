using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace homework
{
    //动作管理器基类，ppt代码
    public class SSActionManager : MonoBehaviour, ISSActionCallback
    {
        private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
        private List<SSAction> waitingAdd = new List<SSAction>();
        private List<int> waitingDelete = new List<int>();

        // Use this for initialization  
        void Start()
        {

        }

        // Update is called once per frame  
        protected void Update()
        {
            foreach (SSAction ac in waitingAdd) actions[ac.GetInstanceID()] = ac;
            waitingAdd.Clear();

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

            foreach (int key in waitingDelete)
            {
                SSAction ac = actions[key]; actions.Remove(key); DestroyObject(ac);
            }
            waitingDelete.Clear();
        }

        public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
        {
            action.gameobject = gameobject;
            action.transform = gameobject.transform;
            action.callback = manager;
            waitingAdd.Add(action);
            action.Start();
        }


        public void addAction(GameObject gameObject, SSAction action, ISSActionCallback whoToNotify)
        {
            action.gameobject = gameObject;
            action.transform = gameObject.transform;
            action.callback = whoToNotify;
            waitingAdd.Add(action);
            action.Start();
        }
        public void actionDone(SSAction source)
        {

        }
    }
}
