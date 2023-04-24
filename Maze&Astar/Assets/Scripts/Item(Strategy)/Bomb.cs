using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, I_Item
{
    public void UseItem()
    {
        Player player = Player.Instance;
        MazeCreate MC = MazeCreate.Instance;

        //�÷��̾� ��ġ�� �������� 3X3 ������ ���� ��Ʈ��
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                //3X3������ �� ���� ���(�Ʒ�&���� , ����&������)
                if (player.PlayerPosX + j > 0 &&
                    player.PlayerPosY + i > 0 &&
                    player.PlayerPosX + j != MC.MazeSize &&
                    player.PlayerPosY + i != MC.MazeSize)
                {
                    //����� ���ְ� false�� �ٲ�
                    MC.Nodes[player.PlayerPosX + j, player.PlayerPosY + i].isWall = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector3(player.PlayerPosX + j, player.PlayerPosY + i));
                }
            }
        }

        StartCoroutine(player.Explode());
    }
}
