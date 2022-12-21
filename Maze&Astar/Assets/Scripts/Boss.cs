using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Boss : MonoBehaviour
{
    private int BossPosX = 1, BossPosY = 0;
    private List<Node> AnswerWay;

    private void Start()
    {

    }
    void Astar()
    {
        int CurPosX = 0, CurPosY = 0;
        int[] X = new int[4] { 0, 0, -1, 1 };
        int[] Y = new int[4] { 1, -1, 0, 0 };
        while (true)
        {
            //���� ������ = Cur������ (Cur������ �ʱ�ȭ)
            CurPosX = BossPosX;
            CurPosY = BossPosY;
            //���������� �������� ��(������ �ʴ� ���� ã���� ��)
            if (MazeCreate.Instance.MazeSize - 1 == BossPosX &&
                MazeCreate.Instance.MazeSize == BossPosY)
            {
                //�� �ݺ��� �����Ѵ�
                break;
            }
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
                    //H�� �̹� ������ �ʾҴٸ�(���ο� �����) ���� ��Ʈ�� H�� ���
                    if (MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].H != 0)
                    {
                        HCount(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]], CurPosX + X[i], CurPosY + Y[i]);

                    }

                    //������! ���� ���� ��尡 �ΰ��� �ٵ� �� ��尡 ���信 �־���? �׷� ����
                    //������ �ذ��! �׷��� ����� ���� �ʾҴٸ� �� ���� ����� H ����� �׸��� �� H�� ������ �ƴ϶�� ��� �����ϸ� �� ����?

                    //H�� ������ �ƴ϶��(��� �� ��尡 �ƴ϶��)
                    if (MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].H >
                        MazeCreate.Instance.Nodes[CurPosX, CurPosY].H)
                    {
                        //���� ��� ����(����)
                        AnswerWay.Add(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]]);
                        CurPosX += X[i];
                        CurPosY += Y[i];
                        i = 0;
                    }
                }
            }
        }
    }
    void HCount(Node node, int _x, int _y)
    {
        node.H = Mathf.Abs(_x - Player.Instance.PlayerPosX) + Mathf.Abs(_y - Player.Instance.PlayerPosY);
    }
    public void Move(int name)
    {
        switch (name)
        {
            case 1:
                gameObject.transform.rotation = Quaternion.Euler(270, -90, 90);
                transform.DOMove(transform.position + new Vector3(0, 1), 0.3f);
                break;
            case 2:
                transform.DOMove(transform.position + new Vector3(-1, 0), 0.3f);
                break;
            case 3:
                transform.DOMove(transform.position + new Vector3(0, -1), 0.3f);
                break;
            case 4:
                transform.DOMove(transform.position + new Vector3(1, 0), 0.3f);
                break;
        }
    }
}
