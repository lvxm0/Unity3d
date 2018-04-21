using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CCActionManager : SSActionManager, ISSActionCallback
{
    public FirstSceneController sceneController;
    public CCFlyAction Fly;
    //public int DiskNumber = 0;
    protected void Start()
    {
        sceneController = (FirstSceneController)Director.getInstance().currentScenceController;
        sceneController.action_manager = this;
    }
    public void fly(GameObject disk)
    {
        Fly = CCFlyAction.GetSSAction(disk);
        this.RunAction(disk, Fly, this);
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)
    {
        // throw new System.NotImplementedException();
    }
}
