using DG.Tweening;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool isWall = false;

    public void PlayerMove(string name)
    {
        switch (name)
        {
            case "Up":
                print("�� ����");
                if(MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX, Player.Instance.PlayerPosY + 1])
                {
                    print("�� �κ� �� ����");
                    print(MazeCreate.Instance.Blocks[2, 1]);
                    isWall = true;
                }
                break;
            case "Down":
                if (MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX, Player.Instance.PlayerPosY - 1])
                {
                    isWall = true;
                }
                break;
            case "Left":
                if (MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX - 1, Player.Instance.PlayerPosY])
                {
                    isWall = true;
                }
                break;
            case "Right":
                if (MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX + 1, Player.Instance.PlayerPosY])
                {
                    isWall = true;
                }
                break;
        }
        if(isWall)
        {
            isWall = false;
        }
        else
        {
            print("�÷��̾� �̵�");
            Player.Instance.PlayerMove(name);
        }
    }
}
