using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Boss : MonoBehaviour
{
    private int BossPosX = 1, BossPosY = 0;
    private List<Node> AnswerWay = new List<Node>();

    Coroutine CurCorouitine;

    private void Start()
    {
        Astar();
    }
    void Astar()
    {
        int CurPosX = 0, CurPosY = 0;
        int[] X = new int[4] { 0, 0, -1, 1 };
        int[] Y = new int[4] { 1, -1, 0, 0 };
        while (true)
        {
            //���������� �������� ��(������ �ʴ� ���� ã���� ��)
            if (MazeCreate.Instance.MazeSize - 1 == CurPosX &&
                MazeCreate.Instance.MazeSize == CurPosY)
            {
                //����Ǵ� �ڷ�ƾ�� ������ ����
                StopCoroutine(CurCorouitine);
                //�ڵ� ���󰡱� �ڷ�ƾ�� ����
                CurCorouitine = StartCoroutine(Move());
                //�� �ݺ��� �����Ѵ�
                break;
            }
            //���� ������ = Cur������ (Cur������ �ʱ�ȭ)
            CurPosX = BossPosX;
            CurPosY = BossPosY;
            //�����¿� Ž���Ѵ�
            for (int i = 0; i < 4; i++)
            {
                //Ž���� ���Դٸ� ������ �ƴϱ� ������ ����Ʈ �ʱ�ȭ
                if (AnswerWay.Count > 0)
                    AnswerWay.Clear();
                //���� üũ�� ��尡 �� ������ �ƴҽ�, ���� �ƴҽ�
                if (CurPosX + X[i] > 0 ||
                    CurPosY + Y[i] > 0 ||
                    CurPosX + X[i] != MazeCreate.Instance.MazeSize ||
                    CurPosY + Y[i] != MazeCreate.Instance.MazeSize ||
                    MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].isWall == false)
                {
                    //�־ȵ�
                    if (MazeCreate.Instance.MazeSize != CurPosY + 1)
                    {

                        //H�� �̹� ������ �ʾҴٸ�(���ο� �����) ���� ��Ʈ�� H�� ���
                        if (MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].H != 0)
                        {
                            //H�� ������ �ƴ϶��(��� �� ��尡 �ƴ϶��)
                            HCount(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]], CurPosX + X[i], CurPosY + Y[i]);
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
        }
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
