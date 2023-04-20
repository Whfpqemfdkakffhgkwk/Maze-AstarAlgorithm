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

        //보스 포지션 = Cur포지션 (Cur포지션 초기화)
        CurPosX = BossPosX;
        CurPosY = BossPosY;
        //상하좌우 탐색한다
        for (int i = 0; i < 4; i++)
        {
            //다음 체크할 노드가 밖 범위가 아닐시, 벽이 아닐시
            if (CurPosX + X[i] > 0 &&
                CurPosY + Y[i] > 0 &&
                CurPosX + X[i] != MazeCreate.Instance.MazeSize &&
                CurPosY + Y[i] != MazeCreate.Instance.MazeSize &&
                MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].isWall == false)
            {
                print("밖 범위랑 벽 범위가 아닐때 들어옴");
                print("??");
                //H가 이미 계산되지 않았다면(새로운 노드라면) 다음 노트의 H를 계산
                if (MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].H == -1)
                {
                    print("H 새로운 거여서 계산");
                    HCount(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]], CurPosX + X[i], CurPosY + Y[i]);

                    MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].G =
                        MazeCreate.Instance.Nodes[CurPosX, CurPosY].G + 1;

                    MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].F =
                        MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].G +
                        MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].H;

                    print(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].F);

                }
                //F가 적은게 아니라면
                if (MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].F >
                    MazeCreate.Instance.Nodes[CurPosX, CurPosY].F)
                {
                    print("H 비교해서 적은거 찾기");
                    //다음 노드 변경(진행)
                    AnswerWay.Add(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]]);
                    CurPosX += X[i];
                    CurPosY += Y[i];
                    i = -1;
                }
                // }
            }
        }
        print("다찾음");
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
