using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreate : MonoBehaviour
{
    [SerializeField] private GameObject MazeStartingPoint, Wall, DeleteWall;

    [SerializeField] private int MazeSize = 100;


    private void Start()
    {
        int Size = MazeSize + 1;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if(i % 2 == 0 || j % 2 == 0)
                {
                    //���ڹ��̷� ������ ������
                    ObjPool.GetObject(EPoolType.Wall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j));
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

                if(i == Size - 2)
                {
                    //������ �κ� y�� �� �վ���
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j + 1));
                    continue;
                }
                if(j == Size - 2)
                {
                    //������ �κ� x�� �� �վ���
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i + 1, MazeStartingPoint.transform.position.y + j));
                    continue;
                }

                if(Random.Range(0, 2) == 0)
                {
                    //�����ϰ� x�� �������� �վ���
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j + 1));
                }
                else
                {
                    //�����ϰ� y�� �������� �վ���
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i + 1, MazeStartingPoint.transform.position.y + j));
                }
            }
        }
        //�������� �վ���
        ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y));
    }
}

//ASTAR
//namespace Map.PathFinding
//{
//    public static class AStar
//    {
//        /// <summary>
//        /// ��ã��
//        /// </summary>
//        /// <param name="board">��</param>
//        /// <param name="start">���� ��ǥ</param>
//        /// <param name="dest">���� ��ǥ</param>
//        /// <returns>������ ��ȯ</returns>
//        public static Block PathFinding(this Board board, Block start, Block dest)
//        {
//            if (board.Exists(start) && board.Exists(dest))
//            {
//                board.CheckClear();

//                List<Block> waittingBlocks = new List<Block>();
//                List<Block> finishedBlocks = new List<Block>();

//                Block current = start;

//                while (current != null)
//                {
//                    // �ֺ� �� ��������
//                    var aroundBlocks = board.GetAroundBlocks(current);

//                    for (int i = 0; i < aroundBlocks.Count; i++)
//                    {
//                        var block = aroundBlocks[i];
//                        if (!waittingBlocks.Equals(block) && !block.check)
//                            waittingBlocks.Add(block);
//                    }

//                    // �˻� �Ϸ� ����Ʈ�� �̰�
//                    current.check = true;
//                    if (waittingBlocks.Remove(current))
//                        finishedBlocks.Add(current);

//                    // �̵� �Ұ� ��, ��ã�� ���� ó��
//                    if (aroundBlocks.Count == 0)
//                        return null;
//                    else
//                    {
//                        // �θ� ����
//                        aroundBlocks = aroundBlocks.FindAll(block => !block.check);
//                    }

//                    // �ֺ� �� ���� ���
//                    CalcRating(aroundBlocks, start, current, dest);

//                    // ���� �˻� ��� ��������
//                    current = GetNextBlock(aroundBlocks, current);
//                    if (current == null)
//                    {
//                        // ���� ��� Ž�� ���� ��, ó������ �����
//                        current = GetPriorityBlock(waittingBlocks);

//                        // ���̻� �˻��� ���� ���ٸ�(���� ã�� �� �� ��� �ش�),
//                        // ��ã�� ���� �� �������� ���� ����� ���� ������ �ȳ�.
//                        if (current == null)
//                        {
//                            Block exceptionBlock = null;
//                            for (int i = 0; i < finishedBlocks.Count; i++)
//                            {
//                                if (exceptionBlock == null || exceptionBlock.H > finishedBlocks[i].H)
//                                    exceptionBlock = finishedBlocks[i];
//                            }
//                            current = exceptionBlock;
//                            break;
//                        }
//                    }
//                    else if (current.Equals(dest))
//                    {
//                        break;
//                    }
//                }

//                // ������ ���� ������ش�.
//                while (!current.Equals(start))
//                {
//                    current.prev.next = current;
//                    current = current.prev;
//                }

//                start.prev = null;
//                return start;
//            }
//            return null;
//        }

//        /// <summary>
//        /// �˻� ��� �� �߿��� ������ ���� ���� ���� ��������
//        /// </summary>
//        /// <param name="waittingBlocks"></param>
//        /// <returns></returns>
//        private static Block GetPriorityBlock(List<Block> waittingBlocks)
//        {
//            // �� ��ġ�� ���� ������ ���� ���� ���� ��ȯ��.
//            Block block = null;
//            var enumerator = waittingBlocks.GetEnumerator();
//            while (enumerator.MoveNext())
//            {
//                var current = enumerator.Current;
//                if (block == null || block.F < current.F)
//                {
//                    block = current;
//                }
//            }

//            return block;
//        }

//        /// <summary>
//        /// ���� �̵� ��� ��������
//        /// </summary>
//        /// <param name="arounds"></param>
//        /// <param name="current"></param>
//        /// <returns></returns>
//        private static Block GetNextBlock(List<Block> arounds, Block current)
//        {
//            Block minValueBlock = null;
//            for (int i = 0; i < arounds.Count; i++)
//            {
//                Block next = arounds[i];
//                if (!next.check)
//                {
//                    // ���� ��� �̵��� �ؾ��ϴ�, ���������κ����� ������ �� ���� ����� Ž���Ѵ�.
//                    if (minValueBlock == null)
//                    {
//                        minValueBlock = next;
//                    }
//                    else if (minValueBlock.H > next.H)
//                    {
//                        minValueBlock = next;
//                    }
//                }
//            }
//            return minValueBlock;
//        }

//        /// <summary>
//        /// �ֺ� ��� ���� ����ϱ�
//        /// </summary>
//        /// <param name="arounds">�ֺ� ��� ����Ʈ</param>
//        /// <param name="start">���� ���</param>
//        /// <param name="current">���� ��ġ ���</param>
//        /// <param name="dest">���� ���</param>
//        private static void CalcRating(List<Block> arounds, Block start, Block current, Block dest)
//        {
//            if (arounds != null)
//            {
//                for (int i = 0; i < arounds.Count; i++)
//                {
//                    var block = arounds[i];
//                    bool isDiagonalBlock = Math.Abs(block.x - current.x) == 1 && Math.Abs(block.y - current.y) == 1;
//                    int priceFromDest = (Math.Abs(dest.x - block.x) + Math.Abs(dest.y - block.y)) * 10;
//                    if (block.prev == null)
//                        block.prev = current;
//                    block.SetPrice(current.G + (isDiagonalBlock ? 14 : 10), priceFromDest);
//                }
//            }
//        }
//    }
//}
