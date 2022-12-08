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
                print("업 들어옴");
                if(MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX, Player.Instance.PlayerPosY + 1])
                {
                    print("업 부분 벽 닿음");
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
            print("플레이어 이동");
            Player.Instance.PlayerMove(name);
        }
    }
}
