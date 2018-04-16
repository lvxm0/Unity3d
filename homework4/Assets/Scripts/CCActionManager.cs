using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework5
{
    //将fly,飞行这个动作也做类似飞碟一样的处理，类似工厂模式
    public class CCActionManager : SSActionManager,ISSActionCallback
    {
        public FirstSceneController sceneController;
        public List<CCFlyAction> Fly;
        public int DiskNumber = 0;
        //used是正在进行的动作，free是还未激活的动作
        private List<SSAction> used = new List<SSAction>();
        private List<SSAction> free = new List<SSAction>();

        SSAction GetSSAction()
        {
            SSAction action = null;
            if (free.Count > 0)
            {
                action = free[0];
                free.Remove(free[0]);
            }
            else
            {
                action = ScriptableObject.Instantiate<CCFlyAction>(Fly[0]);
            }

            used.Add(action);
            return action;
        }

        public void FreeSSAction(SSAction action)
        {
            SSAction tmp = null;
            foreach (SSAction i in used)
            {
                if (action.GetInstanceID() == i.GetInstanceID())
                {
                    tmp = i;
                }
            }
            if (tmp != null)
            {
                tmp.reset();
                free.Add(tmp);
                used.Remove(tmp);
            }
        }

        protected new void Start()
        {
            sceneController = (FirstSceneController)Director.getInstance().currentScenceController;
            sceneController.actionManager = this;
            Fly.Add(CCFlyAction.GetSSAction());

        }
        //实现回调接口，将用过的飞碟和飞的动作加入到free列表中
        public void SSActionEvent(SSAction source,SSActionEventType events = SSActionEventType.Competeted,
            int intParam = 0,string strParam = null,UnityEngine.Object objectParam = null)
        {
            if (source is CCFlyAction)
            {
                DiskNumber--;
                DiskFactory df = Singleton<DiskFactory>.Instance;
                df.FreeDisk(source.gameobject);
                FreeSSAction(source);
            }
        }
        //扔飞碟
        public void StartThrow(Queue<GameObject> diskQueue)
        {
            foreach (GameObject tmp in diskQueue)
            {
                //运行动作，为SSActionManger定义的函数
                RunAction(tmp, GetSSAction(), (ISSActionCallback)this);
            }
        }
    }
}
