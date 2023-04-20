using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Astar : MonoBehaviour
{
    List<Node> ClosedList = new List<Node>();
    Node CurNode, TargetNode;
    Node StartNode;
    List<Node> OpenList = new List<Node>();
    private Player player;
    private void Start()
    {
        player = Player.Instance;
        
        StartNode = MazeCreate.Instance.Nodes[1, 0];
        StartCoroutine(AstarLoop());
    }

    /// <summary>
    /// 게임 포기버튼 눌렀을때 실행되는 함수
    /// </summary>
    public void GameGiveUp()
    {
        //StartCoroutine(AutoClear());
    }

    IEnumerator AstarLoop()
    {
        AStarStart();
        yield return new WaitForSeconds(1);
        StartCoroutine(AstarLoop());
    }

    public void AStarStart()
    {
        if(OpenList != null) OpenList.Clear(); 
        if(ClosedList != null) ClosedList.Clear();
        OpenList.Add(StartNode);
        TargetNode = MazeCreate.Instance.Nodes[player.PlayerPosX, player.PlayerPosY];
        //수정 필요 보스가 서있는 포지션
        List<Node> FinalNodeList = new List<Node>();
        while (OpenList.Count > 0)
        {
            CurNode = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
                    CurNode = OpenList[i];
            }

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);



            //마지막
            if (CurNode == TargetNode)
            {
                print("a");
                Node TargetCurNode = TargetNode;

                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;

                }
                FinalNodeList.Add(StartNode);
                    FinalNodeList.Reverse();

                print("자 한칸 이동하자~");
                gameObject.transform.DOMove(new Vector3(FinalNodeList[1].x, FinalNodeList[1].y), 0.4f);
                StartNode = FinalNodeList[1];

                return;
            }

            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);

        }


    }

    void OpenListAdd(int X, int Y)
    {

        //다음 체크할 노드가 밖 범위가 아닐시, 벽이 아닐시, 닫힌 리스트에 없을 시
        if (X > 0 && Y > 0 &&
            X != MazeCreate.Instance.MazeSize &&
            Y != MazeCreate.Instance.MazeSize &&
            MazeCreate.Instance.Nodes[X, Y].isWall == false &&
            !ClosedList.Contains(MazeCreate.Instance.Nodes[X, Y]))
        {

            Node NeighborNode = MazeCreate.Instance.Nodes[X, Y];
            int MoveCost = CurNode.G + 1;

            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y);
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

}
