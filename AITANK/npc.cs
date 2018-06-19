using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npc : MonoBehaviour
{

    public float hp;//血条

    private Vector3 target;//目标，玩家的位置

    private bool isover;//游戏是否结束，决定是否继续运动或射击

    void Start()
    {
        hp = 100f;
    }
    void Update()
    {
        if (hp < 0)
        {
            this.gameObject.SetActive(false);
            return;
        }
        isover = Director.getInstance().currentSceneController.isGameOver();
        if (!isover)
        {
            target = Director.getInstance().currentSceneController.getPlayerPos();
            //向玩家坦克移动
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(target);

        }
        else
        {//游戏结束，停止寻路
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }

      

    }

    public float gethp()
    {
        return hp;
    }

    public void sethp(float hp)
    {
        this.hp = hp;
    }
  
}
