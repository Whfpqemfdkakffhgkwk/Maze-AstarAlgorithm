using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, I_Item
{
    public void UseItem()
    {
        //�÷��̾� ��ġ�� �������� 3X3 ������ ���� ��Ʈ��
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                //3X3������ �� ���� ���(�Ʒ�&���� , ����&������)
                if (Player.Instance.PlayerPosX + j > 0 &&
                    Player.Instance.PlayerPosY + i > 0 &&
                    Player.Instance.PlayerPosX + j != MazeCreate.Instance.MazeSize &&
                    Player.Instance.PlayerPosY + i != MazeCreate.Instance.MazeSize)
                {
                    //����� ���ְ� false�� �ٲ�
                    MazeCreate.Instance.Nodes[Player.Instance.PlayerPosX + j, Player.Instance.PlayerPosY + i].isWall = false;
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector3(Player.Instance.PlayerPosX + j, Player.Instance.PlayerPosY + i));
                }
            }
        }
    }
}
