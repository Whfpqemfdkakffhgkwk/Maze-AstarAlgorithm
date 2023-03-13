using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour, I_Item
{
    private int x, y;
    public void UseItem()
    {
        x = Player.Instance.PlayerPosX;
        y = Player.Instance.PlayerPosY;
        for (int i = 0; i < 7; i++)
        {
            //�������� ��� ���� �� ���������� �̵�
            if (!MazeCreate.Instance.Nodes[x + 1, y].isWall)
            {
                x += 1;
                ObjPool.GetObject(EPoolType.HintBlock, new Vector2(x, y));
            }
            //�Ǵ� ������ ��� ���� �� �������� �̵�
            else if (!MazeCreate.Instance.Nodes[x, y + 1].isWall)
            {
                y += 1;
                ObjPool.GetObject(EPoolType.HintBlock, new Vector2(x, y));
            }
            //�÷��̾ �������� �ٴ޾��� ��
            if (MazeCreate.Instance.MazeSize - 1 == x &&
                MazeCreate.Instance.MazeSize == y)
            {
                //�� �ڵ� Ŭ���� �ݺ��� �����
                break;
            }
        }
    }
}
