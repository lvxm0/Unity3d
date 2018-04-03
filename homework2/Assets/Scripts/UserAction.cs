using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using homework;

namespace homework
{
    //用户行为，门面模式，分离动作的具体实现和用户的输入方式
    public interface UserAction
    {
        void moveBoat();
        void characterIsClicked(MyCharacterController characterCtrl);
        void restart();
       
    }
}
