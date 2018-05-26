﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHalo : MonoBehaviour {

    private ParticleSystem particleSys;  // 粒子系统（需要挂载）  
    private ParticleSystem.Particle[] particleArr;  // 粒子的数组  
    private CirclePosition[] circle; // 极坐标数组（角度，半径，时间的数组）
    //可以调整的粒子信息
    public int count = 10000;       // 粒子数量  
    public float size = 0.07f;      // 粒子大小  
    public float minRadius = 5.0f;  // 最小半径  
    public float maxRadius = 12.0f; // 最大半径  
    public bool clockwise = true;   // 顺时针|逆时针  
    public float speed = 2f;        // 速度  
    public float pingPong = 0.02f;  // 游离范围 
    // 速度差分层数,通过把粒子分成10不同的速度，变得更加立体 
    private int tier = 10;
    //透明度
   // public Gradient colorGradient;
    // Use this for initialization
    void Start () {
        // 初始化粒子数组  
        particleArr = new ParticleSystem.Particle[count];
        circle = new CirclePosition[count];

        // 初始化粒子系统  
        particleSys = GetComponent<ParticleSystem>();
        particleSys.startSpeed = 0;            // 粒子位置由程序控制  
        particleSys.startSize = size;          // 设置粒子大小  
        particleSys.loop = false;               //都是由程序控制，不循环
        particleSys.maxParticles = count;      // 设置最大粒子量  
        particleSys.Emit(count);               // 发射粒子  
        particleSys.GetParticles(particleArr);

        


        RandomlySpread();   // 初始化各粒子位置  
    }
	
	// Update is called once per frame
	void Update () {//一部分逆时针一部分顺时针
        float midRadius = (maxRadius + minRadius) / 2;
        float one_four_Radius = minRadius + (maxRadius - minRadius) / 4;
        float three_four_Radius = minRadius + (maxRadius - minRadius) * 3 / 4;
        for (int i = 0; i < count; i++)
        {
            if (clockwise)
            {  // 顺时针旋转  
                if(circle[i].radius > three_four_Radius)
                    circle[i].angle -= (i % tier + 1) * (speed / circle[i].radius / tier);
                else
                {
                    circle[i].angle += (i % tier + 1) * (speed / circle[i].radius / tier);
                }
            }
            else
            {        // 逆时针旋转 
                if (circle[i].radius > three_four_Radius)
                    circle[i].angle += (i % tier + 1) * (speed / circle[i].radius / tier);
                else
                    circle[i].angle -= (i % tier + 1) * (speed / circle[i].radius / tier);
            }
            // 保证angle在0~360度  
            circle[i].angle = (360.0f + circle[i].angle) % 360.0f;
            
           
            float theta = circle[i].angle / 180 * Mathf.PI;
            //半径的游离
            circle[i].time += Time.deltaTime;
            circle[i].radius += Mathf.PingPong(circle[i].time / minRadius / maxRadius, pingPong) - pingPong / 2.0f;
            
           particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), 0f, circle[i].radius * Mathf.Sin(theta));
        }

        particleSys.SetParticles(particleArr, particleArr.Length);
    }
    //随机分布
    void RandomlySpread()
    {
        for (int i = 0; i < count; ++i)
        {   
            //修改：随机每个粒子距离中心的半径，希望粒子靠近两遍
            float midRadius = (maxRadius + minRadius) / 2;
            float one_four_Radius = minRadius + (maxRadius - minRadius) / 4;
           float three_four_Radius = minRadius + (maxRadius - minRadius) * 3 / 4;
            float minRate = Random.Range(1.0f, one_four_Radius / minRadius);
           float maxRate = Random.Range(three_four_Radius / maxRadius, 1.0f);
            float radius = Random.Range(minRadius * minRate, maxRadius * maxRate);

            // 随机每个粒子的角度 ，实现缺口 
            float minAngle = Random.Range(-90f, 0.0f);
            float maxAngle = Random.Range(0.0f, 90f);
            float randomangle = Random.Range(minAngle, maxAngle);

            float angle = i % 2 == 0 ? randomangle : randomangle - 180;
           // float angle = Random.Range(0.0f, 360.0f);
            float theta = angle / 180 * Mathf.PI;//真正的theta角度



            // 随机每个粒子的游离起始时间  
            float time = Random.Range(0.0f, 360.0f);

            circle[i] = new CirclePosition(radius, angle, time);

            particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), 0f, circle[i].radius * Mathf.Sin(theta));
            //更新透明度
            //particleArr[i].color = colorGradient.Evaluate(circle[i].angle / 360.0f);
        }

        particleSys.SetParticles(particleArr, particleArr.Length);
    }
}
//这个类来记录粒子的角度，时间，半径等信息，从而确定位置。
public class CirclePosition
{
    public float radius = 0f, angle = 0f, time = 0f;
    public CirclePosition(float radius, float angle, float time)
    {
        this.radius = radius;   // 半径  
        this.angle = angle;     // 角度  
        this.time = time;       // 时间  
    }
}

