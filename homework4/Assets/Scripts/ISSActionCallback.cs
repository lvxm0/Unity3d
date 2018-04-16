
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework5
{ 

    public enum SSActionEventType:int { Started, Competeted }
    public interface ISSActionCallback
    {
    void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
         int intParam = 0, string strParam = null, Object objectParam = null );
    }
}
