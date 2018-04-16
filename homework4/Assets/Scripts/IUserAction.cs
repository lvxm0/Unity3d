using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework5
{
    //UI界面和场景管理器进行通信的接口，在GUI使用，在场景管理器中实现
    public enum GameState { ROUND_START, ROUND_FINISH, RUNNING, PAUSE, START }

    public interface IUserAction
    {
        bool GameOver();
        GameState getGameState();
        void setGameState(GameState gs);
        int GetScore();
        void GameOver1();
        void hit(Vector3 pos);
    }
}
