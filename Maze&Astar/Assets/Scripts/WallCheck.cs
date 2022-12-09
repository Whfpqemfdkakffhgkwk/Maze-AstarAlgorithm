using DG.Tweening;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool isWall = false;

    public void PlayerMove(int name)
    {
        switch (name)
        {
            case 1:
                if(MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX, Player.Instance.PlayerPosY + 1])
                {
                    isWall = true;
                }
                break;
            case 2:
                if (MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX - 1, Player.Instance.PlayerPosY])
                {
                    isWall = true;
                }
                break;
            case 3:
                if(Player.Instance.PlayerPosY - 1 >= 0)
                {
                    if (MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX, Player.Instance.PlayerPosY - 1])
                    {
                        isWall = true;
                    }
                }
                else
                {
                    isWall = true;
                }
                break;
            case 4:
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
            Player.Instance.PlayerMove(name);
        }
    }
}
