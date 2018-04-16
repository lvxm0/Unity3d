using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework5
{
    public class FirstSceneController : MonoBehaviour,ISceneController,IUserAction
    {
        public void GameOver1() { }

        //场记中加入：动作管理，分数记录，要发射的飞碟列表
        public CCActionManager actionManager { get; set; }
        public ScoreRecorder scoreRecorder { get; set; }
        public Queue<GameObject> diskQueue = new Queue<GameObject>();
        //当前回合数
        private int currentRound = -1;
        //飞碟发射时间间隔
        private float time = 0;
        //游戏状态
        private GameState gameState = GameState.START;
        //每回合发射飞碟总数 = 10
        private int diskNumber;


        void awake() {
            Debug.Log("controllerawake!");
            Debug.Log("lalal");
            Director director = Director.getInstance();
            director.currentScenceController = this;
            diskNumber = 10;
            //向挂载场景管理器的对象挂载分数记录和飞碟工厂
            this.gameObject.AddComponent<ScoreRecorder>();
            this.gameObject.AddComponent<DiskFactory>();
            scoreRecorder = Singleton<ScoreRecorder>.Instance;
            director.currentScenceController.LoadResources();

        }
        public void LoadResources() {
            Debug.Log("load!"); Debug.Log("lalal");
        }
        void start() { Debug.Log("start!"); Debug.Log("lalal"); }



        void update() {
            if (actionManager.DiskNumber == 0 && gameState == GameState.RUNNING)
            {
                gameState = GameState.ROUND_FINISH;

            }

            if (actionManager.DiskNumber == 0 && gameState == GameState.ROUND_START)
            {
                currentRound = currentRound + 1;
                //NextRound下一个回合
                DiskFactory df = Singleton<DiskFactory>.Instance;
                //取出10个飞碟到列表中
                for (int i = 0; i < diskNumber; i++)
                {
                    diskQueue.Enqueue(df.getDisk(currentRound));
                }
                //开始仍飞碟，初始化动作管理器的飞碟数
                actionManager.StartThrow(diskQueue);
                actionManager.DiskNumber = 10;
                gameState = GameState.RUNNING;
            }
            if (time > 1)
            {
                //ThrowDisk仍飞碟
                if (diskQueue.Count != 0)
                {
                    GameObject disk = diskQueue.Dequeue();
 
                     //随机确定飞碟出现的位置 
                    Vector3 position = new Vector3(0, 0, 0);
                    float y = UnityEngine.Random.Range(0f, 4f);
                    position = new Vector3(-disk.GetComponent<DiskData>().direction.x * 7, y, 0);
                    disk.transform.position = position;

                    disk.SetActive(true);
                }
                time = 0;
            }
            else
            {
                time += Time.deltaTime;
            }
        }

        public bool GameOver()
        {
            return true;
        }

        public GameState getGameState()
        {
            return gameState;
        }

        public void setGameState(GameState gs)
        {
            gameState = gs;
        }

        public int GetScore()
        {
            return scoreRecorder.score;
        }

        public void hit(Vector3 pos)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                if (hit.collider.gameObject.GetComponent<DiskData>() != null)
                {
                    scoreRecorder.Record(hit.collider.gameObject);

                    /** 
                     * 如果飞碟被击中，那么就移到地面之下，由工厂负责回收 
                     */

                    hit.collider.gameObject.transform.position = new Vector3(0, -5, 0);
                }

            }
        }
    }
}
