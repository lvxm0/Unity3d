using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUserAction
{
    //用户点击游戏界面
    void hit(Vector3 pos);
    //获得分数
    int GetScore();
    //游戏结束
    void GameOver();
    //游戏重新开始
    void ReStart();
    //游戏开始
    void BeginGame();
}
