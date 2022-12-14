using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
public class MazeCreate : Singleton<MazeCreate>
{
    [SerializeField] private GameObject MazeStartingPoint, Wall, DeleteWall, PlayerObj;

    public int MazeSize;

    public bool[,] Blocks;

    private void Start()
    {
        int Size = MazeSize + 1;
        Blocks = new bool[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (i % 2 == 0 || j % 2 == 0)
                {
                    //격자무늬로 벽들을 생성함
                    Blocks[j, i] = true;
                    ObjPool.GetObject(EPoolType.Wall, new Vector2(MazeStartingPoint.transform.position.x + j, MazeStartingPoint.transform.position.y + i));
                }

            }
        }

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                //두줄마다 x, y축 다 살려놓고 랜덤하게 뚫는거기 때문에 건너뛰어줘야함
                if (i % 2 == 0 || j % 2 == 0)
                    continue;

                if(j == Size - 2) //i == 5
                {
                    //마지막 부분 y축 선 뚫어줌
                    Blocks[j, i + 1] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j, MazeStartingPoint.transform.position.y + i + 1));
                    continue;
                }
                if (i == Size - 2)
                {
                    //마지막 부분 x축 선 뚫어줌
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j + 1, MazeStartingPoint.transform.position.y + i));
                    Blocks[j + 1, i] = false;
                    continue;
                }

                //x축을 뚫어주느냐? y축을 뚫어주느냐?
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    //랜덤하게 x축 기준으로 뚫어줌
                    Blocks[j, i + 1] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j, MazeStartingPoint.transform.position.y + i + 1));
                }
                else
                {
                    //랜덤하게 y축 기준으로 뚫어줌
                    Blocks[j + 1, i] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j + 1, MazeStartingPoint.transform.position.y + i));
                }
            }
        }
        //시작지점 뚫어줌
        Blocks[1, 0] = false;
        ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y));
        
        //플레이어 시작점 세팅
        PlayerObj.transform.position = new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y);
        PlayerObj.GetComponent<Player>().PlayerPosX = 1;

        string a = "";
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                a += Blocks[j, i] + ",";
            }
            a += "\n";
        }
        print(a);
    }
}