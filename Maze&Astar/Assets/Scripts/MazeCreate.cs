using UnityEngine;
using UnityEditor;
using System;

public class MazeCreate : Singleton<MazeCreate>
{
    [SerializeField] private GameObject MazeStartingPoint, Wall, DeleteWall, PlayerObj;

    [SerializeField] private int MazeSize;

    //(y, x)
    public bool[,] Blocks;

    private void Start()
    {
        int Size = MazeSize + 1;
        Blocks = new bool[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if(i % 2 == 0 || j % 2 == 0)
                {
                    //격자무늬로 벽들을 생성함
                    Blocks[i, j] = true;
                    ObjPool.GetObject(EPoolType.Wall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j));
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

                if(i == Size - 2)
                {
                    //마지막 부분 y축 선 뚫어줌
                    Blocks[i, j] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j + 1));
                    continue;
                }
                if(j == Size - 2)
                {
                    //마지막 부분 x축 선 뚫어줌
                    Blocks[i, j] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i + 1, MazeStartingPoint.transform.position.y + j));
                    continue;
                }

                //x축을 뚫어주느냐? y축을 뚫어주느냐?
                if(UnityEngine.Random.Range(0, 2) == 0)
                {
                    //랜덤하게 x축 기준으로 뚫어줌
                    Blocks[i, j] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j + 1));
                }
                else
                {
                    //랜덤하게 y축 기준으로 뚫어줌
                    Blocks[i, j] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i + 1, MazeStartingPoint.transform.position.y + j));
                }
            }
        }
        //시작지점 뚫어줌
        print(Blocks[1, 1]);
        Blocks[0, 1] = false;
        ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y));
        PlayerObj.transform.position = new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y);
        PlayerObj.GetComponent<Player>().PlayerPosX = 1;
    }
}