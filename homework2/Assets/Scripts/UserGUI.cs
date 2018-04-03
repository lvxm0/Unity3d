using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using homework;

namespace homework
{

    public class UserGUI : MonoBehaviour
    {
        private UserAction action;
        public int status = 0;
        GUIStyle style;
        GUIStyle buttonStyle;

        void Start()
        {
            action = Director.getInstance().currentSceneController as UserAction;

            style = new GUIStyle();
            style.fontSize = 30;//字号
            style.alignment = TextAnchor.MiddleCenter;//居中

            buttonStyle = new GUIStyle("button");
            buttonStyle.fontSize = 20;
        }
        void OnGUI()
        {
            if (status == 1)//win
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Gameover!", style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
                {
                    status = 0;
                    action.restart();
                }
            }
            else if (status == 2)//lose
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You win!", style);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
                {
                    status = 0;
                    action.restart();
                }
            }
        }
    }

}
