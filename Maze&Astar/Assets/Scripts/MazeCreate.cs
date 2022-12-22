using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

[Serializable]
public class Node
{
    public Node(bool _isWall) { isWall = _isWall; }
    public bool isWall = false;

    public int x;
    public int y;
    [Tooltip("��ǥ������ �Ÿ�")] public int H;
}
public class MazeCreate : Singleton<MazeCreate>
{
    [SerializeField] private GameObject MazeStartingPoint, Wall, DeleteWall, PlayerObj;

    public int MazeSize;

    public Node[,] Nodes;

    private void Start()
    {
        int Size = MazeSize + 1;
        Nodes = new Node[Size, Size];
        print(Nodes.Length);
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Nodes[j, i] = new Node(false);
                Nodes[j, i].x = j;
                Nodes[j, i].y = i;
                
                if (i % 2 == 0 || j % 2 == 0)
                {
                    //���ڹ��̷� ������ ������
                    Nodes[j, i].isWall = true;
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
                    Nodes[j, i + 1].isWall = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j, MazeStartingPoint.transform.position.y + i + 1));
                    continue;
                }
                if (i == Size - 2)
                {
                    //������ �κ� x�� �� �վ���
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j + 1, MazeStartingPoint.transform.position.y + i));
                    Nodes[j + 1, i].isWall = false;
                    continue;
                }

                //x���� �վ��ִ���? y���� �վ��ִ���?
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    //�����ϰ� x�� �������� �վ���
                    Nodes[j, i + 1].isWall = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j, MazeStartingPoint.transform.position.y + i + 1));
                }
                else
                {
                    //�����ϰ� y�� �������� �վ���
                    Nodes[j + 1, i].isWall = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + j + 1, MazeStartingPoint.transform.position.y + i));
                }
            }
        }
        //�������� �վ���
        Nodes[1, 0].isWall = false;
        ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y));
        
        //�÷��̾� ������ ����
        PlayerObj.transform.position = new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y);
        PlayerObj.GetComponent<Player>().PlayerPosX = 1;

        string a = "";
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                a += Nodes[j, i].isWall + ",";
            }
            a += "\n";
        }
        print(a);
    }
}