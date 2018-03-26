using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour
{
    public int turn;//-1,1
    public int count;//count==9 平局
    private int[,] arr = new int[3, 3];//棋盘；
    private bool mode = true;

    void Start()
    {
        restart();
    }
    void restart()
    {
        for(int i = 0;i < 3;i++)
        {
            for(int j = 0;j < 3;j++)
            {
                arr[i, j] = 0;
            }
        }
        count = 0;
        turn = 1;
        mode = true;
    }

   private int isgamewin()
    {
        for(int i = 0;i < 3;i++)
        {
            //行连线
            if (arr[0,i] != 0 && arr[0,i] == arr[1,i] && arr[2, i] == arr[1, i])
            {
                return arr[0,i];
            }
            //列连线
            if (arr[i,0] != 0 && arr[i,0] == arr[i,1] && arr[i,2] == arr[i,1])
            {
                return arr[i,0];
            }
        }
        if(arr[0,0] != 0 && arr[1,1] == arr[0,0] && arr[2,2] == arr[1,1])
        {
            return arr[0, 0];
        }
        if (arr[0, 2] != 0 && arr[1, 1] == arr[0, 2] && arr[2, 0] == arr[1, 1])
        {
            return arr[0, 0];
        }
        if (count == 9) return 3;//平局，1/2为胜利
        return 0;//游戏中
    }

    private void aiturn ()
    {
        if (count == 9) return;

        int xie1 = 0, xie2 = 0, dui1 = 0, dui2 = 0;
        if (arr[0, 0] == 1) xie1++;
        if (arr[1, 1] == 1) xie1++;
        if (arr[2, 2] == 1) xie1++;

        if (xie1 == 2)
        {
            for (int j = 0; j < 3; j++)
            {
                if (arr[j, j] == 0)
                {
                    arr[j, j] = 2;
                    count++;
                    return;
                }
            }
        }

        if (arr[0, 0] == 2) xie2++;
        if (arr[1, 1] == 2) xie2++;
        if (arr[2, 2] == 2) xie2++;

        if (xie2 == 2)
        {
            for (int j = 0; j < 3; j++)
            {
                if (arr[j, j] == 0)
                {
                    arr[j, j] = 2;
                    count++;
                    return;
                }
            }
        }

        if (arr[2, 0] == 1) dui1++;
        if (arr[1, 1] == 1) dui1++;
        if (arr[0, 2] == 1) dui1++;

        if (dui1 == 2)
        {
            for (int j = 0; j < 3; j++)
            {
                if (arr[j, 2 - j] == 0)
                {
                    arr[j, 2 - j] = 2;
                    count++;
                    return;
                }
            }
        }

        if (arr[2, 0] == 2) dui2++;
        if (arr[1, 1] == 2) dui2++;
        if (arr[0, 2] == 2) dui2++;

        if (dui2 == 2)
        {
            for (int j = 0; j < 3; j++)
            {
                if (arr[j, 2 - j] == 0)
                {
                    arr[j, 2 - j] = 2;
                    count++;
                    return;
                }
            }
        }

        
        for(int i = 0;i < 3;i++)
        {
            int hcount1 = 0, lcount1 = 0, hcount2 = 0, lcount2 = 0;
            if (arr[i, 0] == 1) hcount1++;
            if (arr[i, 1] == 1) hcount1++;
            if (arr[i, 2] == 1) hcount1++;
            Debug.Log(hcount1);
            if (hcount1 == 2)
            {
                for(int j = 0;j < 3;j++)
                {
                    if(arr[i,j] == 0)
                    {
                        arr[i,j] = 2;
                        count++;
                        return;
                    }
                }
            }


            if (arr[0, i] == 1) lcount1++;
            if (arr[1, i] == 1) lcount1++;
            if (arr[2, i] == 1) lcount1++;

            if (lcount1 == 2)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[j, i] == 0)
                    {
                        arr[j, i] = 2;
                        count++;
                        return;
                    }
                }
            }

            if (arr[i, 0] == 2) hcount2++;
            if (arr[i, 1] == 2) hcount2++;
            if (arr[i, 2] == 2) hcount2++;

            if (hcount2 == 2)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[i, j] == 0)
                    {
                        arr[i, j] = 2;
                        count++;
                        return;
                    }
                }
            }




            if (arr[0, i] == 2) lcount2++;
            if (arr[1, i] == 2) lcount2++;
            if (arr[2, i] == 2) lcount2++;

            if (lcount2 == 2)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[j, i] == 0)
                    {
                        arr[j, i] = 2;
                        count++;
                        return;
                    }
                }
            }


        }


        for (int i = 0;i < 3;i++)
        {
            for(int j = 0;j < 3;j++)
            {
                if(arr[i,j] == 0)
                {
                    arr[i, j] = 2;
                    count++;
                    return;
                }
            }
        }
    }

    private void OnGUI()
    {
        if( GUI.Button(new Rect(10,170,100,50),"restart") )
        {
            restart();
        }

        if (GUI.Button(new Rect(10, 250, 100, 50), "one player"))
        {
            mode = false;
        }
        if (GUI.Button(new Rect(150, 250, 100, 50), "two player"))
        {
            mode = true;
        }
        int res = isgamewin();
        //判定
        GUIStyle end = new GUIStyle();
        end.fontSize = 30;
        end.fontStyle = FontStyle.Bold;
        end.normal.textColor = Color.red;
        //O的颜色
        GUIStyle playero = new GUIStyle();
        playero.fontSize = 20;
        playero.fontStyle = FontStyle.Bold;
        playero.normal.textColor = Color.yellow;
        playero.alignment = TextAnchor.MiddleCenter;
        //X的颜色
        GUIStyle playerx = new GUIStyle();
        playerx.fontSize = 20;
        playerx.fontStyle = FontStyle.Bold;
        playerx.normal.textColor = Color.blue;
        playerx.alignment = TextAnchor.MiddleCenter;

        string[] temp = {  "O win", "X win", "Equal" };
        if (res == 3 || res == 2 || res == 1)
        {
            GUI.Label(new Rect(500, 100, 100, 50), temp[res-1], style: end);
        }
        
        //渲染页面
        for (int i = 0;i < 3;i++)
        {
            for(int j = 0;j < 3;j++)
            {
                if(arr[i,j] == 1)
                {
                    GUI.Button(new Rect(i * 50, j * 50, 50, 50), "0",style: playero);
                }
                if (arr[i, j] == 2)
                {
                    GUI.Button(new Rect(i * 50, j * 50, 50, 50), "X", style: playerx);
                }
                if(GUI.Button(new Rect(i * 50, j * 50, 50, 50), "" ) && (mode == true || (mode == false && turn == 1) ) )
                {
                    if (res == 0)
                    {
                        if (turn == 1) arr[i, j] = 1;
                        else if (turn == -1) arr[i, j] = 2;
                        count++;
                        turn = -turn;
                        if(mode == false)
                        {
                            aiturn();
                            turn = 1;
                        }
                    }
                }
                
            }
        }
    }
}
