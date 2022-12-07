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
                print("업 들어옴");
                if(MazeCreate.Instance.Blocks[PlayerPosY + 1, PlayerPosX] == true)
                {
                    print("업 부분 벽 닿음");
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
            print("플레이어 이동");
            base.PlayerMove(name);
        }
    }
}
