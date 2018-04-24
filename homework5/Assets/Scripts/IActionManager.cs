using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionManager {

    //按照PPT上的写接口，mode = true 为物理引擎驱动，mode=false为运动学驱动
    void playDisk(bool mode, GameObject disk);
}
