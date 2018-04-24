using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysManager : SSActionManager, ISSActionCallback
{
    public PFlyAction action;

    public void fly(GameObject disk)
    {
        action = PFlyAction.GetSSAction(disk);
        this.RunAction(disk, action, this);
    }
  
    
    protected void Start() { }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)
    {
        // throw new System.NotImplementedException();
    }
}

