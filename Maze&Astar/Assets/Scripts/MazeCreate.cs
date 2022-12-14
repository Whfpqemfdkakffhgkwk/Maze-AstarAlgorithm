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
                    //���ڹ��̷� ������ ������
                    Blocks[j, i] = true;
                    ObjPool.GetObject(EPoolType.Wall, new Vector2(MazeStartingPoint.transform.position.x + j, MazeStartingPoint.transform.position.y + i));
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

                if(j == Size - 2) //i == 5
                {
                    //������ �κ� y�� �� �վ���
                    Blocks[j, i + 1] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j, MazeStartingPoint.transform.position.y + i + 1));
                    continue;
                }
                if (i == Size - 2)
                {
                    //������ �κ� x�� �� �վ���
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j + 1, MazeStartingPoint.transform.position.y + i));
                    Blocks[j + 1, i] = false;
                    continue;
                }

                //x���� �վ��ִ���? y���� �վ��ִ���?
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    //�����ϰ� x�� �������� �վ���
                    Blocks[j, i + 1] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j, MazeStartingPoint.transform.position.y + i + 1));
                }
                else
                {
                    //�����ϰ� y�� �������� �վ���
                    Blocks[j + 1, i] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j + 1, MazeStartingPoint.transform.position.y + i));
                }
            }
        }
        //�������� �վ���
        Blocks[1, 0] = false;
        ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y));
        
        //�÷��̾� ������ ����
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