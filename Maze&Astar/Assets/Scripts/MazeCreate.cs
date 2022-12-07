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
                    //���ڹ��̷� ������ ������
                    Blocks[i, j] = true;
                    ObjPool.GetObject(EPoolType.Wall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j));
                }
            }
        }

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                //���ٸ��� x, y�� �� ������� �����ϰ� �մ°ű� ������ �ǳʶپ������
                if (i % 2 == 0 || j % 2 == 0)
                    continue;

                if(i == Size - 2)
                {
                    //������ �κ� y�� �� �վ���
                    Blocks[i, j] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j + 1));
                    continue;
                }
                if(j == Size - 2)
                {
                    //������ �κ� x�� �� �վ���
                    Blocks[i, j] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i + 1, MazeStartingPoint.transform.position.y + j));
                    continue;
                }

                //x���� �վ��ִ���? y���� �վ��ִ���?
                if(UnityEngine.Random.Range(0, 2) == 0)
                {
                    //�����ϰ� x�� �������� �վ���
                    Blocks[i, j] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j + 1));
                }
                else
                {
                    //�����ϰ� y�� �������� �վ���
                    Blocks[i, j] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i + 1, MazeStartingPoint.transform.position.y + j));
                }
            }
        }
        //�������� �վ���
        print(Blocks[1, 1]);
        Blocks[0, 1] = false;
        ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y));
        PlayerObj.transform.position = new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y);
        PlayerObj.GetComponent<Player>().PlayerPosX = 1;
    }
}