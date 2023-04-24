using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, I_Item
{
    public void UseItem()
    {
        Player player = Player.Instance;
        MazeCreate MC = MazeCreate.Instance;

        //플레이어 위치를 기준으로 3X3 범위로 벽을 터트림
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                //3X3범위가 맵 밖일 경우(아래&왼쪽 , 윗쪽&오른쪽)
                if (player.PlayerPosX + j > 0 &&
                    player.PlayerPosY + i > 0 &&
                    player.PlayerPosX + j != MC.MazeSize &&
                    player.PlayerPosY + i != MC.MazeSize)
                {
                    //블록을 없애고 false로 바꿈
                    MC.Nodes[player.PlayerPosX + j, player.PlayerPosY + i].isWall = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector3(player.PlayerPosX + j, player.PlayerPosY + i));
                }
            }
        }

        StartCoroutine(player.Explode());
    }
}
