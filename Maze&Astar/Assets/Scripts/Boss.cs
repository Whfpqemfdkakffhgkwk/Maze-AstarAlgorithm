using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private int CurPosX = 1, CurPosY = 0;

    private void Start()
    {
        HCount();
    }
    void Astar()
    {
        while (true)
        {
            if (MazeCreate.Instance.MazeSize - 1 == CurPosX &&
                MazeCreate.Instance.MazeSize == CurPosY)
            {
                break;
            }
            //H계산

            for (int i = 1; i < 5; i++)
            {
                switch (i)
                {
                    case 1:
                        //CurPos + 방향
                        if (CurPosX > 0 &&
                            CurPosY > 0 &&
                            CurPosX != MazeCreate.Instance.MazeSize &&
                            CurPosY != MazeCreate.Instance.MazeSize &&
                            MazeCreate.Instance.Nodes[CurPosX, CurPosY].isWall == false)
                        {

                        }
                        break;
                }
            }
        }
    }
    void HCount()
    {
        nodes[0].H = Mathf.Abs(CurPosX - Player.Instance.PlayerPosX) + Mathf.Abs(CurPosY + 1 - Player.Instance.PlayerPosY);
        nodes[1].H = Mathf.Abs(CurPosX - Player.Instance.PlayerPosX) + Mathf.Abs(CurPosY - 1 - Player.Instance.PlayerPosY);
        nodes[2].H = Mathf.Abs(CurPosX - 1 - Player.Instance.PlayerPosX) + Mathf.Abs(CurPosY - Player.Instance.PlayerPosY);
        nodes[3].H = Mathf.Abs(CurPosX + 1 - Player.Instance.PlayerPosX) + Mathf.Abs(CurPosY - Player.Instance.PlayerPosY);
    }
    public void Move(int name)
    {
        switch (name)
        {
            case 1:
                gameObject.transform.rotation = Quaternion.Euler(270, -90, 90);
                transform.DOMove(transform.position + new Vector3(0, 1), 0.3f);
                break;
            case 2:
                transform.DOMove(transform.position + new Vector3(-1, 0), 0.3f);
                break;
            case 3:
                transform.DOMove(transform.position + new Vector3(0, -1), 0.3f);
                break;
            case 4:
                transform.DOMove(transform.position + new Vector3(1, 0), 0.3f);
                break;
        }
    }
}
