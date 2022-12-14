using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, I_Item
{
    public void UseItem()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX + j - 1, Player.Instance.PlayerPosY + i - 1] = false;
                ObjPool.GetObject(EPoolType.DeleteWall, new Vector3(Player.Instance.PlayerPosX + j - 1, Player.Instance.PlayerPosY + i - 1));
            }
        }
        Debug.Log("Boom!!!!!!");
    }
}
