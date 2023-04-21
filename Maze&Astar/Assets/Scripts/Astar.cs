using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Astar : MonoBehaviour
{
    List<Node> ClosedList = new List<Node>();
    List<Node> OpenList = new List<Node>();
    Node CurNode, TargetNode, StartNode;
    private Player player;
    private MazeCreate MC;
    private void Start()
    {
        player = Player.Instance;
        MC = MazeCreate.Instance;

        StartNode = MC.Nodes[1, 0];
        StartCoroutine(AstarLoop());
    }

    /// <summary>
    /// Astar를 n초마다 계산해서 n초마다 보스를 움직이게 하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator AstarLoop()
    {
        AStarStart();
        yield return new WaitForSeconds(1);
        StartCoroutine(AstarLoop());
    }

    public void AStarStart()
    {
        //리스트 초기화
        OpenList = new List<Node> { StartNode };
        ClosedList = new List<Node>();
        List<Node> FinalNodeList = new List<Node>(); //마지막 결과 나온 노드 리스트

        //열린 리스트 첫 요소는 시작노드
        OpenList.Add(StartNode);

        //타겟 노드는 플레이어 노드
        TargetNode = MC.Nodes[player.PlayerPosX, player.PlayerPosY];

        //시작노드가 플레이어 노드라면 (즉, 도착했다면)
        if (StartNode == TargetNode)
            return;

        while (true)
        {
            //현재 노드는 열린 노드의 첫번째이다 (시작 노드)
            CurNode = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++)
            {
                //전 노드보다 현 노드가 목표까지의 거리가 적다면
                if (OpenList[i].H < CurNode.H)
                    CurNode = OpenList[i]; //정답인 노드니 그 노드를 현재 노드에 담아준다
            }

            //열려있는 노드에 삭제한다
            OpenList.Remove(CurNode);
            //닫혀있는 노드에 추가한다
            ClosedList.Add(CurNode);

            //현재 노드가 플레이어 노드라면(다 찾았다면)
            if (CurNode == TargetNode)
            {
                //다음 노드랑 이어주기 위한 TargetCurNode 노드
                Node TargetCurNode = TargetNode;

                //맨 처음까지 다 도는
                while (TargetCurNode != StartNode)
                {
                    //결과 리스트에다 TargetCurNode를 넣어준다
                    FinalNodeList.Add(TargetCurNode);

                    //TargetCurNode에 그 전의 노드를 넣어준다
                    TargetCurNode = TargetCurNode.ParentNode;

                    /* 이게 무슨 작업이냐면, 
                     * 플레이어 위치 노드부터 차례대로 전 노드로 타고 내려가면서 결과 리스트에 하나씩 넣어주는 작업이다 */
                }

                //마지막으로 처음 시작한 노드를 넣어준다
                FinalNodeList.Add(StartNode);
                //그리고 결과 리스트를 거꾸로 뒤집는다
                FinalNodeList.Reverse();

                //처음 노드의 다음 노드 위치로 이동한다
                gameObject.transform.DOMove(new Vector3(FinalNodeList[1].x, FinalNodeList[1].y), 0.4f);

                //다음 반복을 위해 이동한 위치 노드를 처음 시작 노드에 넣어준다
                StartNode = FinalNodeList[1];

                //시작노드가 플레이어 노드라면 (즉, 도착했다면)
                if (StartNode == TargetNode)
                {
                    StartCoroutine(HitMotion());
                    player.Hp--;
                }

                return;
            }

            //↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);

        }



        IEnumerator HitMotion()
        {
            Camera.main.gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>().color.value = Color.red;
            yield return new WaitForSeconds(0.2f);
            Camera.main.gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>().color.value = Color.black;
        }
    }

    void OpenListAdd(int X, int Y)
    {

        //다음 체크할 노드가 밖 범위가 아닐시, 벽이 아닐시, 닫힌 리스트에 없을 시
        if (X > 0 && Y > 0 &&
            X != MC.MazeSize &&
            Y != MC.MazeSize &&
            !MC.Nodes[X, Y].isWall &&
            !ClosedList.Contains(MC.Nodes[X, Y]))
        {
            //다음 탐색에 들어온 노드 (이웃 노드)
            Node NeighborNode = MC.Nodes[X, Y];

            //다음 탐색을 하기 위해 이동했으니 이동 비용 + 1
            int MoveCost = CurNode.G + 1;

            //이동 비용이 탐색에 들어온 이웃 노드보다 작거나(다시 돌아가는 행동 방지)
            //열려있는 노드에 이웃노드가 없다면
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                //이웃노드의 이동 비용을 +1 해줬던 값으로 바꾸고
                NeighborNode.G = MoveCost;
                //이웃노드의 위치와 플레이어의 위치를 계산해서 목표까지의 거리에 넣어주고
                NeighborNode.H = Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y);
                //이웃노드의 전 노드는 들어오기 전 노드(현재 노드)로 넣어주고
                NeighborNode.ParentNode = CurNode;

                //열려있는 노드들에 이웃노드를 넣어준다
                OpenList.Add(NeighborNode);
            }
        }
    }

}
