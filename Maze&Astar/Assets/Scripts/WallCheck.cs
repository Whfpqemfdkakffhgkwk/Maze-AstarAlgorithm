using DG.Tweening;
using UnityEngine;

public class WallCheck : Player
{
    public bool isWall = false;

    public override void PlayerMove(string name)
    {
        switch (name)
        {
            case "Up":
                print("�� ����");
                if(MazeCreate.Instance.Blocks[PlayerPosY + 1, PlayerPosX] == true)
                {
                    print("�� �κ� �� ����");
                    print(MazeCreate.Instance.Blocks[PlayerPosY + 1, PlayerPosX]);
                    isWall = true;
                }
                break;
            case "Down":
                if (MazeCreate.Instance.Blocks[PlayerPosY - 1, PlayerPosX])
                {
                    isWall = true;
                }
                break;
            case "Left":
                if (MazeCreate.Instance.Blocks[PlayerPosY, PlayerPosX - 1])
                {
                    isWall = true;
                }
                break;
            case "Right":
                if (MazeCreate.Instance.Blocks[PlayerPosY, PlayerPosX + 1])
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
            base.PlayerMove(name);
        }
    }
}
