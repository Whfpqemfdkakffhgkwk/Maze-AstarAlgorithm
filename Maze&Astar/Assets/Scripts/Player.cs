using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : Singleton<Player>
{
    public int PlayerPosX, PlayerPosY; 
    public virtual void PlayerMove(string name)
    {
        switch (name)
        {
            case "Up":
                print("플레이어 행동");
                PlayerPosY += 1;
                transform.DOMove(transform.position + new Vector3(0, 1), 0.3f);
                break;
            case "Down":
                PlayerPosY -= 1;
                transform.DOMove(transform.position + new Vector3(0, -1), 0.3f);
                break;
            case "Left":
                PlayerPosX += 1;
                transform.DOMove(transform.position + new Vector3(-1, 0), 0.3f);
                break;
            case "Right":
                PlayerPosX -= 1;
                transform.DOMove(transform.position + new Vector3(1, 0), 0.3f);
                break;
        }
    }
}
