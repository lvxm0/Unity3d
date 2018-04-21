using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public GameObject disk = null;                 //飞碟预制
    private List<DiskData> used = new List<DiskData>();   //正在被使用的飞碟列表登记
    private List<DiskData> free = new List<DiskData>();   //空闲的飞碟列表登记
    public GameObject GetDisk(int round)
    {
        disk = null;
        if(free.Count <= 0)
        {
            Debug.Log("diskf");
            disk = Instantiate(Resources.Load<GameObject>("Prefabs/disk"), new Vector3(0, -10f, 0), Quaternion.identity);
           // disk.AddComponent<DiskData>();
            setRuler(round, disk);
        }else
        {
            //取出第一个,然后从空闲列表删除
            disk = free[0].gameObject;
            disk.SetActive(true);
            setRuler(round, disk);
            free.Remove(free[0]);
        }
        used.Add(disk.GetComponent<DiskData>());
        return disk;
    }
    void setRuler(int round, GameObject adick)
    {
        //发射位置随机
        float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
        adick.GetComponent<DiskData>().direction = new Vector3(RanX, -10f, 0);
        //颜色完全随机
        int selectedColor = Random.Range(0, 499);
        if (selectedColor > 400)
        {
            adick.GetComponent<DiskData>().color = Color.blue;
            adick.GetComponent<Renderer>().material.color = Color.blue;
            adick.GetComponent<DiskData>().score = 3;
        }
        else if (selectedColor > 300)
        {
            adick.GetComponent<DiskData>().color = Color.red;
            adick.GetComponent<Renderer>().material.color = Color.red;
            adick.GetComponent<DiskData>().score = 2;

        }
        else
        {
            adick.GetComponent<DiskData>().color = Color.yellow;
            adick.GetComponent<Renderer>().material.color = Color.yellow;
            adick.GetComponent<DiskData>().score = 1;

        }
        //难度随着回合数递增:速度，大小
        if (round < 3)
        {
            adick.GetComponent<DiskData>().speed = 3.0f * (round + 1);
            adick.GetComponent<DiskData>().size = new Vector3(1, 0.9f, 1);
            adick.GetComponent<DiskData>().score = adick.GetComponent<DiskData>().score * 1;

        }
        else if (round < 10)
        {
            adick.GetComponent<DiskData>().speed = 4.0f + (round * 0.5f);
            adick.GetComponent<DiskData>().size = new Vector3(0.8f, 0.9f, 0.8f);
            adick.GetComponent<DiskData>().score = adick.GetComponent<DiskData>().score * 2;
        }
        else if (round >= 10)
        {
            adick.GetComponent<DiskData>().speed = 8.0f + (round * 0.1f);
            adick.GetComponent<DiskData>().size = new Vector3(0.5f, 0.9f, 0.5f);
            adick.GetComponent<DiskData>().score = adick.GetComponent<DiskData>().score * 3;
        }
        adick.transform.localScale = adick.GetComponent<DiskData>().size;
    }
    public void FreeDisk(GameObject disk)
    {
        for (int i = 0; i < used.Count; i++)
        {
            if (disk.GetInstanceID() == used[i].gameObject.GetInstanceID())
            {
                used[i].gameObject.SetActive(false);
                free.Add(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }

}

