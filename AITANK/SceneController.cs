using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//场景控制器
public class SceneController : MonoBehaviour, IUserAction
{
    public GameObject player;//玩家

    public bool isOver = false;//判断游戏结束 

   // private int npcCount = 6;//npc数量

    void Awake()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        //mF = Singleton<myFactory>.Instance;
        //player = mF.getPlayer();
    }
    void Update()
    {
        // 相机跟随玩家坦克
        Camera.main.transform.position = new Vector3(player.transform.position.x, 15, player.transform.position.z);
    }

    public Vector3 getPlayerPos()
    {//返回玩家的位置
        return player.transform.position;
    }
    public void moveForward()
    {
        player.GetComponent<Rigidbody>().velocity = player.transform.forward * 10;
    }
    public void moveBackWard()
    {
        player.GetComponent<Rigidbody>().velocity = player.transform.forward * -10;
    }
    public void turn(float offsetX)
    {//offset:水平轴的增量,改变玩家的欧拉角转向
        float x = player.transform.localEulerAngles.y + offsetX * 5;
        float y = player.transform.localEulerAngles.x;
        player.transform.localEulerAngles = new Vector3(y, x, 0);
    }
    public bool isGameOver()
    {//返回游戏是否结束
        return isOver;
    }
    public void setGameOver()
    {//游戏结束
        isOver = true;
    }
    public void shoot()
    {

    }

}