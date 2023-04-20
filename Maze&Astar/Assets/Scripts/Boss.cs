using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Boss : MonoBehaviour
{
    private int BossPosX = 1, BossPosY = 0;
    private List<Node> AnswerWay = new List<Node>();
    public int CurPosX, CurPosY;

    Coroutine CurCorouitine;

    private void Start()
    {
        StartCoroutine(Test());
    }
    IEnumerator Test()
    {
        yield return new WaitForSeconds(10);
        Astar();
    }

    void Astar()
    {
        int[] X = new int[4] { 0, 0, -1, 1 };
        int[] Y = new int[4] { 1, -1, 0, 0 };

        //���� ������ = Cur������ (Cur������ �ʱ�ȭ)
        CurPosX = BossPosX;
        CurPosY = BossPosY;
        //�����¿� Ž���Ѵ�
        for (int i = 0; i < 4; i++)
        {
            //���� üũ�� ��尡 �� ������ �ƴҽ�, ���� �ƴҽ�
            if (CurPosX + X[i] > 0 &&
                CurPosY + Y[i] > 0 &&
                CurPosX + X[i] != MazeCreate.Instance.MazeSize &&
                CurPosY + Y[i] != MazeCreate.Instance.MazeSize &&
                MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].isWall == false)
            {
                print("�� ������ �� ������ �ƴҶ� ����");
                print("??");
                //H�� �̹� ������ �ʾҴٸ�(���ο� �����) ���� ��Ʈ�� H�� ���
                if (MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].H == -1)
                {
                    print("H ���ο� �ſ��� ���");
                    HCount(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]], CurPosX + X[i], CurPosY + Y[i]);

                    MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].G =
                        MazeCreate.Instance.Nodes[CurPosX, CurPosY].G + 1;

                    MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].F =
                        MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].G +
                        MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].H;

                    print(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].F);

                }
                //F�� ������ �ƴ϶��
                if (MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].F >
                    MazeCreate.Instance.Nodes[CurPosX, CurPosY].F)
                {
                    print("H ���ؼ� ������ ã��");
                    //���� ��� ����(����)
                    AnswerWay.Add(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]]);
                    CurPosX += X[i];
                    CurPosY += Y[i];
                    i = -1;
                }
                // }
            }
        }
        print("��ã��");
        CurCorouitine = StartCoroutine(Move());
    }
    void HCount(Node node, int _x, int _y)
    {
        node.H = Mathf.Abs(_x - Player.Instance.PlayerPosX) + Mathf.Abs(_y - Player.Instance.PlayerPosY);
    }
    public IEnumerator Move()
    {
        for (int i = 0; i < AnswerWay.Count; i++)
        {
            gameObject.transform.DOMove(new Vector3(AnswerWay[i].x, AnswerWay[i].y), 0.4f);
            yield return new WaitForSeconds(0.4f);
            BossPosX = AnswerWay[i].x;
            BossPosY = AnswerWay[i].y;
        }
    }
}
