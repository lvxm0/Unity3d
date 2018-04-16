using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework5
{
    //有awake!
    //飞碟工厂,有飞碟预设，使用的列表，空闲未使用的列表
    public class DiskFactory : MonoBehaviour
    {
        //这个是飞碟预设
        public GameObject disk;
        //正在使用，空闲尚未使用
        List<DiskData> used;
        List<DiskData> free;

        void Start()
        {
            Debug.Log("start!");
        }
        void Update()
        {

        }
        private void Awake()
        {
            //刚开始就利用预设制造一组非活动状态的飞碟
            disk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk"), Vector3.zero, Quaternion.identity);
            disk.SetActive(false);
        }

        public GameObject getDisk(int round)
        {
            GameObject adick;
            //检查空闲列表有没有飞碟
            if (free.Count >= 0)
            {
                //取出第一个,然后从空闲列表删除
                adick = free[0].gameObject;
                free.Remove(free[0]);
            }
            else
            {
                // 如果没有就从预制克隆一个
                adick = GameObject.Instantiate<GameObject>(disk,new Vector3(0,0,0),Quaternion.identity);
            }
            //将飞碟按照给定数据设置好
            //adick.AddComponent<DiskData>();
            setRuler(round,adick);
            //将取得的飞镖在正使用列表登记
            used.Add(adick.GetComponent<DiskData>());
            return adick;
        }
        //设置跪着
        void setRuler(int round, GameObject adick)
        {
            //发射位置随机
            float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
            adick.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 0);
            //颜色完全随机
            int selectedColor = Random.Range(0, 499);
            if(selectedColor > 400)
            {
                adick.GetComponent<DiskData>().color = Color.blue;
                adick.GetComponent<Renderer>().material.color = Color.blue;
            }
            else if(selectedColor > 300)
            {
                adick.GetComponent<DiskData>().color = Color.red;
                adick.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                adick.GetComponent<DiskData>().color = Color.yellow;
                adick.GetComponent<Renderer>().material.color = Color.yellow;
            }
            //难度随着回合数递增:速度，大小
            if (round < 3)
            {
                adick.GetComponent<DiskData>().speed = 2.0f * (round + 1);
                adick.GetComponent<DiskData>().size = new Vector3(1,0.08f,1);
            }
            else if(round < 10)
            {
                adick.GetComponent<DiskData>().speed = 6.0f + (round * 0.5f);
                adick.GetComponent<DiskData>().size = new Vector3(0.8f, 0.08f, 0.8f);
            }
            else if(round >= 10)
            {
                adick.GetComponent<DiskData>().speed = 11.0f + (round* 0.1f);
                adick.GetComponent<DiskData>().size = new Vector3(0.5f, 0.08f, 0.5f);
            }
        }
        //释放数据
        public void FreeDisk(GameObject disk)
        {
            GameObject temp = null;
            bool flag = false;
            //找到要释放的数据
            foreach (DiskData i in used)
            {
                if(disk.GetInstanceID() == i.gameObject.GetInstanceID())
                {
                    temp = i.gameObject;
                    flag = true;
                }
            }
            if(flag == false)
            {
                //抛出异常
                //throw ;
            }
            free.Add(temp.GetComponent<DiskData>());
            used.Remove(temp.GetComponent<DiskData>());
            free.Add(disk.GetComponent<DiskData>());
        } 

    }
}
