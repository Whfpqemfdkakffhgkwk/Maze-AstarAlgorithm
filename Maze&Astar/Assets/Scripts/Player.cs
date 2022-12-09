using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : Singleton<Player>
{
    public int PlayerPosX, PlayerPosY; 
    public void PlayerMove(int name)
    {
        switch (name)
        {
            case 1:
                PlayerPosY += 1;
                transform.DOMove(transform.position + new Vector3(0, 1), 0.3f);
                break;
            case 2:
                PlayerPosX -= 1;
                transform.DOMove(transform.position + new Vector3(-1, 0), 0.3f);
                break;
            case 3:
                PlayerPosY -= 1;
                transform.DOMove(transform.position + new Vector3(0, -1), 0.3f);
                break;
            case 4:
                PlayerPosX += 1;
                transform.DOMove(transform.position + new Vector3(1, 0), 0.3f);
                break;
        }
    }
}
