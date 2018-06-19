using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IUserAction
{
    void moveForward();
    void moveBackWard();
    void turn(float offsetX);
    void shoot();
    bool isGameOver();
}


public class IUserGUI : MonoBehaviour
{
    IUserAction user;
    // Use this for initialization
    void Start()
    {
        user = Director.getInstance().currentSceneController as IUserAction;
    }

    // Update is called once per frame
    void Update()
    {
        if (!user.isGameOver())
        {
            if (Input.GetKey(KeyCode.W))
            {
                user.moveForward();
            }

            if (Input.GetKey(KeyCode.S))
            {
                user.moveBackWard();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                user.shoot();
            }

            float offsetX = Input.GetAxis("Horizontal1");//获取水平轴的增量，控制玩家的转向
            user.turn(offsetX);
        }
    }
}
//导演类
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
