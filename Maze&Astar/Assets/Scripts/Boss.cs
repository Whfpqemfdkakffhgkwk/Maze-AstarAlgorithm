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
            //도착지점에 도달했을 시(막히지 않는 길을 찾았을 시)
            if (MazeCreate.Instance.MazeSize - 1 == CurPosX &&
                MazeCreate.Instance.MazeSize == CurPosY)
            {
                //실행되던 코루틴이 있으면 종료
                StopCoroutine(CurCorouitine);
                //자동 따라가기 코루틴을 실행
                CurCorouitine = StartCoroutine(Move());
                //이 반복을 종료한다
                break;
            }
            //보스 포지션 = Cur포지션 (Cur포지션 초기화)
            CurPosX = BossPosX;
            CurPosY = BossPosY;
            //상하좌우 탐색한다
            for (int i = 0; i < 4; i++)
            {
                //탐색에 들어왔다면 정답이 아니기 때문에 리스트 초기화
                if (AnswerWay.Count > 0)
                    AnswerWay.Clear();
                //다음 체크할 노드가 밖 범위가 아닐시, 벽이 아닐시
                if (CurPosX + X[i] > 0 ||
                    CurPosY + Y[i] > 0 ||
                    CurPosX + X[i] != MazeCreate.Instance.MazeSize ||
                    CurPosY + Y[i] != MazeCreate.Instance.MazeSize ||
                    MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].isWall == false)
                {
                    //왜안돼
                    if (MazeCreate.Instance.MazeSize != CurPosY + 1)
                    {

                        //H가 이미 계산되지 않았다면(새로운 노드라면) 다음 노트의 H를 계산
                        if (MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]].H != 0)
                        {
                            //H가 적은게 아니라면(계산 된 노드가 아니라면)
                            HCount(MazeCreate.Instance.Nodes[CurPosX + X[i], CurPosY + Y[i]], CurPosX + X[i], CurPosY + Y[i]);
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
