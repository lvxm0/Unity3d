using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using homework;
namespace homework
{   //单例模式，最上层的控制器，通过实例化场景控制器，来切换场景。
    public class Director : System.Object
    {
        private static Director _instance;
        public SceneController currentSceneController { get; set; }

        public static Director getInstance()
        {
            if (_instance == null)
            {
                _instance = new Director();
            }
            return _instance;
        }

    }

}
