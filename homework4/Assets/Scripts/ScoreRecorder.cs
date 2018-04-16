using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework5
{
    //记录得分
    public class ScoreRecorder : MonoBehaviour
    {
        //总分
        public int score;
        //将和得分对应起来,黄1红2蓝3，不同难度，得分翻倍
        private Dictionary<Color, int> scoreTable = new Dictionary<Color, int>();
        void Start()
        {
            Debug.Log("recordstart!");
            score = 0;
            scoreTable.Add(Color.yellow, 1);
            scoreTable.Add(Color.red, 2);
            scoreTable.Add(Color.blue, 4);
        }
        public void Record(GameObject disk)
        {
            int mul = 1;
            if(disk.GetComponent<DiskData>().speed <= 6.0f)
            {
                mul = 1;
            }else if(disk.GetComponent<DiskData>().speed <= 11.0f)
            {
                mul = 2;
            }else
            {
                mul = 3;
            }
            score += mul * scoreTable[disk.GetComponent<DiskData>().color];
        }

        public void Reset()
        {
            score = 0;
        }
    }
}
