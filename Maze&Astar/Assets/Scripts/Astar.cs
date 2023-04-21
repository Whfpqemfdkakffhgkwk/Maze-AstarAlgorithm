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
    /// Astar�� n�ʸ��� ����ؼ� n�ʸ��� ������ �����̰� �ϴ� �ڷ�ƾ
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
        //����Ʈ �ʱ�ȭ
        OpenList = new List<Node> { StartNode };
        ClosedList = new List<Node>();
        List<Node> FinalNodeList = new List<Node>(); //������ ��� ���� ��� ����Ʈ

        //���� ����Ʈ ù ��Ҵ� ���۳��
        OpenList.Add(StartNode);

        //Ÿ�� ���� �÷��̾� ���
        TargetNode = MC.Nodes[player.PlayerPosX, player.PlayerPosY];

        //���۳�尡 �÷��̾� ����� (��, �����ߴٸ�)
        if (StartNode == TargetNode)
            return;

        while (true)
        {
            //���� ���� ���� ����� ù��°�̴� (���� ���)
            CurNode = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++)
            {
                //�� ��庸�� �� ��尡 ��ǥ������ �Ÿ��� ���ٸ�
                if (OpenList[i].H < CurNode.H)
                    CurNode = OpenList[i]; //������ ���� �� ��带 ���� ��忡 ����ش�
            }

            //�����ִ� ��忡 �����Ѵ�
            OpenList.Remove(CurNode);
            //�����ִ� ��忡 �߰��Ѵ�
            ClosedList.Add(CurNode);

            //���� ��尡 �÷��̾� �����(�� ã�Ҵٸ�)
            if (CurNode == TargetNode)
            {
                //���� ���� �̾��ֱ� ���� TargetCurNode ���
                Node TargetCurNode = TargetNode;

                //�� ó������ �� ����
                while (TargetCurNode != StartNode)
                {
                    //��� ����Ʈ���� TargetCurNode�� �־��ش�
                    FinalNodeList.Add(TargetCurNode);

                    //TargetCurNode�� �� ���� ��带 �־��ش�
                    TargetCurNode = TargetCurNode.ParentNode;

                    /* �̰� ���� �۾��̳ĸ�, 
                     * �÷��̾� ��ġ ������ ���ʴ�� �� ���� Ÿ�� �������鼭 ��� ����Ʈ�� �ϳ��� �־��ִ� �۾��̴� */
                }

                //���������� ó�� ������ ��带 �־��ش�
                FinalNodeList.Add(StartNode);
                //�׸��� ��� ����Ʈ�� �Ųٷ� �����´�
                FinalNodeList.Reverse();

                //ó�� ����� ���� ��� ��ġ�� �̵��Ѵ�
                gameObject.transform.DOMove(new Vector3(FinalNodeList[1].x, FinalNodeList[1].y), 0.4f);

                //���� �ݺ��� ���� �̵��� ��ġ ��带 ó�� ���� ��忡 �־��ش�
                StartNode = FinalNodeList[1];

                //���۳�尡 �÷��̾� ����� (��, �����ߴٸ�)
                if (StartNode == TargetNode)
                {
                    StartCoroutine(HitMotion());
                    player.Hp--;
                }

                return;
            }

            //�� �� �� ��
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

        //���� üũ�� ��尡 �� ������ �ƴҽ�, ���� �ƴҽ�, ���� ����Ʈ�� ���� ��
        if (X > 0 && Y > 0 &&
            X != MC.MazeSize &&
            Y != MC.MazeSize &&
            !MC.Nodes[X, Y].isWall &&
            !ClosedList.Contains(MC.Nodes[X, Y]))
        {
            //���� Ž���� ���� ��� (�̿� ���)
            Node NeighborNode = MC.Nodes[X, Y];

            //���� Ž���� �ϱ� ���� �̵������� �̵� ��� + 1
            int MoveCost = CurNode.G + 1;

            //�̵� ����� Ž���� ���� �̿� ��庸�� �۰ų�(�ٽ� ���ư��� �ൿ ����)
            //�����ִ� ��忡 �̿���尡 ���ٸ�
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                //�̿������ �̵� ����� +1 ����� ������ �ٲٰ�
                NeighborNode.G = MoveCost;
                //�̿������ ��ġ�� �÷��̾��� ��ġ�� ����ؼ� ��ǥ������ �Ÿ��� �־��ְ�
                NeighborNode.H = Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y);
                //�̿������ �� ���� ������ �� ���(���� ���)�� �־��ְ�
                NeighborNode.ParentNode = CurNode;

                //�����ִ� ���鿡 �̿���带 �־��ش�
                OpenList.Add(NeighborNode);
            }
        }
    }

}
