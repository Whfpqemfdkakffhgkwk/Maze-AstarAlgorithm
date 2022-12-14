using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, I_Item
{
    public void UseItem()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (Player.Instance.PlayerPosX + j > 0 &&
                    Player.Instance.PlayerPosY + i > 0 &&
                    Player.Instance.PlayerPosX + j != MazeCreate.Instance.MazeSize &&
                    Player.Instance.PlayerPosY + i != MazeCreate.Instance.MazeSize)
                {
                    MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX + j, Player.Instance.PlayerPosY + i] = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector3(Player.Instance.PlayerPosX + j, Player.Instance.PlayerPosY + i));
                }
            }
        }
        Debug.Log("Boom!!!!!!");
    }
}
