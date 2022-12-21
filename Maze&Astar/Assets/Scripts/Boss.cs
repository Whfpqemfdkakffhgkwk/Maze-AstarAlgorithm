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
            //보스 포지션 = Cur포지션 (Cur포지션 초기화)
            CurPosX = BossPosX;
            CurPosY = BossPosY;
            //도착지점에 도달했을 시(막히지 않는 길을 찾았을 시)
            if (MazeCreate.Instance.MazeSize - 1 == BossPosX &&
                MazeCreate.Instance.MazeSize == BossPosY)
            {
                //이 반복을 종료한다
                break;
            }
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
                    //H가 이미 계산되지 않았다면(새로운 노드라면) 다음 노트의 H를 계산
                    if (MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].H != 0)
                    {
                        HCount(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]], CurPosX + X[i], CurPosY + Y[i]);

                    }

                    //문제점! 열지 않은 노드가 두개야 근데 한 노드가 정답에 멀어져? 그럼 어케
                    //문제점 해결법! 그러면 계산이 되지 않았다면 그 방향 노드의 H 계산해 그리고 그 H가 적은게 아니라면 노드 변경하면 돼 ㅇㅋ?

                    //H가 적은게 아니라면(계산 된 노드가 아니라면)
                    if (MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].H >
                        MazeCreate.Instance.Nodes[CurPosX, CurPosY].H)
                    {
                        //다음 노드 변경(진행)
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
