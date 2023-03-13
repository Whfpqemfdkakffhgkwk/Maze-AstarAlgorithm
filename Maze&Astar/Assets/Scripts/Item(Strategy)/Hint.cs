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
            //오른쪽이 비어 있을 시 오른쪽으로 이동
            if (!MazeCreate.Instance.Nodes[x + 1, y].isWall)
            {
                x += 1;
                ObjPool.GetObject(EPoolType.HintBlock, new Vector2(x, y));
            }
            //또는 윗쪽이 비어 있을 시 윗쪽으로 이동
            else if (!MazeCreate.Instance.Nodes[x, y + 1].isWall)
            {
                y += 1;
                ObjPool.GetObject(EPoolType.HintBlock, new Vector2(x, y));
            }
            //플레이어가 도착점에 다달았을 시
            if (MazeCreate.Instance.MazeSize - 1 == x &&
                MazeCreate.Instance.MazeSize == y)
            {
                //이 자동 클리어 반복을 멈춘다
                break;
            }
        }
    }
}
